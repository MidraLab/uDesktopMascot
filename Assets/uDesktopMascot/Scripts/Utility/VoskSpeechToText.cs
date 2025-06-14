using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using Unity.Profiling;
using UnityEngine;
using Newtonsoft.Json;
using Vosk;

namespace uDesktopMascot
{
    /// <summary>
    ///   Vosk を利用したオフライン音声認識。確定テキストを OnSpeechRecognized で通知します。
    /// </summary>
    public class VoskSpeechToText : MonoBehaviour
    {
        [Tooltip("StreamingAssets からの相対パス、または展開済みモデルフォルダ名")]
        public string ModelPath = "vosk-model-small-ja-0.22";

        [Tooltip("マイク入力用コンポーネント")]
        public VoiceProcessor VoiceProcessor;

        [Tooltip("候補として取得する最大アルタナティブ数")]
        public int MaxAlternatives = 1;

        public bool AutoStart = true;

        public List<string> KeyPhrases = new();

        public Action<string> OnSpeechRecognized;
        public Action<string> OnStatusUpdated;

        [Tooltip("無音と判定する秒数（VAD）。この時間だけ音声が途切れたら確定文を送信")] 
        public float VadSilenceSeconds = 1.0f;

        private VoskRecognizer _recognizer;
        private Model _model;
        private bool _recognizerReady;

        private string _modelAbsolutePath;
        private string _grammar;

        private bool _isInitializing;
        private bool _didInit;
        private bool _running;

        private readonly ConcurrentQueue<short[]> _micQueue = new();
        private readonly ConcurrentQueue<string> _resultQueue = new();

        [SerializeField] private TMP_Text recognizerStatusText;

        private static readonly ProfilerMarker markerCreate = new("VoskRecognizer.Create");
        private static readonly ProfilerMarker markerAccept = new("VoskRecognizer.AcceptWaveform");

        private float _silenceTimer = 0f;
        private string _accumulatedText = string.Empty;

        private void Start()
        {
            if (AutoStart) StartVoskStt();
        }

        public void StartVoskStt(List<string> keyPhrases = null, string modelPath = null, bool startMic = false)
        {
            if (_isInitializing) { Debug.LogError("Vosk init is running"); return; }
            if (_didInit) { Debug.Log("Vosk already initialized"); return; }

            if (!string.IsNullOrEmpty(modelPath)) ModelPath = modelPath;
            if (keyPhrases != null) KeyPhrases = keyPhrases;

            StartCoroutine(InitializeCoroutine(startMic));
        }

        private IEnumerator InitializeCoroutine(bool startMic)
        {
            _isInitializing = true;
            yield return WaitMicReady();
            yield return LocateModelCoroutine();

            OnStatusUpdated?.Invoke($"Loading model: {_modelAbsolutePath}");
            _model = new Model(_modelAbsolutePath);

            _isInitializing = false;
            _didInit = true;

            SetupEvents();

            if (startMic) ToggleRecording();
        }

        private void SetupEvents()
        {
            if (VoiceProcessor == null)
            {
                Debug.LogError("VoiceProcessor is null");
                return;
            }
            VoiceProcessor.OnFrameCaptured += samples => _micQueue.Enqueue(samples);
            VoiceProcessor.OnRecordingStop += () => Debug.Log("Mic stopped");
        }

        private IEnumerator WaitMicReady()
        {
            while (Microphone.devices.Length == 0) yield return null;
        }

        private IEnumerator LocateModelCoroutine()
        {
            string streaming = Path.Combine(Application.streamingAssetsPath, ModelPath);
            if (Directory.Exists(streaming)) { _modelAbsolutePath = streaming; yield break; }
            string persistent = Path.Combine(Application.persistentDataPath, ModelPath);
            if (Directory.Exists(persistent)) { _modelAbsolutePath = persistent; yield break; }
            Debug.LogError($"Model not found: {ModelPath}");
        }

        public void ToggleRecording()
        {
            if (VoiceProcessor == null) return;
            if (!VoiceProcessor.IsRecording)
            {
                _running = true;
                VoiceProcessor.StartRecording();
                Task.Run(ProcessAudioLoop);
            }
            else
            {
                _running = false;
                VoiceProcessor.StopRecording();
            }
        }

        private async Task ProcessAudioLoop()
        {
            markerCreate.Begin();
            if (!_recognizerReady)
            {
                UpdateGrammar();
                float sr = VoiceProcessor != null ? VoiceProcessor.SampleRate : 16000f;
                _recognizer = string.IsNullOrEmpty(_grammar)
                    ? new VoskRecognizer(_model, sr)
                    : new VoskRecognizer(_model, sr, _grammar);
                _recognizer.SetMaxAlternatives(MaxAlternatives);
                _recognizerReady = true;
            }
            markerCreate.End();

            markerAccept.Begin();
            while (_running)
            {
                if (_micQueue.TryDequeue(out var data))
                {
                    if (_recognizer.AcceptWaveform(data, data.Length))
                    {
                        _resultQueue.Enqueue(_recognizer.Result());
                    }
                }
                else
                {
                    // パーシャル結果を確認
                    var partial = _recognizer.PartialResult();
                    if (!string.IsNullOrEmpty(partial)) _resultQueue.Enqueue(partial);
                    await Task.Delay(20);
                }
            }
            markerAccept.End();
        }

        private void UpdateGrammar()
        {
            if (KeyPhrases.Count == 0) { _grammar = string.Empty; return; }
            var list = new List<string>(KeyPhrases.Count + 1);
            foreach (var p in KeyPhrases) list.Add(p.ToLower());
            list.Add("[unk]");
            _grammar = JsonConvert.SerializeObject(list);
        }

        private void Update()
        {
            if (_resultQueue.TryDequeue(out var json))
            {
                var res = new RecognitionResult(json);
                if (string.IsNullOrEmpty(res.BestText)) return;

                if (res.Partial)
                {
                    Debug.Log($"[STT][partial] {res.BestText}");
                }
                else
                {
                    Debug.Log($"[STT][final] {res.BestText}");
                    _accumulatedText = string.IsNullOrEmpty(_accumulatedText)
                        ? res.BestText
                        : ($"{_accumulatedText} {res.BestText}").Trim();
                    _silenceTimer = 0f; // 話し続けているのでリセット
                }
            }

            // VAD: 無音が続いたら送信
            if (!string.IsNullOrEmpty(_accumulatedText))
            {
                _silenceTimer += Time.deltaTime;
                if (_silenceTimer >= VadSilenceSeconds)
                {
                    Debug.Log($"[STT][send] {_accumulatedText}");
                    OnSpeechRecognized?.Invoke(_accumulatedText);
                    _accumulatedText = string.Empty;
                    _silenceTimer = 0f;
                }
            }
        }
    }
} 
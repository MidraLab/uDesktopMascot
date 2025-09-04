using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using LLMUnity;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using Unity.Logging;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using uPiper.Core;
using uPiper.Core.AudioGeneration;
using uPiper.Core.Logging;
using uPiper.Core.Phonemizers;
using Unity.InferenceEngine;
using Newtonsoft.Json.Linq;
using System.Linq;
using uPiper.Core.Phonemizers.Implementations;

namespace uDesktopMascot
{
    /// <summary>
    /// チャットダイアログ
    /// </summary>
    public class ChatDialog : DialogBase
    {
        /// <summary>
        /// チャットダイアログの入力フィールド
        /// </summary>
        [SerializeField] private TMP_InputField inputField;

        /// <summary>
        /// チャットダイアログの送信ボタン
        /// </summary>
        [SerializeField] private Button sendButton;
        
        /// <summary>
        /// チャットダイアログのスクロールビュー
        /// </summary>
        [SerializeField] private ScrollRect scrollRect;

        /// <summary>
        /// チャットダイアログのテキスト表示
        /// </summary>
        [SerializeField] private TextMeshProUGUI chatText;

        /// <summary>
        /// LLMキャラクター
        /// </summary>
        [SerializeField] private LLMCharacter llmCharacter;

        /// <summary>
        /// チャット履歴テキストビルダー
        /// </summary>
        private readonly StringBuilder _chatTextBuilder = new StringBuilder();
        
        /// <summary>
        /// TTSのAudioSource
        /// </summary>
        [SerializeField] private AudioSource ttsAudioSource;
        
        [SerializeField] private Button microphoneButton;
        
        [SerializeField] private SwitchMicrophoneIcon microphoneIcon;

        /// <summary>
        /// AIの返信を蓄積するビルダー
        /// </summary>
        private StringBuilder _replyTextBuilder;
        
        /// <summary>
        /// チャットダイアログのオーバーレイ通知イメージ
        /// </summary>
        [SerializeField] private Image overNoticeImage;
        
        /// <summary>
        /// チャットダイアログのオーバーレイ通知ローカライズされた文字列イベント
        /// </summary>
        [SerializeField] private LocalizeStringEvent overNoticeLocalizedStringEvent;
        
        /// <summary>
        /// チャットダイアログのオーバーレイ通知テキスト
        /// </summary>
        [SerializeField] private TextMeshProUGUI overNoticeText;

        /// <summary>
        /// 入力をブロックするフラグ
        /// </summary>
        private bool _inputBlocked = false;

        /// <summary>
        /// 前回の返信の長さを記録する変数
        /// </summary>
        private int _lastReplyLength = 0;
        
        /// <summary>
        /// マイクがオンかどうかのフラグ
        /// </summary>
        private bool _isMiscrophoneOn = false;

        [SerializeField] private VoskSpeechToText speechToText;

        private string _lastVoiceMessage = string.Empty;
        
        // 音声合成用のフィールド
        private InferenceAudioGenerator _ttsGenerator;
        private PhonemeEncoder _phonemeEncoder;
        private AudioClipBuilder _audioClipBuilder;
        private OpenJTalkPhonemizer _japanesePhonemizer;
        private PiperVoiceConfig _ttsVoiceConfig;
        private bool _isTTSInitialized = false;

        private void Start()
        {
            SetEvents();
            // マイク音声認識イベント購読
            if (speechToText != null)
            {
                speechToText.OnSpeechRecognized += OnSpeechRecognized;
            }
            // アイコン初期化
            microphoneIcon.SwitchIcon(_isMiscrophoneOn);
            
            // AudioSourceが設定されていない場合、自動作成
            if (ttsAudioSource == null)
            {
                PiperLogger.LogWarning("[ChatDialog] TTS AudioSource not assigned. Creating one automatically.");
                ttsAudioSource = gameObject.AddComponent<AudioSource>();
                ttsAudioSource.playOnAwake = false;
                ttsAudioSource.volume = 1.0f;
                PiperLogger.LogInfo("[ChatDialog] Created AudioSource for TTS");
            }
            
            // 音声合成の初期化
            InitializeTTS();
        }

        private protected override void OnEnable()
        {
            base.OnEnable();
            // Submitアクションにリスナーを追加
            InputController.Instance.UI.Submit.performed += OnSubmit;
        }

        private protected override void OnDisable()
        {
            base.OnDisable();
            // Submitアクションのリスナーを削除
            InputController.Instance.UI.Submit.performed -= OnSubmit;
        }
        
        /// <summary>
        /// ダイアログを表示する
        /// </summary>
        public override void Show()
        {
            base.Show();
            SwitchModelDownloadState();
        }
        
        /// <summary>
        /// モデルのダウンロード状況によって表示を切り替える
        /// </summary>
        private void SwitchModelDownloadState()
        {
            switch (ModelDownloader.ModelDownloadProgressEnum)
            {
                case ModelDownloadProgressEnum.ProgressChanged: 
                    ShowOverNotice("モデルのダウンロード中...");
                    break;
                case ModelDownloadProgressEnum.DownloadCompleted: 
                    HideOverNotice();
                    break;
                case ModelDownloadProgressEnum.DownloadFailed: 
                    ShowOverNotice("モデルのダウンロードに失敗しました。");
                    break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Submitアクションが実行されたときの処理（Enterキー）
        /// </summary>
        private void OnSubmit(InputAction.CallbackContext context)
        {
            // 入力フィールドが選択されている場合のみ処理
            if (inputField.isFocused)
            {
                SendMessages();

                // InputFieldが改行を追加しないようにする
                inputField.DeactivateInputField();
                inputField.ActivateInputField();
            }
        }
        
        /// <summary>
        /// チャットダイアログを表示する
        /// </summary>
        private void ScrollToBottom()
        {
            // レイアウトを強制的に更新
            Canvas.ForceUpdateCanvases();
            // ScrollRectのverticalNormalizedPositionを0に設定（0が一番下、1が一番上）
            scrollRect.verticalNormalizedPosition = 0f;
            // レイアウトを再度更新
            Canvas.ForceUpdateCanvases();
        }

        /// <summary>
        /// メッセージを送信する
        /// </summary>
        private void SendMessages()
        {
            if (_inputBlocked || string.IsNullOrWhiteSpace(inputField.text))
            {
                return;
            }

            // 入力をブロック
            _inputBlocked = true;
            sendButton.interactable = false;
            inputField.interactable = false;

            // ユーザーのメッセージをチャット履歴に追加
            string userMessage = inputField.text;
            _chatTextBuilder.AppendLine($"あなた: {userMessage}");
            chatText.text = _chatTextBuilder.ToString();

            // ScrollToBottomを呼び出して最新のメッセージを表示
            ScrollToBottom();

            // 入力フィールドをクリア
            inputField.text = string.Empty;

            // AIの返信用のStringBuilderを初期化
            _replyTextBuilder = new StringBuilder();

            // 前回の返信の長さをリセット
            _lastReplyLength = 0;

            // LLMにユーザーのメッセージを送信し、返信を処理
            _ = ReceiveAIResponse(userMessage);
        }

        /// <summary>
        /// 非同期でAIの返信を受信
        /// </summary>
        private async UniTask ReceiveAIResponse(string userMessage)
        {
            try
            {
                // llmCharacter.Chat を呼び出し、返信を受信
                await llmCharacter.Chat(
                    userMessage,
                    HandleReply,
                    ReplyCompleted
                );
            }
            catch (Exception ex)
            {
                Log.Error($"AIの返信の受信中にエラーが発生しました。{ex.Message}");
                // エラーが発生した場合、入力をアンブロック
                _inputBlocked = false;
                sendButton.interactable = true;
                inputField.interactable = true;
            }
        }

        /// <summary>
        /// AIの返信を処理する（ストリーミング対応）
        /// </summary>
        /// <param name="reply">累積されたAIからの返信</param>
        private void HandleReply(string reply)
        {
            // 新しく追加された部分のみを取得
            string newText = reply.Substring(_lastReplyLength);
            _lastReplyLength = reply.Length;

            // AIの返信をビルダーに追加
            _replyTextBuilder.Append(newText);

            // 現在のチャット履歴と進行中のAI返信を表示
            using (var sb = ZString.CreateStringBuilder())
            {
                sb.Append(_chatTextBuilder.ToString());
                sb.Append($"AI: {_replyTextBuilder}");
                chatText.text = sb.ToString();
            }

            // ScrollToBottomを呼び出して最新のメッセージを表示
            ScrollToBottom();
        }

        /// <summary>
        /// AIの返信が完了したときの処理
        /// </summary>
        private void ReplyCompleted()
        {
            // 最終的なAIの返信をチャット履歴に追加
            var aiReplyText = _replyTextBuilder?.ToString() ?? "";
            _chatTextBuilder.AppendLine($"AI: {aiReplyText}");
            chatText.text = _chatTextBuilder.ToString();

            // ScrollToBottomを呼び出して最新のメッセージを表示
            ScrollToBottom();
            
            // 音声合成を実行
            if (_isTTSInitialized && !string.IsNullOrWhiteSpace(aiReplyText))
            {
                PiperLogger.LogInfo($"[ChatDialog] Starting TTS for reply: {aiReplyText.Substring(0, Math.Min(50, aiReplyText.Length))}...");
                _ = SynthesizeAndPlayTTS(aiReplyText);
            }
            else
            {
                PiperLogger.LogWarning($"[ChatDialog] TTS not executed. Initialized: {_isTTSInitialized}, Text empty: {string.IsNullOrWhiteSpace(aiReplyText)}");
            }

            // AIの返信用ビルダーをクリア
            _replyTextBuilder = null;

            // 入力をアンブロック
            _inputBlocked = false;
            sendButton.interactable = true;
            inputField.interactable = true;

            // 入力フィールドにフォーカスをセット
            inputField.ActivateInputField();

            // ユーザがマイク ON であれば録音再開
            if (_isMiscrophoneOn)
            {
                speechToText?.ResumeRecording();
            }
        }

        /// <summary>
        /// イベントを設定する
        /// </summary>
        private void SetEvents()
        {
            sendButton.onClick.AddListener(SendMessages);
            microphoneButton.onClick.AddListener(() =>
            {
                _isMiscrophoneOn = !_isMiscrophoneOn;
                microphoneIcon.SwitchIcon(_isMiscrophoneOn);

                if (speechToText == null) return;

                // 録音状態をトグル
                speechToText.ToggleRecording();

                // マイクONの間はテキスト入力を無効化する
                inputField.interactable = !_isMiscrophoneOn;
            });
        }
        
        /// <summary>
        /// オーバーレイ通知を表示する
        /// </summary>
        /// <param name="notice"></param>
        public void ShowOverNotice(string notice)
        {
            overNoticeLocalizedStringEvent.StringReference.Arguments = new object[] {notice};
            overNoticeLocalizedStringEvent.StringReference.RefreshString();
            overNoticeImage.enabled = true;
            overNoticeText.enabled = true;
        }
        
        /// <summary>
        /// オーバーレイ通知を非表示にする
        /// </summary>
        public void HideOverNotice()
        {
            overNoticeText.enabled = false;
            overNoticeImage.enabled = false;
        }

        /// <summary>
        /// 音声合成を初期化する
        /// </summary>
        private async void InitializeTTS()
        {
            try
            {
                PiperLogger.LogInfo("[ChatDialog] Starting TTS initialization...");
                
                // 初期化用インスタンスを作成
                _ttsGenerator = new InferenceAudioGenerator();
                _audioClipBuilder = new AudioClipBuilder();
                
                // OpenJTalk phonemizerを初期化
                try
                {
                    _japanesePhonemizer = new OpenJTalkPhonemizer();
                    PiperLogger.LogInfo("[ChatDialog] OpenJTalk phonemizer initialized successfully");
                }
                catch (Exception ex)
                {
                    PiperLogger.LogError($"[ChatDialog] Failed to initialize OpenJTalk: {ex.Message}");
                    _japanesePhonemizer = null;
                    return;
                }
                
                // 日本語モデルをロード
                var modelName = "ja_JP-test-medium";
                PiperLogger.LogDebug($"[ChatDialog] Loading model: {modelName}");
                
                // モデルアセットをロード
                var modelAsset = Resources.Load<ModelAsset>($"uPiper/Models/{modelName}");
                if (modelAsset == null)
                {
                    PiperLogger.LogError($"[ChatDialog] Model not found: {modelName}");
                    return;
                }
                
                // JSONコンフィグをロード（Resources.Loadでは拡張子を含めない）
                var jsonAsset = Resources.Load<TextAsset>($"uPiper/Models/{modelName}.onnx");
                if (jsonAsset == null)
                {
                    PiperLogger.LogError($"[ChatDialog] Config not found: {modelName}.onnx.json");
                    return;
                }
                
                // コンフィグをパース
                _ttsVoiceConfig = ParseTTSConfig(jsonAsset.text, modelName);
                _phonemeEncoder = new PhonemeEncoder(_ttsVoiceConfig);
                
                // ジェネレーターを初期化（CPUバックエンドを使用）
                var piperConfig = new PiperConfig
                {
                    Backend = InferenceBackend.CPU,
                    AllowFallbackToCPU = true
                };
                
                await _ttsGenerator.InitializeAsync(modelAsset, _ttsVoiceConfig, piperConfig);
                _isTTSInitialized = true;
                
                PiperLogger.LogInfo("[ChatDialog] TTS initialization completed successfully");
                PiperLogger.LogInfo($"[ChatDialog] AudioSource assigned: {ttsAudioSource != null}");
                
                // AudioSourceの設定を確認
                if (ttsAudioSource != null)
                {
                    PiperLogger.LogInfo($"[ChatDialog] AudioSource settings - Volume: {ttsAudioSource.volume}, Mute: {ttsAudioSource.mute}, Enabled: {ttsAudioSource.enabled}");
                }
            }
            catch (Exception ex)
            {
                PiperLogger.LogError($"[ChatDialog] TTS initialization failed: {ex.Message}");
                _isTTSInitialized = false;
            }
        }
        
        /// <summary>
        /// TTS用のコンフィグをパースする
        /// </summary>
        private PiperVoiceConfig ParseTTSConfig(string json, string modelName)
        {
            var config = new PiperVoiceConfig
            {
                VoiceId = modelName,
                DisplayName = modelName,
                Language = "ja",
                SampleRate = 22050,
                PhonemeIdMap = new System.Collections.Generic.Dictionary<string, int>()
            };
            
            try
            {
                var jsonObj = JObject.Parse(json);
                
                // 言語コードを抽出
                if (jsonObj["language"]?["code"] != null)
                {
                    config.Language = jsonObj["language"]["code"].ToString();
                }
                
                // サンプルレートを抽出
                if (jsonObj["audio"]?["sample_rate"] != null)
                {
                    config.SampleRate = jsonObj["audio"]["sample_rate"].ToObject<int>();
                }
                
                // 推論パラメータを抽出
                if (jsonObj["inference"]?["noise_scale"] != null)
                {
                    config.NoiseScale = jsonObj["inference"]["noise_scale"].ToObject<float>();
                }
                if (jsonObj["inference"]?["length_scale"] != null)
                {
                    config.LengthScale = jsonObj["inference"]["length_scale"].ToObject<float>();
                }
                if (jsonObj["inference"]?["noise_w"] != null)
                {
                    config.NoiseW = jsonObj["inference"]["noise_w"].ToObject<float>();
                }
                
                // phoneme_id_mapを抽出
                if (jsonObj["phoneme_id_map"] is JObject phonemeIdMap)
                {
                    foreach (var kvp in phonemeIdMap)
                    {
                        if (kvp.Value is JArray idArray && idArray.Count > 0)
                        {
                            config.PhonemeIdMap[kvp.Key] = idArray[0].ToObject<int>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                PiperLogger.LogError($"[ChatDialog] Error parsing TTS config: {ex.Message}");
            }
            
            return config;
        }
        
        /// <summary>
        /// テキストを音声合成して再生する
        /// </summary>
        private async UniTask SynthesizeAndPlayTTS(string text)
        {
            try
            {
                PiperLogger.LogInfo($"[ChatDialog] Starting TTS synthesis for text: {text}");
                
                // OpenJTalkで音素に変換
                var phonemizer = new TextPhonemizerAdapter(_japanesePhonemizer);
                var phonemeResult = await phonemizer.PhonemizeAsync(text, "ja");
                var openJTalkPhonemes = phonemeResult.Phonemes;
                
                PiperLogger.LogInfo($"[ChatDialog] OpenJTalk phonemes: {string.Join(" ", openJTalkPhonemes)}");
                
                // Piper形式の音素に変換
                var piperPhonemes = OpenJTalkToPiperMapping.ConvertToPiperPhonemes(openJTalkPhonemes);
                PiperLogger.LogInfo($"[ChatDialog] Piper phonemes: {string.Join(" ", piperPhonemes)}");
                
                // 音素をIDに変換
                var phonemeIds = _phonemeEncoder.Encode(piperPhonemes);
                PiperLogger.LogInfo($"[ChatDialog] Phoneme IDs: {string.Join(", ", phonemeIds)}");
                
                // 音声生成
                var audioData = await _ttsGenerator.GenerateAudioAsync(phonemeIds);
                PiperLogger.LogInfo($"[ChatDialog] Generated audio: {audioData.Length} samples");
                
                // 音声データの正規化
                var maxVal = audioData.Max(x => Math.Abs(x));
                float[] processedAudio;
                
                if (maxVal < 0.01f)
                {
                    // 音声が小さすぎる場合は増幅
                    var amplificationFactor = 0.3f / maxVal;
                    processedAudio = audioData.Select(x => x * amplificationFactor).ToArray();
                    PiperLogger.LogInfo($"[ChatDialog] Amplified audio by factor {amplificationFactor:F2}");
                }
                else if (maxVal > 1.0f)
                {
                    // 音声データを正規化
                    processedAudio = _audioClipBuilder.NormalizeAudio(audioData, 0.95f);
                    PiperLogger.LogInfo("[ChatDialog] Normalized audio data");
                }
                else
                {
                    processedAudio = audioData;
                }
                
                // AudioClipを作成
                var audioClip = _audioClipBuilder.BuildAudioClip(
                    processedAudio,
                    _ttsVoiceConfig.SampleRate,
                    $"TTS_{DateTime.Now:HHmmss}"
                );
                
                // 再生
                if (ttsAudioSource != null && audioClip != null)
                {
                    PiperLogger.LogInfo($"[ChatDialog] AudioClip ready - Length: {audioClip.length}s, Frequency: {audioClip.frequency}Hz, Channels: {audioClip.channels}");
                    PiperLogger.LogInfo($"[ChatDialog] AudioSource state before play - Volume: {ttsAudioSource.volume}, Mute: {ttsAudioSource.mute}, isPlaying: {ttsAudioSource.isPlaying}");
                    
                    ttsAudioSource.clip = audioClip;
                    ttsAudioSource.volume = 1.0f;  // ボリュームを明示的に設定
                    ttsAudioSource.Play();
                    
                    PiperLogger.LogInfo($"[ChatDialog] TTS playback started - isPlaying: {ttsAudioSource.isPlaying}");
                    
                    // 再生状態を確認
                    await UniTask.Delay(100);
                    PiperLogger.LogInfo($"[ChatDialog] After 100ms - isPlaying: {ttsAudioSource.isPlaying}, time: {ttsAudioSource.time}");
                }
                else
                {
                    PiperLogger.LogError($"[ChatDialog] Cannot play TTS - AudioSource: {ttsAudioSource != null}, AudioClip: {audioClip != null}");
                }
            }
            catch (Exception ex)
            {
                PiperLogger.LogError($"[ChatDialog] TTS synthesis failed: {ex.Message}");
            }
        }
        
        private void OnDestroy()
        {
            sendButton.onClick.RemoveAllListeners();

            // リスナーの登録解除
            if (InputController.Instance != null)
            {
                InputController.Instance.UI.Submit.performed -= OnSubmit;
            }

            if (speechToText != null)
            {
                speechToText.OnSpeechRecognized -= OnSpeechRecognized;
            }
            
            // 音声合成関連のリソースを解放
            _ttsGenerator?.Dispose();
            _japanesePhonemizer?.Dispose();
        }

        /// <summary>
        /// 音声認識でユーザーの発話を受け取った際に呼び出される
        /// </summary>
        /// <param name="recognizedText">確定したテキスト</param>
        private void OnSpeechRecognized(string recognizedText)
        {
            if (!_isMiscrophoneOn) return;
            if (string.IsNullOrWhiteSpace(recognizedText)) return;
            if (_inputBlocked) return; // AI返信中は無視

            // 同一メッセージが連続で来ないように判定
            if (recognizedText == _lastVoiceMessage) return;
            _lastVoiceMessage = recognizedText;

            SendVoiceMessage(recognizedText);
        }

        /// <summary>
        /// 音声入力由来のメッセージを送信する
        /// </summary>
        private void SendVoiceMessage(string userMessage)
        {
            if (_inputBlocked || string.IsNullOrWhiteSpace(userMessage))
            {
                return;
            }

            // 入力をブロック
            _inputBlocked = true;
            sendButton.interactable = false;
            inputField.interactable = false;

            // ユーザーのメッセージをチャット履歴に追加
            _chatTextBuilder.AppendLine($"あなた: {userMessage}");
            chatText.text = _chatTextBuilder.ToString();

            // ScrollToBottomを呼び出して最新のメッセージを表示
            ScrollToBottom();

            // AIの返信用のStringBuilderを初期化
            _replyTextBuilder = new StringBuilder();

            // 前回の返信の長さをリセット
            _lastReplyLength = 0;

            // LLMにユーザーのメッセージを送信し、返信を処理
            _ = ReceiveAIResponse(userMessage);

            // LLM 処理中はマイクを停止
            speechToText?.PauseRecording();
        }
    }
}
using System;
using UnityEngine;

namespace uDesktopMascot
{
    /// <summary>
    ///   16kHz モノラルでマイクをキャプチャし、short[] のバッファをイベントで通知するユーティリティ。
    /// </summary>
    public class VoiceProcessor : MonoBehaviour
    {
        public bool IsRecording { get; private set; }

        /// <summary>
        ///   キャプチャした PCM データ (16kHz, short) が 1 フレーム分用意されるたびに呼び出されます。
        /// </summary>
        public event Action<short[]> OnFrameCaptured;
        public event Action OnRecordingStop;

        private const int DesiredSampleRate = 16000;
        public int SampleRate { get; private set; } = DesiredSampleRate;
        private const int FrameSize = 1024; // サンプル数 (約64ms)

        private AudioClip _clip;
        private string _microphoneDevice;
        private int _lastReadPos;

        public void StartRecording()
        {
            if (IsRecording) return;
            if (Microphone.devices.Length == 0)
            {
                Debug.LogError("No microphone devices found.");
                return;
            }

            _microphoneDevice = Microphone.devices[0];
            _clip = Microphone.Start(_microphoneDevice, true, 10, DesiredSampleRate);
            if (_clip != null)
            {
                SampleRate = _clip.frequency; // 実際に取得されたサンプルレート
            }
            else
            {
                Debug.LogError("Failed to start Microphone.");
                return;
            }

            _lastReadPos = 0;
            IsRecording = true;
        }

        public void StopRecording()
        {
            if (!IsRecording) return;

            Microphone.End(_microphoneDevice);
            IsRecording = false;
            _clip = null;
            OnRecordingStop?.Invoke();
        }

        private void Update()
        {
            if (!IsRecording || _clip == null) return;

            int currentPos = Microphone.GetPosition(_microphoneDevice);
            if (currentPos < _lastReadPos) currentPos += _clip.samples; // wrap

            int samplesAvailable = currentPos - _lastReadPos;
            while (samplesAvailable >= FrameSize)
            {
                float[] floatBuffer = new float[FrameSize];
                int clipPos = _lastReadPos % _clip.samples;
                _clip.GetData(floatBuffer, clipPos);

                short[] shortBuffer = new short[FrameSize];
                for (int i = 0; i < FrameSize; i++)
                {
                    float sample = Mathf.Clamp(floatBuffer[i], -1f, 1f);
                    shortBuffer[i] = (short)(sample * short.MaxValue);
                }

                _lastReadPos += FrameSize;
                samplesAvailable -= FrameSize;

                OnFrameCaptured?.Invoke(shortBuffer);
            }
        }
    }
} 
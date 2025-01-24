using UnityEngine;

namespace uDesktopMascot
{
    /// <summary>
    ///    キャラクターモデルの設定
    /// </summary>
    public class CharacterSettings
    {
        /// <summary>
        /// モデルのパス
        /// </summary>
        public string ModelPath { get; set; } = "default.vrm";

        /// <summary>
        /// モデルのスケール（0.1 ～ 10.0）
        /// </summary>
        private float _scale = 3.0f;

        /// <summary>
        /// キャラクターのスケール（0.1 ～ 10.0）
        /// </summary>
        public float Scale
        {
            get => _scale;
            set => _scale = Mathf.Clamp(value, 0.1f, 10.0f);
        }

        private float _positionX = 0.0f;

        /// <summary>
        /// キャラクターのX座標位置（-100.0 ～ 100.0）
        /// </summary>
        public float PositionX
        {
            get => _positionX;
            set => _positionX = Mathf.Clamp(value, -100.0f, 100.0f);
        }

        private float _positionY = 0.0f;

        /// <summary>
        /// キャラクターのY座標位置（-100.0 ～ 100.0）
        /// </summary>
        public float PositionY
        {
            get => _positionY;
            set => _positionY = Mathf.Clamp(value, -100.0f, 100.0f);
        }

        private float _positionZ = 0.0f;

        /// <summary>
        /// キャラクターのZ座標位置（-100.0 ～ 100.0）
        /// </summary>
        public float PositionZ
        {
            get => _positionZ;
            set => _positionZ = Mathf.Clamp(value, -100.0f, 100.0f);
        }

        /// <summary>
        /// キャラクターのX軸回転（0.0 ～ 360.0）
        /// </summary>
        private float _rotationX = 0.0f;

        /// <summary>
        /// キャラクターのX軸回転（0.0 ～ 360.0）
        /// </summary>
        public float RotationX
        {
            get => _rotationX;
            set
            {
                _rotationX = value % 360.0f;
                if (_rotationX < 0) _rotationX += 360.0f;
            }
        }

        /// <summary>
        /// キャラクターのX軸回転（0.0 ～ 360.0）
        /// </summary>
        private float _rotationY = 0.0f;

        /// <summary>
        /// キャラクターのY軸回転（0.0 ～ 360.0）
        /// </summary>
        public float RotationY
        {
            get => _rotationY;
            set
            {
                _rotationY = value % 360.0f;
                if (_rotationY < 0) _rotationY += 360.0f;
            }
        }

        /// <summary>
        /// キャラクターのZ軸回転（0.0 ～ 360.0）
        /// </summary>
        private float _rotationZ = 0.0f;

        /// <summary>
        /// キャラクターのZ軸回転（0.0 ～ 360.0）
        /// </summary>
        public float RotationZ
        {
            get => _rotationZ;
            set
            {
                _rotationZ = value % 360.0f;
                if (_rotationZ < 0) _rotationZ += 360.0f;
            }
        }
    }

    /// <summary>
    ///   サウンドの設定
    /// </summary>
    public class SoundSettings
    {
        private float _voiceVolume = 1.0f;

        /// <summary>
        /// ボイスの音量（0.0～1.0）
        /// </summary>
        public float VoiceVolume
        {
            get => _voiceVolume;
            set => _voiceVolume = Mathf.Clamp01(value);
        }

        private float _bgmVolume = 0.5f;

        /// <summary>
        /// BGMの音量（0.0～1.0）
        /// </summary>
        public float BGMVolume
        {
            get => _bgmVolume;
            set => _bgmVolume = Mathf.Clamp01(value);
        }

        private float _seVolume = 1.0f;

        /// <summary>
        /// 効果音の音量（0.0～1.0）
        /// </summary>
        public float SEVolume
        {
            get => _seVolume;
            set => _seVolume = Mathf.Clamp01(value);
        }
    }

    /// <summary>
    ///   表示設定
    /// </summary>
    public class DisplaySettings
    {
        private float _opacity = 1.0f;

        /// <summary>
        /// キャラクターの透明度（0.0 ～ 1.0）
        /// </summary>
        public float Opacity
        {
            get => _opacity;
            set => _opacity = Mathf.Clamp01(value);
        }

        /// <summary>
        /// 常に最前面に表示するかどうか
        /// </summary>
        public bool AlwaysOnTop { get; set; } = true;
    }

    /// <summary>
    /// パフォーマンス設定
    /// </summary>
    public class PerformanceSettings
    {
        private int _targetFrameRate = 60;

        /// <summary>
        /// ターゲットフレームレート（15 ～ 240）
        /// </summary>
        public int TargetFrameRate
        {
            get => _targetFrameRate;
            set => _targetFrameRate = Mathf.Clamp(value, 15, 240);
        }

        private int _qualityLevel = 2;

        /// <summary>
        /// クオリティレベル（0 ～ 5）
        /// </summary>
        public int QualityLevel
        {
            get => _qualityLevel;
            set => _qualityLevel = Mathf.Clamp(value, 0, 5);
        }
    }
}
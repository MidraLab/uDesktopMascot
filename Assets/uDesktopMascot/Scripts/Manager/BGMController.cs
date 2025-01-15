using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Logging;
using UnityEngine;

namespace uDesktopMascot
{
    /// <summary>
    ///     BGMを制御するクラス
    /// </summary>
    public class BGMController : SingletonMonoBehaviour<BGMController>
    {
        /// <summary>
        ///     BGMのリスト
        /// </summary>
        [SerializeField] private List<AudioClip> bgmClips = new();

        /// <summary>
        ///     オーディオソース
        /// </summary>
        private AudioSource _audioSource;

        /// <summary>
        ///     BGMのロードが完了したかどうか
        /// </summary>
        private bool _bgmLoaded;

        /// <summary>
        ///     BGMのフォルダパス
        /// </summary>
        private const string BgmFolderPath = "BGM";

        /// <summary>
        ///     現在再生中のBGMのインデックス
        /// </summary>
        private int _currentBgmIndex = -1;

        /// <summary>
        ///     キャンセルトークンソース
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource;

        private protected override void Awake()
        {
            base.Awake();

            _audioSource = GetComponent<AudioSource>();

            if (_audioSource == null)
            {
                // AudioSource コンポーネントがアタッチされていない場合は追加
                _audioSource = gameObject.AddComponent<AudioSource>();
            }

            // ループ再生を有効にする
            _audioSource.loop = true;

            _cancellationTokenSource = new CancellationTokenSource();

            // BGMをロード
            LoadBGMAsync(_cancellationTokenSource.Token).Forget();
        }

        /// <summary>
        ///     BGMを非同期でロードする
        /// </summary>
        private async UniTaskVoid LoadBGMAsync(CancellationToken cancellationToken)
        {
            await SoundUtility.LoadSoundsAsync(
                BgmFolderPath,
                bgmClips,
                count =>
                {
                    _bgmLoaded = true;
                    if (count == 0)
                    {
                        Log.Warning("BGMがロードされませんでした。フォルダに有効なオーディオファイルがありません。");
                    } else
                    {
                        Log.Debug("BGMを {0} 件ロードしました。", count);
                    }
                },
                () =>
                {
                    Log.Debug("BGMフォルダが存在しません。BGMは再生されません。");
                    _bgmLoaded = true;
                },
                cancellationToken
            );
        }

        /// <summary>
        ///     次のBGMを再生する
        /// </summary>
        private void PlayNextBGM()
        {
            if (!_bgmLoaded || bgmClips.Count == 0)
            {
                Log.Warning("BGMがロードされていないか、BGMが存在しません。");
                return;
            }

            // 次のBGMのインデックスを決定（循環再生）
            _currentBgmIndex = (_currentBgmIndex + 1) % bgmClips.Count;

            // 選択したBGMを再生
            _audioSource.clip = bgmClips[_currentBgmIndex];
            _audioSource.Play();

            Log.Debug("再生中のBGM: {0}", _audioSource.clip.name);
        }

        /// <summary>
        ///     BGMの再生を停止する
        /// </summary>
        public void StopBGM()
        {
            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
                Log.Debug("BGMの再生を停止しました。");
            }
        }

        private void Update()
        {
            // BGMが再生終了したら次の曲を再生
            if (_bgmLoaded && !_audioSource.isPlaying && bgmClips.Count > 0)
            {
                PlayNextBGM();
            }
        }

        private void OnDestroy()
        {
            StopBGM();

            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}
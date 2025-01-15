using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Logging;
using UnityEngine;
using Random = UnityEngine.Random;

namespace uDesktopMascot
{
    /// <summary>
    ///     ボイスを制御するクラス
    /// </summary>
    public class VoiceController : SingletonMonoBehaviour<VoiceController>
    {
        /// <summary>
        ///     クリックボイス
        /// </summary>
        [SerializeField] private List<AudioClip> clickVoice;

        /// <summary>
        ///     ドラッグボイス
        /// </summary>
        [SerializeField] private List<AudioClip> dragVoice;

        /// <summary>
        ///     アプリ起動時のボイス
        /// </summary>
        [SerializeField] private List<AudioClip> startVoice;

        /// <summary>
        ///     アプリ終了時のボイス
        /// </summary>
        [SerializeField] private List<AudioClip> endVoice;

        /// <summary>
        ///     オーディオソース
        /// </summary>
        private AudioSource _audioSource;

        /// <summary>
        ///     クリックボイスのロードが完了したかどうか
        /// </summary>
        private bool _clickVoicesLoaded;

        /// <summary>
        ///     ドラッグボイスのロードが完了したかどうか
        /// </summary>
        private bool _dragVoicesLoaded;

        /// <summary>
        ///     アプリ起動時のボイスのロードが完了したかどうか
        /// </summary>
        private bool _startVoicesLoaded;

        /// <summary>
        ///     アプリ終了時のボイスのロードが完了したかどうか
        /// </summary>
        private bool _endVoicesLoaded;

        /// <summary>
        ///     クリックボイスのフォルダパス
        /// </summary>
        private const string ClickVoiceFolderPath = "Voice/Click";

        /// <summary>
        ///     ドラッグボイスのフォルダパス
        /// </summary>
        private const string DragVoiceFolderPath = "Voice/Drag";

        /// <summary>
        ///     アプリ起動時のボイスのフォルダパス
        /// </summary>
        private const string StartVoiceFolderPath = "Voice/Start";

        /// <summary>
        ///     アプリ終了時のボイスのフォルダパス
        /// </summary>
        private const string EndVoiceFolderPath = "Voice/End";

        /// <summary>
        ///     キャンセルトークンソース
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource;

        private protected override void Awake()
        {
            base.Awake();
            _cancellationTokenSource = new CancellationTokenSource();
            _audioSource = GetComponent<AudioSource>();

            // キャンセルトークンを取得
            var cancellationToken = _cancellationTokenSource.Token;

            // クリックボイスをロード
            LoadClickVoices(cancellationToken).Forget();

            // ドラッグボイスをロード
            LoadDragVoices(cancellationToken).Forget();

            // アプリ起動時のボイスをロード
            LoadStartVoices(cancellationToken).Forget();

            // アプリ終了時のボイスをロード
            LoadEndVoices(cancellationToken).Forget();
        }

        /// <summary>
        ///     アプリ終了時のボイスを非同期でロードする
        /// </summary>
        /// <param name="cancellationToken"></param>
        private async UniTaskVoid LoadStartVoices(CancellationToken cancellationToken)
        {
            await SoundUtility.LoadSoundsAsync(
                StartVoiceFolderPath,
                startVoice,
                count =>
                {
                    _startVoicesLoaded = true;
                    if (count == 0)
                    {
                        Log.Warning("スタート時のボイスがロードされませんでした。フォルダに有効なオーディオファイルがありません。");
                    }
                    else
                    {
                        Log.Debug("スタート時のボイスを {0} 件ロードしました。", count);
                    }
                },
                () =>
                {
                    Log.Debug("スタート時のボイスフォルダが存在しません。デフォルトのクリックボイスを使用します。");
                    _startVoicesLoaded = true;
                },
                cancellationToken
            );
        }

        /// <summary>
        ///     アプリ起動時のボイスを非同期でロードする
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="NotImplementedException"></exception>
        private async UniTaskVoid LoadEndVoices(CancellationToken cancellationToken)
        {
            await SoundUtility.LoadSoundsAsync(
                EndVoiceFolderPath,
                endVoice,
                count =>
                {
                    _endVoicesLoaded = true;
                    if (count == 0)
                    {
                        Log.Warning("エンド時のボイスがロードされませんでした。フォルダに有効なオーディオファイルがありません。");
                    }
                    else
                    {
                        Log.Debug("エンド時のボイスを {0} 件ロードしました。", count);
                    }
                },
                () =>
                {
                    Log.Debug("エンド時のボイスフォルダが存在しません。デフォルトのクリックボイスを使用します。");
                    _endVoicesLoaded = true;
                },
                cancellationToken
            );
        }

        /// <summary>
        ///     クリックボイスを非同期でロードする
        /// </summary>
        private async UniTaskVoid LoadClickVoices(CancellationToken cancellationToken)
        {
            await SoundUtility.LoadSoundsAsync(
                ClickVoiceFolderPath,
                clickVoice,
                count =>
                {
                    _clickVoicesLoaded = true;
                    if (count == 0)
                    {
                        Log.Warning("クリックボイスがロードされませんでした。フォルダに有効なオーディオファイルがありません。");
                    }
                    else
                    {
                        Log.Debug("クリックボイスを {0} 件ロードしました。", count);
                    }
                },
                () =>
                {
                    Log.Debug("クリックボイスフォルダが存在しません。デフォルトのクリックボイスを使用します。");
                    _clickVoicesLoaded = true;
                },
                cancellationToken
            );
        }

        /// <summary>
        ///     ドラッグボイスを非同期でロードする
        /// </summary>
        private async UniTaskVoid LoadDragVoices(CancellationToken cancellationToken)
        {
            await SoundUtility.LoadSoundsAsync(
                DragVoiceFolderPath,
                dragVoice,
                count =>
                {
                    _dragVoicesLoaded = true;
                    if (count == 0)
                    {
                        Log.Warning("ドラッグボイスがロードされませんでした。フォルダに有効なオーディオファイルがありません。");
                    }
                    else
                    {
                        Log.Debug("ドラッグボイスを {0} 件ロードしました。", count);
                    }
                },
                () =>
                {
                    Log.Debug("ドラッグボイスフォルダが存在しません。デフォルトのドラッグボイスを使用します。");
                    _dragVoicesLoaded = true;
                },
                cancellationToken
            );
        }

        /// <summary>
        ///     クリックボイスを再生する
        /// </summary>
        public void PlayClickVoice()
        {
            if (!_clickVoicesLoaded)
            {
                // ロードが完了していない場合は再生しない
                return;
            }

            if (clickVoice == null || clickVoice.Count == 0)
            {
                return;
            }

            // 現在の音声を停止
            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
            }

            // 新しい音声を再生
            var clip = clickVoice[Random.Range(0, clickVoice.Count)];
            _audioSource.clip = clip;
            _audioSource.Play();
        }

        /// <summary>
        ///     ドラッグボイスを再生する
        /// </summary>
        public void PlayHoldVoice()
        {
            if (!_dragVoicesLoaded)
            {
                // ロードが完了していない場合は再生しない
                return;
            }

            if (dragVoice == null || dragVoice.Count == 0)
            {
                return;
            }

            // 現在の音声を停止
            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
            }

            // 新しい音声を再生
            var clip = dragVoice[Random.Range(0, dragVoice.Count)];
            _audioSource.clip = clip;
            _audioSource.Play();
        }

        /// <summary>
        ///     アプリ起動時のボイスを再生する
        /// </summary>
        public async UniTask PlayStartVoiceAsync(CancellationToken cancellationToken)
        {
            if (!_startVoicesLoaded)
            {
                // ロードが完了していない場合は待機
                await UniTask.WaitUntil(() => _startVoicesLoaded, cancellationToken: cancellationToken);
            }

            if (startVoice == null || startVoice.Count == 0)
            {
                return;
            }

            // 現在の音声を停止
            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
            }

            var clip = startVoice[Random.Range(0, startVoice.Count)];
            _audioSource.clip = clip;
            _audioSource.Play();

            // クリップの長さ分待機
            await UniTask.Delay(TimeSpan.FromSeconds(clip.length), cancellationToken: cancellationToken);
        }

        /// <summary>
        ///     アプリ終了時のボイスを再生する
        /// </summary>
        public async UniTask PlayEndVoiceAsync(CancellationToken cancellationToken)
        {
            if (!_endVoicesLoaded)
            {
                // ロードが完了していない場合は待機
                await UniTask.WaitUntil(() => _endVoicesLoaded, cancellationToken: cancellationToken);
            }

            if (endVoice == null || endVoice.Count == 0)
            {
                return;
            }

            // 現在の音声を停止
            if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
            }

            var clip = endVoice[Random.Range(0, endVoice.Count)];
            _audioSource.clip = clip;
            _audioSource.Play();

            // クリップの長さ分待機
            await UniTask.Delay(TimeSpan.FromSeconds(clip.length), cancellationToken: cancellationToken);
        }

        private void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}
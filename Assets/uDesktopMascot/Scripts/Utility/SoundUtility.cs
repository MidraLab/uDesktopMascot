using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Logging;
using UnityEngine;
using UnityEngine.Networking;

namespace uDesktopMascot
{
    /// <summary>
    ///     サウンド関連のユーティリティクラス
    /// </summary>
    public class SoundUtility
    {
        /// <summary>
        ///     サウンドを非同期でロードする共通メソッド
        /// </summary>
        /// <param name="relativeFolderPath">サウンドのフォルダパス (Application.streamingAssetsPathからの相対パス)</param>
        /// <param name="soundList">ロードした AudioClip を格納するリスト</param>
        /// <param name="onLoaded">ロード完了時のコールバック（ロードしたファイル数を引数に取る）</param>
        /// <param name="onDirectoryNotFound">フォルダが見つからなかったときのコールバック</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        public static async UniTask LoadSoundsAsync(
            string relativeFolderPath,
            IList<AudioClip> soundList,
            Action<int> onLoaded = null,
            Action onDirectoryNotFound = null,
            CancellationToken cancellationToken = default)
        {
            var folderPath = Path.Combine(Application.streamingAssetsPath, relativeFolderPath);

            // フォルダが存在する場合
            if (Directory.Exists(folderPath))
            {
                // フォルダ内のすべてのファイルを取得
                var files = Directory.GetFiles(folderPath);

                if (files.Length == 0)
                {
                    // フォルダ内にファイルが存在しない場合
                    onDirectoryNotFound?.Invoke();
                    return;
                }
                
                // 既存のサウンドリストをクリア
                soundList.Clear();

                foreach (var filePath in files)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var extension = Path.GetExtension(filePath).ToLower();

                    // 対象の拡張子のみを処理
                    if (extension == ".wav" || extension == ".mp3" || extension == ".ogg")
                    {
                        var url = "file://" + filePath;

                        using var www = UnityWebRequestMultimedia.GetAudioClip(url, GetAudioType(extension));
                        try
                        {
                            await www.SendWebRequest().WithCancellation(cancellationToken);

                            if (www.result == UnityWebRequest.Result.ConnectionError ||
                                www.result == UnityWebRequest.Result.ProtocolError)
                            {
                                Log.Warning("Failed to load audio clip: {0}\nError: {1}", filePath, www.error);
                            } else
                            {
                                var clip = DownloadHandlerAudioClip.GetContent(www);
                                if (clip != null)
                                {
                                    soundList.Add(clip);
                                }
                            }
                        } catch (OperationCanceledException)
                        {
                            // キャンセル時の処理（必要に応じて）
                            Log.Debug("ロードがキャンセルされました: {0}", filePath);
                            return;
                        }
                    }
                }

                // ロード完了時のコールバックを呼び出す
                onLoaded?.Invoke(soundList.Count);
            } else
            {
                // フォルダが存在しない場合
                onDirectoryNotFound?.Invoke();
            }
        }

        /// <summary>
        ///     拡張子に応じてAudioTypeを取得する
        /// </summary>
        /// <param name="extension">ファイル拡張子</param>
        /// <returns>AudioType</returns>
        private static AudioType GetAudioType(string extension)
        {
            return extension switch
            {
                ".wav" => AudioType.WAV,
                ".mp3" => AudioType.MPEG,
                ".ogg" => AudioType.OGGVORBIS,
                _ => AudioType.UNKNOWN
            };
        }
    }
}
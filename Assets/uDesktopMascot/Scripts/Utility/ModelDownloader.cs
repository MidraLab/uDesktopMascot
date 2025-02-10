using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace uDesktopMascot
{
    /// <summary>
    /// モデルのダウンロードを行うクラス
    /// </summary>
    public class ModelDownloader
    {
        /// <summary>
        /// モデルのダウンロード進行状況を報告するためのデリゲート
        /// </summary>
        /// <param name="progress">ダウンロードの進捗（0.0〜1.0）</param>
        public delegate void ProgressChanged(float progress);

        /// <summary>
        /// ダウンロード進捗が更新されたときに呼び出されるイベント
        /// </summary>
        public event ProgressChanged OnProgressChanged;

        /// <summary>
        /// ダウンロードが完了したときに呼び出されるイベント
        /// </summary>
        public event Action OnDownloadCompleted;

        /// <summary>
        /// ダウンロードがエラーで失敗したときに呼び出されるイベント
        /// </summary>
        public event Action<Exception> OnDownloadFailed;
        
        /// <summary>
        /// ダウンロードの進行状況を表す列挙型
        /// </summary>
        public static ModelDownloadProgressEnum ModelDownloadProgressEnum { get; set; }

        /// <summary>
        /// モデルを非同期でダウンロードします
        /// </summary>
        /// <param name="url">モデルのダウンロードURL</param>
        /// <param name="savePath">モデルを保存するパス</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async UniTask DownloadModelAsync(string url, string savePath,CancellationToken cancellationToken)
        {
            try
            {
                await DownloadFileAsync(url, savePath, cancellationToken);
                OnDownloadCompleted?.Invoke();
            }
            catch (Exception ex)
            {
                if (!(ex is OperationCanceledException))
                {
                    OnDownloadFailed?.Invoke(ex);
                }
            }
        }

        /// <summary>
        /// ファイルの非同期ダウンロード
        /// </summary>
        /// <param name="url"></param>
        /// <param name="savePath"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="Exception"></exception>
        private async UniTask DownloadFileAsync(string url, string savePath, CancellationToken cancellationToken)
        {
            ModelDownloadProgressEnum = ModelDownloadProgressEnum.ProgressChanged;
            using UnityWebRequest uwr = UnityWebRequest.Get(url);
            uwr.downloadHandler = new DownloadHandlerFile(savePath);
            var asyncOperation = uwr.SendWebRequest();

            while (!asyncOperation.isDone)
            {
                cancellationToken.ThrowIfCancellationRequested();
                OnProgressChanged?.Invoke(uwr.downloadProgress);
                await UniTask.DelayFrame(1, cancellationToken: cancellationToken);
            }

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                ModelDownloadProgressEnum = ModelDownloadProgressEnum.DownloadFailed;
                throw new Exception($"モデルのダウンロードに失敗しました: {uwr.error}");
            }
            else
            {
                ModelDownloadProgressEnum = ModelDownloadProgressEnum.DownloadCompleted;
                OnProgressChanged?.Invoke(1.0f);
            }
        }
    }
    
    /// <summary>
    /// ダウンロードの進行状況を表す列挙型
    /// </summary>
    public enum ModelDownloadProgressEnum
    {
        ProgressChanged,
        DownloadCompleted,
        DownloadFailed
    }
}
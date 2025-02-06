using Cysharp.Threading.Tasks;
using Unity.Logging;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Threading;
using Console = System.Console;

namespace uDesktopMascot
{
    /// <summary>
    ///   バージョンチェックを行うクラス
    /// </summary>
    public class CheckVersion
    {
        /// <summary>
        /// 最新リリース情報を取得するURL
        /// </summary>
        private const string LatestReleaseUrl = "https://api.github.com/repos/MidraLab/uDesktopMascot/releases/latest";

        /// <summary>
        /// 最新バージョン
        /// </summary>
        public string LatestVersion { get; private set; }

        /// <summary>
        /// アップデートがあるかどうかを判定します
        /// </summary>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>アップデートがある場合は true を返します</returns>
        public async UniTask<bool> IsUpdateAvailable(CancellationToken cancellationToken)
        {
            // 最新バージョンを取得
            try
            {
                LatestVersion = await GetLatestVersionAsync(cancellationToken);
            } catch (Exception e)
            {
                Log.Error($"最新バージョンの取得に失敗しました。{e.Message}");
                return false;
            }

            if (string.IsNullOrEmpty(LatestVersion))
            {
                Log.Error("最新バージョンの取得に失敗しました。");
                return false;
            }

            // アプリケーションの現在のバージョンを取得
            string currentVersion = Application.version.TrimStart('v', 'V');

            // バージョンを比較
            bool isUpdateAvailable = IsNewerVersion(LatestVersion, currentVersion);

            Log.Info($"最新バージョン: {LatestVersion}, 現在のバージョン: {currentVersion}, アップデートあり: {isUpdateAvailable}");

            return isUpdateAvailable;
        }

        /// <summary>
        /// GitHub APIから最新バージョンを取得します
        /// </summary>
        private async UniTask<string> GetLatestVersionAsync(CancellationToken cancellationToken)
        {
            using UnityWebRequest request = UnityWebRequest.Get(LatestReleaseUrl);
            // GitHub APIにはUser-Agentヘッダーが必要
            request.SetRequestHeader("User-Agent", "uDesktopMascot");

            // リクエストを送信し、完了を待機
            await request.SendWebRequest().WithCancellation(cancellationToken).SuppressCancellationThrow();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Log.Error($"最新バージョン取得のエラー: {request.error}");
                return null;
            }

            // レスポンスのJSONを取得
            string jsonResponse = request.downloadHandler.text;

            // JSONをパースして最新リリース情報を取得
            LatestReleaseInfo latestRelease = JsonUtility.FromJson<LatestReleaseInfo>(jsonResponse);

            if (latestRelease != null && !string.IsNullOrEmpty(latestRelease.tag_name))
            {
                // タグ名から先頭の 'v' または 'V' を除去
                return latestRelease.tag_name.TrimStart('v', 'V');
            } else
            {
                Log.Error("最新リリース情報の取得に失敗しました。");
                return null;
            }
        }

        /// <summary>
        /// バージョン文字列を比較して、最新バージョンが現在のバージョンより新しいかを判定します
        /// </summary>
        public bool IsNewerVersion(string latestVersion, string currentVersion)
        {
            try
            {
                var latest = new Version(latestVersion);
                var current = new Version(currentVersion);

                return latest.CompareTo(current) > 0;
            } catch (Exception ex)
            {
                Log.Error($"バージョン比較のエラー: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// GitHubからの最新リリース情報を表すクラス
        /// </summary>
        [Serializable]
        private class LatestReleaseInfo
        {
            public string tag_name;
        }
    }
}
using System;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using Unity.Logging;
using UnityEditor;
using UnityEditor.Callbacks;

namespace uDesktopMascot.Editor
{
    public class PostBuildProcessor
    {
        /// <summary>
        ///     ビルド後の処理を行う
        /// </summary>
        /// <param name="target">ビルドターゲット</param>
        /// <param name="pathToBuiltProject">ビルドされたプロジェクトのパス</param>
        [PostProcessBuild(1)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            // ビルドされたプロジェクトのディレクトリを取得
            var buildDirectory = Path.GetDirectoryName(pathToBuiltProject);
            if (string.IsNullOrEmpty(buildDirectory))
            {
                Log.Error("ビルドされたプロジェクトのディレクトリが取得できませんでした。");
                return;
            }

            // アプリケーション名を取得
            var appName = Path.GetFileNameWithoutExtension(pathToBuiltProject);

            // プラットフォームに応じた StreamingAssets のパスを取得
            var streamingAssetsPath = GetStreamingAssetsPath(target, buildDirectory, appName);
            if (string.IsNullOrEmpty(streamingAssetsPath))
            {
                Log.Warning("このプラットフォームはサポートされていません: " + target);
                return;
            }

            // 必要なフォルダを作成
            CreateNecessaryDirectories(streamingAssetsPath);

            // development buildの場合はスキップする
            if (!EditorUserBuildSettings.development)
            {
                // ビルドフォルダを最大圧縮で ZIP 圧縮
                CreateMaxCompressedZipOfBuildFolder(buildDirectory, appName);
            }

            Log.Debug("ビルド後処理が完了しました。");
        }

        /// <summary>
        ///     プラットフォームに応じた StreamingAssets のパスを取得する
        /// </summary>
        /// <param name="target">ビルドターゲット</param>
        /// <param name="buildDirectory">ビルドディレクトリのパス</param>
        /// <param name="appName">アプリケーション名</param>
        /// <returns>StreamingAssets のフルパス</returns>
        private static string GetStreamingAssetsPath(BuildTarget target, string buildDirectory, string appName)
        {
            return target switch
            {
                BuildTarget.StandaloneWindows or BuildTarget.StandaloneWindows64 or BuildTarget.StandaloneLinux64 =>
                    // Windows および Linux の場合
                    Path.Combine(buildDirectory, $"{appName}_Data", "StreamingAssets"),
                BuildTarget.StandaloneOSX =>
                    // macOS の場合
                    Path.Combine(buildDirectory, $"{appName}.app", "Contents", "Resources", "Data", "StreamingAssets"),
                _ => null
            };
        }

        /// <summary>
        ///     必要なフォルダを作成する
        /// </summary>
        /// <param name="streamingAssetsPath">StreamingAssets のフルパス</param>
        private static void CreateNecessaryDirectories(string streamingAssetsPath)
        {
            // StreamingAssets フォルダが存在しない場合は作成
            if (!Directory.Exists(streamingAssetsPath))
            {
                Directory.CreateDirectory(streamingAssetsPath);
                Log.Debug($"StreamingAssets フォルダを作成しました: {streamingAssetsPath}");
            }

            // Voice/Click フォルダを作成
            var clickVoicePath = Path.Combine(streamingAssetsPath, "Voice", "Click");
            if (!Directory.Exists(clickVoicePath))
            {
                Directory.CreateDirectory(clickVoicePath);
                Log.Debug($"Voice/Click フォルダを作成しました: {clickVoicePath}");
            }

            // Voice/Drag フォルダを作成
            var dragVoicePath = Path.Combine(streamingAssetsPath, "Voice", "Drag");
            if (!Directory.Exists(dragVoicePath))
            {
                Directory.CreateDirectory(dragVoicePath);
                Log.Debug($"Voice/Drag フォルダを作成しました: {dragVoicePath}");
            }

            // BGM フォルダを作成
            var bgmPath = Path.Combine(streamingAssetsPath, "BGM");
            if (!Directory.Exists(bgmPath))
            {
                Directory.CreateDirectory(bgmPath);
                Log.Debug($"BGM フォルダを作成しました: {bgmPath}");
            }
        }

        /// <summary>
        ///     ビルドフォルダを最大圧縮で ZIP 圧縮する
        /// </summary>
        /// <param name="buildDirectory">ビルドディレクトリのパス</param>
        /// <param name="appName">アプリケーション名</param>
        private static void CreateMaxCompressedZipOfBuildFolder(string buildDirectory, string appName)
        {
            try
            {
                // ビルドフォルダの親ディレクトリのパス（ZIP ファイルの保存先）
                var parentInfo = Directory.GetParent(buildDirectory);
                if (parentInfo == null)
                {
                    Log.Error("ビルドフォルダの親ディレクトリが取得できませんでした。");
                    return;
                }

                var parentDirectory = parentInfo.FullName;

                // Player Settings からバージョンを取得
                var projectVersion = PlayerSettings.bundleVersion;
                if (string.IsNullOrEmpty(projectVersion))
                {
                    projectVersion = "0.0.0";
                    Log.Warning("Player Settings のバージョンが設定されていません。デフォルト値 '0.0.0' を使用します。");
                }

                // バージョン文字列をファイル名に使用できる形式に変換
                var sanitizedVersion = Regex.Replace(projectVersion, @"[^\d\.]", "").Replace(".", "_");

                // ZIP ファイルの保存先（親ディレクトリに {appName}_v{sanitizedVersion}.zip として保存）
                var zipFileName = $"{appName}_v{sanitizedVersion}.zip";
                var zipFilePath = Path.Combine(parentDirectory, zipFileName);

                // 既存の ZIP ファイルを削除
                if (File.Exists(zipFilePath))
                {
                    File.Delete(zipFilePath);
                    Log.Debug($"既存の ZIP ファイルを削除しました: {zipFilePath}");
                }

                // ビルドディレクトリを最大圧縮で ZIP 圧縮
                CompressDirectory(buildDirectory, zipFilePath, CompressionLevel.Optimal);

                Log.Debug($"ビルドフォルダを最大圧縮で ZIP 圧縮しました: {zipFilePath}");
            }
            catch (Exception ex)
            {
                Log.Error($"ビルドフォルダの ZIP 圧縮中にエラーが発生しました: {ex.Message}");
            }
        }

        /// <summary>
        ///     ディレクトリを最大圧縮で ZIP 圧縮する
        /// </summary>
        /// <param name="sourceDir">圧縮するフォルダのパス</param>
        /// <param name="zipFilePath">出力先の ZIP ファイルのパス</param>
        /// <param name="compressionLevel">圧縮レベル</param>
        private static void CompressDirectory(string sourceDir, string zipFilePath, CompressionLevel compressionLevel)
        {
            // ZIP 圧縮を開始
            using var zipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create);
            var files = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                // ファイルの相対パスを取得
                var relativePath = GetRelativePath(sourceDir, file);

                // ZIP エントリとして追加
                zipArchive.CreateEntryFromFile(file, relativePath, compressionLevel);
            }
        }

        /// <summary>
        ///     ファイルパスの相対パスを取得するヘルパーメソッド
        /// </summary>
        private static string GetRelativePath(string basePath, string targetPath)
        {
            var baseUri = new Uri(basePath.EndsWith(Path.DirectorySeparatorChar.ToString())
                ? basePath
                : basePath + Path.DirectorySeparatorChar);
            var targetUri = new Uri(targetPath);
            return Uri.UnescapeDataString(baseUri.MakeRelativeUri(targetUri)
                .ToString()
                .Replace('/', Path.DirectorySeparatorChar));
        }
    }
}
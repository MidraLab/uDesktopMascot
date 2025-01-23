using System;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using Unity.Logging;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using CompressionLevel = System.IO.Compression.CompressionLevel;

namespace uDesktopMascot.Editor
{
    public sealed class PostBuildProcessor : IPostprocessBuildWithReport
    {
        // コールバックの順序を指定
        public int callbackOrder => 0;

        public void OnPostprocessBuild(BuildReport report)
        {
            var summary = report.summary;
            var target = summary.platform;
            var outputPath = summary.outputPath;

            // ビルドされたプロジェクトのディレクトリを取得
            var buildDirectory = Path.GetDirectoryName(outputPath);
            if (string.IsNullOrEmpty(buildDirectory))
            {
                Log.Error("ビルドされたプロジェクトのディレクトリが取得できませんでした。");
                return;
            }

            // ビルドディレクトリの名前を取得
            var buildDirectoryName = new DirectoryInfo(buildDirectory).Name;

            // ビルド時に選択したフォルダが uDesktopMascotBuild でない場合、警告を出す
            if (buildDirectoryName != "uDesktopMascotBuild")
            {
                Log.Debug($"ビルド出力フォルダ名が 'uDesktopMascotBuild' ではありません（現在のフォルダ名: '{buildDirectoryName}'）。いくつかの後処理が実行されない可能性があります。");
            }

            // アプリケーション名を取得
            var appName = Path.GetFileNameWithoutExtension(outputPath);

            // プラットフォームに応じた StreamingAssets のパスを取得
            var streamingAssetsPath = GetStreamingAssetsPath(target, buildDirectory, appName);
            if (string.IsNullOrEmpty(streamingAssetsPath))
            {
                Log.Debug("このプラットフォームはサポートされていません: " + target);
                return;
            }

            // 必要なフォルダを作成
            CreateNecessaryDirectories(streamingAssetsPath);

            // Development Buildの場合はスキップする（必要に応じて）
            if (summary.options.HasFlag(BuildOptions.Development))
            {
                Log.Debug("Development Build のため、ZIP圧縮をスキップします。");
            }
            else
            {
                // ビルドフォルダを最大圧縮で ZIP 圧縮
                CreateMaxCompressedZipOfBuildFolder(buildDirectory, appName);
            }

            // 不要なフォルダを削除
            DeleteUnnecessaryFolders(target, outputPath);

            Log.Debug("ビルド後処理が完了しました。");
        }

        /// <summary>
        ///     プラットフォームに応じた StreamingAssets のパスを取得する
        /// </summary>
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
                Log.Debug("Voice/Drag フォルダを作成しました: {0}", dragVoicePath);
            }

            // BGM フォルダを作成
            var bgmPath = Path.Combine(streamingAssetsPath, "BGM");
            if (!Directory.Exists(bgmPath))
            {
                Directory.CreateDirectory(bgmPath);
                Log.Debug("BGM フォルダを作成しました: {0}", bgmPath);
            }
        }

        /// <summary>
        ///     ビルドフォルダを最大圧縮で ZIP 圧縮する
        /// </summary>
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
                    Log.Debug("既存の ZIP ファイルを削除しました: {0}", zipFilePath);
                }

                // ビルドディレクトリを最大圧縮で ZIP 圧縮
                CompressDirectory(buildDirectory, zipFilePath, CompressionLevel.Optimal);
                
                Log.Debug("ビルドフォルダを最大圧縮で ZIP 圧縮しました: {0}", zipFilePath);
            }
            catch (Exception ex)
            {
                Log.Error($"ビルドフォルダの ZIP 圧縮中にエラーが発生しました: {0}", ex.Message);
            }
        }

        /// <summary>
        ///     ディレクトリを最大圧縮で ZIP 圧縮する
        /// </summary>
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

        /// <summary>
        ///     不要なフォルダを削除する
        /// </summary>
        private static void DeleteUnnecessaryFolders(BuildTarget target, string outputPath)
        {
            // 削除したいフォルダのパスを構築
            string folderToDelete = string.Empty;
            var outputDirectory = Path.GetDirectoryName(outputPath);
            var productName = PlayerSettings.productName;
            
            if(outputDirectory == null)
            {
                Log.Error("ビルド出力ディレクトリが取得できませんでした。");
                return;
            }

            if (target == BuildTarget.StandaloneWindows || target == BuildTarget.StandaloneWindows64)
            {
                // Windowsの場合
                folderToDelete = Path.Combine(outputDirectory, $"{productName}_BackUpThisFolder_ButDontShipItWithYourGame");
            }
            else if (target == BuildTarget.StandaloneOSX)
            {
                // Macの場合：アプリケーションパッケージ内のパスを指定
                folderToDelete = Path.Combine(outputPath, "Contents", "Resources", "uDesktopMascot_BackUpThisFolder_ButDontShipItWithYourGame");
            }
            // 必要に応じて他のプラットフォームを追加

            // フォルダが存在する場合、削除
            if (!string.IsNullOrEmpty(folderToDelete) && Directory.Exists(folderToDelete))
            {
                Directory.Delete(folderToDelete, true);
                Log.Debug("不要なフォルダを削除しました: {0}", folderToDelete);
            }
            else
            {
                Log.Debug("削除するフォルダが存在しませんでした。");
            }
        }
    }
}
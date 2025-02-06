using System;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Logging;
using UnityEngine;

namespace uDesktopMascot
{
    /// <summary>
    ///    メニューのプレゼンター
    /// </summary>
    public partial class MenuPresenter : IDisposable
    {
        /// <summary>
        ///    メニューのビュー
        /// </summary>
        private readonly MenuDialog _menuDialog;

        /// <summary>
        ///   メニューが開かれているかどうか
        /// </summary>
        public bool IsOpened { get; private set; }

        /// <summary>
        ///  メニューのUIパス
        /// </summary>
        private string MenuUIPath => "Menu";

        /// <summary>
        /// キャンセルトークンソース
        /// </summary>
        private readonly CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        ///  メニューの表示位置のオフセット
        /// </summary>
        private static readonly Vector3 MenuOffset = new Vector3(2.5f, 2, -1);

        public MenuPresenter(MenuDialog menuDialog)
        {
            this._menuDialog = menuDialog;
            
            IsOpened = false;

            _cancellationTokenSource = new CancellationTokenSource();

            menuDialog.OnHelpAction = OpenHelp;
            menuDialog.OnAppSettingAction = OpenAppSetting;
            menuDialog.OnWebUIAction = OpenWebUI;
            menuDialog.OnCloseAction = CloseApp;

            ApplyMenuUISettings();

#if UNITY_EDITOR
            InitDebugMenu();
#endif
            
            Hide();
        }

        /// <summary>
        ///   メニューを表示する
        /// </summary>
        /// <param name="screenPosition"></param>
        public void Show(Vector3 screenPosition)
        {
            IsOpened = true;
            _menuDialog.Show(screenPosition + MenuOffset, _cancellationTokenSource.Token).Forget();
        }

        /// <summary>
        ///  メニューを非表示にする
        /// </summary>
        public void Hide()
        {
            IsOpened = false;
            _menuDialog.Hide();
        }

        /// <summary>
        /// メニューの表示設定を適用する
        /// </summary>
        private void ApplyMenuUISettings()
        {
            var menuUISettings = ApplicationSettings.Instance.MenuUISettings;

            // 背景色を適用
            if (!string.IsNullOrEmpty(menuUISettings.BackgroundColor))
            {
                if (ColorUtility.TryParseHtmlString(menuUISettings.BackgroundColor, out Color color))
                {
                    _menuDialog.SetBackgroundColor(color);
                }
                else
                {
                    Log.Warning("背景色の指定が不正です。正しいカラーコードを設定してください。");
                }
            }

            // 背景画像を適用
            if (!string.IsNullOrEmpty(menuUISettings.BackgroundImagePath))
            {
                LoadBackgroundImageAsync(Path.Combine(MenuUIPath, menuUISettings.BackgroundImagePath),
                    _cancellationTokenSource.Token).Forget();
            }
        }

        /// <summary>
        /// 設定ファイルおよびフォルダを開く
        /// </summary>
        private void OpenAppSetting()
        {
            string folderPath = Application.streamingAssetsPath;
            string filePath = Path.Combine(folderPath, "application_settings.txt");

            Log.Info($"Opening settings file: {filePath}");
            Log.Info($"Opening settings folder: {folderPath}");

#if UNITY_EDITOR
            // In Unity Editor, open the folder and file
            if (Directory.Exists(folderPath))
            {
                UnityEditor.EditorUtility.OpenWithDefaultApp(folderPath);
            }
            else
            {
                Log.Warning($"Folder not found: {folderPath}");
            }

            if (File.Exists(filePath))
            {
                UnityEditor.EditorUtility.OpenWithDefaultApp(filePath);
            }
            else
            {
                Log.Warning($"File not found: {filePath}");
            }
#else
            bool openedFolder = false;
            bool openedFile = false;
            try
            {
                // Open the folder
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = folderPath,
                    UseShellExecute = true,
                    Verb = "open"
                });
                openedFolder = true;
            }
            catch (Exception e)
            {
                Log.Warning("Process.Start failed to open folder: " + e);
            }

            try
            {
                // Open the file
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = filePath,
                    UseShellExecute = true,
                    Verb = "open"
                });
                openedFile = true;
            }
            catch (Exception e)
            {
                Log.Warning("Process.Start failed to open file: " + e);
            }

            if (!openedFolder)
            {
                // Fallback to Application.OpenURL for folder
                Application.OpenURL("file://" + folderPath.Replace("\\", "/"));
            }

            if (!openedFile)
            {
                // Fallback to Application.OpenURL for file
                Application.OpenURL("file://" + filePath.Replace("\\", "/"));
            }
#endif
        }

        /// <summary>
        /// 背景画像をロードするコルーチン
        /// </summary>
        /// <param name="relativePath"></param>
        /// <param name="cancellationToken"></param>
        private async UniTask LoadBackgroundImageAsync(string relativePath, CancellationToken cancellationToken)
        {
            string fullPath = Path.Combine(Application.streamingAssetsPath, relativePath);

            Sprite sprite = await ImageLoader.LoadSpriteAsync(fullPath, cancellationToken);
            if (sprite != null)
            {
                _menuDialog.SetBackgroundImage(sprite);
            }
            else
            {
                Log.Warning("背景画像のロードに失敗しました。パスを確認してください: " + fullPath);
            }
        }

        /// <summary>
        /// ヘルプページ（README.txt）を環境に応じて開く
        /// </summary>
        private void OpenHelp()
        {
            string path;

#if UNITY_EDITOR
            // Unity Editorでは、Assetsフォルダ内のパスを使用
            path = Path.Combine(Application.dataPath, "uDesktopMascot/Document/README.txt");
#else
            // ビルド後のアプリケーションでは、ビルドフォルダのルートへのパスを取得
            string rootPath = Directory.GetParent(Application.dataPath).FullName;
            path = Path.Combine(rootPath, "README.txt");
#endif

            // パスをログに出力
            Log.Info($"Attempting to open file at path: {path}");

            if (File.Exists(path))
            {
                try
                {
                    // ファイルURLを作成
                    string url = $"file:///{path.Replace("\\", "/")}";
                    // ファイルを開く
                    Application.OpenURL(url);
                }
                catch (Exception e)
                {
                    Log.Error($"README.txtを開くことができませんでした:\n{e}");
                }
            }
            else
            {
                Log.Error($"README.txtが次のパスに見つかりませんでした: {path}");
            }
        }

        /// <summary>
        ///  WebUIを開く
        /// </summary>
        private void OpenWebUI()
        {
            OpenWebUIAsync(_cancellationTokenSource.Token).Forget();
        }

        /// <summary>
        ///  WebUIを開く
        /// </summary>
        private async UniTaskVoid OpenWebUIAsync(CancellationToken token)
        {
            SystemManager.Instance.InitializeWebServer();
            var port = SystemManager.Instance.GetPort();

            string templatePath;
#if UNITY_EDITOR
            templatePath = Path.Combine(Application.dataPath, "WebUI/index.html.template");
#else
            templatePath = Path.Combine(Application.streamingAssetsPath, "WebUI/index.html.template");
#endif

            if (File.Exists(templatePath))
            {
                var templateContent = await File.ReadAllTextAsync(templatePath, token);
                var htmlContent = templateContent.Replace("{{PORT}}", $"{port}");
                var htmlPath = templatePath.Replace(".template", "");
                await File.WriteAllTextAsync(htmlPath, htmlContent, token);
                Application.OpenURL("file://" + htmlPath.Replace("\\", "/"));
            }
            else
            {
                Log.Error($"WebUIファイルが見つかりません: {templatePath}\nWebUIフォルダを確認してください");
            }
        }

        /// <summary>
        /// アプリ終了
        /// </summary>
        private void CloseApp()
        {
            Log.Debug("Close App");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void Dispose()
        {
            string htmlPath;
#if UNITY_EDITOR
            htmlPath = Path.Combine(Application.dataPath, "WebUI/index.html");
#else
            htmlPath = Path.Combine(Application.streamingAssetsPath, "WebUI/index.html");
#endif
            if (File.Exists(htmlPath))
            {
                File.Delete(htmlPath);
            }

#if UNITY_EDITOR
            var metaPath = $"{htmlPath}.meta";
            if (File.Exists(metaPath))
            {
                File.Delete(metaPath);
            }
#endif

            _menuDialog.OnHelpAction = null;
            _menuDialog.OnAppSettingAction = null;
            _menuDialog.OnCloseAction = null;

            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();

#if UNITY_EDITOR
            OnDestroyEditor();
#endif
        }
    }
}
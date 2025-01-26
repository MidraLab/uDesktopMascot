using System;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Logging;
using UnityEngine;
using UnityEngine.Networking;

namespace uDesktopMascot
{
    /// <summary>
    ///    メニューのプレゼンター
    /// </summary>
    public partial class MenuPresenter : MonoBehaviour
    {
        /// <summary>
        ///    メニューのビュー
        /// </summary>
        [SerializeField] private MenuView menuView;

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
        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        ///  メニューの表示位置のオフセット
        /// </summary>
        private static readonly Vector3 MenuOffset = new Vector3(2.5f, 2, -1);

        private void Awake()
        {
            IsOpened = false;

            _cancellationTokenSource = new CancellationTokenSource();

            menuView.OnHelpAction = OpenHelp;
            menuView.OnModelSettingAction = () =>
            {
                Log.Debug("ModelSetting");
            };
            menuView.OnAppSettingAction = () =>
            {
                Log.Debug("AppSetting");
            };
            menuView.OnCloseAction = CloseApp;

            ApplyMenuUISettings();

#if UNITY_EDITOR
            InitDebugMenu();
#endif
        }

        /// <summary>
        ///   メニューを表示する
        /// </summary>
        /// <param name="screenPosition"></param>
        public void Show(Vector3 screenPosition)
        {
            IsOpened = true;
            menuView.Show(screenPosition + MenuOffset);
        }

        /// <summary>
        ///  メニューを非表示にする
        /// </summary>
        public void Hide()
        {
            IsOpened = false;
            menuView.Hide();
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
                    menuView.SetBackgroundColor(color);
                } else
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
        /// 背景画像をロードするコルーチン
        /// </summary>
        /// <param name="relativePath"></param>
        /// <param name="cancellationToken"></param>
        private async UniTask LoadBackgroundImageAsync(string relativePath, CancellationToken cancellationToken)
        {
            string fullPath = Path.Combine(Application.streamingAssetsPath, relativePath);

            // ファイルが存在しない場合はエラーログを出力して終了
            if (!File.Exists(fullPath))
            {
                Log.Error("背景画像が見つかりません。パスを確認してください: " + fullPath);
                return;
            }

            UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(fullPath);

            await uwr.SendWebRequest().WithCancellation(cancellationToken);

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Log.Error("背景画像のロードに失敗しました。パスを確認してください: " + fullPath + " エラー: " + uwr.error);
            } else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                if (texture != null)
                {
                    Rect rect = new Rect(0, 0, texture.width, texture.height);
                    Vector2 pivot = new Vector2(0.5f, 0.5f);
                    Sprite sprite = Sprite.Create(texture, rect, pivot);

                    menuView.SetBackgroundImage(sprite);
                } else
                {
                    Log.Error("背景画像のテクスチャがロードできませんでした。パスを確認してください: " + fullPath);
                }
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
                } catch (Exception e)
                {
                    Log.Error($"README.txtを開くことができませんでした:\n{e}");
                }
            } else
            {
                Log.Error($"README.txtが次のパスに見つかりませんでした: {path}");
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

        private void OnDestroy()
        {
            menuView.OnHelpAction = null;
            menuView.OnModelSettingAction = null;
            menuView.OnAppSettingAction = null;
            menuView.OnCloseAction = null;

            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();

#if UNITY_EDITOR
            OnDestroyEditor();
#endif
        }
    }
}
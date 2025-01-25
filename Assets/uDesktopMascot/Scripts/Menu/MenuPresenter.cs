using System;
using System.IO;
using Unity.Logging;
using UnityEngine;

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
        public bool IsOpened { get;private set; }

        /// <summary>
        ///  メニューの表示位置のオフセット
        /// </summary>
        private static readonly Vector3 MenuOffset = new Vector3(2.5f, 2, -1);

        private void Awake()
        {
            IsOpened = false;
            
            menuView.OnHelpAction = OpenHelp;
            menuView.OnModelSettingAction = () => { Log.Debug("ModelSetting"); };
            menuView.OnAppSettingAction = () => { Log.Debug("AppSetting"); };
            menuView.OnCloseAction = CloseApp;

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
            
#if UNITY_EDITOR
            OnDestroyEditor();
#endif
        }
    }
}
using System;
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
            menuView.OnModelSettingAction = () => { Debug.Log("ModelSetting"); };
            menuView.OnAppSettingAction = () => { Debug.Log("AppSetting"); };
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
        /// ヘルプページを開く
        /// </summary>
        private void OpenHelp()
        {
            Debug.Log("Help");
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
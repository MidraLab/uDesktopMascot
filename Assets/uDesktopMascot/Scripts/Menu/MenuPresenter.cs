using System;
using UnityEngine;

namespace uDesktopMascot
{
    /// <summary>
    ///    メニューのプレゼンター
    /// </summary>
    public class MenuPresenter : MonoBehaviour
    {
        /// <summary>
        ///    メニューのビュー
        /// </summary>
        [SerializeField] private MenuView menuView;
        
        /// <summary>
        ///   メニューが開かれているかどうか
        /// </summary>
        public bool IsOpened { get;private set; }

        private void Awake()
        {
            IsOpened = false;
            
            menuView.OnHelpAction = OpenHelp;
            menuView.OnModelSettingAction = () => { Debug.Log("ModelSetting"); };
            menuView.OnAppSettingAction = () => { Debug.Log("AppSetting"); };
            menuView.OnCloseAction = CloseApp;
        }

        /// <summary>
        ///   メニューを表示する
        /// </summary>
        public void Show()
        {
            IsOpened = true;
            menuView.Show();
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
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        } 
    }
}
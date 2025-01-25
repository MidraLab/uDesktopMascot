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

        private void Awake()
        {
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
            menuView.Show();
        }
        
        /// <summary>
        ///  メニューを非表示にする
        /// </summary>
        public void Hide()
        {
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
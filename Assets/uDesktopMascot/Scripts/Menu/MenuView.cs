using System;
using UnityEngine;
using UnityEngine.UI;

namespace uDesktopMascot
{
    /// <summary>
    ///     メニューのビュー
    /// </summary>
    public class MenuView : MonoBehaviour
    {
        /// <summary>
        ///    メニューキャンバス
        /// </summary>
        private Canvas _menuCanvas;

        private void Awake()
        {
            _menuCanvas = GetComponent<Canvas>();
        }

        /// <summary>
        ///     モデル設定ボタン
        /// </summary>
        [SerializeField] private Button modelSettingButton;

        /// <summary>
        ///     便利機能ボタン
        /// </summary>
        [SerializeField] private Button helpButton;

        /// <summary>
        ///     アプリケーション設定
        /// </summary>
        [SerializeField] private Button appSettingButton;

        /// <summary>
        ///     アプリ終了ボタン
        /// </summary>
        [SerializeField] private Button closeButton;
        
        /// <summary>
        ///    ヘルプボタン
        /// </summary>
        public Action OnHelpAction { get; set; }
        
        /// <summary>
        ///    モデル設定ボタンのクリックイベント
        /// </summary>
        public Action OnModelSettingAction { get; set; }
        
        /// <summary>
        ///   アプリケーション設定ボタンのクリックイベント
        /// </summary>
        public Action OnAppSettingAction { get; set; }
        
        /// <summary>
        /// アプリ終了ボタンのクリックイベント
        /// </summary>
        public Action OnCloseAction { get; set; }

        /// <summary>
        ///    モデル設定ボタンのクリックイベント
        /// </summary>
        public void Show()
        {
            _menuCanvas.enabled = true;
        }
        
        /// <summary>
        ///   メニューを非表示にする
        /// </summary>
        public void Hide()
        {
            _menuCanvas.enabled = false;
        }
    }
}
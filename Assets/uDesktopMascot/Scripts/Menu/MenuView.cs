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

        /// <summary>
        ///    メニューのRectTransform
        /// </summary>
        private RectTransform _menuRectTransform;

        /// <summary>
        /// メニューの背景画像
        /// </summary>
        [SerializeField] private Image backgroundImage;

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
        [SerializeField] private Button quitButton;

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

        private void Awake()
        {
            _menuCanvas = GetComponent<Canvas>();
            _menuRectTransform = GetComponent<RectTransform>();
            SetButtonEvent();
        }

        /// <summary>
        /// ボタンのイベントの登録
        /// </summary>
        private void SetButtonEvent()
        {
            helpButton.onClick.AddListener(() => OnHelpAction?.Invoke());
            modelSettingButton.onClick.AddListener(() => OnModelSettingAction?.Invoke());
            appSettingButton.onClick.AddListener(() => OnAppSettingAction?.Invoke());
            quitButton.onClick.AddListener(() => OnCloseAction?.Invoke());
        }

        /// <summary>
        ///    メニューを表示する
        /// </summary>
        /// <param name="screenPosition"></param>
        public void Show(Vector3 screenPosition)
        {
            _menuCanvas.enabled = true;

            // メニューの位置を調整して、画面内に収まるようにする
            AdjustMenuPosition(screenPosition);
        }

        /// <summary>
        ///   メニューを非表示にする
        /// </summary>
        public void Hide()
        {
            _menuCanvas.enabled = false;
        }
        
        /// <summary>
        /// 背景色を設定する
        /// </summary>
        /// <param name="color"></param>
        public void SetBackgroundColor(Color color)
        {
            backgroundImage.color = color;
        }
        
        /// <summary>
        /// 背景画像を設定する
        /// </summary>
        /// <param name="sprite"></param>
        public void SetBackgroundImage(Sprite sprite)
        {
            backgroundImage.sprite = sprite;
        }

        /// <summary>
        /// メニューの位置を調整して、画面内に収まるようにする
        /// </summary>
        /// <param name="screenPosition">表示したいスクリーン座標</param>
        private void AdjustMenuPosition(Vector3 screenPosition)
        {
            // メニューのサイズを取得
            Vector2 menuSize = _menuRectTransform.sizeDelta;

            // スクリーンの幅と高さを取得
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            // RectTransformのpivotを考慮して、メニューの四隅の位置を計算
            Vector2 pivotOffset = new Vector2(menuSize.x * _menuRectTransform.pivot.x,
                menuSize.y * _menuRectTransform.pivot.y);

            // メニューの表示位置を調整するための変数
            Vector3 adjustedPosition = screenPosition;

            // メニューが画面の右端を超える場合の補正
            float rightEdge = adjustedPosition.x + (menuSize.x - pivotOffset.x);
            if (rightEdge > screenWidth)
            {
                adjustedPosition.x -= (rightEdge - screenWidth);
            }

            // メニューが画面の左端を超える場合の補正
            float leftEdge = adjustedPosition.x - pivotOffset.x;
            if (leftEdge < 0)
            {
                adjustedPosition.x -= leftEdge;
            }

            // メニューが画面の上端を超える場合の補正
            float topEdge = adjustedPosition.y + (menuSize.y - pivotOffset.y);
            if (topEdge > screenHeight)
            {
                adjustedPosition.y -= (topEdge - screenHeight);
            }

            // メニューが画面の下端を超える場合の補正
            float bottomEdge = adjustedPosition.y - pivotOffset.y;
            if (bottomEdge < 0)
            {
                adjustedPosition.y -= bottomEdge;
            }

            // RectTransformの位置を設定
            _menuRectTransform.position = adjustedPosition;
        }
    }
}
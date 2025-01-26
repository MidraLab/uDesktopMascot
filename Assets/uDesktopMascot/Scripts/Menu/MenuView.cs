using System;
using System.IO;
using Unity.Logging;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UniTask = Cysharp.Threading.Tasks.UniTask;
using UniTaskExtensions = Cysharp.Threading.Tasks.UniTaskExtensions;

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
            ApplyMenuUISettings();
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
        /// メニューの表示設定を適用する
        /// </summary>
        private void ApplyMenuUISettings()
        {
            var menuUISettings = ApplicationSettings.Instance.MenuUI;

            // 背景色を適用
            if (!string.IsNullOrEmpty(menuUISettings.BackgroundColor))
            {
                if (ColorUtility.TryParseHtmlString(menuUISettings.BackgroundColor, out Color color))
                {
                    backgroundImage.color = color;
                } else
                {
                    Log.Warning("背景色の指定が不正です。正しいカラーコードを設定してください。");
                }
            }

            // 背景画像を適用
            if (!string.IsNullOrEmpty(menuUISettings.BackgroundImagePath))
            {
                LoadBackgroundImage(menuUISettings.BackgroundImagePath);
            }
        }

        /// <summary>
        /// 背景画像をロードする
        /// </summary>
        /// <param name="relativePath"></param>
        private void LoadBackgroundImage(string relativePath)
        {
            UniTaskExtensions.Forget(LoadBackgroundImageCoroutine(relativePath));
        }

        /// <summary>
        /// 背景画像をロードするコルーチン
        /// </summary>
        /// <param name="relativePath"></param>
        private async UniTask LoadBackgroundImageCoroutine(string relativePath)
        {
            string fullPath = Path.Combine(Application.streamingAssetsPath, relativePath);

            UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(fullPath);

            await uwr.SendWebRequest();

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

                    backgroundImage.sprite = sprite;
                    backgroundImage.color = Color.white; // スプライトの色が正しく表示されるようにする
                    backgroundImage.type = Image.Type.Sliced; // 必要に応じて設定
                } else
                {
                    Log.Error("背景画像のテクスチャがロードできませんでした。パスを確認してください: " + fullPath);
                }
            }
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
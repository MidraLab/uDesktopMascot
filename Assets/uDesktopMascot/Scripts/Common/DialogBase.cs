using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace uDesktopMascot
{
    /// <summary>
    ///   ダイアログのベースクラス
    /// </summary>
    public class DialogBase : MonoBehaviour
    {
        /// <summary>
        /// キャンバス
        /// </summary>
        private protected Canvas Canvas;
        
        /// <summary>
        /// キャンバスグループ
        /// </summary>
        private protected CanvasGroup CanvasGroup;
        
        /// <summary>
        /// 閉じるボタン
        /// </summary>
        [SerializeField] private Button closeButton;
        
        /// <summary>
        /// 閉じるボタンのクリックイベント
        /// </summary>
        public Action OnClose { get; set; }
        
        /// <summary>
        ///  awake
        /// </summary>
        private protected virtual void Awake()
        {
            Canvas = GetComponent<Canvas>();
            CanvasGroup = GetComponent<CanvasGroup>();
            
            closeButton.onClick.AddListener(() =>
            {
                OnClose?.Invoke();
            });
        }

        /// <summary>
        ///  ダイアログを表示する
        /// </summary>
        public virtual void Show()
        {
            Canvas.enabled = true;
        }
        
        /// <summary>
        /// ダイアログを表示する
        /// </summary>
        public virtual async UniTask ShowAsync(CancellationToken cancellationToken = default,float fadeAnimationTime = Constant.UIAnimationTime,Ease ease = Constant.UIAnimationDefaultEase)
        {
            Show();
            
            // フェードイン
            await LMotion.Create(0f, 1f, fadeAnimationTime)
                .WithEase(ease)
                .BindToAlpha(CanvasGroup).ToUniTask(cancellationToken: cancellationToken);
        }
        
        /// <summary>
        /// ダイアログを非表示にする
        /// </summary>
        public virtual void Hide()
        {
            Canvas.enabled = false;
        }
        
        /// <summary>
        /// ダイアログを非表示にする
        /// </summary>
        public virtual async UniTask HideAsync(CancellationToken cancellationToken = default,float fadeAnimationTime = Constant.UIAnimationTime,Ease ease = Constant.UIAnimationDefaultEase)
        {
            // フェードアウト
            await LMotion.Create(1f, 0f, fadeAnimationTime)
                .WithEase(ease)
                .BindToAlpha(CanvasGroup).ToUniTask(cancellationToken: cancellationToken);
            
            Hide();
        }
    }
}
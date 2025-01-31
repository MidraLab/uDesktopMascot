using System;
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
        private protected Canvas _canvas;
        
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
            _canvas = GetComponent<Canvas>();
            
            closeButton.onClick.AddListener(() =>
            {
                OnClose?.Invoke();
                Hide();
            });
        }

        /// <summary>
        ///  ダイアログを表示する
        /// </summary>
        public virtual void Show()
        {
            _canvas.enabled = true;
        }
        
        /// <summary>
        /// ダイアログを非表示にする
        /// </summary>
        public void Hide()
        {
            _canvas.enabled = false;
        }
    }
}
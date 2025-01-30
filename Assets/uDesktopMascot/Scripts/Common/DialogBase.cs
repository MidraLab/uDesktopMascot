using UnityEngine;

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
        ///  awake
        /// </summary>
        private protected virtual void Awake()
        {
            _canvas = GetComponent<Canvas>();
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
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace uDesktopMascot
{
    /// <summary>
    ///    メニューボタンクラス
    /// </summary>
    public partial class MenuButton : MonoBehaviour
    {
        /// <summary>
        ///    ボタンのアイコン
        /// </summary>
        [SerializeField] private Image icon;
        
        /// <summary>
        ///   ボタンのテキスト
        /// </summary>
        [SerializeField] private TextMeshProUGUI buttonText;
        
        /// <summary>
        ///  ボタン
        /// </summary>
        private Button _button;
        
        /// <summary>
        ///  ボタンのテキストを設定する
        /// </summary>
        /// <param name="text"></param>
        public void SetText(string text)
        {
            buttonText.text = text;
        }
        
        /// <summary>
        ///   ボタンのアイコンを設定する
        /// </summary>
        /// <param name="sprite"></param>
        public void SetIcon(Sprite sprite)
        {
            icon.sprite = sprite;
        }
    }
}
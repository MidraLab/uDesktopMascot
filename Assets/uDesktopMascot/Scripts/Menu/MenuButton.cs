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
        ///   ボタンの背景画像
        /// </summary>
        [SerializeField] private Image buttonBackgroundImage;
        
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
        /// ボタンの背景色を設定する
        /// </summary>
        /// <param name="color"></param>
        public void SetBackgroundColor(Color color)
        {
            buttonBackgroundImage.color = color;
        }

        /// <summary>
        ///  ボタンの背景画像を設定する
        /// </summary>
        /// <param name="sprite"></param>
        public void SetBackgroundImage(Sprite sprite)
        {
            buttonBackgroundImage.sprite = sprite;
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
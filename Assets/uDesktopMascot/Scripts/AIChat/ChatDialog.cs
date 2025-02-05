using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace uDesktopMascot
{
    /// <summary>
    /// チャットダイアログ
    /// </summary>
    public class ChatDialog : DialogBase
    {
        /// <summary>
        /// チャットダイアログの送信ボタン
        /// </summary>
        [SerializeField] private TMP_InputField inputField;
        
        /// <summary>
        /// チャットダイアログの送信ボタン
        /// </summary>
        [SerializeField] private Button sendButton;
        
        /// <summary>
        /// チャットダイアログのテキスト
        /// </summary>
        [SerializeField] private List<TextMeshProUGUI> chatTexts;

        public void SetEvents()
        {
            
        }
    }
}
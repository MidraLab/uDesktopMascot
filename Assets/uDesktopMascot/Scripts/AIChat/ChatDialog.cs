using System.Text;
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
        [SerializeField] private TextMeshProUGUI chatText;
        
        /// <summary>
        /// チャットテキストビルダー
        /// </summary>
        private readonly StringBuilder _chatTextBuilder = new StringBuilder();

        private void Start()
        {
            SetEvents();
        }

        /// <summary>
        /// チャットテキストを更新する
        /// </summary>
        private void UpdateChatText()
        {
            _chatTextBuilder.AppendLine(inputField.text);
            chatText.text = _chatTextBuilder.ToString();
        }

        /// <summary>
        /// メッセージを送信する
        /// </summary>
        private void SendMessages()
        {
            UpdateChatText();
            inputField.text = string.Empty;
        }

        /// <summary>
        /// イベントを設定する
        /// </summary>
        public void SetEvents()
        {
            sendButton.onClick.AddListener(SendMessages);
        }

        private void OnDestroy()
        {
            sendButton.onClick.RemoveAllListeners();
        }
    }
}
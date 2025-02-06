using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using LLMUnity;

namespace uDesktopMascot
{
    /// <summary>
    /// チャットダイアログ
    /// </summary>
    public class ChatDialog : DialogBase
    {
        /// <summary>
        /// チャットダイアログの入力フィールド
        /// </summary>
        [SerializeField] private TMP_InputField inputField;
        
        /// <summary>
        /// チャットダイアログの送信ボタン
        /// </summary>
        [SerializeField] private Button sendButton;
        
        /// <summary>
        /// チャットダイアログのテキスト表示
        /// </summary>
        [SerializeField] private TextMeshProUGUI chatText;
        
        /// <summary>
        /// LLMキャラクター
        /// </summary>
        [SerializeField] private LLMCharacter llmCharacter;
        
        /// <summary>
        /// チャット履歴テキストビルダー
        /// </summary>
        private readonly StringBuilder _chatTextBuilder = new StringBuilder();
        
        /// <summary>
        /// AIの返信テキストビルダー
        /// </summary>
        private readonly StringBuilder _replyTextBuilder = new StringBuilder();

        private void Start()
        {
            SetEvents();
        }

        /// <summary>
        /// メッセージを送信する
        /// </summary>
        private void SendMessages()
        {
            // ユーザーのメッセージをチャット履歴に追加
            _chatTextBuilder.AppendLine("あなた: " + inputField.text);
            chatText.text = _chatTextBuilder.ToString();

            // AIの返信を初期化
            _replyTextBuilder.Clear();

            // LLMにユーザーのメッセージを送信し、返信を処理
            _ = llmCharacter.Chat(inputField.text, HandleReply, ReplyCompleted);

            // 入力フィールドをクリア
            inputField.text = string.Empty;
        }

        /// <summary>
        /// AIの返信を処理する
        /// </summary>
        /// <param name="reply">AIからの部分的な返信</param>
        private void HandleReply(string reply)
        {
            // AIの返信をビルダーに追加
            _replyTextBuilder.Append(reply);

            // 現在のチャット履歴と進行中のAI返信を表示
            chatText.text = _chatTextBuilder + "\nAI: " + _replyTextBuilder.ToString();
        }

        /// <summary>
        /// AIの返信が完了したときの処理
        /// </summary>
        private void ReplyCompleted()
        {
            // 最終的なAIの返信をチャット履歴に追加
            _chatTextBuilder.AppendLine("AI: " + _replyTextBuilder.ToString());
            _replyTextBuilder.Clear();
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
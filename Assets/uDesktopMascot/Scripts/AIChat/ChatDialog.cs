using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
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
        /// AIの返信を蓄積するビルダー
        /// </summary>
        private StringBuilder _replyTextBuilder;

        /// <summary>
        /// 入力をブロックするフラグ
        /// </summary>
        private bool _inputBlocked = false;

        /// <summary>
        /// 前回の返信の長さを記録する変数
        /// </summary>
        private int _lastReplyLength = 0;

        private void Start()
        {
            SetEvents();
        }

        private void OnEnable()
        {
            // Submitアクションにリスナーを追加
            InputController.Instance.UI.Submit.performed += OnSubmit;
        }

        private void OnDisable()
        {
            // Submitアクションのリスナーを削除
            InputController.Instance.UI.Submit.performed -= OnSubmit;
        }

        /// <summary>
        /// Submitアクションが実行されたときの処理（Enterキー）
        /// </summary>
        private void OnSubmit(InputAction.CallbackContext context)
        {
            // 入力フィールドが選択されている場合のみ処理
            if (inputField.isFocused)
            {
                SendMessages();

                // InputFieldが改行を追加しないようにする
                inputField.DeactivateInputField();
                inputField.ActivateInputField();
            }
        }

        /// <summary>
        /// メッセージを送信する
        /// </summary>
        private void SendMessages()
        {
            if (_inputBlocked || string.IsNullOrWhiteSpace(inputField.text))
            {
                return;
            }

            // 入力をブロック
            _inputBlocked = true;
            sendButton.interactable = false;
            inputField.interactable = false;

            // ユーザーのメッセージをチャット履歴に追加
            string userMessage = inputField.text;
            _chatTextBuilder.AppendLine($"あなた: {userMessage}");
            chatText.text = _chatTextBuilder.ToString();

            // 入力フィールドをクリア
            inputField.text = string.Empty;

            // AIの返信用のStringBuilderを初期化
            _replyTextBuilder = new StringBuilder();

            // 前回の返信の長さをリセット
            _lastReplyLength = 0;

            // LLMにユーザーのメッセージを送信し、返信を処理
            _ = llmCharacter.Chat(
                userMessage,
                HandleReply,
                ReplyCompleted
            );
        }

        /// <summary>
        /// AIの返信を処理する（ストリーミング対応）
        /// </summary>
        /// <param name="reply">累積されたAIからの返信</param>
        private void HandleReply(string reply)
        {
            // 新しく追加された部分のみを取得
            string newText = reply.Substring(_lastReplyLength);
            _lastReplyLength = reply.Length;

            // AIの返信をビルダーに追加
            _replyTextBuilder.Append(newText);

            // 現在のチャット履歴と進行中のAI返信を表示
            chatText.text = $"{_chatTextBuilder}AI: {_replyTextBuilder}";
        }

        /// <summary>
        /// AIの返信が完了したときの処理
        /// </summary>
        private void ReplyCompleted()
        {
            // 最終的なAIの返信をチャット履歴に追加
            _chatTextBuilder.AppendLine($"AI: {_replyTextBuilder}");
            chatText.text = _chatTextBuilder.ToString();

            // AIの返信用ビルダーをクリア
            _replyTextBuilder = null;

            // 入力をアンブロック
            _inputBlocked = false;
            sendButton.interactable = true;
            inputField.interactable = true;
        }

        /// <summary>
        /// イベントを設定する
        /// </summary>
        private void SetEvents()
        {
            sendButton.onClick.AddListener(SendMessages);
        }

        private void OnDestroy()
        {
            sendButton.onClick.RemoveAllListeners();

            // リスナーの登録解除
            if (InputController.Instance != null)
            {
                InputController.Instance.UI.Submit.performed -= OnSubmit;
            }
        }
    }
}
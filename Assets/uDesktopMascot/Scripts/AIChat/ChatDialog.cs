using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using LLMUnity;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using Unity.Logging;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

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
        /// チャットダイアログのスクロールビュー
        /// </summary>
        [SerializeField] private ScrollRect scrollRect;

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

        private protected override void OnEnable()
        {
            base.OnEnable();
            // Submitアクションにリスナーを追加
            InputController.Instance.UI.Submit.performed += OnSubmit;
        }

        private protected override void OnDisable()
        {
            base.OnDisable();
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
        /// チャットダイアログを表示する
        /// </summary>
        private void ScrollToBottom()
        {
            // レイアウトを強制的に更新
            Canvas.ForceUpdateCanvases();
            // ScrollRectのverticalNormalizedPositionを0に設定（0が一番下、1が一番上）
            scrollRect.verticalNormalizedPosition = 0f;
            // レイアウトを再度更新
            Canvas.ForceUpdateCanvases();
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

            // ScrollToBottomを呼び出して最新のメッセージを表示
            ScrollToBottom();

            // 入力フィールドをクリア
            inputField.text = string.Empty;

            // AIの返信用のStringBuilderを初期化
            _replyTextBuilder = new StringBuilder();

            // 前回の返信の長さをリセット
            _lastReplyLength = 0;

            // LLMにユーザーのメッセージを送信し、返信を処理
            _ = ReceiveAIResponse(userMessage);
        }

        /// <summary>
        /// 非同期でAIの返信を受信
        /// </summary>
        private async UniTask ReceiveAIResponse(string userMessage)
        {
            try
            {
                // llmCharacter.Chat を呼び出し、返信を受信
                await llmCharacter.Chat(
                    userMessage,
                    HandleReply,
                    ReplyCompleted
                );
            }
            catch (Exception ex)
            {
                Log.Error($"AIの返信の受信中にエラーが発生しました。{ex.Message}");
                // エラーが発生した場合、入力をアンブロック
                _inputBlocked = false;
                sendButton.interactable = true;
                inputField.interactable = true;
            }
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
            using (var sb = ZString.CreateStringBuilder())
            {
                sb.Append(_chatTextBuilder.ToString());
                sb.Append($"AI: {_replyTextBuilder}");
                chatText.text = sb.ToString();
            }

            // ScrollToBottomを呼び出して最新のメッセージを表示
            ScrollToBottom();
        }

        /// <summary>
        /// AIの返信が完了したときの処理
        /// </summary>
        private void ReplyCompleted()
        {
            // 最終的なAIの返信をチャット履歴に追加
            _chatTextBuilder.AppendLine($"AI: {_replyTextBuilder}");
            chatText.text = _chatTextBuilder.ToString();

            // ScrollToBottomを呼び出して最新のメッセージを表示
            ScrollToBottom();

            // AIの返信用ビルダーをクリア
            _replyTextBuilder = null;

            // 入力をアンブロック
            _inputBlocked = false;
            sendButton.interactable = true;
            inputField.interactable = true;

            // 入力フィールドにフォーカスをセット
            inputField.ActivateInputField();
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
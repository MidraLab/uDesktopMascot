using System;

namespace uDesktopMascot
{
    /// <summary>
    ///    AIチャットメッセージモデル
    /// </summary>
    public class ChatMessage
    {
        public enum Sender
        {
            User,
            AI
        }

        /// <summary>
        ///  送信者情報
        /// </summary>
        public Sender SenderInfo;

        /// <summary>
        /// メッセージ
        /// </summary>
        public string Message;

        /// <summary>
        /// タイムスタンプ
        /// </summary>
        public DateTime Timestamp;

        public ChatMessage(Sender senderInfo, string message, DateTime timestamp)
        {
            this.SenderInfo = senderInfo;
            this.Message = message;
            this.Timestamp = timestamp;
        }
    }
}
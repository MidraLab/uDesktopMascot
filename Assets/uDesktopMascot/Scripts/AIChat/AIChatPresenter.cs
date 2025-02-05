using System;

namespace uDesktopMascot
{
    /// <summary>
    ///   AIチャットプレゼンター
    /// </summary>
    public class AIChatPresenter : IDisposable
    {
        /// <summary>
        /// チャットダイアログ
        /// </summary>
        private ChatDialog _chatDialog;
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AIChatPresenter(ChatDialog chatDialog)
        {
            _chatDialog = chatDialog;
        }
        
        public void Dispose()
        {
        }
    }
}
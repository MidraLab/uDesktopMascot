namespace uDesktopMascot
{
    /// <summary>
    /// ダイアログプール
    /// </summary>
    public interface IDialogPool
    {
        /// <summary>
        /// ダイアログを取得
        /// </summary>
        /// <returns></returns>
        DialogBase Get();
        
        /// <summary>
        /// ダイアログを解放
        /// </summary>
        /// <param name="dialog"></param>
        void Release(DialogBase dialog);
    }
}
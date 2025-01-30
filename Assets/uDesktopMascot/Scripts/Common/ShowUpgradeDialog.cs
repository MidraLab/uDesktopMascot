using TMPro;
using UnityEngine;

namespace uDesktopMascot
{
    /// <summary>
    ///  アップグレードダイアログを表示する
    /// </summary>
    public class ShowUpgradeDialog : DialogBase
    {
        /// <summary>
        /// メッセージテキスト
        /// </summary>
        [SerializeField] private TextMeshProUGUI messageText;

        /// <summary>
        /// テーブル名の定数
        /// </summary>
        private const string TableName = "LocalizationTable";

        /// <summary>
        /// メッセージを設定する
        /// </summary>
        /// <param name="latestVersion">最新バージョン番号</param>
        private void SetMessage(string latestVersion)
        {
            // LocalizationUtility を使用してローカライズされた文字列を同期的に取得
            string message = LocalizationUtility.GetLocalizedStringSync(TableName, "MSG_NEW_VERSION", latestVersion);

            // メッセージテキストを更新
            messageText.text = message;
        }

        public void Show(string latestVersion)
        {
            base.Show();
            SetMessage(latestVersion);
        }
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace uDesktopMascot
{
    /// <summary>
    ///  アップデートダイアログを表示する
    /// </summary>
    public class ShowUpdateDialog : DialogBase
    {
        /// <summary>
        /// メッセージテキスト
        /// </summary>
        [SerializeField] private TextMeshProUGUI messageText;
        
        /// <summary>
        /// アップグレードダイアログをスキップするかどうかのトグル
        /// </summary>
        [SerializeField] private Toggle skipShowUpgradeDialogToggle;
        
        /// <summary>
        /// アップグレードダイアログをスキップするかどうか
        /// </summary>
        public bool SkipShowUpgradeDialog
        {
            get => skipShowUpgradeDialogToggle.isOn;
            set => skipShowUpgradeDialogToggle.isOn = value;
        }

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

        /// <summary>
        /// ダイアログを表示する
        /// </summary>
        /// <param name="latestVersion"></param>
        public void Show(string latestVersion)
        {
            base.Show();
            SetMessage(latestVersion);
        }
    }
}
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace uDesktopMascot
{
    /// <summary>
    ///  アップデートダイアログを表示する
    /// </summary>
    public class ShowUpdateDialog : DialogBase
    {
        /// <summary>
        /// 最新バージョンのローカライズされた文字列イベント
        /// </summary>
        [SerializeField] private LocalizeStringEvent latestVersionLocalizedStringEvent;

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
        /// メッセージを設定する
        /// </summary>
        /// <param name="latestVersion">最新バージョン番号</param>
        private void SetMessage(string latestVersion)
        {
            latestVersionLocalizedStringEvent.StringReference.Arguments = new object[] {latestVersion};
            latestVersionLocalizedStringEvent.StringReference.RefreshString();
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
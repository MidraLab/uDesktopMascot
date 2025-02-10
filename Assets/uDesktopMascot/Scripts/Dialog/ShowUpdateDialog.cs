using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
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
        /// アプリのリンクテキスト
        /// </summary>
        [SerializeField] private TextMeshProUGUI appLinkText;

        /// <summary>
        /// アプリのリンクボタン
        /// </summary>
        [SerializeField] private Button appLinkButton;

        public override void Initialize()
        {
            base.Initialize();
            SetButtonEvent();
        }

        /// <summary>
        /// ボタンのイベントを設定する
        /// </summary>
        private void SetButtonEvent()
        {
            appLinkButton.onClick.AddListener(() =>
            {
                string text = appLinkText.text;
                string url = ExtractUrlFromLinkTag(text);
                if (!string.IsNullOrEmpty(url))
                {
                    Application.OpenURL(url);
                }
                else
                {
                    Debug.LogError("No valid URL found in the link tag.");
                }
            });
        }

        /// <summary>
        /// Extracts the URL from a TMP link tag in the format <link="URL">Text</link>.
        /// </summary>
        /// <param name="text">The TMP text containing the link tag.</param>
        /// <returns>The extracted URL, or null if not found.</returns>
        private string ExtractUrlFromLinkTag(string text)
        {
            const string linkTagStart = "<link=\"";
            const string linkTagEnd = "\">";

            int linkIndex = text.IndexOf(linkTagStart, StringComparison.Ordinal);
            if (linkIndex >= 0)
            {
                int startQuote = linkIndex + linkTagStart.Length;
                int endQuote = text.IndexOf(linkTagEnd, startQuote, StringComparison.Ordinal);
                if (endQuote > startQuote)
                {
                    return text.Substring(startQuote, endQuote - startQuote);
                }
            }
            return null;
        }
        
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
        /// <param name="cancellationToken"></param>
        public async UniTask ShowAsync(string latestVersion,CancellationToken cancellationToken)
        {
            await ShowAsync(cancellationToken);
            
            SetMessage(latestVersion);
        }
    }
}
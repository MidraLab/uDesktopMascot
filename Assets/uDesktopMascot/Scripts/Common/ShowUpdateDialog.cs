using System.Threading;
using Cysharp.Threading.Tasks;
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
        /// キャンセルトークンソース
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource;

        private protected override void Awake()
        {
            base.Awake();
            _cancellationTokenSource = new CancellationTokenSource();
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
        /// テーブル名の定数
        /// </summary>
        private const string TableName = "LocalizationTable";

        /// <summary>
        /// メッセージを設定する
        /// </summary>
        /// <param name="latestVersion">最新バージョン番号</param>
        /// <param name="cancellationToken"></param>
        private async UniTask SetMessage(string latestVersion,CancellationToken cancellationToken)
        {
            // LocalizationUtility を使用してローカライズされた文字列を同期的に取得
            string message = await LocalizationUtility.GetLocalizedStringAsync(TableName, "MSG_NEW_VERSION",cancellationToken, latestVersion);

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
            SetMessage(latestVersion,_cancellationTokenSource.Token).Forget();
        }

        private void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}
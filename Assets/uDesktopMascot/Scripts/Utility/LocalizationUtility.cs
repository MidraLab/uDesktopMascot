using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Logging;
using UnityEngine.Localization;

namespace uDesktopMascot
{
    /// <summary>
    /// ローカライズされた文字列を取得するためのユーティリティクラス
    /// </summary>
    public static class LocalizationUtility
    {
        /// <summary>
        /// デフォルトのテーブル名
        /// </summary>
        private const string DefaultTableName = "LocalizationTable";

        /// <summary>
        /// デフォルトのテーブル名を使用して、指定されたキーと引数で LocalizedString を取得します。
        /// </summary>
        /// <param name="key">ローカライズされた文字列のキー</param>
        /// <param name="arguments">プレースホルダーに挿入する引数</param>
        /// <returns>LocalizedString オブジェクト</returns>
        public static LocalizedString GetLocalizedString(string key, params object[] arguments)
        {
            return GetLocalizedStringFromTable(DefaultTableName, key, arguments);
        }

        /// <summary>
        /// 指定されたテーブル名、キー、引数で LocalizedString を取得します。
        /// </summary>
        /// <param name="tableName">String Table コレクション名</param>
        /// <param name="key">ローカライズされた文字列のキー</param>
        /// <param name="arguments">プレースホルダーに挿入する引数</param>
        /// <returns>LocalizedString オブジェクト</returns>
        public static LocalizedString GetLocalizedStringFromTable(string tableName, string key, params object[] arguments)
        {
            var localizedString = new LocalizedString(tableName, key);

            if (arguments != null && arguments.Length > 0)
            {
                localizedString.Arguments = arguments;
            }

            return localizedString;
        }

        /// <summary>
        /// ローカライズされた文字列を同期的に取得します。
        /// プリロード済みであることが前提です。
        /// </summary>
        /// <param name="localizedString">LocalizedString オブジェクト</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ローカライズされた文字列</returns>
        public static async UniTask<string> GetLocalizedStringAsync(LocalizedString localizedString, CancellationToken cancellationToken)
        {
            try
            {
                return await localizedString.GetLocalizedStringAsync().ToUniTask(cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                Log.Error("ローカライズ文字列の取得中にエラーが発生しました: " + ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// 指定されたテーブル名、キー、引数でローカライズされた文字列を同期的に取得します。
        /// プリロード済みであることが前提です。
        /// </summary>
        /// <param name="tableName">String Table コレクション名</param>
        /// <param name="key">ローカライズされた文字列のキー</param>
        /// <param name="arguments">プレースホルダーに挿入する引数</param>
        /// <param name="cancellationToken"></param>
        /// <returns>ローカライズされた文字列</returns>
        public static async UniTask<string> GetLocalizedStringAsync(string tableName, string key,CancellationToken cancellationToken, params object[] arguments)
        {
            var localizedString = GetLocalizedStringFromTable(tableName, key, arguments);
            return await GetLocalizedStringAsync(localizedString,cancellationToken);
        }
    }
}
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
        /// <returns>ローカライズされた文字列</returns>
        public static string GetLocalizedStringSync(LocalizedString localizedString)
        {
            var handle = localizedString.GetLocalizedStringAsync();

            if (handle.IsDone)
            {
                return handle.Result;
            }
            else
            {
                // プリロードされていない場合の対処
                handle.WaitForCompletion();
                return handle.Result;
            }
        }

        /// <summary>
        /// 指定されたテーブル名、キー、引数でローカライズされた文字列を同期的に取得します。
        /// プリロード済みであることが前提です。
        /// </summary>
        /// <param name="tableName">String Table コレクション名</param>
        /// <param name="key">ローカライズされた文字列のキー</param>
        /// <param name="arguments">プレースホルダーに挿入する引数</param>
        /// <returns>ローカライズされた文字列</returns>
        public static string GetLocalizedStringSync(string tableName, string key, params object[] arguments)
        {
            var localizedString = GetLocalizedStringFromTable(tableName, key, arguments);
            return GetLocalizedStringSync(localizedString);
        }

        /// <summary>
        /// デフォルトのテーブル名を使用して、指定されたキーと引数でローカライズされた文字列を同期的に取得します。
        /// プリロード済みであることが前提です。
        /// </summary>
        /// <param name="key">ローカライズされた文字列のキー</param>
        /// <param name="arguments">プレースホルダーに挿入する引数</param>
        /// <returns>ローカライズされた文字列</returns>
        public static string GetLocalizedStringSync(string key, params object[] arguments)
        {
            return GetLocalizedStringSync(DefaultTableName, key, arguments);
        }
    }
}
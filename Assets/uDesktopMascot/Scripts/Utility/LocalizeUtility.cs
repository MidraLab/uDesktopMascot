using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace uDesktopMascot
{
    /// <summary>
    ///    ローカライズユーティリティ
    /// </summary>
    public class LocalizeUtility
    {
        /// <summary>
        ///   ロケールを取得
        /// </summary>
        /// <param name="systemLanguage"></param>
        /// <returns></returns>
        public static Locale GetLocale(SystemLanguage systemLanguage)
        {
            // 利用可能なロケールを取得
            var availableLocales = LocalizationSettings.AvailableLocales.Locales;

            // システム言語に対応するロケールを検索
            foreach (var locale in availableLocales)
            {
                // ロケールのSystemLanguageと比較
                if (locale.Identifier.CultureInfo != null)
                {
                    if (locale.Identifier.CultureInfo.TwoLetterISOLanguageName == GetTwoLetterISOCode(systemLanguage))
                    {
                        return locale;
                    }
                }
                else if (locale.Identifier.Code == systemLanguage.ToString())
                {
                    return locale;
                }
            }

            // 見つからない場合はnullを返す
            return null;
        }
        
        /// <summary>
        ///   システム言語に対応するロケールを取得
        /// </summary>
        /// <param name="systemLanguage"></param>
        /// <returns></returns>
        private static string GetTwoLetterISOCode(SystemLanguage systemLanguage)
        {
            return systemLanguage switch
            {
                SystemLanguage.English => "en",
                SystemLanguage.French => "fr",
                SystemLanguage.Italian => "it",
                SystemLanguage.Japanese => "ja",
                SystemLanguage.Korean => "ko",
                _ => null
            };
        }
    }
}
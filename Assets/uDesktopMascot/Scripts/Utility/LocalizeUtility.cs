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
        
        /// <summary>
        /// 言語コードから対応する SystemLanguage を取得する
        /// </summary>
        /// <param name="languageCode">言語コード（例："en", "ja"）</param>
        /// <returns>対応する SystemLanguage</returns>
        public static SystemLanguage GetSystemLanguageFromCode(string languageCode)
        {
            return languageCode switch
            {
                "en" => SystemLanguage.English,
                "fr" => SystemLanguage.French,
                "it" => SystemLanguage.Italian,
                "ja" => SystemLanguage.Japanese,
                "ko" => SystemLanguage.Korean,
                _ => SystemLanguage.English
            };
        }
        
        /// <summary>
        /// SystemLanguage から対応する言語コード（国の文字）を取得する
        /// </summary>
        /// <param name="language">SystemLanguage</param>
        /// <returns>対応する言語コード（例："en", "ja"）</returns>
        public static string GetLanguageCodeFromSystemLanguage(SystemLanguage language)
        {
            return language switch
            {
                SystemLanguage.English => "en",
                SystemLanguage.French => "fr",
                SystemLanguage.Italian => "it",
                SystemLanguage.Japanese => "ja",
                SystemLanguage.Korean => "ko",
                _ => "en"
            };
        }
    }
}
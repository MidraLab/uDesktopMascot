#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace uDesktopMascot.Editor.EditorTest
{
    public class LocalizeUtilityTests
    {
        /// <summary>
        /// 利用可能なロケールのセットアップ
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            // テスト用に利用可能なロケールを設定
            var availableLocales = new LocalesProvider();
            availableLocales.AddLocale(Locale.CreateLocale(SystemLanguage.English));
            availableLocales.AddLocale(Locale.CreateLocale(SystemLanguage.French));
            availableLocales.AddLocale(Locale.CreateLocale(SystemLanguage.Italian));
            availableLocales.AddLocale(Locale.CreateLocale(SystemLanguage.Japanese));
            availableLocales.AddLocale(Locale.CreateLocale(SystemLanguage.Korean));
            LocalizationSettings.AvailableLocales = availableLocales;
        }

        /// <summary>
        /// サポートされている言語でロケールを取得できることをテスト
        /// </summary>
        [Test]
        public void GetLocale_SupportedLanguages_ReturnsLocale()
        {
            // 英語
            var locale = LocalizeUtility.GetLocale(SystemLanguage.English);
            Assert.IsNotNull(locale);
            Assert.AreEqual("en", locale.Identifier.Code);

            // フランス語
            locale = LocalizeUtility.GetLocale(SystemLanguage.French);
            Assert.IsNotNull(locale);
            Assert.AreEqual("fr", locale.Identifier.Code);

            // イタリア語
            locale = LocalizeUtility.GetLocale(SystemLanguage.Italian);
            Assert.IsNotNull(locale);
            Assert.AreEqual("it", locale.Identifier.Code);

            // 日本語
            locale = LocalizeUtility.GetLocale(SystemLanguage.Japanese);
            Assert.IsNotNull(locale);
            Assert.AreEqual("ja", locale.Identifier.Code);

            // 韓国語
            locale = LocalizeUtility.GetLocale(SystemLanguage.Korean);
            Assert.IsNotNull(locale);
            Assert.AreEqual("ko", locale.Identifier.Code);
        }

        /// <summary>
        /// サポートされていない言語でnullが返されることをテスト
        /// </summary>
        [Test]
        public void GetLocale_UnsupportedLanguages_ReturnsNull()
        {
            // ドイツ語（サポートされていない）
            var locale = LocalizeUtility.GetLocale(SystemLanguage.German);
            Assert.IsNull(locale);

            // スペイン語（サポートされていない）
            locale = LocalizeUtility.GetLocale(SystemLanguage.Spanish);
            Assert.IsNull(locale);

            // 中国語（サポートされていない）
            locale = LocalizeUtility.GetLocale(SystemLanguage.ChineseSimplified);
            Assert.IsNull(locale);
        }

        /// <summary>
        /// システム言語に対応するISOコードを正しく取得できることをテスト
        /// </summary>
        [Test]
        public void GetTwoLetterISOCode_SupportedLanguages_ReturnsCode()
        {
            // リフレクションを使用してプライベートメソッドをテスト
            var method = typeof(LocalizeUtility).GetMethod("GetTwoLetterISOCode", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

            // 英語
            var code = method.Invoke(null, new object[] { SystemLanguage.English }) as string;
            Assert.AreEqual("en", code);

            // フランス語
            code = method.Invoke(null, new object[] { SystemLanguage.French }) as string;
            Assert.AreEqual("fr", code);

            // イタリア語
            code = method.Invoke(null, new object[] { SystemLanguage.Italian }) as string;
            Assert.AreEqual("it", code);

            // 日本語
            code = method.Invoke(null, new object[] { SystemLanguage.Japanese }) as string;
            Assert.AreEqual("ja", code);

            // 韓国語
            code = method.Invoke(null, new object[] { SystemLanguage.Korean }) as string;
            Assert.AreEqual("ko", code);
        }

        /// <summary>
        /// サポートされていない言語でISOコードがnullであることをテスト
        /// </summary>
        [Test]
        public void GetTwoLetterISOCode_UnsupportedLanguages_ReturnsNull()
        {
            // リフレクションを使用してプライベートメソッドをテスト
            var method = typeof(LocalizeUtility).GetMethod("GetTwoLetterISOCode", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

            // ドイツ語
            var code = method.Invoke(null, new object[] { SystemLanguage.German }) as string;
            Assert.IsNull(code);

            // スペイン語
            code = method.Invoke(null, new object[] { SystemLanguage.Spanish }) as string;
            Assert.IsNull(code);

            // 中国語
            code = method.Invoke(null, new object[] { SystemLanguage.ChineseSimplified }) as string;
            Assert.IsNull(code);
        }
        
        [Test]
        public void ConvertLanguageTest()
        {
            // 正常な言語コードに対するテスト
            Assert.AreEqual(SystemLanguage.English, LocalizeUtility.GetSystemLanguageFromCode("en"), "言語コード 'en' の変換が正しくありません。");
            Assert.AreEqual(SystemLanguage.French,  LocalizeUtility.GetSystemLanguageFromCode("fr"), "言語コード 'fr' の変換が正しくありません。");
            Assert.AreEqual(SystemLanguage.Italian, LocalizeUtility.GetSystemLanguageFromCode("it"), "言語コード 'it' の変換が正しくありません。");
            Assert.AreEqual(SystemLanguage.Japanese, LocalizeUtility.GetSystemLanguageFromCode("ja"), "言語コード 'ja' の変換が正しくありません。");
            Assert.AreEqual(SystemLanguage.Korean,  LocalizeUtility.GetSystemLanguageFromCode("ko"), "言語コード 'ko' の変換が正しくありません。");

            // 定義されていない言語コードの場合はデフォルトで English を返すテスト
            Assert.AreEqual(SystemLanguage.English, LocalizeUtility.GetSystemLanguageFromCode("es"), "未定義の言語コード 'es' はデフォルトで English を返す必要があります。");

            // null の入力に対するテスト（必要に応じて対応）
            Assert.AreEqual(SystemLanguage.English, LocalizeUtility.GetSystemLanguageFromCode(null), "null の場合はデフォルトで English を返す必要があります。");
        }
        
        [Test]
        public void GetLanguageCodeFromSystemLanguage_ValidLanguages_ReturnsCorrectCode()
        {
            // 有効な SystemLanguage に対して、対応する言語コードが正しく返されるか検証
            Assert.AreEqual("en", LocalizeUtility.GetLanguageCodeFromSystemLanguage(SystemLanguage.English), "SystemLanguage.English に対して 'en' が返されるべきです。");
            Assert.AreEqual("fr", LocalizeUtility.GetLanguageCodeFromSystemLanguage(SystemLanguage.French),  "SystemLanguage.French に対して 'fr' が返されるべきです。");
            Assert.AreEqual("it", LocalizeUtility.GetLanguageCodeFromSystemLanguage(SystemLanguage.Italian), "SystemLanguage.Italian に対して 'it' が返されるべきです。");
            Assert.AreEqual("ja", LocalizeUtility.GetLanguageCodeFromSystemLanguage(SystemLanguage.Japanese),  "SystemLanguage.Japanese に対して 'ja' が返されるべきです。");
            Assert.AreEqual("ko", LocalizeUtility.GetLanguageCodeFromSystemLanguage(SystemLanguage.Korean),  "SystemLanguage.Korean に対して 'ko' が返されるべきです。");
        }

        [Test]
        public void GetLanguageCodeFromSystemLanguage_InvalidLanguage_ReturnsDefaultEnglish()
        {
            // SystemLanguage の定義にない値の場合、デフォルトとして "en" が返されるか検証
            SystemLanguage unsupportedLanguage = (SystemLanguage)999;
            Assert.AreEqual("en", LocalizeUtility.GetLanguageCodeFromSystemLanguage(unsupportedLanguage), "定義されていない SystemLanguage の場合、デフォルトで 'en' が返されるべきです。");
        }
    }
}
#endif
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
    }
}
#endif
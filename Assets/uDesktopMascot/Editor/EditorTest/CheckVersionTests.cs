#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
using NUnit.Framework;

namespace uDesktopMascot.Editor.EditorTest
{
    public class CheckVersionTests
    {
        /// <summary>
        /// バージョン比較が正しく行われることをテスト
        /// </summary>
        [Test]
        public void IsNewerVersion_NewVersion_ReturnsTrue()
        {
            // テスト対象のクラスのインスタンスを作成
            var checkVersion = new CheckVersion();

            // テストケース1: 最新バージョンが現在のバージョンより新しい場合
            var result = checkVersion.IsNewerVersion("2.0.0", "1.0.0");
            Assert.IsTrue(result);

            // テストケース2: 最新バージョンが現在のバージョンと同じ場合
            result = checkVersion.IsNewerVersion("1.0.0", "1.0.0");
            Assert.IsFalse(result);

            // テストケース3: 最新バージョンが現在のバージョンより古い場合
            result = checkVersion.IsNewerVersion("1.0.0", "2.0.0");
            Assert.IsFalse(result);
        }

        /// <summary>
        /// 不正なバージョン文字列が与えられた場合に例外が発生しないことをテスト
        /// </summary>
        [Test]
        public void IsNewerVersion_InvalidVersionFormat_ReturnsFalse()
        {
            var checkVersion = new CheckVersion();

            // 不正なバージョン文字列
            var result = checkVersion.IsNewerVersion("invalid_version", "1.0.0");
            Assert.IsFalse(result);

            result = checkVersion.IsNewerVersion("2.0.0", "invalid_version");
            Assert.IsFalse(result);

            result = checkVersion.IsNewerVersion("invalid_version", "invalid_version");
            Assert.IsFalse(result);
        }
    }
}
#endif
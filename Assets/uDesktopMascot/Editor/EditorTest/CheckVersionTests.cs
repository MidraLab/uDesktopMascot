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
            // テスト対象のメソッドを呼び出すために、CheckVersion のインスタンスを作成
            var checkVersion = new CheckVersion();

            // 非公開メソッド IsNewerVersion をリフレクションで取得
            var method = typeof(CheckVersion).GetMethod("IsNewerVersion", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            // メソッドが存在することを確認
            Assert.IsNotNull(method);

            // テストケース1: 最新バージョンが現在のバージョンより新しい場合
            var result = (bool)method.Invoke(checkVersion, new object[] { "2.0.0", "1.0.0" });
            Assert.IsTrue(result);

            // テストケース2: 最新バージョンが現在のバージョンと同じ場合
            result = (bool)method.Invoke(checkVersion, new object[] { "1.0.0", "1.0.0" });
            Assert.IsFalse(result);

            // テストケース3: 最新バージョンが現在のバージョンより古い場合
            result = (bool)method.Invoke(checkVersion, new object[] { "1.0.0", "2.0.0" });
            Assert.IsFalse(result);
        }

        /// <summary>
        /// 不正なバージョン文字列が与えられた場合に例外が発生しないことをテスト
        /// </summary>
        [Test]
        public void IsNewerVersion_InvalidVersionFormat_ReturnsFalse()
        {
            var checkVersion = new CheckVersion();
            var method = typeof(CheckVersion).GetMethod("IsNewerVersion", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Assert.IsNotNull(method);

            // 不正なバージョン文字列
            var result = (bool)method.Invoke(checkVersion, new object[] { "invalid_version", "1.0.0" });
            Assert.IsFalse(result);

            result = (bool)method.Invoke(checkVersion, new object[] { "2.0.0", "invalid_version" });
            Assert.IsFalse(result);

            result = (bool)method.Invoke(checkVersion, new object[] { "invalid_version", "invalid_version" });
            Assert.IsFalse(result);
        }
    }
}
#endif
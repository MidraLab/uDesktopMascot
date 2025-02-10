#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
using NUnit.Framework;
using System.IO;
using UnityEngine;

namespace uDesktopMascot.Editor.EditorTest
{
    public class ModelCacheUtilityTests
    {
        private string testModelPath;
        private string testCacheFolderPath;

        [SetUp]
        public void SetUp()
        {
            // テスト用の一時モデルファイルパスを設定
            testModelPath = Path.Combine(Application.temporaryCachePath, "test_model.vrm");

            // テスト用のダミーモデルファイルを作成
            File.WriteAllText(testModelPath, "Dummy VRM model content");

            // テスト用のキャッシュフォルダパス
            testCacheFolderPath = Path.Combine(Application.persistentDataPath, "ModelCache");

            // 事前にキャッシュフォルダを削除しておく
            if (Directory.Exists(testCacheFolderPath))
            {
                Directory.Delete(testCacheFolderPath, true);
            }
        }

        [TearDown]
        public void TearDown()
        {
            // テスト後に一時ファイルを削除
            if (File.Exists(testModelPath))
            {
                File.Delete(testModelPath);
            }

            // キャッシュフォルダを削除
            if (Directory.Exists(testCacheFolderPath))
            {
                Directory.Delete(testCacheFolderPath, true);
            }
        }

        [Test]
        public void ComputeHash_ValidFile_ReturnsHash()
        {
            // Arrange

            // Act
            string hash = ModelCacheUtility.ComputeHash(testModelPath);

            // Assert
            Assert.IsNotNull(hash);
            Assert.IsTrue(hash.Length > 0);
        }

        [Test]
        public void GetCacheFolderPath_ReturnsValidPath()
        {
            // Arrange

            // Act
            string cacheFolderPath = ModelCacheUtility.GetCacheFolderPath();

            // Assert
            Assert.IsNotNull(cacheFolderPath);
            Assert.IsTrue(Directory.Exists(cacheFolderPath));
            Assert.AreEqual(testCacheFolderPath, cacheFolderPath);
        }

        [Test]
        public void SaveToCache_And_LoadFromCache_WorksCorrectly()
        {
            // Arrange
            string title = "Test Model";
            Texture2D thumbnail = new Texture2D(2, 2);
            thumbnail.SetPixels(new Color[] { Color.red, Color.red, Color.red, Color.red });
            thumbnail.Apply();

            // Act
            ModelCacheUtility.SaveToCache(testModelPath, title, thumbnail);

            // キャッシュが有効か確認
            bool isCacheValid = ModelCacheUtility.IsCacheValid(testModelPath);

            // キャッシュからデータを読み込む
            var (loadedTitle, loadedThumbnail) = ModelCacheUtility.LoadFromCache(testModelPath);

            // Assert
            Assert.IsTrue(isCacheValid);

            // タイトルが保存・読み込みされているか確認
            Assert.AreEqual(title, loadedTitle);

            // サムネイルが保存・読み込みされているか確認
            Assert.IsNotNull(loadedThumbnail);
            Assert.AreEqual(thumbnail.width, loadedThumbnail.width);
            Assert.AreEqual(thumbnail.height, loadedThumbnail.height);

            // ピクセルデータを比較
            Color[] originalPixels = thumbnail.GetPixels();
            Color[] loadedPixels = loadedThumbnail.GetPixels();
            Assert.AreEqual(originalPixels.Length, loadedPixels.Length);
            for (int i = 0; i < originalPixels.Length; i++)
            {
                Assert.AreEqual(originalPixels[i], loadedPixels[i]);
            }
        }

        [Test]
        public void IsCacheValid_WhenCacheDoesNotExist_ReturnsFalse()
        {
            // Arrange

            // Act
            bool isCacheValid = ModelCacheUtility.IsCacheValid(testModelPath);

            // Assert
            Assert.IsFalse(isCacheValid);
        }

        [Test]
        public void LoadFromCache_WhenCacheDoesNotExist_ThrowsException()
        {
            // Arrange

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() =>
            {
                var result = ModelCacheUtility.LoadFromCache(testModelPath);
            });
        }
    }
}
#endif
#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
using NUnit.Framework;
using System.IO;
using UnityEngine;

namespace uDesktopMascot.Editor.EditorTest
{
    public class ImageLoaderTests
    {
        private string testImagePath;

        [SetUp]
        public void SetUp()
        {
            // テスト用の一時画像ファイルパスを設定
            testImagePath = Path.Combine(Application.temporaryCachePath, "test_image.png");

            // テスト用の画像を生成（単色のテクスチャを保存）
            Texture2D texture = new Texture2D(2, 2);
            texture.SetPixels(new Color[] { Color.red, Color.red, Color.red, Color.red });
            texture.Apply();

            byte[] pngData = texture.EncodeToPNG();
            File.WriteAllBytes(testImagePath, pngData);
        }

        [TearDown]
        public void TearDown()
        {
            // テスト後に一時ファイルを削除
            if (File.Exists(testImagePath))
            {
                File.Delete(testImagePath);
            }
        }

        [Test]
        public void LoadSpriteSync_ValidImage_ReturnsSprite()
        {
            // Arrange

            // Act
            Sprite sprite = ImageLoader.LoadSpriteSync(testImagePath);

            // Assert
            Assert.IsNotNull(sprite);
            Assert.IsNotNull(sprite.texture);
            Assert.AreEqual(2, sprite.texture.width);
            Assert.AreEqual(2, sprite.texture.height);
        }

        [Test]
        public void LoadSpriteSync_FileDoesNotExist_ReturnsNull()
        {
            // Arrange
            string nonExistentPath = Path.Combine(Application.temporaryCachePath, "nonexistent_image.png");

            // Act
            Sprite sprite = ImageLoader.LoadSpriteSync(nonExistentPath);

            // Assert
            Assert.IsNull(sprite);
        }

        [Test]
        public void LoadSpriteSync_InvalidImage_ReturnsNull()
        {
            // Arrange
            string invalidImagePath = Path.Combine(Application.temporaryCachePath, "invalid_image.png");
            File.WriteAllText(invalidImagePath, "Not an image content");

            // Act
            Sprite sprite = ImageLoader.LoadSpriteSync(invalidImagePath);

            // Assert
            Assert.IsNull(sprite);

            // Clean up
            if (File.Exists(invalidImagePath))
            {
                File.Delete(invalidImagePath);
            }
        }
    }
}
#endif
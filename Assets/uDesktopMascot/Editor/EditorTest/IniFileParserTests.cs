#if UNITY_EDITOR || UNITY_INCLUDE_TESTS
using NUnit.Framework;
using System.IO;
using UnityEngine;

namespace uDesktopMascot.Editor.EditorTest
{
    public class IniFileParserTests
    {
        private string testFilePath;

        [SetUp]
        public void SetUp()
        {
            // テスト用の一時ファイルパスを設定
            testFilePath = Path.Combine(Application.temporaryCachePath, "test.ini");
        }

        [TearDown]
        public void TearDown()
        {
            // テスト後に一時ファイルを削除
            if (File.Exists(testFilePath))
            {
                File.Delete(testFilePath);
            }
        }

        [Test]
        public void Parse_NormalIniFile_ReturnsCorrectDictionary()
        {
            // Arrange
            var iniContent = @"
            [General]
            Key1=Value1
            Key2=Value2

            [Settings]
            OptionA=Enabled
            OptionB=Disabled
        ";

            File.WriteAllText(testFilePath, iniContent);

            // Act
            var result = IniFileParser.Parse(testFilePath);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ContainsKey("General"));
            Assert.IsTrue(result.ContainsKey("Settings"));
            Assert.AreEqual("Value1", result["General"]["Key1"]);
            Assert.AreEqual("Value2", result["General"]["Key2"]);
            Assert.AreEqual("Enabled", result["Settings"]["OptionA"]);
            Assert.AreEqual("Disabled", result["Settings"]["OptionB"]);
        }

        [Test]
        public void Parse_IniFileWithCommentsAndEmptyLines_IgnoresCommentsAndEmptyLines()
        {
            // Arrange
            var iniContent = @"
            ; This is a comment
            ; Another comment

            [Section]
            Key1=Value1  ; Inline comment
            Key2=Value2  ; Another inline comment

            ; Comment after section
        ";

            File.WriteAllText(testFilePath, iniContent);

            // Act
            var result = IniFileParser.Parse(testFilePath);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ContainsKey("Section"));
            Assert.AreEqual("Value1", result["Section"]["Key1"]);
            Assert.AreEqual("Value2", result["Section"]["Key2"]);
        }

        [Test]
        public void Parse_IniFileWithWhitespace_TrimsKeysAndValues()
        {
            // Arrange
            var iniContent = @"
            [ Section Name ]
            Key1   =   Value1
              Key2=Value2  
        ";

            File.WriteAllText(testFilePath, iniContent);

            // Act
            var result = IniFileParser.Parse(testFilePath);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ContainsKey("Section Name"));
            Assert.AreEqual("Value1", result["Section Name"]["Key1"]);
            Assert.AreEqual("Value2", result["Section Name"]["Key2"]);
        }

        [Test]
        public void Parse_MultipleSectionsWithDuplicateKeys_LastValueIsUsed()
        {
            // Arrange
            var iniContent = @"
            [Section]
            Key=Value1
            Key=Value2
            Key=Value3
        ";

            File.WriteAllText(testFilePath, iniContent);

            // Act
            var result = IniFileParser.Parse(testFilePath);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ContainsKey("Section"));
            Assert.AreEqual("Value3", result["Section"]["Key"]);
        }

        [Test]
        public void Parse_FileDoesNotExist_ReturnsEmptyDictionary()
        {
            // Arrange
            var nonExistentFilePath = Path.Combine(Application.temporaryCachePath, "nonexistent.ini");

            // Act
            var result = IniFileParser.Parse(nonExistentFilePath);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void Parse_EmptyFile_ReturnsEmptyDictionary()
        {
            // Arrange
            File.WriteAllText(testFilePath, string.Empty);

            // Act
            var result = IniFileParser.Parse(testFilePath);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void Parse_InvalidFormat_IgnoresInvalidLines()
        {
            // Arrange
            var iniContent = @"
            [Section]
            Key1=Value1
            InvalidLineWithoutEquals
            Key2=Value2
        ";

            File.WriteAllText(testFilePath, iniContent);

            // Act
            var result = IniFileParser.Parse(testFilePath);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ContainsKey("Section"));
            Assert.AreEqual(2, result["Section"].Count);
            Assert.AreEqual("Value1", result["Section"]["Key1"]);
            Assert.AreEqual("Value2", result["Section"]["Key2"]);
        }

        [Test]
        public void Parse_NoSection_IgnoresKeyValuesWithoutSection()
        {
            // Arrange
            var iniContent = @"
            Key1=Value1

            [Section]
            Key2=Value2
        ";

            File.WriteAllText(testFilePath, iniContent);

            // Act
            var result = IniFileParser.Parse(testFilePath);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.ContainsKey(string.Empty));
            Assert.IsTrue(result.ContainsKey("Section"));
            Assert.AreEqual("Value2", result["Section"]["Key2"]);
        }

        [Test]
        public void Parse_KeysAndValuesWithSpecialCharacters_ParsesCorrectly()
        {
            // Arrange
            var iniContent = @"
            [セクション]
            キー1=値1
            キー2=値=2;コメント
            キー3=値#3
        ";

            File.WriteAllText(testFilePath, iniContent);

            // Act
            var result = IniFileParser.Parse(testFilePath);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ContainsKey("セクション"));
            Assert.AreEqual("値1", result["セクション"]["キー1"]);
            Assert.AreEqual("値=2", result["セクション"]["キー2"]);
            Assert.AreEqual("値#3", result["セクション"]["キー3"]);
        }
    }
}
#endif
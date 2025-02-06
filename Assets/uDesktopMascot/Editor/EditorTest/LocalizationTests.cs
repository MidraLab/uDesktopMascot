#if UNITY_EDITOR || UNITY_INCLUDE_TESTS

using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using UnityEditor.Localization;
using System.IO;

namespace uDesktopMascot.Editor.EditorTest
{
    public class LocalizationTests
    {
        [Test]
        public void LocalizationTablesShouldNotHaveEmptyEntries()
        {
            // ローカリゼーションテーブルが格納されているフォルダのパスを指定
            string localizationFolderPath = "Assets/uDesktopMascot/LocalizationTable";
            string[] tableGuids = AssetDatabase.FindAssets("t:StringTable", new[] { localizationFolderPath });

            foreach (string tableGuid in tableGuids)
            {
                string path = AssetDatabase.GUIDToAssetPath(tableGuid);
                var table = AssetDatabase.LoadAssetAtPath<StringTable>(path);

                if (table == null)
                {
                    continue;
                }

                // ロケール識別子を取得
                var localeIdentifier = table.LocaleIdentifier.Code;

                // テーブル内の各エントリを処理
                foreach (var entry in table.Values)
                {
                    // エントリの値を取得
                    string entryValue = entry.Value;

                    if (string.IsNullOrWhiteSpace(entryValue))
                    {
                        // エントリが空白の場合、テストを失敗させる
                        Assert.Fail($"テーブル '{table.name}' のロケール '{localeIdentifier}' のキー '{entry.Key}' に空の文字列があります。");
                    }
                }
            }
        }
    }
}

#endif
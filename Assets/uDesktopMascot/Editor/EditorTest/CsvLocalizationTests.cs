#if UNITY_EDITOR || UNITY_INCLUDE_TESTS

using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

namespace uDesktopMascot.Editor.EditorTest
{
    public class CsvLocalizationTests
    {
        [Test]
        public void LocalizationCsvShouldNotHaveEmptyEntries()
        {
            // CSVファイルのパス
            string csvPath = "Assets/uDesktopMascot/LocalizationTable/LocalizationTable.csv";

            if (!File.Exists(csvPath))
            {
                Assert.Fail($"CSVファイルが見つかりませんでした: {csvPath}");
            }

            string[] lines = File.ReadAllLines(csvPath);

            if (lines.Length == 0)
            {
                Assert.Fail("CSVファイルが空です。");
            }

            // ヘッダー行（最初の行）を取得
            string headerLine = lines[0];
            string[] headers = SplitCsvLine(headerLine);

            // 言語列を特定（キー列を除く）
            List<string> languages = new List<string>();
            for (int i = 1; i < headers.Length; i++) // 最初の列はキーと仮定
            {
                languages.Add(headers[i]);
            }

            // 各行を処理
            for (int lineNumber = 1; lineNumber < lines.Length; lineNumber++)
            {
                string line = lines[lineNumber];
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue; // 空行はスキップ
                }

                string[] columns = SplitCsvLine(line);

                if (columns.Length != headers.Length)
                {
                    Assert.Fail($"行 {lineNumber + 1}: 列の数がヘッダーと一致しません。期待される列数: {headers.Length}, 実際の列数: {columns.Length}");
                }

                string key = columns[0];

                for (int i = 1; i < columns.Length; i++)
                {
                    string value = columns[i];
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        string language = headers[i];
                        Assert.Fail($"行 {lineNumber + 1}: キー '{key}' の言語 '{language}' の値が空です。");
                    }
                }
            }
        }

        // CSVの1行を分割するヘルパーメソッド
        private string[] SplitCsvLine(string line)
        {
            List<string> fields = new List<string>();
            bool inQuotes = false;
            string field = "";

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        // エスケープされたダブルクオート
                        field += '"';
                        i++; // 次のダブルクオートをスキップ
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == ',' && !inQuotes)
                {
                    fields.Add(field);
                    field = "";
                }
                else
                {
                    field += c;
                }
            }

            fields.Add(field);

            return fields.ToArray();
        }
    }
}

#endif
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Tables;
using UnityEditor.Localization;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Globalization;

namespace uDesktopMascot.Editor
{
    /// <summary>
    /// ローカリゼーションテーブルをセーブ時に自動でCSVにエクスポートする処理
    /// </summary>
    public class LocalizationTableSaveProcessor : AssetModificationProcessor
    {
        /// <summary>
        /// アセットが保存される直前に呼び出されるコールバック。
        /// 保存されるアセットがローカリゼーションテーブルである場合、CSVエクスポートを行います。
        /// </summary>
        /// <param name="paths">保存されるアセットのパスの配列</param>
        /// <returns>保存されるアセットのパスの配列</returns>
        private static string[] OnWillSaveAssets(string[] paths)
        {
            // 保存されるアセットパスをループ
            foreach (var path in paths)
            {
                // アセットがローカリゼーションテーブルかどうかを確認
                var asset = AssetDatabase.LoadAssetAtPath<Object>(path);
                if (asset is StringTable stringTable)
                {
                    // CSVエクスポートの処理を実行
                    ExportStringTableToCSV(stringTable);
                }
            }
            return paths;
        }

        /// <summary>
        /// 指定されたStringTableを含むテーブルコレクションをCSVにエクスポートします。
        /// </summary>
        /// <param name="stringTable">エクスポートするStringTable</param>
        private static void ExportStringTableToCSV(StringTable stringTable)
        {
            // テーブルコレクションを取得
            var tableCollection = LocalizationEditorSettings.GetCollectionFromTable(stringTable) as StringTableCollection;
            if (tableCollection == null)
            {
                Debug.LogError($"テーブルコレクションが見つかりませんでした：{stringTable.TableCollectionName}");
                return;
            }

            // エクスポート先のパスを設定
            var exportPath = "Assets/uDesktopMascot/LocalizationTable/LocalizationTable.csv";
            var exportDirectory = Path.GetDirectoryName(exportPath);
            if (!Directory.Exists(exportDirectory))
            {
                Directory.CreateDirectory(exportDirectory);
            }

            // CSVデータを生成
            var csvData = GenerateCSVData(tableCollection);

            // CSVファイルを書き出し（既に存在する場合は上書き）
            File.WriteAllText(exportPath, csvData, Encoding.UTF8);

            Debug.Log($"CSVエクスポートが完了しました：{exportPath}");
        }

        /// <summary>
        /// テーブルコレクションのデータからCSV形式の文字列を生成します。
        /// </summary>
        /// <param name="tableCollection">CSVデータを生成するStringTableCollection</param>
        /// <returns>生成されたCSV形式の文字列</returns>
        private static string GenerateCSVData(StringTableCollection tableCollection)
        {
            var sb = new StringBuilder();

            // ヘッダーを追加（ダブルクォーテーションなし）
            sb.Append("Key,Id");
            // 言語カラムの順序を元のCSVと同じにする
            var localeCodes = new List<string>();
            foreach (var localeCode in new[] { "en", "fr", "it", "ja", "ko" })
            {
                var table = tableCollection.GetTable(localeCode);
                if (table != null)
                {
                    localeCodes.Add(localeCode);
                    var languageName = GetLanguageName(localeCode);
                    sb.Append($",{languageName}({localeCode})");
                }
            }
            sb.AppendLine();

            // エントリを取得
            var sharedData = tableCollection.SharedData;
            foreach (var sharedEntry in sharedData.Entries)
            {
                // Keyフィールドをダブルクォーテーションで囲む
                var key = EscapeCSVField(sharedEntry.Key);

                // Idフィールドはそのまま（ダブルクォーテーションなし）
                var id = sharedEntry.Id.ToString();

                sb.Append($"{key},{id}");

                foreach (var localeCode in localeCodes)
                {
                    var table = tableCollection.GetTable(localeCode) as StringTable;
                    if (table != null)
                    {
                        var entry = table.GetEntry(sharedEntry.Id);
                        if (entry != null)
                        {
                            // ローカライズされた値をダブルクォーテーションで囲む
                            var localizedValue = EscapeCSVField(entry.LocalizedValue);
                            sb.Append($",{localizedValue}");
                        }
                        else
                        {
                            // 空の値でもダブルクォーテーションを付ける
                            sb.Append($",\"\"");
                        }
                    }
                    else
                    {
                        // テーブルが存在しない場合もダブルクォーテーション付きの空文字を追加
                        sb.Append($",\"\"");
                    }
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        /// <summary>
        /// CSVフィールドをダブルクォーテーションで囲み、内部のダブルクォーテーションをエスケープします。
        /// </summary>
        /// <param name="field">エスケープするフィールドの文字列</param>
        /// <returns>ダブルクォーテーションで囲まれた、エスケープ処理された文字列</returns>
        private static string EscapeCSVField(string field)
        {
            if (string.IsNullOrEmpty(field))
            {
                return "\"\"";
            }

            // ダブルクォーテーションをエスケープ
            if (field.Contains("\""))
            {
                field = field.Replace("\"", "\"\"");
            }

            // フィールドをダブルクォーテーションで囲む
            return $"\"{field}\"";
        }

        /// <summary>
        /// ロケールコードから言語名を取得します。
        /// </summary>
        /// <param name="localeCode">ロケールコード（例："en"）</param>
        /// <returns>言語名（例："English"）</returns>
        private static string GetLanguageName(string localeCode)
        {
            try
            {
                var culture = new CultureInfo(localeCode);
                return culture.EnglishName;
            }
            catch
            {
                // ロケールコードが認識されない場合はそのまま返す
                return localeCode;
            }
        }
    }
}
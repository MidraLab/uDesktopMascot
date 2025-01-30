using System.Collections.Generic;
using System.IO;
using Unity.Logging;

namespace uDesktopMascot
{
    /// <summary>
    ///     INIファイルの読み込みとパースを行うユーティリティクラス
    /// </summary>
    public static class IniFileParser
    {
        /// <summary>
        ///     INIファイルをパースして、セクションごとのキーと値のディクショナリを返す
        /// </summary>
        /// <param name="filePath">INIファイルのパス</param>
        /// <returns>パース結果のディクショナリ</returns>
        public static Dictionary<string, Dictionary<string, string>> Parse(string filePath)
        {
            var settings = new Dictionary<string, Dictionary<string, string>>();

            if (!File.Exists(filePath))
            {
                Log.Warning("INIファイルが見つかりませんでした: " + filePath);
                return settings; // 空のディクショナリを返す
            }

            var lines = File.ReadAllLines(filePath);
            string currentSection = null;

            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();

                // 空行やコメント行を無視
                if (string.IsNullOrEmpty(trimmedLine) || trimmedLine.StartsWith("#") || trimmedLine.StartsWith(";"))
                {
                    continue;
                }

                // セクションの検出
                if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
                {
                    currentSection = trimmedLine.Substring(1, trimmedLine.Length - 2).Trim();
                    if (!settings.ContainsKey(currentSection))
                    {
                        settings[currentSection] = new Dictionary<string, string>();
                    }
                }
                else if (currentSection != null)
                {
                    // キーと値の分割
                    var separatorIndex = trimmedLine.IndexOf('=');
                    if (separatorIndex > 0)
                    {
                        var key = trimmedLine.Substring(0, separatorIndex).Trim();
                        var value = trimmedLine.Substring(separatorIndex + 1).Trim();

                        // 行内コメントを除去
                        var commentIndex = value.IndexOfAny(new char[] { ';'});
                        if (commentIndex >= 0)
                        {
                            value = value.Substring(0, commentIndex).Trim();
                        }

                        settings[currentSection][key] = value;
                    }
                }
            }

            return settings;
        }
    }
}
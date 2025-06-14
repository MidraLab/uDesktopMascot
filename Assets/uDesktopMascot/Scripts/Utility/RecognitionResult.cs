using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace uDesktopMascot
{
    public class RecognitionResult
    {
        public struct Alternative
        {
            public string Text;
            public float Confidence;
        }

        public bool Partial { get; }

        /// <summary>Partial の場合は中間テキスト、Final の場合は信頼度が最も高いテキスト</summary>
        public string BestText { get; }

        public Alternative[] Alternatives { get; }

        public RecognitionResult(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                Partial = true;
                BestText = string.Empty;
                Alternatives = System.Array.Empty<Alternative>();
                return;
            }

            var obj = JObject.Parse(json);

            // Partial か Final か判定
            if (obj.TryGetValue("partial", out var partialToken))
            {
                Partial = true;
                BestText = partialToken.ToString();
                Alternatives = new[] { new Alternative { Text = BestText, Confidence = 0f } };
                return;
            }

            Partial = false;

            // alternatives があれば信頼度の高いものを採用
            if (obj.TryGetValue("alternatives", out var altToken) && altToken is JArray arr && arr.Count > 0)
            {
                var list = new List<Alternative>(arr.Count);
                foreach (var a in arr)
                {
                    string text = a! ["text"].ToString();
                    float conf = a!["confidence"] != null ? a["confidence"].Value<float>() : 0f;
                    list.Add(new Alternative { Text = text, Confidence = conf });
                }
                Alternatives = list.ToArray();

                // 信頼度が最大のテキスト
                Alternative best = Alternatives[0];
                foreach (var alt in Alternatives)
                {
                    if (alt.Confidence > best.Confidence) best = alt;
                }
                BestText = best.Text;
                return;
            }

            // alternatives が無い場合は text フィールドを使用
            if (obj.TryGetValue("text", out var textToken))
            {
                BestText = textToken.ToString();
                Alternatives = new[] { new Alternative { Text = BestText, Confidence = 0f } };
            }
            else
            {
                BestText = string.Empty;
                Alternatives = System.Array.Empty<Alternative>();
            }
        }
    }
} 
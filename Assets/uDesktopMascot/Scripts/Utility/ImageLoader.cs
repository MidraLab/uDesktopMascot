using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Logging;
using UnityEngine;
using UnityEngine.Networking;

namespace uDesktopMascot
{
    /// <summary>
    /// 画像ローダー
    /// </summary>
    public static class ImageLoader
    {
        /// <summary>
        /// 指定したパスから画像をロードし、スプライトを作成して返す（同期処理）
        /// </summary>
        /// <param name="fullPath">画像ファイルのフルパス</param>
        /// <returns>ロードされたスプライト（失敗した場合はnull）</returns>
        public static Sprite LoadSpriteSync(string fullPath)
        {
            // ファイルが存在しない場合はエラーログを出力して終了
            if (!File.Exists(fullPath))
            {
                Log.Error("画像ファイルが見つかりません。パスを確認してください: " + fullPath);
                return null;
            }

            try
            {
                byte[] fileData = File.ReadAllBytes(fullPath);
                Texture2D texture = new Texture2D(2, 2);
                if (texture.LoadImage(fileData))
                {
                    Rect rect = new Rect(0, 0, texture.width, texture.height);
                    Vector2 pivot = new Vector2(0.5f, 0.5f);
                    Sprite sprite = Sprite.Create(texture, rect, pivot);
                    return sprite;
                }
                else
                {
                    Log.Error("画像のテクスチャがロードできませんでした。パスを確認してください: " + fullPath);
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                Log.Error("画像のロード中にエラーが発生しました: " + ex.Message);
                return null;
            }
        }
        
        /// <summary>
        /// 指定したパスから画像をロードし、スプライトを作成して返す
        /// </summary>
        /// <param name="fullPath">画像ファイルのフルパス</param>
        /// <param name="cancellationToken">キャンセレーショントークン</param>
        /// <returns>ロードされたスプライト（失敗した場合はnull）</returns>
        public static async UniTask<Sprite> LoadSpriteAsync(string fullPath, CancellationToken cancellationToken)
        {
            // ファイルが存在しない場合はエラーログを出力して終了
            if (!File.Exists(fullPath))
            {
                Log.Warning("画像ファイルが見つかりません。パスを確認してください: " + fullPath);
                return null;
            }

            UnityWebRequest uwr = UnityWebRequestTexture.GetTexture("file://" + fullPath);

            await uwr.SendWebRequest().WithCancellation(cancellationToken);

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Log.Error("画像ファイルのロードに失敗しました。パスを確認してください: " + fullPath + " エラー: " + uwr.error);
                return null;
            } else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                if (texture != null)
                {
                    Rect rect = new Rect(0, 0, texture.width, texture.height);
                    Vector2 pivot = new Vector2(0.5f, 0.5f);
                    Sprite sprite = Sprite.Create(texture, rect, pivot);
                    return sprite;
                } else
                {
                    Log.Error("画像のテクスチャがロードできませんでした。パスを確認してください: " + fullPath);
                    return null;
                }
            }
        }
    }
}
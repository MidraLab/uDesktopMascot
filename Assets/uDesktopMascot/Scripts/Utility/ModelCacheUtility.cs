using System;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

namespace uDesktopMascot
{
    /// <summary>
    /// モデルのメタデータ（名前とサムネイル）をキャッシュするユーティリティクラス
    /// </summary>
    public static class ModelCacheUtility
    {
        /// <summary>
        /// モデルファイルのハッシュ値を計算
        /// </summary>
        /// <param name="filePath">モデルファイルのパス</param>
        /// <returns>ハッシュ値の文字列</returns>
        public static string ComputeHash(string filePath)
        {
            using var stream = File.OpenRead(filePath);
            var sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(stream);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }

        /// <summary>
        /// キャッシュフォルダのパスを取得（存在しない場合は作成）
        /// </summary>
        /// <returns>キャッシュフォルダのパス</returns>
        public static string GetCacheFolderPath()
        {
            var cacheFolderPath = Path.Combine(Application.persistentDataPath, "ModelCache");
            if (!Directory.Exists(cacheFolderPath))
            {
                Directory.CreateDirectory(cacheFolderPath);
            }
            return cacheFolderPath;
        }

        /// <summary>
        /// モデルファイルに対応するキャッシュファイルのパスを取得
        /// </summary>
        /// <param name="filePath">モデルファイルのパス</param>
        /// <returns>キャッシュファイルのパス</returns>
        public static string GetCacheFilePath(string filePath)
        {
            string hash = ComputeHash(filePath);
            string cacheFolderPath = GetCacheFolderPath();
            return Path.Combine(cacheFolderPath, $"{hash}.json"); // メタデータ（名前）を保存するJSONファイル
        }

        /// <summary>
        /// モデルファイルに対応するサムネイル画像のキャッシュパスを取得
        /// </summary>
        /// <param name="filePath">モデルファイルのパス</param>
        /// <returns>サムネイル画像のキャッシュパス</returns>
        public static string GetThumbnailFilePath(string filePath)
        {
            string hash = ComputeHash(filePath);
            string cacheFolderPath = GetCacheFolderPath();
            return Path.Combine(cacheFolderPath, $"{hash}.png"); // サムネイル画像を保存するPNGファイル
        }

        /// <summary>
        /// キャッシュが有効か確認
        /// </summary>
        /// <param name="filePath">モデルファイルのパス</param>
        /// <returns>キャッシュが有効ならtrue</returns>
        public static bool IsCacheValid(string filePath)
        {
            string cacheFilePath = GetCacheFilePath(filePath);
            string thumbnailFilePath = GetThumbnailFilePath(filePath);
            return File.Exists(cacheFilePath) && File.Exists(thumbnailFilePath);
        }

        /// <summary>
        /// キャッシュからモデル名とサムネイル画像を読み込む
        /// </summary>
        /// <param name="filePath">モデルファイルのパス</param>
        /// <returns>モデル名とサムネイル画像のタプル</returns>
        public static (string title, Texture2D thumbnail) LoadFromCache(string filePath)
        {
            string cacheFilePath = GetCacheFilePath(filePath);
            string thumbnailFilePath = GetThumbnailFilePath(filePath);

            // モデル名を読み込む
            string json = File.ReadAllText(cacheFilePath);
            var metaData = JsonUtility.FromJson<MetaData>(json);
            string title = metaData.Title;

            // サムネイル画像を読み込む
            byte[] imageBytes = File.ReadAllBytes(thumbnailFilePath);
            Texture2D thumbnail = new Texture2D(2, 2);
            thumbnail.LoadImage(imageBytes);
            return (title, thumbnail);
        }

        /// <summary>
        /// キャッシュにモデル名とサムネイル画像を保存
        /// </summary>
        /// <param name="filePath">モデルファイルのパス</param>
        /// <param name="title">モデル名</param>
        /// <param name="thumbnail">サムネイル画像</param>
        public static void SaveToCache(string filePath, string title, Texture2D thumbnail)
        {
            string cacheFilePath = GetCacheFilePath(filePath);
            string thumbnailFilePath = GetThumbnailFilePath(filePath);

            // モデル名を保存
            var metaData = new MetaData { Title = title };
            string json = JsonUtility.ToJson(metaData);
            File.WriteAllText(cacheFilePath, json);

            // サムネイル画像をPNG形式で保存
            byte[] pngData = thumbnail.EncodeToPNG();
            File.WriteAllBytes(thumbnailFilePath, pngData);
        }

        /// <summary>
        /// モデルのメタデータを表すクラス
        /// </summary>
        [Serializable]
        public class MetaData
        {
            public string Title;
        }
    }
}
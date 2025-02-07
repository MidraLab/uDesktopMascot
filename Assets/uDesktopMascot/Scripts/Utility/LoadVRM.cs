using System;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Unity.Logging;
using UniGLTF;
using UniVRM10;
using Object = UnityEngine.Object;

namespace uDesktopMascot
{
    /// <summary>
    /// VRMファイルを読み込む
    /// </summary>
    public static class LoadVRM
    {
        /// <summary>
        /// アニメーションコントローラーを設定
        /// </summary>
        /// <param name="animator"></param>
        public static void UpdateAnimationController(Animator animator)
        {
            if (animator == null)
            {
                Log.Error("Animator が null です。アニメーションコントローラーを設定できません。");
                return;
            }

            var controller = Resources.Load<RuntimeAnimatorController>("CharacterAnimationController");
            if (controller != null)
            {
                animator.runtimeAnimatorController = controller;
                Log.Info("アニメーションコントローラーを設定しました。");

                if (animator.avatar == null)
                {
                    Log.Warning("Animator の avatar が設定されていません。アニメーションが正しく再生されない可能性があります。");
                }
            } else
            {
                Log.Error("CharacterAnimationController が Resources に見つかりませんでした。アニメーションコントローラーが正しく設定されているか確認してください。");
            }
        }

        /// <summary>
        /// モデルをロードする
        /// </summary>
        public static async UniTask<LoadedVRMInfo> LoadModelAsync(string modelPath, CancellationToken cancellationToken)
        {
            try
            {
                if (!string.IsNullOrEmpty(modelPath))
                {
                    Log.Info($"指定されたモデルパス: {modelPath}");

                    // StreamingAssets フォルダ内のフルパスを作成
                    var fullModelPath = Path.Combine(Application.streamingAssetsPath, modelPath);

                    // モデルファイルが存在するか確認
                    if (File.Exists(fullModelPath))
                    {
                        Log.Info($"指定されたモデルファイルをロードします: {modelPath}");
                        // 指定されたモデルをロード
                        return await LoadAndDisplayModel(fullModelPath, cancellationToken);
                    } else
                    {
                        Log.Warning($"指定されたモデルファイルが見つかりませんでした: {modelPath}");
                        // この後、他のモデルファイルを探します
                    }
                } else
                {
                    Log.Info("モデルパスが指定されていません。");
                    return null;
                }
            } catch (Exception e)
            {
                Log.Error($"モデルの読み込みまたは表示中にエラーが発生しました: {e.Message}");
                return null;
            }

            return null;
        }

        /// <summary>
        ///     デフォルトのVRMモデルをロードして表示する
        /// </summary>
        public static async UniTask<Vrm10Instance> LoadDefaultModel()
        {
            // ResourcesフォルダからPrefabをロード
            var request = Resources.LoadAsync<GameObject>(Constant.DefaultVrmFileName);
            await request;

            if (request.asset == null)
            {
                Log.Error($"デフォルトのPrefabがResourcesフォルダに見つかりません: {Constant.DefaultVrmFileName}.prefab");
                return null;
            }

            // Prefabをインスタンス化
            var prefab = request.asset as GameObject;
            var modelGameObject = GameObject.Instantiate(prefab);

            // Vrm10Instance コンポーネントを取得
            var model = modelGameObject.GetComponent<Vrm10Instance>();
            if (model == null)
            {
                Log.Warning(
                    $"インスタンス化したGameObjectにVrm10Instanceコンポーネントがアタッチされていません: {Constant.DefaultVrmFileName}.prefab");
            }

            Log.Debug("デフォルトモデルのロードと表示が完了しました: " + Constant.DefaultVrmFileName);

            return model;
        }

        /// <summary>
        /// VRMファイルを読み込み、モデルを表示する
        /// </summary>
        /// <param name="path">モデルファイルのパス</param>
        /// <param name="cancellationToken"></param>
        private static async UniTask<LoadedVRMInfo> LoadAndDisplayModel(string path,
            CancellationToken cancellationToken)
        {
            return await LoadAndDisplayModelFromPath(path, cancellationToken);
        }

        /// <summary>
        /// ファイルパスからモデルをロードして表示する
        /// </summary>
        /// <param name="path"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static async UniTask<LoadedVRMInfo> LoadAndDisplayModelFromPath(string path,
            CancellationToken cancellationToken)
        {
            // ファイルの拡張子を取得
            var extension = Path.GetExtension(path).ToLowerInvariant();

            GameObject model = null;
            string title = string.Empty;
            Texture2D thumbnailTexture = null;

            if (extension == ".vrm")
            {
                // VRMファイルをロード（VRM 0.x および 1.x に対応）
                Vrm10Instance instance = await Vrm10.LoadPathAsync(path, canLoadVrm0X: true, ct: cancellationToken);
                title = instance.Vrm.Meta.Name;
                thumbnailTexture = instance.Vrm.Meta.Thumbnail;

                // モデルのGameObjectを取得
                model = instance.gameObject;
            } else if (extension == ".glb" || extension == ".gltf")
            {
                // GLBまたはglTFファイルをロード
                model = await LoadGlbOrGltfModelAsync(path);
            } else
            {
                Log.Error($"サポートされていないファイル形式です: {extension}");
                return null;
            }

            if (model == null)
            {
                Log.Error("モデルのロードに失敗しました。");
                return null;
            }

            Log.Info("モデルのロードと表示が完了しました: " + path);

            return new LoadedVRMInfo(model, title, thumbnailTexture);
        }
        
        /// <summary>
        /// VRMファイルのメタ情報を取得
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async UniTask<(string title, Texture2D thumbnailTexture)> LoadVrmMetaAsync(string path)
        {
            // VRMファイルをバイト配列として読み込む
            var bytes = await File.ReadAllBytesAsync(path);

            // GLBデータとしてパース
            var parser = new GlbLowLevelParser(path, bytes);
            using (var gltfData = parser.Parse())
            {
                // VRM 1.0としてパースを試みる
                var vrm10Data = Vrm10Data.Parse(gltfData);
                if (vrm10Data != null)
                {
                    // VRM 1.0のメタ情報を取得
                    var meta = vrm10Data.VrmExtension.Meta;
                    string title = meta.Name;

                    // サムネイル画像を取得
                    Texture2D thumbnailTexture = null;
                    if (meta.ThumbnailImage.HasValue)
                    {
                        var imageIndex = meta.ThumbnailImage.Value;
                        thumbnailTexture = LoadTextureFromImageIndex(vrm10Data.Data, imageIndex);
                    }

                    return (title, thumbnailTexture);
                } else
                {
                    // VRM 0.xの場合、マイグレーションを行う
                    using var migratedGltfData = Vrm10Data.Migrate(gltfData, out var migratedVrm10Data, out var migrationData);
                    if (migratedVrm10Data != null)
                    {
                        // VRM 0.xのメタ情報を取得
                        var meta = migrationData?.OriginalMetaBeforeMigration;
                        string title = meta?.title;

                        // サムネイル画像を取得
                        Texture2D thumbnailTexture = null;
                        if (meta?.texture != null && meta.texture != -1)
                        {
                            var imageIndex = meta.texture;
                            thumbnailTexture = LoadTextureFromImageIndex(gltfData, imageIndex);
                        }

                        return (title, thumbnailTexture);
                    }
                }
            }

            // メタ情報が取得できなかった場合
            return (null, null);
        }

        /// <summary>
        /// 画像インデックスからテクスチャをロード
        /// </summary>
        /// <param name="gltfData"></param>
        /// <param name="imageIndex"></param>
        /// <returns></returns>
        private static Texture2D LoadTextureFromImageIndex(GltfData gltfData, int imageIndex)
        {
            var gltfImage = gltfData.GLTF.images[imageIndex];

            byte[] imageBytes = null;

            if (!string.IsNullOrEmpty(gltfImage.uri))
            {
                if (gltfImage.uri.StartsWith("data:", StringComparison.Ordinal))
                {
                    // Data URIから画像データを取得
                    imageBytes = UriByteBuffer.ReadEmbedded(gltfImage.uri);
                } else
                {
                    // 外部ファイルの場合は対応しない
                    Log.Warning("External image files are not supported.");
                    return null;
                }
            } else if (gltfImage.bufferView >= 0)
            {
                // バッファビューから画像データを取得
                var segment = gltfData.GetBytesFromBufferView(gltfImage.bufferView);
                imageBytes = segment.ToArray();
            } else
            {
                Log.Warning("No image data found for the texture.");
                return null;
            }

            if (imageBytes != null)
            {
                // Texture2Dを作成
                var texture = new Texture2D(2, 2);
                if (texture.LoadImage(imageBytes))
                {
                    return texture;
                } else
                {
                    Log.Warning("Failed to load image into Texture2D.");
                }
            }

            return null;
        }

        /// <summary>
        /// GLBまたはglTFファイルをロードしてモデルを取得
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static async UniTask<GameObject> LoadGlbOrGltfModelAsync(string path)
        {
            try
            {
                // ファイルを自動的にパースする
                var parser = new AutoGltfFileParser(path);

                using var gltfData = parser.Parse();
                // ImporterContextを作成
                var importer = new ImporterContext(gltfData);

                // IAwaitCallerを作成
                var awaitCaller = new RuntimeOnlyAwaitCaller();

                // モデルを非同期でロード
                var gltfInstance = await importer.LoadAsync(awaitCaller);

                // 必要に応じてメッシュを表示
                gltfInstance.ShowMeshes();

                // ルートのGameObjectを取得
                var model = gltfInstance.Root;

                return model;
            } catch (OperationCanceledException)
            {
                Log.Warning("モデルのロードがキャンセルされました。");
                return null;
            } catch (Exception e)
            {
                Log.Error($"モデルのロード中にエラーが発生しました: {e.Message}");
                return null;
            }
        }
    }

    /// <summary>
    /// ロードされたVRMの情報を保持するクラス
    /// </summary>
    public class LoadedVRMInfo
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="model"></param>
        /// <param name="modelName"></param>
        /// <param name="thumbnailTexture"></param>
        public LoadedVRMInfo(GameObject model, string modelName, Texture2D thumbnailTexture)
        {
            Model = model;
            ModelName = modelName;
            ThumbnailTexture = thumbnailTexture;
        }

        /// <summary>
        /// ロードされたモデルのGameObject
        /// </summary>
        public GameObject Model { get; private set; }

        /// <summary>
        /// モデルのタイトル
        /// </summary>
        public string ModelName { get; private set; }

        /// <summary>
        /// サムネイル画像
        /// </summary>
        public Texture2D ThumbnailTexture { get; private set; }
    }
}
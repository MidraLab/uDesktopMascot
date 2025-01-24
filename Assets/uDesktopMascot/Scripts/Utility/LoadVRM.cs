using System;
using System.IO;
using System.Linq;
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
        /// デフォルトのVRMファイル名
        /// </summary>
        private const string DefaultVrmFileName = "DefaultModel/DefaultModel";

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
        public static async UniTask<GameObject> LoadModelAsync(string modelPath,CancellationToken cancellationToken)
        {
            try
            {
                GameObject model = null;

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
                        model = await LoadAndDisplayModel(fullModelPath, cancellationToken);
                    } else
                    {
                        Log.Warning($"指定されたモデルファイルが見つかりませんでした: {modelPath}");
                        // この後、他のモデルファイルを探します
                    }
                } else
                {
                    Log.Info("モデルパスが指定されていません。");
                }

                return model;
            } catch (Exception e)
            {
                Log.Error($"モデルの読み込みまたは表示中にエラーが発生しました: {e.Message}");
                return null;
            }
        }

        /// <summary>
        ///     デフォルトのVRMモデルをロードして表示する
        /// </summary>
        public static GameObject LoadDefaultModel()
        {
            // ResourcesフォルダからPrefabをロード
            var prefab = Resources.Load<GameObject>(DefaultVrmFileName);
            if (prefab == null)
            {
                Log.Error($"デフォルトのPrefabがResourcesフォルダに見つかりません: {DefaultVrmFileName}.prefab");
                return null;
            }

            // Prefabをインスタンス化
            var model = Object.Instantiate(prefab);

            Log.Debug("デフォルトモデルのロードと表示が完了しました: " + DefaultVrmFileName);

            return model;
        }

        /// <summary>
        /// VRMファイルを読み込み、モデルを表示する
        /// </summary>
        /// <param name="path">モデルファイルのパス</param>
        /// <param name="cancellationToken"></param>
        private static async UniTask<GameObject> LoadAndDisplayModel(string path, CancellationToken cancellationToken)
        {
            return await LoadAndDisplayModelFromPath(path, cancellationToken);
        }

        /// <summary>
        /// ファイルパスからモデルをロードして表示する
        /// </summary>
        /// <param name="path"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static async UniTask<GameObject> LoadAndDisplayModelFromPath(string path,
            CancellationToken cancellationToken)
        {
            // ファイルの拡張子を取得
            var extension = Path.GetExtension(path).ToLowerInvariant();

            GameObject model = null;

            if (extension == ".vrm")
            {
                // VRMファイルをロード（VRM 0.x および 1.x に対応）
                Vrm10Instance instance = await Vrm10.LoadPathAsync(path, canLoadVrm0X: true, ct: cancellationToken);

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

            return model;
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
}
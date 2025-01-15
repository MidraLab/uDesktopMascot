using System;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniGLTF;
using Unity.Logging;
using UnityEngine;
using VRM;
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
        private const string DefaultVrmFileName = "DefaultModel/test";

        /// <summary>
        /// アニメーションコントローラーを設定
        /// </summary>
        /// <param name="animator"></param>
        public static void UpdateAnimationController(Animator animator)
        {
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CharacterAnimationController");
        }

        /// <summary>
        /// モデルをロードする
        /// </summary>
        public static async UniTask<GameObject> LoadModel(CancellationToken cancellationToken)
        {
            try
            {
                // StreamingAssetsフォルダが存在するか確認
                if (Directory.Exists(Application.streamingAssetsPath))
                {
                    // StreamingAssetsフォルダ内のVRMファイルを検索
                    var vrmFiles = Directory.GetFiles(Application.streamingAssetsPath, "*.vrm");
                    var userVrmFiles = vrmFiles;

                    if (userVrmFiles.Length > 0)
                    {
                        // ユーザー指定のVRMファイルを使用（最初のもの）
                        var path = userVrmFiles[0];
                        Log.Info($"ユーザー指定のVRMファイルを使用します: {path}");

                        try
                        {
                            // VRMファイルをロードしてモデルを表示
                            return await LoadAndDisplayModel(path, cancellationToken);
                        } catch (Exception e)
                        {
                            Log.Error($"VRMの読み込みまたは表示中にエラーが発生しました: {e.Message}");
                            // エラーが発生した場合、デフォルトのモデルを表示
                            Log.Info("デフォルトのモデルを読み込みます。");
                            return LoadDefaultModel();
                        }
                    }

                    // ユーザー指定のVRMファイルが見つからない場合、デフォルトのモデルを使用
                    Log.Info("ユーザー指定のVRMファイルが見つかりません。デフォルトのモデルを読み込みます。");
                    return LoadDefaultModel();
                }

                // StreamingAssetsフォルダが存在しない場合、デフォルトのモデルをロード
                Log.Info("StreamingAssetsフォルダが見つかりません。デフォルトのモデルを読み込みます。");
                return LoadDefaultModel();
            } catch (Exception e)
            {
                Log.Error($"VRMの読み込みまたは表示中にエラーが発生しました: {e.Message}");
                return null;
            }
        }

        /// <summary>
        ///     デフォルトのVRMモデルをロードして表示する
        /// </summary>
        private static GameObject LoadDefaultModel()
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

            // モデルの位置を設定（原点に配置）
            model.transform.position = Vector3.zero;

            // モデルの大きさを調整
            model.transform.localScale = Vector3.one * 3f;

            // Y軸に180度回転（必要に応じて）
            model.transform.Rotate(0, 180, 0);

            // モデルをアクティブ化
            model.SetActive(true);

            // すべてのRendererを有効化
            EnableAllRenderers(model);

            Log.Debug("デフォルトモデルのロードと表示が完了しました: " + DefaultVrmFileName);

            return model;
        }

        /// <summary>
        /// VRMファイルを読み込み、モデルを表示する
        /// </summary>
        /// <param name="path">VRMファイルのパス</param>
        /// <param name="cancellationToken"></param>
        private static async UniTask<GameObject> LoadAndDisplayModel(string path, CancellationToken cancellationToken)
        {
            // VRMファイルをバイト配列として非同期で読み込み
            var bytes = await File.ReadAllBytesAsync(path, cancellationToken);

            return await LoadAndDisplayModelFromBytes(bytes, path);
        }

        /// <summary>
        ///     バイト配列からVRMモデルをロードして表示する
        /// </summary>
        /// <param name="bytes">VRMファイルのバイト配列</param>
        /// <param name="fileName">ファイル名（ログ用）</param>
        private static async UniTask<GameObject> LoadAndDisplayModelFromBytes(byte[] bytes, string fileName)
        {
            // VRMファイルをパースしてGltfDataを取得
            var parsed = new GlbLowLevelParser(fileName, bytes).Parse();

            // VRMDataを作成
            var vrmData = new VRMData(parsed);

            // VRMImporterContextを作成
            using var vrmImporter = new VRMImporterContext(vrmData);

            // モデルを非同期で読み込み
            var instance = await vrmImporter.LoadAsync(new RuntimeOnlyAwaitCaller());

            // モデルのGameObjectを取得
            GameObject model = instance.Root;

            // モデルの位置を設定（原点に配置）
            model.transform.position = Vector3.zero;

            // Y軸に180度回転
            model.transform.Rotate(0, 180, 0);

            // モデルをアクティブ化
            model.SetActive(true);

            // すべてのRendererを有効化
            EnableAllRenderers(model);

            Log.Info("VRMのロードと表示が完了しました: " + fileName);

            return model;
        }


        /// <summary>
        ///     モデルにColliderを追加する
        /// </summary>
        /// <param name="model"></param>
        public static void AddCollidersToModel(GameObject model)
        {
            // モデルの全てのSkinnedMeshRendererにMeshColliderを追加
            var skinnedMeshes = model.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var skinnedMesh in skinnedMeshes)
            {
                var collider = skinnedMesh.gameObject.AddComponent<MeshCollider>();
                collider.sharedMesh = skinnedMesh.sharedMesh;
                collider.convex = true;
            }
        }

        /// <summary>
        /// モデル内のすべてのRendererコンポーネントを有効化する
        /// </summary>
        /// <param name="model">モデルのGameObject</param>
        private static void EnableAllRenderers(GameObject model)
        {
            // SkinnedMeshRendererを有効化
            var skinnedMeshRenderers = model.GetComponentsInChildren<SkinnedMeshRenderer>(true);
            foreach (var renderer in skinnedMeshRenderers)
            {
                renderer.enabled = true;
            }

            // MeshRendererを有効化
            var meshRenderers = model.GetComponentsInChildren<MeshRenderer>(true);
            foreach (var renderer in meshRenderers)
            {
                renderer.enabled = true;
            }
        }
    }
}
﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UniGLTF;
using VRM;

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
        private const string DefaultVrmFileName = "test.vrm";
        
        /// <summary>
        /// アニメーションコントローラーを設定
        /// </summary>
        /// <param name="animator"></param>
        public static void UpdateAnimationController(Animator animator)
        {
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CharacterAnimationController");
        }

        /// <summary>
        /// LoadModel
        /// </summary>
        public static async UniTask<GameObject> LoadModel(CancellationToken cancellationToken)
        {
            try
            {
                string path = null;

                // デフォルトのVRMファイルのフルパス
                string defaultVrmPath = Path.Combine(Application.streamingAssetsPath, DefaultVrmFileName);

                // StreamingAssetsフォルダ内のVRMファイルを検索（デフォルトのVRMを除外）
                string[] vrmFiles = Directory.GetFiles(Application.streamingAssetsPath, "*.vrm");
                var userVrmFiles = Array.FindAll(vrmFiles, file => !string.Equals(Path.GetFileName(file), DefaultVrmFileName, StringComparison.OrdinalIgnoreCase));

                if (userVrmFiles.Length > 0)
                {
                    // ユーザー指定のVRMファイルを使用（最初のもの）
                    path = userVrmFiles[0];
                    Debug.Log($"ユーザー指定のVRMファイルを使用します: {path}");
                }
                else
                {
                    // ユーザー指定のVRMファイルが見つからない場合、デフォルトのVRMファイルを使用
                    path = defaultVrmPath;
                    Debug.LogWarning("ユーザー指定のVRMファイルが見つかりません。デフォルトのモデルを読み込みます。");
                }

                // ファイルの存在確認
                if (!File.Exists(path))
                {
                    Debug.LogError($"VRMファイルが見つかりません: {path}");
                    return null;
                }

                try
                {
                    // VRMファイルをロードしてモデルを表示
                    return await LoadAndDisplayModel(path, cancellationToken);
                }
                catch (Exception e)
                {
                    Debug.LogError($"VRMの読み込みまたは表示中にエラーが発生しました: {e.Message}");

                    // エラーが発生した場合、デフォルトのモデルを表示
                    if (!string.Equals(path, defaultVrmPath, StringComparison.OrdinalIgnoreCase))
                    {
                        Debug.LogWarning("デフォルトのモデルを読み込みます。");
                        try
                        {
                            await LoadAndDisplayModel(defaultVrmPath, cancellationToken);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"デフォルトモデルの読み込みまたは表示中にエラーが発生しました: {ex.Message}");
                        }
                    }
                }
            } catch (Exception e)
            {
                Debug.LogError($"VRMの読み込みまたは表示中にエラーが発生しました: {e.Message}");
                return null;
            }
            
            return null;
        }

        /// <summary>
        /// VRMファイルを読み込み、モデルを表示する
        /// </summary>
        /// <param name="path">VRMファイルのパス</param>
        /// <param name="cancellationToken"></param>
        private static async UniTask<GameObject> LoadAndDisplayModel(string path,CancellationToken cancellationToken)
        {
            // VRMファイルをバイト配列として非同期で読み込み
            var bytes = await File.ReadAllBytesAsync(path, cancellationToken);

            // VRMファイルをパースしてGltfDataを取得
            var parsed = new GlbLowLevelParser(path, bytes).Parse();

            // VRMDataを作成
            var vrmData = new VRMData(parsed);

            // VRMImporterContextを作成
            using var vrmImporter = new VRMImporterContext(vrmData);

            // モデルを非同期で読み込み
            var instance = await vrmImporter.LoadAsync(new RuntimeOnlyAwaitCaller());

            // モデルのGameObjectを取得
            GameObject model = instance.Root;

            // モデルの位置を設定（例：原点に配置）
            model.transform.position = Vector3.zero;
            
            // Y軸に180度回転
            model.transform.Rotate(0, 180, 0);

            // モデルをアクティブ化
            model.SetActive(true);

            // 全てのRendererを有効化
            EnableAllRenderers(model);

            Debug.Log($"モデルのロードと表示が完了しました: {path}");

            return model;
        }

        /// <summary>
        /// モデル内の全てのRendererコンポーネントを有効化する
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
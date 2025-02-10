using System;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Logging;
using UnityEngine;

namespace uDesktopMascot
{
    /// <summary>
    /// キャラクターモデルを読み込むクラス
    /// </summary>
    public static class LoadCharacterModel
    {
        /// <summary>
        /// モデルの種類を表す列挙型
        /// </summary>
        private enum ModelType
        {
            Unknown,
            FBX,
            VRM,
            GLTF,
            GLB
        }

        /// <summary>
        /// キャラクターモデルを読み込む
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async UniTask<LoadedVRMInfo> LoadModel(CancellationToken cancellationToken)
        {
            // 設定ファイルからキャラクター情報の取得
            var characterSettings = ApplicationSettings.Instance.Character;

            LoadedVRMInfo loadedVrmInfo = null;

            // モデルの種類を判定
            var modelType = GetModelType(characterSettings.ModelPath);

            try
            {
                switch (modelType)
                {
                    case ModelType.FBX:
                        // FBXモデルの読み込み
                        var model = await LoadFBX.LoadModelAsync(characterSettings.ModelPath, cancellationToken);
                        loadedVrmInfo = new LoadedVRMInfo(model, null, null);
                        break;
                    case ModelType.VRM:
                    case ModelType.GLB:
                    case ModelType.GLTF:
                        // VRM、GLB、GLTFモデルの読み込み
                        loadedVrmInfo = await LoadVRM.LoadModelAsync(characterSettings.ModelPath, cancellationToken);
                        break;
                    default:
                        Log.Error("サポートされていないモデル形式です: {0}", characterSettings.ModelPath);
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Error("モデルのロード中にエラーが発生しました: {0}", ex.Message);
            }

            // モデルのロードが失敗した場合、デフォルトモデルをロードする
            if (loadedVrmInfo == null || loadedVrmInfo.Model == null)
            {
                Log.Warning("指定されたモデルのロードに失敗したため、デフォルトモデルをロードします。");
                var defaultModel = await LoadVRM.LoadDefaultModel();
                
                loadedVrmInfo = new LoadedVRMInfo(defaultModel.gameObject, defaultModel.Vrm.Meta.Name, defaultModel.Vrm.Meta.Thumbnail);

                if (loadedVrmInfo.Model == null)
                {
                    Log.Error("デフォルトモデルのロードにも失敗しました。");
                    return null;
                }
            }

            if (characterSettings.UseLilToon)
            {
                // シェーダーをlilToonに置き換える
                bool shaderReplaceSuccess = ReplaceShadersWithLilToon(loadedVrmInfo.Model);

                if (!shaderReplaceSuccess)
                {
                    Log.Warning("シェーダーの置き換えに失敗したため、デフォルトのシェーダーを使用します。");
                }
            }
            
            return loadedVrmInfo;
        }

        /// <summary>
        /// ファイルパスからモデルの種類を判定する
        /// </summary>
        /// <param name="modelPath"></param>
        /// <returns></returns>
        private static ModelType GetModelType(string modelPath)
        {
            if (string.IsNullOrEmpty(modelPath))
            {
                return ModelType.Unknown;
            }

            string extension = Path.GetExtension(modelPath).ToLowerInvariant();

            switch (extension)
            {
                case ".fbx":
                    return ModelType.FBX;
                case ".vrm":
                    return ModelType.VRM;
                case ".gltf":
                    return ModelType.GLTF;
                case ".glb":
                    return ModelType.GLB;
                default:
                    return ModelType.Unknown;
            }
        }

        /// <summary>
        /// モデルのシェーダーをlilToonに置き換える
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private static bool ReplaceShadersWithLilToon(GameObject model)
        {
            try
            {
                // lilToon のシェーダーを取得
                var lilToonCutoutShader = Shader.Find("Hidden/lilToonCutout");
                var lilToonTransparentShader = Shader.Find("Hidden/lilToonTransparent");

                if (lilToonCutoutShader == null || lilToonTransparentShader == null)
                {
                    Log.Warning("lilToon シェーダーの一部が見つかりません。プロジェクトに lilToon シェーダーが含まれており、正しくインストールされていることを確認してください。");
                    return false;
                }

                // すべての Renderer を取得
                var renderers = model.GetComponentsInChildren<Renderer>(true);

                foreach (var renderer in renderers)
                {
                    // 各 Renderer のマテリアルを取得
                    var materials = renderer.materials;

                    foreach (var material in materials)
                    {
                        if (material == null || material.shader == null)
                        {
                            continue;
                        }

                        // シェーダーを置き換え
                        material.shader = lilToonCutoutShader;

                        material.SetFloat("_TransparentMode", 2); // 0: Opaque, 1: Cutout, 2: Transparent, etc.
                        material.SetFloat("_OutlineEnable", 1);   // アウトラインを有効化
                    }
                }

                Log.Info("シェーダーの置き換えが完了しました。");
                return true;
            }
            catch (Exception e)
            {
                Log.Error($"シェーダーの置き換え中にエラーが発生しました: {e.Message}");
                return false;
            }
        }
    }
}
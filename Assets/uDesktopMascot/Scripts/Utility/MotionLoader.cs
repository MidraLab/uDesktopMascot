using System.IO;
using Cysharp.Threading.Tasks;
using UniGLTF;
using UniVRM10;

namespace uDesktopMascot
{
    /// <summary>
    /// モーションをロードするクラス.
    /// </summary>
    public class MotionLoader
    {
        /// <summary>
        /// モーションフォルダのパス.
        /// </summary>
        private const string MotionFolderPath = "Motion";
        
        /// <summary>
        /// StreamingAssets/motion フォルダからモーションを読み込む
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async UniTask<IVrm10Animation> LoadMotionsAsync(string path)
        {
            var ext = Path.GetExtension(path).ToLower();
            if (ext == ".bvh")
            {
                return null;
                // return BvhMotion.LoadBvhFromPath(path);
            }

            // gltf, glb etc...
            using GltfData data = new AutoGltfFileParser(path).Parse();
            using var loader = new VrmAnimationImporter(data);
            RuntimeGltfInstance instance = await loader.LoadAsync(new ImmediateCaller());
            return instance.GetComponent<Vrm10AnimationInstance>();
        }
    }
}
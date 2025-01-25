#if UNITY_EDITOR
using NaughtyAttributes;
using UnityEngine;

namespace uDesktopMascot
{
    /// <summary>
    ///   BGMのデータを保持するDataObject Editor拡張
    /// </summary>
    public partial class BGMData
    {
        [Button("BGMファイル登録を最新化")]
        private void UpdateBGMFiles()
        {
            bgmClips.Clear();
            var bgmFiles = Resources.LoadAll<AudioClip>("DefaultBGM");
            foreach (var bgm in bgmFiles)
            {
                bgmClips.Add(bgm);
            }
        }
    }
}
#endif

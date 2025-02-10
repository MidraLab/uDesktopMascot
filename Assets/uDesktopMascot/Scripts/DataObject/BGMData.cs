using System.Collections.Generic;
using UnityEngine;

namespace uDesktopMascot
{
    /// <summary>
    ///    BGMのデータを保持するDataObject
    /// </summary>
    [CreateAssetMenu(fileName = "DefaultBGMData", menuName = "uDesktopMascot/DefaultBGMData", order = 0)]
    public partial class BGMData : ScriptableObject
    {
        /// <summary>
        ///    BGMのリスト
        /// </summary>
        [SerializeField] private List<AudioClip> bgmClips;
        
        /// <summary>
        ///    BGMのリスト
        /// </summary>
        public IList<AudioClip> BgmClips => bgmClips;
    }
}
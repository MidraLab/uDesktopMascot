using System.Collections.Generic;
using UnityEngine;

namespace uDesktopMascot
{
    [CreateAssetMenu(fileName = "DefaultVoiceData", menuName = "uDesktopMascot/DefaultVoiceData", order = 0)]
    public class VoiceData : ScriptableObject
    {
        /// <summary>
        ///     クリックボイス
        /// </summary>
        [SerializeField] private List<AudioClip> clickVoice;

        /// <summary>
        ///     ドラッグボイス
        /// </summary>
        [SerializeField] private List<AudioClip> dragVoice;

        /// <summary>
        ///     アプリ起動時のボイス
        /// </summary>
        [SerializeField] private List<AudioClip> startVoice;

        /// <summary>
        ///     アプリ終了時のボイス
        /// </summary>
        [SerializeField] private List<AudioClip> endVoice;
        
        /// <summary>
        ///   クリックボイス
        /// </summary>
        public IList<AudioClip> ClickVoice => clickVoice;
        
        /// <summary>
        ///  ドラッグボイス
        /// </summary>
        public IList<AudioClip> DragVoice => dragVoice;
        
        /// <summary>
        /// アプリ起動時のボイス
        /// </summary>
        public IList<AudioClip> StartVoice => startVoice;
        
        /// <summary>
        /// アプリ終了時のボイス
        /// </summary>
        public IList<AudioClip> EndVoice => endVoice;
    }
}
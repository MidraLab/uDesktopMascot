using UnityEngine;
using UnityEngine.UI;

namespace uDesktopMascot
{
    /// <summary>
    ///   マイクのオンオフを切り替えるアイコン
    /// </summary>
    public class SwitchMicrophoneIcon : MonoBehaviour
    {
        [SerializeField] private Sprite microphoneOnIcon;
        [SerializeField] private Sprite microphoneOffIcon;

        [SerializeField] private Image microphoneImage;

        /// <summary>
        ///  マイクのアイコンを切り替える
        /// </summary>
        /// <param name="isMicrophoneOn"></param>
        public void SwitchIcon(bool isMicrophoneOn)
        {
            microphoneImage.sprite = isMicrophoneOn ? microphoneOnIcon : microphoneOffIcon;
        }
    }
}
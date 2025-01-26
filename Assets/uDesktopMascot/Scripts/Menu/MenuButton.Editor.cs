#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.UI;

namespace uDesktopMascot
{
    /// <summary>
    ///   メニューボタンクラス Editor 拡張
    /// </summary>
    public partial class MenuButton
    {
        private void OnValidate()
        {
            if (_button == null)
            {
                _button = GetComponent<Button>();
            }
            if (_button != null && buttonText != null)
            {
                buttonText.enabled = _button.interactable;
                EditorUtility.SetDirty(buttonText);
            }
        }
    }
}
#endif
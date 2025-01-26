#if UNITY_EDITOR
using NaughtyAttributes;

namespace uDesktopMascot
{
    /// <summary>
    /// キャラクターの管理を行うクラス Editor部分
    /// </summary>
    public partial class CharacterManager
    {
        [Button("ドラッグ中")]
        private void DebugDragTrue()
        {
            _isDragging = true;
            _isDraggingModel = true;
        }

        [Button("ドラッグ終了")]
        private void DebugDragFalse()
        {
            _isDragging = false;
            _isDraggingModel = false;
        }
    }
}
#endif

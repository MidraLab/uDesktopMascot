using UnityEngine.InputSystem;

namespace uDesktopMascot
{
    /// <summary>
    ///   入力コントローラー
    /// </summary>
    public class InputController : Singleton<InputController>
    {
        /// <summary>
        /// Input Systemのアクション
        /// </summary>
        private readonly UDMInputActions _inputActions;
        
        /// <summary>
        /// UIアクション
        /// </summary>
        public UDMInputActions.UIActions UI => _inputActions.UI;
        
        /// <summary>
        /// デバッグアクション
        /// </summary>
        public UDMInputActions.DebugActions Debug => _inputActions.Debug;
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public InputController()
        {
            _inputActions = new UDMInputActions();
            _inputActions.UI.Enable();
        }

    }
}
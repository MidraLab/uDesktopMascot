using Unity.Logging;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR

namespace uDesktopMascot
{
    /// <summary>
    ///   メニューのプレゼンター Editor拡張
    /// </summary>
    public partial class MenuPresenter
    {
        /// <summary>
        ///   デバッグメニューを初期化する
        /// </summary>
        private void InitDebugMenu()
        {
            InputController.Instance.Debug.Enable();

            InputController.Instance.Debug.Quit.performed += DebugCloseApp;
            InputController.Instance.Debug.HelpButton.performed += DebugHelp;
            InputController.Instance.Debug.SwitchShowMenu.performed += DebugSwitchShowMenu;
        }
        
        /// <summary>
        /// メニュの表示を切り替える
        /// </summary>
        /// <param name="context"></param>
        private void DebugSwitchShowMenu(InputAction.CallbackContext context)
        {
            if (IsOpened)
            {
                Hide();
            }
            else
            {
                Show(Vector3.zero);
            }
        }
        
        /// <summary>
        ///  アプリケーションを終了する
        /// </summary>
        /// <param name="context"></param>
        private void DebugCloseApp(InputAction.CallbackContext context)
        {
            CloseApp();
        }
        
        /// <summary>
        /// ヘルプページを開く
        /// </summary>
        private void DebugHelp(InputAction.CallbackContext context)
        {
            OpenHelp();
        }
        
        /// <summary>
        /// キャラクターモデルの変更
        /// </summary>
        private void DebugModelSetting()
        {
            Log.Debug("ModelSetting");
        }
        
        /// <summary>
        /// アプリケーション設定
        /// </summary>
        private void DebugAppSetting()
        {
            Log.Debug("AppSetting");
        }
        
        /// <summary>
        /// デバッグメニューの破棄
        /// </summary>
        private void OnDestroyEditor()
        {
            InputController.Instance.Debug.Quit.performed -= DebugCloseApp;
            InputController.Instance.Debug.HelpButton.performed -= DebugHelp;
            InputController.Instance.Debug.SwitchShowMenu.performed -= DebugSwitchShowMenu;
        }
    }
}
#endif

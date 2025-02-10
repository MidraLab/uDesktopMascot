#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using AOT;

namespace uDesktopMascot
{
    /// <summary>
    /// エクスプローラーウィンドウの情報
    /// </summary>
    public class ExplorerWindowInfo
    {
        public WindowsAPI.RECT rect;
        public IntPtr hWnd;
    }

    /// <summary>
    /// エクスプローラーウィンドウを検出するクラス
    /// </summary>
    public static class WindowsExplorerUtility
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll")]
        private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        /// <summary>
        /// ロジックスピクセルのX方向の値
        /// </summary>
        private const int LOGPIXELSX = 88;

        /// <summary>
        /// ロジックスピクセルのY方向の値
        /// </summary>
        private const int LOGPIXELSY = 90;

        /// <summary>
        /// DPIスケールを取得
        /// </summary>
        /// <returns></returns>
        public static float GetDPIScale()
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            try{
                int dpiX = GetDeviceCaps(hdc, LOGPIXELSX);
                int dpiY = GetDeviceCaps(hdc, LOGPIXELSY);

                float dpiScaleX = dpiX / 96.0f;
                float dpiScaleY = dpiY / 96.0f;

                return (dpiScaleX + dpiScaleY) / 2.0f;

            } finally
            {
                ReleaseDC(IntPtr.Zero, hdc);
            }
        }

        public static List<ExplorerWindowInfo> GetExplorerWindows()
        {
            List<ExplorerWindowInfo> explorerWindows = new List<ExplorerWindowInfo>();

            // explorerWindows リストを GCHandle で固定
            var handle = GCHandle.Alloc(explorerWindows);
            try
            {
                // 静的なコールバックメソッドを使用し、lParam に GCHandle のポインタを渡す
                WindowsAPI.EnumWindows(EnumWindowsCallback, GCHandle.ToIntPtr(handle));
            } finally
            {
                // GCHandle を解放
                handle.Free();
            }

            return explorerWindows;
        }

        // MonoPInvokeCallback 属性を追加
        [MonoPInvokeCallback(typeof(WindowsAPI.EnumWindowsProc))]
        private static bool EnumWindowsCallback(IntPtr hWnd, IntPtr lParam)
        {
            // lParam から explorerWindows リストを取得
            var handle = GCHandle.FromIntPtr(lParam);
            if (handle.Target is List<ExplorerWindowInfo> explorerWindows)
            {
                if (WindowsAPI.IsWindowVisible(hWnd))
                {
                    StringBuilder className = new StringBuilder(256);
                    WindowsAPI.GetClassName(hWnd, className, className.Capacity);

                    // エクスプローラーウィンドウのクラス名は "CabinetWClass" または "ExploreWClass"
                    var classNameStr = className.ToString();
                    if (classNameStr == "CabinetWClass" || classNameStr == "ExploreWClass")
                    {
                        if (WindowsAPI.GetWindowRect(hWnd, out var rect))
                        {
                            ExplorerWindowInfo info = new ExplorerWindowInfo
                            {
                                hWnd = hWnd,
                                rect = rect
                            };

                            explorerWindows.Add(info);
                        }
                    }
                }
            }

            return true;
        }
    }
}
#endif
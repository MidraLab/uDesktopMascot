using UnityEngine;
using System.Runtime.InteropServices;

namespace uDesktopMascot
{
    /// <summary>
    /// Windows上のマウスに関するユーティリティクラス
    /// </summary>
    public static class MouseUtility
    {
        /// <summary>
        /// マウスポインタのポジション格納用(WindowsAPI呼び出しで使用)
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        /// <summary>
        /// マウスポインタの位置を取得する
        /// </summary>
        public static Vector2 GetCursorPosition()
        {
            if (!GetCursorPos(out POINT point))
            {
                return Vector2.zero;
            }

            return new Vector2(point.X, point.Y);
        }
    }
}
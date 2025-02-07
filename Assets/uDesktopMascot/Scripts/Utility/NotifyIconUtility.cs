#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
using System;
using System.Runtime.InteropServices;
using Unity.Logging;
using UnityEngine;

namespace uDesktopMascot
{
    // MonoPInvokeCallback属性の定義
    [AttributeUsage(AttributeTargets.Method)]
    public class MonoPInvokeCallbackAttribute : Attribute
    {
        public MonoPInvokeCallbackAttribute(Type t) { }
    }

    public static class NotifyIconUtility
    {
        // 構造体と定数の定義

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct NOTIFYICONDATA
        {
            public uint cbSize;                 // 構造体のサイズ
            public IntPtr hWnd;                 // ウィンドウハンドル
            public uint uID;                    // 識別子
            public uint uFlags;                 // フラグ
            public uint uCallbackMessage;       // コールバックメッセージ
            public IntPtr hIcon;                // アイコンハンドル

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szTip;                // ツールチップテキスト

            public uint dwState;                // 状態
            public uint dwStateMask;            // 状態マスク

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szInfo;               // バルーン通知テキスト

            public uint uTimeoutOrVersion;      // タイムアウトまたはバージョン

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szInfoTitle;          // バルーン通知タイトル

            public uint dwInfoFlags;            // バルーン通知フラグ

            public Guid guidItem;               // アイテムのGUID

            public IntPtr hBalloonIcon;         // バルーン通知のカスタムアイコン
        }

        public enum NIF : uint
        {
            MESSAGE    = 0x00000001,
            ICON       = 0x00000002,
            TIP        = 0x00000004,
            STATE      = 0x00000008,
            INFO       = 0x00000010,
            GUID       = 0x00000020,
            REALTIME   = 0x00000040,
            SHOWTIP    = 0x00000080,
        }

        public enum NIM : uint
        {
            ADD         = 0x00000000,
            MODIFY      = 0x00000001,
            DELETE      = 0x00000002,
            SETFOCUS    = 0x00000003,
            SETVERSION  = 0x00000004,
        }

        // Win32 API 関数の宣言

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern bool Shell_NotifyIcon(NIM dwMessage, ref NOTIFYICONDATA lpData);

        [DllImport("user32.dll")]
        private static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIconName);

        [DllImport("user32.dll")]
        private static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr LoadImage(IntPtr hInst, string lpszName, uint uType, int cxDesired, int cyDesired, uint fuLoad);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        private static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint msg, UIntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();

        [DllImport("kernel32.dll")]
        private static extern uint GetLastError();

        private const uint IMAGE_ICON = 1;
        private const uint LR_LOADFROMFILE = 0x00000010;
        private const int GWL_WNDPROC = -4;
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;

        private static readonly IntPtr IDI_APPLICATION = (IntPtr)0x7F00; // デフォルトアイコン

        private static NOTIFYICONDATA notifyIconData;
        private static bool notifyIconAdded = false;

        private static IntPtr oldWndProcPtr = IntPtr.Zero;
        private static WndProcDelegate newWndProcDelegate;
        
        private const uint WM_USER = 0x0400;
        private const uint WM_TRAYICON = WM_USER + 1; // 通知領域アイコン用のメッセージ識別子

        // マウスメッセージ定数
        private const uint WM_LBUTTONDOWN = 0x0201;
        private const uint WM_LBUTTONDBLCLK = 0x0203;

        // ウィンドウプロシージャ用のデリゲート
        private delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, UIntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 通知領域にアイコンを表示し、ウィンドウを非表示にする
        /// </summary>
        /// <param name="tooltip">ツールチップテキスト</param>
        /// <param name="iconPath">アイコンファイルのパス（.ico形式）</param>
        public static void ShowNotifyIcon(string tooltip = "アプリケーション", string iconPath = null)
        {
            if (notifyIconAdded)
            {
                Log.Warning("通知領域アイコンは既に追加されています。");
                return;
            }

            IntPtr hWnd = GetCurrentWindowHandle();
            if (hWnd == IntPtr.Zero)
            {
                Log.Error("ウィンドウハンドルの取得に失敗しました。");
                return;
            }

            // アイコンパスを指定
            if (string.IsNullOrEmpty(iconPath))
            {
                iconPath = System.IO.Path.Combine(Application.streamingAssetsPath, "Icon", "icon.ico");
                Log.Debug($"アイコンパス: {iconPath}");

                // ファイルの存在を確認
                if (!System.IO.File.Exists(iconPath))
                {
                    Log.Error($"アイコンファイルが見つかりません: {iconPath}");
                }
            }

            notifyIconData = new NOTIFYICONDATA();
            notifyIconData.cbSize = (uint)Marshal.SizeOf(notifyIconData);
            notifyIconData.hWnd = hWnd;
            notifyIconData.uID = 1;
            notifyIconData.uFlags = (uint)(NIF.MESSAGE | NIF.ICON | NIF.TIP);
            notifyIconData.uCallbackMessage = WM_TRAYICON;

            // アイコンの設定
            notifyIconData.hIcon = LoadIconFromFile(iconPath);

            notifyIconData.szTip = tooltip;

            bool result = Shell_NotifyIcon(NIM.ADD, ref notifyIconData);
            if (!result)
            {
                Log.Error($"通知領域アイコンの追加に失敗しました。エラーコード: {GetLastError()}");
            }
            else
            {
                notifyIconAdded = true;
                Log.Debug("通知領域アイコンを追加しました。");

                // ウィンドウプロシージャをフック
                newWndProcDelegate = new WndProcDelegate(WndProc);
                IntPtr newWndProcPtr = Marshal.GetFunctionPointerForDelegate(newWndProcDelegate);
                oldWndProcPtr = SetWindowLongPtr(hWnd, GWL_WNDPROC, newWndProcPtr);
                if (oldWndProcPtr == IntPtr.Zero)
                {
                    Log.Error($"ウィンドウプロシージャのフックに失敗しました。エラーコード: {GetLastError()}");
                }

                // ウィンドウを非表示にする
                ShowWindow(hWnd, SW_HIDE);
                Log.Debug("ウィンドウを非表示にしました。");
            }
        }

        /// <summary>
        /// 通知領域アイコンを非表示にし、ウィンドウプロシージャを元に戻す
        /// </summary>
        private static void HideNotifyIcon()
        {
            if (!notifyIconAdded)
            {
                Log.Warning("通知領域アイコンが追加されていません。");
                return; // アイコンが追加されていない場合は無視
            }

            bool result = Shell_NotifyIcon(NIM.DELETE, ref notifyIconData);
            if (!result)
            {
                Log.Error($"通知領域アイコンの削除に失敗しました。エラーコード: {GetLastError()}");
            }
            else
            {
                notifyIconAdded = false;
                Log.Debug("通知領域アイコンを削除しました。");

                // ウィンドウプロシージャを元に戻す
                IntPtr hWnd = GetCurrentWindowHandle();
                if (hWnd != IntPtr.Zero && oldWndProcPtr != IntPtr.Zero)
                {
                    IntPtr resultPtr = SetWindowLongPtr(hWnd, GWL_WNDPROC, oldWndProcPtr);
                    if (resultPtr == IntPtr.Zero)
                    {
                        Log.Error($"ウィンドウプロシージャの復元に失敗しました。エラーコード: {GetLastError()}");
                    }
                    oldWndProcPtr = IntPtr.Zero;
                    newWndProcDelegate = null;
                }
            }

            if (notifyIconData.hIcon != IntPtr.Zero)
            {
                DestroyIcon(notifyIconData.hIcon);
                notifyIconData.hIcon = IntPtr.Zero;
            }
        }

        // ウィンドウプロシージャの実装
        [MonoPInvokeCallback(typeof(WndProcDelegate))]
        private static IntPtr WndProc(IntPtr hWnd, uint msg, UIntPtr wParam, IntPtr lParam)
        {
            if (msg != WM_TRAYICON)
            {
                return CallWindowProc(oldWndProcPtr, hWnd, msg, wParam, lParam);
            }

            if (wParam.ToUInt32() != notifyIconData.uID)
            {
                return CallWindowProc(oldWndProcPtr, hWnd, msg, wParam, lParam);
            }

            uint mouseMessage = (uint)lParam.ToInt64();
            if (mouseMessage != WM_LBUTTONDOWN && mouseMessage != WM_LBUTTONDBLCLK)
            {
                return CallWindowProc(oldWndProcPtr, hWnd, msg, wParam, lParam);
            }

            // 左クリックまたはダブルクリックでウィンドウを再表示
            ShowWindow(hWnd, SW_SHOW);
            Log.Debug("ウィンドウを再表示しました。");

            // 通知領域アイコンを削除
            HideNotifyIcon();

            // 必要に応じて他の処理を追加
            SystemManager.Instance.ForceStopUniWinControllerHitTestFlag(false);

            // 既存のウィンドウプロシージャを呼び出す
            return CallWindowProc(oldWndProcPtr, hWnd, msg, wParam, lParam);
        }

        /// <summary>
        /// アイコンファイルからアイコンをロード
        /// </summary>
        /// <param name="iconPath">アイコンファイルのパス</param>
        /// <returns>アイコンハンドル</returns>
        private static IntPtr LoadIconFromFile(string iconPath)
        {
            // アイコンファイルをロード
            IntPtr hIcon = IntPtr.Zero;
            if (System.IO.File.Exists(iconPath))
            {
                hIcon = LoadImage(IntPtr.Zero, iconPath, IMAGE_ICON, 0, 0, LR_LOADFROMFILE);
                if (hIcon == IntPtr.Zero)
                {
                    Log.Error($"アイコンの読み込みに失敗しました: {iconPath}");
                }
            }
            else
            {
                Log.Error($"アイコンファイルが見つかりません: {iconPath}");
            }
            return hIcon;
        }

        /// <summary>
        /// 現在のウィンドウハンドルを取得する
        /// </summary>
        /// <returns>ウィンドウハンドル</returns>
        private static IntPtr GetCurrentWindowHandle()
        {
            IntPtr hWnd = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;

            // ハンドルが取得できない場合はアクティブウィンドウを取得
            if (hWnd == IntPtr.Zero)
            {
                hWnd = GetActiveWindow();
            }

            return hWnd;
        }
    }
}
#endif
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBAnaly.Module
{
    public class KeyboardListener
    {
        private static IntPtr _hookID = IntPtr.Zero; // 全局钩子句柄
        private static LowLevelKeyboardProc _hookCallback; // 静态字段保存委托，防止GC回收
        private static readonly List<Action<Keys, bool, bool, bool>> Callbacks = new List<Action<Keys, bool, bool, bool>>(); // 存储所有注册的回调函数
        private static readonly object _lock = new object(); // 确保线程安全
        private static int _refCount = 0; // 引用计数，管理钩子的注册和释放

        private KeyboardListener() { }

        /// <summary>
        /// 注册一个键盘监听回调
        /// </summary>
        /// <param name="callback">键盘回调函数</param>
        public static void Register(Action<Keys, bool, bool, bool> callback)
        {
            lock (_lock)
            {
                // 添加回调到列表
                Callbacks.Add(callback);

                // 如果钩子还未设置，则设置钩子
                if (_refCount == 0)
                {
                    _hookCallback = HookCallback; // 保存回调防止GC回收
                    _hookID = SetHook(_hookCallback);
                }

                // 增加引用计数
                _refCount++;
            }
        }

        /// <summary>
        /// 注销一个键盘监听回调
        /// </summary>
        /// <param name="callback">键盘回调函数</param>
        public static void Unregister(Action<Keys, bool, bool, bool> callback)
        {
            lock (_lock)
            {
                // 移除回调
                Callbacks.Remove(callback);

                // 减少引用计数
                _refCount--;

                // 如果没有回调需要监听，释放钩子
                if (_refCount == 0)
                {
                    UnhookWindowsHookEx(_hookID);
                    _hookID = IntPtr.Zero;
                }
            }
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                IntPtr hook = SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);

                if (hook == IntPtr.Zero)
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    throw new SystemException($"设置全局键盘钩子失败，错误代码: {errorCode}");
                }

                return hook;
            }
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Keys key = (Keys)vkCode;

                // 检测修饰键状态
                bool ctrl = Control.ModifierKeys.HasFlag(Keys.Control);
                bool shift = Control.ModifierKeys.HasFlag(Keys.Shift);
                bool alt = Control.ModifierKeys.HasFlag(Keys.Alt);

                // 调用所有注册的回调函数
                lock (_lock)
                {
                    foreach (var callback in Callbacks)
                    {
                        callback?.Invoke(key, ctrl, shift, alt);
                    }
                }
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        #region Windows API

        private const int WH_KEYBOARD_LL = 13; // 全局键盘钩子
        private const int WM_KEYDOWN = 0x0100; // 键盘按下消息

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion
    }
}

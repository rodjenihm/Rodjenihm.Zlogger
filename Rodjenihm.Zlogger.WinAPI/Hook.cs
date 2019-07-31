using System;
using System.Diagnostics;

namespace Rodjenihm.Zlogger.WinAPI
{
    public abstract class Hook
    {
        public IntPtr HookId { get; protected set; } = IntPtr.Zero;
        public HookType HookType { get; protected set; }

        public IntPtr SetHook(HOOKPROC hookProc)
        {
            using (var currentProcess = Process.GetCurrentProcess())
            using (var mainProcessModule = currentProcess.MainModule)
            {
                return HookId = User32.SetWindowsHookEx(HookType, hookProc,
                    Kernel32.GetModuleHandle(mainProcessModule.ModuleName), 0);
            }
        }

        public bool RemoveHook()
        {
            return User32.UnhookWindowsHookEx(HookId);
        }

        public IntPtr NextHook(int nCode, IntPtr wParam, IntPtr lParam)
        {
            return User32.CallNextHookEx(HookId, nCode, wParam, lParam);
        }
    }
}

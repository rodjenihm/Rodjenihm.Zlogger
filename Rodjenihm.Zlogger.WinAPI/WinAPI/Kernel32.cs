using System;
using System.Runtime.InteropServices;

namespace Rodjenihm.Zlogger
{
    internal static class Kernel32
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();
    }
}

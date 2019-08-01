using System;
using System.Windows.Forms;

namespace Rodjenihm.Zlogger.Core
{
    public class KeyEventArgs : EventArgs
    {
        public int VkCode { get; set; }
        public bool IsCapsLocked { get; set; } = Control.IsKeyLocked(Keys.CapsLock);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rodjenihm.Zlogger.WinAPI
{
    public class LowLevelKeyboardHook : Hook
    {
        public LowLevelKeyboardHook()
        {
            HookType = HookType.WH_KEYBOARD_LL;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HisRoyalRedness.com
{
    public static class EulerNative
    {
        [DllImport("EulerNative.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "lcm")]
        public static extern ulong LeastCommonMultiple(ulong a, ulong b);

    }
}

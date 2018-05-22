using System.Runtime.InteropServices;

namespace RestBin.Common.PInvoke
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Header
    {

        public int version;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string type; 

    }

}

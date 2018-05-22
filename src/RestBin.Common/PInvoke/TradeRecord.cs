﻿using System.Runtime.InteropServices;

namespace RestBin.Common.PInvoke
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TradeRecord
    {

        public int id;

        public int account;

        public double volume;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string comment;

    }

}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestBin.Common.Utils
{
    public static class IOHelper
    {

        public static readonly string SYS32_DIR = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "System32");

    }
}

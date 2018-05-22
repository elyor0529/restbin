using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestBin.Common.Utils
{
    public static class ProcessTool
    {
        public static void Start(string fileName, string args, string workDirectory)
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        UseShellExecute = false,
                        FileName = fileName,
                        CreateNoWindow = true,
                        LoadUserProfile = true,
                        RedirectStandardOutput = false,
                        Verb = "runas",
                        WindowStyle = ProcessWindowStyle.Hidden,
                        WorkingDirectory = workDirectory,
                        Arguments = args
                    },
                };

                if (process.Start())
                    process.WaitForExit();
            }
            catch (Exception e)
            {
                Logging.Error(e.ToString());
            }
        }
    }
}

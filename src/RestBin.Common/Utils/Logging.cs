using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using log4net;
using log4net.Config;

namespace RestBin.Common.Utils
{
    public static class Logging
    {

        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Configure(string path = "./log4net.config")
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            //reseting prev config
            LogManager.ResetConfiguration();

            //setup config
            XmlConfigurator.Configure(new FileInfo(path));
        }

        public static void Info(string info)
        {
            Logger.Info(info);
        }

        public static void Error(string error)
        {
            Logger.Info(error);
        }

    }
}

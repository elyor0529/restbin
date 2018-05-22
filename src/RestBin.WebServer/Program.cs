using System;
using RestBin.Common.Utils;
using RestBin.WebServer.Rest;

namespace RestBin.WebServer
{
    internal static class Program
    {
        private const int PORT = 8899;

        static Program()
        {
            Logging.Configure();
        }

        private static void Main(string[] args)
        {
            var server = new WebServerFactory(PORT);
            {
                server.Start();
            }

            Console.WriteLine("Server started.");
            Console.WriteLine("Runned server by this port {0}.Press to any key server is stoped.",PORT);
            Console.ReadLine(); 
            server.Stop(); 
        }
    }
}
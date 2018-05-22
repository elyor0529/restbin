using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Formatting;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestBin.Common.Utils;
using RestBin.WebServer.Rest.Configs;
using RestBin.WebServer.Rest.IOC;

namespace RestBin.WebServer.Rest
{

    public sealed class WebServerFactory : IDisposable
    {
        private readonly string _baseAddress;
        private readonly HttpSelfHostServer _server;

        public WebServerFactory(int port)
        {
            _baseAddress = "http://localhost:" + port;

            var config = new HttpSelfHostConfiguration(_baseAddress);

            JsonConfig.Register(config);
            RouteConfig.Register(config);
            DependencyConfig.Register(config);
             DbConfig.Migrate();

            _server = new HttpSelfHostServer(config);
        }

        public void Start()
        {

            //http://stackoverflow.com/questions/2521950/wcf-selfhosted-service-installer-class-and-netsh
            var everyone = new SecurityIdentifier("S-1-1-0").Translate(typeof(NTAccount)).ToString();

            //Use Netsh.exe to give your account permissions to reserve the URL.
            //https://www.asp.net/web-api/overview/older-versions/self-host-a-web-api
            ProcessTool.Start("netsh", "http add urlacl url=" + _baseAddress + " user=" + everyone, IOHelper.SYS32_DIR);

            _server.OpenAsync().Wait();

            Logging.Info("Rest server is tarted.Listening by " + _baseAddress);
        }

        public void Stop()
        {
            _server.CloseAsync().Wait();

            ProcessTool.Start("netsh", "http delete urlacl url=" + _baseAddress, IOHelper.SYS32_DIR);

            Logging.Info("Rest server is stopped");
        }

        public void Dispose()
        {
            _server.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
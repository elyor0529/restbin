using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;

namespace RestBin.WebServer.Rest.Configs
{
    public static class RouteConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new
            {
                id = RouteParameter.Optional
            }).DataTokens["Namespaces"] = new string[] { "RestBin.WebServer.Rest.Controllers" };
            config.MapHttpAttributeRoutes(new DefaultDirectRouteProvider());
        }

    }
}

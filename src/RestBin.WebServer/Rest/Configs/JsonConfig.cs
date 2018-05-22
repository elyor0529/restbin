using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RestBin.WebServer.Rest.Configs
{
    public static class JsonConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var json = config.Formatters.JsonFormatter.SerializerSettings;

            json.Formatting = Formatting.Indented;
            json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            json.ContractResolver = new CamelCasePropertyNamesContractResolver();
            json.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
        }
    }
}

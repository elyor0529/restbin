using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using RestBin.ClientEndpoint.Infrastructure;
using RestBin.Common.Extensions;
using RestBin.Common.Models;
using RestBin.Common.PInvoke;
using RestBin.Common.Utils;
using RestBin.Common.ViewModels;

namespace RestBin.ClientEndpoint.Proxy
{

    public sealed class ExportApiEndpoint : BaseApiEndpoint
    {
        private const string API_PATH = "api/export";
        private const string JSON_MIME_TYPE = "application/json";

        public ExportApiEndpoint(string url) : base(url)
        {
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JSON_MIME_TYPE));
        }

        /// <summary>
        /// get all entries
        /// </summary>
        /// <returns></returns>
        public byte[] Get()
        {
            var resp = Client.GetAsync(API_PATH).Result;

            resp.EnsureSuccessStatusCode();

            var model = resp.Content.ReadAsAsync<IEnumerable<ExportViewModel>>().Result;

            return OOXMLHelper.Export(model);
        }

    }
}

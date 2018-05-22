using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using RestBin.ClientEndpoint.Infrastructure;
using RestBin.Common.Extensions;
using RestBin.Common.Models;
using RestBin.Common.PInvoke;
using RestBin.Common.Utils;

namespace RestBin.ClientEndpoint.Proxy
{

    public sealed class ImportApiEndpoint : BaseApiEndpoint
    {
        private const string API_PATH = "api/import";
        private const string JSON_MIME_TYPE = "application/json";

        public ImportApiEndpoint(string url) : base(url)
        {
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JSON_MIME_TYPE));
        }

        /// <summary>
        /// post binary file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool Post(string file)
        { 
            var resp = Client.PostAsJsonAsync(API_PATH, file).Result;

            resp.EnsureSuccessStatusCode();

            return resp.Content.ReadAsAsync<bool>().Result;
        }
    }
}

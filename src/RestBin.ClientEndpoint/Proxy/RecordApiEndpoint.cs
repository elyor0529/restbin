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

    public sealed class RecordApiEndpoint : BaseApiEndpoint
    {
        private const string API_PATH = "api/record";

        public RecordApiEndpoint(string url) : base(url)
        {
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
         

        /// <summary>
        /// get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TradeRecordModel Get(int id)
        {
            var resp = Client.GetAsync(API_PATH + "/" + id).Result;

            resp.EnsureSuccessStatusCode();

            return resp.Content.ReadAsAsync<TradeRecordModel>().Result;
        }
         
        /// <summary>
        /// delete entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var resp = Client.DeleteAsync(API_PATH + "/" + id).Result;

            resp.EnsureSuccessStatusCode();

            return resp.Content.ReadAsAsync<bool>().Result;
        }

    }
}

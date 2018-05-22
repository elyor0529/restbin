using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RestBin.ClientEndpoint.Infrastructure
{
    public abstract class BaseApiEndpoint : IDisposable
    {

        protected readonly HttpClient Client = new HttpClient();

        protected BaseApiEndpoint(string url)
        {
            Client.BaseAddress = new Uri(url);
        }

        public void Dispose()
        {
            Client.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}

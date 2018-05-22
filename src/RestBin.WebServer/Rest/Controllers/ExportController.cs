using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RestBin.Common.Exceptions;
using RestBin.Common.Extensions;
using RestBin.Common.Models;
using RestBin.Common.PInvoke;
using RestBin.Common.ViewModels;
using RestBin.WebServer.EF;
using ServiceStack;

namespace RestBin.WebServer.Rest.Controllers
{
    /// <summary>
    ///     Header controller. Contains CURD operations on product
    /// </summary>
    public class ExportController : ApiController
    {

        private readonly IEntityRepository<HeaderModel> _vRepository;

        public ExportController(IEntityRepository<HeaderModel> vRepository)
        {
            _vRepository = vRepository;
        }

        // GET api/<controller>
        public IEnumerable<ExportViewModel> Get()
        {
            var query = _vRepository.GetAll();
            var items = new List<ExportViewModel>();

            foreach (var header in query.GroupBy(g => new { g.Version, g.Type }))
            {
                items.AddRange(header.SelectMany(s => s.TradeRecords).Select(s => new ExportViewModel
                {
                    Version = header.Key.Version,
                    Type = header.Key.Type,
                    Id = s.Id,
                    Account = s.Account,
                    Volume = s.Account,
                    Comment = s.Comment
                }));
            }

            return items;
        }
         
        protected override void Dispose(bool disposing)
        {
            _vRepository.Dispose();
        }

    }
}
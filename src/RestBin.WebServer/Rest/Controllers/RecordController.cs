using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RestBin.Common.Exceptions;
using RestBin.Common.Models;
using RestBin.Common.PInvoke;
using RestBin.WebServer.EF;

namespace RestBin.WebServer.Rest.Controllers
{
    /// <summary>
    ///     Header controller. Contains CURD operations on product
    /// </summary>
    public class RecordController : ApiController
    {
        private readonly IEntityRepository<TradeRecordModel> _tRepository;

        public RecordController(IEntityRepository<TradeRecordModel> tRepository)
        {
            _tRepository = tRepository;
        }

        // GET api/<controller>/5
        public TradeRecordModel Get(int id)
        {
            return _tRepository.GetById(id);
        }

        // DELETE api/<controller>/5
        public bool Delete(int id)
        {
            var model = _tRepository.GetById(id);

            if (model == null)
                throw new AppException("Entry is null");

            _tRepository.Remove(id);

            return _tRepository.Save() > 0;
        }

        protected override void Dispose(bool disposing)
        {
            _tRepository.Dispose();
        }
    }
}
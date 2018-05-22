using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using RestBin.Common.Exceptions;
using RestBin.Common.Extensions;
using RestBin.Common.Models;
using RestBin.Common.PInvoke;
using RestBin.Common.Utils;
using RestBin.WebServer.EF;

namespace RestBin.WebServer.Rest.Controllers
{
    /// <summary>
    ///     Header controller. Contains CURD operations on product
    /// </summary>
    public class ImportController : ApiController
    {
        private readonly IEntityRepository<TradeRecordModel> _tRepository;
        private readonly IEntityRepository<HeaderModel> _vRepository;

        public ImportController(IEntityRepository<HeaderModel> vRepository, IEntityRepository<TradeRecordModel> tRepository)
        {
            _vRepository = vRepository;
            _tRepository = tRepository;
        }

        public bool Post([FromBody] string file)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Upload", file);

            if (!File.Exists(filePath))
                throw new AppException("File '" + file + "' is not found");

            //read buffer
            var buffer = File.ReadAllBytes(filePath);
            var model = StructureParser.Deserialize(buffer).AsModel();

            //check unique version
            var ent = _vRepository.Find(a => a.Version == model.Version);
            var isNew = false;

            //if null
            if (ent == null)
            {
                isNew = true;
                ent = new HeaderModel
                {
                    Type = model.Type,
                    Version = model.Version
                };
            }
            else
            {
                //also update
                ent.Type = model.Type;
                ent.Version = model.Version;
            }

            //migrate exists trader records 
            foreach (var record in model.TradeRecords)
            {
                var trader = ent.TradeRecords.FirstOrDefault(a => a.Id == record.Id);

                if (trader == null)
                {
                    trader = new TradeRecordModel
                    {
                        Id = record.Id,
                        Volumne = record.Volumne,
                        Account = record.Account,
                        Comment = record.Comment
                    };

                    ent.TradeRecords.Add(trader);
                }
                else
                {
                    trader.Volumne = record.Volumne;
                    trader.Account = record.Account;
                    trader.Comment = record.Comment;
                }
            }

            //add
            if (isNew)
                _vRepository.Add(ent);
            else
                //edit
                _vRepository.Edit(ent);

            return _vRepository.Save() > 0;
        }

        protected override void Dispose(bool disposing)
        {
            _vRepository.Dispose();
            _tRepository.Dispose();
        }
    }
}
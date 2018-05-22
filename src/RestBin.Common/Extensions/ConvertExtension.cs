using System;
using System.Globalization;
using RestBin.Common.Exceptions;
using RestBin.Common.Models;
using RestBin.Common.PInvoke;

namespace RestBin.Common.Extensions
{
    public static class ConvertExtension
    {
        public static HeaderModel AsModel(this Tuple<Header, TradeRecord[]> item)
        {
            //header
            var model = new HeaderModel
            {
                Type = item.Item1.type,
                Version = item.Item1.version
            };

            //header
            foreach (var record in item.Item2)
            {
                model.TradeRecords.Add(new TradeRecordModel
                {
                    Volumne = record.volume,
                    Account = record.account,
                    Id = record.id,
                    Comment = record.comment
                });
            }

            return model;
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using RestBin.Common.Models;
using RestBin.Common.ViewModels;

namespace RestBin.Common.Utils
{
    public static class OOXMLHelper
    {
        public static byte[] Export(IEnumerable<ExportViewModel> items)
        {
            using (var stream = new MemoryStream())
            {
                var wb = new XLWorkbook(XLEventTracking.Disabled);
                var ws = wb.Worksheets.Add("Sheet1");

                ws.Cell(1, 1).Value = "version";
                ws.Cell(1, 2).Value = "type";
                ws.Cell(1, 3).Value = "id";
                ws.Cell(1, 4).Value = "account";
                ws.Cell(1, 5).Value = "volume";
                ws.Cell(1, 6).Value = "comment";

                var row = 1;
                foreach (var item in items)
                {
                    row++;

                    ws.Cell(row, 1).Value = item.Version;
                    ws.Cell(row, 2).Value = item.Type;
                    ws.Cell(row, 3).Value = item.Id;
                    ws.Cell(row, 4).Value = item.Account;
                    ws.Cell(row, 5).Value = item.Volume;
                    ws.Cell(row, 6).Value = item.Comment;
                }

                wb.SaveAs(stream);

                return stream.GetBuffer();
            }

        }
    }
}

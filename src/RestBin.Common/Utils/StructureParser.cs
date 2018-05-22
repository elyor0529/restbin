using System;
using System.Runtime.InteropServices;
using RestBin.Common.Exceptions;
using RestBin.Common.PInvoke;

namespace RestBin.Common.Utils
{

    public static class StructureParser
    {
        public static byte[] Serialize(Header header, TradeRecord[] records)
        {
             
            try
            {
                var array = new byte[0];

                //header
                var headerSize = Marshal.SizeOf<Header>();
                Array.Resize(ref array, headerSize);

                var headerPtr = Marshal.AllocHGlobal(headerSize);
                Marshal.StructureToPtr(header, headerPtr, true);
                Marshal.Copy(headerPtr, array, 0, headerSize);
                Marshal.FreeHGlobal(headerPtr);

                //records
                var recordSize = Marshal.SizeOf<TradeRecord>();

                for (var i = 0; i < records.Length; i++)
                {
                    Array.Resize(ref array, headerSize + (i + 1) * recordSize);

                    var recordPtr = Marshal.AllocHGlobal(recordSize);
                    Marshal.StructureToPtr(records[i], recordPtr, true);
                    Marshal.Copy(recordPtr, array, headerSize + i * recordSize, recordSize);
                    Marshal.FreeHGlobal(recordPtr);

                }

                return array;
            }
            catch (Exception e)
            {
                Logging.Error(e.ToString());

                throw new AppException(e);
            }
        }

        public static Tuple<Header, TradeRecord[]> Deserialize(byte[] array)
        {

            try
            {
                //header
                var headerSize = Marshal.SizeOf<Header>();
                var headerPtr = Marshal.AllocHGlobal(headerSize);
                Marshal.Copy(array, 0, headerPtr, headerSize);

                var header = (Header)Marshal.PtrToStructure(headerPtr, typeof(Header));
                Marshal.FreeHGlobal(headerPtr);

                //records
                var recordSize = Marshal.SizeOf<TradeRecord>();
                var totalRecords = (array.Length - headerSize) / recordSize;
                var records = new TradeRecord[totalRecords];

                for (var i = 0; i < totalRecords; i++)
                {
                    var recordPtr = Marshal.AllocHGlobal(recordSize);
                    Marshal.Copy(array, headerSize + i * recordSize, recordPtr, recordSize);

                    var record = (TradeRecord)Marshal.PtrToStructure(recordPtr, typeof(TradeRecord));
                    Marshal.FreeHGlobal(recordPtr);

                    records[i] = record;
                }

                return Tuple.Create(header, records);
            }
            catch (Exception e)
            {
                Logging.Error(e.ToString());

                throw new AppException(e);
            }
        }
    }
}
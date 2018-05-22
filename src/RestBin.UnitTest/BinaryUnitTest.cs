using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestBin.Common.PInvoke;
using RestBin.Common.Utils;

namespace RestBin.UnitTest
{
    [TestClass]
    public class BinaryUnitTest
    {
        private static readonly string BIN_FILE = Path.Combine(Environment.CurrentDirectory, "myDataStruct.bin");

        [TestMethod]
        [Description("Encode")]
        public void TestMethod_01()
        {
            var header = new Header
            {
                version = 1,
                type = "type1"
            };
            var records = new TradeRecord[64];
            for (var i = 0; i < records.Length; i++)
            {
                records[i] = new TradeRecord
                {
                    account = i + 1,
                    id = i,
                    volume = i * 10d,
                    comment = "comment" + i
                };
            }

            var bytes = StructureParser.Serialize(header, records);

            Assert.AreNotEqual(bytes.Length,0,"Bad encoded");

            File.WriteAllBytes(BIN_FILE, bytes); 
        }

        [TestMethod]
        [Description("Decode")]
        public void TestMethod_02()
        {
            var bytes = File.ReadAllBytes(BIN_FILE);
            var myDataStruct1 = StructureParser.Deserialize(bytes);
            var bytes1 = StructureParser.Serialize(myDataStruct1.Item1, myDataStruct1.Item2);

            Assert.IsTrue(bytes.SequenceEqual(bytes1), "Bad decoded");

        }

    }
}

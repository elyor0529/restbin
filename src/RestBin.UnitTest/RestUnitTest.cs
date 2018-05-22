using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestBin.ClientEndpoint.Proxy;

namespace RestBin.UnitTest
{
    [TestClass]
    public class RestUnitTest
    {
        private const string BASE_URL = "http://localhost:8899";
        private static readonly string BIN_FILE = Path.Combine(Environment.CurrentDirectory, "test.bin");
        private static readonly string EXPORT_FILE = Path.Combine(Environment.CurrentDirectory, "test.xlsx");

        [TestMethod]
        [Description("Upload binary")]
        public void TestMethod_01()
        { 
            var importClient = new ImportApiEndpoint(BASE_URL);
            var result = importClient.Post(BIN_FILE);

            Assert.IsTrue(result, "Upload is failed");

            Console.WriteLine("Binary uploaded.");
        }

        [TestMethod]
        [Description("Export excel")]
        public void TestMethod_02()
        { 
            var exportClient = new ExportApiEndpoint(BASE_URL);
            var data = exportClient.Get();

            Assert.IsTrue(data.Length > 0, "Export is failed");

            File.WriteAllBytes(EXPORT_FILE, data);
        }

        [TestMethod]
        [Description("Get record by id")]
        public void TestMethod_03()
        {
            var recordClient = new RecordApiEndpoint(BASE_URL);
            var id = 1;
            var data = recordClient.Get(id);

            Assert.IsNotNull(data, "Record is null");
            Console.WriteLine("Record#{0} is ready", data.Id);
        }

        [TestMethod]
        [Description("Delete record by id")]
        public void TestMethod_04()
        { 
            var recordClient = new RecordApiEndpoint(BASE_URL);
            var id = 1;

            Assert.IsTrue(recordClient.Delete(id), "Deleting failed");
        } 

    }
}

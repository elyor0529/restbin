using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Plossum.CommandLine;
using RestBin.ClientEndpoint.Proxy;
using RestBin.Common.PInvoke;
using RestBin.Common.Utils;

namespace RestBin.Cmd
{
    [CommandLineManager(ApplicationName = "Rest Bin", Copyright = "Copyright (C) 2017", EnabledOptionStyles = OptionStyles.Group | OptionStyles.LongUnix)]
    [CommandLineOptionGroup("commands", Name = "Commands", Require = OptionGroupRequirement.ExactlyOne)]
    [CommandLineOptionGroup("options", Name = "Options")]
    internal class Options
    {
        [CommandLineOption(Name = "g", Aliases = "generate", Description = "Generated test data", GroupId = "commands")]
        public string Generate { get; set; }

        [CommandLineOption(Name = "u", Aliases = "upload", Description = "Convert remote file to SQL Compact database", GroupId = "commands")]
        public string Upload { get; set; }

        [CommandLineOption(Name = "d", Aliases = "download", Description = "Download excel file from server side database", GroupId = "commands")]
        public string Download { get; set; }

        [CommandLineOption(Name = "f", Aliases = "find", Description = "Get record by id", GroupId = "commands")]
        public int Find { get; set; }

        [CommandLineOption(Name = "r", Aliases = "remove", Description = "Delete record by id", GroupId = "commands")]
        public int Remove { get; set; }

        [CommandLineOption(Name = "h", Aliases = "help", Description = "Shows this help text", GroupId = "options")]
        public bool Help { get; set; }
    }

    internal static class Program
    {
        private const string BASE_URL = "http://localhost:8899";

        private static void Main(string[] args)
        {
            var options = new Options();
            var parser = new CommandLineParser(options);
            parser.Parse();

            if (options.Help)
            {
                Console.WriteLine(parser.UsageInfo.ToString(78, false));

                return;
            }

            if (parser.HasErrors)
            {
                Console.WriteLine(parser.UsageInfo.ToString(78, true));

                return;
            }

            if (!String.IsNullOrWhiteSpace(options.Generate))
            {
                Generate(options.Generate);
            }

            if (!String.IsNullOrWhiteSpace(options.Upload))
            {
                Upload(options.Upload);
            }

            if (!String.IsNullOrWhiteSpace(options.Download))
            {
                Download(options.Download);
            }

            if (options.Find>0)
            {
                GetById(options.Find);
            }

            if (options.Remove>0)
            {
                DeleteById(options.Remove);
            }

#if DEBUG
            Console.ReadLine();
#endif  
        }

        private static void Generate(string file)
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
            File.WriteAllBytes(file, bytes);

            Console.WriteLine("Test binary file is generated!");
            Console.WriteLine("Please copy to server in temparery folder and put file name with '-u' command");
        }

        /// <summary>
        ///     Upload binary
        /// </summary>
        private static void Upload(string file)
        {
            var importClient = new ImportApiEndpoint(BASE_URL);
            var result = importClient.Post(file);

            if (result)
                Console.WriteLine("Binary file is uploaded.");
        }

        /// <summary>
        ///     Export excel
        /// </summary>
        private static void Download(string file)
        {
            var exportClient = new ExportApiEndpoint(BASE_URL);
            var data = exportClient.Get();

            File.WriteAllBytes(file, data);

            Console.WriteLine("Excel file '{0}' is ready!", file);

            try
            {
                Process.Start(file);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        ///     Get record by id
        /// </summary>
        private static void GetById(int id)
        {
            var recordClient = new RecordApiEndpoint(BASE_URL);
            var data = recordClient.Get(id);

            if (data != null)
            {
                Console.WriteLine("Record#{0} is ready", data.Id);
                Console.WriteLine("*************************");
                Console.WriteLine("Account:{0}", data.Account);
                Console.WriteLine("Volume:{0}", data.Volumne);
                Console.WriteLine("{0}", data.Comment);
                Console.WriteLine("*************************");
            }
        }

        /// <summary>
        ///     Delete record by id
        /// </summary>
        private static void DeleteById(int id)
        {
            var recordClient = new RecordApiEndpoint(BASE_URL);

            if (recordClient.Delete(id))
            {
                Console.WriteLine("Record#{0} is deleted", id);
            }
        }
    }
}
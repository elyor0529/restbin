namespace RestBin.Common.ViewModels
{
    public class ExportViewModel
    {
        //header
        public int Version { get; set; }
        public string Type { get; set; }

        //record
        public int Id { get; set; }
        public int Account { get; set; }
        public double Volume { get; set; }
        public string Comment { get; set; }
    }
}
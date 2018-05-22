using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestBin.Common.Models
{
    public class TradeRecordModel
    {
        [Key]
        public int Id { get; set; }

        public int Account { get; set; }

        public double Volumne { get; set; }

        [StringLength(64)]
        [Required]
        public string Comment { get; set; }

        [ForeignKey("Header")]
        public int? HeaderVersion { get; set; }

        public virtual HeaderModel Header { get; set; }

    }
}

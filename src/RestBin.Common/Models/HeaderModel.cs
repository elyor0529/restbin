using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestBin.Common.Models
{
    public class HeaderModel
    {
        [Key]
        public int Version { get; set; }

        [Required]
        [StringLength(16)]
        public string Type { get; set; }

        public virtual ICollection<TradeRecordModel> TradeRecords { get; set; }

        public HeaderModel()
        {
            TradeRecords = new HashSet<TradeRecordModel>();
        }

    }
}

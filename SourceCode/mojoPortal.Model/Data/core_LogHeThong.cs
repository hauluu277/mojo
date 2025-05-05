using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("core_LogHeThong")]
    public class core_LogHeThong
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ItemID { get; set; }
        public string DiaChiIP { get; set; }
        public string Type { get; set; }
        public string HanhDongThaoTac { get; set; }
        public string NoiDung { get; set; }
        public string DuongDanThaoTac { get; set; }
        public string CreatedBy { get; set; }
        public int CreatedByUser { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

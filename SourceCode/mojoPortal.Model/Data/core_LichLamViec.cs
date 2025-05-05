using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("core_LichLamViec")]
    public class core_LichLamViec
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ItemID { get; set; }
        public DateTime? NgayLamViec { get; set; }
        public DateTime? ThoiGianLamViec { get; set; }
        public string NoiDung { get; set; }
        public string DiaDiem { get; set; }
        public string ThanhPhanThamDu { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public int? CreatedByUser { get; set; }
        public int? EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
        public bool IsPublish { get; set; }
    }
}

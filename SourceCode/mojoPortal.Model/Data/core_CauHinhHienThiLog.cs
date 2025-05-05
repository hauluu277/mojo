using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("core_CauHinhHienThiLog")]
    public class core_CauHinhHienThiLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ItemID { get; set; }
        public string TruongHienThi { get; set; }
        public string MaTruongHienThi { get; set; }
        public bool IsShow { get; set; }
        public string CreateBy { get; set; }
        public int CreateByUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EditDate { get; set; }
        public int EditByUser { get; set; }
    }
}

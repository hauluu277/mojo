using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("core_ThuTuc_ThanhPhanHS")]
    public class core_ThuTuc_ThanhPhanHS
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long ItemID { get; set; }
        public Nullable<long> IdThuTuc { get; set; }
        public string TenGiayTo { get; set; }
        public string MauDonToKhai { get; set; }
        public string SoLuong { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("md_BaoCao")]
    public  class md_BaoCao
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ItemID { get; set; }
        public string TenBaoCao { get; set; }
        public Nullable<int> NamChuKyBaoCao { get; set; }
        public string BieuMau { get; set; }
        public string SoQuyetDinhCongBo { get; set; }
        public Nullable<System.DateTime> NgayCongBo { get; set; }
        public string PathFile { get; set; }
        public Nullable<int> LinhVucID { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<int> CreatedByUser { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ItemUrl { get; set; }
        public Nullable<System.Guid> ItemGuid { get; set; }
        public Nullable<int> SiteID { get; set; }
        public Nullable<bool> IsPublish { get; set; }
    }
}

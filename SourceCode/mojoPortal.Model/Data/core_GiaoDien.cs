

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mojoPortal.Model.Entities
{

    [Table("core_GiaoDien")]
    public  class core_GiaoDien
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ItemID { get; set; }
        public Nullable<int> SiteID { get; set; }
        public string TenGiaoDien { get; set; }
        public string MaGiaoDien { get; set; }
        public string DuongDan { get; set; }
        public string DuongDanZipTaiLen { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<int> CreatedByUser { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public Nullable<int> EditByUser { get; set; }
    }
}

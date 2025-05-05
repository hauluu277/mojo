using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("core_Category")]
    public class core_Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ItemID { get; set; }
        public Nullable<int> ParentID { get; set; }
        public int SiteID { get; set; }
        public string Name { get; set; }
        public string NameEN { get; set; }
        public string Description { get; set; }
        public int ItemCount { get; set; }
        public System.DateTime CreatedUtc { get; set; }
        public System.Guid CreatedBy { get; set; }
        public System.DateTime ModifiedUtc { get; set; }
        public System.Guid ModifiedBy { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<int> LangID { get; set; }
        public Nullable<int> IconID { get; set; }
        public Nullable<bool> Automatic { get; set; }
        public Nullable<int> CoreSkinID { get; set; }
        public Nullable<bool> CoreSkinDefault { get; set; }
        public Nullable<bool> IsPhongBan { get; set; }
        public Nullable<bool> ShowMenuLeft { get; set; }
        public string PathIMG { get; set; }
        public string PathFile { get; set; }
        public string SubName { get; set; }
        public Nullable<bool> IsTinTuc { get; set; }
        public Nullable<bool> IsLinhVucDieuTra { get; set; }
        public string Code { get; set; }
        public string Sumary { get; set; }
        public Nullable<bool> TargetBlank { get; set; }
        public string Color { get; set; }
        public bool? ShowCategoryChild { get; set; }
    }
}

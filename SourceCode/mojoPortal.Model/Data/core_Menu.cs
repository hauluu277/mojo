using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("core_Menu")]
    public  class core_Menu
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ItemID { get; set; }
        public Nullable<int> SiteID { get; set; }
        public Nullable<int> ParentID { get; set; }
        public string Name { get; set; }
        public string LinkMenu { get; set; }
        public string ImageUrl { get; set; }
        public Nullable<int> OrderBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> UpdateBy { get; set; }
        public string StyleCss { get; set; }
        public Nullable<int> TypeMenu { get; set; }
        public Nullable<bool> Show { get; set; }
        public Nullable<bool> IsDisplayLink { get; set; }
        public Nullable<bool> IsPhongBan { get; set; }
        public Nullable<bool> IsEnglish { get; set; }
        public Nullable<int> TypeLink { get; set; }
        public Nullable<long> ItemLink { get; set; }
        public Nullable<bool> IsLogin { get; set; }
        public Nullable<bool> NoClick { get; set; }
        public Nullable<bool> TargetBlank { get; set; }
    }
}

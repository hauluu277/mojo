using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("core_CCTC")]
    public class core_CCTC
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ItemID { get; set; }
        public Nullable<int> SiteID { get; set; }
        public string Description { get; set; }
        public Nullable<bool> ShowLanhDao { get; set; }
        public Nullable<bool> ShowPhongBan { get; set; }
        public string TitleBoxLanhDao { get; set; }
        public string TitleBoxPhongBan { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<int> CreatedByUser { get; set; }
        public Nullable<System.DateTime> EditedDate { get; set; }
        public Nullable<int> EditedBy { get; set; }
    }
}

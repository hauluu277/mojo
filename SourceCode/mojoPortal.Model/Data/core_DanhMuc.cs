using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("core_DanhMuc")]
    public class core_DanhMuc
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long ItemID
        {
            get; set;
        }
        public int SiteID { get; set; }
        public string Title { get; set; }
        public string UrlLink { get; set; }
        public string PathIMG { get; set; }
        public string Sapo { get; set; }
        public int OrderBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int CreatedByUser { get; set; }
        public bool IsPublish { get; set; }
    }
}

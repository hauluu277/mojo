using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("core_SettingMenu")]
    public class core_SettingMenu
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ItemID { get; set; }
        public Nullable<int> SiteID { get; set; }
        public string UrlIMG { get; set; }
        public string UrlItem { get; set; }
        public Nullable<int> TypeIMG { get; set; }
    }
}

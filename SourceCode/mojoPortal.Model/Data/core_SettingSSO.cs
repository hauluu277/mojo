using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("core_SettingSSO")]
    public class core_SettingSSO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ItemID { get; set; }
        public string UrlSSO { get; set; }
        public string UrlSSOReturn { get; set; }
        public bool ActiveSSO { get; set; }
        public string BackgroundButton { get; set; }
        public string TypeTheme { get; set; }
        public bool IsDisable { get; set; }

    }
}

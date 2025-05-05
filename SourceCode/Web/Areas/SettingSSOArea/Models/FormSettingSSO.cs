using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mojoPortal.Web.Areas.SettingSSOArea.Models
{
    public class FormSettingSSO
    {
        public int ItemID { get; set; }
        [Required(ErrorMessage ="UrlSSO không được để trống")]
        public string UrlSSO { get; set; }
        [Required(ErrorMessage = "UrlSSO trả về không được để trống")]
        public string UrlSSOReturn { get; set; }
        public bool ActiveSSO { get; set; }
        public string BackgroundButton { get; set; }
        public string TypeTheme { get; set; }
        public bool IsDisable { get; set; }
    }
}
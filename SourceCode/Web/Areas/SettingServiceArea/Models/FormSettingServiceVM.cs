using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mojoPortal.Web.Areas.SettingServiceArea.Models
{
    public class FormSettingServiceVM
    {
        public int ItemID { get; set; }
        public Nullable<int> SiteID { get; set; }
        [Required(ErrorMessage ="Bạn nhập tên webservice/api")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Bạn nhập Url webservice/api")]
        public string ServiceUrl { get; set; }
        public bool? IsNew { get; set; }
    }
}
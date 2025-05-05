using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Web.Areas.GiaoDienArea.Models
{
    public class GiaoDienFormVM
    {
        public int ItemID { get; set; }
        public Nullable<int> SiteID { get; set; }
        public string TenGiaoDien { get; set; }
        public string MaGiaoDien { get; set; }
        public string DuongDan { get; set; }
        public string DuongDanZipTaiLen { get; set; }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Service.CommonModel.Site
{
    public class SiteSortBO
    {
        public int SiteID { get; set; }
        public string SiteName { get; set; }
        public int? LinhVucID { get; set; }
        public string LinhVucName { get; set; }
        public string UrlSiteMap { get; set; }
    }
}

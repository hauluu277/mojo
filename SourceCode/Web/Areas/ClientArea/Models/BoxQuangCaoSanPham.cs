using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Web.Areas.ClientArea.Models
{
    public class BoxQuangCaoSanPham
    {
        public string TenNhanHieu {  get; set; }
        public string UrlWebsite { get; set; }
        public string UrlLogo {  get; set; }
        public List<QuangCaoItem> ListItem { get; set; }
    }
    public class QuangCaoItem
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public string Price { get; set; }
    }

    public class RenderBoxQuangCaoInput
    {
        public BoxQuangCaoSanPham Model { get; set; }
        public int CountItem { get; set; } = 6;
    }
}
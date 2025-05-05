using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mojoPortal.Web.Areas.CategoryArea.Models
{
    public class CategoryFormVM
    {
        public int ItemID { get; set; }
        public int? ParentID { get; set; }
        public int SiteID { get; set; }
        [Required(ErrorMessage ="Tiêu đề không được để trống")]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public string PathIMG { get; set; }
    }
}
using mojoPortal.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.CategoryArea.Models
{
    public class FormCategoryModel
    {
        public CoreCategory Category { get; set; }
        public List<SelectListItem> ListParent { get; set; }
        public int? ParentID { get; set; }
        public int SiteID { get; set; }
        public int OrderBy { get; set; }
        public string Name { get; set; }
        public bool IsCategoryArticle { get; set; }


    }
}
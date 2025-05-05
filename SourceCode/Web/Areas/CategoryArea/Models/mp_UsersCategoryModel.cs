using mojoPortal.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.CategoryArea.Models
{
    public class mp_UsersCategoryModel
    {
        public CoreCategory Category { get; set; }
        public List<SelectListItem> ListParent { get; set; }
        public int? ParentID { get; set; }
        public int SiteID { get; set; }
        public int OrderBy { get; set; }

    }
}


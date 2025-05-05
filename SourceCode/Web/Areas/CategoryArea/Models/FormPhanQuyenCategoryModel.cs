using mojoPortal.Business;
using mojoPortal.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.CategoryArea.Models
{
    public class FormPhanQuyenCategoryModel
    {
        public CoreCategory Category { get; set; }
        public List<SelectListItem> ListParent { get; set; }
        public int? ParentID { get; set; }
        public int SiteID { get; set; }
        public int OrderBy { get; set; }

        public List<mp_Users> ListUser { get; set; }
        public List<mp_Users> ListUserSelected { get; set; }
    }
}
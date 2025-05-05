using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using mojoPortal.Business;

namespace mojoPortal.Web.Areas.MenuContextArea.Models
{
    public class FormMenuModel
    {
        public coreMenu Menu { get; set; }
        public List<SelectListItem> ListParent { get; set; }
        public int? ParentID { get; set; }
        public int TypeMenu { get; set; }
        public int SiteID { get; set; }
        public bool? Show { get; set; }
        public bool IsLogin { get; set; }
        public string TreeId { get; set; }
        public List<SelectListItem> ListTypeLink { get; set; }
        public List<SelectListItem> ListItemLink { get; set; }
    }
}
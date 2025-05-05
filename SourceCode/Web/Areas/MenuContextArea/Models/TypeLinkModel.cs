using mojoPortal.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.MenuContextArea.Models
{
    public class TypeLinkModel
    {
        public List<SelectListItem> ListLinkItem { get; set; }
        public int TypeItem { get; set; }
        public coreMenu MenuObj { get; set; }
        public string UrlItem { get; set; }
    }
}
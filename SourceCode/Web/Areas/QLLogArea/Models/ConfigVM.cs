using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.QLLogArea.Models
{
    public class ConfigVM
    {
        public List<SelectListItem> listLoaiLog { get; set; }
        public List<string> configItem { get; set; }
    }
}
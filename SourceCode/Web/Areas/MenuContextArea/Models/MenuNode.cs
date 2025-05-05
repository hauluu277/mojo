using mojoPortal.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Web.Areas.MenuContextArea.Models
{
    public class TreeNodeBO
    {
        public string name { get; set; }
        public int id { get; set; }
        public List<TreeNodeBO> children { get; set; }
        public int ParentId { get; set; }
        public int OrderBy { get; set; }
        public string LinkMenu { get; set; }
        public string IsDisplayText { get; set; }
        public bool? Show { get; set; }
    }
    public class MenuNode
    {
        public string name { get; set; }
        public int id { get; set; }
        public List<MenuNode> children { get; set; }
    }
}
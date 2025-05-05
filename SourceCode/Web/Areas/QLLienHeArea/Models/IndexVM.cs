using mojoportal.Service.CommonBusiness;
using mojoPortal.Service.CommonModel.QLLienHe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Web.Areas.QLLienHeArea.Models
{
    public class IndexVM
    {
        public PageListResultBO<QLLienHeBO> ListData { get; set; }
    }
}
using mojoportal.Service.CommonBusiness;
using mojoPortal.Service.CommonModel.QLLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Web.Areas.QLLogArea.Models
{
    public class IndexVM
    {
        public PageListResultBO<QLLogBO> ListData { get; set; }
    }
}
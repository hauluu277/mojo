using mojoportal.Service.CommonBusiness;
using mojoPortal.Service.CommonModel.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Web.Areas.SettingServiceArea.Models
{
    public class SettingServiceIndexVM
    {
        public PageListResultBO<SiteBO> ListData { get; set; }
    }
}
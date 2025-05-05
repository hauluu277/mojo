using mojoportal.Service.CommonBusiness;
using mojoPortal.Service.CommonModel.GiaoDien;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Web.Areas.GiaoDienArea.Models
{
    public class GiaoDienIndexVM
    {
        public PageListResultBO<GiaoDienBO> ListData { get; set; }
    }
}
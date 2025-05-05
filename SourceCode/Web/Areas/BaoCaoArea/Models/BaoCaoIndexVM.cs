using mojoportal.Service.CommonBusiness;
using mojoPortal.Model.Data;
using mojoPortal.Service.CommonModel.BaoCao;
using System.Collections.Generic;

namespace mojoPortal.Web.Areas.BaoCaoArea.Models
{
    public class BaoCaoIndexVM
    {
        public PageListResultBO<BaoCaoBO> ListData { get; set; }
        public List<core_Category> ListCategory { get; set; }
        public List<md_BaoCao> ListBaoCao { get; set; }
    }
}
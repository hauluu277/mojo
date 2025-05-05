using mojoportal.Service.CommonBusiness;
using mojoPortal.Model.Data;
using mojoPortal.Service.CommonModel.ThuTuc;
using System.Collections.Generic;

namespace mojoPortal.Web.Areas.ThuTucArea.Models
{
    public class ThuTucIndexVM
    {
        public PageListResultBO<ThuTucBO> ListData { get; set; }
        public List<core_Category> ListCategory { get; set; }
        public List<core_ThuTuc> ListThuTuc { get; set; }
    }
}
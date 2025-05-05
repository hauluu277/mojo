using mojoPortal.Model.Data;
using mojoPortal.Service.CommonModel.BaoCao;
using System.Collections.Generic;

namespace mojoPortal.Web.Areas.ThuTucArea.Models
{
    public class BaoCaoListVM
    {
        public List<BaoCaoBO> ListBaoCao { get; set; }
        public core_Category Category { get; set; }
    }
}
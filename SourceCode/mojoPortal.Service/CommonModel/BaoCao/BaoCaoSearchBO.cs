using mojoportal.Service.BaseBusines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Service.CommonModel.BaoCao
{
    public class BaoCaoSearchBO : SearchBaseBO
    {
        public string QR_TEN_BAOCAO { get; set; }
        public int? QR_NAM_CHUKYBAOCAO { get; set; }
        public int? QR_LINHVUC { get; set; }
        public string QR_SOQD_CONGBO { get; set; }
        public bool? QR_ISPUBLISH { get; set; }
    }
}

using mojoportal.Service.BaseBusines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Service.CommonModel.ThuTuc
{
    public class ThuTucSearchBO : SearchBaseBO
    {
        public string KeywordFillter { get; set; }
        public string TenThuThucFillter { get; set; }
        public int? IdLinhVucFillter { get; set; }
        public int? IdCoQuanFilter { get; set; }
        public int? IdMucDoDVCFillter { get; set; }
        public int? IdCachThucThucHienFilter { get; set; }
        public int? IdDoiTuongThucHien { get; set; }
    }
}

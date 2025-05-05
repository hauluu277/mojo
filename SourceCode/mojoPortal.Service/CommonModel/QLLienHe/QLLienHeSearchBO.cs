using mojoportal.Service.BaseBusines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Service.CommonModel.QLLienHe
{
    public class QLLienHeSearchBO : SearchBaseBO
    {
        public string TenFilter { get; set; }
        public string EmailFilter { get; set; }
        public string SubjectFilter { get; set; }
        public int? TrangThaiPhanHoiFilter { get; set; }
    }
}

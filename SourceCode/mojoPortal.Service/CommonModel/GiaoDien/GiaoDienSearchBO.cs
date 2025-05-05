using mojoportal.Service.BaseBusines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Service.CommonModel.GiaoDien
{
    public class GiaoDienSearchBO : SearchBaseBO
    {
        public string TenGiaoDienFillter { get; set; }
        public string MaGiaoDienFillter { get; set; }
    }
}

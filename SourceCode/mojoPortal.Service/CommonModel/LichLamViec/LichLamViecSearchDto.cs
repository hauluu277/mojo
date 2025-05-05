using mojoportal.Service.BaseBusines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Service.CommonModel.LichLamViec
{
    public class LichLamViecSearchDto : SearchBaseBO
    {
        public int? IdLanhDaoFilter { get; set; }
        public string NgayLamViecFilter { get; set; }
    }
}

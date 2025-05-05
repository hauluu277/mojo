using mojoportal.Service.BaseBusines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Service.CommonModel.QLLog
{
    public class QLLogSearchBO : SearchBaseBO
    {
        public string TypeFilter { get; set; }
        public string HanhDongThaoTacFilter { get; set; }
    }
}

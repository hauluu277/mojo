using mojoportal.Service.BaseBusines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Service.CommonModel.client
{
    public class ClientSearchBO : SearchBaseBO
    {
        public string QR_ClientName { get; set; }
        public int groupId { get; set; }
    }
}

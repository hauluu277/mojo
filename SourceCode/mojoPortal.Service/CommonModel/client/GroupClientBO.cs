using mojoPortal.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Service.CommonModel.client
{
    public class GroupClientBO
    {
        public string GroupName { get; set; }
        public List<core_Client> listClients { get; set; }
    }
}

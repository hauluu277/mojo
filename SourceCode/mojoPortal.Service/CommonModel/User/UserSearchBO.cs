using mojoportal.Service.BaseBusines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Service.CommonModel.User
{
    public class UserSearchBO : SearchBaseBO
    {
        public string HoTenFilter { get; set; } 
        public string EmailFilter { get; set; }
    }
}

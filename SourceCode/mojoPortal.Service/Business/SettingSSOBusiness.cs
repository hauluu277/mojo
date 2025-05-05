using mojoportal.Service.BaseBusines;
using mojoportal.Service.UoW;
using mojoPortal.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Service.Business
{
    public class SettingSSOBusiness : BaseBusiness<core_SettingSSO>
    {
        public SettingSSOBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public core_SettingSSO GetFirst()
        {
            var getFirst = context.core_SettingSSO.FirstOrDefault();
            if (getFirst == null) return new core_SettingSSO();
            return getFirst;
        }
    }
}

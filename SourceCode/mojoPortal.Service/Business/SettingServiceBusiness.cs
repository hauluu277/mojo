using mojoportal.Service.BaseBusines;
using mojoportal.Service.UoW;
using mojoPortal.Model.Data;
using System.Collections.Generic;
using System.Linq;

namespace mojoPortal.Service.Business
{

    public class SettingServiceBusiness : BaseBusiness<core_SettingService>
    {
        public SettingServiceBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public List<core_SettingService> GetListBy(int siteId)
        {
            return this.context.core_SettingService.Where(x => x.SiteID == siteId).ToList();
        }

    }
}

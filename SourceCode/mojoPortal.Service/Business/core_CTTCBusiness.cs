using mojoportal.Service.BaseBusines;
using mojoportal.Service.UoW;
using mojoPortal.Model.Data;
using System.Linq;

namespace mojoPortal.Service.Business
{
    public class core_CTTCBusiness : BaseBusiness<core_CCTC>
    {
        public core_CTTCBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public core_CCTC GetItem(int id = 0)
        {
            if (id > 0)
            {
                var search = this.Find(id);
                if(search != null)
                {
                    return search;
                }
            }
            return new core_CCTC();
        }
        public core_CCTC GetBySite(int siteId = 0)
        {
            var search = this.Filter(x => x.SiteID == siteId).FirstOrDefault();
            if (search != null) return search;
            return new core_CCTC();
        }
    }
}

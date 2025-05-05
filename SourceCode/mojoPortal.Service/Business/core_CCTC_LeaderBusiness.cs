using mojoportal.Service.BaseBusines;
using mojoportal.Service.UoW;
using mojoPortal.Model.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace mojoPortal.Service.Business
{
    public class core_CCTC_LeaderBusiness : BaseBusiness<core_CCTC_Leader>
    {
        public core_CCTC_LeaderBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public List<core_CCTC_Leader> GetListBy(int cctcId = 0)
        {
            if (cctcId > 0)
            {
                return this.context.core_CCTC_Leader.Where(x => x.CCTC_ID == cctcId).OrderBy(x => x.OrderBy).ToList();
            }
            return new List<core_CCTC_Leader>();
        }

        public List<SelectListItem> GetChildItem()
        {
            var list = this.context.core_CCTC_Leader.Where(x=> !string.IsNullOrEmpty(x.Title)).
                     Select(x => new SelectListItem 
                     {
                         Text = x.Title, 
                         Value = x.ItemID.ToString() 
                     }).ToList();
            return list;
        }
    }
}

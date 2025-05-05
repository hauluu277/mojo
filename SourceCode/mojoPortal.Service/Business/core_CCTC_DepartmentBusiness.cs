using mojoportal.Service.BaseBusines;
using mojoportal.Service.UoW;
using mojoPortal.Model.Data;
using System.Collections.Generic;
using System.Linq;

namespace mojoPortal.Service.Business
{
    public class core_CCTC_DepartmentBusiness : BaseBusiness<core_CCTC_Department>
    {
        public core_CCTC_DepartmentBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public List<core_CCTC_Department> GetListBy(int cctcId = 0)
        {
            if (cctcId > 0)
            {
                var List = this.context.core_CCTC_Department.Where(x => x.CCTC_ID == cctcId).OrderBy(x => x.OrderBy).ToList();
                return List;
            }
            return new List<core_CCTC_Department>();
        }
    }

}

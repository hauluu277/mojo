

using mojoportal.Service.BaseBusines;
using mojoportal.Service.UoW;
using mojoPortal.Model.Data;
using System.Collections.Generic;
using System.Linq;

namespace mojoPortal.Service.Business
{
    public class ThuTucThanhPhanBusiness : BaseBusiness<core_ThuTuc_ThanhPhanHS>
    {
        public ThuTucThanhPhanBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public List<core_ThuTuc_ThanhPhanHS> GetList(long idThucTuc)
        {
            return context.core_ThuTuc_ThanhPhanHS.Where(x => x.IdThuTuc == idThucTuc).ToList();
        }
    }
}


using mojoportal.Service.BaseBusines;
using mojoportal.Service.UoW;
using mojoPortal.Model.Data;
using System.Collections.Generic;
using System.Linq;

namespace mojoPortal.Service.Business
{
    public class ThuTucBieuMauBusiness : BaseBusiness<core_ThuTuc_BieuMau>
    {
        public ThuTucBieuMauBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public List<core_ThuTuc_BieuMau> GetList(long idThuTuc)
        {
            return context.core_ThuTuc_BieuMau.Where(x => x.IdThuTuc == idThuTuc).ToList();
        }
        public core_ThuTuc_BieuMau UpdateTotalDownload(long itemId)
        {
            var getData = Find(itemId);
            if (getData != null)
            {
                getData.TotalDownload = getData.TotalDownload + 1;
                Save(getData);
            }
            return getData;
        }
    }
}

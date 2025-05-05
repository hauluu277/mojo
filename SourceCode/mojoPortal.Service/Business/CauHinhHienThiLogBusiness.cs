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
    public class CauHinhHienThiLogBusiness : BaseBusiness<core_CauHinhHienThiLog>
    {
        public CauHinhHienThiLogBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public core_CauHinhHienThiLog GetByItemId(long itemId = 0)
        {
            if (itemId > 0)
            {
                var result = this.Find(itemId);
                if (result != null)
                {
                    return result;
                }
            }
            return new core_CauHinhHienThiLog();
        }
        public core_CauHinhHienThiLog GetByMaAndUser(string MaTruongHienThi, int IdUser)
        {
            return this.context.core_CauHinhHienThiLog.AsQueryable().Where(x => x.MaTruongHienThi == MaTruongHienThi && x.CreateByUser == IdUser).FirstOrDefault();
        }
    }
}

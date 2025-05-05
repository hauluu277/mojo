using mojoportal.Service.BaseBusines;
using mojoportal.Service.CommonBusiness;
using mojoportal.Service.UoW;
using mojoPortal.Model.Entities;
using System.Linq.Dynamic;
using mojoPortal.Service.CommonModel.BaoCao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mojoportal.CoreHelpers;
using PagedList;
using mojoPortal.Service.CommonModel.BieuMauThongTin;
using mojoPortal.Model.Data;

namespace mojoPortal.Service.Business
{
    public class KeKhaiBieuMauBusiness : BaseBusiness<bentre_KeKhaiBieuMau>
    {
        public KeKhaiBieuMauBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}

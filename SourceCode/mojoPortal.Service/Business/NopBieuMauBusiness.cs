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
using mojoPortal.Service.CommonModel.NopBieuMauThongTin;
using System.Data.Entity;

namespace mojoPortal.Service.Business
{
    public class NopBieuMauBusiness : BaseBusiness<bentre_NopBieuMau>
    {
        public NopBieuMauBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public PageListResultBO<NopBieuMauThongTinBO> GetDataByPage(NopBieuMauThongTinSearchBO searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = (from nopbieumau in this.context.bentre_NopBieuMau
                         join bieumau in this.context.bentre_BieuMauThongTin
                         on nopbieumau.IdBieuMauThongTin equals bieumau.Id
                         into groupBieuMau
                         from gBieuMau in groupBieuMau.DefaultIfEmpty()
                         select new NopBieuMauThongTinBO()
                         {
                             Id = nopbieumau.Id,
                             TenBieuMauThongTin = gBieuMau.Ten,
                             Hoten = nopbieumau.Hoten,
                             Email = nopbieumau.Email,
                             DienThoai = nopbieumau.DienThoai,
                             IdBieuMauThongTin = nopbieumau.IdBieuMauThongTin,
                             NgayNop = nopbieumau.NgayNop,
                         });

            if (searchModel != null)
            {
                if (searchModel.IdBieuMauFilter != null)
                {
                    query = query.Where(x => x.IdBieuMauThongTin == searchModel.IdBieuMauFilter);
                }

                if (searchModel.NgayNopStartFilter != null)
                {
                    query = query.Where(x => DbFunctions.TruncateTime(x.NgayNop) >= DbFunctions.TruncateTime(searchModel.NgayNopStartFilter));
                }

                if (searchModel.NgayNopEndFilter != null)
                {
                    query = query.Where(x => DbFunctions.TruncateTime(x.NgayNop) <= DbFunctions.TruncateTime(searchModel.NgayNopEndFilter));
                }
                if (!string.IsNullOrEmpty(searchModel.SortQuery))
                {
                    query = query.OrderBy(searchModel.SortQuery);
                }
                else
                {
                    query = query.OrderByDescending(x => x.Id);
                }
            }
            else
            {
                query = query.OrderByDescending(x => x.Id);
            }

            var resultModel = new PageListResultBO<NopBieuMauThongTinBO>();
            if (pageSize == -1)
            {
                var dataPageList = query.ToList();
                resultModel.Count = dataPageList.Count;
                resultModel.TotalPage = 1;
                resultModel.ListItem = dataPageList;

            }
            else
            {
                var dataPageList = query.ToPagedList(pageIndex, pageSize);
                resultModel.Count = dataPageList.TotalItemCount;
                resultModel.TotalPage = dataPageList.PageCount;
                resultModel.ListItem = dataPageList.ToList();
            }
            return resultModel;
        }

    }
}

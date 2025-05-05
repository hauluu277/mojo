using mojoportal.CoreHelpers;
using mojoportal.Service.BaseBusines;
using mojoportal.Service.CommonBusiness;
using mojoportal.Service.UoW;
using mojoPortal.Model.Data;
using mojoPortal.Model.Entities;
using mojoPortal.Service.CommonModel.GiaoDien;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace mojoPortal.Service.Business
{
    public class GiaoDienBusiness : BaseBusiness<core_GiaoDien>
    {
        public GiaoDienBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public PageListResultBO<GiaoDienBO> GetDaTaByPage(GiaoDienSearchBO searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var siteBusiness = new SiteBusiness(new UnitOfWork());
            var getSite = siteBusiness.Find(1);
            var giaoDienCurrent = string.Empty;
            if(getSite != null)
            {
                giaoDienCurrent = getSite.Skin;
            }

            var query = this.context.core_GiaoDien.AsQueryable();

            if (searchModel != null)
            {

                if (!string.IsNullOrEmpty(searchModel.TenGiaoDienFillter))
                {
                    query = query.Where(x => x.TenGiaoDien.Contains(searchModel.TenGiaoDienFillter));
                }
                if (!string.IsNullOrEmpty(searchModel.MaGiaoDienFillter))
                {
                    query = query.Where(x => x.MaGiaoDien.Contains(searchModel.MaGiaoDienFillter));
                }

                if (!string.IsNullOrEmpty(searchModel.sortQuery))
                {
                    query = query.OrderBy(searchModel.sortQuery);
                }
                else
                {
                    query = query.OrderByDescending(x => x.ItemID);
                }
            }
            else
            {
                query = query.OrderByDescending(x => x.ItemID);
            }

            var resultmodel = new PageListResultBO<GiaoDienBO>();
            if (pageSize == -1)
            {
                var dataPageList = query.ToList();
                resultmodel.Count = dataPageList.Count;
                resultmodel.TotalPage = 1;
                resultmodel.ListItem = MapDataHelper<GiaoDienBO, core_GiaoDien>.MapDataList(dataPageList);

            }
            else
            {
                var dataPageList = query.ToPagedList(pageIndex, pageSize);
                resultmodel.Count = dataPageList.TotalItemCount;
                resultmodel.TotalPage = dataPageList.PageCount;
                resultmodel.ListItem = MapDataHelper<GiaoDienBO, core_GiaoDien>.MapDataList(dataPageList.ToList());
            }
            resultmodel.PageIndex = pageIndex;
            resultmodel.PageSize = pageSize;
            foreach (var item in resultmodel.ListItem)
            {
                if (item.MaGiaoDien.Equals(giaoDienCurrent))
                {
                    item.IsActiveGiaoDien = true;
                }
            }
            return resultmodel;
        }


        public core_GiaoDien GetByItemId(long itemId = 0)
        {
            if (itemId > 0)
            {
                var result = this.Find(itemId);
                if (result != null)
                {
                    return result;
                }
            }
            return new core_GiaoDien();
        }
        public bool CheckTrungMa(string MaGiaoDien)
        {
            return this.context.core_GiaoDien.AsQueryable().Any(x => x.MaGiaoDien == MaGiaoDien);
        }


    }
}
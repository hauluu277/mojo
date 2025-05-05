using mojoportal.Service.BaseBusines;
using mojoportal.Service.CommonBusiness;
using mojoportal.Service.UoW;
using mojoPortal.Model.Data;
using mojoPortal.Service.CommonModel.coreDanhMuc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using mojoportal.CoreHelpers;
using PagedList;

namespace mojoPortal.Service.Business
{
    public class core_DanhMucBusiness : BaseBusiness<core_DanhMuc>
    {
        public core_DanhMucBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public List<core_DanhMuc>GetTop(int top, int siteId)
        {
            return context.core_DanhMuc.Where(x => x.SiteID == siteId && x.IsPublish).OrderBy(x => x.OrderBy).Take(top).ToList();
        }

        public PageListResultBO<core_DanhMucBO> GetDaTaByPage(core_DanhMucSearchBO searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = this.context.core_DanhMuc.AsQueryable();

            var roleBusiness = new RoleBusiness(new UnitOfWork());

            if (searchModel != null)
            {

                if (!string.IsNullOrEmpty(searchModel.TitleFilter))
                {
                    query = query.Where(x => x.Title.Contains(searchModel.TitleFilter));
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

            var resultmodel = new PageListResultBO<core_DanhMucBO>();
            if (pageSize == -1)
            {
                var dataPageList = query.ToList();
                resultmodel.Count = dataPageList.Count;
                resultmodel.TotalPage = 1;
                resultmodel.ListItem = MapDataHelper<core_DanhMucBO, core_DanhMuc>.MapDataList(dataPageList);
            }
            else
            {
                var dataPageList = query.ToPagedList(pageIndex, pageSize);
                resultmodel.Count = dataPageList.TotalItemCount;
                resultmodel.TotalPage = dataPageList.PageCount;
                resultmodel.ListItem = MapDataHelper<core_DanhMucBO, core_DanhMuc>.MapDataList(dataPageList.ToList());
            }
            resultmodel.PageIndex = pageIndex;
            resultmodel.PageSize = pageSize;
            var clientCategory = new core_ClientCategoryBusiness(new UnitOfWork());
            return resultmodel;
        }

    }
}

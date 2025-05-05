using mojoportal.CoreHelpers;
using mojoportal.Service.BaseBusines;
using mojoportal.Service.CommonBusiness;
using mojoportal.Service.UoW;
using mojoPortal.Model.Data;
using mojoPortal.Service.CommonModel.QLLog;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Service.Business
{
    public class QLLogBusiness : BaseBusiness<core_LogHeThong>
    {
        public QLLogBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public PageListResultBO<QLLogBO> GetDaTaByPage(QLLogSearchBO searchModel, int pageIndex = 1, int pageSize = 20)
        {

            var query = this.context.core_LogHeThong.AsQueryable();

            if (searchModel != null)
            {

                if (!string.IsNullOrEmpty(searchModel.TypeFilter))
                {
                    query = query.Where(x => x.Type.Contains(searchModel.TypeFilter));
                }
                if (!string.IsNullOrEmpty(searchModel.HanhDongThaoTacFilter))
                {
                    query = query.Where(x => x.HanhDongThaoTac.Contains(searchModel.HanhDongThaoTacFilter));
                }

                query = query.OrderByDescending(x => x.ItemID);
            }
            else
            {
                query = query.OrderByDescending(x => x.ItemID);
            }

            var resultmodel = new PageListResultBO<QLLogBO>();
            if (pageSize == -1)
            {
                var dataPageList = query.ToList();
                resultmodel.Count = dataPageList.Count;
                resultmodel.TotalPage = 1;
                resultmodel.ListItem = MapDataHelper<QLLogBO, core_LogHeThong>.MapDataList(dataPageList);

            }
            else
            {
                var dataPageList = query.ToPagedList(pageIndex, pageSize);
                resultmodel.Count = dataPageList.TotalItemCount;
                resultmodel.TotalPage = dataPageList.PageCount;
                resultmodel.ListItem = MapDataHelper<QLLogBO, core_LogHeThong>.MapDataList(dataPageList.ToList());
            }
            resultmodel.PageIndex = pageIndex;
            resultmodel.PageSize = pageSize;
            return resultmodel;
        }

        public core_LogHeThong GetByItemId(long itemId = 0)
        {
            if (itemId > 0)
            {
                var result = this.Find(itemId);
                if (result != null)
                {
                    return result;
                }
            }
            return new core_LogHeThong();
        }
    }
}

using mojoportal.CoreHelpers;
using mojoportal.Service.BaseBusines;
using mojoportal.Service.CommonBusiness;
using mojoportal.Service.UoW;
using mojoPortal.Model.Data;
using mojoPortal.Service.CommonModel.ThuTuc;
using PagedList;
using System.Linq;
using System.Linq.Dynamic;

namespace mojoPortal.Service.Business
{
    public class ThuTucBusiness : BaseBusiness<core_ThuTuc>
    {
        public ThuTucBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public PageListResultBO<ThuTucBO> GetDaTaByPage(ThuTucSearchBO searchModel, int pageIndex = 1, int pageSize = 20)
        {

            var query = this.context.core_ThuTuc.AsQueryable();

            if (searchModel != null)
            {
                if (!string.IsNullOrEmpty(searchModel.KeywordFillter))
                {
                    query = query.Where(x => x.MaThuTuc.Contains(searchModel.KeywordFillter) || x.TenThuTuc.Contains(searchModel.KeywordFillter));
                }
                if (searchModel.IdLinhVucFillter.HasValue)
                {
                    query = query.Where(x => x.IdLinhVuc == searchModel.IdLinhVucFillter);
                }
                if (searchModel.IdMucDoDVCFillter.HasValue)
                {
                    query = query.Where(x => x.IdMucDo == searchModel.IdMucDoDVCFillter);
                }
                if (searchModel.IdCachThucThucHienFilter.HasValue)
                {
                    query = query.Where(x => x.CachThucThucHien.Contains("," + searchModel.IdCachThucThucHienFilter + ","));
                }

                if (searchModel.IdDoiTuongThucHien.HasValue)
                {
                    query = query.Where(x => x.IdDoiTuongThucHien == searchModel.IdDoiTuongThucHien);
                }

                if (searchModel.IdCoQuanFilter.HasValue)
                {
                    query = query.Where(x => x.IdCoQuan == searchModel.IdCoQuanFilter);
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

            var resultmodel = new PageListResultBO<ThuTucBO>();
            if (pageSize == -1)
            {
                var dataPageList = query.ToList();
                resultmodel.Count = dataPageList.Count;
                resultmodel.TotalPage = 1;
                resultmodel.ListItem = MapDataHelper<ThuTucBO, core_ThuTuc>.MapDataList(dataPageList);

            }
            else
            {
                var dataPageList = query.ToPagedList(pageIndex, pageSize);
                resultmodel.Count = dataPageList.TotalItemCount;
                resultmodel.TotalPage = dataPageList.PageCount;
                resultmodel.ListItem = MapDataHelper<ThuTucBO, core_ThuTuc>.MapDataList(dataPageList.ToList());
            }
            resultmodel.PageIndex = pageIndex;
            resultmodel.PageSize = pageSize;

            CategoryBusiness categoryBusiness = new CategoryBusiness(new UnitOfWork());
            foreach (var item in resultmodel.ListItem)
            {
                if (item.IdMucDo.HasValue)
                {
                    item.TenMucDo = categoryBusiness.GetName(item.IdMucDo.Value);
                }
                if (item.IdLinhVuc.HasValue)
                {
                    item.TenLinhVuc = categoryBusiness.GetName(item.IdLinhVuc.Value);
                }
            }

            return resultmodel;
        }


        public core_ThuTuc GetByItemId(long itemId = 0)
        {
            if (itemId > 0)
            {
                var result = this.Find(itemId);
                if (result != null)
                {
                    return result;
                }
            }
            return new core_ThuTuc();
        }

        public int GetCount(int idLinhVuc, bool getForParent = false)
        {
            if (getForParent)
            {
                var listChild = context.core_Category.Where(x => x.ItemID == idLinhVuc || x.ParentID == idLinhVuc).Select(x => x.ItemID).ToList();
                return context.core_ThuTuc.Where(x => x.IdLinhVuc.HasValue && listChild.Contains(x.IdLinhVuc.Value)).Count();
            }
            return context.core_ThuTuc.Where(x => x.IdLinhVuc == idLinhVuc).Count();
        }
    }
}
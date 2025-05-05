using mojoportal.CoreHelpers;
using mojoportal.Service.BaseBusines;
using mojoportal.Service.CommonBusiness;
using mojoportal.Service.UoW;
using mojoPortal.Model.Data;
using mojoPortal.Service.CommonModel.BaoCao;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace mojoPortal.Service.Business
{
    public class BaoCaoBusiness : BaseBusiness<md_BaoCao>
    {
        public BaoCaoBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public PageListResultBO<BaoCaoBO> GetDaTaByPage(BaoCaoSearchBO searchModel, int pageIndex = 1, int pageSize = 20)
        {

            var query = this.context.md_BaoCao.AsQueryable();

            if (searchModel != null)
            {
                if (searchModel.QR_LINHVUC.HasValue)
                {
                    query = query.Where(x => x.LinhVucID == searchModel.QR_LINHVUC);
                }
                if (searchModel.QR_NAM_CHUKYBAOCAO.HasValue)
                {
                    query = query.Where(x => x.NamChuKyBaoCao == searchModel.QR_NAM_CHUKYBAOCAO);
                }
                if (!string.IsNullOrEmpty(searchModel.QR_SOQD_CONGBO))
                {
                    query = query.Where(x => x.SoQuyetDinhCongBo.Contains(searchModel.QR_SOQD_CONGBO));
                }
                if (!string.IsNullOrEmpty(searchModel.QR_TEN_BAOCAO))
                {
                    query = query.Where(x => x.TenBaoCao.Contains(searchModel.QR_TEN_BAOCAO));
                }
                if (!string.IsNullOrEmpty(searchModel.sortQuery))
                {
                    query = query.OrderBy(searchModel.sortQuery);
                }
                else
                {
                    query = query.OrderByDescending(x => x.NamChuKyBaoCao).OrderByDescending(x => x.ItemID);
                }
            }
            else
            {
                query = query.OrderByDescending(x => x.NamChuKyBaoCao).OrderByDescending(x => x.ItemID);
            }

            var resultmodel = new PageListResultBO<BaoCaoBO>();
            if (pageSize == -1)
            {
                var dataPageList = query.ToList();
                resultmodel.Count = dataPageList.Count;
                resultmodel.TotalPage = 1;
                resultmodel.ListItem = MapDataHelper<BaoCaoBO, md_BaoCao>.MapDataList(dataPageList);

            }
            else
            {
                var dataPageList = query.ToPagedList(pageIndex, pageSize);
                resultmodel.Count = dataPageList.TotalItemCount;
                resultmodel.TotalPage = dataPageList.PageCount;
                resultmodel.ListItem = MapDataHelper<BaoCaoBO, md_BaoCao>.MapDataList(dataPageList.ToList());
            }
            resultmodel.PageIndex = pageIndex;
            resultmodel.PageSize = pageSize;
            foreach (var item in resultmodel.ListItem)
            {
                item.NgayCongBoString = string.Format("{0:dd/MM/yyyy}", item.NgayCongBo);
            }
            return resultmodel;
        }


        public md_BaoCao GetByItemId(int itemId = 0)
        {
            if (itemId > 0)
            {
                var result = this.Find(itemId);
                if (result != null)
                {
                    return result;
                }
            }
            return new md_BaoCao();
        }


        public List<BaoCaoBO> ListByLinhVuc(int categoryId)
        {
            var query = this.context.md_BaoCao.Where(x => x.LinhVucID == categoryId).OrderByDescending(x => x.NamChuKyBaoCao).Take(5).ToList();
            if (query != null && query.Any())
            {
                var CategoryBusiness = new CategoryBusiness(new UnitOfWork());
                var result = MapDataHelper<BaoCaoBO, md_BaoCao>.MapDataList(query);
                foreach (var item in result)
                {
                    item.TenLinhVuc = CategoryBusiness.GetName(item.LinhVucID.GetValueOrDefault(0));
                }
                return result;
            }

            return new List<BaoCaoBO>();
        }

        public List<md_BaoCao> ListBaoCao(int year, int? idLinhVuc = 0)
        {

            var query = context.md_BaoCao.Where(x => x.NamChuKyBaoCao == year);
            if (idLinhVuc.HasValue && idLinhVuc > 0)
            {
                query = query.Where(x => x.LinhVucID == idLinhVuc);
            }
            return query.OrderByDescending(x => x.NamChuKyBaoCao).OrderByDescending(x => x.ItemID).ToList();
        }
        public List<md_BaoCao> ListBaoCaoPublish(int year, int? idLinhVuc = 0)
        {

            var query = context.md_BaoCao.Where(x => x.IsPublish == true && x.NamChuKyBaoCao == year);
            if (idLinhVuc.HasValue && idLinhVuc > 0)
            {
                query = query.Where(x => x.LinhVucID == idLinhVuc);
            }
            return query.OrderByDescending(x => x.NamChuKyBaoCao).OrderByDescending(x => x.ItemID).ToList();
        }
    }
}

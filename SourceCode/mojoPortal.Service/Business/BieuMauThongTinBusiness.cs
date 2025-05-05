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
    public class BieuMauThongTinBusiness : BaseBusiness<bentre_BieuMauThongTin>
    {
        public BieuMauThongTinBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public PageListResultBO<BieuMauThongTinBO> GetDaTaByPage(BieuMauThongTinSearchBO searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = (from bieumau in this.context.bentre_BieuMauThongTin

                         select new BieuMauThongTinBO()
                         {
                             Id = bieumau.Id,
                             Ten = bieumau.Ten,
                             NgayTao = bieumau.NgayTao,
                             NgayCapNhat = bieumau.NgayCapNhat,
                             IsShow = bieumau.IsShow
                         });

            if (searchModel != null)
            {
                if (!string.IsNullOrEmpty(searchModel.TenBieuMauFilter))
                {
                    searchModel.TenBieuMauFilter = searchModel.TenBieuMauFilter.Trim().ToLower();
                    query = query.Where(x => x.Ten.Trim().ToLower().Contains(searchModel.TenBieuMauFilter));
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

            var resultModel = new PageListResultBO<BieuMauThongTinBO>();
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


        public PageListResultBO<BieuMauThongTinBO> GetDaTaByPageForGuest(BieuMauThongTinSearchBO searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = (from bieumau in this.context.bentre_BieuMauThongTin
                         where bieumau.IsShow == true
                         select new BieuMauThongTinBO()
                         {
                             Id = bieumau.Id,
                             Ten = bieumau.Ten,
                             NgayTao = bieumau.NgayTao,
                             NgayCapNhat = bieumau.NgayCapNhat,
                         });

            if (searchModel != null)
            {
                if (!string.IsNullOrEmpty(searchModel.TenBieuMauFilter))
                {
                    searchModel.TenBieuMauFilter = searchModel.TenBieuMauFilter.Trim().ToLower();
                    query = query.Where(x => x.Ten.Trim().ToLower().Contains(searchModel.TenBieuMauFilter));
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

            var resultModel = new PageListResultBO<BieuMauThongTinBO>();
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

        //public md_BaoCao GetByItemId(int itemId = 0)
        //{
        //    if (itemId > 0)
        //    {
        //        var result = this.Find(itemId);
        //        if (result != null)
        //        {
        //            return result;
        //        }
        //    }
        //    return new md_BaoCao();
        //}


        //public List<BaoCaoBO> ListByLinhVuc(int categoryId)
        //{
        //    var query = this.context.md_BaoCao.Where(x => x.LinhVucID == categoryId).OrderByDescending(x => x.NamChuKyBaoCao).Take(5).ToList();
        //    if (query != null && query.Any())
        //    {
        //        var CategoryBusiness = new CategoryBusiness(new UnitOfWork());
        //        var result = MapDataHelper<BaoCaoBO, md_BaoCao>.MapDataList(query);
        //        foreach (var item in result)
        //        {
        //            item.TenLinhVuc = CategoryBusiness.GetName(item.LinhVucID.GetValueOrDefault(0));
        //        }
        //        return result;
        //    }

        //    return new List<BaoCaoBO>();
        //}

        //public List<md_BaoCao> ListBaoCao(int year,int? idLinhVuc=0)
        //{

        //    var query= context.md_BaoCao.Where(x => x.NamChuKyBaoCao == year);
        //    if(idLinhVuc.HasValue && idLinhVuc > 0)
        //    {
        //        query = query.Where(x => x.LinhVucID == idLinhVuc);
        //    }
        //    return query.ToList();
        //}
    }
}

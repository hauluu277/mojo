using mojoportal.CoreHelpers;
using mojoportal.Service.BaseBusines;
using mojoportal.Service.CommonBusiness;
using mojoportal.Service.UoW;
using mojoPortal.Model.Data;
using mojoPortal.Service.CommonModel.BaoCao;
using mojoPortal.Service.CommonModel.client;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace mojoPortal.Service.Business
{
    public class core_ClientBusiness : BaseBusiness<core_Client>
    {
        public core_ClientBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public List<GroupClientBO> GetPageClientGroup(ClientSearchBO searchModel)
        {
            var result = new List<GroupClientBO>();

            var cateRootNhomThanhVien = this.context.core_Category.Where(x => x.Code == "DM_CONGTHANHVIEN").FirstOrDefault();

            if (cateRootNhomThanhVien != null)
            {
                var listGroup = this.context.core_Category.Where(x => x.ParentID == cateRootNhomThanhVien.ItemID).ToList();
                if (searchModel != null)
                {
                    if (searchModel.groupId > 0)
                    {
                        listGroup = listGroup.Where(x => x.ItemID == searchModel.groupId).ToList();
                    }
                }
                foreach (var item in listGroup)
                {
                    var listclient = this.context.core_Client.Where(x => x.IdNhomCongThanhVien == item.ItemID).ToList();
                    result.Add(new GroupClientBO
                    {
                        GroupName = item.Name,
                        listClients = listclient
                    });
                }
            }



            return result;
        }

        public PageListResultBO<ClientBO> GetPageClient(ClientSearchBO searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = this.context.core_Client.AsQueryable();

            var roleBusiness = new RoleBusiness(new UnitOfWork());

            if (searchModel != null)
            {

                if (!string.IsNullOrEmpty(searchModel.QR_ClientName))
                {
                    query = query.Where(x => x.ClientName.Contains(searchModel.QR_ClientName));
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

            var resultmodel = new PageListResultBO<ClientBO>();
            if (pageSize == -1)
            {
                var dataPageList = query.ToList();
                resultmodel.Count = dataPageList.Count;
                resultmodel.TotalPage = 1;
                resultmodel.ListItem = MapDataHelper<ClientBO, core_Client>.MapDataList(dataPageList);
            }
            else
            {
                var dataPageList = query.ToPagedList(pageIndex, pageSize);
                resultmodel.Count = dataPageList.TotalItemCount;
                resultmodel.TotalPage = dataPageList.PageCount;
                resultmodel.ListItem = MapDataHelper<ClientBO, core_Client>.MapDataList(dataPageList.ToList());
            }
            resultmodel.PageIndex = pageIndex;
            resultmodel.PageSize = pageSize;
            var clientCategory = new core_ClientCategoryBusiness(new UnitOfWork());
            foreach (var item in resultmodel.ListItem)
            {
                item.CategoryName = clientCategory.GetNameCategory(item.ItemID);
            }
            return resultmodel;
        }

        public List<core_Client> GetListExcept(string clientId)
        {
            return repository.GetAllAsQueryable().Where(x => x.ClientID.Equals(clientId) == false).ToList();
        }
        public List<core_Client> GetListClient(string clientId)
        {
            return repository.GetAllAsQueryable().Where(x => x.ClientID == clientId).ToList();
        }
        public List<core_Client> GetAll()
        {
            return repository.GetAllAsQueryable().ToList();
        }
        public core_Client GetFirst(string clientId)
        {
            return repository.GetAllAsQueryable().FirstOrDefault(x => x.ClientID.Equals(clientId));
        }
        public List<BaoCaoArticleDonViBO> GetDataArticleDonVi(DateTime? startDate, DateTime? endDate)
        {
            var result = new List<BaoCaoArticleDonViBO>();
            var queryArticle = context.md_Articles.Where(x => x.IsCongThanhVien && !string.IsNullOrEmpty(x.ClientId)).AsQueryable();
            if (startDate.HasValue)
            {
                queryArticle = queryArticle.Where(x => x.CreatedDate.HasValue && x.CreatedDate >= startDate);
            }
            if (endDate.HasValue)
            {
                queryArticle = queryArticle.Where(x => x.CreatedDate.HasValue && x.CreatedDate <= endDate);
            }

            var listClient = context.core_Client.ToList();
            foreach (var item in listClient)
            {
                result.Add(new BaoCaoArticleDonViBO()
                {
                    MaDonVi = item.ClientID,
                    TenDonVi = item.ClientName,
                    SoLuongArticle = queryArticle.Count(x => x.ClientId == item.ClientID)
                });
            }

            return result;
        }
    }
}

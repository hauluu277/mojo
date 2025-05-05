using mojoportal.CoreHelpers;
using mojoportal.Service.BaseBusines;
using mojoportal.Service.CommonBusiness;
using mojoportal.Service.UoW;

using mojoPortal.Model.Data;
using mojoPortal.Service.CommonModel.QLLog;
using mojoPortal.Service.CommonModel.User;
using PagedList;
using System.Collections.Generic;
using System.Linq;

namespace mojoportal.Service.Business
{
    public class UserBusiness : BaseBusiness<mp_Users>
    {
        public UserBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public List<mp_Users> GetBySite(int siteId)
        {
            return this.GetAllAsQueryable().Where(x => x.SiteID == siteId).ToList();
        }

        public List<mp_Users> GetUserNotInDanhMucTinTuc(int IdCate)
        {
            var listNguoiDunginDanhMuc = this.context.core_CategoryUserArticle.Where(x => x.CategoryID == IdCate).Select(x => x.UserID).ToList();

            return this.GetAllAsQueryable().Where(x => !listNguoiDunginDanhMuc.Contains(x.UserID)).ToList();
        }
        public List<mp_Users> GetUserHasInDanhMucTinTuc(int IdCate)
        {
            var listNguoiDunginDanhMuc = this.context.core_CategoryUserArticle.Where(x => x.CategoryID == IdCate).Select(x => x.UserID).ToList();

            return this.GetAllAsQueryable().Where(x => listNguoiDunginDanhMuc.Contains(x.UserID)).ToList();
        }

        public PageListResultBO<mp_Users> GetDataByPage( UserSearchBO searchModel,int pageIndex = 1, int pageSize = 20)
        {
            var query = this.context.mp_Users.AsQueryable();

            if(searchModel != null)
            {
                if(!string.IsNullOrEmpty(searchModel.HoTenFilter))
                {
                    query = query.Where(x => x.Name.Equals(searchModel.HoTenFilter));
                }

                if(!string.IsNullOrEmpty(searchModel.EmailFilter))
                {
                    query = query.Where(x => x.Email.Equals(searchModel.EmailFilter));
                }

                query = query.OrderByDescending(x => x.UserID);
            } else
            {
                query = query.OrderByDescending(x => x.UserID);
            }

            var resultmodel = new PageListResultBO<mp_Users>();

            if(pageSize == -1)
            {
                var dataPageList = query.ToList();
                resultmodel.Count = dataPageList.Count;
                resultmodel.TotalPage = 1;
                resultmodel.ListItem = dataPageList;
            } else
            {
                var dataPageList = query.ToPagedList(pageIndex, pageSize);
                resultmodel.Count = dataPageList.TotalItemCount;
                resultmodel.TotalPage = dataPageList.PageCount;
                resultmodel.ListItem = dataPageList.ToList();
            }
            resultmodel.PageIndex = pageIndex;
            resultmodel.PageSize = pageSize;

            return resultmodel;
        }
    }
}

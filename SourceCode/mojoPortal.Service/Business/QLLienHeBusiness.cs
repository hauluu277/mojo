using mojoPortal.Model.Data;
using mojoportal.Service.BaseBusines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mojoportal.Service.UoW;
using mojoportal.CoreHelpers;
using PagedList;
using mojoPortal.Service.CommonModel.QLLienHe;
using mojoportal.Service.CommonBusiness;

namespace mojoPortal.Service.Business
{
    public class QLLienHeBusiness : BaseBusiness<mp_ContactFormMessage>
    {
        public QLLienHeBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public PageListResultBO<QLLienHeBO> GetDaTaByPage(QLLienHeSearchBO searchModel, int pageIndex = 1, int pageSize = 20)
        {

            var query = this.context.mp_ContactFormMessage.AsQueryable();

            if (searchModel != null)
            {

                if (!string.IsNullOrEmpty(searchModel.TenFilter))
                {
                    query = query.Where(x => x.Url.ToLower().Contains(searchModel.TenFilter.ToLower()));
                }
                if (!string.IsNullOrEmpty(searchModel.EmailFilter))
                {
                    query = query.Where(x => x.Email.Contains(searchModel.EmailFilter));
                }
                if (!string.IsNullOrEmpty(searchModel.SubjectFilter))
                {
                    query = query.Where(x => x.Subject.Contains(searchModel.SubjectFilter));
                }

                if (searchModel.TrangThaiPhanHoiFilter.HasValue)
                {
                    query = query.Where(x => x.TrangThai == searchModel.TrangThaiPhanHoiFilter);
                }
                query = query.OrderByDescending(x => x.CreatedUtc);
            }
            else
            {
                query = query.OrderByDescending(x => x.CreatedUtc);
            }

            var resultmodel = new PageListResultBO<QLLienHeBO>();
            if (pageSize == -1)
            {
                var dataPageList = query.ToList();
                resultmodel.Count = dataPageList.Count;
                resultmodel.TotalPage = 1;
                resultmodel.ListItem = MapDataHelper<QLLienHeBO, mp_ContactFormMessage>.MapDataList(dataPageList);

            }
            else
            {
                var dataPageList = query.ToPagedList(pageIndex, pageSize);
                resultmodel.Count = dataPageList.TotalItemCount;
                resultmodel.TotalPage = dataPageList.PageCount;
                resultmodel.ListItem = MapDataHelper<QLLienHeBO, mp_ContactFormMessage>.MapDataList(dataPageList.ToList());
            }
            resultmodel.PageIndex = pageIndex;
            resultmodel.PageSize = pageSize;
            foreach (var item in resultmodel.ListItem)
            {
                if (item.ThoiGianPhanHoi.HasValue)
                {
                    item.ThoiGianPhanHoi_text = string.Format("{0:dd/MM/yyyy HH:mm}", item.ThoiGianPhanHoi);
                }
            }
            return resultmodel;
        }

        public mp_ContactFormMessage GetById(Guid RowGuid)
        {
            var query = this.context.mp_ContactFormMessage.AsQueryable();
            var result = query.Where(x => x.RowGuid.Equals(RowGuid)).FirstOrDefault();
            return result;
        }
    }
}

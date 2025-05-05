using mojoportal.CoreHelpers;
using mojoportal.Service.BaseBusines;
using mojoportal.Service.CommonBusiness;
using mojoportal.Service.UoW;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Model.Data;
using mojoPortal.Service.CommonModel.LichCongTac;
using mojoPortal.Service.CommonModel.LichLamViec;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace mojoPortal.Service.Business
{
    public class LichLamViecBusiness : BaseBusiness<core_LichLamViec>
    {
        public LichLamViecBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public PageListResultBO<LichLamViecDto> GetDaTaByPage(LichLamViecSearchDto searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = from lichLamViectbl in context.core_LichLamViec
                        select lichLamViectbl;


            if (searchModel != null)
            {
                if (searchModel.IdLanhDaoFilter.HasValue)
                {
                    query = query.Where(x => x.ThanhPhanThamDu.Contains(searchModel.IdLanhDaoFilter.ToString()));
                }
                if (!string.IsNullOrEmpty(searchModel.NgayLamViecFilter))
                {
                    var _search = searchModel.NgayLamViecFilter.ToDateTimeV2();
                    query = query.Where(x => x.NgayLamViec == _search);
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



            var resultmodel = new PageListResultBO<LichLamViecDto>();
            if (pageSize == -1)
            {
                var dataPageList = query.ToList();
                resultmodel.Count = dataPageList.Count;
                resultmodel.TotalPage = 1;
                resultmodel.ListItem = MapDataHelper<LichLamViecDto, core_LichLamViec>.MapDataList(dataPageList);

            }
            else
            {
                var dataPageList = query.ToPagedList(pageIndex, pageSize);
                resultmodel.Count = dataPageList.TotalItemCount;
                resultmodel.TotalPage = dataPageList.PageCount;
                resultmodel.ListItem = MapDataHelper<LichLamViecDto, core_LichLamViec>.MapDataList(dataPageList.ToList());
            }
            resultmodel.PageIndex = pageIndex;
            resultmodel.PageSize = pageSize;
            var categoryBusiness = new CategoryBusiness(new UnitOfWork());
            foreach (var item in resultmodel.ListItem)
            {
                if (!String.IsNullOrEmpty(item.ThanhPhanThamDu))
                {
                    var array = item.ThanhPhanThamDu.Split(' ');
                    foreach (var tp in array)
                    {
                        item.ListThanhPhanThamDu.Add(categoryBusiness.GetName(tp.ToIntOrZero()));
                    }
                }
                if (item.ThoiGianLamViec.HasValue)
                {
                    item.ThoiGianLamViec_text = string.Format("{0:dd/MM/yyyy HH:mm}", item.ThoiGianLamViec);
                }
            }

            return resultmodel;
        }

        public LichLamViecDto GetDetail(long id)
        {
            var result = new LichLamViecDto();
            if (id > 0)
            {
                var getData = Find(id);
                if (getData != null)
                {
                    result = MapDataHelper<LichLamViecDto, core_LichLamViec>.MapData(getData);
                }
            }

            return result;
        }


        public ShowLichCongTacIndexDto ShowLichCongTac(DateTime? startDate = null, DateTime? endDate = null)
        {
            var result = new ShowLichCongTacIndexDto();
            var query = context.core_LichLamViec.AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(x => x.NgayLamViec >= startDate);
            }

            if (endDate.HasValue)
            {
                query = query.Where(x => x.NgayLamViec <= endDate);
            }

            result.NgayTrongTuan = this.LThu(startDate.HasValue ? startDate.Value : DateTime.Now);
            result.sDate = result.NgayTrongTuan.FirstOrDefault().dateTime;
            result.eDate = result.NgayTrongTuan.LastOrDefault().dateTime;

            var listLich = query.ToList();

            result.ListInforLich = MapDataHelper<LichLamViecDto, core_LichLamViec>.MapDataList(listLich);
            var categoryBusiness = new CategoryBusiness(new UnitOfWork());
            foreach (var item in result.ListInforLich)
            {
                if (!String.IsNullOrEmpty(item.ThanhPhanThamDu))
                {
                    var array = item.ThanhPhanThamDu.Split(' ');
                    foreach (var tp in array)
                    {
                        item.ListThanhPhanThamDu.Add(categoryBusiness.GetName(tp.ToIntOrZero()));
                    }
                }
            }
            return result;
        }


        public List<InforNgayTrongTuan> LThu(DateTime dt)
        {
            var monDay = WeeksExtension.GetDayOfWeek(dt, DayOfWeek.Monday);
            var sDay_New = new DateTime(monDay.Year, monDay.Month, monDay.Day, 0, 0, 0);
            return new List<InforNgayTrongTuan>()
            {
                new InforNgayTrongTuan() { DayOfWeek = DayOfWeek.Monday,TenHienThi = "Thứ Hai", dateTime = sDay_New},
                new InforNgayTrongTuan() { DayOfWeek = DayOfWeek.Tuesday,TenHienThi = "Thứ Ba", dateTime = sDay_New.AddDays(1)},
                new InforNgayTrongTuan() { DayOfWeek = DayOfWeek.Wednesday,TenHienThi = "Thứ Tư", dateTime = sDay_New.AddDays(2)},
                new InforNgayTrongTuan() { DayOfWeek = DayOfWeek.Thursday,TenHienThi = "Thứ Năm", dateTime = sDay_New.AddDays(3)},
                new InforNgayTrongTuan() { DayOfWeek = DayOfWeek.Friday,TenHienThi = "Thứ Sáu", dateTime = sDay_New.AddDays(4)},
                new InforNgayTrongTuan() { DayOfWeek = DayOfWeek.Saturday,TenHienThi = "Thứ Bảy", dateTime = sDay_New.AddDays(5)},
                new InforNgayTrongTuan() { DayOfWeek = DayOfWeek.Sunday,TenHienThi = "Chủ Nhật", dateTime = sDay_New.AddDays(6)}
            };
        }
    }
}

using mojoportal.CoreHelpers;
using mojoportal.CoreHelpers.Ultilities;
using mojoportal.Service.BaseBusines;
using mojoportal.Service.CommonBusiness;
using mojoportal.Service.UoW;
using mojoPortal.Model.Data;
using mojoPortal.Service.CommonBusiness;
using mojoPortal.Service.CommonModel.ThuTuc;
using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic;

namespace mojoPortal.Service.Business
{
    public class ThongKeTruyCapBusiness : BaseBusiness<core_ThongKeTruyCap>
    {
        public ThongKeTruyCapBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }


        public int GetThongKeTruyCapByType(string type)
        {
            if (type == ThongKeTruyCapConstant.TrongNgay)
            {
                var currentDay = DateTime.Now.ToString("dd/MM/yyyy").ToDateTime();
                return this.context.core_ThongKeTruyCap.Where(x => x.Type == type && x.CurrentDay == currentDay).Select(x => x.Total).FirstOrDefault().GetValueOrDefault(0);
            }
            if (type == ThongKeTruyCapConstant.TrongTuan)
            {
                CultureInfo currentCulture = CultureInfo.CurrentCulture;
                //lấy tuần hiện tại của năm
                var weekNum = currentCulture.Calendar.GetWeekOfYear(
                  DateTime.Now,
                  CalendarWeekRule.FirstDay,
                  DayOfWeek.Monday);

                var currentDay = DateTime.Now.ToString("dd/MM/yyyy").ToDateTime();
                return this.context.core_ThongKeTruyCap.Where(x => x.Type == type && x.CurrentWeek == weekNum && x.Year == currentDay.Year).Select(x => x.Total).FirstOrDefault().GetValueOrDefault(0);
            }
            if (type == ThongKeTruyCapConstant.TrongThang)
            {
                var currentDay = DateTime.Now.ToString("dd/MM/yyyy").ToDateTime();
                return this.context.core_ThongKeTruyCap.Where(x => x.Type == type && x.CurrentMonth == currentDay.Month && x.Year == currentDay.Year).Select(x => x.Total).FirstOrDefault().GetValueOrDefault(0);
            }
            if (type == ThongKeTruyCapConstant.Tong)
            {
                var currentDay = DateTime.Now.ToString("dd/MM/yyyy").ToDateTime();
                return this.context.core_ThongKeTruyCap.Where(x => x.Type == type).Select(x => x.Total).FirstOrDefault().GetValueOrDefault(0);
            }
            return 0;
        }



        public PageListResultBO<ThuTucBO> GetDaTaByPage(ThuTucSearchBO searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var query = this.context.core_ThuTuc.AsQueryable();

            if (searchModel != null)
            {

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

            return resultmodel;

        }


        public void AutoAddVisit()
        {
             var currentDay = DateTime.Now.ToString("dd/MM/yyyy").ToDateTime();

            var getDay = this.context.core_ThongKeTruyCap.FirstOrDefault(x => x.Type == ThongKeTruyCapConstant.TrongNgay && x.CurrentDay == currentDay);
            // Cập nhật dữ liệu trong result theo logic của TrongNgay
            if (getDay == null)
            {
                //tạo mới bản ghi đếm lượt truy cập cho ngày hiện tại
                getDay = new core_ThongKeTruyCap();
                getDay.Type = ThongKeTruyCapConstant.TrongNgay;
                getDay.CurrentDay = currentDay;
                getDay.DateAdd = DateTime.Now;
                getDay.Total = 1;
                getDay.Year = DateTime.Now.Year;
                this.Save(getDay);
            }
            else
            {
                getDay.Total = getDay.Total + 1;
                this.Save(getDay);
            }

            //kiểm tra đã có lưu số lượt truy cập của tuần hiện tại
            CultureInfo currentCulture = CultureInfo.CurrentCulture;

            //lấy tháng hiện tại của năm
            var currentTime = DateTime.Now;
            var getMonth = context.core_ThongKeTruyCap.Where(x => x.Type == ThongKeTruyCapConstant.TrongThang && x.CurrentMonth == currentTime.Month && x.Year == currentTime.Year).FirstOrDefault();
            if (getMonth == null)
            {
                //tạo mới bản ghi đếm lượt truy cập cho thang hiện tại hiện tại
                getMonth = new core_ThongKeTruyCap();
                getMonth.Type = ThongKeTruyCapConstant.TrongThang;
                getMonth.CurrentMonth = currentTime.Month;
                getMonth.DateAdd = DateTime.Now;
                getMonth.Total = 1;
                getMonth.Year = DateTime.Now.Year;
                this.Save(getMonth);
            }
            else
            {
                getMonth.Total = getMonth.Total + 1;
                this.Save(getMonth);
            }


            //lấy tuần hiện tại của năm
            var weekNum = currentCulture.Calendar.GetWeekOfYear(
              DateTime.Now,
              CalendarWeekRule.FirstDay,
              DayOfWeek.Monday);

            var getWeek = this.context.core_ThongKeTruyCap.FirstOrDefault(x => x.Type == ThongKeTruyCapConstant.TrongTuan && x.CurrentWeek == weekNum && x.Year == DateTime.Now.Year);

            if (getWeek == null)
            {
                //tạo mới bản ghi đếm lượt truy cập cho tuần hiện tại hiện tại
                getWeek = new core_ThongKeTruyCap();
                getWeek.Type = ThongKeTruyCapConstant.TrongTuan;
                getWeek.CurrentWeek = weekNum;
                getWeek.DateAdd = DateTime.Now;
                getWeek.Total = 1;
                getWeek.Year = DateTime.Now.Year;
                this.Save(getWeek);

            }
            else
            {
                getWeek.Total = getWeek.Total + 1;
                this.Save(getWeek);
            }


            //kiểm tra đã có lưu tổng số số lượt truy cập 
            var getTotal = this.context.core_ThongKeTruyCap.FirstOrDefault(x => x.Type == ThongKeTruyCapConstant.Tong);
            if (getTotal == null)
            {
                //tạo mới bản ghi đếm tổng lượt truy cập
                getTotal = new core_ThongKeTruyCap();
                getTotal.Type = ThongKeTruyCapConstant.Tong;
                //getTotal.CurrentWeek = weekNum;
                getTotal.DateAdd = DateTime.Now;
                getTotal.Total = 1;
                this.Save(getTotal);

            }
            else
            {
                getTotal.Total = getTotal.Total + 1;

                this.Save(getTotal);
            }

        }

        /// <summary>
        /// Ngày, tuần, tháng, năm
        /// </summary>
        /// <returns></returns>
        public Tuple<int, int, int, int> GetViSit()
        {

            int TrongNgayData = GetThongKeTruyCapByType(ThongKeTruyCapConstant.TrongNgay);
            int TrongTuanData = GetThongKeTruyCapByType(ThongKeTruyCapConstant.TrongTuan);
            int TongData = GetThongKeTruyCapByType(ThongKeTruyCapConstant.Tong);

            int TongThangData = GetThongKeTruyCapByType(ThongKeTruyCapConstant.TrongThang);

            return Tuple.Create(TrongNgayData, TrongTuanData, TongThangData, TongData);

        }

        public core_ThongKeTruyCap GetByType(string type)
        {
            return this.context.core_ThongKeTruyCap.Where(x => x.Type.Equals(type)).OrderByDescending(x => x.ItemID).FirstOrDefault();
        }

    }
}



































//    public class ThongKeTruyCapBusiness : BaseBusiness<core_ThongKeTruyCap>
//    {

//        private string connectionString;

//        public ThongKeTruyCapBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
//        {

//        }

//        public PageListResultBO<ThuTucBO> GetDaTaByPage(ThuTucSearchBO searchModel, int pageIndex = 1, int pageSize = 20)
//        {

//            var query = this.context.core_ThuTuc.AsQueryable();

//            if (searchModel != null)
//            {

//                if (!string.IsNullOrEmpty(searchModel.sortQuery))
//                {
//                    query = query.OrderBy(searchModel.sortQuery);
//                }
//                else
//                {
//                    query = query.OrderByDescending(x => x.ItemID);
//                }
//            }
//            else
//            {
//                query = query.OrderByDescending(x => x.ItemID);
//            }

//            var resultmodel = new PageListResultBO<ThuTucBO>();
//            if (pageSize == -1)
//            {
//                var dataPageList = query.ToList();
//                resultmodel.Count = dataPageList.Count;
//                resultmodel.TotalPage = 1;
//                resultmodel.ListItem = MapDataHelper<ThuTucBO, core_ThuTuc>.MapDataList(dataPageList);

//            }
//            else
//            {
//                var dataPageList = query.ToPagedList(pageIndex, pageSize);
//                resultmodel.Count = dataPageList.TotalItemCount;
//                resultmodel.TotalPage = dataPageList.PageCount;
//                resultmodel.ListItem = MapDataHelper<ThuTucBO, core_ThuTuc>.MapDataList(dataPageList.ToList());
//            }
//            resultmodel.PageIndex = pageIndex;
//            resultmodel.PageSize = pageSize;

//            return resultmodel;
//        }      

//public core_ThongKeTruyCap GetByType(string type)
//{
//    var result = new core_ThongKeTruyCap();
//    if (type == ThongKeTruyCapConstant.TrongNgay)
//    {
//        result = this.context.core_ThongKeTruyCap.FirstOrDefault(x => x.TrongNgay == true);


//    }
//    else if (type == ThongKeTruyCapConstant.TrongTuan)
//    {
//        result = this.context.core_ThongKeTruyCap.FirstOrDefault(x => x.TrongTuan == true);


//    }
//    else if (type == ThongKeTruyCapConstant.Tong)
//    {
//        result = this.context.core_ThongKeTruyCap.FirstOrDefault(x => x.Tong == true);

//    }
//    return result;
//}
//    }

//}























//public core_ThongKeTruyCap GetByType(string type)
//{
//    var result = new core_ThongKeTruyCap();

//    using (SqlConnection connection = new SqlConnection(connectionString))
//    {
//        connection.Open();
//        if (type == ThongKeTruyCapConstant.TrongNgay)
//        {
//            DateTime today = DateTime.Today;
//            string query = "SELECT CurrentDay FROM ThongKeTruyCap WHERE Type = @Type AND CurrentDay = @CurrentDay";
//            using (SqlCommand command = new SqlCommand(query, connection))
//            {
//                command.Parameters.AddWithValue("@Type", type);
//                command.Parameters.AddWithValue("@CurrentDay", today);

//                using (SqlDataReader reader = command.ExecuteReader())
//                {
//                    if (reader.Read())
//                    {
//                        result.CurrentWeek = reader.GetInt32(1);
//                    }
//                }
//            }
//            if (result.CurrentDay != null)
//            {
//                string updateQuery = "UPDATE ThongKeTruyCap SET CurrentDay = CurrentDay + 1 WHERE Type = @Type AND CurrentDay = @CurrentDay";
//                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
//                {
//                    updateCommand.Parameters.AddWithValue("@Type", type);
//                    updateCommand.Parameters.AddWithValue("@CurrentDay", today);
//                    updateCommand.ExecuteNonQuery();
//                }
//            }
//            else
//            {
//                string insertQuery = "INSERT INTO ThongKeTruyCap (Type, CurrentDay) VALUES (@Type, 1)";
//                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
//                {
//                    insertCommand.Parameters.AddWithValue("@Type", type);
//                    insertCommand.ExecuteNonQuery();
//                }
//            }
//        }
//        else if (type == ThongKeTruyCapConstant.TrongTuan)
//        {
//            DateTime today = DateTime.Today;
//            DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek);
//            DateTime endOfWeek = startOfWeek.AddDays(6);

//            string query = "SELECT CurrentWeek FROM ThongKeTruyCap WHERE Type = @Type";
//            using (SqlCommand command = new SqlCommand(query, connection))
//            {
//                command.Parameters.AddWithValue("@Type", type);

//                using (SqlDataReader reader = command.ExecuteReader())
//                {
//                    if (reader.Read())
//                    {
//                        result.CurrentWeek = reader.GetInt32(0);
//                    }
//                }
//            }
//            if (result.CurrentWeek != null)
//            {

//                string updateQuery = "UPDATE ThongKeTruyCap SET CurrentWeek = CurrentWeek + 1 WHERE Type = @Type";
//                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
//                {
//                    updateCommand.Parameters.AddWithValue("@Type", type);
//                    updateCommand.ExecuteNonQuery();
//                }
//            }
//            else
//            {

//                string insertQuery = "INSERT INTO ThongKeTruyCap (Type, CurrentWeek) VALUES (@Type, 1)";
//                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
//                {
//                    insertCommand.Parameters.AddWithValue("@Type", type);
//                    insertCommand.ExecuteNonQuery();
//                }
//            }
//        }
//        else if (type == ThongKeTruyCapConstant.Tong)
//        {

//            string query = "SELECT Total FROM ThongKeTruyCap WHERE Type = @Type";
//            using (SqlCommand command = new SqlCommand(query, connection))
//            {
//                command.Parameters.AddWithValue("@Type", type);

//                using (SqlDataReader reader = command.ExecuteReader())
//                {
//                    if (reader.Read())
//                    {
//                        result.Total = reader.GetInt32(0);
//                    }
//                }
//            }
//            if (result.Total != null)
//            {

//                string updateQuery = "UPDATE ThongKeTruyCap SET Total = Total + 1 WHERE Type = @Type";
//                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
//                {
//                    updateCommand.Parameters.AddWithValue("@Type", type);
//                    updateCommand.ExecuteNonQuery();
//                }
//            }
//            else
//            {

//                string insertQuery = "INSERT INTO ThongKeTruyCap (Type, Total) VALUES (@Type, 1)";
//                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
//                {
//                    insertCommand.Parameters.AddWithValue("@Type", type);
//                    insertCommand.ExecuteNonQuery();
//                }
//            }
//        }
//    }
//    return result;
//}
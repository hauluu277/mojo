using mojoportal.CoreHelpers;
using mojoportal.Service.BaseBusines;
using mojoportal.Service.CommonBusiness;
using mojoportal.Service.UoW;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Model.Data;
using mojoPortal.Service.CommonModel.Site;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace mojoPortal.Service.Business
{

    public class SiteBusiness : BaseBusiness<mp_Sites>
    {
        public SiteBusiness(UnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public mp_Sites GetById(int siteId)
        {
            return this.Find(siteId);
        }

        public List<SiteBO> GetListTongDieuTra()
        {
            var result = new List<SiteBO>();
            var listSearch = this.context.mp_Sites.Where(x => x.IsTongDieuTra == true).ToList();
            if (listSearch != null && listSearch.Any())
            {
                foreach (var item in listSearch)
                {
                    var siteBO = new SiteBO();
                    siteBO = MaperData.Map<SiteBO, mp_Sites>(siteBO, item);
                    result.Add(siteBO);
                }
            }
            return result;
        }

        public List<mp_Sites> ListCuocDieuTra()
        {
            return this.context.mp_Sites.Where(x => x.IsCuocDieuTra == true).ToList();
        }
        public List<SiteBO> GetListBy(int? linhVucId = 0)
        {
            var result = new List<SiteBO>();

            var listSearch = new List<mp_Sites>();
            if (linhVucId > 0)
            {
                listSearch = this.context.mp_Sites.Where(x => x.LinhVucID == linhVucId).ToList();
            }
            else
            {
                listSearch = this.context.mp_Sites.Where(x => x.LinhVucID.HasValue && x.LinhVucID > 0).ToList();
            }
            CategoryBusiness categoryBusiness = new CategoryBusiness(new UnitOfWork());
            foreach (var item in listSearch)
            {
                var siteBO = new SiteBO();
                siteBO = MaperData.Map<SiteBO, mp_Sites>(siteBO, item);
                siteBO.LinhVucName = categoryBusiness.GetName(item.LinhVucID.GetValueOrDefault(0));
                result.Add(siteBO);
            }
            return result;
        }

        public List<SiteBO> GetListBy(int? linhVucId = 0, int year = 0)
        {
            var result = new List<SiteBO>();

            var listSearch = new List<mp_Sites>();
            var query = context.mp_Sites.Where(x => (x.TrangThaiDieuTra == CuocDieuTraConstant.DaCongBo || x.TrangThaiDieuTra == CuocDieuTraConstant.DuThaoVaCongBo));
            if (linhVucId > 0)
            {
                listSearch = query.Where(x => x.LinhVucID == linhVucId && x.Nam == year).ToList();
            }
            else
            {
                listSearch = query.Where(x => x.LinhVucID.HasValue && x.LinhVucID > 0 && x.Nam == year).ToList();
            }
            CategoryBusiness categoryBusiness = new CategoryBusiness(new UnitOfWork());
            foreach (var item in listSearch)
            {
                var siteBO = new SiteBO();
                siteBO = MaperData.Map<SiteBO, mp_Sites>(siteBO, item);
                siteBO.LinhVucName = categoryBusiness.GetName(item.LinhVucID.GetValueOrDefault(0));
                result.Add(siteBO);
            }
            return result;
        }



        public List<SiteBO> GetListCuocDieuTra()
        {
            var listCuocDieuTra = this.context.mp_Sites.Where(x => x.IsCuocDieuTra == true).ToList();
            var listCuocDieuTraBO = MapDataHelper<SiteBO, mp_Sites>.MapDataList(listCuocDieuTra);
            foreach (var item in listCuocDieuTraBO)
            {
                item.API = this.context.core_SettingService.Where(x => x.SiteID == item.SiteID).Select(x => x.ServiceUrl).FirstOrDefault();
            }
            return listCuocDieuTraBO;
        }

        public string GetName(int id = 0)
        {
            if (id > 0)
            {
                var search = this.Find(id);
                if (search != null) return search.SiteName;
            }
            return string.Empty;
        }

        public PageListResultBO<SiteBO> GetPageReport(SiteSearchBO searchModel, int pageIndex = 1, int pageSize = 20)
        {
            var querySite = this.context.mp_Sites.AsQueryable();
            var query = this.context.mp_Sites.Where(x => x.IsCuocDieuTra == true).AsQueryable();

            if (searchModel != null)
            {
                if (searchModel.QR_TrangThaiDieuTra.HasValue)
                {
                    query = query.Where(x => x.TrangThaiDieuTra == searchModel.QR_TrangThaiDieuTra);
                }
                if (!string.IsNullOrEmpty(searchModel.QR_NameCuocDieuTra))
                {
                    query = query.Where(x => x.SiteName.Contains(searchModel.QR_NameCuocDieuTra));
                }
                if (searchModel.QR_LinhVuc != null)
                {
                    query = query.Where(x => x.LinhVucID == searchModel.QR_LinhVuc);
                }
                if (!string.IsNullOrEmpty(searchModel.QR_DoiTuongDonVi))
                {
                    query = query.Where(x => x.DoiTuongDieuTra.Contains(searchModel.QR_DoiTuongDonVi));
                }
                if (!string.IsNullOrEmpty(searchModel.QR_TanSuatDieuTra))
                {
                    query = query.Where(x => x.TanSuatDieuTra.Contains(searchModel.QR_TanSuatDieuTra));
                }
                if (!string.IsNullOrEmpty(searchModel.QR_PhamViTongHop))
                {
                    query = query.Where(x => x.PhamViSoLieu.Contains(searchModel.QR_PhamViTongHop));
                }
                if (searchModel.QR_NamDieuTra != null)
                {
                    query = query.Where(x => x.Nam == searchModel.QR_NamDieuTra);
                }
                if (searchModel.QR_GroupDieuTra.HasValue)
                {
                    query = query.Where(x => x.ParentID == searchModel.QR_GroupDieuTra);
                }

                if (!string.IsNullOrEmpty(searchModel.sortQuery))
                {
                    query = query.OrderBy(searchModel.sortQuery);
                }
                else
                {
                    query = query.OrderByDescending(x => x.Nam);
                }
            }
            else
            {
                query = query.OrderByDescending(x => x.Nam);
            }

            var resultmodel = new PageListResultBO<SiteBO>();
            if (pageSize == -1)
            {
                var dataPageList = query.ToList();
                resultmodel.Count = dataPageList.Count;
                resultmodel.TotalPage = 1;
                resultmodel.ListItem = MapDataHelper<SiteBO, mp_Sites>.MapDataList(dataPageList);
            }
            else
            {
                var dataPageList = query.ToPagedList(pageIndex, pageSize);
                resultmodel.Count = dataPageList.TotalItemCount;
                resultmodel.TotalPage = dataPageList.PageCount;
                resultmodel.ListItem = MapDataHelper<SiteBO, mp_Sites>.MapDataList(dataPageList.ToList());
            }
            resultmodel.PageIndex = pageIndex;
            resultmodel.PageSize = pageSize;
            CategoryBusiness categoryBusiness = new CategoryBusiness(new UnitOfWork());
            foreach (var item in resultmodel.ListItem)
            {
                item.LinhVucName = categoryBusiness.GetName(item.LinhVucID.GetValueOrDefault(0));
                item.TotalService = this.context.core_SettingService.Where(x => x.SiteID == item.SiteID).Count();
                if (item.ParentID.HasValue && item.ParentID > 0)
                {
                    item.GroupName = querySite.Where(x => x.SiteID == item.ParentID).Select(x => x.SiteName).FirstOrDefault();
                }
            }
            return resultmodel;
        }



        //public PageListResultBO<SiteBO> GetPageGopYPhuongAn(SiteUser userInfo, SiteSearchBO searchModel, int pageIndex = 1, int pageSize = 20)
        //{
        //    var isLanhDaoCucVu = false;
        //    var isAddPhuongAn = false;
        //    var isLanhDaoPhong = false;
        //    if (userInfo.MaChucVu == ChucVuConstant.CucTruong
        //        || userInfo.MaChucVu == ChucVuConstant.PhoCucTruong
        //        || userInfo.MaChucVu == ChucVuConstant.PhoCucTruongPhuTrach
        //        || userInfo.MaChucVu == ChucVuConstant.VuTruong
        //        || userInfo.MaChucVu == ChucVuConstant.PhoVuTruongPhuTrach
        //        || userInfo.MaChucVu == ChucVuConstant.PhoVuTruong
        //        )
        //    {
        //        isLanhDaoCucVu = true;
        //    }
        //    if (userInfo.MaChucVu == ChucVuConstant.TruongPhong
        //    || userInfo.MaChucVu == ChucVuConstant.PhoTruongPhongPhuTrach
        //    || userInfo.MaChucVu == ChucVuConstant.PhoTruongPhong
        //    || userInfo.MaChucVu == ChucVuConstant.ChiCucTruong
        //    || userInfo.MaChucVu == ChucVuConstant.PhoChiCucTruongPhuTrach
        //    || userInfo.MaChucVu == ChucVuConstant.PhoChiCucTruong
        //    )
        //    {
        //        isLanhDaoPhong = true;
        //    }
        //    if (userInfo.MaChucVu != ChucVuConstant.CucTruong
        //       && userInfo.MaChucVu != ChucVuConstant.PhoCucTruongPhuTrach
        //       && userInfo.MaChucVu != ChucVuConstant.TruongPhong
        //       && userInfo.MaChucVu != ChucVuConstant.PhoTruongPhongPhuTrach
        //       && userInfo.MaChucVu != ChucVuConstant.VuTruong
        //       && userInfo.MaChucVu != ChucVuConstant.PhoVuTruongPhuTrach
        //       && userInfo.MaChucVu != ChucVuConstant.ChiCucTruong
        //       && userInfo.MaChucVu != ChucVuConstant.PhoChiCucTruongPhuTrach
        //       )
        //    {
        //        isAddPhuongAn = true;
        //    }


        //    var query = this.context.mp_Sites.Where(x => x.IsCuocDieuTra == true && x.TrangThaiDieuTra == CuocDieuTraConstant.DuThao).AsQueryable();

        //    var roleBusiness = new RoleBusiness(new UnitOfWork());
        //    var isRoleTongHopGopY = roleBusiness.HasRole(RoleConstant.RoleTongHopGopY, userInfo.UserId);
        //    var isCucTTDL = userInfo.AD_MCS == DonViConstant.CucThuThapDuLieu;

        //    if (searchModel != null)
        //    {

        //        if (!string.IsNullOrEmpty(searchModel.QR_NameCuocDieuTra))
        //        {
        //            query = query.Where(x => x.SiteName.Contains(searchModel.QR_NameCuocDieuTra));
        //        }
        //        if (searchModel.QR_LinhVuc != null)
        //        {
        //            query = query.Where(x => x.LinhVucID == searchModel.QR_LinhVuc);
        //        }
        //        if (!string.IsNullOrEmpty(searchModel.QR_DoiTuongDonVi))
        //        {
        //            query = query.Where(x => x.DoiTuongDieuTra.Contains(searchModel.QR_DoiTuongDonVi));
        //        }
        //        if (!string.IsNullOrEmpty(searchModel.QR_TanSuatDieuTra))
        //        {
        //            query = query.Where(x => x.TanSuatDieuTra.Contains(searchModel.QR_TanSuatDieuTra));
        //        }
        //        if (!string.IsNullOrEmpty(searchModel.QR_PhamViTongHop))
        //        {
        //            query = query.Where(x => x.PhamViSoLieu.Contains(searchModel.QR_PhamViTongHop));
        //        }
        //        if (searchModel.QR_NamDieuTra != null)
        //        {
        //            query = query.Where(x => x.Nam == searchModel.QR_NamDieuTra);
        //        }

        //        if (!string.IsNullOrEmpty(searchModel.sortQuery))
        //        {
        //            query = query.OrderBy(searchModel.sortQuery);
        //        }
        //        else
        //        {
        //            query = query.OrderBy(x => x.SiteID);
        //        }
        //    }
        //    else
        //    {
        //        query = query.OrderBy(x => x.SiteID);
        //    }

        //    var resultmodel = new PageListResultBO<SiteBO>();
        //    if (pageSize == -1)
        //    {
        //        var dataPageList = query.ToList();
        //        resultmodel.Count = dataPageList.Count;
        //        resultmodel.TotalPage = 1;
        //        resultmodel.ListItem = MapDataHelper<SiteBO, mp_Sites>.MapDataList(dataPageList);
        //    }
        //    else
        //    {
        //        var dataPageList = query.ToPagedList(pageIndex, pageSize);
        //        resultmodel.Count = dataPageList.TotalItemCount;
        //        resultmodel.TotalPage = dataPageList.PageCount;
        //        resultmodel.ListItem = MapDataHelper<SiteBO, mp_Sites>.MapDataList(dataPageList.ToList());
        //    }
        //    resultmodel.PageIndex = pageIndex;
        //    resultmodel.PageSize = pageSize;
        //    CategoryBusiness categoryBusiness = new CategoryBusiness(new UnitOfWork());
        //    foreach (var item in resultmodel.ListItem)
        //    {
        //        if (item.HanGopY.HasValue)
        //        {
        //            item.IsHetHanGopY = item.HanGopY < DateTime.Now;
        //        }
        //        item.IsLanhDaoCuc = isLanhDaoCucVu;
        //        item.IsLanhDaoPhong = isLanhDaoPhong;
        //        var pheDuyetPhong = this.context.md_PhuongAnDieuTra.Where(x => x.SiteID == item.SiteID && x.AD_PhongBan == userInfo.AD_PhongBan && x.TrangThai == TrangThaiPhuongAnConstant.DaDuyet).FirstOrDefault();
        //        if (pheDuyetPhong == null)
        //        {
        //            item.PhuongAnDieuTraCapPhong = this.context.md_PhuongAnDieuTra.Where(x => x.SiteID == item.SiteID && x.TrangThaiCucTongHop == null && x.TrangThai != TrangThaiPhuongAnConstant.DaDuyet && x.AD_PhongBan == userInfo.AD_PhongBan).FirstOrDefault();
        //        }
        //        item.PheDuyetPhong = pheDuyetPhong;

        //        var pheDuyetCuc = this.context.md_PhuongAnDieuTra.Where(x => x.SiteID == item.SiteID && x.TrangThaiCucTongHop == TrangThaiPhuongAnConstant.DaDuyet && x.AD_MCS == userInfo.AD_MCS).FirstOrDefault();
        //        if (pheDuyetCuc == null)
        //        {
        //            item.PhuongAnDieuTraCapCuc = this.context.md_PhuongAnDieuTra.Where(x => x.SiteID == item.SiteID && x.TrangThai == null && x.AD_MCS == userInfo.AD_MCS).FirstOrDefault();
        //        }
        //        item.PheDuyetCuc = pheDuyetCuc;

        //        item.IsAddPhuongAn = isAddPhuongAn;
        //        item.GopYCapPhong = this.context.md_PhuongAnDieuTra.Where(x => x.SiteID == item.SiteID && x.UserID == userInfo.UserId && x.TrangThai.HasValue).FirstOrDefault();

        //        item.GopYCapCuc = this.context.md_PhuongAnDieuTra.Where(x => x.SiteID == item.SiteID && x.UserID == userInfo.UserId && x.TrangThaiCucTongHop.HasValue).FirstOrDefault();

        //        item.HasGopYCapPhong = this.context.md_PhuongAnDieuTra.Any(x => x.SiteID == item.SiteID && x.TrangThai == TrangThaiPhuongAnConstant.DaDuyet);
        //        if (isRoleTongHopGopY)
        //        {
        //            item.TotalGopY = this.context.md_PhuongAnDieuTra.Where(x => x.SiteID == item.SiteID && (x.TrangThaiCucTongHop == TrangThaiPhuongAnConstant.DaDuyet || x.IsLanhDaoCucPheDuyet == true || x.MaChucVu == ChucVuConstant.CucTruong || x.MaChucVu == ChucVuConstant.VuTruong)).Count();
        //        }
        //        else
        //        {
        //            if (userInfo.IsVu || userInfo.AD_MCS == DonViConstant.CucThuThapDuLieu)
        //            {
        //                item.TotalGopY = this.context.md_PhuongAnDieuTra.Where(x => x.SiteID == item.SiteID && x.AD_MCS == userInfo.AD_MCS && x.TrangThaiCucTongHop.HasValue).Count();
        //            }
        //            else
        //            {
        //                item.TotalGopY = this.context.md_PhuongAnDieuTra.Where(x => x.SiteID == item.SiteID && x.AD_MCS == userInfo.AD_MCS && x.TrangThai == TrangThaiPhuongAnConstant.DaDuyet).Count();
        //            }
        //        }
        //        item.IsRoleTongHopGopY = isRoleTongHopGopY;

        //        item.IsVu = userInfo.IsVu;
        //        item.IsCucTTDL = isCucTTDL;
        //        item.LinhVucName = categoryBusiness.GetName(item.LinhVucID.GetValueOrDefault(0));
        //    }
        //    return resultmodel;
        //}





        public List<SiteBO> GetGeneral(SiteSearchBO searchModel)
        {

            var query = this.context.mp_Sites.Where(x => x.IsCuocDieuTra == true).AsQueryable();

            if (searchModel != null)
            {

                if (!string.IsNullOrEmpty(searchModel.sortQuery))
                {
                    query = query.OrderBy(searchModel.sortQuery);
                }
                else
                {
                    query = query.OrderByDescending(x => x.SiteID);
                }
            }
            else
            {
                query = query.OrderByDescending(x => x.SiteID);
            }


            var result = MapDataHelper<SiteBO, mp_Sites>.MapDataList(query.ToList());
            CategoryBusiness categoryBusiness = new CategoryBusiness(new UnitOfWork());
            foreach (var item in result)
            {
                item.LinhVucName = categoryBusiness.GetName(item.LinhVucID.GetValueOrDefault(0));
                item.TotalService = this.context.core_SettingService.Where(x => x.SiteID == item.SiteID).Count();
            }
            return result;
        }


    }
}



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
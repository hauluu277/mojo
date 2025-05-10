using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Features
{
    public static class ArticleConstant
    {
        /// <summary>
        /// Feature Guid of Article module
        /// </summary>
        public static string FEATUREGUID = "8bdb1450-91e5-4cb0-af1a-2427d7e7e611";

        /// <summary>
        /// Hiển thị kiểu tin mới, đọc nhiều
        /// </summary>
        public static int TabTinMoiDocNhieu = 1;
        /// <summary>
        /// Hiển thị kiểu tin thông báo
        /// </summary>
        public static int TabTinThongBao = 2;
        /// <summary>
        /// Hiển thị kiểu tin công nghệ thông tin và chuyển đổi số
        /// </summary>
        public static int TabCongNgheThongTinCDS = 3;
        /// <summary>
        /// Hiển thị kiểu thông tin tuyển sinh
        /// </summary>
        public static int TabThongTinTuyenSinh = 4;
        /// <summary>
        /// Hiển thị kiểu thư viện ảnh và video
        /// </summary>
        public static int TabGalleryVideo = 5;

        /// <summary>
        /// tab văn bản mới
        /// </summary>
        public static int TabVanBanMoi = 11;
        /// <summary>
        /// tab danh sách trường
        /// </summary>
        public static int TabDanhSachTruong = 12;
        /// <summary>
        /// tab tin tức sự kiện
        /// </summary>
        public static int TabTinTucSuKien = 13;

        /// <summary>
        /// tab các phòng GD&ĐT và đơn vị trực thuộc
        /// </summary>
        public static int TabPhongTrucThuoc = 14;

        /// <summary>
        /// tab thành tích bảng vàng
        public static int TabBangVangThanhTich = 15;
        /// <summary>
        /// tab liên kết website
        /// </summary>
        public static int TabLienKetWebsite = 16;

        /// <summary>
        /// tab gương sáng
        /// </summary>
        public static int TabGuongSang = 17;
        /// <summary>
        /// tab thông báo mới, văn bản mới
        /// </summary>
        public static int TabVanBanThongBao = 18;
        /// <summary>
        /// tab hiển thị danh sách các chuyên mục
        /// </summary>
        public static int TabDanhSachCacChuyenMuc = 19; 
        /// <summary>
        /// tab hiển thị tin tức và chuyên mục con
        /// </summary>
        public static int TabTinVaChuyenMucCon = 22;
        /// <summary>
        /// Hiển thị kiểu thư viện kinh Doanh
        /// </summary>
        public static int TabKinhDoanh = 23;
        /// <summary>
        /// Hiển thị kiểu thư viện kinh Doanh
        /// </summary>
        public static int Tab5Tin2Anh = 24;
        
        // Tab hiển thị kiểu chuyển tiếp
        public static int TabTinChuyenTiep = 31; 
        // Tab hiển thị kiểu bố cục nổi bật
        public static int TabTinNoiBat = 32; 


    }

    public class ArticleTabListTypeConstant
    {
        public static string ShowImage = "ShowImage";
        public static string HideImage = "HideImage";


        public static string ShowFull = "full-input";
        public static string ShowSort = "";

    }

    /// <summary>
    /// Danh sách Animation
    /// </summary>
    public static class HieuUngConstant
    {

        public static string KhongHieuUng = "KhongHieuUng";
        public static string FadeInLeft = "fadeInLeft";
        public static string FadeInRight = "fadeInRight";

    }

    public static class ArticleHomeConstant
    {
        public static int Type_1 = 1;
        public static int Type_2 = 2;
        public static int Type_3 = 3;
        public static int Type_4 = 4;
    }
    public static class LanguageConstant
    {
        /// <summary>
        /// Feature Guid of Article module
        /// </summary>
        public static int VN = 1;
        public static int EN = 2;

    }
    public static class RoleConstant
    {
        /// <summary>
        /// Feature Guid of Article module
        /// </summary>
        public static int isPost = 0;
        public static int isApprove = 1;
    }

    public static class DisplaySettingConstant
    {
        public const int DisplayNoIMG = 1;
        public const int DisplayType_ThongBao = 2;
        public const int DisplayType_DaoTao = 3;
        public const int DisplayType_KhoaHocCongNghe = 4;
        public const int DisplayType_HopTacQuocTe = 5;
        public const int DisplayType_Khac = 6;
        public const int DisplayType_LichCongTac = 7;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace mojoPortal.Business.WebHelpers
{
    public static class TinhTPConstant
    {

        public static List<ListItem> ListTinhTP = new List<ListItem>()
        {
            new ListItem{Text="Hà Nội",Value="HANOI"},
            new ListItem{Text="Thành phố Hồ Chí Minh",Value="HOCHIMINH"},
            new ListItem{Text="An Giang",Value="ANGIANG"},
            new ListItem{Text="Bà Rịa - Vũng Tàu",Value="BARIAVUNGTAU"},
            new ListItem{Text="Bắc Giang",Value="BACGIANG"},
            new ListItem{Text="Bắc Kạn",Value="BACKAN"},
            new ListItem{Text="Bạc Liêu",Value="BACLIEU"},
            new ListItem{Text="Bắc Ninh",Value="BACNINH"},
            new ListItem{Text="Bến Tre",Value="BENTRE"},
            new ListItem{Text="Bình Định",Value="BINHDINH"},
            new ListItem{Text="Bình Dương",Value="BINHDUONG"},
            new ListItem{Text="Bình Phước",Value="BINHPHUOC"},
            new ListItem{Text="Bình Thuận",Value="BINHTHUAN"},
            new ListItem{Text="Cà Mau",Value="CAMAU"},
            new ListItem{Text="Cần thơ",Value="CANTHO"},
            new ListItem{Text="Cao Bằng",Value="CAOBANG"},
            new ListItem{Text="Đà Nẵng",Value="DANANG"},
            new ListItem{Text="Đắk Lắk",Value="DAKLAK"},
            new ListItem{Text="Đắk Nông",Value="DAKNONG"},
            new ListItem{Text="Điện Biên",Value="DIENBIEN"},
            new ListItem{Text="Đồng Nai",Value="DONGNAI"},
            new ListItem{Text="Đồng Tháp",Value="DONGTHAP"},
            new ListItem{Text="Gia Lai",Value="GIALAI"},
            new ListItem{Text="Hà Giang",Value="HAGIANG"},
            new ListItem{Text="Hà Nam",Value="HANAM"},
                        new ListItem{Text="Hà Tĩnh",Value="HATINH"},
                        new ListItem{Text="Hải Dương",Value="HAIDUONG"},
                        new ListItem{Text="Hải Phòng",Value="HAIPHONG"},
                        new ListItem{Text="Hậu Giang",Value="HAUGIANG"},
                        new ListItem{Text="Hòa Bình",Value="HOABINH"},
                        new ListItem{Text="Hưng Yên",Value="HUNGYEN"},
                        new ListItem{Text="Khánh Hòa",Value="KHANHHOA"},
                        new ListItem{Text="Kiên Giang",Value="KIENGIANG"},
                        new ListItem{Text="Kon Tum",Value="KONTUM"},
                        new ListItem{Text="Lai Châu",Value="LAICHAU"},
                        new ListItem{Text="Lâm Đồng",Value="LAMDONG"},
                        new ListItem{Text="Lạng Sơn",Value="LANGSON"},
                        new ListItem{Text="Lào Cai",Value="LAOCAI"},
                        new ListItem{Text="Long An",Value="LONGAN"},
                        new ListItem{Text="Nam Định",Value="NAMDINH"},
                        new ListItem{Text="Nghệ An",Value="NGHEAN"},
                        new ListItem{Text="Ninh Bình",Value="NINHBINH"},
                        new ListItem{Text="Ninh Thuận",Value="NINHTHUAN"},
                        new ListItem{Text="Phú Thọ",Value="PHUTHO"},
                        new ListItem{Text="Phú Yên",Value="PHUYEN"},
                        new ListItem{Text="Quảng Nam",Value="QUANGNAM"},
                        new ListItem{Text="Quảng Ngãi",Value="QUANGNGAI"},
                        new ListItem{Text="Quảng Ninh",Value="QUANGNINH"},
                        new ListItem{Text="Quảng Trị",Value="QUANGTRI"},
                        new ListItem{Text="Sóc Trăng",Value="SOCTRANG"},
                        new ListItem{Text="Sơn La",Value="SONLA"},
                        new ListItem{Text="Tây Ninh",Value="TAYNINH"},
                        new ListItem{Text="Thái Bình",Value="THAIBINH"},
                        new ListItem{Text="Thái Nguyên",Value="THAINGUYEN"},
                        new ListItem{Text="Thanh Hóa",Value="THANHHOA"},
                        new ListItem{Text="Thừa Thiên Huế",Value="THUATHIENHUE"},
                        new ListItem{Text="Tiền Giang",Value="TIENGIANG"},
                        new ListItem{Text="Trà Vinh",Value="TRAVINH"},
                        new ListItem{Text="Tuyên Quang",Value="TUYENQUANG"},
                        new ListItem{Text="Vĩnh Long",Value="VINHLONG"},
                        new ListItem{Text="Vĩnh Phúc",Value="VINHPHUC"},
                        new ListItem{Text="Yên Bái",Value="YENBAI"},
                        new ListItem{Text="Khác",Value="KHAC"},

        };
        public static string GetText(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return ListTinhTP.Where(x => x.Value.Equals(value)).Select(x => x.Text).FirstOrDefault();
            }
            return string.Empty;
        }
    }
    public static class PhongBanConstant
    {
        public const int LANHDAO = 1;
        public const int NHANVIEN = 2;
    }


    public static class MenuTypeLinkConstant
    {
        public const int Khac = 0;
        public const int Page = 1;
        public const int Category = 2;

        public static List<ListItem> GetListItem(int? selected = 0)
        {
            var result = new List<ListItem>();
            result.Add(new ListItem { Text = "Khác", Value = Khac.ToString(), Selected = (selected == Khac) });
            result.Add(new ListItem { Text = "Page", Value = Page.ToString(), Selected = (selected == Page) });
            result.Add(new ListItem { Text = "Category", Value = Category.ToString(), Selected = (selected == Category) });

            return result;
        }
    }

    public static class MenuConstant
    {
        public const int MenuMain = 1;
        public const int MenuTop = 2;
    }

    public static class TypeSearchArticleConstant
    {
        /// <summary>
        /// 1 tuần
        /// </summary>
        public const int OneWeek = 6;
        /// <summary>
        /// 1 năm
        /// </summary>
        public const int OneYear = 1;
        /// <summary>
        /// 1 tháng
        /// </summary>
        public const int OneMonth = 2;
        /// <summary>
        /// 3 tháng
        /// </summary>
        public const int ThreeMonth = 3;
        /// <summary>
        /// 6 tháng
        /// </summary>
        public const int SixMonth = 4;
        /// <summary>
        /// 9 tháng
        /// </summary>
        public const int NineMonth = 5;
        /// <summary>
        /// tất cả
        /// </summary>
        public const int All = 100;
        /// <summary>
        /// 2 năm
        /// </summary>
        public const int TwoYear = 7;
        /// <summary>
        /// 3 năm
        /// </summary>
        public const int ThreeYear = 8;
        /// <summary>
        /// 4 năm
        /// </summary>
        public const int FourYear = 9;
        /// <summary>
        /// 5 năm
        /// </summary>
        public const int FiveYear = 10;

        /// <summary>
        /// tìm kiếm tiêu đề
        /// </summary>
        public const int SearchWithTitle = 15;

        public const int SearchWithAuthor = 16;
        public const int SearchWithSapo = 17;
        public const int SearchAll = 18;
        public const int SearchContent = 19;


        public static List<ListItem> GetListSearch()
        {
            var result = new List<ListItem>();
            result.Add(new ListItem() { Text = "Tất cả", Value = SearchAll.ToString() });
            result.Add(new ListItem() { Text = "Tiêu đề", Value = SearchWithTitle.ToString() });
            result.Add(new ListItem() { Text = "Sapo", Value = SearchWithSapo.ToString() });
            result.Add(new ListItem() { Text = "Tác giả", Value = SearchWithAuthor.ToString() });

            return result;
        }


        public static List<ListItem> GetListItem()
        {
            var result = new List<ListItem>();
            result.Add(new ListItem { Text = "1 tuần gần đây", Value = OneWeek.ToString() });
            result.Add(new ListItem { Text = "1 tháng gần đây", Value = OneMonth.ToString() });
            result.Add(new ListItem { Text = "3 tháng gần đây", Value = ThreeMonth.ToString() });
            result.Add(new ListItem { Text = "6 tháng gần đây", Value = SixMonth.ToString() });
            result.Add(new ListItem { Text = "9 tháng gần đây", Value = NineMonth.ToString() });
            result.Add(new ListItem { Text = "1 năm gần đây", Value = OneYear.ToString() });
            result.Add(new ListItem { Text = "Tất cả", Value = All.ToString() });
            return result;
        }
        public static List<ListItem> GetListItemYear()
        {
            var result = new List<ListItem>();
            result.Add(new ListItem { Text = "Xem theo 3 tháng gần đây", Value = ThreeMonth.ToString() });
            result.Add(new ListItem { Text = "Xem theo 1 năm gần đây", Value = OneYear.ToString() });
            result.Add(new ListItem { Text = "Xem theo 2 năm gần đây", Value = TwoYear.ToString() });
            result.Add(new ListItem { Text = "Xem theo 3 năm gần đây", Value = ThreeYear.ToString() });
            result.Add(new ListItem { Text = "Xem theo 4 năm gần đây", Value = FourYear.ToString() });
            result.Add(new ListItem { Text = "Xem theo 5 năm gần đây", Value = FiveYear.ToString() });
            result.Add(new ListItem { Text = "Xem toàn bộ tin bài", Value = All.ToString() });
            return result;
        }

    }
    public class CuocDieuTraConstant
    {
        public static int DuThao = 1;
        public static int DaCongBo = 2;
        public static int DuThaoVaCongBo = 3;

        public static List<ListItem> GetListItem()
        {
            var result = new List<ListItem>();
            result.Add(new ListItem { Text = "Dự thảo", Value = DuThao.ToString() });
            result.Add(new ListItem { Text = "Đã công bố", Value = DaCongBo.ToString() });
            result.Add(new ListItem { Text = "Dự thảo và công bố", Value = DuThaoVaCongBo.ToString() });
            return result;
        }
    }

    public class GopYPhuongAnConstant
    {
        public static int ChoPheDuyet = 1;
        public static int DaPheDuyet = 2;
    }

    public class KieuLogConstant
    {
        public static string LogXacThucNguoiDung { get; set; } = "LogXacThucNguoiDung";
        public static string LogQuanTriTin { get; set; } = "LogQuanTriTin";
        public static string LogThayDoiGiaoDien { get; set; } = "LogThayDoiGiaoDien";
        public static string LogQuanTriVanBan { get; set; } = "LogQuanTriVanBan";

        public static string ThemMoi { get; set; } = "ThemMoiDuLieu";
        public static string CapNhapDuLieu { get; set; } = "CapNhapDuLieu";
        public static string XoaDuLieu { get; set; } = "XoaDuLieu";
        public static string DangNhapHeThong { get; set; } = "DangNhapHeThong";
        public static string ThayDoiGiaoDien { get; set; } = "ThayDoiGiaoDien";

        public static List<ListItem> lstThaoTac = new List<ListItem>()
        {
            new ListItem(){Text = "Thêm mới dữ liệu", Value = "ThemMoiDuLieu"},
            new ListItem(){Text = "Cập nhập dữ liêu", Value = "CapNhapDuLieu"},
            new ListItem(){Text = "Xóa dữ liệu", Value = "XoaDuLieu"},
            new ListItem(){Text = "Đăng nhập hệ thống", Value = "DangNhapHeThong"},
            new ListItem(){Text = "Thay đổi giao diện", Value = "ThayDoiGiaoDien"}
        };

        public static List<ListItem> lstKieuLog = new List<ListItem>()
        {
            new ListItem(){Text = "Log xác thực người dùng", Value = "LogXacThucNguoiDung"},
            new ListItem(){Text = "Log quản trị tin bài", Value = "LogQuanTriTin"},
            new ListItem(){Text = "Log thay đổi giao diện", Value = "LogThayDoiGiaoDien"},
            new ListItem(){Text = "Log quản trị văn bản", Value = "LogQuanTriVanBan"}
        };

        public static string GetTextThaoTac(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return lstThaoTac.Where(x => x.Value.Equals(value)).Select(x => x.Text).FirstOrDefault();
            }
            return string.Empty;
        }

        public static string GetTextKieuLog(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return lstKieuLog.Where(x => x.Value.Equals(value)).Select(x => x.Text).FirstOrDefault();
            }
            return string.Empty;
        }
    }
}

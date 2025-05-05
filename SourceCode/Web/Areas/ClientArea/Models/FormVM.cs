using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mojoPortal.Web.Areas.ClientArea.Models
{
    public class FormVM
    {
        public int ItemID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public string ClientID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public string ClientUrl { get; set; }
        //[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public string ClientCallBack { get; set; }
        //[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public string ClientSignIn { get; set; }
        //[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public string ClientSignOut { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public string ClientName { get; set; }
        // id nghiều chuyên mục cách nhau bởi dấu phẩy
        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public string ChuyenMucId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public int? IdNhomCongThanhVien { get; set; }



        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public int? ThoiGianLayTin { get; set; }
        public bool? isLayTinTuDong { get; set; }
        public string APIChuyenMucTin { get; set; }
        public string APIDanhSachTin { get; set; }

    }
}
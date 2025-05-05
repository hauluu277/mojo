using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.QL_LichCongTacArea.Models
{
    public class EditVM
    {
		[Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public int ItemID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public DateTime? NgayLamViec { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public DateTime? ThoiGianLamViec { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public string NoiDung { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public string DiaDiem { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public string[] ThanhPhanThamDuArray { get; set; }
        public string ThanhPhanThamDu { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public bool IsPublish { get; set; }
        public int Gio { get; set; }
        public int Phut { get; set; }
        public List<SelectListItem> ListThanhPhanThamDu { get; set; }
        public List<SelectListItem> ListDropdownGio { get; set; }
        public List<SelectListItem> ListDropdownPhut { get; set; }


    }
}
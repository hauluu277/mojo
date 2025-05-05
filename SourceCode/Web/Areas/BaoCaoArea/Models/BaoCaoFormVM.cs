using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.BaoCaoArea.Models
{
    public class BaoCaoFormVM
    {
        public int ItemID { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tên báo cáo")]
        public string TenBaoCao { get; set; }
        [Required(ErrorMessage = "Bạn chưa chọn chu kỳ báo cáo")]
        public Nullable<int> NamChuKyBaoCao { get; set; }
        public string BieuMau { get; set; }
        public string SoQuyetDinhCongBo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? NgayCongBo { get; set; }
        public string PathFile { get; set; }
        [Required(ErrorMessage = "Bạn chưa chọn lĩnh vực")]
        public Nullable<int> LinhVucID { get; set; }
        public bool? IsPublish { get; set; }


        public List<SelectListItem> ListLinhVuc { get; set; }
    }
}
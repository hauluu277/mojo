using mojoportal.Service.CommonBusiness;
using mojoPortal.Service;
using mojoPortal.Service.CommonModel.BieuMauThongTin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mojoPortal.Web.Areas.BieuMauThongTinArea.Data
{
    public class BieuMauThongTinEditViewModel
    {
        public int Id { get; set; }

        [DisplayName("Tên biểu mẫu")]
        [Required(ErrorMessage = "Vui lòng nhập thông tin này")]
        public string Ten { get; set; }

        public string Path { get; set; }

        public bool IsShow { get; set; }
    }
}
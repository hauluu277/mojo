using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mojoPortal.Web.Areas.OrganizationArea.Models
{
    public class LeaderFormVM
    {
        public int ItemID { get; set; }
        public Nullable<int> CCTC_ID { get; set; }
        [Required(ErrorMessage ="Bạn chưa nhập tên lãnh đạo")]
        public string Title { get; set; }
        public string PathIMG { get; set; }
        public Nullable<int> OrderBy { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập chức vụ lãnh đạo")]
        public string ChucVu { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string LinkDetail { get; set; }
        public string Description { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.UnitArea.Models
{
    public class UnitFormVM
    {
        public int ItemID { get; set; }
        public Nullable<int> SiteID { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập tiêu đề")]
        [Display(Name ="Tiêu đề")]
        public string Title { get; set; }
        [Display(Name ="Thứ tự")]
        public Nullable<int> OrderBy { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string CreatedByUser { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [Display(Name ="Quyền chỉnh sửa")]
        public string AllowUserEdit { get; set; }
        public Nullable<int> EditedBy { get; set; }
        public Nullable<bool> IsShowQuestion { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập đường dẫn")]
        [Display(Name ="Đường dẫn")]
        public string ItemUrl { get; set; }
        [Display(Name ="Xuất bản")]
        public Nullable<bool> IsPublished { get; set; }
        [Display(Name ="Loại đơn vị")]
        [Required]
        public string Type { get; set; }
        public List<SelectListItem> ListUser { get; set; }
        public List<SelectListItem> ListType { get; set; }
    }
}
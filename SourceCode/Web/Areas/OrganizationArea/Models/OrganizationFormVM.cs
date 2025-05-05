using mojoPortal.Model.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mojoPortal.Web.Areas.OrganizationArea.Models
{
    public class OrganizationFormVM
    {
        public int ItemID { get; set; }
        public Nullable<int> SiteID { get; set; }
        public string Description { get; set; }
        public Nullable<bool> ShowLanhDao { get; set; }
        public Nullable<bool> ShowPhongBan { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tiêu đề khối thông tin lãnh đạo")]
        public string TitleBoxLanhDao { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tiêu đề khối thông phòng ban")]
        public string TitleBoxPhongBan { get; set; }
        public List<core_CCTC_Leader> ListLeader { get; set; }
        public List<core_CCTC_Department> ListDepartment { get; set; }
    }
}
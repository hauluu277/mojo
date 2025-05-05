using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mojoPortal.Web.Areas.OrganizationArea.Models
{
    public class DepartmentFormVM
    {
        public int ItemID { get; set; }
        public Nullable<int> CCTC_ID { get; set; }
        [Required(ErrorMessage ="Bạn chưa nhập tên phòng ban")]
        public string Name { get; set; }
        public string PathIMG { get; set; }
        public string LinkDetail { get; set; }
        public Nullable<int> ParentID { get; set; }
        public string Description { get; set; }
        public Nullable<int> OrderBy { get; set; }
        public Nullable<int> CCTC_Leader_ID { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mojoPortal.Web.Areas.DanhMucArea.Models
{
    public class DanhMucVM
    {
        public long ItemID
        {
            get; set;
        }
        [Required(ErrorMessage = "Bạn chưa nhập tiêu đề")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập đường dẫn link")]
        public string UrlLink { get; set; }
        public string PathIMG { get; set; }
        public string Sapo { get; set; }
        public int OrderBy { get; set; }
        public bool IsPublish { get; set; }
    }
}
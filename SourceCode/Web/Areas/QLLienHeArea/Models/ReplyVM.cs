using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mojoPortal.Web.Areas.QLLienHeArea.Models
{
    public class ReplyVM
    {
        public Guid RowGuid { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tiêu đề phản hồi")]
        public string TieuDe { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập nội dung phản hồi")]
        public string Message { get; set; }
    }
}
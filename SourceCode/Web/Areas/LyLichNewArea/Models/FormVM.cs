using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mojoPortal.Web.Areas.LyLichNewArea.Models
{
    public class FormVM
    {
        public int ItemID { get; set; }
        

        public int Nam { get; set; }
        public int Week { get; set; }
        public DateTime? StartDate { get; set; }
        public string Thu { get; set; }
        public string ThoiGian { get; set; }
        public string NoiDung { get; set; }
        public string DiaDiem { get; set; }
        public string ThanhPhanThamDu { get; set; }
    }
}
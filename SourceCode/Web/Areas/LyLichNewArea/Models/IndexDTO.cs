using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Web.Areas.LyLichNewArea.Models
{
    public class IndexDTO
    {

        public string Nam { get; set; }
        public string Tuan { get; set; }
        public string Ngay { get; set; }
        public List<IndexDTO> listdata { get; set; }
    }
}
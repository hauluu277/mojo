using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Service.CommonModel.client
{
    public class IndexDTO
    {

        public string Nam { get; set; }
        public string Tuan { get; set; }
        public DateTime Ngay { get; set; }
        public string Thu { get; set; }
        public string ThoiGian { get; set; }
        public string NoiDung { get; set; }
        public string DiaDiem { get; set; }
        public string ThanhPhanThamDu { get; set; }
        public List<IndexDTO1> listdata { get; set; }
    }
    public class IndexDTO1
    {

        public string Nam { get; set; }
        public string Tuan { get; set; }
        public DateTime Ngay { get; set; }
        public string Thu { get; set; }
        public string ThoiGian { get; set; }
        public string NoiDung { get; set; }
        public string DiaDiem { get; set; }
        public string ThanhPhanThamDu { get; set; }
        public List<IndexDTO> listdata { get; set; }
    }
}
using mojoPortal.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Service.CommonModel.LichCongTac
{
    public class LichLamViecDto:core_LichLamViec
    {
        public List<string> ListThanhPhanThamDu { get; set; } = new List<string>();
        public String ThoiGianLamViec_text { get; set; }
    }
}

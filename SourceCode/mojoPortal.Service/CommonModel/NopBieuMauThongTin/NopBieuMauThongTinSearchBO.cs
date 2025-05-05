using mojoPortal.Business.WebHelpers.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Service.CommonModel.BieuMauThongTin
{
    public class NopBieuMauThongTinSearchBO : SearchBaseBO
    {
        public int? IdBieuMauFilter { get; set; }
        public DateTime? NgayNopStartFilter { get; set; }
        public DateTime? NgayNopEndFilter { get; set; }
    }
}

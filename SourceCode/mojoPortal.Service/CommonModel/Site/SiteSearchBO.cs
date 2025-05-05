using mojoportal.Service.BaseBusines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Service.CommonModel.Site
{
    public class SiteSearchBO:SearchBaseBO
    {
        public string QR_NameCuocDieuTra { get; set; }
        public int? QR_LinhVuc { get; set; }
        public string QR_DoiTuongDonVi { get; set; }
        public string QR_TanSuatDieuTra { get; set; }
        public string QR_PhamViTongHop { get; set; }
        public int? QR_NamDieuTra { get; set; }
        public int UserID { get; set; }
        public bool IsCucThuThapDuLieu { get; set; }
        public int? QR_GroupDieuTra { get; set; }
        public int? QR_TrangThaiDieuTra { get; set; }

    }
}

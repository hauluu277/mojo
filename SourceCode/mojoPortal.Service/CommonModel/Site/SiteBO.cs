using mojoPortal.Model.Data;

namespace mojoPortal.Service.CommonModel.Site
{
    public class SiteBO : mp_Sites
    {
        public string LinhVucName { get; set; }
        public int TotalService { get; set; }
        public string API { get; set; }
        public int TotalGopY { get; set; }
        public string GroupName { get; set; }
        public bool IsHetHanGopY { get; set; }
        public bool IsLanhDao { get; set; }
        public bool IsLanhDaoCuc { get; set; }
        public bool IsLanhDaoPhong { get; set; }
        public bool IsAddPhuongAn { get; set; }


        public bool HasGopYCapPhong { get; set; }
        public bool IsRoleTongHopGopY { get; set; }
        public bool IsVu { get; set; }
        public bool IsCucTTDL { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("core_ThuTuc")]
    public class core_ThuTuc
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long ItemID { get; set; }
        public Nullable<int> IdCoQuan { get; set; }
        public Nullable<int> IdMucDo { get; set; }
        public Nullable<int> IdCapDoThuTuc { get; set; }
        public string MaThuTuc { get; set; }
        public string TenThuTuc { get; set; }
        public Nullable<int> IdLinhVuc { get; set; }
        public string CachThucThucHien { get; set; }
        public Nullable<int> IdDoiTuongThucHien { get; set; }
        public string TrinhTuThucHien { get; set; }
        public string ThoiHanGianQuyet { get; set; }
        public string Phi { get; set; }
        public string LePhi { get; set; }
        public string ThanhPhanHoSo { get; set; }
        public Nullable<int> SoLuongHoSo { get; set; }
        public string YeuCauDieuKien { get; set; }
        public string CanCuPhapLy { get; set; }
        public string KetQuaThucHien { get; set; }
        public string LinkDVC { get; set; }
        public Nullable<bool> IsPublish { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<int> CreatedByUser { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> SiteID { get; set; }
        public Nullable<System.DateTime> EditDate { get; set; }
        public Nullable<int> EditByUser { get; set; }
    }
}

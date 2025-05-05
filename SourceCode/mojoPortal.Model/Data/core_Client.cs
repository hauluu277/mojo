using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Model.Data
{
    [Table("core_Client")]
    public class core_Client
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ItemID { get; set; }
        // app Id
        public string ClientID { get; set; }
        //domain
        public string ClientUrl { get; set; }
        public string ClientCallBack { get; set; }
        public string ClientSignIn { get; set; }
        public string ClientSignOut { get; set; }
        public string ClientName { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<int> CreatedByUser { get; set; }
        public Nullable<System.DateTime> EditedDate { get; set; }
        public Nullable<int> EditedBy { get; set; }
        // id nghiều chuyên mục cách nhau bởi dấu phẩy
        public string ChuyenMucId { get; set; }

        public int? IdNhomCongThanhVien { get; set; }



        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public int? ThoiGianLayTin { get; set; }
        public bool? isLayTinTuDong { get; set; }
        public string APIChuyenMucTin { get; set; }
        public string APIDanhSachTin { get; set; }

    }
}

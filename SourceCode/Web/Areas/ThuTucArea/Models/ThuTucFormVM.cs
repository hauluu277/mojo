using mojoPortal.Model.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.ThuTucArea.Models
{
    public class ThuTucFormVM
    {
        public long ItemID { get; set; }

        [Required(ErrorMessage = "Bạn chưa chọn cơ quan thực hiện")]
        public Nullable<int> IdCoQuan { get; set; }
        [Required(ErrorMessage = "Bạn chưa chọn mức độ DVC")]
        public Nullable<int> IdMucDo { get; set; }
        [Required(ErrorMessage = "Bạn chưa chọn cấp độ thủ tục")]
        public Nullable<int> IdCapDoThuTuc { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập mã thủ tục")]
        public string MaThuTuc { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tên thủ tục")]
        public string TenThuTuc { get; set; }
        [Required(ErrorMessage = "Bạn chưa chọn lĩnh vực")]
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
        [Required(ErrorMessage = "Bạn chưa nhập link DVC")]
        public string LinkDVC { get; set; }
        public Nullable<bool> IsPublish { get; set; }


        public string[] TenGiayTo { get; set; }
        public string[] MauDonToKhai { get; set; }
        public string[] SoLuong { get; set; }

        public string[] TenMau { get; set; }
        public string[] PathFile { get; set; }


        public List<SelectListItem> ListMucDoDVC { get; set; }
        public List<SelectListItem> ListCachThucThucHien { get; set; }
        public List<SelectListItem> ListDoiTuongThucHien { get; set; }

        public List<SelectListItem> ListLinhVuc { get; set; }
        public List<SelectListItem> ListCapThuTuc { get; set; }
        public List<SelectListItem> ListCoQuanThucHien { get; set; }

        public List<core_ThuTuc_ThanhPhanHS> ListThuTucThanhPhanHS { get; set; }
        public List<core_ThuTuc_BieuMau> ListThuTucBieuMau { get; set; }
    }
}
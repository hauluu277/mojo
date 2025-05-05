using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.ClientArea.Models
{
    public class ResultAPI
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<DanhMucWeb> Data { get; set; }
    }

    public class DanhMucWeb {
        public int IdCoreClient { get; set; }
        public long DanhMucId { get; set; }
        public string TenDanhMuc { get; set; }
        public bool IsDaLayDanhMuc { get; set; }
        public List<SelectListItem> listDanhMuc { get; set; }
    }
    public class ResultTinBaiAPI
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<TinBaiWeb> Data { get; set; }
    }
    public class TinBaiWeb
    {
        public bool IsThemVaoDanhMuc { get; set; }
        public long BaiVietId { get; set; }
        public DateTime NgayDang { get; set; }
        public string TieuDe { get; set; }
        public string MoTa { get; set; }
        public string NoiDung { get; set; }
        public string TacGia { get; set; }
        public DateTime NgayTao { get; set; }
        public string AnhDaiDien { get; set; }
        public string NguoiTao { get; set; }
        public string ClientId { get; set; }
        public long DanhMucId { get; set; }
    }
}
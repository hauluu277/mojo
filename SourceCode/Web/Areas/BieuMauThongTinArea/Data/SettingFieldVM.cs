using mojoPortal.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.BieuMauThongTinArea.Data
{
    public class SettingFieldVM
    {
        public bool? IsUseToReport { get; set; }
        public bentre_TieuChiBieuMau TieuChi { get; set; }
        public string TenCha { get; set; }
        public List<SelectListItem> LISTENABLED { get; set; }
        public List<SelectListItem> LISTDISABLED { get; set; }

        public List<string> KeysDisabled { get; set; }
        public List<string> KeysEnabled { get; set; }

        public List<SelectListItem> ListNhomDanhMuc { get; set; } = new List<SelectListItem>();
        public int? IdLoaiTongHop { get; set; }
        public List<SelectListItem> ListLoaiTongHop { get; set; }
        public List<SelectListItem> ListBoChiSo { get; set; }
    }
}
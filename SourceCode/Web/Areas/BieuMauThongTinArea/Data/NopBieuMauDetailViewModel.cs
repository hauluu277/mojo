using mojoportal.Service.CommonBusiness;
using mojoPortal.Model.Data;
using mojoPortal.Service;
using mojoPortal.Service.CommonModel.BieuMauThongTin;
using mojoPortal.Service.CommonModel.NopBieuMauThongTin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.BieuMauThongTinArea.Data
{
    public class NopBieuMauDetailViewModel
    {
        public bentre_NopBieuMau NopBieuMau { get; set; }
        public List<bentre_KeKhaiBieuMau> DuLieuKeKhai { get; set; }
        public List<bentre_TieuChiBieuMau> ListTieuChi { get; set; }
        public bentre_BieuMauThongTin BieuMauThongTin { get; set; }

        public Regex RegAlphabet { get; set; } = new Regex(@"^.*[a-zA-Z].*$");
        public Regex Reg { get; set; } = new Regex(@"\[\[[\w\.]*\]\]");
        public Regex RegCheckBox { get; set; } = new Regex(@"\[\[ck\.\w*\]\]");
        public Regex RegArea { get; set; } = new Regex(@"\[\[area\.\w*\]\]");
        public Regex RegScore { get; set; } = new Regex(@"\[\[diem\.\w*\]\]");
        public Regex RegScoreTongThe { get; set; } = new Regex(@"\[\[diemtongthe\.\w*\]\]");
        public Regex RegTyLe { get; set; } = new Regex(@"\[\[tyle\.\w*\]\]");
        public Regex RegBieuMau { get; set; } = new Regex(@"\[\[downloadbieumau\.\w*\]\]");
        public Regex RegScoreCuoiCung { get; set; } = new Regex(@"\[\[diemcuoicung\.\w*\]\]");
        public Regex RegRadio { get; set; } = new Regex(@"\[\[rb\.\w*\.\w*\]\]");
        public Regex RegUpload { get; set; } = new Regex(@"\[\[upload\.\w*\]\]");

        public Regex RegTaiBieuMau { get; } = new Regex(@"\[\[downloadbieumau\.\w*\]\]");

    }
}
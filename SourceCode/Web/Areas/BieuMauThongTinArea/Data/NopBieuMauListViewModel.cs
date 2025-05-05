using mojoportal.Service.CommonBusiness;
using mojoPortal.Service;
using mojoPortal.Service.CommonModel.BieuMauThongTin;
using mojoPortal.Service.CommonModel.NopBieuMauThongTin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.BieuMauThongTinArea.Data
{
    public class NopBieuMauListViewModel
    {
        public PageListResultBO<NopBieuMauThongTinBO> DanhSachNop { get; set; }
        public List<SelectListItem> DanhSachBieuMauThongTin { get; set; }
    }
}
using mojoportal.Service.CommonBusiness;
using mojoPortal.Service;
using mojoPortal.Service.CommonModel.BieuMauThongTin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Web.Areas.BieuMauThongTinArea.Data
{
    public class BieuMauThongTinListViewModel
    {
        public PageListResultBO<BieuMauThongTinBO> DanhSachBieuMauThongTin { get; set; }
    }
}
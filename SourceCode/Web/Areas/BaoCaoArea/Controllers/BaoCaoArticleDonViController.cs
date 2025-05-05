using mojoPortal.Service.Business;
using mojoPortal.Web.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.BaoCaoArea.Controllers
{
    public class BaoCaoArticleDonViController : BaseController
    {
        public BaoCaoArticleDonViController()
        {
            core_ClientBusiness = Get<core_ClientBusiness>();
            md_ArticlesBusiness = Get<md_ArticlesBusiness>();  
        }

        // GET: BaoCaoArea/BaoCaoArticleDonVi
        public ActionResult Index()
        {
            var data = core_ClientBusiness.GetDataArticleDonVi(null,null);
            return View(data);
        }

        public ActionResult DanhSachArticleTheoDonVi(string clientId)
        {
            var listData = md_ArticlesBusiness.GetDanhSachByIdDonVi(clientId, null, null);
            return PartialView("_DanhSachArticleTheoDonVi", listData);
        }

        [HttpPost]
        public ActionResult RenderBang(string TuNgay, string DenNgay)
        {
            DateTime? tuNgayX = new DateTime();
            DateTime? denNgayX = new DateTime();
            if (!string.IsNullOrEmpty(TuNgay))
            {
                tuNgayX = DateTime.Parse(TuNgay);
            }
            else
            {
                tuNgayX = null;
            }

            if (!string.IsNullOrEmpty(DenNgay))
            {
                denNgayX = DateTime.Parse(DenNgay);
            }
            else
            {
                denNgayX = null;
            }
            var data = core_ClientBusiness.GetDataArticleDonVi(tuNgayX,denNgayX);
            return PartialView("_RenderBang", data);
        }
        [HttpPost]
        public ActionResult RenderChart(string TuNgay, string DenNgay)
        {
            DateTime? tuNgayX = new DateTime();
            DateTime? denNgayX = new DateTime();
            if (!string.IsNullOrEmpty(TuNgay))
            {
                tuNgayX = DateTime.Parse(TuNgay);
            }
            else
            {
                tuNgayX = null;
            }

            if (!string.IsNullOrEmpty(DenNgay))
            {
                denNgayX = DateTime.Parse(DenNgay);
            }
            else
            {
                denNgayX = null;
            }
            var DmTinTuc = WebConfigSettings.DM_TinTuc;
            var data = core_ClientBusiness.GetDataArticleDonVi(tuNgayX, denNgayX);

            return PartialView("_RenderChart", data);
        }
    }
}
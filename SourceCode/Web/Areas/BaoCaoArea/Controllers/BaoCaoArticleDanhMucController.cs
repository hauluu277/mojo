using mojoPortal.Model.Data;
using mojoPortal.Service.Business;
using mojoPortal.Web.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.BaoCaoArea.Controllers
{
    public class BaoCaoArticleDanhMucController : BaseController
    {

        public BaoCaoArticleDanhMucController()
        {
            md_ArticleCategoryBusiness = Get<md_ArticleCategoryBusiness>();
            md_ArticlesBusiness = Get<md_ArticlesBusiness>();
        }

        // GET: BaoCaoArea/BaoCaoArticleDanhMuc
        public ActionResult Index()
        {
            var DmTinTuc = WebConfigSettings.DM_TinTuc;
            var listData = md_ArticlesBusiness.getTinTucByDanhMuc(DmTinTuc, null, null);
            return View(listData);
        }

        public ActionResult DanhSachArticleTheoDonVi(int IdTinTuc)
        {
            var DmTinTuc = WebConfigSettings.DM_TinTuc;
            var listData = md_ArticlesBusiness.getTinTucByDanhMucKhongBieuDo(DmTinTuc, null, null);
            var obj = listData.FirstOrDefault(x => x.core_Category.ItemID == IdTinTuc);
            return PartialView("_DanhSachArticleDanhMuc", obj);
        }

        [HttpPost]
        public ActionResult RenderBang(string TuNgay, string DenNgay)
        {
            DateTime? tuNgayX = new DateTime();
            DateTime? denNgayX = new DateTime();
            if(!string.IsNullOrEmpty(TuNgay))
            {
                tuNgayX = DateTime.Parse(TuNgay);
            } else
            {
                tuNgayX = null;
            }

            if(!string.IsNullOrEmpty(DenNgay))
            {
                denNgayX = DateTime.Parse(DenNgay);
            }else
            {
                denNgayX = null;
            }

            var DmTinTuc = WebConfigSettings.DM_TinTuc;
            var listData = md_ArticlesBusiness.getTinTucByDanhMuc(DmTinTuc, tuNgayX, denNgayX);
            return PartialView("_RenderBang", listData);
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
            var listData = md_ArticlesBusiness.getTinTucByDanhMuc(DmTinTuc, tuNgayX, denNgayX);
            return PartialView("_RenderChart", listData);
        }

    }
}
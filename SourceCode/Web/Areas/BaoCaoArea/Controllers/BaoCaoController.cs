using mojoportal.CoreHelpers;
using mojoportal.Service.CommonBusiness;
using mojoPortal.Model.Data;
using mojoPortal.Service.Business;
using mojoPortal.Service.CommonModel.BaoCao;
using mojoPortal.Web.Areas.BaoCaoArea.Models;
using mojoPortal.Web.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.BaoCaoArea.Controllers
{
    public class BaoCaoController : BaseController
    {
        int dmLinhVucId = 0;
        string dmLinhVucCode = "DM_LINHVUC_CONGKHAI_NGANSACH";
        public BaoCaoController()
        {
            baoCaoBusiness = Get<BaoCaoBusiness>();
            categoryBusiness = Get<CategoryBusiness>();
            dmLinhVucId = categoryBusiness.GetByCode(dmLinhVucCode).ItemID;

        }
        // GET: BaoCaoArea/BaoCaoArea
        public PartialViewResult FormBaoCao(int id = 0)
        {
            var listLinhVuc = new List<SelectListItem>();
            BaoCaoFormVM model = new BaoCaoFormVM();
            if (id > 0)
            {
                var report = baoCaoBusiness.GetByItemId(id);
                model = MapDataHelper<BaoCaoFormVM, md_BaoCao>.MapData(report);
                listLinhVuc = categoryBusiness.GetChild(dmLinhVucId).Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString(), Selected = (report.LinhVucID == x.ItemID) }).ToList();
            }
            else
            {
                listLinhVuc = categoryBusiness.GetChild(dmLinhVucId).Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString() }).ToList();
            }
            model.ListLinhVuc = listLinhVuc;
            return PartialView("_FormBaoCao", model);
        }

        [HttpPost]
        public JsonResult SaveForm(BaoCaoFormVM model, FormCollection formCollection)
        {
            var result = new JsonResultBO(true);
            var report = new md_BaoCao();
            try
            {
                result.Message = "Thêm mới thông tin báo cáo ngân sách thành công";

                report = new md_BaoCao();
                if (model.ItemID > 0)
                {
                    report = baoCaoBusiness.GetByItemId(model.ItemID);
                    result.Message = "Cập nhật thông tin báo cáo ngân sách thành công";
                }
                else
                {
                    report.CreatedBy = siteUser.Name;
                    report.CreatedByUser = siteUser.UserId;
                    report.CreatedDate = DateTime.Now;
                    report.ItemGuid = Guid.NewGuid();
                    report.SiteID = siteSettings.SiteId;
                }
                //get rewrite url product
                report = MaperData.MapAllowNull<md_BaoCao, BaoCaoFormVM>(report, model);
                report.BieuMau = model.BieuMau;
                report.NgayCongBo = formCollection["NgayCongBo"].ToDateTime();
                report.IsPublish = formCollection["IsPublish"].ToBooleanOnOff();
                baoCaoBusiness.Save(report);
            }
            catch (Exception ex)
            {
                result.Message = "Không thể thực hiện thao tác này";
                result.Status = false;
            }
            return Json(result);
        }



        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = new JsonResultBO(true);
            try
            {
                result.Message = "Xóa báo cáo ngân sách thành công";
                baoCaoBusiness.Delete(id);
                baoCaoBusiness.context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Message = "Không thực hiện được thao tác này";
                result.Status = false;
            }
            return Json(result);
        }


        public ActionResult Index()
        {

            BaoCaoIndexVM model = new BaoCaoIndexVM();
            model.ListData = baoCaoBusiness.GetDaTaByPage(null);
            var searchModel = new BaoCaoSearchBO();
            searchModel.pageIndex = 1;
            searchModel.pageSize = 20;
            SessionManager.SetValue("BaoCaoSearchModel", searchModel);
            ViewBag.ListLinhVuc = categoryBusiness.GetChild(dmLinhVucId).Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString() }).ToList();
            return View(model);
        }

        private List<SelectListItem> GetListYear(int selected = 0)
        {
            var list = new List<SelectListItem>();
            for (int i = 2010; i <= DateTime.Now.Year; i++)
            {
                list.Add(new SelectListItem
                {
                    Text = "Năm " + i,
                    Value = i.ToString(),
                    Selected = (i == selected)
                });
            }

            return list;
        }



        [HttpPost]
        public JsonResult GetData(int pageIndex, string sortQuery, int pageSize)
        {
            var searchModel =
                (BaoCaoSearchBO)SessionManager.GetValue("BaoCaoSearchModel");
            if (searchModel == null) searchModel = new BaoCaoSearchBO();
            searchModel.pageIndex = pageIndex;
            searchModel.pageSize = pageSize;
            searchModel.sortQuery = sortQuery;

            SessionManager.SetValue("BaoCaoSearchModel", searchModel);
            var data = baoCaoBusiness.GetDaTaByPage(searchModel, pageIndex, pageSize);
            return Json(data);
        }
        [HttpPost]

        public JsonResult SearchData(BaoCaoSearchBO searchModel)
        {
            if (searchModel is null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            var search =
         (BaoCaoSearchBO)SessionManager.GetValue("BaoCaoSearchModel");
            search = MapDataHelper<BaoCaoSearchBO, BaoCaoSearchBO>.MapData(searchModel);

            SessionManager.SetValue("BaoCaoSearchModel", search);
            var data = baoCaoBusiness.GetDaTaByPage(search, 1, searchModel.pageSize);
            data.PageIndex = 1;
            data.PageSize = 20;
            return Json(data);
        }
        [AllowAnonymous]
        public PartialViewResult LoadByLinhVuc(int categoryId)
        {
            BaoCaoListVM model = new BaoCaoListVM();
            model.ListBaoCao = baoCaoBusiness.ListByLinhVuc(categoryId);
            model.Category = categoryBusiness.GetById(categoryId);
            return PartialView("_ListByLinhVuc", model);
        }
        [AllowAnonymous]
        public ActionResult BaoCaoPublish(int categoryId = 0)
        {
            BaoCaoIndexVM model = new BaoCaoIndexVM();
            var searchModel = new BaoCaoSearchBO();
            searchModel.pageIndex = 1;
            searchModel.pageSize = 20;
            searchModel.QR_ISPUBLISH = true;
            searchModel.QR_LINHVUC = categoryId;
            model.ListData = baoCaoBusiness.GetDaTaByPage(searchModel);

            SessionManager.SetValue("BaoCaoSearchPublish", searchModel);
            ViewBag.CategoryName = categoryBusiness.GetName(categoryId);
            ViewBag.ListLinhVuc = categoryBusiness.GetChild(dmLinhVucId).Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString(), Selected = x.ItemID == categoryId }).ToList();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult GetPublish(int pageIndex, string sortQuery, int pageSize)
        {
            var searchModel =
                (BaoCaoSearchBO)SessionManager.GetValue("BaoCaoSearchPublish");
            if (searchModel == null) searchModel = new BaoCaoSearchBO();
            searchModel.pageIndex = pageIndex;
            searchModel.pageSize = pageSize;
            searchModel.sortQuery = sortQuery;
            searchModel.QR_ISPUBLISH = true;

            SessionManager.SetValue("BaoCaoSearchPublish", searchModel);
            var data = baoCaoBusiness.GetDaTaByPage(searchModel, pageIndex, pageSize);
            return Json(data);
        }
        [HttpPost]

        [AllowAnonymous]
        public JsonResult SearchPublish(BaoCaoSearchBO searchModel)
        {
            var search =
         (BaoCaoSearchBO)SessionManager.GetValue("BaoCaoSearchPublish");
            search = MapDataHelper<BaoCaoSearchBO, BaoCaoSearchBO>.MapData(searchModel);

            SessionManager.SetValue("BaoCaoSearchPublish", search);
            var data = baoCaoBusiness.GetDaTaByPage(search, 1, searchModel.pageSize);
            data.PageIndex = 1;
            data.PageSize = 20;
            return Json(data);
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult TabPublish()
        {
            BaoCaoIndexVM model = new BaoCaoIndexVM();
            var currentYear = DateTime.Now.Year;
            int idLinhVuc = 0;
            model.ListCategory = categoryBusiness.GetListChild("DM_LINHVUC_CONGKHAI_NGANSACH");
            if (model.ListCategory != null && model.ListCategory.Any())
            {
                idLinhVuc = model.ListCategory.Select(x => x.ItemID).FirstOrDefault();
            }
            model.ListBaoCao = baoCaoBusiness.ListBaoCaoPublish(currentYear, idLinhVuc);
            ViewBag.ListYear = GetListYear(currentYear);

            return PartialView("_TabPublish", model);
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult SearchBaoCao(int linhVucFillter, int yearFillter)
        {
            var search = new BaoCaoSearchBO();
            search.QR_LINHVUC = linhVucFillter;
            search.QR_NAM_CHUKYBAOCAO = yearFillter;


            SessionManager.SetValue("BaoCaoSearchModel", search);
            var data = baoCaoBusiness.ListBaoCaoPublish(yearFillter, linhVucFillter);
            return PartialView("_ListBaoCao", data);
        }
    }
}
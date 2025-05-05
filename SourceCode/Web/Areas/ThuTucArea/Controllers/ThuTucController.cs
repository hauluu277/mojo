using mojoportal.CoreHelpers;
using mojoportal.Service.CommonBusiness;
using mojoPortal.Business;
using mojoPortal.Model.Data;
using mojoPortal.Service.Business;
using mojoPortal.Service.CommonModel.BaoCao;
using mojoPortal.Service.CommonModel.ThuTuc;
using mojoPortal.Web.Areas.BaoCaoArea.Models;
using mojoPortal.Web.Areas.ThuTucArea.Models;
using mojoPortal.Web.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.ThuTucArea.Controllers
{
    public class ThuTucController : BaseController
    {
        int dmLinhVucId = 0;
        string dmLinhVucCode = "DM_LINHVUC_THUTUC";

        int dmMucDoId = 0;
        string dmMucDoCode = WebConfigSettings.DM_MucDoDVC;

        int dmCapThuTucId = 0;
        string dmCapThuTuc = WebConfigSettings.DM_CapDoThuTuc;

        int dmDoiTuongThucHienId = 0;
        string dmDoiTuongThucHien = WebConfigSettings.DM_DoiTuongThucHien;
        

        int dmCachThucThucHienId = 0;
        string dmCachThucThucHien = WebConfigSettings.DM_CachThucThucHien;

        int dmCoQuanThucHienId = 0;
        string dmCoQuanThucHien = WebConfigSettings.DM_COQUANTHUCHIENDVC;


        public ThuTucController()
        {
            thuTucBieuMauBusiness = Get<ThuTucBieuMauBusiness>();
            thuTucBusiness = Get<ThuTucBusiness>();
            thuTucThanhPhanBusiness = Get<ThuTucThanhPhanBusiness>();

            categoryBusiness = Get<CategoryBusiness>();
            dmLinhVucId = categoryBusiness.GetByCode(dmLinhVucCode).ItemID;

            dmMucDoId = categoryBusiness.GetByCode(dmMucDoCode).ItemID;
            dmCapThuTucId = categoryBusiness.GetByCode(dmCapThuTuc).ItemID;
            dmDoiTuongThucHienId = categoryBusiness.GetByCode(dmDoiTuongThucHien).ItemID;
            dmCachThucThucHienId = categoryBusiness.GetByCode(dmCachThucThucHien).ItemID;

            dmCoQuanThucHienId = categoryBusiness.GetByCode(dmCoQuanThucHien).ItemID;
        }
        // GET: BaoCaoArea/BaoCaoArea
        public PartialViewResult FormThuTuc(int id = 0)
        {
            var listLinhVuc = new List<SelectListItem>();
            var listMucDoDVC = new List<SelectListItem>();
            var listDoiTuongThucHien = new List<SelectListItem>();
            var listCachThucThucHien = new List<SelectListItem>();
            var listCapThuTuc = new List<SelectListItem>();
            var listCoQuanThucHien = new List<SelectListItem>();


            ThuTucFormVM model = new ThuTucFormVM();
            if (id > 0)
            {
                var report = thuTucBusiness.GetByItemId(id);
                model = MapDataHelper<ThuTucFormVM,core_ThuTuc>.MapData(report);
                listLinhVuc = categoryBusiness.GetChild(dmLinhVucId).Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString(), Selected = (report.IdLinhVuc == x.ItemID) }).ToList();

                listMucDoDVC = categoryBusiness.GetChild(dmMucDoId).Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString(), Selected = (report.IdMucDo == x.ItemID) }).ToList();
                listDoiTuongThucHien = categoryBusiness.GetChild(dmDoiTuongThucHienId).Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString(), Selected = (report.IdDoiTuongThucHien == x.ItemID) }).ToList();
                listCachThucThucHien = categoryBusiness.GetChild(dmCachThucThucHienId).Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString() }).ToList();
                listCapThuTuc = categoryBusiness.GetChild(dmCapThuTucId).Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString(), Selected = (report.IdCapDoThuTuc == x.ItemID) }).ToList();

                listCoQuanThucHien = categoryBusiness.GetChild(dmCoQuanThucHienId).Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString(), Selected = (report.IdCoQuan == x.ItemID) }).ToList();

                model.ListThuTucBieuMau = thuTucBieuMauBusiness.GetList(id);
                model.ListThuTucThanhPhanHS = thuTucThanhPhanBusiness.GetList(id);
            }
            else
            {
                listLinhVuc = categoryBusiness.GetChild(dmLinhVucId).Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString() }).ToList();
                listMucDoDVC = categoryBusiness.GetChild(dmMucDoId).Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString() }).ToList();
                listDoiTuongThucHien = categoryBusiness.GetChild(dmDoiTuongThucHienId).Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString() }).ToList();
                listCachThucThucHien = categoryBusiness.GetChild(dmCachThucThucHienId).Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString() }).ToList();
                listCapThuTuc = categoryBusiness.GetChild(dmCapThuTucId).Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString() }).ToList();
                listCoQuanThucHien = categoryBusiness.GetChild(dmCoQuanThucHienId).Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString() }).ToList();
            }
            model.ListLinhVuc = listLinhVuc;
            model.ListCachThucThucHien = listCachThucThucHien;
            model.ListDoiTuongThucHien = listDoiTuongThucHien;
            model.ListMucDoDVC = listMucDoDVC;
            model.ListCapThuTuc = listCapThuTuc;
            model.ListCoQuanThucHien = listCoQuanThucHien;

            return PartialView("_FormThuTuc", model);
        }

        public PartialViewResult AddThanhPhanHS()
        {
            return PartialView("_AddThanhPhanHoSo");
        }


        public PartialViewResult AddBieuMau()
        {
            return PartialView("_AddBieuMau");
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public JsonResult SaveForm(ThuTucFormVM model, FormCollection formCollection)
        {
            var result = new JsonResultBO(true);
            var report = new core_ThuTuc();
            try
            {
                result.Message = "Thêm mới thông tin thủ tục hành chính thành công";

                report = new core_ThuTuc();
                if (model.ItemID > 0)
                {
                    report = thuTucBusiness.GetByItemId(model.ItemID);
                    result.Message = "Cập nhật thông tin thủ tục hành chính thành công";
                }
                else
                {
                    report.CreatedBy = siteUser.Name;
                    report.CreatedByUser = siteUser.UserId;
                    report.CreatedDate = DateTime.Now;
                    report.SiteID = siteSettings.SiteId;
                }
                //get rewrite url product
                report = MaperData.Map<core_ThuTuc, ThuTucFormVM>(report, model);
                report.IsPublish = formCollection["IsPublish"].ToBooleanOnOff();
                string cachThucThucHien = string.Empty;
                if (!string.IsNullOrEmpty(formCollection["CachThucThucHien"]))
                {
                    cachThucThucHien = "," + formCollection["CachThucThucHien"];
                }
                report.CachThucThucHien = cachThucThucHien;
                thuTucBusiness.Save(report);

                //save thành phần hồ sơ
                if (model.TenGiayTo != null && model.TenGiayTo.Any())
                {
                    for (int i = 0; i < model.TenGiayTo.Length; i++)
                    {
                        core_ThuTuc_ThanhPhanHS thanhPhanHS = new core_ThuTuc_ThanhPhanHS();
                        thanhPhanHS.TenGiayTo = model.TenGiayTo[i];
                        thanhPhanHS.SoLuong = model.SoLuong[i];
                        thanhPhanHS.MauDonToKhai = model.MauDonToKhai[i];
                        thanhPhanHS.IdThuTuc = report.ItemID;
                        thuTucThanhPhanBusiness.Save(thanhPhanHS);
                    }
                }

                //save biểu mẫu
                if (model.TenMau != null && model.TenMau.Any())
                {
                    for (int i = 0; i < model.TenMau.Length; i++)
                    {
                        core_ThuTuc_BieuMau bieuMau = new core_ThuTuc_BieuMau();
                        bieuMau.IdThuTuc = report.ItemID;
                        bieuMau.PathFile = model.PathFile[i];
                        bieuMau.TenMau = model.TenMau[i];
                        thuTucBieuMauBusiness.Save(bieuMau);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = "Không thể thực hiện thao tác này";
                result.Status = false;
            }
            return Json(result);
        }





        [HttpPost]
        public JsonResult DeleteThanhPhanHoSo(int id)
        {
            var result = new JsonResultBO(true);
            try
            {
                result.Message = "Xóa thành phần hồ sơ thành công";
                thuTucThanhPhanBusiness.Delete(id);
                thuTucThanhPhanBusiness.context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Message = "Không thực hiện được thao tác này";
                result.Status = false;
            }
            return Json(result);
        }


        [HttpPost]
        public JsonResult DeleteBieuMau(int id)
        {
            var result = new JsonResultBO(true);
            try
            {
                result.Message = "Xóa thành biểu mẫu thành công";
                thuTucBieuMauBusiness.Delete(id);
                thuTucBieuMauBusiness.context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Message = "Không thực hiện được thao tác này";
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
                result.Message = "Xóa thủ tục hành chính thành công";
                thuTucBusiness.Delete(id);
                thuTucBusiness.context.SaveChanges();
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
            ThuTucIndexVM model = new ThuTucIndexVM();
            model.ListData = thuTucBusiness.GetDaTaByPage(null);
            var searchModel = new ThuTucSearchBO();
            searchModel.pageIndex = 1;
            searchModel.pageSize = 20;
            SessionManager.SetValue("ThuTucSearchModel", searchModel);
            ViewBag.ListLinhVuc = categoryBusiness.GetChild(dmLinhVucId).Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString() }).ToList();

            ViewBag.ListMucDoDVC = categoryBusiness.GetChild(dmMucDoId).Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString() }).ToList();
            ViewBag.ListDoiTuongThucHien = categoryBusiness.GetChild(dmDoiTuongThucHienId).Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString() }).ToList();
            ViewBag.ListCachThucThucHien = categoryBusiness.GetChild(dmCachThucThucHienId).Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString() }).ToList();

            ViewBag.ListCoQuanThucHien= categoryBusiness.GetChild(dmCoQuanThucHienId).Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString() }).ToList();
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
                (ThuTucSearchBO)SessionManager.GetValue("ThuTucSearchModel");
            if (searchModel == null) searchModel = new ThuTucSearchBO();
            searchModel.pageIndex = pageIndex;
            searchModel.pageSize = pageSize;
            searchModel.sortQuery = sortQuery;

            SessionManager.SetValue("ThuTucSearchModel", searchModel);
            var data = thuTucBusiness.GetDaTaByPage(searchModel, pageIndex, pageSize);
            return Json(data);
        }
        [HttpPost]

        public JsonResult SearchData(ThuTucSearchBO searchModel)
        {
            if (searchModel is null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            var search =
         (ThuTucSearchBO)SessionManager.GetValue("ThuTucSearchModel");
            search = MapDataHelper<ThuTucSearchBO, ThuTucSearchBO>.MapData(searchModel);

            SessionManager.SetValue("ThuTucSearchModel", search);
            var data = thuTucBusiness.GetDaTaByPage(search, 1, searchModel.pageSize);
            data.PageIndex = 1;
            data.PageSize = 20;
            return Json(data);
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
            model.ListBaoCao = baoCaoBusiness.ListBaoCao(currentYear);
            model.ListCategory = categoryBusiness.GetListChild("DM_LINHVUC_CONGKHAI_NGANSACH");
            ViewBag.ListYear = GetListYear(currentYear);

            return PartialView("_TabPublish", model);
        }


        [HttpPost]
        public ActionResult SearchBaoCao(int linhVucFillter, int yearFillter)
        {
            var search = new BaoCaoSearchBO();
            search.QR_LINHVUC = linhVucFillter;
            search.QR_NAM_CHUKYBAOCAO = yearFillter;


            SessionManager.SetValue("ThuTucSearchModel", search);
            var data = baoCaoBusiness.ListBaoCao(yearFillter, linhVucFillter);
            return PartialView("_ListBaoCao", data);
        }
    }
}
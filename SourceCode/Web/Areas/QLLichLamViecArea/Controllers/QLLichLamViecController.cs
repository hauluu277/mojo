using mojoportal.CoreHelpers;
using mojoportal.Service.CommonBusiness;
using mojoportal.Service.UoW;
using mojoPortal.Model.Data;
using mojoPortal.Service.CommonModel.BaoCao;
using mojoPortal.Service.CommonModel.LichCongTac;
using mojoPortal.Service.CommonModel.LichLamViec;
using mojoPortal.Web.Areas.QL_LichCongTacArea.Models;
using mojoPortal.Web.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.QLLichLamViecArea.Controllers
{
    public class QLLichLamViecController : BaseController
    {
        // GET: QLLichLamViecArea/QLLichLamViec
        string searchKey = "searchLichLamViecModel";
        public QLLichLamViecController()
        {
            var unitOfwork = new UnitOfWork();
            lichLamViecBusiness = new Service.Business.LichLamViecBusiness(unitOfwork);
            categoryBusiness = new Service.Business.CategoryBusiness(unitOfwork);
        }
        public ActionResult Index()
        {

            var listData = lichLamViecBusiness.GetDaTaByPage(null);
            SessionManager.SetValue(searchKey, null);
            ViewBag.ListLanhDao = categoryBusiness.GetListChildSelectItem(WebConfigSettings.DM_LANHDAO);

            return View(listData);
        }

        [HttpPost]
        public JsonResult getData(int indexPage, string sortQuery, int pageSize)
        {
            var searchModel = SessionManager.GetValue(searchKey) as LichLamViecSearchDto;
            if (!string.IsNullOrEmpty(sortQuery))
            {
                if (searchModel == null)
                {
                    searchModel = new LichLamViecSearchDto();
                }
                searchModel.sortQuery = sortQuery;
                if (pageSize > 0)
                {
                    searchModel.pageSize = pageSize;
                }
                SessionManager.SetValue(searchKey, searchModel);
            }
            var data = lichLamViecBusiness.GetDaTaByPage(searchModel, indexPage, pageSize);
            return Json(data);
        }
        public PartialViewResult Create()
        {
            var myModel = new CreateVM();
            myModel.ListThanhPhanThamDu = categoryBusiness.GetListChildSelectItem(WebConfigSettings.DM_LANHDAO);
            var currentTime = DateTime.Now;
            myModel.ListDropdownGio = GetHours(currentTime.Hour);

            myModel.ListDropdownPhut = GetMinutes(0);
            return PartialView("_CreatePartial", myModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public JsonResult Create(CreateVM model, FormCollection formCollection)
        {
            var result = new JsonResultBO(true, "Tạo mới lịch công tác thành công");
            try
            {
                var entity = new core_LichLamViec();
                entity = MaperData.Map<core_LichLamViec, CreateVM>(entity, model);
                if (entity.NgayLamViec.HasValue)
                {
                    var thoiGianLamViec = entity.NgayLamViec.Value.AddHours(model.Gio).AddMinutes(model.Phut);
                    entity.ThoiGianLamViec = thoiGianLamViec;
                }
                entity.IsPublish = formCollection["IsPublish"].ToBooleanOnOff();
                if (model.ThanhPhanThamDuArray != null && model.ThanhPhanThamDuArray.Any())
                {
                    entity.ThanhPhanThamDu = string.Join(" ", model.ThanhPhanThamDuArray);
                }
                entity.CreatedBy = siteUser.Name;
                entity.CreatedByUser = siteUser.UserId;
                entity.CreatedDate = DateTime.Now;
                lichLamViecBusiness.Save(entity);
            }
            catch (Exception ex)
            {
                result.MessageFail(ex.Message);
            }
            return Json(result);
        }

        public PartialViewResult Edit(long id)
        {
            var myModel = new EditVM();

            var obj = lichLamViecBusiness.Find(id);
            if (obj == null)
            {
                throw new HttpException(404, "Không tìm thấy thông tin");
            }
            myModel = MapDataHelper<EditVM, core_LichLamViec>.MapData(obj);
            var hour = 0;
            var minute = 0;
            if (obj.ThoiGianLamViec.HasValue)
            {
                hour = obj.ThoiGianLamViec.Value.Hour;
                minute = obj.ThoiGianLamViec.Value.Minute;
            }
            myModel.Gio = hour;
            myModel.Phut = minute;
            myModel.ListThanhPhanThamDu = categoryBusiness.GetListChildSelectItem(WebConfigSettings.DM_LANHDAO);
            myModel.ListDropdownGio = GetHours(hour);
            myModel.ListDropdownPhut = GetMinutes(minute);

            return PartialView("_EditPartial", myModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public JsonResult Edit(EditVM model, FormCollection formCollection)
        {
            var result = new JsonResultBO(true);
            try
            {
                var itemId = formCollection["ItemID"];
                var obj = lichLamViecBusiness.Find(model.ItemID);
                if (obj == null)
                {
                    throw new Exception("Không tìm thấy thông tin");
                }
                var entity = new core_LichLamViec();
                entity = MaperData.Map<core_LichLamViec, EditVM>(obj, model);
                if (entity.NgayLamViec.HasValue)
                {
                    var thoiGianLamViec = entity.NgayLamViec.Value.AddHours(model.Gio).AddMinutes(model.Phut);
                    entity.ThoiGianLamViec = thoiGianLamViec;
                }
                if (model.ThanhPhanThamDuArray != null && model.ThanhPhanThamDuArray.Any())
                {
                    entity.ThanhPhanThamDu = string.Join(" ", model.ThanhPhanThamDuArray);
                }
                entity.EditedBy = siteUser.UserId;
                entity.EditedDate = DateTime.Now;
                entity.IsPublish = formCollection["IsPublish"].ToBooleanOnOff();
                lichLamViecBusiness.Save(entity);

            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = "Không cập nhật được";
            }
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult searchData(LichLamViecSearchDto searchModel)
        {
            if (searchModel == null)
            {
                searchModel = new LichLamViecSearchDto();
                searchModel.pageSize = 20;
            }

            SessionManager.SetValue((searchKey), searchModel);

            var data = lichLamViecBusiness.GetDaTaByPage(searchModel, 1, searchModel.pageSize);
            return Json(data);
        }

        [HttpPost]
        public JsonResult Delete(long id)
        {
            var result = new JsonResultBO(true, "Xóa Quản lý lịch công tác thành công");
            try
            {
                var user = lichLamViecBusiness.Find(id);
                if (user == null)
                {
                    throw new Exception("Không tìm thấy thông tin để xóa");
                }
                lichLamViecBusiness.Delete(id);
                lichLamViecBusiness.context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.MessageFail("Không thực hiện được");
            }
            return Json(result);
        }

        private List<SelectListItem> GetHours(int? selected = 1)
        {
            var result = new List<SelectListItem>();
            for (int i = 0; i < 24; i++)
            {
                result.Add(new SelectListItem()
                {
                    Text = i + " giờ",
                    Value = i.ToString(),
                    Selected = selected == i
                });
            }

            return result;
        }

        private List<SelectListItem> GetMinutes(int? selected = 1)
        {
            var result = new List<SelectListItem>();
            for (int i = 0; i < 60; i++)
            {
                result.Add(new SelectListItem()
                {
                    Text = i + " phút",
                    Value = i.ToString(),
                    Selected = selected == i
                });
            }

            return result;
        }


        public ActionResult Detail(long id)
        {
            var model = new DetailVM();
            model.objInfo = lichLamViecBusiness.GetDetail(id);
            return View(model);
        }
        //[PermissionAccess(Code = permissionImport)]

        //public FileResult ExportExcel()
        //{
        //    var searchModel = SessionManager.GetValue(searchKey) as QL_LichCongTacSearchDto;
        //    var data = _QL_LichCongTacService.GetDaTaByPage(searchModel).ListItem;
        //    var dataExport = _mapper.Map<List<QL_LichCongTacExportDto>>(data);
        //    var fileExcel = ExportExcelV2Helper.Export<QL_LichCongTacExportDto>(dataExport);
        //    return File(fileExcel, "application/octet-stream", "QL_LichCongTac.xlsx");
        //}

        //[PermissionAccess(Code = permissionImport)]
        //public ActionResult Import()
        //{
        //    var model = new ImportVM();
        //    model.PathTemplate = Path.Combine(@"/Uploads", WebConfigurationManager.AppSettings["IMPORT_QL_LichCongTac"]);

        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult CheckImport(FormCollection collection, HttpPostedFileBase fileImport)
        //{
        //    JsonResultImportBO<QL_LichCongTacImportDto> result = new JsonResultImportBO<QL_LichCongTacImportDto>(true);
        //    //Kiểm tra file có tồn tại k?
        //    if (fileImport == null)
        //    {
        //        result.Status = false;
        //        result.Message = "Không có file đọc dữ liệu";
        //        return View(result);
        //    }

        //    //Lưu file upload để đọc
        //    var saveFileResult = UploadProvider.SaveFile(fileImport, null, ".xls,.xlsx", null, "TempImportFile", HostingEnvironment.MapPath("/Uploads"));
        //    if (!saveFileResult.status)
        //    {
        //        result.Status = false;
        //        result.Message = saveFileResult.message;
        //        return View(result);
        //    }
        //    else
        //    {

        //        #region Config để import dữ liệu
        //        var importHelper = new ImportExcelHelper<QL_LichCongTacImportDto>();
        //        importHelper.PathTemplate = saveFileResult.fullPath;
        //        //importHelper.StartCol = 2;
        //        importHelper.StartRow = collection["ROWSTART"].ToIntOrZero();
        //        importHelper.ConfigColumn = new List<ConfigModule>();
        //        importHelper.ConfigColumn = ExcelImportExtention.GetConfigCol<QL_LichCongTacImportDto>(collection);
        //        #endregion
        //        var rsl = importHelper.ImportCustomRow();
        //        if (rsl.Status)
        //        {
        //            result.Status = true;
        //            result.Message = rsl.Message;

        //            result.ListData = rsl.ListTrue;
        //            result.ListFalse = rsl.lstFalse;
        //        }
        //        else
        //        {
        //            result.Status = false;
        //            result.Message = rsl.Message;
        //        }

        //    }
        //    return View(result);
        //}


        //[HttpPost]
        //public JsonResult GetExportError(List<List<string>> lstData)
        //{
        //    ExportExcelHelper<QL_LichCongTacImportDto> exPro = new ExportExcelHelper<QL_LichCongTacImportDto>();
        //    exPro.PathStore = Path.Combine(HostingEnvironment.MapPath("/Uploads"), "ErrorExport");
        //    exPro.PathTemplate = Path.Combine(HostingEnvironment.MapPath("/Uploads"), WebConfigurationManager.AppSettings["IMPORT_QL_LichCongTac"]);
        //    exPro.StartRow = 5;
        //    exPro.StartCol = 2;
        //    exPro.FileName = "ErrorImportQL_LichCongTac";
        //    var result = exPro.ExportText(lstData);
        //    if (result.Status)
        //    {
        //        result.PathStore = Path.Combine(@"/Uploads/ErrorExport", result.FileName);
        //    }
        //    return Json(result);
        //}

        //[HttpPost]
        //public JsonResult SaveImportData(List<QL_LichCongTacImportDto> Data)
        //{
        //    var result = new JsonResultBO(true);

        //    var lstObjSave = new List<QL_LichCongTac>();
        //    try
        //    {
        //        foreach (var item in Data)
        //        {
        //            var obj = _mapper.Map<QL_LichCongTac>(item);
        //            _QL_LichCongTacService.Create(obj);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        result.Status = false;
        //        result.Message = "Lỗi dữ liệu, không thể import";
        //        _Ilog.Error("Lỗi Import", ex);
        //    }

        //    return Json(result);
        //}
        [AllowAnonymous]
        public PartialViewResult LichCongTacView(string startDate = "", string endDate = "")
        {
            var data = lichLamViecBusiness.ShowLichCongTac(startDate.ToDateTime(), endDate.ToDateTime());
            return PartialView("_LichCongTacPartial", data);
        }
        [AllowAnonymous]
        public ActionResult IndexLichCongTac()
        {
            var data = lichLamViecBusiness.ShowLichCongTac();
            return View(data);
        }
    }
}
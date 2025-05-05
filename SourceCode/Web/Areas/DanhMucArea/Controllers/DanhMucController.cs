using mojoPortal.Service.Business;
using mojoPortal.Web.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mojoPortal.Model.Data;
using mojoportal.Service.CommonBusiness;
using mojoportal.CoreHelpers;
using mojoPortal.Service.CommonModel.coreDanhMuc;
using mojoPortal.Web.Areas.DanhMucArea.Models;

namespace mojoPortal.Web.Areas.DanhMucArea.Controllers
{
    public class DanhMucController : BaseController
    {
        // GET: DanhMucArea/DanhMuc
        public DanhMucController()
        {
            core_DanhMucBusiness = Get<core_DanhMucBusiness>();

        }
        // GET: BaoCaoArea/BaoCaoArea
        public PartialViewResult FormDanhMuc(int id = 0)
        {
            var model = new DanhMucVM();
            if (id > 0)
            {
              var  core_DanhMuc = core_DanhMucBusiness.Find(id);
                if (core_DanhMuc == null)
                {
                    throw new Exception("Không tìm thấy dữ liệu");
                }
                model = MapDataHelper<DanhMucVM, core_DanhMuc>.MapData(core_DanhMuc);

            }
            return PartialView("_FormDanhMuc", model);
        }

        [HttpPost]
        public JsonResult SaveForm(DanhMucVM model, FormCollection formCollection)
        {
            var result = new JsonResultBO(true);
            var report = new core_DanhMuc();
            try
            {
                result.Message = "Thêm mới chuyên mục thành công";

                report = new core_DanhMuc();
                if (model.ItemID > 0)
                {
                    report = core_DanhMucBusiness.Find(model.ItemID);
                    result.Message = "Cập nhật thông tin chuyên mục thành công";
                }
                else
                {
                    report.CreatedByUser = siteUser.UserId;
                    report.CreatedDate = DateTime.Now;
                    report.SiteID = siteSettings.SiteId;
                }
                //get rewrite url product
                report = MaperData.MapAllowNull<core_DanhMuc, DanhMucVM>(report, model);
                report.IsPublish = formCollection["IsPublish"].ToBooleanOnOff();
                core_DanhMucBusiness.Save(report);
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
                result.Message = "Xóa báo chuyên mục liên kết thành công";
                core_DanhMucBusiness.Delete(id);
                core_DanhMucBusiness.context.SaveChanges();
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

           var   data = core_DanhMucBusiness.GetDaTaByPage(null);
            var searchModel = new core_DanhMucSearchBO();
            searchModel.pageIndex = 1;
            searchModel.pageSize = 20;
            SessionManager.SetValue("danhMucSearchModel", searchModel);
            return View(data);
        }

        [HttpPost]
        public JsonResult GetData(int pageIndex, string sortQuery, int pageSize)
        {
            var searchModel =
                (core_DanhMucSearchBO)SessionManager.GetValue("danhMucSearchModel");
            if (searchModel == null) searchModel = new core_DanhMucSearchBO();
            searchModel.pageIndex = pageIndex;
            searchModel.pageSize = pageSize;
            searchModel.sortQuery = sortQuery;

            SessionManager.SetValue("BaoCaoSearchModel", searchModel);
            var data = core_DanhMucBusiness.GetDaTaByPage(searchModel, pageIndex, pageSize);
            return Json(data);
        }
        [HttpPost]

        public JsonResult SearchData(core_DanhMucSearchBO searchModel)
        {
            if (searchModel is null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            var search =
         (core_DanhMucSearchBO)SessionManager.GetValue("danhMucSearchModel");
            search = MapDataHelper<core_DanhMucSearchBO, core_DanhMucSearchBO>.MapData(searchModel);

            SessionManager.SetValue("danhMucSearchModel", search);
            var data = core_DanhMucBusiness.GetDaTaByPage(search, 1, searchModel.pageSize);
            data.PageIndex = 1;
            data.PageSize = 20;
            return Json(data);
        }
    }
}
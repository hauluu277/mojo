using mojoportal.CoreHelpers;
using mojoportal.Service.CommonBusiness;
using mojoPortal.Model.Data;
using mojoPortal.Service.Business;
using mojoPortal.Service.CommonModel.Site;
using mojoPortal.Web.Areas.SettingServiceArea.Models;
using mojoPortal.Web.Base;
using System;
using System.Linq;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.SettingServiceArea.Controllers
{
    public class SettingServiceController : BaseController
    {
        // GET: SettingServiceArea/SettingService

        public SettingServiceController()
        {
            siteBusiness = Get<SiteBusiness>();
            settingServiceBusiness = Get<SettingServiceBusiness>();
            categoryBusiness = Get<CategoryBusiness>();
        }
        public ActionResult Index()
        {
            SettingServiceIndexVM model = new SettingServiceIndexVM();
            model.ListData = siteBusiness.GetPageReport(null);
            var searchModel = new SiteSearchBO();
            searchModel.pageIndex = 1;
            searchModel.pageSize = 20;
            SessionManager.SetValue("SettingServiceSearchModel", searchModel);
            ViewBag.ListLinhVuc = categoryBusiness.GetChildItem(WebConfigSettings.DM_LinhVucDieuTra, 1);
            return View(model);
        }

        public PartialViewResult FormSetting(int id = 0, int siteId = 0)
        {
            FormSettingServiceVM model = new FormSettingServiceVM();
            if (id > 0)
            {
                var search = settingServiceBusiness.FindIfNull(id);
                model.IsNew = search.IsNew;
                model = MaperData.Map<FormSettingServiceVM, core_SettingService>(model, search);
            }
            model.SiteID = siteId;
            return PartialView("_FormSetting", model);
        }
        [HttpPost]
        public JsonResult SaveForm(FormSettingServiceVM model)
        {
            var result = new JsonResultBO(true);
            try
            {
                var listIsNew = settingServiceBusiness.GetAllAsQueryable().Where(x => x.IsNew == true && x.SiteID == model.SiteID).ToList();
                result.Message = "Thêm mới webservice/api thành công";
                var setting = new core_SettingService();
                if (model.IsNew == true && listIsNew != null && listIsNew.Any())
                {
                    foreach (var item in listIsNew)
                    {
                        var settingid = settingServiceBusiness.FindIfNull(item.ItemID);
                        settingid.IsNew = false;
                        settingServiceBusiness.Save(settingid);
                    }
                }
                if (model.ItemID > 0)
                {
                    result.Message = "Cập nhật webservice/api thành công";
                    setting = settingServiceBusiness.FindIfNull(model.ItemID);

                    setting.IsNew = model.IsNew;
                    setting.EditedBy = siteUser.UserId;
                    setting.EditedDate = DateTime.Now;
                }
                else
                {
                    setting.IsNew = model.IsNew;
                    setting.CreatedBy = siteUser.Name;
                    setting.CreatedByUser = siteUser.UserId;
                    setting.CreatedDate = DateTime.Now;
                }
                bool isUpdateAPI = false;
                if (setting.ServiceUrl==null || setting.ServiceUrl.Equals(model.ServiceUrl) == false)
                {
                    isUpdateAPI = true;
                }
                setting = MaperData.Map<core_SettingService, FormSettingServiceVM>(setting, model);

                settingServiceBusiness.Save(setting);
            }
            catch
            {
                result.Status = false;
                result.Message = "Không thể thực hiện thao tác này";
            }
            return Json(result);
        }

        public PartialViewResult IndexService(int siteId)
        {
            IndexServiceVM model = new IndexServiceVM();
            model.ListSetting = settingServiceBusiness.GetListBy(siteId);
            model.Site = siteBusiness.GetById(siteId);
            return PartialView("_IndexService", model);
        }

        public PartialViewResult LoadWebservice(int siteId)
        {
            var result = settingServiceBusiness.GetListBy(siteId);
            return PartialView("_ListService", result);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = new JsonResultBO(true);
            try
            {
                settingServiceBusiness.Delete(id);
                settingServiceBusiness.context.SaveChanges();
            }
            catch
            {

                result.Status = false;
            }

            return Json(result);
        }
        [HttpPost]
        public JsonResult GetData(int pageIndex, string sortQuery, int pageSize)
        {
            var searchModel =
                (SiteSearchBO)SessionManager.GetValue("SettingServiceSearchModel");
            if (searchModel == null) searchModel = new SiteSearchBO();
            searchModel.pageIndex = pageIndex;
            searchModel.pageSize = pageSize;
            searchModel.sortQuery = sortQuery;

            SessionManager.SetValue("SettingServiceSearchModel", searchModel);
            var data = siteBusiness.GetPageReport(searchModel, pageIndex, pageSize);
            return Json(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SearchData(SiteSearchBO searchModel)
        {
            var search =
         (SiteSearchBO)SessionManager.GetValue("SettingServiceSearchModel");
            search = MaperData.MapAllowNull<SiteSearchBO, SiteSearchBO>(search, searchModel);

            SessionManager.SetValue("SettingServiceSearchModel", search);
            var data = siteBusiness.GetPageReport(search, 1, search.pageSize);
            return Json(data);
        }



    }
}
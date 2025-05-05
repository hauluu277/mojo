using mojoportal.CoreHelpers;
using mojoportal.Service.CommonBusiness;
using mojoPortal.Model.Data;
using mojoPortal.Service.Business;
using mojoPortal.Web.Areas.SettingSSOArea.Models;
using mojoPortal.Web.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.SettingSSOArea.Controllers
{
    public class SettingSSOController : BaseController
    {
        // GET: SettingSSOArea/SettingSSO
        public ActionResult Index()
        {
            var settingSSOBussiness = new SettingSSOBusiness(new mojoportal.Service.UoW.UnitOfWork());
            FormSettingSSO model = new FormSettingSSO();
            var getSettingSSO = settingSSOBussiness.GetFirst();
            model = MaperData.MapAllowNull<FormSettingSSO, core_SettingSSO>(model, getSettingSSO);
            ViewBag.ListTheme = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text="Mặc định",
                    Value="",
                    Selected=(model.TypeTheme == "")
                },
                new SelectListItem()
                {
                    Text="Ẩn form login",
                    Value="hide-form-login",
                    Selected=(model.TypeTheme == "hide-form-login")
                }
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveForm(FormSettingSSO form, FormCollection formCollection)
        {
            var result = new JsonResultBO(true);
            try
            {
                var setting = new core_SettingSSO();
                setting = MaperData.MapAllowNull<core_SettingSSO, FormSettingSSO>(setting, form);
                var settingSSOBussiness = new SettingSSOBusiness(new mojoportal.Service.UoW.UnitOfWork());
                settingSSOBussiness.Save(setting);
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
            }

            return Json(result);
        }
    }
}
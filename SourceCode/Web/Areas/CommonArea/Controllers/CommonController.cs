using mojoportal.Service.CommonBusiness;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.Base;
using mojoPortal.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.CommonArea.Controllers
{
    public class CommonController : BaseController
    {
        // GET: CommonArea/Common
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UpdateFooter(int siteId)
        {
            var sites = new SiteSettings(siteId);
            ViewBag.SiteID = siteId;
            ViewBag.Footer = sites.Footer;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public JsonResult SaveFooter(FormCollection formCollection)
        {
            var result = new JsonResultBO(true);
            result.Message = "Cập nhật Footer thành công";
            SiteSettings.UpdateFooter(formCollection["SiteID"].ToIntOrZero(), formCollection["Footer"]);
            return Json(result);
        }

        [HttpPost]
        public string GetUrlItem(string name)
        {
            try
            {
                var result = "/" + name.UrlRewriteDefault();
                return result;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public ActionResult DeleteUser()
        {
            var listUser = SiteUser.GetBySite(1);
            var listUserValid = new List<SiteUser>();
            foreach (var user in listUser)
            {
                if (listUserValid.Any(x=>x.LoginName == user.LoginName && x.Email == user.Email))
                {
                    user.DeleteUserFinal();
                }
                else
                {
                    listUserValid.Add(user);
                }
            }
            return View();
        }

      


    }
}
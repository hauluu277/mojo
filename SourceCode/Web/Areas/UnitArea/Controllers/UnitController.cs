using mojoportal.Service.Business;
using mojoportal.Service.CommonBusiness;
using mojoPortal.Web.Areas.UnitArea.Models;
using mojoPortal.Web.Base;
using System.Linq;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.UnitArea.Controllers
{
    public class UnitController : BaseController
    {
        private UserBusiness userBusiness;

        public UnitController()
        {
            userBusiness = Get<UserBusiness>();
        }
        // GET: UnitArea/Unit
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult CreateUnit()
        {
            return PartialView("_CreateUnit");
        }

        public ActionResult FormUnit()
        {
            UnitFormVM model = new UnitFormVM();
            //model.ListType = ConstantUtilities.GetDropDown<UnitConstant>().ToList();
            model.ListUser = userBusiness.GetBySite(siteSettings.SiteId).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.UserID.ToString()
            }).ToList();
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SaveForm(UnitFormVM model)
        {
            var result = new JsonResultBO(true);
            //var unit = new md_Units();
            //unit.AllowUserEdit = model.AllowUserEdit;
            //unit.CreatedBy = siteUser.UserId;
            //unit.CreatedByUser = siteUser.Name;
            //unit.CreatedDate = DateTime.Now;
            //unit.IsPublished = model.IsPublished;
            //unit.IsShowQuestion = model.IsShowQuestion;
            //unit.ItemUrl = model.ItemUrl;
            //unit.OrderBy = model.OrderBy;
            //unit.SiteID = siteSettings.SiteId;
            //unit.Title = model.Title;
            //unit.Type = model.Type;
            //unitBusiness.Save(unit);
            return Json(result);
        }
    }
}
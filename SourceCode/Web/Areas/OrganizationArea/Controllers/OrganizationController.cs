using mojoportal.CoreHelpers;
using mojoportal.Service.CommonBusiness;
using mojoPortal.Model.Data;
using mojoPortal.Service.Business;
using mojoPortal.Web.Areas.OrganizationArea.Models;
using mojoPortal.Web.Base;
using System;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.OrganizationArea.Controllers
{
    public class OrganizationController : BaseController
    {
        // GET: OrganizationArea/Organization
        public OrganizationController()
        {
            core_CCTC_DepartmentBusiness = Get<core_CCTC_DepartmentBusiness>();
            core_CCTC_LeaderBusiness = Get<core_CCTC_LeaderBusiness>();
            core_CTTCBusiness = Get<core_CTTCBusiness>();
        }
        public ActionResult Index(int siteId = 1)
        {
            OrganizationFormVM model = new OrganizationFormVM();
            var organization = core_CTTCBusiness.GetBySite(siteId);
            model = MaperData.Map<OrganizationFormVM, core_CCTC>(model, organization);
            model.ListDepartment = core_CCTC_DepartmentBusiness.GetListBy(model.ItemID);
            model.ListLeader = core_CCTC_LeaderBusiness.GetListBy(model.ItemID);
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult OrganizationPublish(int siteId = 1)
        {
            OrganizationPublishVM model = new OrganizationPublishVM();
            model.core_CCTC = core_CTTCBusiness.GetBySite(siteId);
            model.ListDepartment = core_CCTC_DepartmentBusiness.GetListBy(model.core_CCTC.ItemID);
            model.ListLeader = core_CCTC_LeaderBusiness.GetListBy(model.core_CCTC.ItemID);
            var isMobileDevice = SiteUtils.IsMobileDevice();
            if (isMobileDevice == true)
            {
                return View("OrganizationPublishMobie", model);
            }
            else
            {
                return View("OrganizationPublish", model);
            }
        }

        [HttpPost]
        public JsonResult SaveOrganization(OrganizationFormVM model, FormCollection formCollection)
        {
            var result = new JsonResultBO(true);
            try
            {
                var oke = formCollection["TitleBoxLanhDao"];
                var organization = new core_CCTC();
                if (model.ItemID > 0)
                {
                    organization = core_CTTCBusiness.FindIfNull(model.ItemID);
                    organization.EditedBy = siteUser.UserId;
                    organization.EditedDate = DateTime.Now;
                }
                else
                {
                    organization.CreatedBy = siteUser.Name;
                    organization.CreatedByUser = siteUser.UserId;
                    organization.CreatedDate = DateTime.Now;
                    organization.SiteID = 1;
                }
                organization = MaperData.Map<core_CCTC, OrganizationFormVM>(organization, model);

                core_CTTCBusiness.Save(organization);
            }
            catch
            {

                result.Status = false;
            }

            return Json(result);
        }

        public PartialViewResult FormLeader(int cctcId = 0, int id = 0)
        {
            var leader = new LeaderFormVM();
            if (id > 0)
            {
                var search_leader = core_CCTC_LeaderBusiness.Find(id);
                if (search_leader != null)
                {
                    leader = MaperData.Map<LeaderFormVM, core_CCTC_Leader>(leader, search_leader);
                }
            }
            leader.CCTC_ID = cctcId;
            return PartialView("_FormLeader", leader);
        }
        [HttpPost]
        public JsonResult SaveLeader(LeaderFormVM model)
        {
            var result = new JsonResultBO(true);
            result.Message = "Thêm mới lãnh đạo thành công";
            try
            {
                var leader = new core_CCTC_Leader();
                if (model.ItemID > 0)
                {
                    result.Message = "Cập nhật lãnh đạo thành công";
                    var search = core_CCTC_LeaderBusiness.Find(model.ItemID);
                    if (search != null)
                    {
                        leader = search;
                    }
                }
                leader = MaperData.Map<core_CCTC_Leader, LeaderFormVM>(leader, model);
                leader.PathIMG = model.PathIMG;
                core_CCTC_LeaderBusiness.Save(leader);
            }
            catch
            {
                result.Message = "Không thể thực hiện thao tác này";
                result.Status = false;
            }

            return Json(result);
        }
        public PartialViewResult TableLeader(int cctcId = 0)
        {
            var result = core_CCTC_LeaderBusiness.GetListBy(cctcId);

            return PartialView("_TableLeader", result);
        }
        public PartialViewResult TableDepartment(int cctcId = 0)
        {
            var result = core_CCTC_DepartmentBusiness.GetListBy(cctcId);

            return PartialView("_TableDepartment", result);
        }

        public PartialViewResult FormDepartment(int cctcId = 0, int id = 0)
        {
            var department = new DepartmentFormVM();
            ViewBag.ListLeader = core_CCTC_LeaderBusiness.GetChildItem();
            if (id > 0)
            {
                var search_department = core_CCTC_DepartmentBusiness.Find(id);
                if (search_department != null)
                {
                    department = MaperData.Map<DepartmentFormVM, core_CCTC_Department>(department, search_department);
                }
            }
            department.CCTC_ID = cctcId;
            return PartialView("_FormDepartment", department);
        }
        [HttpPost]
        public JsonResult SaveDepartment(DepartmentFormVM model)
        {
            var result = new JsonResultBO(true);
            result.Message = "Thêm mới phòng ban thành công";
            try
            {
                var department = new core_CCTC_Department();
                if (model.ItemID > 0)
                {
                    result.Message = "Cập nhật phòng ban thành công";
                    var search = core_CCTC_DepartmentBusiness.Find(model.ItemID);
                    if (search != null)
                    {
                        department = search;
                    }
                }
                department = MaperData.Map<core_CCTC_Department, DepartmentFormVM>(department, model);

                core_CCTC_DepartmentBusiness.Save(department);
            }
            catch
            {
                result.Message = "Không thể thực hiện thao tác này";
                result.Status = false;
            }

            return Json(result);
        }

        [HttpPost]
        public JsonResult DeleteLeader(int id = 0)
        {
            var result = new JsonResultBO(true);
            if (id > 0)
            {
                core_CCTC_LeaderBusiness.Delete(id);
                core_CCTC_LeaderBusiness.context.SaveChanges();
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult DeleteDepartment(int id = 0)
        {
            var result = new JsonResultBO(true);
            if (id > 0)
            {
                core_CCTC_DepartmentBusiness.Delete(id);
                core_CCTC_DepartmentBusiness.context.SaveChanges();
            }
            return Json(result);
        }
    }
}
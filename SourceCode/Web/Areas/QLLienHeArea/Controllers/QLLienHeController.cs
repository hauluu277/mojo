using CKFinder.Connector; 
using GCheckout.Util;
using mojoportal.CoreHelpers;
using mojoportal.Service.CommonBusiness;
using mojoPortal.Model.Data;
using mojoPortal.Net;
using mojoPortal.Service.Business;
using mojoPortal.Service.CommonModel.QLLienHe;
using mojoPortal.Web.Areas.QLLienHeArea.Models;
using mojoPortal.Web.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.QLLienHeArea.Controllers
{
    public class QLLienHeController : BaseController
    {
        public QLLienHeController()
        {
            qlLienHeBusiness = Get<QLLienHeBusiness>();
        }

        // GET: QLLogArea/QLLog
        public ActionResult Index()
        {
            var model = new IndexVM();
            model.ListData = qlLienHeBusiness.GetDaTaByPage(null);
            var searchModel = new QLLienHeSearchBO();
            searchModel.pageIndex = 1;
            searchModel.pageSize = 20;
            SessionManager.SetValue("QLLienHeSearchModel", searchModel);
            ViewBag.user = siteUser;
            return View(model);
        }

        [HttpPost]
        public JsonResult GetData(int pageIndex, string sortQuery, int pageSize)
        {
            var searchModel = (QLLienHeSearchBO)SessionManager.GetValue("QLLienHeSearchModel");
            if (searchModel == null) searchModel = new QLLienHeSearchBO();
            searchModel.pageIndex = pageIndex;
            searchModel.pageSize = pageSize;
            searchModel.sortQuery = sortQuery;

            SessionManager.SetValue("QLLienHeSearchModel", searchModel);
            var data = qlLienHeBusiness.GetDaTaByPage(searchModel, pageIndex, pageSize);
            return Json(data);
        }

        [HttpPost]
        public JsonResult SearchData(QLLienHeSearchBO searchModel)
        {
            if (searchModel is null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            var search = (QLLienHeSearchBO)SessionManager.GetValue("QLLienHeSearchModel");
            search = MapDataHelper<QLLienHeSearchBO, QLLienHeSearchBO>.MapData(searchModel);

            SessionManager.SetValue("QLLienHeSearchModel", search);
            var data = qlLienHeBusiness.GetDaTaByPage(search, 1, searchModel.pageSize);
            data.PageIndex = 1;
            data.PageSize = 20;
            return Json(data);
        }

        public PartialViewResult FormReply(Guid Id)
        {
            var data = qlLienHeBusiness.GetById(Id);
            ViewBag.data = data;
            return PartialView("_FormReply");
        }

        public PartialViewResult FormDetail(Guid Id)
        {
            var data = qlLienHeBusiness.GetById(Id);
            ViewBag.data = data;
            return PartialView("_FormDetail");
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public JsonResult SavePhanHoi(ReplyVM model, FormCollection formCollection)
        {
            var result = new JsonResultBO(true);
            try
            {
                result.Message = "Phản hồi thành công";
                var contactMessage = qlLienHeBusiness.GetById(model.RowGuid);
                if (string.IsNullOrEmpty(model.RowGuid.ToString()))
                {
                    result.Message = "Phản hồi không thành công";
                }
                else
                {
                    //Gửi Email
                    SmtpSettings smtpSettings = SiteUtils.GetSmtpSettings();
                    string fromAddress = siteSettings.DefaultEmailFromAddress;
                    string to = contactMessage.Email;
                    try
                    {
                        Email.SendEmailSingle(
                            to,
                            contactMessage.Url,
                            model.Message,
                            model.TieuDe);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message, ex);
                    }

                    //thay đổi trạng thái
                    contactMessage.ThoiGianPhanHoi = DateTime.Now;
                    contactMessage.TrangThai = 1;
                    contactMessage.NoiDungPhanHoi = model.Message;
                    qlLienHeBusiness.Save(contactMessage);
                }
            }
            catch (Exception ex)
            {
                result.Message = "Không thể thực hiện thao tác này";
                result.Status = false;
            }
            return Json(result);
        }
    }
}
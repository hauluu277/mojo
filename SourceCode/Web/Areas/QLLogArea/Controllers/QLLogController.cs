using mojoportal.CoreHelpers;
using mojoportal.Service.CommonBusiness;
using mojoPortal.Model.Data;
using mojoPortal.Service.Business;
using mojoPortal.Service.CommonModel.QLLog;
using mojoPortal.Web.Areas.QLLogArea.Models;
using mojoPortal.Web.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.QLLogArea.Controllers
{
    public class QLLogController : BaseController
    {
        List<SelectListItem> listConfig = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Value = "DiaChiIP",
                Text = "Địa chỉ IP"
            },
            new SelectListItem
            {
                Value = "LoaiLog",
                Text = "Loại Log"
            },
            new SelectListItem
            {
                Value = "HanhDongThaoTac",
                Text = "Hành động thao tác"
            },
            new SelectListItem
            {
                Value = "NoiDung",
                Text = "Nội dung"
            },
            new SelectListItem
            {
                Value = "DuongDanThaoTac",
                Text = "Đường dẫn thao tác"
            },
            new SelectListItem
            {
                Value = "NguoiThaoTac",
                Text = "Người thao tác"
            },
            new SelectListItem
            {
                Value = "ThoiGian",
                Text = "Thời gian"
            }
        };

        public QLLogController()
        {
            qlLogBusiness = Get<QLLogBusiness>();
            cauHinhHienThiLogBusiness = Get<CauHinhHienThiLogBusiness>();

        }

        // GET: QLLogArea/QLLog
        public ActionResult Index()
        {
            var model = new IndexVM();
            model.ListData = qlLogBusiness.GetDaTaByPage(null);
            var searchModel = new QLLogSearchBO();
            searchModel.pageIndex = 1;
            searchModel.pageSize = 20;
            SessionManager.SetValue("QLLogSearchModel", searchModel);
            var lstConfigUser = cauHinhHienThiLogBusiness
                    .GetAllAsQueryable()
                    .Where(x => x.CreateByUser == siteUser.UserId)
                    .ToList();
            ViewBag.ConfigUser = lstConfigUser.Count() > 0 ? lstConfigUser : new List<core_CauHinhHienThiLog>();
            ViewBag.user = siteUser;
            return View(model);
        }

        [HttpPost]
        public JsonResult GetData(int pageIndex, string sortQuery, int pageSize)
        {
            var searchModel =
                (QLLogSearchBO)SessionManager.GetValue("QLLienHeSearchModel");
            if (searchModel == null) searchModel = new QLLogSearchBO();
            searchModel.pageIndex = pageIndex;
            searchModel.pageSize = pageSize;
            searchModel.sortQuery = sortQuery;

            SessionManager.SetValue("QLLogSearchModel", searchModel);
            var data = qlLogBusiness.GetDaTaByPage(searchModel, pageIndex, pageSize);
            return Json(data);
        }

        [HttpPost]
        public JsonResult SearchData(QLLogSearchBO searchModel)
        {
            if (searchModel is null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            var search = (QLLogSearchBO)SessionManager.GetValue("QLLienHeSearchModel");
            search = MapDataHelper<QLLogSearchBO, QLLogSearchBO>.MapData(searchModel);

            SessionManager.SetValue("QLLienHeSearchModel", search);
            var data = qlLogBusiness.GetDaTaByPage(search, 1, searchModel.pageSize);
            data.PageIndex = 1;
            data.PageSize = 20;
            return Json(data);
        }

        public PartialViewResult FormConfig(int id = 0)
        {
            ConfigVM model = new ConfigVM();
            var lstConfigUser = new List<SelectListItem>();
            var lstCauHinh = cauHinhHienThiLogBusiness.GetAllAsQueryable();

            foreach (var item in listConfig)
            {
                var config = new SelectListItem()
                {
                    Value = item.Value,
                    Text = item.Text,
                    Selected = lstCauHinh.Any(x => x.CreateByUser == siteUser.UserId && item.Value.Contains(x.MaTruongHienThi) && x.IsShow == true)
                };
                lstConfigUser.Add(config);
            }

            model.listLoaiLog = lstConfigUser;
            return PartialView("_ConfigPartial", model);
        }

        [HttpPost]
        public JsonResult SaveConfig(ConfigVM model)
        {
            var result = new JsonResultBO(true);
            try
            {
                //update tất cả về dạng isShow = false
                var getConfigUser = cauHinhHienThiLogBusiness
                    .GetAllAsQueryable()
                    .Where(x => x.CreateByUser == siteUser.UserId)
                    .ToList();
                if (getConfigUser.Count() > 0)
                {
                    foreach (var item in getConfigUser)
                    {
                        item.IsShow = false;
                        cauHinhHienThiLogBusiness.Save(item);
                    }
                }

                foreach (var item in model.configItem)
                {
                    var datacheck = cauHinhHienThiLogBusiness.GetByMaAndUser(item, siteUser.UserId);
                    if (datacheck != null)
                    {
                        datacheck.IsShow = true;
                        datacheck.EditDate = DateTime.Now;
                        datacheck.EditByUser = siteUser.UserId;
                        cauHinhHienThiLogBusiness.Save(datacheck);
                    }
                    else
                    {
                        var newItem = new core_CauHinhHienThiLog()
                        {
                            TruongHienThi = listConfig.Where(x => x.Value == item).Select(x => x.Text).FirstOrDefault(),
                            MaTruongHienThi = item,
                            IsShow = true,
                            CreateBy = siteUser.Name,
                            CreateByUser = siteUser.UserId,
                            CreatedDate = DateTime.Now,
                            EditDate = DateTime.Now
                        };
                        cauHinhHienThiLogBusiness.Save(newItem);
                    }
                }
                result.Message = "Cấu hình thành công";
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
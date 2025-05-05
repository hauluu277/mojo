using mojoPortal.Web.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mojoPortal.Web.Areas.LyLichNewArea.Models;
using mojoportal.Service.CommonBusiness;
using mojoPortal.Model.Data;
using mojoPortal.Service.Business;

namespace mojoPortal.Web.Areas.LyLichNewArea.Controllers
{
    public class LyLichNewController : BaseController
    {
        private readonly md_LichCongTacNewBusiness _md_LichCongTacNewBusiness;
        public LyLichNewController()
        {
            _md_LichCongTacNewBusiness = Get<md_LichCongTacNewBusiness>();
        }

        // GET: LyLichNewArea/LyLichNew
        public ActionResult Index()
        {

            var data = _md_LichCongTacNewBusiness.getDataAll();

            return View();
        }


        public ActionResult FormClient(int id = 0)
        {
            

            return PartialView("_Form");
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SaveForm(FormVM model)
        {
            var result = new JsonResultBO(true);

            try
            {
                result.Message = "Thêm mới lịch công tác thành công";
                    var objnew = new md_LichCongTacNew();

                if (model.ItemID > 0)
                {
                }
                else
                {
                    objnew.Nam = model.Nam;
                    objnew.Week = model.Week;
                    objnew.Thu =  "Thứ " + model.Thu;
                    objnew.ThoiGian = model.ThoiGian;
                    objnew.NoiDung = model.NoiDung;
                    objnew.DiaDiem = model.DiaDiem;
                    objnew.ThanhPhanThamDu = model.ThanhPhanThamDu;
                    objnew.StartDate = model.StartDate;


                    _md_LichCongTacNewBusiness.Save(objnew);
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
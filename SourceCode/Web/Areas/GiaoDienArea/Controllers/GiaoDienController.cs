using CommonHelper.Upload;
using mojoportal.CoreHelpers;
using mojoportal.Service.CommonBusiness;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features.Business.QLLog;
using mojoPortal.Model.Entities;
using mojoPortal.Service.Business;
using mojoPortal.Service.CommonModel.GiaoDien;
using mojoPortal.Web.Areas.GiaoDienArea.Models;
using mojoPortal.Web.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.GiaoDienArea.Controllers
{
    public class GiaoDienController : BaseController
    {

        public GiaoDienController()
        {
            giaoDienBusiness = Get<GiaoDienBusiness>();

        }

        // GET: GiaoDienArea/GiaoDien
        public ActionResult Index()
        {

            GiaoDienIndexVM model = new GiaoDienIndexVM();
            model.ListData = giaoDienBusiness.GetDaTaByPage(null);
            var searchModel = new GiaoDienSearchBO();
            searchModel.pageIndex = 1;
            searchModel.pageSize = 20;
            SessionManager.SetValue("GiaoDienSearchModel", searchModel);
            return View(model);
        }

        [HttpPost]
        public JsonResult GetData(int pageIndex, string sortQuery, int pageSize)
        {
            var searchModel =
                (GiaoDienSearchBO)SessionManager.GetValue("GiaoDienSearchModel");
            if (searchModel == null) searchModel = new GiaoDienSearchBO();
            searchModel.pageIndex = pageIndex;
            searchModel.pageSize = pageSize;
            searchModel.sortQuery = sortQuery;

            SessionManager.SetValue("GiaoDienSearchModel", searchModel);
            var data = giaoDienBusiness.GetDaTaByPage(searchModel, pageIndex, pageSize);
            return Json(data);
        }

        [HttpPost]
        public JsonResult SearchData(GiaoDienSearchBO searchModel)
        {
            if (searchModel is null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            var search =
         (GiaoDienSearchBO)SessionManager.GetValue("GiaoDienSearchModel");
            search = MapDataHelper<GiaoDienSearchBO, GiaoDienSearchBO>.MapData(searchModel);

            SessionManager.SetValue("GiaoDienSearchModel", search);
            var data = giaoDienBusiness.GetDaTaByPage(search, 1, searchModel.pageSize);
            data.PageIndex = 1;
            data.PageSize = 20;

            return Json(data);
        }

        public PartialViewResult FormGiaoDien(int id = 0)
        {
            var listLinhVuc = new List<SelectListItem>();
            GiaoDienFormVM model = new GiaoDienFormVM();
            if (id > 0)
            {
                var report = giaoDienBusiness.GetByItemId(id);
                model = MapDataHelper<GiaoDienFormVM, core_GiaoDien>.MapData(report);
            }

            return PartialView("_FormGiaoDien", model);
        }

        [HttpPost]
        public JsonResult SaveChonGiaoDien(string maGiaoDien)
        {
            var result = new JsonResultBO(true);
            siteSettings.Skin = maGiaoDien;
            siteSettings.Save();
            return Json(result);
        }



        [HttpPost]
        public JsonResult SaveForm(GiaoDienFormVM model, HttpPostedFileBase filebase)
        {
            var result = new JsonResultBO(true);
            var report = new core_GiaoDien();
            try
            {
                var datacheck = giaoDienBusiness.CheckTrungMa(model.MaGiaoDien);
                if (datacheck)
                {
                    model.MaGiaoDien = model.MaGiaoDien + DateTime.Now.ToString("ddMMyyyyHHmm");
                }
                result.Message = "Thêm mới thông tin giao diện thành công";
                var pathSave = Server.MapPath("/") + "\\Data\\sites\\1\\skins\\" + model.MaGiaoDien;
                var DuongDanZip = "";
                if (filebase != null && filebase.ContentLength > 0)
                {
                    var data = UploadProvider.SaveFile(filebase, null, UploadProvider.ListExtensionCommonWinRar, null, "GiaoDienNenWinrar", Server.MapPath("/Uploads"));
                    if (data.extension != "zip")
                    {
                        result.Status = false;
                        result.Message = "Vui lòng nén file dạng zip và tải lại.";
                        return Json(result);
                    }
                    #region Kiểm tra có lưu thư mục 'pathSave' không. Nếu chưa có thì tạo

                    var pathFolder = "";
                    if (string.IsNullOrEmpty(model.MaGiaoDien))
                    {
                        model.MaGiaoDien = "Unknow";
                    }
                    var path = Server.MapPath("/") + "/Data/sites/1/skins/";
                    pathFolder = Path.Combine(path, model.MaGiaoDien);

                    //Kiểm tra folder đã tồn tại chưa. Nếu chưa tồn tại rồi thì tạo mới
                    if (!Directory.Exists(pathFolder))
                    {
                        try
                        {
                            Directory.CreateDirectory(pathFolder);
                        }
                        catch
                        {
                            result.Status = false;
                            result.Message = "Không tạo được folder";
                            return Json(result);
                        }

                    }
                    else
                    {
                        var di = new DirectoryInfo(pathFolder);
                        foreach (FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo dir in di.GetDirectories())
                        {
                            dir.Delete(true);
                        }
                    }

                    #endregion
                    DuongDanZip = data.path;
                    //Giải nén file và lưu lại vào thư mục: /Data/sites/1/skins/Mã viết không dấu
                    if (data.extension == "zip")
                    {
                        System.IO.Compression.ZipFile.ExtractToDirectory(data.fullPath, pathSave);
                    }
                }

                QLLog qlLog = new QLLog()
                {
                    DiaChiIP = Common.findMyIP().ToString(),
                    Type = KieuLogConstant.LogThayDoiGiaoDien,
                    DuongDanThaoTac = "/globalmodule/giaodien/managegiaodien.aspx"
                };
                

                report = new core_GiaoDien();
                if (model.ItemID > 0)
                {
                    qlLog.HanhDongThaoTac = KieuLogConstant.CapNhapDuLieu;
                    qlLog.NoiDung = "Cập nhập giao diện Id: " + model.ItemID;
                    qlLog.Save();


                    report = giaoDienBusiness.GetByItemId(model.ItemID);
                    result.Message = "Cập nhật thông tin giao diện thành công";
                }
                else
                {
                    qlLog.HanhDongThaoTac = KieuLogConstant.ThemMoi;
                    qlLog.NoiDung = "Thêm mới giao diện: " + model.TenGiaoDien;
                    qlLog.CreatedBy = siteUser.Name;
                    qlLog.CreatedByUser = siteUser.UserId;
                    qlLog.CreatedDate = DateTime.Now;
                    qlLog.Save();

                    report.CreatedBy = siteUser.Name;
                    report.CreatedByUser = siteUser.UserId;
                    report.CreatedDate = DateTime.Now;
                    report.SiteID = siteSettings.SiteId;
                }
                //get rewrite url product
                report = MaperData.Map<core_GiaoDien, GiaoDienFormVM>(report, model);
                report.DuongDan = "/Data/sites/1/skins/" + model.MaGiaoDien + "/";
                report.DuongDanZipTaiLen = DuongDanZip;
                giaoDienBusiness.Save(report);


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
                QLLog qlLog = new QLLog()
                {
                    DiaChiIP = Common.findMyIP().ToString(),
                    Type = KieuLogConstant.LogThayDoiGiaoDien,
                    DuongDanThaoTac = "/globalmodule/giaodien/managegiaodien.aspx",
                    HanhDongThaoTac = KieuLogConstant.XoaDuLieu,
                    NoiDung = "Xóa giao diện: " + id,
                    CreatedBy = siteUser.Name,
                    CreatedByUser = siteUser.UserId,
                    CreatedDate = DateTime.Now
                };

                result.Message = "Xóa giao diện thành công!";
                giaoDienBusiness.Delete(id);
                giaoDienBusiness.context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Message = "Không thực hiện được thao tác này";
                result.Status = false;
            }
            return Json(result);
        }

        public PartialViewResult DetailGiaoDien(long id)
        {
            var model = giaoDienBusiness.GetByItemId(id);
            var pathFolder = Server.MapPath("/") + model.DuongDan;
            var di = new DirectoryInfo(pathFolder);
            var Tree = new ThongTinChiTietVM();
            Tree.TreeFolderThem = new List<FileOrFoderTree>();
            var getFolder = di.GetDirectories();
            // get all file and folder cấp 1
            if (getFolder != null)
            {

                var lstFolder = GetFolder(model.MaGiaoDien);
                Tree.TreeFolderThem = lstFolder;

                // get all file and folder cấp 2
                foreach (var data in Tree.TreeFolderThem)
                {
                    data.children = new List<FileOrFoderTree>();
                    if (data.type == "folder")
                    {
                        var PathFile = data.DuongdanFolder;
                        data.children = GetFolder(PathFile);
                        // Get cấp 3
                        foreach (var data3 in data.children)
                        {
                            data3.children = new List<FileOrFoderTree>();
                            if (data3.type == "folder")
                            {
                                var PathFile3 = data3.DuongdanFolder;
                                data3.children = GetFolder(PathFile3);

                                // Get cấp 4
                                foreach (var data4 in data3.children)
                                {
                                    data4.children = new List<FileOrFoderTree>();
                                    if (data4.type == "folder")
                                    {
                                        var PathFile4 = data4.DuongdanFolder;
                                        data4.children = GetFolder(PathFile4);

                                        // Get cấp 5
                                        foreach (var data5 in data4.children)
                                        {
                                            data5.children = new List<FileOrFoderTree>();
                                            if (data5.type == "folder")
                                            {
                                                var PathFile5 = data5.DuongdanFolder;
                                                data5.children = GetFolder(PathFile5);


                                                // Get cấp 6
                                                foreach (var data6 in data5.children)
                                                {
                                                    data6.children = new List<FileOrFoderTree>();
                                                    if (data6.type == "folder")
                                                    {
                                                        var PathFile6 = data6.DuongdanFolder;
                                                        data6.children = GetFolder(PathFile6);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            ViewBag.IdGiaoDien = id;
            return PartialView(Tree);
        }

        private List<FileOrFoderTree> GetFolder(string PathFolder)
        {
            var dataTheoCap = new List<FileOrFoderTree>();
            var datapath = Server.MapPath("/") + "\\Data\\sites\\1\\skins\\" + PathFolder;
            var di = new DirectoryInfo(datapath);
            foreach (FileInfo file in di.GetFiles())
            {
                var dataTree = new FileOrFoderTree();

                dataTree.name = file.Name;
                dataTree.text = file.Name;
                dataTree.DuoiFile = file.Extension;
                dataTree.type = "";
                dataTree.DuongdanCha = PathFolder;
                dataTheoCap.Add(dataTree);
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                var dataTree = new FileOrFoderTree();
                dataTree.name = dir.Name;
                dataTree.text = dir.Name;
                dataTree.DuoiFile = dir.Extension;
                dataTree.type = "folder";
                dataTree.selected = true;
                dataTree.open = false;
                dataTree.DuongdanFolder = PathFolder + "/" + dataTree.name;
                dataTree.DuongdanCha = PathFolder;
                dataTheoCap.Add(dataTree);
            }

            return dataTheoCap;
        }

        [HttpPost]
        public PartialViewResult getDataFile(string nameFile, string duongdan)
        {
            var pathFolder = Server.MapPath("/") + "\\Data\\sites\\1\\skins\\" + duongdan + "\\" + nameFile;
            var data = new DaTaFileVM();
            String line;
            try
            {
                var arrName = nameFile.Split('.');
                var extention = '.' + arrName[arrName.Length - 1];

                data.pathFileData = "/Data/sites/1/skins/" + duongdan + "/" + nameFile;
                data.nameFile = nameFile;
                data.duongdan = duongdan;
                if (extention == ".jpg" || extention == ".png" || extention == ".jpeg")
                {
                    data.isImg = true;
                    data.Data = "/Data/sites/1/skins/" + duongdan + "/" + nameFile;
                   
                    return PartialView(data);
                }
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(pathFolder);

                //Read the first line of text
                line = sr.ReadToEnd();
                //Continue to read until you reach end of file

                data.Data = line;
                //close the file
                sr.Close();

            }
            catch (Exception e)
            {
                throw new Exception("Lỗi đã xảy ra: ", e);
            }
            return PartialView(data);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult saveFileData(string datasave, string pathFile)
        {
            var result = new JsonResultBO(true);
            try
            {
                var pathFolder = Server.MapPath("/") + pathFile;
                System.IO.File.Delete(pathFolder);
                System.IO.File.WriteAllText(pathFolder, datasave);
                result.Message = "Cập nhật thành công!";
                result.Status = true;

                QLLog qlLog = new QLLog()
                {
                    DiaChiIP = Common.findMyIP().ToString(),
                    Type = KieuLogConstant.LogThayDoiGiaoDien,
                    DuongDanThaoTac = "/globalmodule/giaodien/managegiaodien.aspx",
                    HanhDongThaoTac = KieuLogConstant.ThayDoiGiaoDien,
                    NoiDung = "Thay đổi nội dung File giao diện: " + pathFile,
                    CreatedBy = siteUser.Name,
                    CreatedByUser = siteUser.UserId,
                    CreatedDate = DateTime.Now
                };
                qlLog.Save();

            }
            catch (Exception ex)
            {
                result.Message = "Không thực hiện được thao tác này";
                result.Status = false;
            }

            return Json(result);
        }

    }

}
using CommonHelper.ObjectExtend;
using CommonHelper.Upload;
using DocumentFormat.OpenXml.Packaging;
using mojoportal.CoreHelpers;
using mojoportal.CoreHelpers.Ultilities;
using mojoportal.Service.CommonBusiness;
using mojoPortal.Model.Data;
using mojoPortal.Service.Business;
using mojoPortal.Service.CommonModel.BieuMauThongTin;
using mojoPortal.Web.Areas.BieuMauThongTinArea.Data;
using mojoPortal.Web.Base;
using Novacode;
using OpenXmlPowerTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Xml.Linq;

namespace mojoPortal.Web.Areas.BieuMauThongTinArea.Controllers
{
    public class BieuMauThongTinController : BaseController
    {
        string SearchKey = "SearchBieuMauThongTin";
        string SearchKeyNopBieuMau = "SearchNopBieuMauThongTin";
        public BieuMauThongTinController()
        {
            BieuMauThongTinBusiness = Get<BieuMauThongTinBusiness>();
            TieuChiBieuMauBusiness = Get<TieuChiBieuMauBusiness>();
            KeKhaiBieuMauBusiness = Get<KeKhaiBieuMauBusiness>();
            NopBieuMauBusiness = Get<NopBieuMauBusiness>();
        }

        // GET: BieuMauThongTinArea/BieuMauThongTin
        public ActionResult Index()
        {
            SessionManager.ResetValue(SearchKey);
            var viewModel = new BieuMauThongTinListViewModel()
            {
                DanhSachBieuMauThongTin = BieuMauThongTinBusiness.GetDaTaByPage(searchModel: null)
            };
            return View(viewModel);
        }

        //tim kiem
        [HttpPost]
        public JsonResult SearchData(BieuMauThongTinSearchBO searchModel)
        {
            if (searchModel is null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            var search = SessionManager.GetValue(SearchKey) as BieuMauThongTinSearchBO;
            search = MapDataHelper<BieuMauThongTinSearchBO, BieuMauThongTinSearchBO>.MapData(searchModel);

            SessionManager.SetValue(SearchKey, search);
            var data = BieuMauThongTinBusiness.GetDaTaByPage(search, 1);
            data.PageIndex = 1;
            data.PageSize = 20;
            return Json(data);
        }


        //phan trang
        [HttpPost]
        public JsonResult GetData(int pageIndex, string sortQuery, int pageSize)
        {
            var searchModel =
                (BieuMauThongTinSearchBO)SessionManager.GetValue(SearchKey);
            if (searchModel == null) searchModel = new BieuMauThongTinSearchBO();
            searchModel.PageIndex = pageIndex;
            searchModel.PageSize = pageSize;
            searchModel.SortQuery = sortQuery;

            SessionManager.SetValue(SearchKey, searchModel);
            var data = BieuMauThongTinBusiness.GetDaTaByPage(searchModel, pageIndex, pageSize);
            return Json(data);
        }

        //them/sua
        public PartialViewResult EditBieuMauThongTin(long id = 0)
        {
            var info = BieuMauThongTinBusiness.GetAllAsQueryable()
                .Where(bm => bm.Id == id).FirstOrDefault() ?? new bentre_BieuMauThongTin();
            var viewModel = new BieuMauThongTinEditViewModel()
            {
                Id = info.Id,
                Ten = info.Ten,
                Path = info.Path,
                IsShow = info.IsShow
            };
            return PartialView("_EditBieuMauThongTin", viewModel);
        }

        [HttpPost]
        public JsonResult SaveBieuMauThongTin(BieuMauThongTinEditViewModel model)
        {
            var result = new JsonResultBO(true, "Cập nhật thông tin biểu mẫu thành công");
            try
            {
                if (!ModelState.IsValid)
                {
                    result.MessageFail("Cập nhật thông tin biểu mẫu thất bại");
                    return Json(result);
                }

                var isExisted = BieuMauThongTinBusiness.GetAllAsQueryable()
                    .Where(bm => bm.Ten == model.Ten && bm.Id != model.Id)
                    .Any();
                if (isExisted)
                {
                    result.MessageFail($"Biểu mẫu {model.Ten} đã tồn tại.");
                    return Json(result);
                }

                //kiểm tra đường dẫn file
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    var arrName = file.FileName.Split('.');
                    var extension = arrName[arrName.Length - 1];

                    var extention = '.' + extension;

                    var listExtention = UploadProvider.ListExtensionCommonDoc.Split(',');
                    if (!listExtention.Contains(extention.ToLower()))
                    {
                        result.MessageFail("Định dạng file không được chấp nhận");
                        return Json(result);
                    }
                }


                var infoBieuMau = BieuMauThongTinBusiness.GetAllAsQueryable()
                .Where(bm => bm.Id == model.Id).FirstOrDefault() ?? new bentre_BieuMauThongTin();
                infoBieuMau.Ten = model.Ten;
                infoBieuMau.NgayCapNhat = DateTime.Now;
                if (model.Id == 0)
                {
                    infoBieuMau.NgayTao = DateTime.Now;
                }
                infoBieuMau.IsShow = model.IsShow;
                BieuMauThongTinBusiness.Save(infoBieuMau);

                if (Request.Files.Count > 0)
                {
                    var fileBieuMau = Request.Files[0];

                    var uploadResult = UploadProvider.SaveFile(fileBieuMau, fileBieuMau.FileName, UploadProvider.ListExtensionCommonDoc,
                        null, "BieuMauThongTin", Server.MapPath("/Uploads"));

                    //lấy thông tin của biểu mẫu
                    var filePath = uploadResult.fullPath;
                    var docx = DocX.Load(filePath);
                    var countTable = docx.Tables.Count;
                    for (int i = 0; i < countTable; i++)
                    {
                        Novacode.Table t = docx.Tables[i];
                        if (t.TableCaption != null)
                        {
                            var rowcount = t.RowCount;
                            for (int k = rowcount - 1; k >= 1; k--)
                            {
                                t.RemoveRow(k);
                            }

                            t.InsertRow(1);
                            Novacode.Row myRow = t.Rows[1];
                            for (int j = 0; j < t.ColumnCount; j++)
                            {

                                var key_name = "[[ISTABLE_" + Ultilities.RemoveUnicode((t.TableCaption + "_" + t.Paragraphs[j].Text))
                                    .ToUpper().Replace(" ", "_") + "]]";
                                myRow.Cells[j].Paragraphs.First().InsertText(" " + key_name);
                            }
                        }
                    }
                    docx.SaveAs(filePath);
                    byte[] byteArray = System.IO.File.ReadAllBytes(filePath);

                    //cập nhật chứng thư
                    using (var memoryStream = new MemoryStream())
                    {
                        memoryStream.Write(byteArray, 0, byteArray.Length);
                        using (var doc = WordprocessingDocument.Open(memoryStream, true))
                        {
                            string documentText;
                            using (StreamReader reader = new StreamReader(doc.MainDocumentPart.GetStream()))
                            {
                                documentText = reader.ReadToEnd();
                            }
                            documentText = documentText.Replace("##date##", DateTime.Today.ToShortDateString());
                            using (StreamWriter writer = new StreamWriter(doc.MainDocumentPart.GetStream(FileMode.Create)))
                            {
                                writer.Write(documentText);
                            }
                            int imageCounter = 0;
                            var settings = new HtmlConverterSettings()
                            {
                                PageTitle = infoBieuMau.Ten,
                            };
                            XElement html = HtmlConverter.ConvertToHtml(doc, settings);
                            infoBieuMau.NoiDungHTML = html.ToString();
                            var regex = new Regex(@"\[\[[\w\.]*\]\]");
                            var matches = regex.Matches(infoBieuMau.NoiDungHTML);
                            var tmpMatches = matches.Cast<Match>().Select(m => m.Value.Trim()).ToArray();
                            infoBieuMau.Keys = string.Join(",", tmpMatches);
                            infoBieuMau.Keys = infoBieuMau.Keys.Replace("[[", "");
                            infoBieuMau.Keys = infoBieuMau.Keys.Replace("]]", "");
                            infoBieuMau.Path = uploadResult.path;
                            BieuMauThongTinBusiness.Save(infoBieuMau);
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                result.Message = "Không thể thực hiện thao tác này";
                result.Status = false;
            }
            return Json(result);
        }

        //xoa
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = new JsonResultBO(true, "Xóa biểu mẫu thông tin thành công");
            try
            {
                BieuMauThongTinBusiness.Delete(id);
                BieuMauThongTinBusiness.context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.MessageFail("Không thực hiện được thao tác");
            }
            return Json(result);
        }

        public ActionResult Detail(int id)
        {
            var infoBieuMau = BieuMauThongTinBusiness.GetAllBySqlQuery()
                .Where(bm => bm.Id == id)
                .FirstOrDefault() ?? new bentre_BieuMauThongTin();

            var viewModel = new BieuMauThongTinConfigViewModel()
            {
                InfoBieuMau = infoBieuMau,
                ListTieuChi = TieuChiBieuMauBusiness.GetAllBySqlQuery().Where(tc => tc.IdBieuMau == id)
                .ToList()
            };
            return View(viewModel);
        }


        public PartialViewResult SettingField(string key, int idBieuMau)
        {
            var infoTieuChi = TieuChiBieuMauBusiness
                .GetAllAsQueryable()
                .Where(x => x.IdBieuMau == idBieuMau && x.Key.Equals(key))
                .FirstOrDefault() ?? new bentre_TieuChiBieuMau()
                {
                    Key = key,
                    IdBieuMau = idBieuMau
                };

            var viewModel = new SettingFieldVM()
            {
                TieuChi = infoTieuChi,
            };
            return PartialView("_SettingField", viewModel);
        }

        public JsonResult SaveSettingField(FormCollection form)
        {
            var result = new JsonResultBO(true);
            var idTieuChi = form["Id"].ToIntOrZero();
            //var idBieuMau = form["BoChiSoId"].ToIntOrZero();
            //var cdskey = form["CdsKey"].Trim().ToListStringLower(',');

            try
            {
                var infoTieuChi = TieuChiBieuMauBusiness.GetAllBySqlQuery().Where(t => t.Id == idTieuChi).FirstOrDefault() ?? new bentre_TieuChiBieuMau();
                //infoTieuChi.BoChiSoId = form["BoChiSoId"].ToIntOrNULL();
                //infoTieuChi.TruCotSoId = form["TruCotSoId"].ToIntOrNULL();
                //infoTieuChi.NhomTieuChiId = form["NhomTieuChiId"].ToIntOrNULL();
                infoTieuChi.IdBieuMau = form["IdBieuMau"].ToIntOrZero();
                infoTieuChi.Key = form["Key"].Trim();
                infoTieuChi.SoThuTu = form["SoThuTu"].ToIntOrZero();
                //infoTieuChi.ListDisabled = form["KeysDisabled"];
                //infoTieuChi.ListEnabled = form["KeysEnabled"];
                infoTieuChi.GioiHanTren = form["GioiHanTren"].ToFloatOrZero();
                infoTieuChi.GioiHanDuoi = form["GioiHanDuoi"].ToFloatOrZero();
                infoTieuChi.CongThuc = form["CongThuc"].Trim();
                //infoTieuChi.IdDanhMuc = form["IdDanhMuc"].ToIntOrNULL();
                //infoTieuChi.IsMultiple = form["IsMultiple"].ToBoolOrFalse();
                infoTieuChi.IsComboBox = form["IsComboBox"].ToBoolOrFalse();
                infoTieuChi.DataType = form["DataType"].ToIntOrNULL();
                infoTieuChi.Required = form["Required"].ToBoolOrFalse();
                infoTieuChi.Ten = form["Ten"].Trim();
                //infoTieuChi.TenDaiDien = form["TenDaiDien"].Trim();
                //infoTieuChi.TenDaiDienNoSign = ConvertToUnSign(entityTieuChi.TenDaiDien);
                //infoTieuChi.IsDaiDienChoNhomTieuChi = form["IsDaiDienChoNhomTieuChi"].ToBoolOrFalse();
                TieuChiBieuMauBusiness.Save(infoTieuChi);

                result.Message = "Cập nhật tiêu chí thành công";
            }
            catch (Exception ex)
            {
                result.MessageFail("lỗi");
            }
            return Json(result);
        }

        /// <summary>
        /// @author:duynn
        /// @description: danh sách nộp biểu mẫu thông tin
        /// </summary>
        /// <returns></returns>
        public ActionResult IndexNopBieuMauThongTin()
        {
            SessionManager.SetValue(SearchKeyNopBieuMau, null);
            var viewModel = new NopBieuMauListViewModel()
            {
                DanhSachNop = NopBieuMauBusiness.GetDataByPage(null),
                DanhSachBieuMauThongTin = BieuMauThongTinBusiness.GetAllBySqlQuery()
                .Select(bm => new SelectListItem()
                {
                    Value = bm.Id.ToString(),
                    Text = bm.Ten
                }).ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public JsonResult GetDataNopBieuMau(int pageIndex, string sortQuery, int pageSize)
        {
            var searchModel =
                (NopBieuMauThongTinSearchBO)SessionManager.GetValue(SearchKeyNopBieuMau);
            if (searchModel == null) searchModel = new NopBieuMauThongTinSearchBO();
            searchModel.PageIndex = pageIndex;
            searchModel.PageSize = pageSize;
            searchModel.SortQuery = sortQuery;

            SessionManager.SetValue(SearchKey, searchModel);
            var data = NopBieuMauBusiness.GetDataByPage(searchModel, pageIndex, pageSize);
            return Json(data);
        }

        /// <summary>
        /// @author:duynn
        /// @description: tìm kiếm nộp biểu mẫu
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SearchDataNopBieuMau(NopBieuMauThongTinSearchBO searchModel)
        {
            if (searchModel is null)
            {
                throw new ArgumentNullException(nameof(searchModel));
            }

            var search = SessionManager.GetValue(SearchKeyNopBieuMau) as NopBieuMauThongTinSearchBO;
            search = MapDataHelper<NopBieuMauThongTinSearchBO, NopBieuMauThongTinSearchBO>.MapData(searchModel);

            SessionManager.SetValue(SearchKey, search);
            var data = NopBieuMauBusiness.GetDataByPage(search, 1);
            data.PageIndex = 1;
            data.PageSize = 20;
            return Json(data);
        }


        /// <summary>
        /// @author:duynn
        /// @description: chi tiết biểu mẫu thông tin
        /// </summary>
        /// <param name="idNopBieuMau"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult DetailNopBieuMau(int idNopBieuMau)
        {
            var infoNopBieuMau = NopBieuMauBusiness.GetAllBySqlQuery()
                .Where(bm => bm.Id == idNopBieuMau)
                .FirstOrEmpty();

            var listDuLieuKeKhai = KeKhaiBieuMauBusiness.GetAllBySqlQuery()
                .Where(kk => kk.IdNopBieuMau == idNopBieuMau)
                .ToList();

            var infoBieuMauThongTin = BieuMauThongTinBusiness.GetAllAsQueryable()
                .Where(bm => bm.Id == infoNopBieuMau.IdBieuMauThongTin)
                .FirstOrEmpty();

            var listTieuChi = TieuChiBieuMauBusiness.GetAllBySqlQuery()
                .Where(t => t.IdBieuMau == infoBieuMauThongTin.Id)
                .ToList();

            var viewModel = new NopBieuMauDetailViewModel()
            {
                NopBieuMau = infoNopBieuMau,
                BieuMauThongTin = infoBieuMauThongTin,
                DuLieuKeKhai = listDuLieuKeKhai,
                ListTieuChi = listTieuChi
            };
            return View(viewModel);
        }

        /// <summary>
        /// @author:duynn
        /// @description: danh sách biểu mẫu thông tin cho khách
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult IndexGuest()
        {
            var viewModel = new BieuMauThongTinListViewModel()
            {
                DanhSachBieuMauThongTin = BieuMauThongTinBusiness.GetDaTaByPageForGuest(searchModel: null)
            };
            return View(viewModel);
        }

        /// <summary>
        /// @author:duynn
        /// @description: nộp biểu mẫu thông tin
        /// </summary>
        /// <param name="idBieuMau"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult KeKhaiBieuMauThongTin(int idBieuMau)
        {
            var info = BieuMauThongTinBusiness.GetAllAsQueryable()
                .Where(bm => bm.Id == idBieuMau).FirstOrDefault() ?? new bentre_BieuMauThongTin();
            var listTieuChiBieuMau = TieuChiBieuMauBusiness.GetAllBySqlQuery().Where(tc => tc.IdBieuMau == info.Id)
                .ToList();
            var viewModel = new BieuMauThongTinKeKhaiViewModel()
            {
                InfoBieuMau = info,
                ListTieuChi = listTieuChiBieuMau,
            };
            return View(viewModel);
        }

        /// <summary>
        /// @author:duynn
        /// @description: nộp kê khai biểu mẫu
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public JsonResult NopKeKhaiBieuMauThongTin(FormCollection form)
        {
            var result = new JsonResultBO(true, "Nộp biểu mẫu thông tin thành công");
            try
            {
                var idBieuMauThongTin = form["IdBieuMauThongTin"].ToIntOrZero();
                var dataNopBieuMau = new bentre_NopBieuMau()
                {
                    Hoten = form["Hoten"]?.Trim() ?? "",
                    Email = form["Email"]?.Trim() ?? "",
                    DienThoai = form["DienThoai"]?.Trim() ?? "",
                    DiaChi = form["DiaChi"]?.Trim() ?? "",
                    IdBieuMauThongTin = idBieuMauThongTin,
                    NgayNop = DateTime.Now
                };
                NopBieuMauBusiness.Insert(dataNopBieuMau);
                NopBieuMauBusiness.Save();

                var formKeys = form.AllKeys;
                var listDuLieuKeKhaiToInsert = new List<bentre_KeKhaiBieuMau>();
                foreach (var submitKey in formKeys)
                {
                    var arrKey = submitKey.Split('@');

                    var itemKey = "";
                    var itemNhomTieuChiOfKey = 0;

                    if (arrKey.Length <= 1)
                    {
                        continue;
                    }

                    if (arrKey.Length > 1)
                    {
                        itemKey = arrKey[0];
                        itemNhomTieuChiOfKey = arrKey[1].ToIntOrZero();
                    }

                    if (string.IsNullOrEmpty(itemKey) && itemNhomTieuChiOfKey == 0)
                    {
                        continue;
                    }

                    var entitySoLieuKeKhaiTieuChi = new bentre_KeKhaiBieuMau()
                    {
                        Key = itemKey,
                        Value = form[submitKey],
                        IdNopBieuMau = dataNopBieuMau.Id,
                        NgayNop = DateTime.Now
                    };
                    listDuLieuKeKhaiToInsert.Add(entitySoLieuKeKhaiTieuChi);
                }

                if (listDuLieuKeKhaiToInsert.Count > 0)
                {
                    KeKhaiBieuMauBusiness.InsertRange(listDuLieuKeKhaiToInsert);
                    KeKhaiBieuMauBusiness.Save();
                }
            }
            catch
            {
                result.MessageFail("Nộp biểu mẫu thông tin thất bại");
            }
            return Json(result);
        }
    }
}
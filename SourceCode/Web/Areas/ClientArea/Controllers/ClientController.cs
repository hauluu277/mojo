using mojoportal.CoreHelpers;
using mojoportal.Service.CommonBusiness;
using mojoportal.Service.UoW;
using mojoPortal.Business;
using mojoPortal.Model.Data;
using mojoPortal.Service.Business;
using mojoPortal.Service.CommonModel.client;
using mojoPortal.Web.Areas.ClientArea.Models;
using mojoPortal.Web.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace mojoPortal.Web.Areas.ClientArea.Controllers
{
    public class ClientController : BaseController
    {
        private readonly core_ClientBusiness _core_ClientBusiness;
        private readonly CategoryBusiness categoryBusiness;
        private readonly core_ClientCategoryBusiness _core_ClientCategoryBusiness;
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private readonly ArticlesBusiness _articlesBusiness;
        private readonly ArticleCategoryBusiness _articleCategoryBusiness;

        // GET: ClientArea/Client

        public ClientController()
        {
            _core_ClientBusiness = new core_ClientBusiness(_unitOfWork);
            categoryBusiness = Get<CategoryBusiness>();
            _core_ClientCategoryBusiness = Get<core_ClientCategoryBusiness>();
            _articlesBusiness = Get<ArticlesBusiness>();
            _articleCategoryBusiness = Get<ArticleCategoryBusiness>();
        }

        public ActionResult Index()
        {
            IndexClientVM model = new IndexClientVM();
            model.ListData = _core_ClientBusiness.GetPageClient(null);

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult IndexGroup(int colunm, int group)
        {
            IndexClientVM model = new IndexClientVM();
            var searchModel = new ClientSearchBO();
            searchModel.groupId = group;
            model.listWithGroup = _core_ClientBusiness.GetPageClientGroup(searchModel);
            model.colunm = colunm;
            model.group = group;
            return View(model);
        }

        public List<SelectListItem> ListItemCategory(int siteId, int parentId = 0, int selected = 0)
        {
            List<SelectListItem> root = new List<SelectListItem>();
            if (parentId > 0)
            {
                root = CoreCategory.GetChildren(parentId).Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString(), Selected = (x.ItemID == selected) }).ToList();
            }
            else
            {
                root = CoreCategory.GetRoot(siteId).Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString(), Selected = (x.ItemID == selected) }).ToList();
            }
            for (int i = 0; i < root.Count; i++)
            {
                List<CoreCategory> childens = CoreCategory.GetChildren(root[i].Value.ToIntOrZero());
                if (childens.Count <= 0) continue;
                string prefix = string.Empty;
                while (root[i].Text.StartsWith("|"))
                {
                    prefix += root[i].Text.Substring(0, 3);
                    root[i].Text = root[i].Text.Remove(0, 3);
                }
                root[i].Text = prefix + root[i].Text;
                int index = 1;
                foreach (CoreCategory child in childens)
                {
                    SelectListItem item = new SelectListItem
                    {
                        Text = prefix + "|--" + child.Name,
                        Value = child.ItemID.ToString(),
                        Selected = (child.ItemID == selected)
                    };
                    int position = root.IndexOf(root[i]) + index;
                    root.Insert(position, item);
                    index++;
                }
            }
            //if (parentId > 0)
            //{
            //    var parent = new CoreCategory(parentId);
            //    root.Insert(0, new SelectListItem { Text = parent.Name, Value = parent.ItemID.ToString() });
            //}
            return root;
        }

        public PartialViewResult FormClient(int id = 0)
        {
            FormVM model = new FormVM();
            if (id > 0)
            {
                var searchData = _core_ClientBusiness.Find(id);
                if (searchData == null)
                {
                    throw new Exception("Ứng dụng không tồn tại");
                }
                model = MaperData.MapAllowNull<FormVM, core_Client>(model, searchData);
                ViewBag.dropdownNhomCongThanhVien = categoryBusiness.GetChildItem("DM_CONGTHANHVIEN", 1, model.IdNhomCongThanhVien);
            }
            else
            {
                ViewBag.dropdownNhomCongThanhVien = categoryBusiness.GetChildItem("DM_CONGTHANHVIEN", 1);
                //model.ClientID = Guid.NewGuid().ToString();
            }

            ViewBag.DropChuyenMuc = ListItemCategory(0, 423);
            return PartialView("_Form", model);
        }

        public async Task<PartialViewResult> DongBoChuyenMucTin(int id = 0)
        {
            HttpClient client = new HttpClient();
            var getClient = _core_ClientBusiness.Find(id);
            if (getClient == null)
            {
                getClient = new core_Client();
            }
            client.BaseAddress = new Uri(getClient.ClientUrl);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, getClient.APIChuyenMucTin);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string json = "{\"Username\": \"" + getClient.TenDangNhap + "\", \"Password\": \"" + getClient.MatKhau + "\"}";
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            request.Content = content;
            HttpResponseMessage response = await client.SendAsync(request);
            var data = await response.Content.ReadAsStringAsync();


            var objResultData = JsonConvert.DeserializeObject<ResultAPI>(data);
            var dataxx = objResultData.Data;
            var getCategory = CoreCategory.GetByCode(1, WebConfigSettings.DM_TinTuc);


            foreach (var item in dataxx)
            {

                var IdDanhMucSelectByClientId = _core_ClientCategoryBusiness.ByClientDanhMucClient(id, (int)item.DanhMucId);
                if (IdDanhMucSelectByClientId != null)
                {
                    item.listDanhMuc = ListItemCategory(1, getCategory.ItemID, IdDanhMucSelectByClientId.CategoryId);
                    item.IsDaLayDanhMuc = true;
                    item.IdCoreClient = IdDanhMucSelectByClientId.ItemID;
                }
                else
                {
                    item.listDanhMuc = ListItemCategory(1, getCategory.ItemID, 0);
                    item.IsDaLayDanhMuc = false;
                    item.IdCoreClient = 0;
                }
            }

            ViewBag.IdDonVi = id;
            return PartialView("_FormDongBoChuyenMucTin", dataxx);
        }

        [HttpPost]
        public ActionResult SaveClientCategory(int IdDonVi, int IdDanhMucDonVi, int DanhMucxy = 0)
        {
            var result = new JsonResultBO(true, "Lưu thành công");

            var clientCategory = new core_ClientCategory();

            var objTonTai = _core_ClientCategoryBusiness.ByClientDanhMucClient(IdDonVi, IdDanhMucDonVi);

            try
            {

                if (DanhMucxy > 0)
                {

                    if (objTonTai != null)
                    {
                        objTonTai.CategoryId = DanhMucxy;
                        _core_ClientCategoryBusiness.Save(objTonTai);


                        //update artical
                        var objArtical = _articlesBusiness.GetByIdBaiVetClient(objTonTai.CategoryId);


                        foreach (var item in objArtical)
                        {
                            item.CategoryID = DanhMucxy;
                            _articlesBusiness.Save(item);
                        }

                    }
                    else
                    {
                        clientCategory.ClientId = IdDonVi;
                        clientCategory.CategoryClientId = IdDanhMucDonVi;
                        clientCategory.CategoryId = DanhMucxy;
                        _core_ClientCategoryBusiness.Save(clientCategory);
                    }



                }

            }
            catch (Exception ex)
            {
                result.MessageFail("Đồng bộ thất bại");
            }


            return Json(result);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SaveForm(FormVM model, FormCollection formCollection, List<string> DanhMucChuyenMuc)
        {
            var result = new JsonResultBO(true);
            var client = new core_Client();
            try
            {
                result.Message = "Thêm mới thông tin ứng dụng thành công";

                if (model.ItemID > 0)
                {
                    client = _core_ClientBusiness.Find(model.ItemID);
                    if (client == null)
                    {
                        throw new Exception("Không tìm thấy ứng dụng");
                    }
                    result.Message = "Cập nhật thông tin ứng dụng thành công";
                    client.EditedBy = siteUser.UserId;
                    client.EditedDate = DateTime.Now;
                }
                else
                {
                    client.CreatedDate = DateTime.Now;
                    client.CreatedByUser = siteUser.UserId;
                    client.CreatedBy = siteUser.LoginName;

                }
                //get rewrite url product
                client = MaperData.MapAllowNull<core_Client, FormVM>(client, model);
                if (DanhMucChuyenMuc != null && DanhMucChuyenMuc.Any())
                {
                    client.ChuyenMucId = string.Join(",", DanhMucChuyenMuc);
                }
                else
                {
                    client.ChuyenMucId = null;
                }
                client.isLayTinTuDong = formCollection["isLayTinTuDong"].ToBooleanOnOff();
                _core_ClientBusiness.Save(client);
            }
            catch (Exception ex)
            {
                result.Message = "Không thể thực hiện thao tác này";
                result.Status = false;
            }
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SearchData(ClientSearchBO searchModel)
        {
            var search =
         (ClientSearchBO)SessionManager.GetValue("ClientSearchModel");
            if (search == null) search = new ClientSearchBO();
            search = MapDataHelper<ClientSearchBO, ClientSearchBO>.MapData(searchModel);
            SessionManager.SetValue("ClientSearchModel", search);

            var data = _core_ClientBusiness.GetPageClient(search, 1, search.pageSize);
            return Json(data);
        }

        [HttpPost]
        public JsonResult GetData(int pageIndex, string sortQuery, int pageSize)
        {
            var searchModel =
                (ClientSearchBO)SessionManager.GetValue("ClientSearchModel");
            if (searchModel == null) searchModel = new ClientSearchBO();
            searchModel.pageIndex = pageIndex;
            searchModel.pageSize = pageSize;
            searchModel.sortQuery = sortQuery;

            SessionManager.SetValue("ClientSearchModel", searchModel);
            var data = _core_ClientBusiness.GetPageClient(searchModel, pageIndex, pageSize);
            return Json(data);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = new JsonResultBO(true);
            try
            {
                _core_ClientBusiness.Delete(id);
                _core_ClientBusiness.context.SaveChanges();
            }
            catch
            {

                result.Status = false;
            }

            return Json(result);
        }

        public async Task<PartialViewResult> IndexLayTinBaiClient(int idClientCategory)
        {
            var objClientCategory = _core_ClientCategoryBusiness.GetByIdItem(idClientCategory);
            var result = new List<TinBaiWeb>();
            if (objClientCategory != null)
            {
                var getClient = _core_ClientBusiness.Find(objClientCategory.ClientId);
                if (getClient == null)
                {
                    getClient = new core_Client();
                }
                ViewBag.Client = getClient;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(getClient.ClientUrl);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, getClient.APIDanhSachTin);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string json = "{\"Username\": \"" + getClient.TenDangNhap + "\", \"Password\": \"" + getClient.MatKhau + "\",\"DanhMucId\":\"" + objClientCategory.ItemID + "\" }";

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                request.Content = content;
                HttpResponseMessage response = await client.SendAsync(request);
                var data = await response.Content.ReadAsStringAsync();

                var objResultData = JsonConvert.DeserializeObject<ResultTinBaiAPI>(data);
                if (objResultData != null && objResultData.Data != null)
                {
                    var dataxx = objResultData.Data;
                    result = dataxx;
                    var listArticle = _articlesBusiness.GetAllAsQueryable()
                        .Where(x => x.ClientId == getClient.ClientID && x.ClientBaiVietId != null).Select(x => x.ClientBaiVietId).ToList();

                    foreach (var item in result)
                    {
                        item.IsThemVaoDanhMuc = listArticle.Where(x => x == item.BaiVietId).Count() > 0 ? true : false;
                    }
                }


            }
            ViewBag.idClientCategory = idClientCategory;
            return PartialView(result);
        }


        [HttpPost]
        public async Task<JsonResult> UpdateTinTuc(int idClientCategory, List<string> listId)
        {

            var result = new JsonResultBO(true, "Thêm thành công");
            try
            {
                var objClientCategory = _core_ClientCategoryBusiness.GetByIdItem(idClientCategory);
                if (objClientCategory != null)
                {
                    var getClient = _core_ClientBusiness.Find(objClientCategory.ClientId);
                    if (getClient == null)
                    {
                        getClient = new core_Client();
                    }

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(getClient.ClientUrl);
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, getClient.APIDanhSachTin);
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string json = "{\"Username\": \"" + getClient.TenDangNhap + "\", \"Password\": \"" + getClient.MatKhau + "\",\"DanhMucId\":\"" + objClientCategory.ItemID + "\" }";
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    request.Content = content;
                    HttpResponseMessage response = await client.SendAsync(request);
                    var data = await response.Content.ReadAsStringAsync();

                    var objResultData = JsonConvert.DeserializeObject<ResultTinBaiAPI>(data);
                    if (objResultData != null && objResultData.Data != null)
                    {
                        var dataxx = objResultData.Data;
                        var dataLong = new List<long>();
                        foreach (var item in listId)
                        {
                            dataLong.Add(long.Parse(item));
                        }

                        var listAddNew = dataxx.Where(x => dataLong.Contains(x.BaiVietId)).ToList();

                        foreach (var item in listAddNew)
                        {
                            var noidungTin = item.NoiDung;
                            if (!string.IsNullOrEmpty(item.NoiDung))
                            {
                                //dùng hàm Regex để lấy ra toàn bộ hình ảnh
                                var ListAnh = Regex.Matches(item.NoiDung, @"(?<=<img\s+[^>]*?src=(?<q>['""]))(?<url>.+?)(?=\k<q>)");

                                //khai báo mảng chứa danh sách hình, url, src
                                var list = new HashSet<String>(); //HashSet có nhiều hình ảnh giống nhau thì chỉ lấy 1
                                                                  //duyệt mảng


                                foreach (var img in ListAnh)
                                {
                                    if (img != "")
                                    {
                                        list.Add(img.ToString());
                                        var dataimg = img.ToString().Split('/');
                                        var NameImg = dataimg[dataimg.Length - 1];
                                        using (WebClient webClient = new WebClient())
                                        {
                                            //lấy path của serverAPI
                                            var duongDanLuuTin = Server.MapPath("/") + "Data\\Images\\Article";
                                            var pathFolder = "";
                                            pathFolder = Path.Combine(duongDanLuuTin);
                                            //kiểm tra chưa có thư mục thì tạo mới
                                            if (!Directory.Exists(pathFolder))
                                            {
                                                Directory.CreateDirectory(pathFolder);
                                            }

                                            var fileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + NameImg;


                                            //lưu lại ảnh vật lý
                                            webClient.DownloadFile(new Uri(img.ToString()), duongDanLuuTin + "\\" + fileName);
                                            //replace ảnh cũ theo ảnh của server mình
                                            item.NoiDung = item.NoiDung.Replace(img.ToString(), "/Data/Images/Article/" + fileName);

                                        }
                                    }
                                }
                            }
                            if (item.AnhDaiDien != null && !string.IsNullOrEmpty(item.AnhDaiDien))
                            {
                                var dataimg = item.AnhDaiDien.Split('/');
                                var NameImg = dataimg[dataimg.Length - 1];
                                using (WebClient webClient = new WebClient())
                                {
                                    //lấy path của serverAPI
                                    var duongDanLuuTin = Server.MapPath("/") + "Data\\Images\\Article";
                                    var pathFolder = "";
                                    pathFolder = Path.Combine(duongDanLuuTin);
                                    //kiểm tra chưa có thư mục thì tạo mới
                                    if (!Directory.Exists(pathFolder))
                                    {
                                        Directory.CreateDirectory(pathFolder);
                                    }
                                    var fileName = NameImg;
                                    if (!string.IsNullOrEmpty(NameImg))
                                    {
                                        var arr = NameImg.Split('.');
                                        var extension = arr[arr.Length - 1];
                                        fileName = arr[arr.Length - 1] + DateTime.Now.ToString("ddMMyyyyHHmmss") + "." + extension;
                                    }

                                    //lưu lại ảnh vật lý
                                    webClient.DownloadFile(new Uri(item.AnhDaiDien.ToString()), duongDanLuuTin + "/" + fileName);
                                    //Lấy theo đưuòng dẫn server
                                    item.AnhDaiDien = fileName;

                                }
                            }




                            md_Articles articles = new md_Articles();
                            articles.SiteID = 1;
                            articles.Title = item.TieuDe;
                            articles.Summary = item.MoTa;
                            articles.ImageUrl = item.AnhDaiDien;
                            //articles.ItemUrl 
                            articles.CategoryID = objClientCategory.CategoryId;


                            articles.Description = item.NoiDung;
                            articles.ClientId = item.ClientId;
                            articles.CreateDateArticle = DateTime.Now;
                            articles.LastModUtc = DateTime.Now;
                            articles.IsPublished = false;
                            articles.IsApproved = false;


                            articles.CreatedByUser = item.NguoiTao;
                            articles.AuthorFTS = item.TacGia;


                            //ngày hiển thị
                            articles.StartDate = item.NgayTao;

                            //ngày đăng 
                            articles.CreatedDate = item.NgayDang;
                            SiteSettings siteSettings = new SiteSettings(1);
                            string newUrl = SiteUtils.SuggestFriendlyUrl(item.TieuDe.Replace("~/", string.Empty), siteSettings);
                            articles.ItemUrl = "~/" + newUrl;
                            articles.ModuleID = -1;
                            articles.CommentCount = 1;
                            articles.HitCount = 1;
                            articles.IsCongThanhVien = true;
                            articles.ArticleGuid = Guid.NewGuid();
                            articles.ModuleGuid = Guid.NewGuid();
                            articles.UserGuid = Guid.NewGuid();
                            articles.LastModUserGuid = Guid.NewGuid();
                            articles.IsApproved = false;
                            articles.AllowComment = true;
                            articles.AllowWCAG = true;
                            articles.IsHot = false;
                            articles.IsHienThiTacGia = false;
                            articles.ClientBaiVietId = (int?)item.BaiVietId;
                            _articlesBusiness.Save(articles);


                            FriendlyUrl newFriendlyUrl = new FriendlyUrl
                            {
                                SiteId = siteSettings.SiteId,
                                SiteGuid = siteSettings.SiteGuid,
                                PageGuid = articles.ArticleGuid.Value,
                                Url = newUrl,
                                RealUrl = "~/Article/ViewPost.aspx?pageid="
                                     + 1
                                     + "&mid=" + articles.ModuleID
                                     + "&ItemID=" + articles.ItemID
                            };

                            newFriendlyUrl.Save();

                            md_ArticleCategory articalescategory = new md_ArticleCategory();
                            articalescategory.SiteID = 1;
                            articalescategory.ArticleID = articles.ItemID;
                            articalescategory.CategoryID = articles.CategoryID;
                            _articleCategoryBusiness.Save(articalescategory);


                        }
                    }
                }



            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;

            }
            return Json(result);

        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult GetSearchThongTinXe(string keyWord = "")
        {
            var result = new JsonResultBO(true, "Thành công");

            try
            {
                var objArtical = _articlesBusiness.GetByIdBaiVetClientKeyWord(9812, keyWord);
                if (objArtical != null && objArtical.Any())
                {
                    result.Data = objArtical.Select(x => new { title = x.Title, Url = x.ItemUrl.Replace("~", "") }); // Title, ItemUrl
                }
                else
                {
                    result.Status = false;
                    result.Message = "Không có dữ liệu"; 
                }
            }
            catch (Exception ex)
            {
                result.MessageFail("Không có dữ liệu");
            }
            return Json(result);
        }

    }
}
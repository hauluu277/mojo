using mojoportal.Service.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Model.Data;
using mojoPortal.Service.Business;
using mojoPortal.Web.Areas.CategoryArea.Models;
using mojoPortal.Web.Areas.MenuContextArea.Models;
using mojoPortal.Web.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;


namespace mojoPortal.Web.Areas.CategoryArea.Controllers
{

    public class CategoryController : BaseController
    {
        // GET: MenuContextArea/MenuContext
        private CategoryBusiness categoryBusiness;
        private UserBusiness userBusiness;
        private CategoryUserArticleBusiness categoryUserArticleBusiness;
        public CategoryController()
        {
            categoryBusiness = Get<CategoryBusiness>();
            userBusiness = Get<UserBusiness>();
            categoryUserArticleBusiness = Get<CategoryUserArticleBusiness>();
        }
        private List<TreeNodeBO> listAll = new List<TreeNodeBO>();
        public async Task<ActionResult> Index()
        {
            return View();
        }


        private void LoadAllNote(int siteId)
        {
            var allCategory = CoreCategory.GetBySite(siteId);
            foreach (var item in allCategory)
            {
                TreeNodeBO node = new TreeNodeBO();
                node.id = item.ItemID;
                node.name = item.Name;
                node.OrderBy = item.Priority;
                node.ParentId = item.ParentID;
                node.LinkMenu = item.Description;
                listAll.Add(node);
            }
        }


        private void LoadAllNoteArticle(int siteId)
        {
            var sites = new SiteSettings(siteId);
            var allCategory = CoreCategory.GetChildren(sites.ArticleCategory);
            foreach (var item in allCategory)
            {
                TreeNodeBO node = new TreeNodeBO();
                node.id = item.ItemID;
                node.name = item.Name;
                node.OrderBy = item.Priority;
                node.ParentId = item.ParentID;
                node.LinkMenu = item.Description;
                listAll.Add(node);
            }
        }

        [HttpPost]
        public JsonResult GetList(int siteId)
        {
            LoadAllNote(siteId);
            var allNode = new List<TreeNodeBO>();
            var root = CoreCategory.GetRoot(siteId);
            foreach (var item in root)
            {
                TreeNodeBO menu = new TreeNodeBO();
                menu.id = item.ItemID;
                menu.name = item.Name;
                menu.OrderBy = item.Priority;
                menu.ParentId = item.ParentID;
                menu.LinkMenu = item.Description;
                GetChild(ref menu);
                allNode.Add(menu);
            }
            allNode.Insert(0, new TreeNodeBO()
            {
                id = 0,
                name = "Root",
                LinkMenu = "/",
                ParentId = 0,
                Show = true,
                OrderBy = 0
            });
            return Json(allNode);
        }


        [HttpPost]
        public JsonResult GetListArticle(int siteId)
        {

            LoadAllNote(siteId);
            var sites = new SiteSettings(siteId);
            var allNode = new List<TreeNodeBO>();
            var root = CoreCategory.GetChildren(sites.ArticleCategory);
            var coreCategory = new CoreCategory(sites.ArticleCategory);
            TreeNodeBO _menu = new TreeNodeBO();
            _menu.id = coreCategory.ItemID;
            _menu.name = coreCategory.Name;
            _menu.OrderBy = coreCategory.Priority;
            _menu.ParentId = coreCategory.ParentID;
            _menu.LinkMenu = coreCategory.Description;
            var lstcc = new List<TreeNodeBO>();
            foreach (var item in root)
            {
                TreeNodeBO menu = new TreeNodeBO();
                menu.id = item.ItemID;
                menu.name = item.Name;
                menu.OrderBy = item.Priority;
                menu.ParentId = item.ParentID;
                menu.LinkMenu = item.Description;
                GetChild(ref menu);
                lstcc.Add(menu);
            }
            _menu.children = new List<TreeNodeBO>();
            _menu.children.AddRange(lstcc);
            allNode.Add(_menu);
            allNode.Insert(0, new TreeNodeBO()
            {
                id = 0,
                name = "Root",
                LinkMenu = "/",
                ParentId = 0,
                Show = true,
                OrderBy = 0
            });
            return Json(allNode);
        }


        public void GetChild(ref TreeNodeBO Node)
        {
            var parentId = Node.id;
            var lstChild = listAll.Where(x => x.ParentId == parentId).OrderBy(x => x.OrderBy).ToList();
            if (lstChild.Count > 0)
            {
                Node.children = new List<TreeNodeBO>();
                Node.children.AddRange(lstChild);
                for (int i = 0; i < lstChild.Count; i++)
                {
                    var item = Node.children[i];
                    GetChild(ref item);
                    Node.children[i] = item;
                }
            }
        }

        public void GetChildArticle(ref TreeNodeBO Node)
        {
            var parentId = Node.id;
            var lstChild = CoreCategory.GetChildren(parentId);
            if (lstChild.Count > 0)
            {
                var cc = lstChild.Select(item => new TreeNodeBO
                {
                    id = item.ItemID,
                    name = item.Name,
                    OrderBy = item.Priority,
                    ParentId = item.ParentID,
                    LinkMenu = item.Description,
                }).ToList();

                Node.children = new List<TreeNodeBO>();
                Node.children.AddRange(cc);
                for (int i = 0; i < lstChild.Count; i++)
                {
                    var item = Node.children[i];
                    GetChild(ref item);
                    Node.children[i] = item;
                }
            }
        }



        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SaveForm(FormCollection form)
        {
            try
            {
                string msg = "Thêm mới danh mục thành công";
                var menuId = form["CategoryID"].ToIntOrZero();
                var category = new CoreCategory();
                if (menuId > 0)
                {
                    category = new CoreCategory(menuId);
                    msg = "Cập nhật danh mục thành công";
                }
                category.ParentID = form["ParentID"].ToIntOrZero();
                category.Description = form["Description"];
                category.Name = form["Name"];
                category.Priority = form["Priority"].ToIntOrZero();
                category.SiteID = form["SiteID"].ToShortOrZero();
                category.IsPhongBan = form["IsPhongBan"].ToBoolByOnOff();
                category.ShowMenuLeft = form["ShowMenuLeft"].ToBoolByOnOff();
                category.ShowCategoryChild = form["ShowCategoryChild"].ToBoolByOnOff();
                category.PathIMG = form["PathIMG"];
                category.PathFile = form["PathFile"];
                category.SubName = form["SubName"];
                category.Code = form["Code"];
                category.Sumary = form["Sumary"];
                category.TargetBlank = form["TargetBlank"].ToBoolByOnOff();
                category.Color = form["Color"];
                category.Save();
                ReloadCategory(category.SiteID);
                return Json(new { Status = true, Message = msg, ItemID = category.ItemID, Name = category.Name });
            }
            catch
            {

                return Json(new { Status = false, Message = "Không thể thực hiện thao tác này" });
            }
        }

        //Cập nhật lại IsLinhVucDieuTra
        //Cập nhật lại IsTinTuc
        //Dựa vào ArticleCategory và CoreChuDe trong thiết lập websie
        private void ReloadCategory(int siteId)
        {
            var setting = new SiteSettings(siteId);
            //cập nhật cho tin tức
            if (setting.ArticleCategory > 0)
            {
                var listCategoryId = string.Join(",", CoreCategory.GetListChildrenID(setting.ArticleCategory).ToArray());
                CoreCategory.UpdateMultiple(siteId, listCategoryId, true, false);
            }
            //cập nhật cho lĩnh vực điều tra
            if (setting.CoreChuDe > 0)
            {
                var listCategoryId = string.Join(",", CoreCategory.GetListChildrenID(setting.CoreChuDe).ToArray());
                CoreCategory.UpdateMultiple(siteId, listCategoryId, false, true);
            }
        }

        [HttpPost]
        public JsonResult DeleteCategory(int id = 0)
        {
            try
            {
                CoreCategory.Delete(id);
                return Json(new { Status = true });
            }
            catch
            {

                return Json(new { Status = false });
            }

        }


        //public PartialViewResult FormPhanQuyenCategory(int siteId, int parentId = 0, int id = 0, int addArticle = 0)
        //{
        //    var sites = new SiteSettings(siteId);
        //    FormPhanQuyenCategoryModel model = new FormPhanQuyenCategoryModel();
        //    model.Category = new CoreCategory(id);
        //    if (id > 0)
        //    {
        //        parentId = model.Category.ParentID;
        //        model.OrderBy = model.Category.Priority;
        //    }
        //    //else
        //    //{
        //    //    model.OrderBy = categoryBusiness.CountByParent(parentId) + 1;
        //    //}
        //    model.SiteID = siteId;
        //    model.ParentID = parentId;        
        //    return PartialView("_PhanQuyenChuyenMuc", model);
        //}


        //public PartialViewResult FormPhanQuyenCategory(int siteId, int parentId = 0, int id = 0, int addArticle = 0)
        //{
        //    var sites = new SiteSettings(siteId);
        //    FormPhanQuyenCategoryModel model = new FormPhanQuyenCategoryModel();
        //    model.Category = new CoreCategory(id);
        //    if (id > 0)
        //    {
        //        parentId = model.Category.ParentID;
        //        model.OrderBy = model.Category.Priority;
        //    }
        //    //else
        //    //{
        //    //    model.OrderBy = categoryBusiness.CountByParent(parentId) + 1;
        //    //}
        //    model.SiteID = siteId;
        //    model.ParentID = parentId;
        //    return PartialView("_PhanQuyenChuyenMuc", model);
        //}

        public PartialViewResult FormPhanQuyenCategory(int siteId, int parentId = 0, int id = 0, int addArticle = 0)
        {
            var sites = new SiteSettings(siteId);
            mp_UsersCategoryModel model = new mp_UsersCategoryModel();

            model.Category = new CoreCategory(id);

            return PartialView("_PhanQuyenChuyenMuc", model);
        }


        //public PartialViewResult PhanQuyenChuyenMuc(int siteId, int parentId = 0, int id = 0, int addArticle = 0)
        //{
        //    var sites = new SiteSettings(siteId);
        //    mp_UsersCategoryModel model = new mp_UsersCategoryModel();

        //    model.Category = new CoreCategory(id);

        //    return PartialView("_PhanQuyenChuyenMuc", model);
        //}








        public PartialViewResult FormCategory(int siteId, int parentId = 0, int id = 0, int addArticle = 0)
        {
            var sites = new SiteSettings(siteId);
            FormCategoryModel model = new FormCategoryModel();
            model.Category = new CoreCategory(id);
            if (id > 0)
            {
                parentId = model.Category.ParentID;
                model.OrderBy = model.Category.Priority;
            }
            //else
            //{
            //    model.OrderBy = categoryBusiness.CountByParent(parentId) + 1;
            //}
            model.SiteID = siteId;
            model.ParentID = parentId;
            if (addArticle > 0)
            {
                model.ListParent = ListItemCategory(siteId, sites.ArticleCategory, parentId);
            }
            else
            {
                model.ListParent = ListItemCategory(siteId);
            }
            return PartialView("_FormCategory", model);
        }

        public ActionResult MyAction(int siteId)
        {
            FormCategoryModel model = new FormCategoryModel();
            //List<mp_Users> userList = model.GetBySite(siteId);

            return PartialView("_FormCategory", model);
        }



        ////thêm
        //public PartialViewResult _FormPhanQuyenCategory(int siteId, int parentId = 0, int id = 0, int addArticle = 0)
        //{
        //    var sites = new SiteSettings(siteId);

        //    var listPageUser = userBusiness.GetUserNotInDanhMucTinTuc(id);
        //    ViewBag.IdCate = id;
        //    ViewBag.NameCate = categoryBusiness.GetName(id);
        //    return PartialView("_FormPhanQuyenCategory", listPageUser);


        public PartialViewResult FormCategoryPhanNguoiDungChucNang(int siteId, int parentId = 0, int id = 0, int addArticle = 0)
        {
            var sites = new SiteSettings(siteId);

            var model = new FormPhanQuyenCategoryModel();


            ViewBag.IdCate = id;
            ViewBag.NameCate = categoryBusiness.GetName(id);
            model.ListUser = userBusiness.GetAllAsQueryable().ToList();
            model.ListUserSelected = userBusiness.GetUserHasInDanhMucTinTuc(id);
            return PartialView("_FormCategoryPhanNguoiDungChucNang", model);
        }

        [HttpPost]
        public ActionResult ThemQuyenNguoiDungChoCategory(int idCate, List<int> listIdNguoiDung)
        {


            try
            {

                var listCategoryDelete = categoryUserArticleBusiness.GetAllAsQueryable()
                    .Where(x => x.CategoryID == idCate).ToList();
                if(listCategoryDelete != null && listCategoryDelete.Any())
                {
                    categoryUserArticleBusiness.DeleteRange(listCategoryDelete);
                    categoryUserArticleBusiness.Save();
                }

                if (listIdNguoiDung != null && listIdNguoiDung.Any())
                {
                    foreach (var item in listIdNguoiDung)
                    {
                        var obj = new core_CategoryUserArticle();

                        obj.UserID = item;
                        obj.CategoryID = idCate;
                        categoryUserArticleBusiness.Save(obj);
                    }
                }
                return Json(new { Status = true, Message = "Phân quyền danh mục tin tức thành công" });
            }
            catch (System.Exception)
            {
                return Json(new { Status = false, Message = "Phân quyền danh mục tin tức thất bại" });

            }


        }


        private List<SelectListItem> ListItemCategory(int siteId, int parentId = 0, int selected = 0)
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
            if (parentId > 0)
            {
                var parent = new CoreCategory(parentId);
                root.Insert(0, new SelectListItem { Text = parent.Name, Value = parent.ItemID.ToString() });
            }
            return root;
        }
    }
}

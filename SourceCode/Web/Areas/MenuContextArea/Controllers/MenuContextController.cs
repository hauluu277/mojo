using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.Areas.MenuContextArea.Models;
using Newtonsoft.Json;

namespace mojoPortal.Web.Areas.MenuContextArea.Controllers
{
    public class MenuContextController : Controller
    {
        // GET: MenuContextArea/MenuContext
        private List<TreeNodeBO> listAll = new List<TreeNodeBO>();
        private string siteRoot = SiteUtils.GetNavigationSiteRoot();
        private readonly SiteSettings siteSettings = CacheHelper.GetCurrentSiteSettings();
        public ActionResult Index()
        {
            var listSiteID = SiteSettings.GetAllSiteID();
            foreach (var siteId in listSiteID)
            {
                var listPage = PageSettings.GetList(siteId);
                var listCategory = CoreCategory.GetBySite(siteId);
                var listMenu = coreMenu.GetBySite(siteId);
                foreach (var menu in listMenu)
                {
                    if (!string.IsNullOrEmpty(menu.Name) && !string.IsNullOrEmpty(menu.LinkMenu))
                    {
                        var hasPage = listPage.Where(x => x.PageName.ToLower().Equals(menu.Name) || x.Url.Replace("~", string.Empty).Equals(menu.LinkMenu.Replace("~", string.Empty))).FirstOrDefault();
                        if (hasPage != null)
                        {
                            menu.TypeLink = MenuTypeLinkConstant.Page;
                            menu.ItemLink = hasPage.PageId;
                        }
                        else
                        {
                            var hasCategory = listCategory.Where(x => x.Name.ToLower().Equals(menu.Name)
                            || x.Description.Replace("~", string.Empty).Equals(menu.LinkMenu.Replace("~", string.Empty))).FirstOrDefault();
                            if (hasCategory != null)
                            {
                                menu.TypeLink = MenuTypeLinkConstant.Category;
                                menu.ItemLink = hasCategory.ItemID;
                            }
                            else
                            {
                                menu.TypeLink = MenuTypeLinkConstant.Khac;
                            }
                        }
                    }
                    else
                    {
                        menu.TypeLink = MenuTypeLinkConstant.Khac;
                    }
                    menu.Save();
                }
            }


            return View();
        }

        private void LoadAllNote(int siteId)
        {
            var allCategory = coreMenu.GetBySite(siteId);
            foreach (var item in allCategory)
            {
                TreeNodeBO node = new TreeNodeBO();
                node.Show = item.Show;
                if (item.Show.HasValue && item.Show.Value)
                {
                    node.IsDisplayText = "Hiển thị";
                }
                node.id = item.ItemID;
                node.name = item.Name + " (" + node.IsDisplayText + ")";
                node.OrderBy = item.OrderBy;
                node.ParentId = item.ParentID;

                listAll.Add(node);
            }
        }

        [HttpPost]
        public JsonResult GetTree(int siteId, int typeMenu)
        {
            LoadAllNote(siteId);
            var allNode = new List<TreeNodeBO>();

            var root = coreMenu.GetRoot(siteId, typeMenu, false);
            foreach (var item in root)
            {
                TreeNodeBO menu = new TreeNodeBO();
                menu.IsDisplayText = "Không hiển thị";
                if (item.Show.HasValue && item.Show.Value)
                {
                    menu.IsDisplayText = "Hiển thị";
                }
                menu.id = item.ItemID;
                menu.name = item.Name + " (" + menu.IsDisplayText + ")";
                menu.OrderBy = item.OrderBy;
                menu.ParentId = item.ParentID;
                menu.LinkMenu = siteRoot + item.LinkMenu;

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
        public JsonResult GetTreeEnglish(int siteId, int typeMenu)
        {
            LoadAllNote(siteId);
            var allNode = new List<TreeNodeBO>();

            var root = coreMenu.GetRoot(siteId, typeMenu, true);
            foreach (var item in root)
            {
                TreeNodeBO menu = new TreeNodeBO();
                menu.IsDisplayText = "Không hiển thị";
                if (item.Show.HasValue && item.Show.Value)
                {
                    menu.IsDisplayText = "Hiển thị";
                }
                menu.id = item.ItemID;
                menu.name = item.Name + " (" + menu.IsDisplayText + ")";
                menu.OrderBy = item.OrderBy;
                menu.ParentId = item.ParentID;
                menu.LinkMenu = siteRoot + item.LinkMenu;

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


        [HttpPost]
        public JsonResult SaveForm(FormCollection form)
        {
            try
            {
                string msg = "Thêm mới menu thành công";
                var menuId = form["MenuID"].ToIntOrZero();
                var menu = new coreMenu();
                if (menuId > 0)
                {
                    menu = new coreMenu(menuId);
                    menu.UpdatedDate = DateTime.Now;
                    msg = "Cập nhật Menu thành công";
                }
                else
                {
                    menu.CreatedDate = DateTime.Now;
                }
                menu.IsEnglish = false;
                if (!string.IsNullOrEmpty(form["IsEnglish"]))
                {
                    menu.IsEnglish = true;
                }
                menu.ParentID = form["ParentIDMenu"].ToIntOrZero();
                menu.ImageUrl = form["ImageUrl"];
                menu.LinkMenu = form["LinkMenu"];
                menu.Name = form["Name"];
                menu.OrderBy = form["OrderBy"].ToIntOrZero();
                menu.SiteID = form["SiteID"].ToShortOrZero();
                menu.TypeMenu = form["TypeMenu"].ToIntOrZero();
                menu.StyleCss = form["StyleCss"];
                menu.ItemLink = form["ItemLink"].ToLongOrNULL();
                menu.TypeLink = form["TypeLink"].ToIntOrZero();
                //menu.IsDisplayLink = form["IsDisplayLink"].ToBoolByOnOff();
                menu.Show = form["Show"].ToBoolByOnOff();
                menu.TargetBlank = form["TargetBlank"].ToBoolByOnOff();
                menu.IsLogin = form["IsLogin"].ToBooleanOnOff();
                menu.IsPhongBan = form["IsPhongBan"].ToBoolByOnOff();
                menu.NoClick = form["NoClick"].ToBoolByOnOff();
                menu.Save();
                return Json(new { Status = true, Message = msg, ItemID = menu.ItemID, Name = menu.Name });
            }
            catch (Exception ex)
            {

                return Json(new { Status = false, Message = "Không thể thực hiện thao tác này", MessageError = ex.Message });
            }

        }
        [HttpPost]
        public JsonResult DeleteMenu(int id = 0)
        {
            try
            {
                coreMenu.Delete(id);
                return Json(new { Status = true });
            }
            catch
            {

                return Json(new { Status = false });
            }

        }

        public PartialViewResult GetTypeLink(int siteId, int typeItem, string url, int menuId = 0)
        {
            TypeLinkModel model = new TypeLinkModel();
            model.UrlItem = url;
            model.TypeItem = typeItem;
            model.MenuObj = new coreMenu(menuId);
            if (typeItem == MenuTypeLinkConstant.Category)
            {
                model.ListLinkItem = BindCategories(siteId);
            }
            else if (typeItem == MenuTypeLinkConstant.Page)
            {
                model.ListLinkItem = BindPage(siteId, model.MenuObj.ItemLink);
            }
            return PartialView("_LinkItem", model);
        }


        public PartialViewResult FormMenu(int siteId, int typeMenu, int parentId = 0, int id = 0, string treeId = "")
        {
            FormMenuModel model = new FormMenuModel();
            model.ListItemLink = new List<SelectListItem>();
            model.Menu = new coreMenu(id);
            model.Menu.IsEnglish = false;
            if (id > 0)
            {
                parentId = model.Menu.ParentID;
                model.IsLogin = model.Menu.IsLogin;

                if (model.Menu.TypeLink == MenuTypeLinkConstant.Category)
                {
                    model.ListItemLink = BindCategories(siteId);
                }
                else if (model.Menu.TypeLink == MenuTypeLinkConstant.Page)
                {
                    model.ListItemLink = BindPage(siteId, model.Menu.ItemLink);
                }
            }
            model.TreeId = treeId;
            model.SiteID = siteId;
            model.ParentID = parentId;
            model.ListParent = ListItemMenu(false, siteId, typeMenu).Select(x => new SelectListItem { Text = x.Text, Value = x.Value, Selected = (x.Value == parentId.ToString()) }).ToList(); ;
            model.TypeMenu = typeMenu;
            model.Show = model.Menu.Show;
            model.ListTypeLink = MenuTypeLinkConstant.GetListItem(model.Menu.TypeLink)
                .Select(x => new SelectListItem { Text = x.Text, Value = x.Value, Selected = x.Selected }).ToList();
            return PartialView("_FormMenu", model);
        }

        public PartialViewResult FormMenuEnglish(int siteId, int typeMenu, int parentId = 0, int id = 0, string treeId = "")
        {
            FormMenuModel model = new FormMenuModel();
            model.Menu = new coreMenu(id);
            model.Menu.IsEnglish = true;
            if (id > 0)
            {
                parentId = model.Menu.ParentID;
                if (model.Menu.TypeLink == MenuTypeLinkConstant.Category)
                {
                    model.ListItemLink = BindCategories(siteId);
                }
                else if (model.Menu.TypeLink == MenuTypeLinkConstant.Page)
                {
                    model.ListItemLink = BindPage(siteId, model.Menu.ItemLink);
                }
            }
            model.TreeId = treeId;
            model.SiteID = siteId;
            model.ParentID = parentId;
            model.ListParent = ListItemMenu(true, siteId, typeMenu);
            model.TypeMenu = typeMenu;
            model.Show = model.Menu.Show;
            model.ListTypeLink = MenuTypeLinkConstant.GetListItem(model.Menu.TypeLink)
         .Select(x => new SelectListItem { Text = x.Text, Value = x.Value, Selected = x.Selected }).ToList();
            return PartialView("_FormMenu", model);
        }


        private List<SelectListItem> BindPage(int siteId, long? selected = 0)
        {

            var result = new List<SelectListItem>();
            List<PageBO> roots = PageBO.GetListParent(siteId).ToList();
            foreach (PageBO item in roots)
            {
                SelectListItem list = new SelectListItem
                {
                    Text = item.PageName,
                    Value = item.PageID.ToString()
                };
                result.Add(list);
            }
            for (int i = 0; i < result.Count; i++)
            {
                List<PageBO> children = PageBO.GetListChild(int.Parse(result[i].Value));
                if (children.Count <= 0) continue;
                string prefix = string.Empty;
                while (result[i].Text.StartsWith("|"))
                {
                    prefix += result[i].Text.Substring(0, 3);
                    result[i].Text = result[i].Text.Remove(0, 3);
                }
                result[i].Text = prefix + result[i].Text;
                int index = 1;
                foreach (PageBO child in children)
                {
                    SelectListItem list = new SelectListItem
                    {
                        Text = prefix + @"|--" + child.PageName,
                        Value = child.PageID.ToString()
                    };
                    result.Insert(result.IndexOf(result[i]) + index, list);
                    index++;
                }
            }
            return result;
        }


        private List<SelectListItem> BindCategories(int siteId)
        {
            var result = new List<SelectListItem>();
            var site = new SiteSettings(siteId);
            if (site.ArticleCategory > 0)
            {
                var coreCategory = new CoreCategory(site.ArticleCategory);
                var listChild = CoreCategory.GetChildrenByParent(site.ArticleCategory);
                if (listChild != null && listChild.Count > 0)
                {
                    result.AddRange(listChild.Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString() }));
                    PopulateChildNode(ref result);
                }
            }
            return result;
        }

        private void PopulateChildNode(ref List<SelectListItem> root)
        {
            for (int i = 0; i < root.Count; i++)
            {
                List<CoreCategory> children = CoreCategory.GetChildren(int.Parse(root[i].Value));
                if (children.Count <= 0) continue;
                string prefix = string.Empty;
                while (root[i].Text.StartsWith("|"))
                {
                    prefix += root[i].Text.Substring(0, 3);
                    root[i].Text = root[i].Text.Remove(0, 3);
                }
                root[i].Text = prefix + root[i].Text;
                int index = 1;
                foreach (CoreCategory child in children)
                {
                    SelectListItem list = new SelectListItem
                    {
                        Text = prefix + @"|--" + child.Name,
                        Value = child.ItemID.ToString()
                    };
                    root.Insert(root.IndexOf(root[i]) + index, list);
                    index++;
                }
            }
        }



        private List<SelectListItem> ListItemMenu(bool isEnglish, int siteId, int typeMenu, int parentId = 0, int selected = 0)
        {
            List<SelectListItem> root = new List<SelectListItem>();

            if (parentId > 0)
            {
                root = coreMenu.GetByParent(parentId, isEnglish, null).Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString(), Selected = (x.ItemID == selected) }).ToList();
            }
            else
            {
                root = coreMenu.GetParentRoot(siteId, typeMenu, isEnglish).Select(x => new SelectListItem { Text = x.Name, Value = x.ItemID.ToString(), Selected = (x.ItemID == selected) }).ToList();
            }
            for (int i = 0; i < root.Count; i++)
            {
                List<coreMenu> childens = coreMenu.GetByParent(root[i].Value.ToIntOrZero(), isEnglish, null);
                if (childens.Count <= 0) continue;
                string prefix = string.Empty;
                while (root[i].Text.StartsWith("|"))
                {
                    prefix += root[i].Text.Substring(0, 3);
                    root[i].Text = root[i].Text.Remove(0, 3);
                }
                root[i].Text = prefix + root[i].Text;
                int index = 1;
                foreach (coreMenu child in childens)
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
            return root;
        }
    }
}
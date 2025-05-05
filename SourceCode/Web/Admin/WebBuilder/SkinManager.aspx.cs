/// Author:					Joe Audette
/// Created:				2008-06-22
/// Last Modified:			2011-03-21
/// 
/// The use and distribution terms for this software are covered by the 
/// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
/// which can be found in the file CPL.TXT at the root of this distribution.
/// By using this software in any fashion, you are agreeing to be bound by 
/// the terms of this license.
///
/// You must not remove this notice, or any other, from this software.

using System;
using mojoPortal.Business.WebHelpers;
using Resources;
using mojoPortal.Web.Components;
using mojoPortal.Business;
using System.Linq;
using System.Web.Services;
using System.Web.Http;
using mojoPortal.Web.Framework;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.AdminUI
{

    public partial class SkinManager : NonCmsBasePage
    {
        private bool isAdmin = false;
        private bool isContentAdmin = false;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        protected void Page_Load(object sender, EventArgs e)
        {

            LoadSettings();

            if ((!isAdmin) && (!isContentAdmin))
            {
                SiteUtils.RedirectToAccessDeniedPage();
                return;
            }

            if (!siteSettings.IsServerAdminSite)
            {
                SiteUtils.RedirectToAccessDeniedPage();
                return;

            }

            PopulateLabels();
            PopulateControls();

        }

        private void BindParent()
        {
            ddlSkinTypeCreate.DataValueField = "Value";
            ddlSkinTypeCreate.DataTextField = "Text";
            ddlSkinTypeCreate.DataSource = SiteContants.GetListSkinType();
            ddlSkinTypeCreate.DataBind();
        }
        private void PopulateControls()
        {
            BindParent();
            BindCategory();
            BindSkin();
        }
        #region load category
        private void BindCategory()
        {
            ddlCategory.DataTextField = "Name";
            ddlCategory.DataValueField = "ItemID";
            ddlCategory.DataSource = GetCategory();
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem { Value = "", Text = "Danh mục" });
        }
        private List<CoreCategory> GetCategory()
        {
            List<CoreCategory> categories = new List<CoreCategory>();

            //CoreCategory defaultCat = new CoreCategory();
            //defaultCat.ItemID = 0;
            //defaultCat.Name = Resources.Resource.ParentCategoryChoose;
            //categories.Add(defaultCat);

            List<CoreCategory> roots = CoreCategory.GetCoreSkinDefaultRoot(siteSettings.SiteId);
            foreach (CoreCategory item in roots)
            {
                categories.Add(item);
            }
            PopulateChildItem(categories, 0);

            return categories;
        }
        private void PopulateChildItem(List<CoreCategory> root, int itemId)
        {
            for (int i = 0; i < root.Count; i++)
            {
                List<CoreCategory> children = CoreCategory.GetChildren(root[i].ItemID);
                if (children.Count <= 0) continue;
                string prefix = string.Empty;
                while (root[i].Name.StartsWith("|"))
                {
                    prefix += root[i].Name.Substring(0, 3);
                    root[i].Name = root[i].Name.Remove(0, 3);
                }
                root[i].Name = prefix + root[i].Name;
                int index = 1;
                foreach (CoreCategory child in children)
                {
                    if (child.ItemID.Equals(itemId)) continue;

                    child.Name = prefix + @"|--" + child.Name;
                    root.Insert(root.IndexOf(root[i]) + index, child);
                    index++;
                }
            }
        }


        #endregion
        protected string GetUrlSkinCategory(int skinID)
        {
            return string.Format("{0}/Admin/WebBuilder/SkinCategory.aspx?skins={1}", siteSettings.SiteRoot, skinID);
        }
        protected string GetUrlSkinPage(int skinID)
        {
            return string.Format("{0}/Admin/WebBuilder/SkinPageManager.aspx?skins={1}", siteSettings.SiteRoot, skinID);
        }
        private void BindSkin()
        {
            var listSkin = CoreSkin.GetAll().OrderBy(x => x.ItemID).ToList();
            foreach (var item in listSkin)
            {
                item.SkinTypeName = SiteContants.GetSkinType(item.SkinType);
            }
            rptSkin.DataSource = listSkin;
            rptSkin.DataBind();
        }

        [WebMethod]
        [HttpPost]
        public static void DeleteSkin(int id)
        {
            CoreSkin.Delete(id);
            #region Delete category skin
            CoreCategory.DeleteCoreSkin(id);
            #endregion
            #region  delete skin Page
            var listPage = CoreSkinPage.GetAll().Where(x => x.SkinID == id).ToList();
            if (listPage.Any())
            {
                foreach (var item in listPage)
                {
                    CoreSkinPage.Delete(item.ItemID);
                }
            }

            #endregion


        }

        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, "Quản trị template website");

            lnkAdminMenu.Text = Resource.AdminMenuLink;
            lnkAdminMenu.NavigateUrl = SiteRoot + "/Admin/AdminMenu.aspx";

            lnkWebBuilder.NavigateUrl = SiteRoot + "/Admin/WebBuilder/WebBuilderMenu.aspx";
            lnkWebBuilder.Text = "Xây dựng website";

            lnkCurrentPage.Text = "Quản trị template website";
            lnkCurrentPage.NavigateUrl = SiteRoot + "/Admin/WebBuilder/SkinManager.aspx";

            heading.Text = "Danh sách template website";
            btnSaveSkin.Text = "Thêm mới";

            UIHelper.DisableButtonAfterClick(
            btnSaveSkin,
            ArticleResources.ButtonDisabledPleaseWait,
            Page.ClientScript.GetPostBackEventReference(btnSaveSkin, string.Empty)
            );
        }

        private void LoadSettings()
        {
            isAdmin = WebUser.IsAdmin;
            isContentAdmin = WebUser.IsContentAdmin;

            AddClassToBody("administration");
            AddClassToBody("coredata");
        }


        #region OnInit

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);
            btnSaveSkin.Click += BtnSaveSkin_Click;
            SuppressMenuSelection();
            SuppressPageMenu();

        }

        private void BtnSaveSkin_Click(object sender, EventArgs e)
        {
            CoreSkin skin = null;

            if (!string.IsNullOrEmpty(hdfSkinID.Value))
            {
                skin = new CoreSkin(int.Parse(hdfSkinID.Value));
                if (ddlCategory.SelectedItem != null && ddlCategory.SelectedValue != "")
                {
                    skin.CategoryArticle = int.Parse(ddlCategory.SelectedValue);
                }
                #region xóa CoreCategory cũ
                CoreCategory.DeleteCoreSkin(int.Parse(hdfSkinID.Value));
                #endregion
            }
            else
            {
                skin = new CoreSkin();
            }
            skin.CreateDate = DateTime.Now;
            if (!string.IsNullOrEmpty(txtOrder.Text))
            {
                int order = -1;
                int.TryParse(txtOrder.Text, out order);
                skin.OrderBy = order;
            }
            int categoryID = -1;
            if (ddlCategory.SelectedItem != null && ddlCategory.SelectedValue != "")
            {
                int.TryParse(ddlCategory.SelectedValue, out categoryID);
                //skin.CategoryArticle = category;
            }
            skin.SkinType = int.Parse(ddlSkinTypeCreate.SelectedValue);
            skin.Title = txtSkinTitle.Text;
            skin.UserCreate = siteUser.UserId;
            skin.Save();

            #region Lưu danh sách category mặc định skin website
            //Nếu cập nhật skin thì không lưu mới CoreCategory
            if (!string.IsNullOrEmpty(hdfSkinID.Value))
            {

            }
            //Nêu thêm mới skin thì lưu danh sách coreCategory mặc đinh
            List<CategoryParentSkin> categoryParent = new List<CategoryParentSkin>();
            var listCategoryTemplate = CoreCategory.GetCoreSkinDefault(siteSettings.SiteId);
            if (listCategoryTemplate.Any())
            {
                foreach (var item in listCategoryTemplate)
                {
                    CoreCategory category = new CoreCategory();
                    category.Name = item.Name;
                    category.CreatedBy = item.CreatedBy;
                    category.Priority = item.Priority;
                    category.CoreSkinID = skin.ItemID;
                    category.Description = item.Description;
                    category.CoreSkinDefault = false;
                    category.Save();
                    //lưu skinCategoryID và itemID của template mặc định
                    categoryParent.Add(new CategoryParentSkin { CategoryID = category.ItemID, CategoryParentID = item.ItemID });
                    //Nếu parentID template mặc định > 0
                    if (categoryID > 0)
                    {
                        if (categoryID == item.ItemID)
                        {
                            skin.CategoryArticle = category.ItemID;
                            skin.Save();
                        }
                    }
                    if (item.ParentID > 0)
                    {
                        //Kiểm tra danh sách của CategoryParent nếu tồn tại thì xet parentID of CoreSkinCategory = CoreSkinCategory.ItemID
                        var parent = categoryParent.Where(x => x.CategoryParentID == item.ParentID).FirstOrDefault();
                        if (parent != null)
                        {
                            //CoreSkinCategory parentCategory = new CoreSkinCategory(parent.CategoryID);
                            //if (parentCategory != null)
                            //{
                            category.ParentID = parent.CategoryID;
                            category.Save();
                            //}
                        }
                    }
                }
            }

            #endregion

            Response.Redirect(Request.RawUrl);
        }

        #endregion
    }
    public class CategoryParentSkin
    {
        public int CategoryID { get; set; }
        public int CategoryParentID { get; set; }
    }
}
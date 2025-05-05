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
using mojoPortal.Web.Framework;
using mojoPortal.Business;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.Http;

namespace mojoPortal.Web.AdminUI
{

    public partial class SkinPageManager : NonCmsBasePage
    {
        private bool isAdmin = false;
        private bool isContentAdmin = false;
        private int skinID = 1;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadParam();
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
            BindPageParent();
            BindCategory();
            if (!IsPostBack)
            {
                PopulateControls();

            }

        }
        private void LoadParam()
        {
            skinID = WebUtils.ParseInt32FromQueryString("skins", skinID);
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




        private void BindPageParent()
        {
            ddlPageParent.DataTextField = "Title";
            ddlPageParent.DataValueField = "ItemID";
            ddlPageParent.DataSource = LoadSkinPage();
            ddlPageParent.DataBind();
            ddlPageParent.Items.Insert(0, new ListItem { Value = "", Text = "Chọn trang cha" });
        }
        private List<CoreSkinPage> LoadSkinPage()
        {
            List<CoreSkinPage> listPage = new List<CoreSkinPage>();
            List<CoreSkinPage> roots = CoreSkinPage.GetAll().Where(x => x.ParentID <= 0 && x.SkinID == skinID).ToList();
            foreach (CoreSkinPage item in roots)
            {
                listPage.Add(item);
            }
            PopulateChildItem(listPage, 0);

            return listPage;
        }
        private void PopulateChildItem(List<CoreSkinPage> root, int itemId)
        {
            for (int i = 0; i < root.Count; i++)
            {
                List<CoreSkinPage> children = CoreSkinPage.GetAll().Where(x => x.ParentID == root[i].ItemID).ToList();
                if (children.Count <= 0) continue;
                string prefix = string.Empty;
                while (root[i].Title.StartsWith("|"))
                {
                    prefix += root[i].Title.Substring(0, 3);
                    root[i].Title = root[i].Title.Remove(0, 3);
                }
                root[i].Title = prefix + root[i].Title;
                int index = 1;
                foreach (CoreSkinPage child in children)
                {
                    if (child.ItemID.Equals(itemId)) continue;

                    child.Title = prefix + @"|--" + child.Title;
                    root.Insert(root.IndexOf(root[i]) + index, child);
                    index++;
                }
            }
        }

        [System.Web.Services.WebMethod]
        [HttpPost]
        public static void DeletePage(int id)
        {
            CoreSkinPage.Delete(id);
            #region delete skin feature
            var listFeature = CoreSkinPageDefault.GetAll().Where(x => x.SkinPageID == id).ToList();
            if (listFeature.Any())
            {
                foreach (var item in listFeature)
                {
                    CoreSkinPageDefault.Delete(item.ItemID);
                }
            }
            #endregion
        }
        private void PopulateControls()
        {

            var listSkinPage = CoreSkinPage.GetAllBySkin(skinID).ToList();
            rptSkinPage.DataSource = listSkinPage;
            rptSkinPage.DataBind();
        }
        protected string GetUrlSkinPageFeature(int skinID, int pageID)
        {
            return string.Format("{0}/Admin/WebBuilder/SkinPageFeature.aspx?skins={1}&skinpageid={2}", siteSettings.SiteRoot, skinID, pageID);

        }

        protected string GetUrlPageBuilder( int skinPage)
        {
            return string.Format("{0}/Admin/WebBuilder/PageBuilder.aspx?skinID={1}skinpage={2}", siteSettings.SiteRoot,skinID, skinPage);
        }

        private void PopulateLabels()
        {

            lnkAdminMenu.Text = Resource.AdminMenuLink;
            lnkAdminMenu.NavigateUrl = SiteRoot + "/Admin/AdminMenu.aspx";

            lnkWebBuilder.NavigateUrl = SiteRoot + "/Admin/WebBuilder/WebBuilderMenu.aspx";
            lnkWebBuilder.Text = "Xây dựng website";

            lnkSkinManager.NavigateUrl = SiteRoot + "/Admin/WebBuilder/SkinManager.aspx";
            lnkSkinManager.Text = "Quản trị template website";
            Title = SiteUtils.FormatPageTitle(siteSettings, "Quản trị trang thuộc template website");
            lnkCurrentPage.Text = "Quản trị trang thuộc template website";
            lnkCurrentPage.NavigateUrl = SiteRoot + "/Admin/WebBuilder/SkinPageManager.aspx";
            var skin = new CoreSkin(skinID);
            if (skin != null)
            {
                heading.Text = "Danh sách trang thuộc template website - " + skin.Title;
            }
            else
            {

                heading.Text = "Danh sách trang thuộc template website";
            }



            btnCreatePage.Text = "Thêm mới";

            UIHelper.DisableButtonAfterClick(
            btnCreatePage,
            ArticleResources.ButtonDisabledPleaseWait,
            Page.ClientScript.GetPostBackEventReference(btnCreatePage, string.Empty)
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
            btnCreatePage.Click += BtnCreatePage_Click;
            SuppressMenuSelection();
            SuppressPageMenu();

        }



        protected void BtnCreatePage_Click(object sender, EventArgs e)
        {
            CoreSkinPage skinPage = null;
            if (!string.IsNullOrEmpty(hdfPageID.Value))
            {
                skinPage = new CoreSkinPage(int.Parse(hdfPageID.Value));
            }
            else
            {
                skinPage = new CoreSkinPage();
            }
            skinPage.Title = txtTitlePage.Text;
            skinPage.CreateDate = DateTime.Now;
            skinPage.SkinID = skinID;
            int parentID = -1;
            if (ddlPageParent.SelectedItem != null && !string.IsNullOrEmpty(ddlPageParent.SelectedItem.Value))
            {
                try
                {
                    parentID = int.Parse(ddlPageParent.SelectedValue);

                }
                catch
                {

                    parentID = -1;
                }
            }
            int categoryID = -1;
            if (ddlCategory.SelectedItem != null && !string.IsNullOrEmpty(ddlCategory.SelectedItem.Value))
            {
                try
                {
                    categoryID = int.Parse(ddlCategory.SelectedValue);

                }
                catch
                {

                    categoryID = -1;
                }

                skinPage.CategoryID = categoryID;
            }
            skinPage.ParentID = parentID;
            if (!string.IsNullOrEmpty(txtOrder.Text))
            {
                //int orderby;
                //int.TryParse(txtOrder.Text, out orderby);
                try
                {
                    skinPage.OrderBy = int.Parse(txtOrder.Text);

                }
                catch (Exception)
                {

                    skinPage.OrderBy = -1;
                }
            }
            skinPage.UserCreate = siteUser.UserId;
            skinPage.Save();
            Response.Redirect(Request.RawUrl);
        }

        #endregion
    }
}
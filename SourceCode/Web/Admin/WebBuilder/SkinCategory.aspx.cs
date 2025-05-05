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

    public partial class SkinCategory : NonCmsBasePage
    {
        private bool isAdmin = false;
        private bool isContentAdmin = false;
        private int skinID = -1;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        protected void Page_Load(object sender, EventArgs e)
        {

            LoadSettings();
            PopulateParameter();
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

        private void PopulateParameter()
        {
            skinID = WebUtils.ParseInt32FromQueryString("skins", skinID);
        }
        private void PopulateControls()
        {
            BindCategory();
            BindDropCategory();
        }

        private void BindCategory()
        {
            rptCategory.DataSource = CoreCategory.GetBySkin(skinID);
            rptCategory.DataBind();
        }
        private void BindDropCategory()
        {
            ddlCategory.DataValueField = "ItemID";
            ddlCategory.DataTextField = "Name";
            ddlCategory.DataSource = GetCategory();
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem { Text = "Danh mục cha", Value = "" });
        }

        private List<CoreCategory> GetCategory()
        {
            List<CoreCategory> categories = new List<CoreCategory>();

            //CoreCategory defaultCat = new CoreCategory();
            //defaultCat.ItemID = 0;
            //defaultCat.Name = Resources.Resource.ParentCategoryChoose;
            //categories.Add(defaultCat);

            List<CoreCategory> roots = new List<CoreCategory>();
            if(skinID > 0)
            {
                roots = CoreCategory.GetBySkin(skinID);
            }
            else
            {
                roots = CoreCategory.GetCoreSkinDefault(siteSettings.SiteId);
            }
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

        [WebMethod]
        [HttpPost]
        public static void DeleteCategory(int id)
        {
            CoreCategory.Delete(id);
        }

        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, "Quản trị template website");

            lnkAdminMenu.Text = Resource.AdminMenuLink;
            lnkAdminMenu.NavigateUrl = SiteRoot + "/Admin/AdminMenu.aspx";

            lnkWebBuilder.NavigateUrl = SiteRoot + "/Admin/WebBuilder/WebBuilderMenu.aspx";
            lnkWebBuilder.Text = "Xây dựng website";

            lnkCurrentPage.Text = "Danh sách danh mục";
            lnkCurrentPage.NavigateUrl = SiteRoot + "/Admin/WebBuilder/SkinCategory.aspx?skins=" + skinID;
            lnkSkinManager.NavigateUrl = SiteRoot + "/Admin/WebBuilder/SkinManager.aspx";
            lnkSkinManager.Text = "Quản trị template website";
            var coreSkin = new CoreSkin(skinID);
            if (coreSkin != null)
            {
                heading.Text = "Danh sách danh mục thuộc template - " + coreSkin.Title;
                btnSaveCategory.Text = "Thêm mới";
            }
            else
            {
                heading.Text = "Danh sách danh mục thuộc template";
                btnSaveCategory.Text = "Thêm mới";
            }


            UIHelper.DisableButtonAfterClick(
            btnSaveCategory,
            ArticleResources.ButtonDisabledPleaseWait,
            Page.ClientScript.GetPostBackEventReference(btnSaveCategory, string.Empty)
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
            btnSaveCategory.Click += btnSaveCategory_Click;
            SuppressMenuSelection();
            SuppressPageMenu();

        }

        private void btnSaveCategory_Click(object sender, EventArgs e)
        {
            CoreCategory category = null;

            if (!string.IsNullOrEmpty(hdfCategoryID.Value))
            {
                category = new CoreCategory(int.Parse(hdfCategoryID.Value));
            }
            else
            {
                category = new CoreCategory();
            }
            if (!string.IsNullOrEmpty(txtOrder.Text))
            {
                int order = -1;
                int.TryParse(txtOrder.Text, out order);
                category.Priority = order;
            }
            if (ddlCategory.SelectedItem != null && ddlCategory.SelectedValue != "")
            {
                category.ParentID = int.Parse(ddlCategory.SelectedValue);
            }
            category.SiteID = SiteId;
            category.Name = txtCategoryName.Text;
            category.Description = txtUrl.Text;
            category.CoreSkinDefault = false;
            category.CoreSkinID = skinID;
            category.Save();
            Response.Redirect(Request.RawUrl);
        }

        #endregion
    }
}
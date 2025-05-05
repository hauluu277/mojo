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

namespace mojoPortal.Web.AdminUI
{

    public partial class WebBuilderMenu : NonCmsBasePage
    {
        private bool isAdmin = false;
        private bool isContentAdmin = false;

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

        private void PopulateControls()
        {


        }


        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, "Xây dựng subPortal");

            lnkAdminMenu.Text = Resource.AdminMenuLink;
            lnkAdminMenu.NavigateUrl = SiteRoot + "/Admin/AdminMenu.aspx";

            lnkCurrentPage.Text = "Xây dựng subPortal";
            lnkCurrentPage.NavigateUrl = SiteRoot + "/Admin/WebBuilder/WebBuilderMenu.aspx";

            heading.Text = "Xây dựng subPortal";
            //xây dựng website
            lnkBuilderWebsite.Visible = siteSettings.IsServerAdminSite;
            lnkBuilderWebsite.Text = "Tạo subPortal";
            lnkBuilderWebsite.NavigateUrl = SiteRoot + "/Admin/WebBuilder/WebBuilder.aspx?siteid=-1";

            //Danh sách template website
            lnkListTemplate.Visible = siteSettings.IsServerAdminSite;
            lnkListTemplate.Text = "Quản trị template subPortal";
            lnkListTemplate.NavigateUrl = SiteRoot + "/Admin/WebBuilder/WebBuilderManager.aspx";

            lnkCategoryTemplate.Text = "Danh mục mặc định";
            lnkCategoryTemplate.NavigateUrl = SiteRoot + "/Admin/WebBuilder/CategoryTemplate.aspx";

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

            SuppressMenuSelection();
            SuppressPageMenu();

        }

        #endregion
    }
}
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

    public partial class SystemMenu : NonCmsBasePage
    {
        private bool IsAdmin = false;
        private bool isContentAdmin = false;
        private bool IsAdminOrContentAdmin = false;
        private bool isSiteEditor = false;
        protected void Page_Load(object sender, EventArgs e)
        {

            LoadSettings();

            if ((!IsAdmin) && (!isContentAdmin))
            {
                SiteUtils.RedirectToAccessDeniedPage();
                return;
            }

            if (!siteSettings.IsServerAdminSite && siteSettings.SiteId == 1)
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
            Title = SiteUtils.FormatPageTitle(siteSettings, "Quản trị hệ thống");
            liSettingWebBuilder.Visible = WebUser.IsInRoles(WebConfigSettings.RoleManageSubPortal);
            lnkSettingWebBuilder.Visible = IsAdmin;
            lnkSettingWebBuilder.Text = "Xây dựng subPortal";
            lnkSettingWebBuilder.NavigateUrl = SiteRoot + "/Admin/WebBuilder/WebBuilderMenu.aspx";

            lnkAdminMenu.Text = Resource.AdminMenuLink;
            lnkAdminMenu.NavigateUrl = SiteRoot + "/Admin/AdminMenu.aspx";

            lnkCurrentPage.Text = "Quản trị hệ thống";
            lnkCurrentPage.NavigateUrl = SiteRoot + "/Admin/Article/ArticleMenu.aspx";

            heading.Text = "Quản trị hệ thống";
            liAdvancedTools.Visible = (IsAdminOrContentAdmin || isSiteEditor);
            lnkAdvancedTools.Text = Resource.AdvancedToolsLink;
            lnkAdvancedTools.NavigateUrl = SiteRoot + "/Admin/AdvancedTools.aspx";


            liServerInfo.Visible = (IsAdminOrContentAdmin || isSiteEditor) && (siteSettings.IsServerAdminSite || WebConfigSettings.ShowSystemInformationInChildSiteAdminMenu);
            lnkServerInfo.Text = Resource.AdminMenuServerInfoLabel;
            lnkServerInfo.NavigateUrl = SiteRoot + "/Admin/ServerInformation.aspx";

            liLogViewer.Visible = IsAdmin && siteSettings.IsServerAdminSite && WebConfigSettings.EnableLogViewer;
            lnkLogViewer.Text = Resource.AdminMenuServerLogLabel;
            lnkLogViewer.NavigateUrl = SiteRoot + "/Admin/ServerLog.aspx";


            //
            // Member List
            //
            lnkMemberList.Text = Resource.MemberListLink;
            lnkMemberList.NavigateUrl = SiteRoot + WebConfigSettings.MemberListUrl;
            lnkMemberList.Visible = ((WebUser.IsInRoles(siteSettings.RolesThatCanViewMemberList)) || (WebUser.IsInRoles(siteSettings.RolesThatCanLookupUsers)));

            liAddUser.Visible = ((WebUser.IsInRoles(siteSettings.RolesThatCanCreateUsers)) || (WebUser.IsInRoles(siteSettings.RolesThatCanManageUsers)) || (WebUser.IsInRoles(siteSettings.RolesThatCanLookupUsers)));
            lnkAddUser.Text = Resource.MemberListAddUserLabel;
            lnkAddUser.NavigateUrl = SiteRoot + "/Admin/ManageUsers.aspx?userId=-1";

            if ((WebConfigSettings.UseRelatedSiteMode) && (WebConfigSettings.RelatedSiteModeHideRoleManagerInChildSites))
            {
                if (siteSettings.SiteId != WebConfigSettings.RelatedSiteID)
                {
                    liRoleAdmin.Visible = false;
                }
            }

            liPageTree.Visible = (IsAdminOrContentAdmin || isSiteEditor || (SiteMapHelper.UserHasAnyCreatePagePermissions(siteSettings)));
            lnkPageTree.Text = Resource.AdminMenuPageTreeLink;
            lnkPageTree.NavigateUrl = SiteRoot + WebConfigSettings.PageTreeRelativeUrl;

            liRoleAdmin.Visible = (siteSettings.SiteId == 1 && (WebUser.IsRoleAdmin || IsAdmin));
            lnkRoleAdmin.Text = "Quản lý vai trò";
            lnkRoleAdmin.NavigateUrl = SiteRoot + "/Admin/RoleManager.aspx";

            liPermissions.Visible = IsAdmin;
            lnkPermissionAdmin.Text = Resource.SiteSettingsPermissionsTab;
            lnkPermissionAdmin.NavigateUrl = SiteRoot + "/Admin/PermissionsMenu.aspx";

            liContentManager.Visible = (IsAdminOrContentAdmin || isSiteEditor);
            lnkContentManager.Text = Resource.AdminMenuContentManagerLink;
            lnkContentManager.NavigateUrl = SiteRoot + "/Admin/ContentCatalog.aspx";


            //hoạt động người dùng
            liHoatDongNguoiDung.Visible = WebUser.IsInRoles(WebConfigSettings.RoleManageHoatDongNguoiDung);
            hplHoatDongNguoiDung.Text = "Hoạt động người dùng";
            hplHoatDongNguoiDung.NavigateUrl = SiteRoot + "/logsystem/manage.aspx";


            liSettingSSO.Visible = IsAdmin;



            liSettingSSO.Visible = IsAdmin;
            linkSettingSSO.Text = "Cấu hình đăng nhập SSO";
            linkSettingSSO.ToolTip = "Cấu hình đăng nhập SSO";
            linkSettingSSO.NavigateUrl = SiteRoot + "/SettingSSOArea/SettingSSO/index";



            liSiteList.Visible = IsAdminOrContentAdmin || isSiteEditor;
            lnkSiteList.Text = "Danh sách Site";
            lnkSiteList.NavigateUrl = SiteRoot + "/Admin/SiteList.aspx";

            liSiteSettings.Visible = IsAdminOrContentAdmin || isSiteEditor;
            lnkSiteSettings.Text = Resource.AdminMenuSiteSettingsLink;
            lnkSiteSettings.NavigateUrl = SiteRoot + "/Admin/SiteSettings.aspx";
        }

        private void LoadSettings()
        {
            IsAdmin = WebUser.IsAdmin;
            isSiteEditor = SiteUtils.UserIsSiteEditor();
            isContentAdmin = WebUser.IsContentAdmin;
            IsAdminOrContentAdmin = WebUser.IsAdminOrContentAdmin;
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
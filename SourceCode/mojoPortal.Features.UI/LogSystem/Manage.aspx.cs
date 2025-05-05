using ArticleFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.SearchIndex;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LogSystemFeature.UI
{
    public partial class Manage : mojoBasePage
    {
        protected ArticleConfiguration config = new ArticleConfiguration();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private int reloadArticleLucenIndex = 0;
        private int pageId = -1;
        private int moduleId = -1;
        private bool userCanEdit;
        private static TimeZoneInfo _timeZone;
        protected static Double _timeOffset;
        override protected void OnInit(EventArgs e)
        {
            LoadParams();
            Load += Page_Load;
            base.OnInit(e);
            LoadSettings();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                SiteUtils.RedirectToLoginPage(this);
                return;
            }
            if (!WebUser.IsInRoles(WebConfigSettings.RoleManageHoatDongNguoiDung))
            {
                SiteUtils.RedirectToAccessDeniedPage(this);
                return;
            }
            SecurityHelper.DisableBrowserCache();


            PopulateLabels();

            PopulateControls();
        }
        private void PopulateControls()
        {
            PostList.ModuleId = moduleId;
            PostList.PageId = pageId;
            PostList.Config = config;
            PostList.SiteRoot = SiteRoot;
        }
        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, ArticleResources.PostList);
            TitleControl.Visible = false;
            TitleControl.ModuleInstance = GetModule(moduleId);
            heading.Text = "Danh sách hoạt động người dùng";

        }

        private void LoadSettings()
        {
            userCanEdit = UserCanEditModule(moduleId);
            pnlContainer.ModuleId = moduleId;
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new ArticleConfiguration(moduleSettings);
            _timeZone = SiteUtils.GetUserTimeZone();
            _timeOffset = SiteUtils.GetUserTimeOffset();
            pnlAdminCrumbs.Visible = false;
            pnlAdminCrumbs.Visible = true;
            lnkCurrentPage.NavigateUrl = SiteRoot + "/LogSystem/Manage.aspx";
            lnkCurrentPage.Text = "Danh sách hoạt động thành viên";

            lnkAdminMenu.Text = "Quản trị hệ thống";
            lnkAdminMenu.NavigateUrl = SiteRoot + "/Admin/AdminMenu.aspx";
        }
    }
}
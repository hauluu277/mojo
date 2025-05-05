using ArticleFeature.Business;
using mojoPortal.Business;
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

namespace ArticleFeature.UI
{
    public partial class CategoryPage : mojoBasePage
    {
        protected ArticleConfiguration config = new ArticleConfiguration();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private int reloadArticleLucenIndex = 0;
        private int pageId = -1;
        private int moduleId = -1;
        private int categoryId = -1;
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
            SecurityHelper.DisableBrowserCache();
            PopulateLabels();
            PopulateControls();
        }
        private void PopulateControls()
        {
            CategoryPageControl.ModuleId = moduleId;
            CategoryPageControl.PageId = pageId;
            CategoryPageControl.Config = config;
            CategoryPageControl.SiteRoot = SiteRoot;
            CategoryPageControl.ImageSiteRoot = ImageSiteRoot;
            CategoryPageControl.SiteId = SiteId;
        }
        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, ArticleResources.PostList);
            TitleControl.Visible = false;
            TitleControl.ModuleInstance = GetModule(moduleId);

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
            categoryId = WebUtils.ParseInt32FromQueryString("catid", categoryId);
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new ArticleConfiguration(moduleSettings);
            _timeZone = SiteUtils.GetUserTimeZone();
            _timeOffset = SiteUtils.GetUserTimeOffset();
            pnlAdminCrumbs.Visible = true;
            var category = new CoreCategory(categoryId);
            lnkCurrentPage.NavigateUrl = SiteRoot + "/Article/CategoryPage.aspx?catid=" + categoryId;
            lnkCurrentPage.Text = category.Name;


            lnkAdminMenu.Text = "Trang chủ";
            lnkAdminMenu.NavigateUrl = SiteRoot + "/";
        }

    }
}
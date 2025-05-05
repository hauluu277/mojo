using mojoPortal.Business;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BannerFeature.UI
{
    public partial class ManagePost : mojoBasePage
    {
        protected BannerConfiguration config = new BannerConfiguration();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private int pageId = -1;
        private int moduleId = -1;
        private bool userCanEdit;

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
            SecurityHelper.DisableBrowserCache();


            if (!userCanEdit)
            {
                SiteUtils.RedirectToEditAccessDeniedPage();
            }

            PopulateLabels();

            PopulateControls();
        }
        private void PopulateControls()
        {
            PostList.ModuleId = moduleId;
            PostList.PageId = pageId;
            PostList.Config = config;
            PostList.SiteRoot = SiteRoot;
            PostList.ImageSiteRoot = ImageSiteRoot;
        }
        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, ArticleResources.PostList);
            TitleControl.Visible = false;
            TitleControl.ModuleInstance = GetModule(moduleId);
            if (siteUser.IsInRoles("Admins"))
            {
                TitleControl.Visible = true;
            }

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
            config = new BannerConfiguration(moduleSettings);
        }
    }
}
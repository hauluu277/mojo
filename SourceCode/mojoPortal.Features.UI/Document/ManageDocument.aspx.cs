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

namespace DocumentFeature.UI
{
    public partial class ManageDocument : mojoBasePage
    {
        protected DocumentConfiguration config = new DocumentConfiguration();
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

            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }
        private void PopulateControls()
        {
            if(pageId <= -1)
            {
                pageId = 3986;
            }
            ManageControl.ModuleId = moduleId;
            ManageControl.PageId = pageId;
            ManageControl.Config = config;
            ManageControl.SiteRoot = SiteRoot;
            ManageControl.ImageSiteRoot = ImageSiteRoot;
        }
        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, ArticleResources.PostList);
            BreadCrumbControl.InfoLink_1 = new mojoPortal.Business.WebHelpers.CommonModel.InfoLink();
            BreadCrumbControl.InfoLink_1.NameLink = "Quản lý văn bản";
            BreadCrumbControl.InfoLink_1.UrlLink = "/Document/ManageDocument.aspx";
            BreadCrumbControl.InfoLink_1.ActiveLink = true;

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
            config = new DocumentConfiguration(moduleSettings);
        }
    }
}
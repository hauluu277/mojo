using mojoPortal.Business;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Web.UI;

namespace EventFeature.UI
{
    public partial class ManagePost : mojoBasePage
    {
        protected EventConfiguration config = new EventConfiguration();
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


            //if (!userCanEdit)
            if (!CommonEvent.AccessManageEvent)
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
            eventList.ModuleId = moduleId;
            eventList.PageId = pageId;
            eventList.Config = config;
            eventList.SiteRoot = SiteRoot;
            eventList.ImageSiteRoot = ImageSiteRoot;
        }
        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, EventResources.PostList);
            TitleControl.Visible = false;
            TitleControl.ModuleInstance = GetModule(moduleId);
            if (siteUser.IsInRoles("Admins"))
            {
                TitleControl.Visible = true;
            }
            heading.Text = "Quản lý danh sách sự kiện";
            
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
            config = new EventConfiguration(moduleSettings);
        }
    }
}
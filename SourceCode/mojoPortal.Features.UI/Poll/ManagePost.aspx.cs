using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
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

namespace PollFeature.UI
{
    public partial class ManagePost : mojoBasePage
    {
        protected PollConfiguration config = new PollConfiguration();
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


            if (userCanEdit || WebUser.IsInRoles(config.RoleApprove) || WebUser.IsInRoles(config.RolePublish))
            {
                //continue
            }else
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
            QuestionList.ModuleId = moduleId;
            QuestionList.PageId = pageId;

        }
        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, PollResources.PollExplorationManager);
            TitleControl.Visible = false;
            TitleControl.ModuleInstance = GetModule(moduleId);
            if (WebUser.IsInRoles(config.RoleApprove) || WebUser.IsInRoles(config.RolePublish))
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
            config = new PollConfiguration(moduleSettings);
        }
    }
}
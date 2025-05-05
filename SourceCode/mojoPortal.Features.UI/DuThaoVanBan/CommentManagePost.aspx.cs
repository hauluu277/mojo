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

namespace DuThaoVanBanFeature.UI
{
    public partial class CommentManagePost : mojoBasePage
    {
        protected DuThaoVanBanConfiguration config = new DuThaoVanBanConfiguration();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private int pageId = -1;
        private int moduleId = -1;
        private bool isUserApprove = false;
        private bool isAdmin = false;
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

            PopulateLabels();

            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }
        private void PopulateControls()
        {
            CommentsList.ModuleId = moduleId;
            CommentsList.PageId = pageId;
            CommentsList.Config = config;
            CommentsList.SiteRoot = SiteRoot;
            CommentsList.ImageSiteRoot = ImageSiteRoot;
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
            if ((WebUser.IsAdminOrContentAdmin) || (SiteUtils.UserIsSiteEditor())) { isAdmin = true; }
            userCanEdit = UserCanEditModule(moduleId);
            pnlContainer.ModuleId = moduleId;
            if (!isUserApprove && !isAdmin)
            {
                SiteUtils.RedirectToAccessDeniedPage(this);
                return;
            }
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new DuThaoVanBanConfiguration(moduleSettings);
            if (WebUser.IsInRoles(config.RoleApprove))
            {
                isUserApprove = true;
            }
        }
    }
}
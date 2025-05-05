using mojoPortal.Business;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using System.Collections;
using mojoPortal.Business.WebHelpers;

namespace QuestionAnswerFeatures.UI
{
    public partial class ManagePost : mojoBasePage
    {
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private int pageId = -1;
        private int moduleId = -1;
        private bool userCanEdit;
        override protected void OnInit(EventArgs e)
        {
            Load += Page_Load;
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                SiteUtils.RedirectToLoginPage(this);
                return;
            }
            if (!WebUser.IsInRoles(WebConfigSettings.RoleManageHoiDap))
            {
                SiteUtils.RedirectToAccessDeniedPage(this);
                return;
            }
            SecurityHelper.DisableBrowserCache();

            LoadParams();
            LoadSettings();




            //if (!userCanEdit)
            //{
            //    SiteUtils.RedirectToEditAccessDeniedPage();
            //}

            PopulateLabels();

            PopulateControls();

        }
        private void PopulateControls()
        {
            PostList.ModuleId = moduleId;
            PostList.PageId = pageId;
            PostList.SiteRoot = SiteRoot;
        }
        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, SwirlingQuestionResource.QuanTriHoiDapTitle);
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
        }
    }
}
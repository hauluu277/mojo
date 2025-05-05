using MediaFeature.UI;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
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

namespace mojoPortal.Features.UI.Audio
{
    public partial class ManageAudio : mojoBasePage
    {
        protected MediaConfiguration config = new MediaConfiguration();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private int pageId = -1;
        private int moduleId = -1;
        private bool userCanEdit;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                SiteUtils.RedirectToLoginPage(this);
                return;
            }
            if (!WebUser.IsInRoles(WebConfigSettings.RoleManageAudio))
            {
                SiteUtils.RedirectToAccessDeniedPage(this);
                return;
            }
            SecurityHelper.DisableBrowserCache();


            //if (!userCanEdit)
            //{
            //    SiteUtils.RedirectToEditAccessDeniedPage();
            //}
            LoadParams();
            PopulateLabels();

            PopulateControls();
        }
        private void PopulateControls()
        {


        }
        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, MediaResources.ManageDataMultiMedia);
            TitleControl.Visible = true;
            TitleControl.EditText = "Quản lý audio";
            TitleControl.ModuleInstance = GetModule(moduleId);
            heading.Text = "Quản lý audio";
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
            config = new MediaConfiguration(moduleSettings);
        }
    }
}
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using System;
using System.Collections;

namespace LichCongTacFeature.UI
{
    public partial class ManagePostNew : mojoBasePage
    {
        //protected TrainingConfiguaration config = new TrainingConfiguaration();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private int pageId = -1;
        private int moduleId = -1;
        private bool userCanEdit;
        protected override void OnInit(EventArgs e)
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
            if (!WebUser.IsInRoles(WebConfigSettings.RoleManageLichCongTac))
            {
                SiteUtils.RedirectToAccessDeniedPage(this);
                return;
            }
            SecurityHelper.DisableBrowserCache();


            PopulateLables();

        }

        private void PopulateLables()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, "Quản lý lịch công tác");
        }
        private void LoadSettings()
        {
            userCanEdit = UserCanEditModule(moduleId);
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            Hashtable moduleSetting = ModuleSettings.GetModuleSettings(moduleId);
            //config = new TrainingConfiguaration(moduleSetting);
        }
    }
}
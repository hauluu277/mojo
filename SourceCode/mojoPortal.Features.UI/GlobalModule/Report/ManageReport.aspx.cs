using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Web.UI;

namespace ReportFeatures.UI
{
    public partial class ManageReport : mojoBasePage
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
            if (!WebUser.IsInRoles(WebConfigSettings.RoleManageCongKhaiNganSach))
            {
                SiteUtils.RedirectToAccessDeniedPage(this);
                return;
            }
            SecurityHelper.DisableBrowserCache();


            PopulateLables();

        }

        private void PopulateLables()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, "Quản lý báo cáo ngân sách");
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
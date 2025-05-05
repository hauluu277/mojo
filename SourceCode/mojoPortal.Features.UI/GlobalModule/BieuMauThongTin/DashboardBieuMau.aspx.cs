using mojoPortal.Business;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Web.UI;

namespace BieuMauThongTinFeatures.UI
{
    public partial class DashboardBieuMau : mojoBasePage
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
            SecurityHelper.DisableBrowserCache();


            PopulateLables();

        }

        private void PopulateLables()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, "Biểu mẫu thông tin");
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
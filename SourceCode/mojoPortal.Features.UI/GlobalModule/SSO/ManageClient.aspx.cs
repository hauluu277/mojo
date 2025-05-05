using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Web.UI;

namespace SSOFeatures.UI
{
    public partial class ManageClient : mojoBasePage
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
            if (!WebUser.IsInRoles(WebConfigSettings.RoleCongThanhVien))
            {
                SiteUtils.RedirectToAccessDeniedPage(this);
                return;
            }
            SecurityHelper.DisableBrowserCache();


            PopulateLables();

        }

        private void PopulateLables()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, "Danh sách cổng thành viên");
            BreadCrumbControl.InfoLink_1 = new mojoPortal.Business.WebHelpers.CommonModel.InfoLink();
            BreadCrumbControl.InfoLink_1.NameLink = "Danh sách cổng thành viên";
            BreadCrumbControl.InfoLink_1.UrlLink = "/GlobalModule/SSO/ManageClient.aspx";
            BreadCrumbControl.InfoLink_1.ActiveLink = true;
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
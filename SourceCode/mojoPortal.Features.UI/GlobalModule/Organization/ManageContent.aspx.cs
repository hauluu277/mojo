using mojoPortal.Business;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Text;
using System.Web.UI;

namespace OrganizationFeatures.UI
{
    public partial class ManageContent : mojoBasePage
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
            StringBuilder scriptFunc = new StringBuilder();
            scriptFunc.Append("$(document).ready(function(){");
            scriptFunc.Append("CallAjaxLoading('get','/OrganizationArea/Organization/Index',{siteId:" + siteSettings.SiteId + "},true,function(rs){");
            scriptFunc.Append("$('#content_unit').html(rs);");
            scriptFunc.Append("});");
            scriptFunc.Append("});");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadOrganization", scriptFunc.ToString(), true);
        }

        private void PopulateLables()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, "Cập nhật thông tin cơ cấu tổ chức");
            BreadCrumbControl.InfoLink_1 = new mojoPortal.Business.WebHelpers.CommonModel.InfoLink();
            BreadCrumbControl.InfoLink_1.NameLink = "Cập nhật thông tin cơ cấu tổ chức";
            BreadCrumbControl.InfoLink_1.UrlLink = "/GlobalModule/Organization/ManageContent.aspx";
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
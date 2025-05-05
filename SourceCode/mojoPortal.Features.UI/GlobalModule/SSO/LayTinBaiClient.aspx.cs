using mojoPortal.Business;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Text;
using System.Web.UI;

namespace SSOFeatures.UI
{
    public partial class LayTinBaiClient : mojoBasePage
    {
        //protected TrainingConfiguaration config = new TrainingConfiguaration();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private int pageId = -1;
        private int moduleId = -1;
        private bool userCanEdit;
        private int idCoreClient = -1;
        private string catname = string.Empty;
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
            var idDiv = report_div.ClientID;
            StringBuilder scriptFunc = new StringBuilder();
            scriptFunc.Append("$(document).ready(function(){");
            scriptFunc.Append("CallAjaxLoading('get','/clientarea/client/IndexLayTinBaiClient',{idClientCategory : "+ idCoreClient + "},true,function(rs){");
            scriptFunc.Append("$('#" + idDiv + "').html(rs);");
            scriptFunc.Append("});");
            scriptFunc.Append("});");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadReport_" + idDiv, scriptFunc.ToString(), true);
        }

        private void PopulateLables()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, "Danh sách ứng dụng tích hợp ");
            BreadCrumbControl.InfoLink_1 = new mojoPortal.Business.WebHelpers.CommonModel.InfoLink();
            BreadCrumbControl.InfoLink_1.NameLink = "Lấy tin bài từ chuyên mục cổng thành viên";
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
            idCoreClient = WebUtils.ParseInt32FromQueryString("idCoreClient", -1);
            catname = WebUtils.ParseStringFromQueryString("catname", catname);
            Hashtable moduleSetting = ModuleSettings.GetModuleSettings(moduleId);
            //config = new TrainingConfiguaration(moduleSetting);
        }
    }
}
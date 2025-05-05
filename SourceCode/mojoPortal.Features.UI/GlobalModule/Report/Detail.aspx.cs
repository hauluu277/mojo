using mojoPortal.Business;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Text;
using System.Web.UI;

namespace ReportFeatures.UI
{
    public partial class Detail : mojoBasePage
    {
        //protected TrainingConfiguaration config = new TrainingConfiguaration();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private int pageId = -1;
        private int moduleId = -1;
        private bool userCanEdit;
        private int reportId = 0;
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
            if (!IsPostBack)
            {
                var idDiv = report.ClientID;
                StringBuilder scriptFunc = new StringBuilder();
                scriptFunc.Append("$(document).ready(function(){");
                scriptFunc.Append("CallAjaxLoading('get','/DieuTraArea/DieuTra/ReportDetail',{reportId:" + reportId + "},true,function(rs){");
                scriptFunc.Append("$('#" + idDiv + "').html(rs);");
                scriptFunc.Append("});");
                scriptFunc.Append("});");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "LoadReport_" + idDiv, scriptFunc.ToString(), true);
            }
        }

        private void PopulateLables()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, "Báo cáo thống kê cuộc điều tra");
            BreadCrumbControl.InfoLink_1 = new mojoPortal.Business.WebHelpers.CommonModel.InfoLink();
            BreadCrumbControl.InfoLink_1.NameLink = "Báo cáo thống kê các cuộc điều tra";
            BreadCrumbControl.InfoLink_1.UrlLink = "/GlobalModule/Report/ManagerPost.aspx";

            BreadCrumbControl.InfoLink_2 = new mojoPortal.Business.WebHelpers.CommonModel.InfoLink();
            BreadCrumbControl.InfoLink_2.ActiveLink = true;
            BreadCrumbControl.InfoLink_2.NameLink = SiteSettings.GetName(reportId);
        }
        private void LoadSettings()
        {
            userCanEdit = UserCanEditModule(moduleId);
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            reportId = WebUtils.ParseInt32FromQueryString("reportid", reportId);
            if (reportId <= 0) reportId = SiteId;
            Hashtable moduleSetting = ModuleSettings.GetModuleSettings(moduleId);
            //config = new TrainingConfiguaration(moduleSetting);
        }
    }
}
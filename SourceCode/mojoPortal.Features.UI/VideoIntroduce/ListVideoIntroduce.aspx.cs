using mojoPortal.Business;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VideoIntroduceFeatures.UI
{
    public partial class ListVideoIntroduce : mojoBasePage
    {
        protected VideoIntroduceConfiguration config = new VideoIntroduceConfiguration();
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
            LoadParams();
            LoadSettings();
            SecurityHelper.DisableBrowserCache();
            PopulateLabels();
            PopulateControls();
        }
        private void PopulateControls()
        {
            VideoIntroduceRecenlist.ModuleId = moduleId;
            VideoIntroduceRecenlist.PageId = pageId;
            VideoIntroduceRecenlist.SiteRoot = SiteRoot;
        }
        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, "Dữ liệu đa phương tiện");
        }

        private void LoadSettings()
        {
            //userCanEdit = UserCanEditModule(moduleId);
            //pnlContainer.ModuleId = moduleId;
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new VideoIntroduceConfiguration(moduleSettings);
            LoadSideContent(true, true);
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);

        }
    }
}
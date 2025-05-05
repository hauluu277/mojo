using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Features;
using mojoPortal.Business;
using mojoPortal.Web;
using System.Collections;
using mojoPortal.Web.Framework;


namespace VideoIntroduceFeatures.UI
{
    public partial class VideoListModule : SiteModuleControl
    {
        VideoIntroduceConfiguration config = new VideoIntroduceConfiguration();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            EnableViewState = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadParams();
            LoadSettings();
            PopulateLabels();
            PopulateControls();
        }

        private void PopulateLabels()
        {

        }

        private void PopulateControls()
        {
                VideoListControl.ModuleId = ModuleId;
                VideoListControl.PageId = PageId;
                VideoListControl.SiteRoot = SiteRoot;
            VideoListControl.Config = config;
        }
        private void LoadSettings()
        {
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new VideoIntroduceConfiguration(moduleSettings);
        }
        private void LoadParams()
        {

        }
    }
}
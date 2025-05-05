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
using VideoIntroduceFeatures.Business;

namespace VideoIntroduceFeature.UI
{
    public partial class ViewList : mojoBasePage
    {
        protected VideoIntroduceConfiguration config = new VideoIntroduceConfiguration();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private int pageId = -1;
        private int moduleId = -1;
        private bool userCanEdit;
        private int itemId = -1;

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
            VideoListControl.ModuleId = moduleId;
            VideoListControl.PageId = pageId;
            VideoListControl.SiteRoot = SiteRoot;
            VideoListControl.Config = config;

        }
        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, "Danh sách Video");
            if (itemId > 0)
            {
                VideoIntroduceFeatures.Business.VideoIntroduce video = new VideoIntroduceFeatures.Business.VideoIntroduce(itemId);
                if (video != null)
                {
                    Title = SiteUtils.FormatPageTitle(siteSettings, video.Title);
                }
            }
        }

        private void LoadSettings()
        {
            //userCanEdit = UserCanEditModule(moduleId);
            //pnlContainer.ModuleId = moduleId;
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new VideoIntroduceConfiguration(moduleSettings);
            LoadSideContent(false, false);
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            itemId = WebUtils.ParseInt32FromQueryString("item", itemId);
        }
    }
}
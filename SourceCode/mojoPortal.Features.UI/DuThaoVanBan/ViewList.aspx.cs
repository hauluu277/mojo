using System;
using System.Collections;
using mojoPortal.Business;
using mojoPortal.Web.Framework;
using mojoPortal.Web;
using Resources;
using ArticleFeature.Business;
using mojoPortal.Features;

namespace DuThaoVanBanFeature.UI
{
    public partial class ViewList : mojoBasePage
    {
        protected int pageId = -1;
        protected int moduleId = -1;
        private bool userCanEdit;
        private int countOfDrafts = 0;
        private int pageNumber = 1;
        private DuThaoVanBanConfiguration config = new DuThaoVanBanConfiguration();

        #region OnInit

        protected override void OnPreInit(EventArgs e)
        {
            AllowSkinOverride = true;
            base.OnPreInit(e);
        }

        override protected void OnInit(EventArgs e)
        {
            LoadParams();
            LoadPanels();
            Load += Page_Load;
            base.OnInit(e);
        }

        #endregion

        private void LoadPanels()
        {
            LoadSideContent(config.ShowLeftPanelSetting, config.ShowRightPanelSetting/*, config.ShowTopPanelSetting, config.ShowBottomPanelSetting, config.ShowNumberModuleSetting*/);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            //if (!UserCanViewPage(moduleId))
            //{
            //    SiteUtils.RedirectToAccessDeniedPage();
            //    return;
            //}

            LoadSettings();
            PopulateControls();

        }

        private void PopulateControls()
        {
           
            RecentList.ModuleId = moduleId;
            RecentList.PageId = pageId;
            RecentList.IsEditable = userCanEdit;
            RecentList.SiteRoot = SiteRoot;
            RecentList.ImageSiteRoot = ImageSiteRoot;
            RecentList.Config = config;
            //make this page look as close as possible to the way a cms page with the blog module on it looks

        }


        private void LoadSettings()
        {
            userCanEdit = UserCanEditModule(moduleId);
        }

        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            Hashtable settings = ModuleSettings.GetModuleSettings(moduleId);
            config = new DuThaoVanBanConfiguration(settings);
        }


    }
}
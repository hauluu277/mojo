using System;
using System.Collections;
using mojoPortal.Business;
using mojoPortal.Web.Framework;
using mojoPortal.Web;
using Resources;
using ArticleFeature.Business;
using mojoPortal.Features;
using mojoPortal.Business.WebHelpers;

namespace ArticleFeature.UI
{
    public partial class ViewListArticle : mojoBasePage
    {
        protected int pageId = -1;
        protected int moduleId = -1;
        private bool userCanEdit;
        private int countOfDrafts = 0;
        private int pageNumber = 1;
        private int catId = 0;
        private ArticleSharedConfiguration config = new ArticleSharedConfiguration();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        #region OnInit

        protected override void OnPreInit(EventArgs e)
        {
            AllowSkinOverride = true;
            base.OnPreInit(e);
        }

        override protected void OnInit(EventArgs e)
        {
            LoadParams();
            Load += Page_Load;
            base.OnInit(e);
        }

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {

            LoadSettings();
            PopulateControls();

        }

        private void PopulateControls()
        {
            if (catId > 0)
            {
                var getCategory = new CoreCategory(catId);
                Title = SiteUtils.FormatPageTitle(siteSettings, getCategory.Name);
            }


            ViewListControl.ModuleId = moduleId;
            ViewListControl.PageId = pageId;
            ViewListControl.IsEditable = userCanEdit;
            ViewListControl.SiteRoot = SiteRoot;
            ViewListControl.ImageSiteRoot = ImageSiteRoot;
            ViewListControl.Config = config;
            ViewListControl.SiteId = SiteId;
            //make this page look as close as possible to the way a cms page with the blog module on it looks

        }


        private void LoadSettings()
        {
            userCanEdit = UserCanEditModule(moduleId);
            //if (userCanEdit) { countOfDrafts = Article.CountOfDrafts(moduleId); }

        }

        private void LoadParams()
        {
            catId = WebUtils.ParseInt32FromQueryString("catid", catId);
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            Hashtable settings = ModuleSettings.GetModuleSettings(moduleId);
            config = new ArticleSharedConfiguration(settings);
        }


    }
}
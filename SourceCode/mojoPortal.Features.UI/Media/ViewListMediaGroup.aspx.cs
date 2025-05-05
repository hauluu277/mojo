using System;
using System.Collections;
using mojoPortal.Business;
using mojoPortal.Web.Framework;
using mojoPortal.Web;
using Resources;
using mojoPortal.Features;

namespace MediaFeature.UI
{
    public partial class ViewListMediaGroup : mojoBasePage
    {
        protected int pageId = -1;
        protected int moduleId = -1;
        private bool userCanEdit;
        private int countOfDrafts;
        private int pageNumber = 1;
        private MediaConfiguration config = new MediaConfiguration();

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
            //if (!UserCanViewPage(moduleId))
            //{
            //    SiteUtils.RedirectToAccessDeniedPage();
            //    return;
            //}
            LoadSettings();
            PopulateLabel();
            PopulateControls();

        }
        private void PopulateLabel()
        {
            moduleTitle.Visible = false;
            //pnlBreadcrumb.Visible = config.ShowLink;
            //if (config.ShowLink)
            //{
            //    lnkPageCrumb.Text = siteSettings.SiteName;
            //    lnkPageCrumb.NavigateUrl = SiteUtils.GetCurrentPageUrl();
            //    lnkPageParentChild.Text = config.NameTitle;
            //}
        }
        private void PopulateControls()
        {
            MediaGroupList.ModuleId = moduleId;
            MediaGroupList.PageId = pageId;
            MediaGroupList.SiteRoot = SiteRoot;
            MediaGroupList.ImageSiteRoot = ImageSiteRoot;
            MediaGroupList.PageSize = config.PageSize;
            MediaGroupList.Is_PhanTrang = true;
            Title = SiteUtils.FormatPageTitle(siteSettings, MediaResources.CategoryMultiMediaTitle);
        }


        private void LoadSettings()
        {
            //userCanEdit = UserCanEditModule(moduleId);
            ////if (userCanEdit) { countOfDrafts = Article.CountOfDrafts(moduleId); }
            //pnlWrapper.CssClass += config.ViewListCssClass != string.Empty
            //                           ? " " + config.ViewListCssClass
            //                           : " " + config.InstanceCssClass;
            LoadSideContent(true, false);
        }

        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            Hashtable settings = ModuleSettings.GetModuleSettings(moduleId);
            config = new MediaConfiguration(settings);
            AnalyticsSection = ConfigHelper.GetStringProperty("AnalyticsBlogSection", "blog");
        }

    }
}
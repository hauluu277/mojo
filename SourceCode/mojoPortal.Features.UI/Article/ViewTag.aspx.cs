using System;
using System.Collections;
using mojoPortal.Business;
using mojoPortal.Web.Framework;
using mojoPortal.Web;
using Resources;
using ArticleFeature.Business;
using mojoPortal.Features;
using DictionaryFeature.Business;

namespace ArticleFeature.UI
{
    public partial class ViewTag : mojoBasePage
    {
        protected int pageId = -1;
        protected int moduleId = -1;
        private bool userCanEdit;
        private int countOfDrafts = 0;
        private int pageNumber = 1;
        private int tag = -1;
        private Dictionary dictionary = new Dictionary();
        private ArticleConfiguration config = new ArticleConfiguration();

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

            LoadSettings();
            PopulateControls();

        }

        private void PopulateControls()
        {
            if (tag > 0)
            {
                dictionary = new Dictionary(tag);
            }
            Title = dictionary.Name;
            lblTagName.Text = dictionary.Name;
            //moduleTitle.ModuleInstance = GetModule(moduleId);
            //if ((userCanEdit) && (countOfDrafts > 0))
            //{
            //    //BlogEditCategoriesLabel
            //    moduleTitle.LiteralExtraMarkup =
            //        "&nbsp;<a href='"
            //        + SiteRoot
            //        + "/Article/EditPost.aspx?pageid=" + pageId.ToInvariantString()
            //        + "&amp;mid=" + moduleId.ToInvariantString()
            //        + "' class='ModuleEditLink' title='" + ArticleResources.BlogAddPostLabel + "'><img src='/Data/Icon16x16/Modify.png' alt='" + ArticleResources.BlogAddPostLabel + " |'</img></a>"
            //        //+ "&nbsp;<a href='"
            //        //+ SiteRoot
            //        //+ "/Article/EditCategory.aspx?pageid=" + pageId.ToInvariantString()
            //        //+ "&amp;mid=" + moduleId.ToInvariantString()
            //        //+ "' class='editcategorieslink' title='" + ArticleResources.BlogEditCategoriesLabel + "'><img src='/Data/Icon16x16/Folder.png' alt='" + ArticleResources.BlogEditCategoriesLabel + " |'</img></a>"
            //        + "&nbsp;<a href='"
            //        + SiteRoot
            //        + "/Article/Drafts.aspx?pageid=" + pageId.ToInvariantString()
            //        + "&amp;mid=" + moduleId.ToInvariantString()
            //        + "' class='draftlink' title='" + ArticleResources.BlogDraftsLink + "'><img src='/Data/Icon16x16/Bubble.png' alt='" + ArticleResources.BlogDraftsLink + " |'</img></a>"
            //        + "&nbsp;<a href='"
            //        + SiteRoot
            //        + "/Article/ManagePost.aspx?pageid=" + pageId.ToInvariantString()
            //        + "&amp;mid=" + moduleId.ToInvariantString()
            //        + "' class='managepostlink' title='" + ArticleResources.AdminManagePostLabel + "'><img src='/Data/Icon16x16/Text.png' alt='" + ArticleResources.AdminManagePostLabel + "'</img></a>";
            //}
            //else if (userCanEdit)
            //{
            //    moduleTitle.LiteralExtraMarkup =
            //        "&nbsp;<a href='"
            //        + SiteRoot
            //        + "/Article/EditPost.aspx?pageid=" + pageId.ToInvariantString()
            //        + "&amp;mid=" + moduleId.ToInvariantString()
            //        + "' class='ModuleEditLink' title='" + ArticleResources.BlogAddPostLabel + "'><img src='/Data/Icon16x16/Modify.png' alt='" + ArticleResources.BlogAddPostLabel + " |'</img></a>"
            //        //+ "&nbsp;<a href='"
            //        //+ SiteRoot
            //        //+ "/Article/EditCategory.aspx?pageid=" + pageId.ToInvariantString()
            //        //+ "&amp;mid=" + moduleId.ToInvariantString()
            //        //+ "' class='editcategorieslink' title='" + ArticleResources.BlogEditCategoriesLabel + "'><img src='/Data/Icon16x16/Folder.png' alt='" + ArticleResources.BlogEditCategoriesLabel + " |'</img></a>"
            //        + "&nbsp;<a href='"
            //        + SiteRoot
            //        + "/Article/ManagePost.aspx?pageid=" + pageId.ToInvariantString()
            //        + "&amp;mid=" + moduleId.ToInvariantString()
            //        + "' class='managepostlink' title='" + ArticleResources.AdminManagePostLabel + "'><img src='/Data/Icon16x16/Text.png' alt='" + ArticleResources.AdminManagePostLabel + "'</img></a>";
            //}
            articleListByTag.Tag = tag;
            articleListByTag.ModuleId = moduleId;
            articleListByTag.PageId = pageId;
            articleListByTag.IsEditable = userCanEdit;
            articleListByTag.SiteRoot = SiteRoot;
            articleListByTag.ImageSiteRoot = ImageSiteRoot;
            articleListByTag.Config = config;
            articleListByTag.SiteId = SiteId;
            //make this page look as close as possible to the way a cms page with the blog module on it looks

        }


        private void LoadSettings()
        {
            userCanEdit = UserCanEditModule(moduleId);
            //if (userCanEdit) { countOfDrafts = Article.CountOfDrafts(moduleId); }
            pnlWrapper.CssClass += config.ViewListCssClass != string.Empty
                                       ? " " + config.ViewListCssClass
                                       : " " + config.InstanceCssClass;
        }

        private void LoadParams()
        {
            tag = WebUtils.ParseInt32FromQueryString("tag", tag);
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            Hashtable settings = ModuleSettings.GetModuleSettings(moduleId);
            config = new ArticleConfiguration(settings);
            AnalyticsSection = ConfigHelper.GetStringProperty("AnalyticsBlogSection", "blog");
        }


    }
}
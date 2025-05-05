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
    public partial class ViewList : mojoBasePage
    {
        protected int pageId = -1;
        protected int moduleId = -1;
        private bool userCanEdit;
        private int countOfDrafts = 0;
        private int pageNumber = 1;
        private ArticleConfiguration config = new ArticleConfiguration();
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

            if (!UserCanViewPage(moduleId))
            {
                SiteUtils.RedirectToAccessDeniedPage();
                return;
            }

            LoadSettings();
            PopulateControls();

        }

        private void PopulateControls()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, ArticleResources.PostList);

            moduleTitle.ModuleInstance = GetModule(moduleId);
            moduleTitle.Visible = false;
            if (siteUser != null)
            {
                if (siteUser.IsInRoles("Admins") || WebUser.IsInRoles(config.RolePost) || WebUser.IsInRoles(config.RoleApproved))
                {
                    if (siteUser.IsInRoles("Admins"))
                    {
                        moduleTitle.DisabledModuleSettingsLink = false;
                    }
                    else
                    {
                        moduleTitle.DisabledModuleSettingsLink = true;
                    }
                    moduleTitle.Visible = true;
                }

                moduleTitle.EditUrl = SiteRoot + "/article/postarticle.aspx";
                moduleTitle.EditText = "Thêm mới tin bài";
                if (userCanEdit)
                {
                    moduleTitle.LiteralExtraMarkup =
                        "&nbsp;<a href='"
                        + SiteRoot
                        + "/article/managepost.aspx?pageid=" + pageId.ToInvariantString()
                        + "&amp;mid=" + moduleId.ToInvariantString()
                        + "' class='ModuleEditLink' title='Quản lý danh sách tin bài thuộc chuyên mục'>Quản lý danh sách tin bài thuộc chuyên mục</a>"
                        ;
                }
            }

            
            resentList.ModuleId = moduleId;
            resentList.PageId = pageId;
            resentList.IsEditable = userCanEdit;
            resentList.SiteRoot = SiteRoot;
            resentList.ImageSiteRoot = ImageSiteRoot;
            resentList.Config = config;
            resentList.SiteId = SiteId;
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
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            Hashtable settings = ModuleSettings.GetModuleSettings(moduleId);
            config = new ArticleConfiguration(settings);
            AnalyticsSection = ConfigHelper.GetStringProperty("AnalyticsBlogSection", "blog");
        }


    }
}
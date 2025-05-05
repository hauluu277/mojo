using System;
using System.Collections;
using mojoPortal.Business;
using mojoPortal.Web.Framework;
using mojoPortal.Web;
using Resources;
using ArticleFeature.Business;
using mojoPortal.Features;
using mojoPortal.Business.WebHelpers;

namespace AnnouncementFeatures.UI
{
    public partial class ViewList : mojoBasePage
    {
        protected int pageId = -1;
        protected int moduleId = -1;
        private bool userCanEdit;
        private int countOfDrafts = 0;
        private int pageNumber = 1;
        private AnnouncementConfiguration config = new AnnouncementConfiguration();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();

        #region OnInit

        protected override void OnPreInit(EventArgs e)
        {
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
                if (siteUser.IsInRoles("Admins"))
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

                moduleTitle.EditUrl = SiteRoot + "/Announcement/editpost.aspx";
                moduleTitle.EditText = "Thêm mới thông báo";
                if (userCanEdit)
                {
                    moduleTitle.LiteralExtraMarkup =
                        "&nbsp;<a href='"
                        + SiteRoot
                        + "/article/managepost.aspx?pageid=" + pageId.ToInvariantString()
                        + "&amp;mid=" + moduleId.ToInvariantString()
                        + "' class='ModuleEditLink' title='Quản lý danh sách thông báo thuộc chuyên mục'>Quản lý danh sách tin bài thuộc chuyên mục</a>"
                        ;
                }
            }


            AnnouncementRecenlist.ModuleId = moduleId;
            AnnouncementRecenlist.PageId = pageId;
            AnnouncementRecenlist.IsEditable = userCanEdit;
            AnnouncementRecenlist.SiteRoot = SiteRoot;
            AnnouncementRecenlist.Config = config;
            AnnouncementRecenlist.SiteId = SiteId;
            //make this page look as close as possible to the way a cms page with the blog module on it looks

        }
        private void LoadSettings()
        {
            userCanEdit = UserCanEditModule(moduleId);
            //if (userCanEdit) { countOfDrafts = Article.CountOfDrafts(moduleId); }

        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            Hashtable settings = ModuleSettings.GetModuleSettings(moduleId);
            config = new AnnouncementConfiguration(settings);
            AnalyticsSection = ConfigHelper.GetStringProperty("AnalyticsBlogSection", "blog");
        }
    }
}
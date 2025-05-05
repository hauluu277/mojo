using ArticleFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Features.UI.Article.Components;
using mojoPortal.SearchIndex;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArticleFeature.UI
{
    public partial class ManageArticle : mojoBasePage
    {
        protected ArticleConfiguration config = new ArticleConfiguration();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private int reloadArticleLucenIndex = 0;
        private int pageId = -1;
        private int moduleId = -1;
        private bool userCanEdit;
        private static TimeZoneInfo _timeZone;
        protected static Double _timeOffset;
        override protected void OnInit(EventArgs e)
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

            if (ArticleHelper.HasRoleArticle() == false)
            {
                SiteUtils.RedirectToAccessDeniedPage(this);
            }
          

            PopulateLabels();

            PopulateControls();
        }
        private void PopulateControls()
        {
            ManageArticleControl.ModuleId = moduleId;
            ManageArticleControl.PageId = pageId;
            ManageArticleControl.Config = config;
            ManageArticleControl.SiteRoot = SiteRoot;
            ManageArticleControl.ImageSiteRoot = ImageSiteRoot;
        }
        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, ArticleResources.PostList);
            TitleControl.Visible = false;
            TitleControl.ModuleInstance = GetModule(moduleId);
            //if (siteUser.IsInRoles("Admins"))
            //{
            //    TitleControl.Visible = true;
            //}
            heading.Text = "Quản lý danh sách tin bài";

        }

        private void LoadSettings()
        {
            userCanEdit = UserCanEditModule(moduleId);
            pnlContainer.ModuleId = moduleId;
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            reloadArticleLucenIndex = WebUtils.ParseInt32FromQueryString("reloadArticleLucenIndex", reloadArticleLucenIndex);
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new ArticleConfiguration(moduleSettings);
            _timeZone = SiteUtils.GetUserTimeZone();
            _timeOffset = SiteUtils.GetUserTimeOffset();
            pnlAdminCrumbs.Visible = false;
            if (pageId <= 0)
            {
                pnlAdminCrumbs.Visible = true;
                lnkCurrentPage.NavigateUrl = SiteRoot + "/Article/ManageAllPost.aspx";
                lnkCurrentPage.Text = "Danh sách tin bài";


                lnkAdminMenu.Text = "Quản trị hệ thống";
                lnkAdminMenu.NavigateUrl = SiteRoot + "/Admin/AdminMenu.aspx";


                lnkAdminArtile.Text = "Quản trị tin bài";
                lnkAdminArtile.NavigateUrl = SiteRoot + "/Admin/Article/ArticleMenu.aspx";
            }
        }

        [WebMethod(EnableSession = true)]
        public static bool PheDuyetNoiDung(string itemid, string isapprove, string comment, string role)
        {
            int _role = 0;
            if (!string.IsNullOrEmpty(role))
            {
                _role = int.Parse(role);
            }
            if (_role == RoleConstant.isApprove)
            {
                if (!string.IsNullOrEmpty(itemid) && !string.IsNullOrEmpty(isapprove))
                {
                    Article article = new Article(int.Parse(itemid));
                    if (article != null && article.ItemID > 0)
                    {
                        if (int.Parse(isapprove) == 1)
                        {
                            article.IsApproved = true;
                            article.ApprovedDate = DateTime.Now;
                        }
                        else
                        {
                            article.IsApproved = false;
                            article.CommentByBoss = comment;
                            article.ApprovedDate = DateTime.Now;
                        }
                        article.ApprovedGuid = SiteUtils.GetCurrentSiteUser().UserGuid;
                        article.Save();
                        //}
                    }
                    return false;
                }
            }
            return false;

        }

        public void ReloadArticleLucenIndex()
        {
            var listArticle = Article.GetAll().Where(x => x.SiteId == SiteId);
            foreach (var item in listArticle)
            {
                item.ContentChanged += article_ContentChanged;
                item.Save();
                SiteUtils.QueueIndexing();
            }
        }
        void article_ContentChanged(object sender, ContentChangedEventArgs e)
        {
            IndexBuilderProvider indexBuilder = IndexBuilderManager.Providers["ArticleIndexBuilderProvider"];
            if (indexBuilder != null)
            {
                indexBuilder.ContentChangedHandler(sender, e);
            }
        }

        [WebMethod(EnableSession = true)]
        public static bool XuatBanNoiDung(string itemid, string ispublish, string role)
        {
            int _role = 0;
            if (!string.IsNullOrEmpty(role))
            {
                _role = int.Parse(role);
            }
            if (_role == RoleConstant.isApprove || _role == RoleConstant.isPost)
            {
                if (!string.IsNullOrEmpty(itemid) && !string.IsNullOrEmpty(ispublish))
                {
                    Article article = new Article(int.Parse(itemid));
                    if (article != null && article.ItemID > 0)
                    {
                        //đồng ý xuất bản
                        if (int.Parse(ispublish) == 1)
                        {
                            article.IsPublished = true;
                        }
                        else
                        {
                            article.IsPublished = false;
                        }
                        article.PublishedDate = DateTime.Now;
                        article.PublishedGuid = SiteUtils.GetCurrentSiteUser().UserGuid;
                        article.Save();
                    }
                    return false;
                }
            }
            return false;
        }
    }
}
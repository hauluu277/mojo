using ArticleFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Features.UI.Article.Components;
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
    public partial class ManagePost : mojoBasePage
    {
        protected ArticleConfiguration config = new ArticleConfiguration();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private int pageId = -1;
        private int moduleId = -1;
        private bool userCanEdit;
        private static TimeZoneInfo _timeZone;
        protected static Double _timeOffset;
        public bool isUserApprove = false;
        public bool isUserPost = false;
        public bool isAdmin = false;
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
            SecurityHelper.DisableBrowserCache();


            //if (!userCanEdit)
            //{
            //    SiteUtils.RedirectToEditAccessDeniedPage();
            //}

            PopulateLabels();

            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }
        private void PopulateControls()
        {
            articleList.ModuleId = moduleId;
            articleList.PageId = pageId;
            articleList.Config = config;
            articleList.SiteRoot = SiteRoot;
            articleList.ImageSiteRoot = ImageSiteRoot;
        }
        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, ArticleResources.PostList);
            TitleControl.Visible = false;
            TitleControl.ModuleInstance = GetModule(moduleId);
            if (siteUser.IsInRoles("Admins"))
            {
                TitleControl.Visible = true;
            }

        }

        private void LoadSettings()
        {
            if (!Request.IsAuthenticated)
            {
                SiteUtils.RedirectToLoginPage(this);
                return;
            }
            if ((WebUser.IsAdminOrContentAdmin) || (SiteUtils.UserIsSiteEditor())) { isAdmin = true; }
            userCanEdit = UserCanEditModule(moduleId);
            pnlContainer.ModuleId = moduleId;
            if (WebUser.IsInRoles(config.RoleApproved))
            {
                isUserApprove = true;
            }
            else if (WebUser.IsInRoles(config.RolePost))
            {
                isUserPost = true;
            }
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new ArticleConfiguration(moduleSettings);
            _timeZone = SiteUtils.GetUserTimeZone();
            _timeOffset = SiteUtils.GetUserTimeOffset();
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
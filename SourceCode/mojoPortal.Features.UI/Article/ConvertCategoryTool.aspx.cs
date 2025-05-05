using ArticleFeature.Business;
using mojoPortal.Business;
using mojoPortal.Features;
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
    public partial class ConvertCategoryTool : mojoBasePage
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


            if (siteUser.IsInRoles("Admins"))
            {
                //continue
            }
            else
            {
                SiteUtils.RedirectToEditAccessDeniedPage();
            }

            PopulateLabels();
            if (!IsPostBack)
            {
                PopulateControls();
            }

        }
        private void PopulateControls()
        {
            var listArticle = Article.GetAll().ToList();
            foreach (var item in listArticle)
            {
                if (item.CategoryID > 0)
                {
                    var category = new CoreCategory(item.CategoryID);
                    if (category.ItemID > 0)
                    {

                        var siteSetting = new SiteSettings(item.SiteId);

                        if (category.ParentID == siteSettings.ArticleCategory)
                        {
                            ArticleCategory articleCategory = new ArticleCategory();
                            articleCategory.ArticleID = item.ItemID;
                            articleCategory.CategoryID = item.CategoryID;
                            articleCategory.SiteID = item.SiteId;
                            articleCategory.Save();
                        }
                        else
                        {
                            ArticleCategory articleCategory = new ArticleCategory();
                            articleCategory.ArticleID = item.ItemID;
                            articleCategory.CategoryID = item.CategoryID;
                            articleCategory.SiteID = item.SiteId;
                            articleCategory.Save();
                        }
                    }
                }
            }
            lblNotify.Text = "Convert data category success";
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
    }
}
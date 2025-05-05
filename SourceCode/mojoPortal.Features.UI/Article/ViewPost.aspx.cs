using ArticleFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArticleFeature.UI
{
    public partial class ViewPost : mojoBasePage
    {
        private int moduleId = -1;
        ArticleConfiguration config = new ArticleConfiguration();
        private static readonly SiteSettings siteSettings = CacheHelper.GetCurrentSiteSettings();
        private static int mid = -1;
        private static int pageid = -1;
        private static int itemid = -1;
        private static int cateid = -1;
        #region OnInit

        protected override void OnPreInit(EventArgs e)
        {
            AllowSkinOverride = true;
            base.OnPreInit(e);
        }

        override protected void OnInit(EventArgs e)
        {
            Load += Page_Load;
            LoadPanels();
            base.OnInit(e);
        }

        private void LoadPanels()
        {
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            Hashtable settings = ModuleSettings.GetModuleSettings(moduleId);
            config = new ArticleConfiguration(settings);
            LoadSideContent(config.ShowLeftPanelSetting, config.ShowRightPanelSetting/*, config.ShowTopPanelSetting, config.ShowBottomPanelSetting, config.ShowNumberModuleSetting*/);
        }

        #endregion

        private void Page_Load(object sender, EventArgs e)
        {
            LoadParam();
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            var article = new Article(itemid);
            SiteUtils.FormatPageTitle(siteSettings, article.Title);
        }
        private void LoadParam()
        {
            mid = WebUtils.ParseInt32FromQueryString("mid", mid);
            pageid = WebUtils.ParseInt32FromQueryString("pageid", pageid);
            itemid = WebUtils.ParseInt32FromQueryString("itemid", itemid);
        }

        [WebMethod]
        [HttpPost]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public static ArticlePagerBO SearchOrtherArticle(int pageIndex = 1, int category = 0, string startdate = "", string keyword = "")
        {
            int totalPage = 0;
            int totalCount = 0;
            int pageSize = 10;
            DateTime? startDate = null;
            if (!string.IsNullOrEmpty(startdate))
            {
                try
                {
                    startDate = startdate.ToDateTime();
                }
                catch
                {

                    startDate = null;
                }
            }
            ArticlePagerBO result = new ArticlePagerBO();
            result.ListArticle = new List<ArticleOrtherBO>();
            var reader = Article.GetAllOrtherPage(siteSettings.SiteId, itemid, category, startDate, keyword, pageIndex, pageSize, out totalPage, out totalCount);
            string folder = System.Configuration.ConfigurationManager.AppSettings["ArticleImagesFolder"];
            foreach (var item in reader)
            {
                ArticleOrtherBO orther = new ArticleOrtherBO();
                orther.ItemID = item.ItemID;
                orther.ItemUrl = item.ItemUrl;
                orther.ModuleID = item.ModuleID;
                orther.PageID = pageid;
                orther.SiteRoot = siteSettings.SiteRoot;
                orther.StartDate = string.Format("{0:dd/MM/yyyy}", item.StartDate);
                orther.Title = item.Title;
                orther.ArticleImagesFolder = folder;
                orther.ImageUrl = item.ImageUrl;
                orther.Summary = item.Summary;
                result.ListArticle.Add(orther);
            }
            result.PageSize = pageSize;
            result.PageIndex = pageIndex;
            result.CountItem = totalCount;
            return result;
        }
        protected override void OnError(EventArgs e)
        {
            Exception lastError = Server.GetLastError();
            if ((lastError == null) || (!(lastError is NullReferenceException)) || !Page.IsPostBack) return;
            if (!lastError.StackTrace.Contains("Recaptcha")) return;
            Server.ClearError();
            WebUtils.SetupRedirect(this, Request.RawUrl);
        }
    }
}

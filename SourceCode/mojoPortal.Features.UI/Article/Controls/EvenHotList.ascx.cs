using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using mojoPortal.Web.UI;
using log4net;
using mojoPortal.Business;
using mojoPortal.Features;
using Resources;
using mojoPortal.Business.WebHelpers;
using System.Text.RegularExpressions;
using ArticleFeature.Business;

namespace ArticleFeature.UI
{
    public partial class EvenHotList : UserControl
    {
        protected ArticleConfiguration config = new ArticleConfiguration();

        private int pageId = -1;
        private int moduleId = -1;
        private int number = -1;
        private string siteRoot = string.Empty;
        private SiteSettings siteSettings;
        protected bool visibleImg = false;
        public int PageId
        {
            get { return pageId; }
            set { pageId = value; }
        }

        public int ModuleId
        {
            get { return moduleId; }
            set { moduleId = value; }
        }
        public string SiteRoot
        {
            get { return siteRoot; }
            set { siteRoot = value; }
        }
        #region OnInit

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(Page_Load);

        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSettings();
            PopulateLabels();
            if (!IsPostBack)
            {
                PopulateControls();
            }
        }
        private void PopulateControls()
        {
            PopulateTopSilde();
        }
        private void PopulateLabels()
        {
            lblTit.Text = ArticleResources.EventHighlightsTitle;
        }
        private void PopulateTopSilde()
        {
            List<Article> slide = Article.GetArticleEventHot(siteSettings.SiteId, 1);
            rptArticle.DataSource = slide;
            rptArticle.DataBind();
        }
        protected string formatContent(string Content)
        {
            if (!string.IsNullOrEmpty(Content))
            {
                return Regex.Replace(Content, "<(.|\n)*?>", string.Empty);
            }
            else
            {
                return string.Empty;
            }
        }
        private void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new ArticleConfiguration(getModuleSettings);
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            visibleImg = config.ShowImageRight;
        }
    }
}
using ArticleFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.SearchIndex;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;

namespace LogSystemFeature.UI
{
    public partial class PostList : UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;
        private TimeZoneInfo timeZone;
        protected Double timeOffset;
        private string dateTimeFormat;
        private mojoBasePage basePage;
        private Module module;
        protected ArticleConfiguration config = new ArticleConfiguration();

        private int pageId = -1;
        private int moduleId = -1;

        private string loginName = string.Empty;
        private string fullName = string.Empty;
        private string dateFrom = string.Empty;
        private string dateTo = string.Empty;
        private string startLoginFrom = string.Empty;
        private string startLoginTo = string.Empty;
        private string endLoginFrom = string.Empty;
        private string endLoginTo = string.Empty;

        private int itemId = -1;
        private SiteSettings siteSettings;

        private int totalRow = 0;

        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkText = SwirlingQuestionResource.ButtonDelete;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkText = SwirlingQuestionResource.ButtonEdit;
        protected string EditLinkImageUrl = string.Empty;
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        readonly SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
        public string SiteRoot
        {
            get; set;
        }
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


        public ArticleConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }
        public bool IsEditable { get; set; }
        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            btnSearch.Click += btnSearch_Click;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            LoadParams();
            LoadSettings();
            PopulateLabels();
            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }
        protected string FormatDate(DateTime? date)
        {
            var result = "";
            if (date != null)
            {
                return date.Value.ToString(config.DateTimeFormat);
            }
            return result;
        }
        private void PopulateLabels()
        {
            btnSearch.Text = ArticleResources.ArticleSearchButton;
            UIHelper.DisableButtonAfterClick(
                btnSearch,
                ArticleResources.ButtonDisabledPleaseWait,
                Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty)
                );
        }
        private void PopulateControls()
        {
            BindLog();
            txtLoginName.Text = loginName;
            txtFullName.Text = fullName;
            txtDateFrom.Value = dateFrom;
            txtDateTo.Value = dateTo;
            txtEndLoginFrom.Value = endLoginFrom;
            txtEndLoginTo.Value = endLoginTo;
            txtStartLoginFrom.Value = startLoginFrom;
            txtStartLoginTo.Value = startLoginTo;
        }
        private void BindLog()
        {
            List<core_LogSystemMojo> reader = new List<core_LogSystemMojo>();

            reader = core_LogSystemMojo.GetPage(loginName, fullName, dateFrom.ToDateTime(), dateTo.ToDateTime()
                , startLoginFrom.ToDateTime(), startLoginTo.ToDateTime(),
                endLoginFrom.ToDateTime(), endLoginTo.ToDateTime(), pageNumber, 20, out totalPages);

            rptArticles.DataSource = reader;
            rptArticles.DataBind();
            lblTotalArticle.Text = string.Format("Tổng số {0} tin bài", totalRow);
            string pageUrl = SiteRoot + "/logsystem/manage.aspx"
                  + "?loginname=" + loginName
                   + "&fullname=" + fullName
                   + "&datefrom=" + dateFrom
                   + "&dateto=" + dateTo
                   + "&startloginfrom=" + startLoginFrom
                   + "&startloginto=" + startLoginTo
                    + "&endloginfrom=" + endLoginFrom
                     + "&endloginto=" + endLoginTo
                   + "&pagenumber={0}";

            pgrLogSystem.PageURLFormat = pageUrl;
            pgrLogSystem.ShowFirstLast = true;
            pgrLogSystem.PageSize = config.PageSize;
            pgrLogSystem.PageCount = totalPages;
            pgrLogSystem.CurrentIndex = pageNumber;
            pnlLogSystem.Visible = (totalPages > 1);
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            timeZone = SiteUtils.GetUserTimeZone();
            dateTimeFormat = config.DateTimeFormat.ToString();
            timeOffset = SiteUtils.GetUserTimeOffset();
        }
        protected virtual void LoadSettings()
        {
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            loginName = WebUtils.ParseStringFromQueryString("loginname", loginName);
            fullName = WebUtils.ParseStringFromQueryString("fullname", fullName);
            dateFrom = WebUtils.ParseStringFromQueryString("datefrom", dateFrom);
            dateTo = WebUtils.ParseStringFromQueryString("dateto", dateTo);
            startLoginFrom = WebUtils.ParseStringFromQueryString("startloginfrom", startLoginFrom);
            endLoginFrom = WebUtils.ParseStringFromQueryString("endloginTo", endLoginTo);
            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
        }
        protected virtual void btnSearch_Click(object sender, EventArgs e)
        {
            loginName = txtLoginName.Text;
            fullName = txtFullName.Text;
            dateFrom = txtDateFrom.Value;
            dateTo = txtDateTo.Value;
            startLoginFrom = txtStartLoginFrom.Value;
            startLoginTo = txtStartLoginTo.Value;
            endLoginFrom = txtEndLoginFrom.Value;
            endLoginTo = txtEndLoginTo.Value;


            string pageUrl = SiteRoot + "/logsystem/manage.aspx"
               + "?loginname=" + loginName
               + "&fullname=" + fullName
               + "&datefrom=" + dateFrom
               + "&dateto=" + dateTo
               + "&startloginfrom=" + startLoginFrom
               + "&startloginto=" + startLoginTo
                + "&endloginfrom=" + endLoginFrom
                 + "&endloginto=" + endLoginTo
               + "&pagenumber=1";
            WebUtils.SetupRedirect(this, pageUrl);
        }
        protected string FormatArticleDate(DateTime startDate)
        {
            if (config.DateTimeFormat == string.Empty) return string.Empty;
            string result = "";
            result = timeZone != null ? TimeZoneInfo.ConvertTimeFromUtc(startDate, timeZone).ToString(config.DateTimeFormat) : startDate.AddHours(timeOffset).ToString(config.DateTimeFormat);
            return result;
        }

    }
}
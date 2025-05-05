using ArticleFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.UI;

namespace ArticleFeature.UI
{
    public partial class ArticleSharedControl : UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;
        private int siteId = -1;
        private mojoBasePage basePage;
        private Module module;
        protected ArticleSharedConfiguration config = new ArticleSharedConfiguration();
        private TimeZoneInfo timeZone;
        protected Double TimeOffset;

        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        private SiteSettings siteSettings;
        private int categoryID = -1;
        private int status = -1;
        protected int type = -1;
        private string keyword = string.Empty;
        protected string FeedBackLabel = string.Empty;
        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkText = SwirlingQuestionResource.ButtonDelete;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkText = SwirlingQuestionResource.ButtonEdit;
        protected string EditLinkTooltip = ArticleResources.BlogEditEntryLink;
        protected string EditLinkImageUrl = string.Empty;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        protected bool ShowCommentCounts = true;
        protected string IntenseDebateAccountId = string.Empty;
        protected int langId = 1;
        protected int langRefer = 1;
        private bool? isApprove = null;
        private bool? isPublish = null;
        protected bool isAdmin = false;
        List<Article> reader = null;
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
        public int SiteId
        {
            get { return siteId; }
            set { siteId = value; }
        }
        public int CategoryId
        {
            get { return categoryID; }
            set { categoryID = value; }
        }
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        public string Keyword
        {
            get { return keyword; }
            set { keyword = value; }
        }
        public string SiteRoot
        {
            get { return siteRoot; }
            set { siteRoot = value; }
        }

        public ArticleSharedConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }

        public string ImageSiteRoot
        {
            get { return imageSiteRoot; }
            set { imageSiteRoot = value; }
        }

        public bool IsEditable { get; set; }
        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            //EnableViewState = false;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSettings();
            PopulateLabels();
            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }

        private void PopulateControls()
        {
            string categories = string.Empty;
            if (!string.IsNullOrEmpty(config.SiteCategorySetting))
            {
                categories = string.Join(" ", config.SiteCategorySetting.Split('-')).Trim();
            }
            reader = Article.GetPageByCategory(pageNumber, config.PageSize, categories, out totalPages);
            rptArticle.DataSource = reader;
            rptArticle.DataBind();

            //#region setup redirect url
            //foreach (var item in reader)
            //{
            //    var newUrl = item.ItemUrl.Replace("~/", string.Empty);
            //    string realUrl = "~/Article/Viewpost.aspx?pageid=" + pageId + "&mid=" + moduleId + "&itemid=" + item.ItemID;
            //    FriendlyUrl friendlyUrl = new FriendlyUrl();

            //    friendlyUrl.SiteId = siteSettings.SiteId;
            //    friendlyUrl.SiteGuid = siteSettings.SiteGuid;
            //    friendlyUrl.PageGuid = item.ArticleGuid;
            //    friendlyUrl.Url = newUrl;
            //    friendlyUrl.RealUrl = realUrl;
            //    friendlyUrl.Save();
            //}
            //#endregion


            string pageUrl = SiteRoot + "/ArticleShared/ArticleSharedList.aspx"
                   + "?pageid=" + pageId.ToInvariantString()
                   + "&amp;mid=" + moduleId.ToInvariantString()
                   + "&amp;pagenumber={0}";

            pgr.PageURLFormat = pageUrl;
            pgr.ShowFirstLast = true;
            pgr.PageSize = config.PageSize;
            pgr.PageCount = totalPages;
            pgr.CurrentIndex = pageNumber;
            pnlArticlePager.Visible = (totalPages > 1);
        }

        protected bool ShowImage(string imageUrl)
        {
            if (String.IsNullOrEmpty(imageUrl))
            {
                return false;
            }
            else
            {
                string filePath = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["ArticleImagesFolder"] + imageUrl;
                filePath = filePath.Replace("/", "\\");
                if (File.Exists(filePath))
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
        }

        protected virtual void LoadSettings()
        {
            if ((WebUser.IsAdminOrContentAdmin) || (SiteUtils.UserIsSiteEditor())) { isAdmin = true; }

            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new ArticleSharedConfiguration(getModuleSettings);


            siteSettings = CacheHelper.GetCurrentSiteSettings();
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            categoryID = WebUtils.ParseInt32FromQueryString("catid", categoryID);
            string _status = WebUtils.ParseStringFromQueryString("status", string.Empty);
            if (!string.IsNullOrEmpty(_status))
            {
                status = int.Parse(_status);
                if (_status == "1")
                {
                    isApprove = true;
                }
                else if (_status == "0")
                {
                    isApprove = false;
                }
            }
            keyword = WebUtils.ParseStringFromQueryString("keyword", keyword);

            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }


            TimeOffset = SiteUtils.GetUserTimeOffset();
            timeZone = SiteUtils.GetUserTimeZone();
        }

        protected virtual void PopulateLabels()
        {
            mojoBasePage mojoBasePage = Page as mojoBasePage;
            if (mojoBasePage != null)
            {
                if (!mojoBasePage.UseTextLinksForFeatureSettings)
                {
                    EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
                }

                if (mojoBasePage.AnalyticsSection.Length == 0)
                {
                    mojoBasePage.AnalyticsSection = ConfigHelper.GetStringProperty("AnalyticsBlogSection", "article");
                }
            }

        }

        protected string FormatArticleDate(DateTime startDate)
        {
            return string.Format("{0:dd/MM/yyyy HH:mm}", startDate);
            //if (config.DateTimeFormat == string.Empty) return string.Empty;
            //string result = "";
            //result = timeZone != null ? TimeZoneInfo.ConvertTimeFromUtc(startDate, timeZone).ToString(config.DateTimeFormat) : startDate.AddHours(TimeOffset).ToString(config.DateTimeFormat);
            //return result;
        }

        protected string FormatPostAuthor(string userGuid)
        {
            //return ArticleUtils.FormatPostAuthor(userGuid, config);
            if (config.ShowAuthorSignature)
            {
                return userGuid;
            }
            else return string.Empty;
        }


        protected string BuildEditUrl(int itemID)
        {
            return SiteRoot + "/Article/PostArticle.aspx?pageid=" + pageId + "&ItemID=" +
                       itemID + "&mid=" + moduleId;

        }

    }
}
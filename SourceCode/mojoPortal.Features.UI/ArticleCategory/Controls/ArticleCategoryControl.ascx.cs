using ArticleFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Linq;

namespace ArticleFeature.UI
{
    public partial class ArticleCategoryControl : UserControl
    {
        #region Properties

        private int pageNumber = 1;
        private int totalPages = 1;
        protected Double TimeOffset;
        private TimeZoneInfo timeZone;
        protected bool ShowGoogleMap = true;
        protected string FeedBackLabel = string.Empty;
        protected string GmapApiKey = string.Empty;
        protected bool EnableContentRating;
        protected string disqusFlag = string.Empty;
        protected string IntenseDebateAccountId = string.Empty;
        protected bool ShowCommentCounts = true;
        protected string EditLinkImageUrl = string.Empty;
        private mojoBasePage basePage;
        private Module module;
        protected ArticleConfiguration config = new ArticleConfiguration();
        private int pageId = -1;
        private int moduleId = -1;
        private int siteId = -1;
        private int pureModuleId = -1;
        protected int langId = -1;
        protected int type = -1;
        private int itemId = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        private SiteSettings siteSettings;
        private string[] listModuleId;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private string moduleHeading = string.Empty;

        public string[] ListModuleId
        {
            get { return listModuleId; }
            set { listModuleId = value; }
        }

        public int PureModuleId
        {
            get { return pureModuleId; }
            set { pureModuleId = value; }
        }

        public int PageId
        {
            get { return pageId; }
            set { pageId = value; }
        }
        public int SiteId
        {
            get { return siteId; }
            set { siteId = value; }
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

        public string ImageSiteRoot
        {
            get { return imageSiteRoot; }
            set { imageSiteRoot = value; }
        }

        public string ModuleHeading
        {
            get { return moduleHeading; }
            set { moduleHeading = value; }
        }

        public ArticleConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }

        public bool IsEditable { get; set; }

        public bool DisplayTab { get; set; }

        #endregion


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            EnableViewState = false;
            //pgr.Command += new CommandEventHandler(pgr_Command);
        }

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            var lang = CultureInfo.CurrentCulture.Name;
            LoadSettings();
            PopulateLabels();
            //SetupRssLink();
            if (!IsPostBack)
            {
                PopulateControls();
            }
        }

        private void PopulateControls()
        {
            var category = config.ArticleCategoryConfig;
            if (!string.IsNullOrEmpty(category))
            {
                var categoryFirst = 0;
                var categoryList = category.Split('-');
                foreach (var item in categoryList)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        categoryFirst = Convert.ToInt32(item);
                        break;
                    }
                }
                var listCategory = CoreCategory.GetChildren(categoryFirst);
                rptMenuCategory.DataSource = listCategory;
                rptMenuCategory.DataBind();

                rptCategoryParent.DataSource = listCategory;
                rptCategoryParent.DataBind();

                //rptMenuCategory.DataSource = listCategoryc2;
                //rptMenuCategory.DataBind();
            }

        }

        protected List<Article> LoadArticle(object categoryId)
        {
            var listArticle = Article.GetArticleTop(Convert.ToInt32(categoryId), 100);
            return listArticle;
        }



        protected string FormatArticleDate(DateTime startDate)
        {
            //if (config.DateTimeFormat == string.Empty) return startDate.ToShortDateString();
            //return startDate.ToString(config.DateTimeFormat);
            if (config.DateTimeFormat == string.Empty) return string.Empty;
            string result = "";
            result = timeZone != null ? TimeZoneInfo.ConvertTimeFromUtc(startDate, timeZone).ToString(config.DateTimeFormat) : startDate.AddHours(TimeOffset).ToString(config.DateTimeFormat);
            return result;
        }


        protected virtual void PopulateLabels()
        {

            mojoBasePage mojoBasePage = Page as mojoBasePage;
            if (mojoBasePage == null) return;

        }




        protected string BuildEditUrl(int itemID)
        {
            return ArticleUtils.BuildEditUrl(itemID, listModuleId, SiteRoot, PageId, ModuleId);
        }

        private string GetRssUrl()
        {
            return ArticleUtils.GetRssUrl(config, SiteRoot, ModuleId);
        }

        protected virtual void LoadSettings()
        {
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            siteId = siteSettings.SiteId;
            TimeOffset = SiteUtils.GetUserTimeOffset();
            timeZone = SiteUtils.GetUserTimeZone();
            GmapApiKey = SiteUtils.GetGmapApiKey();
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            var module_settings = ModuleSettings.GetModuleSettings(moduleId);
            config = new ArticleConfiguration(module_settings);
            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
        }

        protected List<CoreCategory> LoadCategoryChild(int parentId)
        {
            return CoreCategory.GetChildrenByParent(parentId);
        }


        protected string FormartDateTime(object startDate)
        {
            if (startDate != null)
            {
                return string.Format("{0:dddd - dd/MM/yyyy}", startDate);
            }
            return string.Empty;
        }
    }
}
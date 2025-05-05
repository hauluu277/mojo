using EventFeature.Business;
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
using System.Web.UI.WebControls;

namespace EventFeature.UI
{
    public partial class RecentList : UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;
        private int siteId = -1;
        private mojoBasePage basePage;
        private Module module;
        protected EventConfiguration config = new EventConfiguration();

        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        public SiteSettings siteSettings;
        private int categoryID = -1;
        private int isApprove = -1;
        private int isPublish = -1;
        private int status = -1;
        private string keyword = string.Empty;
        protected string FeedBackLabel = string.Empty;
        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkText = SwirlingQuestionResource.ButtonDelete;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkText = SwirlingQuestionResource.ButtonEdit;
        protected string EditLinkTooltip = EventResources.BlogEditEntryLink;
        protected string EditLinkImageUrl = string.Empty;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        protected bool ShowCommentCounts = true;
        protected string IntenseDebateAccountId = string.Empty;
        protected int langId = 1;
        protected int langRefer = 1;
        protected Double TimeOffset;
        private TimeZoneInfo timeZone;
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

        public EventConfiguration Config
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
            BindEvents();
        }

        //public List<Language> BindLanguage()
        //{
        //    List<Language> language = Language.GetAll();
        //    return language;
        //}
        private void BindEvents()
        {
            EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
            DeleteLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;

            List<Event> reader = Event.GetPageForEndUser(siteSettings.SiteId, moduleId, pageNumber, config.PageSize, categoryID, keyword.ConvertToVN(), out totalPages);
            rptRecentEvents.DataSource = reader;
            rptRecentEvents.DataBind();

            string pageUrl = SiteRoot + "/event/viewlist.aspx"
                   + "?pageid=" + pageId.ToInvariantString()
                   + "&amp;mid=" + moduleId.ToInvariantString()
                   + "&amp;pagenumber={0}";

            pgr.PageURLFormat = pageUrl;
            pgr.ShowFirstLast = true;
            pgr.PageSize = config.PageSize;
            pgr.PageCount = totalPages;
            pgr.CurrentIndex = pageNumber;
            pnlEventPager.Visible = (totalPages > 1) && config.ShowPager;
        }
        private void PopulateOtherEvents()
        {
            if (config.ShowOtherEvents)
            {
                List<Event> result = Event.GetOthersPageByModuleId(ModuleId, config.PageSize, pageNumber);
                if (result != null && result.Count > 0)
                {
                    gvOthersEvent.DataSource = result;
                    gvOthersEvent.DataBind();
                    gvOthersEvent.AllowPaging = config.OtherEventsPageSizeSetting < result.Count;
                }
                lblOtherHeader.Visible = result.Count > 0;
            }
            else
            {
                pnlOthersEvent.Visible = false;
            }
        }
        void gvOthersEvent_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOthersEvent.PageIndex = e.NewPageIndex;
            PopulateOtherEvents();
        }
        protected virtual void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new EventConfiguration(getModuleSettings);
            TimeOffset = SiteUtils.GetUserTimeOffset();
            timeZone = SiteUtils.GetUserTimeZone();
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            categoryID = WebUtils.ParseInt32FromQueryString("catid", categoryID);
            keyword = WebUtils.ParseStringFromQueryString("keyword", keyword);

            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
            IntenseDebateAccountId = config.IntenseDebateAccountId.Length > 0 ? config.IntenseDebateAccountId : siteSettings.IntenseDebateAccountId;

            if (config.AllowComments)
            {
                if ((IntenseDebateAccountId.Length > 0) && (config.CommentSystem == "intensedebate"))
                {
                    ShowCommentCounts = false;
                }
            }
        }
        protected bool ShowImage(string imageUrl)
        {
            if (String.IsNullOrEmpty(imageUrl))
            {
                return false;
            }
            else
            {
                string filePath = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["EventImagesFolder"] + imageUrl;
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
        protected virtual void PopulateLabels()
        {
            pnlScrollable.CssClass = "scrollable" + ModuleId;
            FeedBackLabel = EventResources.BlogFeedbackLabel;
            mojoBasePage mojoBasePage = Page as mojoBasePage;
            if (mojoBasePage != null)
            {
                if (!mojoBasePage.UseTextLinksForFeatureSettings)
                {
                    EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
                }

                if (mojoBasePage.AnalyticsSection.Length == 0)
                {
                    mojoBasePage.AnalyticsSection = ConfigHelper.GetStringProperty("AnalyticsBlogSection", "Event");
                }
            }

        }
        protected void btnaddnew_Click(object sender, EventArgs e)
        {
            WebUtils.SetupRedirect(this, SiteRoot + "/event/editpost.aspx?pageid=" + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString());

        }
        protected string FormatImgUrlLanguage(string code)
        {
            string Imgurl = "~/Data/SiteImages/flags/" + code + ".gif";
            return Imgurl;
        }
        protected string FormatEventDate(DateTime startDate)
        {
            if (startDate != null)
            {
                return string.Format("{0:dd/MM/yyyy HH:mm}", startDate);
                //try
                //{
                //    return timeZone != null ? TimeZoneInfo.ConvertTimeFromUtc(startDate, timeZone).ToString(config.DateTimeFormat) : startDate.AddHours(TimeOffset).ToString(config.DateTimeFormat);
                //}
                //catch { }
            }
            return "";
        }

        protected string FormatPostAuthor(string userGuid)
        {
            //return EventUtils.FormatPostAuthor(userGuid, config);
            if (config.ShowAuthorSignature)
            {
                return userGuid;
            }
            else return string.Empty;
        }

        protected string BuildEditUrl(int itemID)
        {
            return SiteRoot + "/event/editpost.aspx?pageid=" + pageId + "&ItemID=" +
                       itemID + "&mid=" + moduleId;
        }

    }
}
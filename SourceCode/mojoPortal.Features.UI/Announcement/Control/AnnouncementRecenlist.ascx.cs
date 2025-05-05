using mojoPortal.Business;
using mojoPortal.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Features;
using Resources;
using VideoIntroduceFeatures.Business;
using System.IO;
using System.Text;
using System.Configuration;
using mojoPortal.Web.Framework;
using mojoPortal.Business.WebHelpers;
using System.Collections;

namespace AnnouncementFeatures.UI
{
    public partial class AnnouncementRecenlist : System.Web.UI.UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int pageSize = 10;
        private int totalPages = 1;
        private int siteId = -1;
        private mojoBasePage basePage;
        private Module module;
        protected AnnouncementConfiguration config = new AnnouncementConfiguration();
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
        
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        protected bool ShowCommentCounts = true;
        protected string IntenseDebateAccountId = string.Empty;
        protected int langId = 1;
        protected int langRefer = 1;
        protected bool isAdmin = false;
        List<md_Announcement> reader = null;
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

        public AnnouncementConfiguration Config
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
            //btn_add_addmission.Click += btnaddnew_Click;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSettings();
            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }

        private void PopulateLabel()
        {


        }

        private void PopulateControls()
        {

            BinAnnouncement();

        }
        private void BinAnnouncement()
        {
            List<md_Announcement> reader = md_Announcement.GetTopHot(6, siteSettings.SiteId);
            rptAdmission.DataSource = reader;
            rptAdmission.DataBind();

            //string pageUrl = SiteRoot + "/Announcement/ViewList.aspx"
            //       + "?pageid=" + pageId.ToInvariantString()
            //       + "&mid=" + moduleId.ToInvariantString()
            //       + "&pagenumber={0}";
            //pgrAdmission.PageURLFormat = pageUrl;
            //pgrAdmission.ShowFirstLast = true;
            //pgrAdmission.PageSize = config.PageSize;
            //pgrAdmission.PageCount = totalPages;
            //pgrAdmission.CurrentIndex = pageNumber;
            //pnlAdmissionPager.Visible = (totalPages > 1);
        }
        private void LoadParams()
        {
            itemId = WebUtils.ParseInt32FromQueryString("item", itemId);
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
        }
        protected void btnaddnew_Click(object sender, EventArgs e)
        {
            WebUtils.SetupRedirect(this, SiteRoot + "/Announcement/editpost.aspx?pageid=" + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString());

        }
        protected virtual void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new AnnouncementConfiguration(getModuleSettings);
            TimeOffset = SiteUtils.GetUserTimeOffset();
            timeZone = SiteUtils.GetUserTimeZone();
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            keyword = WebUtils.ParseStringFromQueryString("keyword", keyword);

            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }

        }
        protected string FormatAnnouncementDate(DateTime startDate)
        {
            if (startDate != null)
            {
                return string.Format("{0:yyyy/MM/dd}", startDate);
            }
            return "";
        }
    }
}
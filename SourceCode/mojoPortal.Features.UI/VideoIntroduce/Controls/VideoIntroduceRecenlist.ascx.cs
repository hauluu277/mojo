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

namespace VideoIntroduceFeatures.UI
{
    public partial class VideoIntroduceRecenlist : System.Web.UI.UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;
        private int siteId = -1;
        private mojoBasePage basePage;
        private Module module;
        protected VideoIntroduceConfiguration config = new VideoIntroduceConfiguration();
        private TimeZoneInfo timeZone;
        protected Double TimeOffset;
        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
        private string siteRoot = string.Empty;
        private SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
        private string keyword = string.Empty;
        private List<VideoIntroduce> ListVideo = new List<VideoIntroduce>();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();

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

        public SiteSettings siteSettings
        {
            get { return siteSetting; }
            set { siteSetting = value; }
        }

        public VideoIntroduceConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }

        public int PageNumber
        {
            get { return pageNumber; }
            set { pageNumber = value; }
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
            LoadParams();
            LoadSettings();
            PopulateLabel();
            if (!IsPostBack)
            {
                PopulateControls();
            }
        }


        private void PopulateControls()
        {
            ListVideo = VideoIntroduce.GetPage(siteSetting.SiteId, 1, -1, string.Empty, pageNumber, 16, out totalPages);
            rptVideo.DataSource = ListVideo;
            rptVideo.DataBind();
            if (ListVideo.Count == 0)
            {
                VideoNull.Visible = true;
                VideoNull.Text = "Không tìm thấy dữ liệu";
            }
            string pageUrl = SiteRoot + "/VideoIntroduce/ListVideoIntroduce.aspx?pageid=" + pageId + "&mid=" + moduleId + "&pagenumber={0}";

            pgrVideo.PageURLFormat = pageUrl;
            pgrVideo.ShowFirstLast = true;
            pgrVideo.PageSize = 16;
            pgrVideo.PageCount = totalPages;
            pgrVideo.CurrentIndex = pageNumber;
            pnlVideoPager.Visible = (totalPages > 1);
        }

        private void PopulateLabel()
        {


        }
        private void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new VideoIntroduceConfiguration(getModuleSettings);
            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }

        }
        private void LoadParams()
        {
            itemId = WebUtils.ParseInt32FromQueryString("item", itemId);
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
        }
    }
}
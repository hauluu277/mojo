using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VideoIntroduceFeatures.Business;

namespace VideoIntroduceFeature.UI
{
    public partial class Detail : System.Web.UI.UserControl
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
        public VideoIntroduceConfiguration Config
        {
            get { return config; }
            set { config = value; }
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
            ListVideo = VideoIntroduce.GetTopOrtherVideo(siteSetting.SiteId, config.DisplayOrtherVideo, itemId);
            rptVideo.DataSource = ListVideo;
            rptVideo.DataBind();
            if (ListVideo != null && ListVideo.Count > 0)
            {
                pnlOrtherVideo.Visible = true;
            }

            VideoIntroduce VideoShow = new VideoIntroduce();
            if (itemId > 0)
            {
                VideoShow = new VideoIntroduce(itemId);
            }
            if (VideoShow != null)
            {

                lblDate.Text = string.Format("{0:dd/MM/yyyy}", VideoShow.CreateDate);
                lblTittle.Text = VideoShow.Title;
                lblView.Text = VideoShow.Views.ToString();
                StringBuilder player = new StringBuilder();
                if (VideoShow.TypePlayer == VideoIntroduceConstant.VideoPlayer)
                {
                    hdfTitle.Value = VideoShow.Title;
                    hdfVideo.Value = ConfigurationManager.AppSettings["VideoIntroduceFileFolder"] + VideoShow.Video;
                }
                else
                {
                    player.Append(VideoShow.YoutubeUrl);
                }
                literPlayer.Text = player.ToString();
            }
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

        }
    }
}
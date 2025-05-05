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
using System.Collections;
using System.Configuration;

namespace VideoIntroduceFeatures.UI
{
    public partial class VideoHot : System.Web.UI.UserControl
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
        private SiteSettings siteSetting;
        private string keyword = string.Empty;
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

            if (!Page.IsPostBack)
            {
                PopulateControls();
                BindOrtherVideo();
                hplVideo.Text = "Video";
            }

        }

        private void PopulateControls()
        {
            BindVideo();
        }

        private void BindOrtherVideo()
        {
            List<VideoIntroduce> ListVideo = VideoIntroduce.GetTopOrtherVideo(siteSetting.SiteId, 3);
            if (ListVideo != null && ListVideo.Count > 0)
            {
                rptVideo.DataSource = ListVideo;
                rptVideo.DataBind();
            }
        }

        private void BindVideo()
        {
            VideoIntroduce VideoShow = new VideoIntroduce();
            VideoShow = VideoIntroduce.GetVideoIsHot(siteSetting.SiteId);
            if (VideoShow != null)
            {

                StringBuilder player = new StringBuilder();
                if (VideoShow.TypePlayer == VideoIntroduceConstant.VideoPlayer)
                {
                    hdfTitle.Value = VideoShow.Title.ToString();
                    hdfVideo.Value = ConfigurationManager.AppSettings["VideoIntroduceFileFolder"] + VideoShow.Video;
                    hdfBackground.Value = ConfigurationManager.AppSettings["VideoIntroduceFileFolder"] + VideoShow.ImageVideo;
                }
                else
                {
                    if (!string.IsNullOrEmpty(VideoShow.YoutubeUrl))
                    {
                        player.Append(VideoShow.YoutubeUrl);
                    }
                }
                hplVideo.Text = VideoShow.Title.ToString();
                hplVideo.ToolTip = VideoShow.Title;
                hplVideo.NavigateUrl = VideoIntroduceUtils.VideoDetailItemUrl(SiteRoot, VideoShow.PageID, VideoShow.ModuleID, VideoShow.ItemID, VideoShow.ItemUrl);
                hplVideoHot.Text = VideoShow.Title.ToString();
                hplVideoHot.ToolTip = VideoShow.Title;
                hplVideoHot.NavigateUrl = VideoIntroduceUtils.VideoDetailItemUrl(SiteRoot, VideoShow.PageID, VideoShow.ModuleID, VideoShow.ItemID, VideoShow.ItemUrl);

                literPlayer.Text = player.ToString();
                //TitleVideo.Text = VideoShow.Title;
            }
            else
            {
                var ListData = VideoIntroduce.GetAllPublic(siteSetting.SiteId);
                if (ListData != null && ListData.Count > 0)
                {
                    VideoShow = ListData.FirstOrDefault();
                    if (VideoShow != null)
                    {
                        StringBuilder player = new StringBuilder();
                        if (VideoShow.TypePlayer == VideoIntroduceConstant.VideoPlayer)
                        {
                            hdfTitle.Value = VideoShow.Title;
                            hdfVideo.Value = ConfigurationManager.AppSettings["VideoIntroduceFileFolder"] + VideoShow.Video;
                            hdfBackground.Value = ConfigurationManager.AppSettings["VideoIntroduceFileFolder"] + VideoShow.ImageVideo;
                        }
                        else
                        {
                            player.Append("<video width='100%' height='180' style='float: left;' controls>");
                            player.Append("<source src='" + VideoShow.YoutubeUrl + "' type='video/youtube'>");
                            player.Append("</video>");
                        }
                        literPlayer.Text = player.ToString();
                        hplVideo.Text = VideoShow.Title;
                        hplVideo.ToolTip = VideoShow.Title;
                        hplVideo.NavigateUrl = VideoIntroduceUtils.VideoDetailItemUrl(SiteRoot, VideoShow.PageID, VideoShow.ModuleID, VideoShow.ItemID, VideoShow.ItemUrl);
                        hplVideoHot.NavigateUrl = VideoIntroduceUtils.VideoDetailItemUrl(SiteRoot, VideoShow.PageID, VideoShow.ModuleID, VideoShow.ItemID, VideoShow.ItemUrl);
                        hplVideoHot.ToolTip = VideoShow.Title;
                        hplVideoHot.Text = VideoShow.Title;
                    }
                }
            }
        }
        private void LoadSettings()
        {
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new VideoIntroduceConfiguration(moduleSettings);
        }
    }
}
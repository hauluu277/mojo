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

namespace VideoIntroduceFeatures.UI
{
    public partial class VideoIntroduceHotRecentList : System.Web.UI.UserControl
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
            hplLinkVideo.Text = "Xem thêm video";
            hplLinkVideo.NavigateUrl = "/video";
            hplLinkVideo.ToolTip = "Xem danh sách video";
            if (!IsPostBack)
            {
                SetupScripts();
            }
        }
        private void LoadParams()
        {

        }
        private void PopulateControls()
        {

        }
        private void SetupScripts()
        {
            //if (!(Page is mojoBasePage)) { return; }

            // include the main scripts
            mojoBasePage basePage = Page as mojoBasePage;
            basePage.ScriptConfig.IncludejPlayer = true;
            basePage.ScriptConfig.IncludejPlayerPlaylist = true;

            // setup the instance script
            StringBuilder script = new StringBuilder();
            script.Append("\n<script type=\"text/javascript\">\n");

            script.Append("(function() {");
            script.Append("var pl_" + this.ClientID + " = new jPlayerPlaylist({");
            script.Append("jPlayer: \"#" + PlayerInstance.ClientID + "\",");



            script.Append("cssSelectorAncestor: \"#" + PlayerContainer.ClientID + "\"");
            script.Append("}");

            //Start the construction of the playlist
            script.Append(",[");
            bool isFirstTrack = true;
            //Keep a list of the file types that were added for the track to use to create the 
            //"supplied" jPlayer constructor option
            bool isFirstFile = true;
            List<string> suppliedTypes = new List<string>();
            List<VideoIntroduce> mediaFiles = VideoIntroduce.GetAllVideoPublic(siteSetting.SiteId).Take(10).ToList(); ;
            foreach (VideoIntroduce file in mediaFiles)
            {
                if (isFirstTrack)
                {
                    script.Append("{");
                    isFirstTrack = false;
                }
                else
                {
                    script.Append(",{");
                }

                script.Append("title:\"" + file.Title + "\",");
                script.Append("artist:\"" + string.Format("{0:dd/MM/yyyy}", file.CreateDate) + "\",");
                //Add the proper property for each file depending upon it's file extension.
                string filePath = "/Data/File/VideoIntroduce/" + file.Video;

                string fullFilePath = Page.ResolveUrl(filePath);
                //string fullFilePath = siteRoot + file.FilePath.Replace("~", string.Empty);

                string fileExt = Path.GetExtension(file.Video).ToLowerInvariant();

                switch (fileExt)
                {
                    case ".m4v":
                        script.Append("m4v:\"" + fullFilePath + "\"");
                        if (!suppliedTypes.Contains("m4v"))
                            suppliedTypes.Add("m4v");
                        break;
                    case ".mp4":
                        script.Append("m4v:\"" + fullFilePath + "\"");
                        if (!suppliedTypes.Contains("m4v"))
                            suppliedTypes.Add("m4v");
                        break;
                    case ".webmv":
                        script.Append("webmv:\"" + fullFilePath + "\"");
                        if (!suppliedTypes.Contains("webmv"))
                            suppliedTypes.Add("webmv");
                        break;
                    case ".webm":
                        script.Append("webmv:\"" + fullFilePath + "\"");
                        if (!suppliedTypes.Contains("webmv"))
                            suppliedTypes.Add("webmv");
                        break;
                    case ".ogv":
                        script.Append("ogv:\"" + fullFilePath + "\"");
                        if (!suppliedTypes.Contains("ogv"))
                            suppliedTypes.Add("ogv");
                        break;
                    case ".ogg":
                        script.Append("ogv:\"" + fullFilePath + "\"");
                        if (!suppliedTypes.Contains("ogv"))
                            suppliedTypes.Add("ogv");
                        break;
                    case ".flv":
                        script.Append("flv:\"" + fullFilePath + "\"");
                        if (!suppliedTypes.Contains("flv"))
                            suppliedTypes.Add("flv");
                        break;
                    default:
                        throw new ArgumentException("No Supported Video File Extension Found");
                }
                script.Append("}");
            }
            script.Append("]");
            //End of playlist

            script.Append(",{");
            script.Append("playlistOptions: {");
            script.Append("loopOnPrevious: true");
            //if (config.AutoStart)
            //{
            //    script.Append(",autoPlay: true");
            //}
            script.Append("},");
            script.Append("swfPath: \"" + Page.ResolveUrl(WebConfigSettings.JPlayerBasePath + "Jplayer.swf") + "\"");
            script.Append(",supplied: \"");

            bool isFirstSupplied = true;
            foreach (string type in suppliedTypes)
            {
                if (isFirstSupplied)
                {
                    isFirstSupplied = false;
                }
                else
                {
                    script.Append(", ");
                }

                script.Append(type);
            }

            script.Append("\"");

            //script.Append(",preload:\"" + VideoPlayerConfiguration.VideoPreload + "\"");

            //script.Append(",wmode: \"" + VideoPlayerConfiguration.VideoWindowMode + "\"");

            //if (VideoPlayerConfiguration.PreferFlashSolution)
            //{
            script.Append(",solution:'flash,html'");
            //}

            //script.Append(",wmode: \"transparent\"");

            //if (config.ContinuousPlay)
            //{
            //script.Append(",loop: true");
            //}

            //if (VideoPlayerConfiguration.EnableErrors)
            //{
            //script.Append(",errorAlerts:true");
            //}

            //if (VideoPlayerConfiguration.EnableWarnings)
            //{
            //script.Append(",warningAlerts:true");
            //}

            script.Append("});");


            script.Append("})();");

            script.Append("\n</script>");

            this.Page.ClientScript.RegisterStartupScript(
                this.GetType(),
                this.UniqueID,
                script.ToString());
        }
        private void PopulateLabel()
        {

            VideoPlayLink.InnerHtml = MediaPlayerResources.PlayText;
            //Player control buttons.
            PreviousLink.InnerText = MediaPlayerResources.PreviousText;
            PlayLink.InnerText = MediaPlayerResources.PlayText;
            PauseLink.InnerText = MediaPlayerResources.PauseText;
            NextLink.InnerText = MediaPlayerResources.NextText;
            //StopLink.InnerText = MediaPlayerResources.StopText;
            //MuteLink.Title = MediaPlayerResources.MuteText;
            //MuteLink.InnerText = MediaPlayerResources.MuteText;
            //UnmuteLink.InnerText = MediaPlayerResources.UnmuteText;
            //UnmuteLink.Title = MediaPlayerResources.UnmuteText;
            //MaxVolumeLink.InnerText = MediaPlayerResources.MaxVolumeText;
            //MaxVolumeLink.Title = MediaPlayerResources.MaxVolumeText;

            //Player toggle buttons
            //Hide Fullscreen buttons for now, some browsers do not handle the full screen option well, waiting to find a solution.
            //if (!config.AllowFullScreen)
            //{
            //    FullScreenControl.Visible = false;
            //    RestoreScreenControl.Visible = false;
            //}

            // JA: this should be ok by default because there are default settings to disable it in browsers with known problems
            // see : http://www.jplayer.org/latest/developer-guide/#jPlayer-option-noFullScreen

            FullScreenLink.InnerText = MediaPlayerResources.FullScreenText;
            FullScreenLink.Title = MediaPlayerResources.FullScreenText;
            RestoreScreenLink.InnerText = MediaPlayerResources.RestoreScreenText;
            RestoreScreenLink.Title = MediaPlayerResources.RestoreScreenText;
            ShuffleLink.InnerText = MediaPlayerResources.ShuffleText;
            ShuffleLink.Title = MediaPlayerResources.ShuffleText;
            ShuffleOffLink.InnerText = MediaPlayerResources.ShuffleOffText;
            ShuffleOffLink.Title = MediaPlayerResources.ShuffleOffText;
            //RepeatLink.InnerText = MediaPlayerResources.RepeatText;
            //RepeatLink.Title = MediaPlayerResources.RepeatText;
            //RepeatOffLink.InnerText = MediaPlayerResources.RepeatOffText;
            //RepeatOffLink.Title = MediaPlayerResources.RepeatOffText;

            //No Solution Statement
            NoSolutionLiteral.Text = MediaPlayerResources.NoSolutionMessageMarkup;
        }
        private void LoadSettings() { }
    }
}
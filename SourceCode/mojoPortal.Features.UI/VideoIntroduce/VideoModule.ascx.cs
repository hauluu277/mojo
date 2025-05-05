using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Features;
using mojoPortal.Business;
using mojoPortal.Web;
using System.Collections;
using mojoPortal.Web.Framework;
using VideoIntroduceFeatures.Business;
using System.Text;

namespace VideoFeatures.UI
{
    public partial class VideoModule : SiteModuleControl
    {
        VideoIntroduceConfiguration config = new VideoIntroduceConfiguration();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            EnableViewState = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadParams();
            LoadSettings();
            PopulateLabels();
            PopulateControls();
        }

        private void PopulateLabels()
        {
            TitleControl.Visible = false;

            TitleControl.EditUrl = SiteRoot + "/VideoIntroduce/editpost.aspx";
            TitleControl.EditText = "Thêm mới video";
            if (IsEditable)
            {
                TitleControl.Visible = true;
                TitleControl.LiteralExtraMarkup = "&nbsp;&nbsp;&nbsp;<a href='"
                    + SiteRoot
                    + "/VideoIntroduce/ManagePost.aspx?pageid=" + PageId.ToInvariantString()
                    + "' class='ModuleEditLink' title='Quản trị video'>Quản trị video</a>"
                    ;
            }
        }

        private void PopulateControls()
        {
            if (!IsPostBack)
            {
                if (config.VideoHienThiSetting > 0)
                {
                    pnlVideo.CssClass = config.KieuHienThiSetting;

                    var video = new VideoIntroduce(config.VideoHienThiSetting);
                    var player = new StringBuilder();
                    if (video.TypePlayer == VideoIntroduceConstant.VideoPlayer)
                    {
                        player.Append("<video width='100%' height='250' style='float: left;' controls autoplay>");
                        player.Append("<source src='" + video.ItemUrl + "' type='video/mp4'>");
                        player.Append("</video>");
                    }
                    //else
                    //{
                    //    player.Append(video.youtubeUrl);    
                    //}

                    literVideo.Text = player.ToString();

                    lblTitle.Text = video.Title;
                    lblCreateddate.Text = string.Format("{0:dd/MM/yyyy}", video.CreateDate);
                }

            }

        }
        private void LoadSettings()
        {
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new VideoIntroduceConfiguration(moduleSettings);
        }
        private void LoadParams()
        {

        }
    }
}
using MediaAlbumFeature.Business;
using MediaGroupFeature.Business;
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
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using VideoIntroduceFeatures.Business;

namespace VideoIntroduceFeature.UI
{
    public partial class VideoListControl : System.Web.UI.UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;

        private mojoBasePage basePage;
        private Module module;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        protected VideoIntroduceConfiguration config = new VideoIntroduceConfiguration();
        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
        private int groupMediaId = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        private SiteSettings siteSettings;
        private int categoryID = -1;
        private bool? isFeatured = null;
        private int status = -1;
        private string keyword = string.Empty;
        private int orderBy = 0;

        private int isPublic = -1;
        private int typePlayer = -1;
        private int siteID = -1;

        protected VideoIntroduce firstVideo = new VideoIntroduce();

        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkImageUrl = string.Empty;
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
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
        public int CategoryId
        {
            get { return categoryID; }
            set { categoryID = value; }
        }
        public VideoIntroduceConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }
        public int GroupMediaID
        {
            get { return groupMediaId; }
            set { groupMediaId = value; }
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

        public string ImageSiteRoot
        {
            get { return imageSiteRoot; }
            set { imageSiteRoot = value; }
        }
        #endregion
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
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
        private void PopulateControls()
        {
            BindVideo();
        }
        private void BindVideo()
        {
            var listVideo = VideoIntroduce.GetAllPublic(siteSettings.SiteId);


            List<VideoIntroduce> lstMenu = new List<VideoIntroduce>();
            lstMenu = VideoIntroduce.GetPage(1, isPublic, typePlayer, keyword.ConvertToFTS(), pageNumber, Config.NumberShowSetting, out totalPages);


            firstVideo = listVideo.FirstOrDefault();
            IdItem.Value = firstVideo.ItemID.ToString();
            lblname.Text = firstVideo.Title;
            //hplFirst.NavigateUrl = MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, first.ItemUrl.ToString(), Convert.ToInt32(first.ItemID.ToString()), PageId, ModuleId);
            imgFirst.ImageUrl = "~/Data/File/VideoIntroduce/" + firstVideo.ImageVideo;


            StringBuilder setup = new StringBuilder();
            setup.Append("<div id='text'>");
            setup.Append("<a href='javascript: ShowVideo("+ firstVideo.ItemID + ")' id='video_"+ firstVideo.ItemID + "' class='showVideo' data-video='"+ firstVideo.ItemUrl+"' data-title='"+ firstVideo.Title+"' data-ngaytao='"+ firstVideo.CreateDate+ "'><span class='spot'></span> <i class='fa fa-play-circle' aria-hidden='true'></i></a>");
            setup.Append("</div>");
            literbtnChiTetVideo.Text = setup.ToString();
            rptVideo.DataSource = lstMenu.Skip(1);
            rptVideo.DataBind();


            string pageUrl = SiteRoot + "/VideoIntroduce/ViewList.aspx"
                  + "?pageid=" + PageId.ToInvariantString()
                  + "&mid=" + ModuleId.ToInvariantString()
                  + "&createByUser=" + "bigadmin"
                  //+ "&keyword=" + keyword
                  + "&pagenumber={0}";


            pgrDanhBa.PageURLFormat = pageUrl;
            pgrDanhBa.ShowFirstLast = true;
            pgrDanhBa.PageSize = config.PageSize;
            pgrDanhBa.PageCount = totalPages;
            pgrDanhBa.CurrentIndex = pageNumber;

            pnlDonViPager.Visible = (totalPages > 1);
        }


     
        private void LoadParams()
        {
            //pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            //moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            groupMediaId = WebUtils.ParseInt32FromQueryString("groupMediaId", GroupMediaID);
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);

        }
        public void PopulateLabels()
        {
            //lblIMGofLibrary.Text = MediaResources.ImageOfLibraryLabel;
        }
        protected virtual void LoadSettings()
        {

            siteSettings = CacheHelper.GetCurrentSiteSettings();
            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
        }

    }
}
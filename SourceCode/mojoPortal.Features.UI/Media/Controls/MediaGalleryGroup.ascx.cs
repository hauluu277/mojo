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
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace MediaFeature.UI
{
    public partial class MediaGalleryGroup : System.Web.UI.UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;
        private string createByUser = string.Empty;
        private DateTime? createDate = null;

        private mojoBasePage basePage;
        private Module module;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        protected MediaConfiguration config = new MediaConfiguration();
        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
        private int groupMediaId = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        private SiteSettings siteSettings;
        private int categoryID = -1;
        private bool? isPublish = null;
        private int status = -1;
        private string keyword = string.Empty;

        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkText = SwirlingQuestionResource.ButtonDelete;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkText = SwirlingQuestionResource.ButtonEdit;
        protected string EditLinkImageUrl = string.Empty;
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        private bool is_PhanTrang = false;
        public bool Is_PhanTrang
        {
            get { return is_PhanTrang; }
            set { is_PhanTrang = value; }
        }
        private int pageSize = 4;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }
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
        public MediaConfiguration Config
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
            //btnSearch.Click +=btnSearch_Click;
        }

        protected void btnSearch2_Click(object sender, EventArgs e)
        {
            keyword = txtSearch2.Value;
            string pageUrl = SiteRoot + "/Media/ViewListMediaGroup.aspx"
                  + "?pageid=" + PageId.ToInvariantString()
                  + "&mid=" + ModuleId.ToInvariantString()
                  + "&keyword=" + keyword
                  + "&pagenumber=1";
            WebUtils.SetupRedirect(this, pageUrl);
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
            LoadMediaData();
            txtSearch2.Value = keyword;
        }


        private void LoadMediaData()
        {
            EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
            DeleteLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;
            List<MediaGroup> lstMedia = new List<MediaGroup>();
            lstMedia = MediaGroup.GetPageParent(createByUser, createDate,siteSettings.SiteId, moduleId, pageNumber, config.PageSizeModule, keyword, out totalPages);
            if (lstMedia == null || lstMedia.Count() == 0)
            {
                lstMedia = MediaGroup.GetPage(siteSettings.SiteId, moduleId, pageNumber, config.PageSizeModule, keyword,-1, out totalPages);
            }
            dtlData.DataSource = lstMedia;
            dtlData.DataBind();
            if (lstMedia.Count == 0)
            {
                DanhBanull.Visible = true;
                DanhBanull.Text = MediaResources.NoDataFound;
            }
            string pageUrl = SiteRoot + "/Media/ViewListMediaGroup.aspx"
                  + "?pageid=" + PageId.ToInvariantString()
                  + "&mid=" + ModuleId.ToInvariantString()
                  + "&pagenumber={0}";

            //pgrDanhBa.PageURLFormat = pageUrl;
            //pgrDanhBa.ShowFirstLast = true;
            //pgrDanhBa.PageSize = config.PageSize;
            //pgrDanhBa.PageCount = totalPages;
            //pgrDanhBa.CurrentIndex = pageNumber;
            //pnlDonViPager.Visible = (totalPages > 1);
        }
        private void LoadParams()
        {
            //pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            // moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);

        }
        public void PopulateLabels()
        {
            lblDictionaryTitle.Text = MediaResources.CategoryMultiMediaTitle;
            btnSearch2.InnerText = MediaResources.MediaButtonSearch;
            //hplTitle.Text = config.NameTitle;
            //hplTitle.NavigateUrl = SiteRoot + "/thu-vien-anh";
            if(literVideo !=null)
            {
                literVideo.Text = Config.TextLinkVideo;
            }
            lblSlogan.Text = config.SloganSchool;

        }
        protected virtual void LoadSettings()
        {

            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new MediaConfiguration(getModuleSettings);
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            keyword = WebUtils.ParseStringFromQueryString("keyword", keyword);
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            siteRoot = SiteRoot;
            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
        }
    }
}
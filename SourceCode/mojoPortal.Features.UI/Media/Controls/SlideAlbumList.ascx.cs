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
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using VideoIntroduceFeatures.Business;

namespace MediaFeature.UI
{
    public partial class SlideAlbumList : System.Web.UI.UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;

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
        private bool? isFeatured = null;
        private int status = -1;
        private string keyword = string.Empty;
        private int orderBy = 0;

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
        public MediaConfiguration Config
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
            var isCategoryChild = MediaGroup.GetAllBySitePublish(siteSettings.SiteId).Where(x => x.Category == groupMediaId && x.IsPublish == true && x.Step == 6).FirstOrDefault();
            //nếu có danh mục con thì hiển thị các danh mục con
            if (isCategoryChild != null)
            {
                pnlGallery.Visible = false;
                pnlCategoryChild.Visible = true;
                LoadCategoryChild();
                lblCategory.Text = isCategoryChild.NameGroup;
                var group = new MediaGroup(groupMediaId);
                lblDateCreate.Text = string.Format("{0:dd/MM/yyyy}", group.CreatedDate);
                lblAuthor.Text = group.CreatedByUser;
            }
            //nếu không có danh mục con thì hiển thị danh sách ảnh
            else
            {
                pnlGallery.Visible = true;
                pnlCategoryChild.Visible = false;
                LoadMediaData();
                LoadGoupMedia();
            }

        }


        private void LoadCategoryChild()
        {
            List<MediaGroup> lstMedia = new List<MediaGroup>();
            lstMedia = MediaGroup.GetPage(siteSettings.SiteId, moduleId, pageNumber, config.PageSize, keyword, groupMediaId, out totalPages);
            rptCategoryChild.DataSource = lstMedia;
            rptCategoryChild.DataBind();
            if (lstMedia.Count == 0)
            {
                Categorynull.Visible = true;
                Categorynull.Text = MediaResources.NoDataFound;
            }
            string pageUrl = SiteRoot + "/Media/ViewListMediaAlbum.aspx"
                  + "?pageid=" + PageId.ToInvariantString()
                  + "&mid=" + ModuleId.ToInvariantString()
                  + "&groupMediaId=" + groupMediaId.ToInvariantString()
                  //+ "&keyword=" + keyword
                  + "&pagenumber={0}";
        }

        private void LoadMediaData()
        {
            EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
            DeleteLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;
            List<MediaAlbum> lstMedia = new List<MediaAlbum>();
            lstMedia = MediaAlbum.GetByGroup(siteSettings.SiteId, groupMediaId);
            dtlData.DataSource = lstMedia;
            dtlData.DataBind();
            //rptSlider.DataSource = lstMedia;
            //rptSlider.DataBind();

        }
        private void LoadGoupMedia()
        {
            var group = new MediaGroup(groupMediaId);
            if (group != null)
            {
                lblGroupName.Text = group.NameGroup;
                lblSapoLibrary.Text = group.Sapo;
                lblDateCreate.Text = string.Format("{0:dd/MM/yyyy}", group.CreatedDate);
                lblAuthor.Text = group.CreatedByUser;
            }
            var listGroupMedia = MediaGroup.GetGroupOther(siteSettings.SiteId, groupMediaId, config.PageSize);
            rptGroupMedia.DataSource = listGroupMedia;
            rptGroupMedia.DataBind();
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            groupMediaId = WebUtils.ParseInt32FromQueryString("groupMediaId", GroupMediaID);
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);

        }
        public void PopulateLabels()
        {
            hfView.Value = MediaResources.TotalViewsTitle;
            hfFeatured.Value = MediaResources.MediaAlbumFeaturedTitle;
            lblOrtherLibrary.Text = MediaResources.LibraryOrtherLabel;
            lblIMGofLibrary.Text = MediaResources.ImageOfLibraryLabel;
        }
        protected virtual void LoadSettings()
        {

            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new MediaConfiguration(getModuleSettings);
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
            hplEdit.Visible = false;
            if (Request.IsAuthenticated && siteUser != null)
            {
                hplEdit.Visible = siteUser.IsInRoles("Admins") || siteUser.IsInRoles(WebConfigSettings.RoleGalleryManage);
                hplEdit.NavigateUrl = SiteRoot + "/Media/Editpost.aspx?item=" + itemId;
            }
        }

    }
}
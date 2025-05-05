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
    public partial class MediaNewControl : System.Web.UI.UserControl
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
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            LoadParams();
            LoadSettings();
            PopulateLabels();
            if (!Page.IsPostBack)
            {
                pnlTab1.Visible = false;
                pnlTab2.Visible = false;
                if (Config.UseTabSetting == ArticleConstant.TabGalleryVideo)
                {
                    pnlTab2.Visible = true;
                    BindTab2();
                }
                else
                {
                    pnlTab1.Visible = true;
                    BindTab1();
                }
            }
        }
        private void BindTab1()
        {
            var media = new MediaGroup();
            if (!string.IsNullOrEmpty(config.CategoryGallerySetting))
            {
                var categoryId = config.CategoryGallerySetting.Split('-')[0].ToIntOrZero();
                var category = new CoreCategory(categoryId);
                media = MediaGroup.GetMediaByCategory(siteSettings.SiteId, categoryId, 1).FirstOrDefault();
                if(media == null) { media = new MediaGroup(); }

                hpllCategory.Text = category.Name;
                hpllCategory.NavigateUrl = category.Description;
            }
            else
            {
                hpllCategory.Text = "Ảnh mới";
                hpllCategory.NavigateUrl = "/anh";
                media = new MediaGroup(true);

            }

            hplMedia.NavigateUrl = siteRoot + media.ItemUrl.Replace("~", string.Empty);
            hplMedia.Text = media.NameGroup;
            hplMedia.ToolTip = media.NameGroup;
            imgNew.ToolTip = media.NameGroup;
            imgNew.AlternateText = media.NameGroup;
            imgNew.ImageUrl = "/Data/File/Media/" + media.FilePath;
        }
        private void BindTab2()
        {
            var listGroupMedia = MediaGroup.GetGroupOther(siteSettings.SiteId, groupMediaId, config.PageSize);
            rptGroupMedia.DataSource = listGroupMedia;
            rptGroupMedia.DataBind();
        }


        private void LoadParams()
        {
            //pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            // moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);

        }
        public void PopulateLabels()
        {

        }
        protected virtual void LoadSettings()
        {

            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
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
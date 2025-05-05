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
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace MediaFeature.UI
{
    public partial class GalleryHot : System.Web.UI.UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;
        protected bool AllowEdit=false;

        private mojoBasePage basePage;
        private Module module;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        protected MediaConfiguration config = new MediaConfiguration();
        private int pageId = -1;
        private int moduleId = -1;
        private int siteId = 0;
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
                PopulateControls();
            }
        }
        private void PopulateControls()
        {
            pnlTab1.Visible = false;
            pnlTab2.Visible = false;
            pnlTab3.Visible = false;
            pnlTab4.Visible = false;
            pnlTab5.Visible = false;
            if (config.UseTabSetting == ArticleConstant.TabCongNgheThongTinCDS)
            {
                pnlTab1.Visible = true;
                BindTab1();
            }
            else if (config.UseTabSetting == ArticleConstant.TabGalleryVideo)
            {
                pnlTab2.Visible = true;
                BindTab2();
            }
            else if (config.UseTabSetting == ArticleConstant.TabThongTinTuyenSinh)
            {
                pnlTab3.Visible = true;
                BindTab3();
            }
            else if (config.UseTabSetting == ArticleConstant.TabTinMoiDocNhieu)
            {
                pnlTab4.Visible = true;
                BindTab4();
            }
            else if (config.UseTabSetting == ArticleConstant.TabTinThongBao)
            {
                pnlTab5.Visible = true;
                BindTab5();
            }

        }


        private void BindTab1()
        {
            EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
            DeleteLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;
            List<MediaGroup> lstMedia = new List<MediaGroup>();
            //Lấy tất cả ảnh
            lstMedia = MediaGroup.GetTopHot(siteSettings.SiteId, config.NumberGallery);
            if (lstMedia != null && lstMedia.Any())
            {
                var galleryFirst = lstMedia.FirstOrDefault();
                //imgFirst.ImageUrl = "/Data/File/Media/" + galleryFirst.FilePath;
                //hplFirst.Text = galleryFirst.NameGroup;
                //hplFirst.NavigateUrl = MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, galleryFirst.ItemUrl, galleryFirst.ItemID, PageId, ModuleId);
                //hplImg.NavigateUrl = MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, galleryFirst.ItemUrl, galleryFirst.ItemID, PageId, ModuleId);
                //hplImg.ToolTip = galleryFirst.CategoryName;

                rptGalleryHot.DataSource = lstMedia.Take(4);
                rptGalleryHot.DataBind();

                if (lstMedia.Count > 4)
                {
                    rptGallery.DataSource = lstMedia.Skip(4);
                    rptGallery.DataBind();
                }
            }

        }
        /// <summary>
        /// Tab 2 cho phép thiết lập chọn và hiển thị thư viện ảnh ở trang Nghị viện thế giới
        /// </summary>
        private void BindTab2()
        {
            var media = MediaGroup.GetTopHot(siteSettings.SiteId, 1);
            if (media != null && media.Count > 0)
            {
                var firstId = Convert.ToInt32(config.GallerySelectSetting.Split('-')[0]);
                var gallery = new MediaGroup(media.FirstOrDefault().ItemID);
                imgTab2.ImageUrl = "/Data/File/Media/" + gallery.FilePath;

                hplTab2Title.Text = gallery.NameGroup;
                hplTab2Title.NavigateUrl = MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, gallery.ItemUrl, gallery.ItemID, PageId, ModuleId);

                hplTab2Image.NavigateUrl = MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, gallery.ItemUrl, gallery.ItemID, PageId, ModuleId);
                hplTab2Image.ToolTip = gallery.CategoryName;
            }

            if (!string.IsNullOrEmpty(config.GallerySelectSetting))
            {

            }
        }
        /// <summary>
        /// Tab 3 hiện thị phóng sự ảnh ở trang Quốc hội
        /// </summary>
        private void BindTab3()
        {
            if (!string.IsNullOrEmpty(config.GallerySelectSetting))
            {

                var listGallery = MediaGroup.GetListItem(siteSettings.SiteId, config.GallerySelectSetting);
                if (listGallery != null && listGallery.Count > 0)
                {
                    var galleryFirst = listGallery.FirstOrDefault();
                    imgTab3.ImageUrl = "/Data/File/Media/" + galleryFirst.FilePath;

                    hplTab3Title.Text = galleryFirst.NameGroup;
                    hplTab3Title.NavigateUrl = MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, galleryFirst.ItemUrl, galleryFirst.ItemID, PageId, ModuleId);

                    hplTab3Image.NavigateUrl = MediaUtils.FormatEditGroupMediaTitleUrl(SiteRoot, galleryFirst.ItemUrl, galleryFirst.ItemID, PageId, ModuleId);
                    hplTab3Image.ToolTip = galleryFirst.CategoryName;
                    if (listGallery.Count > 1)
                    {
                        rptGallery.DataSource = listGallery.Skip(1);
                        rptGallery.DataBind();
                    }
                }
            }
        }
        private void BindTab4()
        {
            var listGroupMedia = MediaGroup.GetGroupOther(siteSettings.SiteId, groupMediaId, config.PageSize);
            rptGroupMedia.DataSource = listGroupMedia.Take(4).ToList();
            rptGroupMedia.DataBind();
        }
        private void BindTab5()
        {
            if (!string.IsNullOrEmpty(Config.GallerySelectSetting))
            {
                var listCategory = Config.GallerySelectSetting.ToListIntFix('-');
                if (listCategory != null && listCategory.Count > 0)
                {
                    var categoryID = listCategory.FirstOrDefault();
                    var category = new CoreCategory(categoryID);
                    hplTitleCategoryImage.Text = category.Name;
                    hplTitleCategoryImage.ToolTip = category.Name;
                    hplTitleCategoryImage.NavigateUrl = siteRoot + category.Description;

                    var listMedia = MediaGroup.GetMediaByCategory(siteSettings.SiteId, categoryID, Config.TopVideoSetting);
                    rptGalleryImageTab5.DataSource = listMedia;
                    rptGalleryImageTab5.DataBind();
                }
            }
        }
        private void LoadParams()
        {
            //pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            // moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);

        }

        protected string FormatDateGallery(object dateTime)
        {
            if (dateTime != null)
            {
                var time = string.Format("{0:HH:mm}", dateTime);
                var date = string.Format("{0:dd/MM/yyyy}", dateTime);
                return time + " | " + date;
            }
            return string.Empty;
        }

        public void PopulateLabels()
        {

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
            if (Request.IsAuthenticated && siteUser != null)
            {
                AllowEdit = siteUser.IsInRoles("Admins") || siteUser.IsInRoles(WebConfigSettings.RoleGalleryManage);
            }
        }
    }
}
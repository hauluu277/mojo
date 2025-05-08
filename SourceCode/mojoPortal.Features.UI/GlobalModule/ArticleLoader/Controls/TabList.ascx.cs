using ArticleFeature.Business;
using MediaGroupFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Service.Business;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ArticleFeature.UI
{
    public partial class TabLoader : UserControl
    {
        #region Properties

        private int pageNumber = 1;
        private int totalPages = 1;
        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string EditBlogAltText = "Edit";
        protected Double TimeOffset;
        private TimeZoneInfo timeZone;
        protected DateTime CalendarDate;
        protected bool ShowGoogleMap = true;
        protected string FeedBackLabel = string.Empty;
        protected string GmapApiKey = string.Empty;
        protected bool EnableContentRating;
        protected string disqusFlag = string.Empty;
        protected string IntenseDebateAccountId = string.Empty;
        protected bool ShowCommentCounts = true;
        protected string EditLinkText = ArticleResources.BlogEditEntryLink;
        protected string EditLinkTooltip = ArticleResources.BlogEditEntryLink;
        protected string EditLinkImageUrl = string.Empty;
        private mojoBasePage basePage;
        private Module module;
        protected ArticleConfiguration config = new ArticleConfiguration();
        private int pageId = -1;
        private int moduleId = -1;
        private int siteId = -1;
        private int pureModuleId = -1;
        protected int langId = -1;
        protected int type = -1;
        private int itemId = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        private SiteSettings siteSettings;
        private string[] listModuleId;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private string moduleHeading = string.Empty;
        protected bool visibleHighlight = false;
        protected bool visibleList = false;
        public string lnkHotArticle_Title = "";
        public string lnkHotArticle_HRef = "";
        public string lnkHotArticle_Summary = "";
        public string lnkHotArticle_Src = ""; 
        public string[] ListModuleId
        {
            get { return listModuleId; }
            set { listModuleId = value; }
        }

        public int PureModuleId
        {
            get { return pureModuleId; }
            set { pureModuleId = value; }
        }

        public int PageId
        {
            get { return pageId; }
            set { pageId = value; }
        }
        public int SiteId
        {
            get { return siteId; }
            set { siteId = value; }
        }
        public int ModuleId
        {
            get { return moduleId; }
            set { moduleId = value; }
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

        public string ModuleHeading
        {
            get { return moduleHeading; }
            set { moduleHeading = value; }
        }

        public ArticleConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }

        public bool IsEditable { get; set; }

        public bool DisplayTab { get; set; }

        #endregion


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            EnableViewState = false;
            //pgr.Command += new CommandEventHandler(pgr_Command);
        }

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            var lang = CultureInfo.CurrentCulture.Name;
            langId = lang == "vi-VN" ? LanguageConstant.VN : LanguageConstant.EN;
            type = langId;
            LoadSettings();
            PopulateLabels();
            //SetupRssLink();

            PopulateControls();

        }

        private void PopulateControls()
        {
            pnlTinMoiTinDocNhieu.Visible = false;
            pnlThongBao.Visible = false;
            pnlCongNgheThongTin.Visible = false;
            pnlThongTinTuyenSinh.Visible = false;
            pnlGalleryVideo.Visible = false;
            pnlCacPhongDonVi.Visible = false;
            pnlDanhSach.Visible = false;

            pnlVanBanMoi.Visible = false;
            pnlTab5.Visible = false;
            pnlDanhSach.Visible = false;
            pnlThanhTichBangVang.Visible = false;
            pnlLienKetWebsite.Visible = false;
            pnlTinTucSuKien.Visible = false;

            pnlThongBaoVanBan.Visible = false;
            pnlHienThiCacChuyenMuc.Visible = false;


            pnlGuongSang.Visible = false; 
            pnlChuyenMucCon.Visible = false;
            pnlTinSuKien.Visible = false;

            if (config.TabSelectorSetting == ArticleConstant.TabTinMoiDocNhieu)
            {
                BindTinMoiDocNhieu();
            }
            else if (config.TabSelectorSetting == ArticleConstant.TabCongNgheThongTinCDS)
            {
                BindCongNgheThongTin();
            }
            else if (config.TabSelectorSetting == ArticleConstant.TabThongTinTuyenSinh)
            {
                BindThongTinTuyenSinh();
            }
            else if (config.TabSelectorSetting == ArticleConstant.TabGalleryVideo)
            {
                BindGalleryVideo();
            }
            else if (config.TabSelectorSetting == ArticleConstant.TabTinThongBao)
            {
                BindThongBao();
            }
            else if (config.TabSelectorSetting == ArticleConstant.TabVanBanMoi)
            {
                BindVanBanMoi();
            }
            else if (config.TabSelectorSetting == ArticleConstant.TabPhongTrucThuoc)
            {
                BindPhongSoGDDT();
            }
            else if (config.TabSelectorSetting == ArticleConstant.TabDanhSachTruong)
            {
                BindDanhSachTruong();
            }
            else if (config.TabSelectorSetting == ArticleConstant.TabBangVangThanhTich)
            {
                BindBangVangThanhTich();
            }
            else if (config.TabSelectorSetting == ArticleConstant.TabLienKetWebsite)
            {
                BindLienKetWebsite();
            }
            else if (config.TabSelectorSetting == ArticleConstant.TabTinTucSuKien)
            {
                BindTinTucSuKien();
            }
            else if (config.TabSelectorSetting == ArticleConstant.TabGuongSang)
            {
                BindGuongSang();
            }
            else if (config.TabSelectorSetting == ArticleConstant.TabVanBanThongBao)
            {
                BindVanBanThongBao();
            }
            else if (config.TabSelectorSetting == ArticleConstant.TabDanhSachCacChuyenMuc)
            {
                BindDanhSachChuyenMuc();
            }
            else if (config.TabSelectorSetting == ArticleConstant.TabTinVaChuyenMucCon)
            {
                BindHienThiTinVaChuyenMuc();
            }
            else if (config.TabSelectorSetting == ArticleConstant.TabKinhDoanh)
            {
                BindTinKinhDoanh();
            }
            else if (config.TabSelectorSetting == ArticleConstant.TabTinChuyenTiep)
            {
                BindHienThiTinChuyenTiep();
            }
            else
            {
                BindCongNgheThongTin();
            }
        }
        private void BindDanhSachChuyenMuc()
        {
            pnlHienThiCacChuyenMuc.Visible = true;
            var core_DanhMucBusiness = new core_DanhMucBusiness(new mojoportal.Service.UoW.UnitOfWork());
            var data = core_DanhMucBusiness.GetTop(config.NumberArticleLimit, siteSettings.SiteId);
            rptChuyenMucLienKet.DataSource = data;
            rptChuyenMucLienKet.DataBind();
        }
        private void BindVanBanThongBao()
        {
            pnlThongBaoVanBan.Visible = true;

            //bind văn bản, tài liệu
            int totalVanBan = 0;
            List<Documentation> reader = Documentation.GetTopSlide(siteSettings.SiteId, config.NumberArticleLimit);
            rptVanBanMoi.DataSource = reader;
            rptVanBanMoi.DataBind();
            hplVanBanMoiNhat.Text = "Văn bản mới";
            hplVanBanMoiNhat.NavigateUrl = SiteRoot + "/van-ban";
            hplVanBanMoiMore.NavigateUrl = SiteRoot + "/van-ban";

            //bind tin tức
            var categories = config.ArticleCategoryConfig.Replace("-", " ");
            if (!string.IsNullOrEmpty(categories))
            {
                var categorySetting = config.ArticleCategoryConfig.Replace("-", " ");

                categories = categories.Trim();


                var listThongBaoMoi = Article.GetArticleTopNew(categorySetting, config.NumberArticleLimit, siteSettings.SiteId, false);

                rptThongBaoMoi.DataSource = listThongBaoMoi;
                rptThongBaoMoi.DataBind();

                var dataHitCount = Article.GetArticleTopHitCount("", config.NumberArticleLimit, siteSettings.SiteId, false);
                rptDocNhieuNhat.DataSource = dataHitCount;
                rptDocNhieuNhat.DataBind();


                var categorieSplit = categories.Replace("-", " ");
                if (!string.IsNullOrEmpty(categories))
                {
                    categories = categories.Trim();
                    var categoryFirst = categories.Split(' ')[0].ToIntOrZero();
                    var category = new CoreCategory(categoryFirst);

                    hplThongBaoMoiNhat.Text = category.Name;

                    hplThongBaoMoiNhat.NavigateUrl = siteRoot + category.Description;
                    hplThongBaoMoiMore.NavigateUrl = siteRoot + category.Description;
                }

            }

        }

        private void BindGuongSang()
        {
            pnlGuongSang.Visible = true;
            var categories = config.ArticleCategoryConfig.Replace("-", " ");
            LoadCategory(config.ArticleCategoryConfig, hplGuongSang, hplMoreGuongSang);
            if (!string.IsNullOrEmpty(categories))
            {
                categories = categories.Trim();
                var listArticle = Article.GetArticleHotByCategory(siteId, categories, config.NumberArticleLimit, 0, true);
                if (listArticle != null && listArticle.Any())
                {
                    var firstArticle = listArticle[0];
                    hplArticleGuongSang.NavigateUrl = ArticleUtils.FormatBlogTitleUrl(SiteRoot, firstArticle.ItemUrl, firstArticle.ItemID, pageId, moduleId);
                    hplArticleGuongSang.Text = firstArticle.Title;
                    liArticleGuongSang.Text = firstArticle.Summary;

                    imgArticleGuongSang.ImageUrl = ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], firstArticle.ImageUrl);

                    rptArticleGuongSang.DataSource = listArticle.Skip(1).ToList();
                    rptArticleGuongSang.DataBind();
                }
            }
        }

        /// <summary>
        /// tin tức sự kiện
        /// </summary>
        private void BindTinTucSuKien()
        {
            LoadTab(pnlTinTucSuKien, rptTinTucSuKien, hplTinTucSuKien, hplMoreTinTucSuKien);
        }/// <summary>
        /// tin tức sự kiện
        /// </summary>
        private void BindHienThiTinVaChuyenMuc()
        {
            pnlChuyenMucCon.Visible = true;
            var categories = config.ArticleCategoryConfig.Replace("-", " ");

            if (!string.IsNullOrEmpty(categories))
            {
                categories = categories.Trim();
                var firstCategory = config.ArticleCategoryConfig.Split('-')[0];

                // Load category chính (hiển thị trong HyperLink)
                LoadCategory(config.ArticleCategoryConfig, hplChuyenMucCon);

                // Load các sub-categories (hiển thị trong Repeater)
                var listCategory = CoreCategory.GetChildren(Convert.ToInt32(firstCategory));

                rptChuyenMucCon1.DataSource = listCategory;
                rptChuyenMucCon1.DataBind();

                // Tạo danh sách category IDs để lấy bài viết
                var lstCategory = string.Join(" ", listCategory.Select(x => x.ItemID).ToArray());
                lstCategory += " " + firstCategory;

                // Lấy danh sách bài viết hot theo categories
                var listArticle = Article.GetArticleHotByCategory(siteId, lstCategory, config.NumberArticleLimit, 0, true);

                if (listArticle != null && listArticle.Any())
                {
                    // Bài viết đầu tiên hiển thị bên trái
                    var firstArticle = listArticle[0];

                    hplChuyenMucCon2.NavigateUrl = ArticleUtils.FormatBlogTitleUrl(SiteRoot, firstArticle.ItemUrl, firstArticle.ItemID, pageId, moduleId);
                    hplChuyenMucCon2.Text = firstArticle.Title;
                    liArticleChuyenMucCon.Text = firstArticle.Summary;
                    ImgChuyenMucCon.ImageUrl = ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], firstArticle.ImageUrl);

                    // Các bài viết còn lại hiển thị bên phải (skip bài đầu tiên)
                    rptChuyenMucCon2.DataSource = listArticle.Skip(1).ToList();
                    rptChuyenMucCon2.DataBind();
                }
            }
        }
        private void BindHienThiTinChuyenTiep()
        {
            pnlTinSuKien.Visible = true;
            var categories = config.ArticleCategoryConfig.Replace("-", " ");

            if (!string.IsNullOrEmpty(categories))
            {
                categories = categories.Trim();
                var firstCategory = config.ArticleCategoryConfig.Split('-')[0];

                // Load category chính (hiển thị trong HyperLink)
                LoadCategory(config.ArticleCategoryConfig, hplChuyenMucTin);

                var listCategory = CoreCategory.GetChildren(Convert.ToInt32(firstCategory));
                // Tạo danh sách category IDs để lấy bài viết
                var lstCategory = string.Join(" ", listCategory.Select(x => x.ItemID).ToArray());
                lstCategory += " " + firstCategory;

                // Lấy danh sách bài viết hot theo categories
                var listArticle = Article.GetArticleHotByCategory(siteId, lstCategory, config.NumberArticleLimit, 0, true);
                rptTinSuKien.DataSource = listArticle;
                rptTinSuKien.DataBind(); 
            }
        }
        private void BindLienKetWebsite()
        {
            pnlLienKetWebsite.Visible = true;
            var parent = CoreCategory.GetByCode(siteSettings.SiteId, "LIENKET-WEBSITE");
            if (parent != null)
            {
                var listLienKet = CoreCategory.GetChildren(parent.ItemID);
                ddlLienKetWebsite.DataTextField = "Name";
                ddlLienKetWebsite.DataValueField = "Description";
                ddlLienKetWebsite.DataSource = listLienKet;
                ddlLienKetWebsite.DataBind();
                ddlLienKetWebsite.Items.Insert(0, new ListItem { Value = "", Text = "-Liên kết website-" });
            }
        }
        private void BindBangVangThanhTich()
        {
            pnlThanhTichBangVang.Visible = true;

            var parent = CoreCategory.GetByCode(siteSettings.SiteId, WebConfigSettings.DM_BANGVANG);
            if (parent != null)
            {
                var listNamHoc = CoreCategory.GetChildren(parent.ItemID);
                if (listNamHoc != null)
                {
                    var firstNamHoc = listNamHoc.FirstOrDefault();
                    hplThanhTichNamHoc.Text = firstNamHoc.Name;
                    hplThanhTichNamHoc.Text = siteRoot + firstNamHoc.Description;
                    var listThanhTich = CoreCategory.GetChildren(firstNamHoc.ItemID);
                    rptThanhTichNamHoc.DataSource = listThanhTich;
                    rptThanhTichNamHoc.DataBind();
                }
            }
        }


        private void BindDanhSachTruong()
        {
            pnlDanhSach.Visible = true;
            var catId = config.CategorySetting;
            if (config.CategorySetting == 0)
            {
                var parent = CoreCategory.GetByCode(siteSettings.SiteId, WebConfigSettings.DM_TRUONGHOC);
                catId = parent.ItemID;
            }

            var listLoaiTruong = CoreCategory.GetChildren(catId);
            rptDanhSachTruong.DataSource = listLoaiTruong;
            rptDanhSachTruong.DataBind();
            if (listLoaiTruong != null && listLoaiTruong.Any())
            {
                var firstLoaiTruong = listLoaiTruong.FirstOrDefault();
                var listTruongHoc = CoreCategory.GetChildren(firstLoaiTruong.ItemID).Take(config.NumberArticleLimit).ToList();
                rptCacTruong.DataSource = listTruongHoc;
                rptCacTruong.DataBind();

                hplMoreDanhSachTruong.Text = "Xem thêm";
                hplMoreDanhSachTruong.NavigateUrl = siteRoot + firstLoaiTruong.Description;
            }
        }
        
        private void BindTinKinhDoanh()
        {
            Panelkd.Visible = true;
            var categories = config.ArticleCategoryConfig.Replace("-", " ");

            if (!string.IsNullOrEmpty(categories))
            {
                categories = categories.Trim();
                var fistCategory = config.ArticleCategoryConfig.Split('-')[0];
                LoadCategory(config.ArticleCategoryConfig, hplCategoryKinhDoanh);
                var listCategory = CoreCategory.GetChildren(Convert.ToInt32(fistCategory));

                Repeaterkd.DataSource = listCategory;
                Repeaterkd.DataBind();

                var lstCategory = string.Join(" ", listCategory.Select(x => x.ItemID).ToArray());
                lstCategory += " " + fistCategory;

                var listArticle = Article.GetArticleHotByCategory(siteId, lstCategory, config.NumberArticleLimit, 0, true);
                if (listArticle != null && listArticle.Any())
                {
                    var firstArticle = listArticle[0];

                    hplKinhDoanh.NavigateUrl = ArticleUtils.FormatBlogTitleUrl(SiteRoot, firstArticle.ItemUrl, firstArticle.ItemID, pageId, moduleId);
                    hplKinhDoanh.Text = firstArticle.Title;

                    hpldescriptionKinhDoanh.Text = firstArticle.Summary;
                    imgKinhDoanh.ImageUrl = ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], firstArticle.ImageUrl);

                    var secondArticle = listArticle[1];

                    hplKinhDoanh1.NavigateUrl = ArticleUtils.FormatBlogTitleUrl(SiteRoot, secondArticle.ItemUrl, secondArticle.ItemID, pageId, moduleId);
                    hplKinhDoanh1.Text = secondArticle.Title;
                    hpldescriptionKinhDoanh1.Text = secondArticle.Summary;



                    rptKinhDoanh.DataSource = listArticle.Skip(2).ToList();
                    rptKinhDoanh.DataBind();
                }
            }
        }



        private void BindPhongSoGDDT()
        {
            //var parent = CoreCategory.GetByCode(siteSettings.SiteId, WebConfigSettings.DM_PHONGBAN);
            //if (parent != null)
            //{
            //    pnlCacPhongDonVi.Visible = true;
            //    var listPhong = CoreCategory.GetChildren(parent.ItemID);
            //    rptCategoryCacPhong.DataSource = listPhong.Take(config.NumberArticleLimit).ToList();
            //    rptCategoryCacPhong.DataBind();

            //}
            LoadTab(pnlCacPhongDonVi, rptArticleCacPhong, hplCategoryCacPhong);
        }


        private void BindGalleryVideo()
        {
            pnlGalleryVideo.Visible = true;

            var listGallery = MediaGroup.GetTopHot(siteSettings.SiteId, config.NumberArticleLimit);
            if (listGallery != null && listGallery.Count > 0)
            {
                var first = listGallery[0];
                imgThuVienAnh.ImageUrl = "/Data/File/Media/" + first.FilePath;
                hplThuVienAnh.NavigateUrl = siteRoot + first.ItemUrl;
                hplThuVienAnh.Text = first.NameGroup;

                rptThuVienAnh.DataSource = listGallery.Skip(1).ToList();
                rptThuVienAnh.DataBind();
            }
        }

        /// <summary>
        /// Công nghệ thông tin và chuyển đổi số
        /// </summary>
        private void BindCongNgheThongTin()
        {
            pnlCongNgheThongTin.Visible = true;
            var categories = config.ArticleCategoryConfig.Replace("-", " ");
            LoadCategory(config.ArticleCategoryConfig, hplCongNgheThongTin, hplMoreCongNgheThongTin);
            if (!string.IsNullOrEmpty(categories))
            {
                categories = categories.Trim();
                var listArticle = Article.GetArticleHotByCategory(siteId, categories, config.NumberArticleLimit, 0, true);
                if (listArticle != null && listArticle.Any())
                {
                    var firstArticle = listArticle[0];
                    hplArticleCongNgheThongTin.NavigateUrl = ArticleUtils.FormatBlogTitleUrl(SiteRoot, firstArticle.ItemUrl, firstArticle.ItemID, pageId, moduleId);
                    hplArticleCongNgheThongTin.Text = firstArticle.Title;

                    imgArticleCongNgheThongTin.ImageUrl = ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], firstArticle.ImageUrl);

                    rptCongNgheThongTin.DataSource = listArticle.Skip(1).ToList();
                    rptCongNgheThongTin.DataBind();
                }
            }
        }
        /// <summary>
        /// thông báo
        /// </summary>
        private void BindThongBao()
        {
            pnlThongBao.Visible = true;
            LoadTab(pnlThongBao, rptThongBao, null);
        }

        /// <summary>
        /// Văn bản mới
        /// </summary>
        private void BindVanBanMoi()
        {
            pnlVanBanMoi.Visible = true;
            //bind văn bản, tài liệu
            int totalVanBan = 0;

            List<Documentation> reader = Documentation.GetTopSlide(siteSettings.SiteId, config.NumberArticleLimit);
            rptDocument.DataSource = reader;
            rptDocument.DataBind();

        }


        /// <summary>
        /// Hiển tab tin mới, đọc nhiều
        /// </summary>
        private void BindTinMoiDocNhieu()
        {
            var listArticle = Article.GetAll();
            pnlTinMoiTinDocNhieu.Visible = true;



            //rptTinDocNhieu.DataSource = listArticle.Take(5);
            //rptTinDocNhieu.DataBind();


            rptTinMoi.DataSource = listArticle.Skip(5).Take(5);
            rptTinMoi.DataBind();
        }


        /// <summary>
        /// Hiển tab thông tin tuyển sinh
        /// </summary>
        private void BindThongTinTuyenSinh()
        {
            pnlThongTinTuyenSinh.Visible = true;
            var categories = config.ArticleCategoryConfig.Replace("-", " ");

            if (!string.IsNullOrEmpty(categories))
            {
                categories = categories.Trim();
                var fistCategory = config.ArticleCategoryConfig.Split('-')[0];
                LoadCategory(config.ArticleCategoryConfig, hplCategoryThongTinTuyenSinh);
                var listCategory = CoreCategory.GetChildren(Convert.ToInt32(fistCategory));

                rptCategoryTuyenSinh.DataSource = listCategory;
                rptCategoryTuyenSinh.DataBind();

                var lstCategory = string.Join(" ", listCategory.Select(x => x.ItemID).ToArray());
                lstCategory += " " + fistCategory;

                var listArticle = Article.GetArticleHotByCategory(siteId, lstCategory, config.NumberArticleLimit, 0, true);
                if (listArticle != null && listArticle.Any())
                {
                    var firstArticle = listArticle[0];

                    hplArticleTuyenSinh.NavigateUrl = ArticleUtils.FormatBlogTitleUrl(SiteRoot, firstArticle.ItemUrl, firstArticle.ItemID, pageId, moduleId);
                    hplArticleTuyenSinh.Text = firstArticle.Title;
                    liArticleTuyenSinh.Text = firstArticle.Summary;

                    imgArticleTuyenSinh.ImageUrl = ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], firstArticle.ImageUrl);

                    rptThongTinTuyenSinh.DataSource = listArticle.Skip(1).ToList();
                    rptThongTinTuyenSinh.DataBind();
                }
            }

        }

        private void LoadTab(Panel pnlTab, Repeater rptArticle, HyperLink hplCategory, HtmlAnchor htmlAnchor = null)
        {
            pnlTab.Visible = true;
            var categories = config.ArticleCategoryConfig.Replace("-", " ");

            LoadCategory(config.ArticleCategoryConfig, hplCategory, htmlAnchor);
            if (!string.IsNullOrEmpty(categories))
            {
                categories = categories.Trim();
                var listArticle = Article.GetArticleHotByCategory(siteId, categories, config.NumberArticleLimit, 0, true);
                if (listArticle != null && listArticle.Any())
                {
                    rptArticle.DataSource = listArticle;
                    rptArticle.DataBind();
                }
            }
        }


        private void LoadCategory(string categoryConfig, HyperLink hplCategory, HtmlAnchor htmlAnchor = null)
        {
            if (hplCategory != null)
            {
                var categories = categoryConfig.Replace("-", " ");
                if (!string.IsNullOrEmpty(categories))
                {
                    categories = categories.Trim();
                    var categoryFirst = categories.Split(' ')[0].ToIntOrZero();
                    var category = new CoreCategory(categoryFirst);
                    hplCategory.NavigateUrl = siteRoot + category.Description;
                    if (htmlAnchor != null)
                    {
                        htmlAnchor.HRef = siteRoot + category.Description;
                    }
                    if (!string.IsNullOrEmpty(category.SubName))
                    {
                        hplCategory.Text = category.SubName;
                    }
                    else
                    {
                        hplCategory.Text = category.Name;
                    }
                }
            }
        }


        public List<Article> BindArticlesTab4(int categoryID, int count = 10)
        {
            List<Article> result = Article.GetOrtherByCategory(categoryID, itemId, count);
            return result;
        }

        public List<Article> BindArticles(int categoryID)
        {
            List<Article> result = Article.GetOrtherByCategory(categoryID, itemId, config.NumberArticleLimit);
            return result;
        }
        protected bool ShowImage(string imageUrl)
        {
            if (String.IsNullOrEmpty(imageUrl))
            {
                return false;
            }
            else
            {
                if (imageUrl.Contains("http") || imageUrl.Contains("https")) return true;
                string filePath = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["ArticleImagesFolder"] + imageUrl;
                filePath = filePath.Replace("/", "\\");
                if (File.Exists(filePath))
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
        }


        public List<ArticleBO> BindArticlesRight(int categoryID)
        {
            List<ArticleBO> result = ArticleBO.GetPageCategory(categoryID, langId, config.PageSize, pageNumber, out totalPages).Skip(5).Take(5).ToList();
            return result;
        }

        public List<ArticleBO> BindArticleFirst(int categoryID)
        {
            List<ArticleBO> result = ArticleBO.GetPageCategory(categoryID, langId, config.PageSize, pageNumber, out totalPages).Take(1).ToList();
            if (result != null && result.Count > 0)
            {
                itemId = result[0].ItemID;
            }
            return result;
        }

        public List<Article> BindListArticles(int categoryID)
        {
            List<Article> result = Article.GetPageCategory(categoryID, config.PageSize, pageNumber, out totalPages).Take(5).ToList();
            return result;
        }
        protected string FormatArticleDate(DateTime startDate)
        {
            //if (config.DateTimeFormat == string.Empty) return startDate.ToShortDateString();
            //return startDate.ToString(config.DateTimeFormat);
            if (config.DateTimeFormat == string.Empty) return string.Empty;
            string result = "";
            result = timeZone != null ? TimeZoneInfo.ConvertTimeFromUtc(startDate, timeZone).ToString(config.DateTimeFormat) : startDate.AddHours(TimeOffset).ToString(config.DateTimeFormat);
            return result;
        }

        protected virtual void PopulateLabels()
        {
            if (config.UseListArticle)
            {
                visibleList = true;
                visibleHighlight = false;
            }
            else
            {
                visibleList = false;
                visibleHighlight = true;
            }
            EditBlogAltText = ArticleResources.EditImageAltText;
            FeedBackLabel = ArticleResources.BlogFeedbackLabel;

            mojoBasePage mojoBasePage = Page as mojoBasePage;
            if (mojoBasePage == null) return;
            if (!mojoBasePage.UseTextLinksForFeatureSettings)
            {
                EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
            }
        }

        protected string FormatPostAuthor(string userGuid)
        {
            return ArticleUtils.FormatPostAuthor(userGuid, config);
        }

        protected string FormatBlogDate(DateTime startDate)
        {
            return ArticleUtils.FormatBlogDate(startDate, config, timeZone, TimeOffset);
        }

        protected string BuildEditUrl(int itemID)
        {
            return ArticleUtils.BuildEditUrl(itemID, listModuleId, SiteRoot, PageId, ModuleId);
        }

        private string GetRssUrl()
        {
            return ArticleUtils.GetRssUrl(config, SiteRoot, ModuleId);
        }

        protected virtual void LoadSettings()
        {
            SetJQueryScript();
            visibleHighlight = true;
            visibleList = false;
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            siteId = siteSettings.SiteId;
            TimeOffset = SiteUtils.GetUserTimeOffset();
            timeZone = SiteUtils.GetUserTimeZone();
            GmapApiKey = SiteUtils.GetGmapApiKey();
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);

            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }

            CalendarDate = WebUtils.ParseDateFromQueryString("blogdate", DateTime.UtcNow).Date;

            if (CalendarDate > DateTime.UtcNow.Date)
            {
                CalendarDate = DateTime.UtcNow.Date;
            }

            if ((config.UseExcerpt) && (!config.GoogleMapIncludeWithExcerpt)) { ShowGoogleMap = false; }

            EnableContentRating = config.EnableContentRating;
            if (config.UseExcerpt) { EnableContentRating = false; }

            IntenseDebateAccountId = config.IntenseDebateAccountId.Length > 0 ? config.IntenseDebateAccountId : siteSettings.IntenseDebateAccountId;

            if (config.AllowComments)
            {
                if ((IntenseDebateAccountId.Length > 0) && (config.CommentSystem == "intensedebate"))
                {
                    ShowCommentCounts = false;
                }
            }

            //if (config.Copyright.Length > 0)
            //{
            //    lblCopyright.Text = config.Copyright;
            //}

            if (IsEditable)
            {
                //Article.CountOfDrafts(ModuleId);
            }
        }

        private void SetJQueryScript()
        {
            //StringBuilder sb = ArticleUtils.SetJQueryScript(config, pureModuleId);
            //if (!Page.ClientScript.IsStartupScriptRegistered(typeof(Page), "postlist" + pureModuleId) && sb.ToString() != string.Empty)
            //{
            //    Page.ClientScript.RegisterStartupScript(typeof(Page), "postlist" + pureModuleId, sb.ToString());
            //}
            //if (Page.ClientScript.IsStartupScriptRegistered(typeof(Page), "jquerytool")) return;
            //string script = "<script src='" + imageSiteRoot + "/ClientScript/jqmojo/jquery.tools.min.js'></script>";
            //Page.ClientScript.RegisterStartupScript(typeof(Page), "jquerytool", script);
        }

        protected virtual void SetupRssLink()
        {
            if (WebConfigSettings.DisableBlogRssMetaLink) { return; }
            if (module == null || Page.Master == null) return;
            Control head = Page.Master.FindControl("Head1");
            if (head == null) return;
            Literal rssLink = new Literal
            {
                Text = @"<link rel=""alternate"" type=""application/rss+xml"" title="""
                       + module.ModuleTitle + @""" href="""
                       + GetRssUrl() + @""" />"
            };
            head.Controls.Add(rssLink);
        }


        protected string FormartDateTime(object startDate)
        {
            if (startDate != null)
            {
                return string.Format("{0:dddd - dd/MM/yyyy}", startDate);
            }
            return string.Empty;
        }

        protected string GetIconDocument(string pathFile)
        {
            var result = string.Empty;
            if (!string.IsNullOrEmpty(pathFile))
            {
                var getExtension = pathFile.Split('.').LastOrDefault().ToLower();
                switch (getExtension)
                {
                    case "pdf":
                        result = "pdf.png";
                        break;
                    case "docx":
                        result = "word.png";
                        break;
                    case "doc":
                        result = "word.png";
                        break;
                    default:
                        return "/data/images/word.png";
                }
                return "/data/images/" + result;
            }
            return "/data/images/word.png";
        }

    }

}
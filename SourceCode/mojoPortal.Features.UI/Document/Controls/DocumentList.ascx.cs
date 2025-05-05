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
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DocumentFeature.UI
{
    public partial class DocumentList : UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;

        private mojoBasePage basePage;
        private Module module;
        protected DocumentConfiguration config = new DocumentConfiguration();

        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        public SiteSettings siteSettings;
        private int linhVucId = -1;
        private int loaiVb = -1;
        private int coQuanId = -1;
        private int namBanHanh = -1;
        private int chuDe = -1;
        protected int langId = 1;
        private string keyword = string.Empty;
        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkText = SwirlingQuestionResource.ButtonDelete;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkText = SwirlingQuestionResource.ButtonEdit;
        protected string EditLinkImageUrl = string.Empty;
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        readonly SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
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
        public int LinhVucId
        {
            get { return linhVucId; }
            set { linhVucId = value; }
        }
        public int LoaiVb
        {
            get { return loaiVb; }
            set { loaiVb = value; }
        }
        public int CoQuanId
        {
            get { return coQuanId; }
            set { coQuanId = value; }
        }
        public int NamBanHanh
        {
            get { return namBanHanh; }
            set { namBanHanh = value; }
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

        public DocumentConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }

        public string ImageSiteRoot
        {
            get { return imageSiteRoot; }
            set { imageSiteRoot = value; }
        }

        public bool IsEditable { get; set; }
        #endregion

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            btnSearch.Click += btnSearch_Click;
            //ddlLinhVuc.SelectedIndexChanged += new EventHandler(ddlLinhVuc_SelectedIndexChanged);
            //ddlLoaiVB.SelectedIndexChanged += new EventHandler(ddlLoaiVB_SelectedIndexChanged);
            //ddlCoQuan.SelectedIndexChanged += new EventHandler(ddlCoQuan_SelectedIndexChanged);
            //ddlNam.SelectedIndexChanged += new EventHandler(ddlNam_SelectedIndexChanged);
            btnReset.Click += btnReset_Click;
            //EnableViewState = false;
        }

        void btnReset_Click(object sender, EventArgs e)
        {
            SiteUtils.RedirectToUrl(SiteRoot + "/Document/ViewPost.aspx?pageid=" + PageId + "&mid=" + ModuleId);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var lang = CultureInfo.CurrentCulture.Name;
            langId = lang == "vi-VN" ? LanguageConstant.VN : LanguageConstant.EN;
            LoadSettings();
            PopulateLabels();
            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }

        private void PopulateLabels()
        {
            heading.Text = "Văn bản quản lý";
            btnSearch.Text = DocumentResources.SearchButton;
            btnReset.Text = DocumentResources.ButtonReset;
            //ToDo?
            //ValidateDeleteAll();
            UIHelper.DisableButtonAfterClick(
                btnSearch,
                ArticleResources.ButtonDisabledPleaseWait,
                Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty)
                );
        }

        private void PopulateControls()
        {
            bindLinhVucVanBan();
            BindDocument();
            bindLoaiVB();
            bindCoQuan();
            bindNam();
            if (linhVucId > 0)
            {
                ddlLinhVuc.SelectedValue = linhVucId.ToString();
            }
            if (loaiVb > 0)
            {
                ddlLoaiVB.SelectedValue = loaiVb.ToString();
            }
            if (coQuanId > 0)
            {
                ddlCoQuan.SelectedValue = coQuanId.ToString();
            }
            if (namBanHanh > 0)
            {
                ddlNam.SelectedValue = namBanHanh.ToString();
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                txtKeyword.Text = keyword;
            }
        }

        private void BindDocument()
        {
            int totalVanBan = 0;
            List<Documentation> reader = new List<Documentation>();
            if (siteSetting.SiteId == 1)
            {
                reader = Documentation.GetPageAll(0, 0, linhVucId, loaiVb, coQuanId, pageNumber, config.PageSize, 1, namBanHanh, chuDe, keyword.ConvertToVN(), config.NhomVanBanSetting, out totalPages, out totalVanBan);
            }
            else
            {
                reader = Documentation.GetPage(siteSettings.SiteId, moduleId, linhVucId, loaiVb, coQuanId, pageNumber, config.PageSize, 1, namBanHanh, chuDe, keyword.ConvertToVN(), out totalPages, out totalVanBan);
            }
            rptArticles.DataSource = reader;
            rptArticles.DataBind();
            string pageUrl = SiteRoot + "/document/viewpost.aspx"
                   + "?pageid=" + PageId.ToInvariantString()
                   + "&mid=" + ModuleId.ToInvariantString()
                   + "&linhvucid=" + linhVucId.ToInvariantString()
                   + "&loaivb=" + loaiVb.ToInvariantString()
                   + "&coquan=" + coQuanId.ToInvariantString()
                   + "&nam=" + namBanHanh.ToInvariantString()
                   + "&chude=" + chuDe.ToInvariantString()
                   + "&keyword=" + keyword
                   + "&pagenumber={0}";

            pgrArticle.PageURLFormat = pageUrl;
            pgrArticle.ShowFirstLast = true;
            pgrArticle.PageSize = config.PageSize;
            pgrArticle.PageCount = totalPages;
            pgrArticle.CurrentIndex = pageNumber;
            pnlArticlePager.Visible = (totalPages > 1) && config.ShowPager;
            lblTotalVanBan.Text = string.Format("Tổng số văn bản <span class='red'>{0}</span> ", totalVanBan);
        }
        private void BindListLinhVuc()
        {
            //string name = string.Empty;
            //List<CategoryDocument> dt = new List<CategoryDocument>();
            //int count = 0;
            //List<CoreCategory> linhVuc = CoreCategory.GetChildren(siteSetting.SiteId, siteSetting.CoreLinhVucVanBanQuyPham);
            //if (linhVuc != null && linhVuc.Count > 0)
            //{
            //    foreach (var lv in linhVuc)
            //    {
            //        CategoryDocument item = new CategoryDocument();
            //        count = Document.GetCountByLinhVuc(siteSettings.SiteId, moduleId, lv.ItemID);
            //        name = lv.Name;
            //        item.Name = name;
            //        item.Url = SiteRoot + "/Document/ViewPost.aspx?pageid=" + PageId + "&mid=" + ModuleId + "&linhvucid=" + lv.ItemID;
            //        item.Count = "(" + count + ")";
            //        dt.Add(item);
            //    }
            //}
            //var dtlinhvuc = dt.OrderBy(o => o.Name).ToList();
            //rptCountLinhVuc.DataSource = dtlinhvuc;
            //rptCountLinhVuc.DataBind();
        }
        private void bindLinhVucVanBan()
        {
            //get all categories
            int CategoryConfig = siteSetting.CoreLinhVucVanBanQuyPham;
            //List<CoreCategory> lstLoai = CoreCategory.GetChildren(siteSetting.SiteId, CategoryConfig);
            List<CoreCategory> lstLoai = CoreCategory.GetChildren(1, WebConfigSettings.DM_LinhVucVanBan);
            ddlLinhVuc.DataValueField = "ItemID";
            ddlLinhVuc.DataTextField = "Name";
            ddlLinhVuc.DataSource = lstLoai;
            ddlLinhVuc.DataBind();
            ddlLinhVuc.Items.Insert(0, new ListItem("--Chọn lĩnh vực văn bản--", "0"));
        }
        private void bindLoaiVB()
        {
            //get all categories
            int CategoryConfig = siteSetting.CoreLoaiVanBanQuyPham;
            //List<CoreCategory> lstLoai = CoreCategory.GetChildren(siteSetting.SiteId, CategoryConfig);
            List<CoreCategory> lstLoai = CoreCategory.GetChildren(1, WebConfigSettings.DM_LoaiVanBan);
            ddlLoaiVB.DataValueField = "ItemID";
            ddlLoaiVB.DataTextField = "Name";
            ddlLoaiVB.DataSource = lstLoai;
            ddlLoaiVB.DataBind();
            ddlLoaiVB.Items.Insert(0, new ListItem("--" + DocumentResources.ChooseTypeDocumentLabel + "--", "0"));
        }
        private void bindCoQuan()
        {
            //get all categories
            int CategoryConfig = siteSetting.CoreCoQuanBanHanhVanBanQuyPham;
            //List<CoreCategory> lstCoQuan = CoreCategory.GetChildren(siteSetting.SiteId, CategoryConfig);
            List<CoreCategory> lstLoai = CoreCategory.GetChildren(1, WebConfigSettings.DM_CoQuanBanHanh);
            ddlCoQuan.DataValueField = "ItemID";
            ddlCoQuan.DataTextField = "Name";
            ddlCoQuan.DataSource = lstLoai;
            ddlCoQuan.DataBind();
            ddlCoQuan.Items.Insert(0, new ListItem("--" + DocumentResources.AgencyLabel + "--", "0"));
        }

        private void bindNam()
        {
            List<DocumentYear> dy = new List<DocumentYear>();
            List<Documentation> document = Documentation.GetAllByYear(siteSetting.SiteId);
            foreach (var doc in document)
            {
                DocumentYear item = new DocumentYear();
                item.Name = doc.YearPromulgate.ToString();
                item.Key = doc.YearPromulgate;
                dy.Add(item);
            }
            var distinDY = dy.GroupBy(d => d.Key).Select(y => y.First());
            ddlNam.DataValueField = "Key";
            ddlNam.DataTextField = "Name";
            ddlNam.DataSource = distinDY;
            ddlNam.DataBind();
            ddlNam.Items.Insert(0, new ListItem("--" + Resources.DocumentResources.AllLabel + "--", "0"));
        }
        protected virtual void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new DocumentConfiguration(getModuleSettings);

            siteSettings = CacheHelper.GetCurrentSiteSettings();
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            linhVucId = WebUtils.ParseInt32FromQueryString("linhvucid", linhVucId);
            loaiVb = WebUtils.ParseInt32FromQueryString("loaivb", loaiVb);
            coQuanId = WebUtils.ParseInt32FromQueryString("coquan", coQuanId);
            namBanHanh = WebUtils.ParseInt32FromQueryString("nam", namBanHanh);
            chuDe = WebUtils.ParseInt32FromQueryString("chude", chuDe);
            keyword = WebUtils.ParseStringFromQueryString("keyword", keyword);

            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
        }


        protected string formatContent(string Content)
        {
            if (!string.IsNullOrEmpty(Content))
            {
                return Regex.Replace(Content, "<(.|\n)*?>", string.Empty);
            }
            else
            {
                return string.Empty;
            }
        }
        protected void Search()
        {
            loaiVb = string.IsNullOrEmpty(ddlLoaiVB.SelectedValue) ? -1 : int.Parse(ddlLoaiVB.SelectedValue);
            coQuanId = string.IsNullOrEmpty(ddlCoQuan.SelectedValue) ? -1 : int.Parse(ddlCoQuan.SelectedValue);
            namBanHanh = string.IsNullOrEmpty(ddlNam.SelectedValue) ? -1 : int.Parse(ddlNam.SelectedValue);
            linhVucId = string.IsNullOrEmpty(ddlLinhVuc.SelectedValue) ? -1 : int.Parse(ddlLinhVuc.SelectedValue);
            keyword = txtKeyword.Text;
            string pageUrl = SiteRoot + "/document/viewpost.aspx"
                    + "?pageid=" + PageId.ToInvariantString()
                    + "&mid=" + ModuleId.ToInvariantString()
                    + "&linhvucid=" + linhVucId.ToInvariantString()
                   + "&loaivb=" + loaiVb.ToInvariantString()
                   + "&coquan=" + coQuanId.ToInvariantString()
                    + "&nam=" + namBanHanh.ToInvariantString()
                    + "&keyword=" + keyword
                    + "&pagenumber=1";
            WebUtils.SetupRedirect(this, pageUrl);
        }
        protected virtual void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }
        protected void ddlNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }
        protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }
        protected void ddlLoaiVB_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }
        protected void ddlCoQuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }
        protected string FormatArticleDate(DateTime datePromulgate)
        {
            if (config.DateTimeFormat == string.Empty) return string.Empty;
            return datePromulgate.ToString(config.DateTimeFormat);
        }


        protected string DownloadFile(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                return SiteRoot + "/" + ConfigurationManager.AppSettings["DocumentFileFolder"] + filePath;
            }
            return string.Empty;
        }

    }
}
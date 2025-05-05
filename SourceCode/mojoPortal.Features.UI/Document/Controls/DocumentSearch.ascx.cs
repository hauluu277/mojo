using ArticleFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DocumentFeature.UI
{
    public partial class DocumentSearch : UserControl
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
        private SiteSettings siteSettings;
        private int linhVucId = -1;
        private int loaiVb = -1;
        private int coQuanId = -1;
        private int namBanHanh = -1;
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
            ddlLinhVuc.SelectedIndexChanged += new EventHandler(ddlLinhVuc_SelectedIndexChanged);
            ddlCoQuan.SelectedIndexChanged += new EventHandler(ddlCoQuan_SelectedIndexChanged);
            ddlNam.SelectedIndexChanged += new EventHandler(ddlNam_SelectedIndexChanged);
            //EnableViewState = false;
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
            legendSearchProperty.InnerText = DocumentResources.BoxSearchLabel;
            btnSearch.Text = DocumentResources.SearchButton;

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
            BindListLinhVuc();
            bindLinhVuc();
            bindCoQuan();
            bindNam();
            if (linhVucId > 0)
            {
                ddlLinhVuc.SelectedValue = linhVucId.ToString();
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
        private void BindListLinhVuc()
        {
            string name = string.Empty;
            List<CategoryDocument> dt = new List<CategoryDocument>();
            int count = 0;
            List<LinhVuc> linhVuc = LinhVuc.GetAll(siteSetting.SiteId);
            foreach (var lv in linhVuc)
            {
                CategoryDocument item = new CategoryDocument();
                count = Documentation.GetCountByLinhVuc(siteSettings.SiteId, moduleId, lv.ItemID);
                name = lv.Name;
                item.Name = name;
                item.Url = SiteRoot + "/Document/ViewPost.aspx?pageid=" + PageId + "&mid=" + ModuleId + "&linhvucid=" + lv.ItemID;
                item.Count = "(" + count + ")";
                dt.Add(item);
            }
            var dtlinhvuc = dt.OrderBy(o => o.Name).ToList();
            rptCountLinhVuc.DataSource = dtlinhvuc;
            rptCountLinhVuc.DataBind();
        }
        private void bindLinhVuc()
        {
            List<LinhVuc> linhvuc = LinhVuc.GetAll(siteSetting.SiteId);
            ddlLinhVuc.DataValueField = "ItemID";
            if (langId == LanguageConstant.VN)
            {
                ddlLinhVuc.DataTextField = "Name";
            }
            else
            {
                ddlLinhVuc.DataTextField = "NameEN";
            }
            var lv = linhvuc.OrderBy(o => o.Name).ToList();
            ddlLinhVuc.DataSource = lv;
            ddlLinhVuc.DataBind();
            ddlLinhVuc.Items.Insert(0, new ListItem("--" + Resources.DocumentResources.ChooseFieldLabel + "--", "0"));
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
        private void bindCoQuan()
        {
            List<CoQuan> cq = CoQuan.GetAll(siteSetting.SiteId);
            ddlCoQuan.DataValueField = "ItemID";
            if (langId == LanguageConstant.VN)
            {
                ddlCoQuan.DataTextField = "Name";
            }
            else
            {
                ddlCoQuan.DataTextField = "NameEN";
            }
            var coquan = cq.OrderBy(o => o.Name).ToList();
            ddlCoQuan.DataSource = coquan;
            ddlCoQuan.DataBind();
            ddlCoQuan.Items.Insert(0, new ListItem("--" + Resources.DocumentResources.AgencyLabel + "--", "0"));
        }
        protected virtual void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new DocumentConfiguration(getModuleSettings);
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
        }
        protected void Search()
        {
            linhVucId = string.IsNullOrEmpty(ddlLinhVuc.SelectedValue) ? -1 : int.Parse(ddlLinhVuc.SelectedValue);
            coQuanId = string.IsNullOrEmpty(ddlCoQuan.SelectedValue) ? -1 : int.Parse(ddlCoQuan.SelectedValue);
            namBanHanh = string.IsNullOrEmpty(ddlNam.SelectedValue) ? -1 : int.Parse(ddlNam.SelectedValue);
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
        protected void ddlLinhVuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }
        protected void ddlCoQuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }

    }
}
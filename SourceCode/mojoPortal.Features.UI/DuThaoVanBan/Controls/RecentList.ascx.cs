using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DuThaoVanBanFeature.UI
{
    public partial class RecentList : UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int pageNumberEx = 1;
        private int totalPages = 1;

        private mojoBasePage basePage;
        private Module module;
        protected DuThaoVanBanConfiguration config = new DuThaoVanBanConfiguration();

        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        private SiteSettings siteSettings;
        private int linhVucId = -1;
        private int coQuanBHId = -1;
        private int loaiVb = -1;
        protected string seeCommentLabel = DuThaoVanBanResources.SeeCommentLabel;
        protected string createCommentLabel = DuThaoVanBanResources.CreateCommentLabel;
        protected string CommentLabel = DuThaoVanBanResources.CommentHeaderLabel;
        protected string StartDateLabel = DuThaoVanBanResources.DateStartLabel;
        protected string EndDateLabel = DuThaoVanBanResources.DateExpiresLabel;
        private string keyword = string.Empty;
        protected string downLoadLabel = DuThaoVanBanResources.DownloadLabel;
        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkText = SwirlingQuestionResource.ButtonDelete;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkText = SwirlingQuestionResource.ButtonEdit;
        protected string EditLinkImageUrl = string.Empty;
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        protected int i = 1;
        protected int j = 1;
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

        public DuThaoVanBanConfiguration Config
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
            btnSearch.ServerClick += btnSearch_Click;
            //EnableViewState = false;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            linhVucId = string.IsNullOrEmpty(ddlLinhVuc.SelectedValue) ? -1 : int.Parse(ddlLinhVuc.SelectedValue);
            loaiVb = string.IsNullOrEmpty(ddlLoaiVanBan.SelectedValue) ? -1 : int.Parse(ddlLoaiVanBan.SelectedValue);
            coQuanBHId = string.IsNullOrEmpty(ddlCoQuanBanHanh.SelectedValue) ? -1 : int.Parse(ddlCoQuanBanHanh.SelectedValue);
            keyword = txtKeyword.Value;

            string pageUrl = SiteRoot + "/duthaovanban/viewlist.aspx"
                    + "?pageid=" + PageId.ToInvariantString()
                    + "&mid=" + ModuleId.ToInvariantString()
                    + "&linhvucid=" + linhVucId.ToInvariantString()
                    + "&loaivb=" + loaiVb.ToInvariantString()
                    + "&kw=" + keyword
                    + "&cqbh=" + coQuanBHId
                    + "&pagenumber=1";
            WebUtils.SetupRedirect(this, pageUrl);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSettings();
            PopulateLabels();
            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }

        private void PopulateLabels()
        {
        }

        private void PopulateControls()
        {
            bindCoQuanBanHanh();
            bindLinhVuc();
            bindLoaiVB();
            txtKeyword.Value = keyword;
            BindDuThao();
            BindDuThaoHetHan();
        }
        private void bindLoaiVB()
        {
            //get all categories
            List<CoreCategory> lstLoai = CoreCategory.GetChildren(1, WebConfigSettings.DM_LoaiVanBan);
            ddlLoaiVanBan.DataValueField = "ItemID";
            ddlLoaiVanBan.DataTextField = "Name";
            ddlLoaiVanBan.DataSource = lstLoai;
            ddlLoaiVanBan.DataBind();
            ddlLoaiVanBan.Items.Insert(0, new ListItem("Tất cả", "0"));
            if (LoaiVb > 0)
            {
                ddlLoaiVanBan.SelectedValue = loaiVb.ToString();
            }
        }
        private void bindLinhVuc()
        {
            //get all categories
            List<CoreCategory> lstLinhVuc = CoreCategory.GetChildren(1, WebConfigSettings.DM_LinhVucVanBan);
            ddlLinhVuc.DataValueField = "ItemID";
            ddlLinhVuc.DataTextField = "Name";
            ddlLinhVuc.DataSource = lstLinhVuc;
            ddlLinhVuc.DataBind();
            ddlLinhVuc.Items.Insert(0, new ListItem("Tất cả", "0"));

            if (linhVucId > 0)
            {
                ddlLinhVuc.SelectedValue = linhVucId.ToString();
            }
        }

        private void bindCoQuanBanHanh()
        {
            //get all categories
            List<CoreCategory> lstLinhVuc = CoreCategory.GetChildren(1, WebConfigSettings.DM_CoQuanBanHanh);
            ddlCoQuanBanHanh.DataValueField = "ItemID";
            ddlCoQuanBanHanh.DataTextField = "Name";
            ddlCoQuanBanHanh.DataSource = lstLinhVuc;
            ddlCoQuanBanHanh.DataBind();
            ddlCoQuanBanHanh.Items.Insert(0, new ListItem("Tất cả", "0"));

            if (coQuanBHId > 0)
            {
                ddlCoQuanBanHanh.SelectedValue = coQuanBHId.ToString();
            }
        }

        private void BindDuThaoHetHan()
        {
            var searchKW = keyword.ConvertToFTS();
            List<DuThaoVanBan> lstDraftExpire = DuThaoVanBan.GetPage(siteSettings.SiteId, moduleId, linhVucId, loaiVb, searchKW, true, 1, DateTime.Now, pageNumberEx, config.PageSize, out totalPages);
            rptDraftExpires.DataSource = lstDraftExpire;
            rptDraftExpires.DataBind();
            string pageUrl = SiteRoot + "/DuThaoVanBan/viewlist.aspx"
               + "?pageid=" + PageId.ToInvariantString()
               + "&mid=" + ModuleId.ToInvariantString()
                            + "&linhvucid=" + linhVucId.ToInvariantString()
                    + "&loaivb=" + loaiVb.ToInvariantString()
                    + "&kw=" + keyword
                    + "&cqbh=" + coQuanBHId
               + "&pagenumberEx={0}";
            pgrDuThaoEx.PageURLFormat = pageUrl;
            pgrDuThaoEx.ShowFirstLast = true;
            pgrDuThaoEx.PageSize = config.PageSize;
            pgrDuThaoEx.PageCount = totalPages;
            pgrDuThaoEx.CurrentIndex = pageNumberEx;
            pnDuThaoEx.Visible = (totalPages > 1) && config.ShowPager;
        }
        private void BindDuThao()
        {
            //List<DuThaoVanBan> lstDraft = new List<DuThaoVanBan>();
            //List<DuThaoVanBan> lstDraftExpire = new List<DuThaoVanBan>();

            //foreach (var item in reader)
            //{
            //    if (!string.IsNullOrEmpty(item.EndDate.ToString()) && item.EndDate <= DateTime.Now)
            //    {
            //        lstDraftExpire.Add(item);
            //    }
            //    else
            //    {
            //        lstDraft.Add(item);
            //    }
            //}
            var searchKeyword = keyword.ConvertToFTS();
            List<DuThaoVanBan> lstDraft = DuThaoVanBan.GetPage(siteSettings.SiteId, moduleId, linhVucId, loaiVb, searchKeyword, true, 0, DateTime.Now, pageNumber, config.PageSize, out totalPages);
            rptDuThao.DataSource = lstDraft.OrderBy(x => x.StartDate).ToList();
            rptDuThao.DataBind();
            string pageUrl = SiteRoot + "/DuThaoVanBan/viewlist.aspx"
               + "?pageid=" + PageId.ToInvariantString()
               + "&mid=" + ModuleId.ToInvariantString()
              + "&linhvucid=" + linhVucId.ToInvariantString()
                    + "&loaivb=" + loaiVb.ToInvariantString()
                    + "&kw=" + keyword
                    + "&cqbh=" + coQuanBHId
               + "&pagenumber={0}";

            pgrArticle.PageURLFormat = pageUrl;
            pgrArticle.ShowFirstLast = true;
            pgrArticle.PageSize = config.PageSize;
            pgrArticle.PageCount = totalPages;
            pgrArticle.CurrentIndex = pageNumber;
            pnlDuThaoPager.Visible = (totalPages > 1) && config.ShowPager;
        }
        protected List<FileDuThao> PopulateFile(int itemID)
        {
            i = 1;
            j = 1;
            List<FileDuThao> lstFile = new List<FileDuThao>();
            lstFile = FileDuThao.GetAllByDuThaoId(itemID);
            return lstFile;
        }
        protected virtual void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new DuThaoVanBanConfiguration(getModuleSettings);

            siteSettings = CacheHelper.GetCurrentSiteSettings();
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            pageNumberEx = WebUtils.ParseInt32FromQueryString("pagenumberEx", pageNumberEx);

            linhVucId = WebUtils.ParseInt32FromQueryString("linhvucid", linhVucId);
            loaiVb = WebUtils.ParseInt32FromQueryString("loaivb", loaiVb);
            coQuanBHId = WebUtils.ParseInt32FromQueryString("cqbh", coQuanBHId);

            keyword = WebUtils.ParseStringFromQueryString("kw", keyword);

            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
        }

        protected string BuildEditUrl(int itemID)
        {
            return SiteRoot + "/DuThaoVanBan/EditPost.aspx?pageid=" + pageId + "&item=" +
                           itemID + "&mid=" + moduleId;
        }
        protected string formatContent(string Content)
        {
            string _Content = string.Empty;
            if (!string.IsNullOrEmpty(Content))
            {
                _Content = Regex.Replace(Content, "<(.|\n)*?>", string.Empty);
                return DuThaoVanBanUltils.Truncate(_Content, config.NumberCharacter);
            }
            else
            {
                return string.Empty;
            }
        }
        protected string FormatArticleDate(DateTime datePromulgate)
        {
            if (config.DateTimeFormat == string.Empty) return string.Empty;
            return datePromulgate.ToString(config.DateTimeFormat);
        }

    }
}
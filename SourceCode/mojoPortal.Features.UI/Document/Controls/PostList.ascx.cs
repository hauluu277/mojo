using ArticleFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Features.Business;
using mojoPortal.Features.Business.QLLog;
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

namespace DocumentFeature.UI
{
    public partial class PostList : UserControl
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
        private int chuDe = -1;
        private int? isApprove = -1;
        private int namBanHanh = -1;
        private string keyword = string.Empty;
        private string _status = string.Empty;
        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkText = SwirlingQuestionResource.ButtonDelete;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkText = SwirlingQuestionResource.ButtonEdit;
        protected string EditLinkImageUrl = string.Empty;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        public SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
        protected int langId = 1;
        protected int langRefer = 1;
        protected string StateLink = DuThaoVanBanResources.ChangeStatus;
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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            btnSearch.Click += btnSearch_Click;
            btnaddnew.Click += btnaddnew_Click;
            btnDelAll.Click += btnDelAll_Click;
            //EnableViewState = false;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var lang = CultureInfo.CurrentCulture.Name;
            langId = lang == "vi-VN" ? LanguageConstant.VN : LanguageConstant.EN;
            LoadParams();
            LoadSettings();
            PopulateLabels();
            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }

        private void PopulateLabels()
        {
            legendSearchProperty.InnerText = ArticleResources.ArticleEditSearchPropertyLabel;
            btnSearch.Text = ArticleResources.ArticleSearchButton;
            btnDelAll.BackColor = System.Drawing.Color.Red;
            btnDelAll.Text = ArticleResources.ButtonDeleteAll;
            btnaddnew.Text = ArticleResources.ButtonAddNew;
            btnDelAll.OnClientClick = "return confirm(" + "'" + ArticleResources.DeleteAllConfirmWarning + "');";
            //ToDo?
            //ValidateDeleteAll();
            UIHelper.DisableButtonAfterClick(
                btnSearch,
                ArticleResources.ButtonDisabledPleaseWait,
                Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty)
                );
            UIHelper.DisableButtonAfterClick(
                btnDelAll,
                ArticleResources.ButtonDisabledPleaseWait,
                Page.ClientScript.GetPostBackEventReference(btnDelAll, string.Empty)
                );
        }

        private void PopulateControls()
        {
            if (langId == LanguageConstant.VN)
            {
                langRefer = LanguageConstant.EN;
            }
            else
            {
                langRefer = LanguageConstant.VN;
            }
            BindDocument();
            bindLinhVuc();
            bindLoaiVB();
            bindCoQuan();
            PopulateStatus();
            if (!string.IsNullOrEmpty(_status))
            {
                ddlStatus.SelectedValue = _status;
            }
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
            //if (sta >= 0)
            //{
            //    ddln.SelectedValue = namBanHanh.ToString();
            //}

            if (!string.IsNullOrEmpty(keyword))
            {
                txtKeyword.Text = keyword;
            }
        }

        private void BindDocument()
        {
            EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
            DeleteLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;
            int totalVanBan = 0;
            List<Documentation> reader = new List<Documentation>();
            if (siteSetting.SiteId == 1)
            {
                reader = Documentation.GetPageAll(siteSettings.SiteId, 0, linhVucId, loaiVb, coQuanId, pageNumber, config.PageSize, isApprove, namBanHanh, chuDe, keyword.ConvertToVN(), config.NhomVanBanSetting, out totalPages, out totalVanBan);
            }
            else
            {
                reader = Documentation.GetPage(siteSettings.SiteId, moduleId, linhVucId, loaiVb, coQuanId, pageNumber, config.PageSize, isApprove, namBanHanh, chuDe, keyword.ConvertToVN(), out totalPages, out totalVanBan);
            }
            rptArticles.DataSource = reader;
            rptArticles.DataBind();
            string pageUrl = SiteRoot + "/document/managepost.aspx"
               + "?pageid=" + PageId.ToInvariantString()
               + "&mid=" + ModuleId.ToInvariantString()
               + "&linhvucid=" + linhVucId.ToInvariantString()
               + "&loaivb=" + loaiVb.ToInvariantString()
               + "&coquan=" + coQuanId.ToInvariantString()
               + "&nam=" + namBanHanh.ToInvariantString()
               + "&chude=" + chuDe.ToInvariantString()
               + "&status=" + _status
               + "&keyword=" + keyword
               + "&pagenumber={0}";

            pgrArticle.PageURLFormat = pageUrl;
            pgrArticle.ShowFirstLast = true;
            pgrArticle.PageSize = config.PageSize;
            pgrArticle.PageCount = totalPages;
            pgrArticle.CurrentIndex = pageNumber;
            pnlArticlePager.Visible = (totalPages > 1) && config.ShowPager;
            literTotalVanBan.Text = string.Format("Tổng số <span class='red font-bold'>{0}</span> văn bản", totalVanBan);
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
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
            _status = WebUtils.ParseStringFromQueryString("status", _status);
            if (!string.IsNullOrEmpty(_status))
            {
                try
                {
                    isApprove = int.Parse(_status);

                }
                catch (Exception)
                {

                    isApprove = -1;
                }
            }
            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
        }
        //private void PopulateCategories()
        //{
        //    string listCategoryConfig = pageSettings.CategoryConfig;
        //    if (!string.IsNullOrEmpty(config.ArticleCategoryConfig))
        //        listCategoryConfig = config.ArticleCategoryConfig;
        //    var list_category_config = listCategoryConfig.Split('-');
        //    CoreCategory coreCategory = new CoreCategory();
        //    foreach (var item in list_category_config)
        //    {
        //        if (!string.IsNullOrEmpty(item))
        //        {
        //            coreCategory = new CoreCategory(int.Parse(item));
        //            ListItem list = new ListItem
        //            {
        //                Text = coreCategory.Name,
        //                Value = item
        //            };
        //            ddlCategories.Items.Add(list);
        //        }
        //    }
        //    PopulateChildNode(ddlCategories);
        //    ddlCategories.Items.Insert(0, new ListItem(ArticleResources.ParentCategoryChoose, "0"));
        //}

        //private void PopulateChildNode(ListControl root)
        //{
        //    for (int i = 0; i < root.Items.Count; i++)
        //    {
        //        List<CoreCategory> children = CoreCategory.GetChildren(int.Parse(root.Items[i].Value));
        //        if (children.Count <= 0) continue;
        //        string prefix = string.Empty;
        //        while (root.Items[i].Text.StartsWith("|"))
        //        {
        //            prefix += root.Items[i].Text.Substring(0, 3);
        //            root.Items[i].Text = root.Items[i].Text.Remove(0, 3);
        //        }
        //        root.Items[i].Text = prefix + root.Items[i].Text;
        //        int index = 1;
        //        foreach (CoreCategory child in children)
        //        {
        //            ListItem list = new ListItem
        //            {
        //                Text = prefix + @"|--" + child.Name,
        //                Value = child.ItemID.ToString()
        //            };
        //            root.Items.Insert(root.Items.IndexOf(root.Items[i]) + index, list);
        //            index++;
        //        }
        //    }
        //}

        private void PopulateStatus()
        {
            var status = SiteUtils.StringToDictionary(ArticleResources.ArticleStatus.ToString(), ",");

            ddlStatus.DataSource = status;
            ddlStatus.DataTextField = "Value";
            ddlStatus.DataValueField = "Key";
            ddlStatus.DataBind();
        }
        private void bindLoaiVB()
        {
            //get all categories
            //int CategoryConfig = siteSetting.CoreLoaiVanBanQuyPham;
            //List<CoreCategory> lstLoai = CoreCategory.GetChildren(siteSetting.SiteId, CategoryConfig);
            List<CoreCategory> lstLoai = CoreCategory.GetChildren(1, WebConfigSettings.DM_LoaiVanBan);
            ddlLoaiVB.DataValueField = "ItemID";
            ddlLoaiVB.DataTextField = "Name";
            ddlLoaiVB.DataSource = lstLoai;
            ddlLoaiVB.DataBind();
            ddlLoaiVB.Items.Insert(0, new ListItem("--" + DocumentResources.ChooseTypeDocumentLabel + "--", "0"));
        }
        private void bindLinhVuc()
        {
            //get all categories
            //int CategoryConfig = siteSetting.CoreLinhVucVanBanQuyPham;
            //List<CoreCategory> lstLinhVuc = CoreCategory.GetChildren(siteSetting.SiteId, CategoryConfig);
            List<CoreCategory> lstLinhVuc = CoreCategory.GetChildren(1, WebConfigSettings.DM_LinhVucVanBan);
            ddlLinhVuc.DataValueField = "ItemID";
            ddlLinhVuc.DataTextField = "Name";
            ddlLinhVuc.DataSource = lstLinhVuc;
            ddlLinhVuc.DataBind();
            ddlLinhVuc.Items.Insert(0, new ListItem("--" + DocumentResources.ChooseFieldLabel + "--", "0"));
        }
        private void bindCoQuan()
        {
            //get all categories
            //int CategoryConfig = siteSetting.CoreCoQuanBanHanhVanBanQuyPham;
            //List<CoreCategory> lstCoQuan = CoreCategory.GetChildren(siteSetting.SiteId, CategoryConfig);
            List<CoreCategory> lstCoQuan = CoreCategory.GetChildren(1, WebConfigSettings.DM_CoQuanBanHanh);
            ddlCoQuan.DataValueField = "ItemID";
            ddlCoQuan.DataTextField = "Name";
            ddlCoQuan.DataSource = lstCoQuan;
            ddlCoQuan.DataBind();
            ddlCoQuan.Items.Insert(0, new ListItem("--" + DocumentResources.AgencyLabel + "--", "0"));
        }

        protected string ImgLanguage()
        {
            string Imgurl = string.Empty;

            if (langId == LanguageConstant.VN)
            {
                Imgurl = "~/Data/SiteImages/flags/en.gif";
            }
            else
            {
                Imgurl = "~/Data/SiteImages/flags/vi.gif";
            }
            return Imgurl;
        }
        protected bool ShowLanguge(int rootItem)
        {
            bool show = true;
            List<DocumentReference> docRefer = new List<DocumentReference>();
            docRefer = DocumentReference.GetDocumentByRootId(rootItem);
            if (docRefer != null && docRefer.Count > 0)
            {
                show = false;
            }
            return show;
        }
        protected virtual void btnSearch_Click(object sender, EventArgs e)
        {
            linhVucId = string.IsNullOrEmpty(ddlLinhVuc.SelectedValue) ? -1 : int.Parse(ddlLinhVuc.SelectedValue);
            loaiVb = string.IsNullOrEmpty(ddlLoaiVB.SelectedValue) ? -1 : int.Parse(ddlLoaiVB.SelectedValue);
            coQuanId = string.IsNullOrEmpty(ddlCoQuan.SelectedValue) ? -1 : int.Parse(ddlCoQuan.SelectedValue);
            _status = ddlStatus.SelectedValue;
            //namBanHanh = string.IsNullOrEmpty(ddlNam.SelectedValue) ? -1 : int.Parse(ddlNam.SelectedValue);
            keyword = txtKeyword.Text;

            string pageUrl = SiteRoot + "/document/managepost.aspx"
                    + "?pageid=" + PageId.ToInvariantString()
                    + "&mid=" + ModuleId.ToInvariantString()
                    + "&linhvucid=" + linhVucId.ToInvariantString()
                    + "&loaivb=" + loaiVb.ToInvariantString()
                    + "&coquan=" + coQuanId.ToInvariantString()
                    + "&nam=" + namBanHanh.ToInvariantString()
                    + "&chude=" + chuDe.ToInvariantString()
                    + "&status=" + _status
                    + "&keyword=" + keyword
                    + "&pagenumber=1";
            WebUtils.SetupRedirect(this, pageUrl);
        }

        protected void rptArticles_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;

            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName.Equals("EditItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    WebUtils.SetupRedirect(this, SiteRoot + "/document/editpost.aspx?pageid=" + PageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString() + "&item=" + itemId);
                }
                else if (e.CommandName.Equals("DeleteItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    Documentation.Delete(itemId);
                    saveLog(itemId);
                    WebUtils.SetupRedirect(this, SiteRoot + "/document/managepost.aspx?pageid="
                        + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString());
                }
                else if (e.CommandName.Equals("EditStateItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    Documentation doc = new Documentation(itemId);
                    if (doc != null)
                    {
                        if (doc.IsApproved == true)
                        {
                            doc.IsApproved = false;
                            doc.Save();
                        }
                        else
                        {
                            doc.IsApproved = true;
                            doc.Save();
                        }
                    }
                    WebUtils.SetupRedirect(this, SiteRoot + "/document/managepost.aspx?pageid="
                        + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString());
                }
            }
        }

        protected void rptArticles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;
            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                ImageButton ibtnChangeState = e.Item.FindControl("ibtnChangeState") as ImageButton;
                SiteUtils.AddConfirmButton(ibtnChangeState, DocumentResources.ChangeStateWarning);
                ImageButton ibtnDelete = e.Item.FindControl("ibtnDelete") as ImageButton;
                SiteUtils.AddConfirmButton(ibtnDelete, "Dữ liệu này sẽ bị xóa, bạn có chắc chắn muốn tiếp tục?");
            }
        }

        protected void btnaddnew_Click(object sender, EventArgs e)
        {
            WebUtils.SetupRedirect(this, SiteRoot + "/document/editpost.aspx?pageid=" + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString());

        }
        protected void btnDelAll_Click(object sender, EventArgs e)
        {
            int deleteNumber = 0;
            foreach (RepeaterItem ri in rptArticles.Items)
            {
                CheckBox chkFlag = ri.FindControl("chk") as CheckBox;
                if (chkFlag.Checked)
                {
                    deleteNumber++;
                    int itemid = Convert.ToInt32((ri.FindControl("repeaterID") as Literal).Text);
                    Documentation.Delete(itemid);
                    saveLog(itemid);
                }
            }
            if (deleteNumber > 0)
            {
                _status = ddlStatus.SelectedValue;
                if (!string.IsNullOrEmpty(_status))
                {
                    try
                    {
                        isApprove = int.Parse(_status);

                    }
                    catch (Exception)
                    {

                        isApprove = -1;
                    }
                }
                string pageUrl = SiteRoot + "/document/managepost.aspx"
                        + "?pageid=" + PageId.ToInvariantString()
                        + "&mid=" + ModuleId.ToInvariantString()
                        + "&linhvucid=" + linhVucId.ToInvariantString()
                        + "&loaivb=" + loaiVb.ToInvariantString()
                        + "&coquan=" + coQuanId.ToInvariantString()
                        + "&nam=" + namBanHanh.ToInvariantString()
                        + "&status=" + _status
                        + "&keyword=" + keyword
                        + "&pagenumber=1";
                WebUtils.SetupRedirect(this, pageUrl);
            }
        }

        protected string FormatArticleDate(DateTime startDate)
        {
            if (config.DateTimeFormat == string.Empty) return string.Empty;
            return startDate.ToString(config.DateTimeFormat);
        }

        private void saveLog(int ItemId)
        {
            QLLog qlLog = new QLLog()
            {
                DiaChiIP = Common.findMyIP().ToString(),
                Type = KieuLogConstant.LogQuanTriVanBan,
                DuongDanThaoTac = HttpContext.Current.Request.Url.PathAndQuery,
                HanhDongThaoTac = KieuLogConstant.XoaDuLieu,
                NoiDung = "Xóa văn bản Id: " + ItemId,
                CreatedBy = siteUser.Name,
                CreatedByUser = siteUser.UserId,
                CreatedDate = DateTime.Now
            };
            qlLog.Save();
        }

    }
}
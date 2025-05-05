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
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DuThaoVanBanFeature.UI
{
    public partial class PostList : UserControl
    {
        #region Properties
        private int pageNumber = 1;
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
        private int loaiVb = -1;
        private int coQuanId = -1;
        private bool? isApprove = null;
        private int namBanHanh = -1;
        private string keyword = string.Empty;
        private string status = string.Empty;

        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkText = SwirlingQuestionResource.ButtonDelete;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkText = SwirlingQuestionResource.ButtonEdit;
        protected string EditLinkImageUrl = string.Empty;
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        readonly SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
        protected int langId = 1;
        protected int langRefer = 1;
        protected bool isAdmin = false;
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
            if (!isAdmin)
            {
                pnBtnAdmin.Visible = false;
            }
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
            PopulateStatus();
            if (linhVucId > 0)
            {
                ddlLinhVuc.SelectedValue = linhVucId.ToString();
            }
            if (loaiVb > 0)
            {
                ddlLoaiVB.SelectedValue = loaiVb.ToString();
            }
            if (!string.IsNullOrEmpty(status))
            {
                ddlStatus.SelectedValue = status;
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                txtKeyword.Text = keyword;
            }
        }

        private void BindDocument()
        {
            
            EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
            DeleteLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;
            List<DuThaoVanBan> reader = DuThaoVanBan.GetPage(siteSettings.SiteId, moduleId, linhVucId, loaiVb, keyword.ConvertToVN(), isApprove, null, null, pageNumber, config.PageSize, out totalPages);
            rptArticles.DataSource = reader;
            rptArticles.DataBind();
            string pageUrl = SiteRoot + "/duthaovanban/managepost.aspx"
                   + "?pageid=" + PageId.ToInvariantString()
                   + "&mid=" + ModuleId.ToInvariantString()
                   + "&linhvucid=" + linhVucId.ToInvariantString()
                   + "&loaivb=" + loaiVb.ToInvariantString()
                   + "&coquan=" + coQuanId.ToInvariantString()
                   + "&status=" + status
                   + "&keyword=" + keyword
                   + "&pagenumber={0}";

            pgrArticle.PageURLFormat = pageUrl;
            pgrArticle.ShowFirstLast = true;
            pgrArticle.PageSize = config.PageSize;
            pgrArticle.PageCount = totalPages;
            pgrArticle.CurrentIndex = pageNumber;
            pnlArticlePager.Visible = (totalPages > 1) && config.ShowPager;
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
        }
        protected virtual void LoadSettings()
        {
            if ((WebUser.IsAdminOrContentAdmin) || (SiteUtils.UserIsSiteEditor())) { isAdmin = true; }
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new DuThaoVanBanConfiguration(getModuleSettings);

            siteSettings = CacheHelper.GetCurrentSiteSettings();
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            linhVucId = WebUtils.ParseInt32FromQueryString("linhvucid", linhVucId);
            loaiVb = WebUtils.ParseInt32FromQueryString("loaivb", loaiVb);
            keyword = WebUtils.ParseStringFromQueryString("keyword", keyword);
            status = WebUtils.ParseStringFromQueryString("status", status);
            if (!string.IsNullOrEmpty(status))
            {
                if (status == "1")
                {
                    isApprove = true;
                }
                else if (status == "0")
                {
                    isApprove = false;
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
            int CategoryConfig = siteSetting.CoreLoaiVanBanQuyPham;
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
            int CategoryConfig = siteSetting.CoreLinhVucVanBanQuyPham;
            List<CoreCategory> lstLinhVuc = CoreCategory.GetChildren(1, WebConfigSettings.DM_LinhVucVanBan);
            ddlLinhVuc.DataValueField = "ItemID";
            ddlLinhVuc.DataTextField = "Name";
            ddlLinhVuc.DataSource = lstLinhVuc;
            ddlLinhVuc.DataBind();
            ddlLinhVuc.Items.Insert(0, new ListItem("--" + DocumentResources.ChooseFieldLabel + "--", "0"));
        }

        protected virtual void btnSearch_Click(object sender, EventArgs e)
        {
            linhVucId = string.IsNullOrEmpty(ddlLinhVuc.SelectedValue) ? -1 : int.Parse(ddlLinhVuc.SelectedValue);
            loaiVb = string.IsNullOrEmpty(ddlLoaiVB.SelectedValue) ? -1 : int.Parse(ddlLoaiVB.SelectedValue);
            keyword = txtKeyword.Text;
            status = ddlStatus.SelectedValue;

            string pageUrl = SiteRoot + "/duthaovanban/managepost.aspx"
                    + "?pageid=" + PageId.ToInvariantString()
                    + "&mid=" + ModuleId.ToInvariantString()
                    + "&linhvucid=" + linhVucId.ToInvariantString()
                    + "&loaivb=" + loaiVb.ToInvariantString()
                    + "&keyword=" + keyword
                    + "&status=" + status
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
                    WebUtils.SetupRedirect(this, SiteRoot + "/duthaovanban/editpost.aspx?pageid=" + PageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString() + "&item=" + itemId);
                }
                else if (e.CommandName.Equals("DeleteItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    DuThaoVanBan.Delete(itemId);
                    WebUtils.SetupRedirect(this, SiteRoot + "/duthaovanban/managepost.aspx?pageid="
                        + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString());
                }
                else if (e.CommandName.Equals("EditStateItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    DuThaoVanBan duthao = new DuThaoVanBan(itemId);
                    if (duthao != null)
                    {
                        if (duthao.IsPublic == true)
                        {
                            duthao.IsPublic = false;
                            duthao.Save();
                        }
                        else
                        {
                            duthao.IsPublic = true;
                            duthao.Save();
                        }
                    }
                    WebUtils.SetupRedirect(this, SiteRoot + "/duthaovanban/managepost.aspx?pageid="
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
                SiteUtils.AddConfirmButton(ibtnChangeState, DuThaoVanBanResources.ChangeStateWarning);
                ImageButton ibtnDelete = e.Item.FindControl("ibtnDelete") as ImageButton;
                SiteUtils.AddConfirmButton(ibtnDelete, ArticleResources.DeleteConfirmWarning);
            }
        }

        protected void btnaddnew_Click(object sender, EventArgs e)
        {
            WebUtils.SetupRedirect(this, SiteRoot + "/duthaovanban/editpost.aspx?pageid=" + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString());

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
                    DuThaoVanBan.Delete(itemid);
                }
            }
            if (deleteNumber > 0)
            {
                string pageUrl = SiteRoot + "/duthaovanban/managepost.aspx"
                        + "?pageid=" + PageId.ToInvariantString()
                        + "&mid=" + ModuleId.ToInvariantString()
                        + "&linhvucid=" + linhVucId.ToInvariantString()
                   + "&loaivb=" + loaiVb.ToInvariantString()
                        + "&keyword=" + keyword
                        + "&pagenumber=1";
                WebUtils.SetupRedirect(this, pageUrl);
            }
        }
        protected string UrlComments(int itemID)
        {
            string url = string.Empty;
            url = siteSettings.SiteRoot + "/duthaovanban/CommentManagePost.aspx" +
                "?pageid=" + PageId.ToInvariantString()
                 + "&mid=" + ModuleId.ToInvariantString()
                 + "&DuThaoID=" + itemID;
            return url;
        }
        protected string FormatArticleDate(DateTime startDate)
        {
            if (config.DateTimeFormat == string.Empty) return string.Empty;
            return startDate.ToString(config.DateTimeFormat);
        }

    }
}
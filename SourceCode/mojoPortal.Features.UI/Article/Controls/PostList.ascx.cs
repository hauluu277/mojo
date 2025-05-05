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
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArticleFeature.UI
{
    public partial class PostList : UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;
        private TimeZoneInfo timeZone;
        protected Double timeOffset;
        private string dateTimeFormat;
        private mojoBasePage basePage;
        private Module module;
        protected ArticleConfiguration config = new ArticleConfiguration();

        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        private SiteSettings siteSettings;
        private int categoryID = -1;
        private int apStatus = -1;
        private int puStatus = -1;
        private string keyword = string.Empty;
        protected int type = -1;
        protected int langId = -1;
        protected int langRefer = -1;
        //private bool? isApprove = null;
        //private bool? isPublish = null;
        private int isApprove = -1;
        private int isPublish = -1;
        protected bool isAdmin = false;
        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkText = SwirlingQuestionResource.ButtonDelete;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkText = SwirlingQuestionResource.ButtonEdit;
        protected string EditLinkImageUrl = string.Empty;
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        readonly SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
        private Guid authorID = Guid.Empty;
        private string createDateArticle = string.Empty;
        protected int role = -1;
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
        public int ApStatus
        {
            get { return apStatus; }
            set { apStatus = value; }
        }
        public int PuStatus
        {
            get { return puStatus; }
            set { puStatus = value; }
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

        public ArticleConfiguration Config
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
            btnChangeStatus.Click += btnChangeStatus_Click;
            //EnableViewState = false;
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

        private void PopulateLabels()
        {
            btnSearch.Text = ArticleResources.ArticleSearchButton;
            btnDelAll.Text = ArticleResources.ButtonDeleteAll;
            btnaddnew.Text = ArticleResources.ButtonAddNew;
            btnChangeStatus.Text = ArticleResources.ButtonChangeStatus;
            btnDelAll.OnClientClick = "return confirm(" + "'" + ArticleResources.DeleteAllConfirmWarning + "');";
            //SiteUtils.AddConfirmButton(btnDelAll, ArticleResources.DeleteConfirmWarning);


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
            dpBeginDate.Text = timeZone != null ? DateTime.Now.ToLocalTime(timeZone).ToString(dateTimeFormat) : DateTimeHelper.LocalizeToCalendar(DateTime.Now.ToString(dateTimeFormat));
            //reqStartDate.ErrorMessage = ArticleResources.BlogBeginDateRequiredHelp;
        }
        protected string FormatDate(DateTime? date)
        {
            //if (!String.IsNullOrEmpty(date))
            //{
            //    try
            //    {
            //        var dateModified = DateTime.Parse(date);
            //        return timeZone != null ? TimeZoneInfo.ConvertTimeFromUtc(dateModified, timeZone).ToString(config.DateTimeFormat) : dateModified.AddHours(timeOffset).ToString(config.DateTimeFormat);
            //    }
            //    catch { }
            //}
            //return "";
            var result = "";
            if (date != null)
            {
                return string.Format("{0:dd/MM/yyyy HH:mm}", date);
            }
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
                return true;
                //string filePath = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["ArticleImagesFolder"] + imageUrl;
                //filePath = filePath.Replace("/", "\\");
                //if (File.Exists(filePath))
                //{
                //    return true;

                //}
                //else
                //{
                //    return false;
                //}
            }
        }
        private void BindAuthor()
        {
            var listAuthor = SiteUser.GetUserBySiteGuid(siteSetting.SiteId);
            //Danh sách tác giả
            lboxAuthor.DataTextField = "LoginName";
            lboxAuthor.DataValueField = "UserGuid";
            lboxAuthor.DataSource = listAuthor;
            lboxAuthor.DataBind();
        }
        private void PopulateControls()
        {
            BindArticles();
            PopulateCategories();
            PopulateStatus();
            BindAuthor();
            if (categoryID > 0)
            {
                ddlCategories.SelectedValue = categoryID.ToString();
            }
            if (apStatus >= 0)
            {
                ddlApproveStatus.SelectedValue = apStatus.ToString();
            }
            if (puStatus >= 0)
            {
                ddlPublishStatus.SelectedValue = puStatus.ToString();
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                txtKeyword.Text = keyword;
            }
            if (authorID != Guid.Empty)
            {
                lboxAuthor.SelectedValue = authorID.ToString();
            }
            txtCreateDateArticle.Value = createDateArticle;
        }

        private void BindArticles()
        {
            EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
            DeleteLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;

            List<Article> reader = new List<Article>();
            var keyword_search = !string.IsNullOrEmpty(keyword) ? keyword.ConvertToVN() : string.Empty;
            if (isAdmin == true)
            {
                reader = Article.GetPage(siteSettings.SiteId, moduleId, pageNumber, config.PageSize, categoryID, isApprove, isPublish, keyword_search, authorID, createDateArticle, out totalPages);
            }
            rptArticles.DataSource = reader;
            rptArticles.DataBind();

            string pageUrl = SiteRoot + "/article/managepost.aspx"
                   + "?pageid=" + PageId.ToInvariantString()
                   + "&mid=" + ModuleId.ToInvariantString()
                   + "&credate=" + createDateArticle
                    + "&auid=" + authorID
                   + "&catid=" + categoryID.ToInvariantString()
                   + "&apstatus=" + apStatus.ToInvariantString()
                   + "&pustatus=" + puStatus.ToInvariantString()
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
            timeZone = SiteUtils.GetUserTimeZone();
            timeOffset = SiteUtils.GetUserTimeOffset();

            dateTimeFormat = config.DateTimeFormat.ToString();
        }
        protected virtual void LoadSettings()
        {
            if (siteUser.IsInRoles("Admins") || (WebUser.IsAdminOrContentAdmin) || (SiteUtils.UserIsSiteEditor())) { isAdmin = true; role = RoleConstant.isApprove; }

            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new ArticleConfiguration(getModuleSettings);

            if (siteUser.IsInRoles("Admins") || WebUser.IsInRoles(config.RolePost) || WebUser.IsInRoles(config.RoleApproved))
            {
                isAdmin = true;
                role = RoleConstant.isApprove;
            }

            if (siteUser.IsInRoles("Admins") || WebUser.IsInRoles(config.RoleApproved))
            {
                role = RoleConstant.isApprove;
            }
            else if (siteUser.IsInRoles("Admins") || WebUser.IsInRoles(config.RolePost))
            {
                role = RoleConstant.isPost;
            }
            else
            {
                role = -1;
            }
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            categoryID = WebUtils.ParseInt32FromQueryString("catid", categoryID);
            string ap_status = WebUtils.ParseStringFromQueryString("apstatus", string.Empty);
            string pu_status = WebUtils.ParseStringFromQueryString("pustatus", string.Empty);
            authorID = WebUtils.ParseGuidFromQueryString("auid", authorID);
            createDateArticle = WebUtils.ParseStringFromQueryString("credate", createDateArticle);
            if (!string.IsNullOrEmpty(ap_status))
            {
                apStatus = int.Parse(ap_status);
                isApprove = apStatus;
            }
            if (!string.IsNullOrEmpty(pu_status))
            {
                puStatus = int.Parse(pu_status);
                isPublish = puStatus;
            }
            keyword = WebUtils.ParseStringFromQueryString("keyword", keyword);

            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
        }
        private void PopulateCategories()
        {
            string listCategoryConfig = pageSettings.CategoryConfig;
            if (!string.IsNullOrEmpty(config.ArticleCategoryConfig))
                listCategoryConfig = config.ArticleCategoryConfig;
            var list_category_config = listCategoryConfig.Split('-');
            CoreCategory coreCategory = new CoreCategory();
            foreach (var item in list_category_config)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    coreCategory = new CoreCategory(int.Parse(item));
                    ListItem list = new ListItem
                    {
                        Text = coreCategory.Name,
                        Value = item
                    };
                    ddlCategories.Items.Add(list);
                }
            }
            PopulateChildNode(ddlCategories);
            ddlCategories.Items.Insert(0, new ListItem(ArticleResources.ParentCategoryChoose, "0"));
        }

        private void PopulateChildNode(ListControl root)
        {
            for (int i = 0; i < root.Items.Count; i++)
            {
                List<CoreCategory> children = CoreCategory.GetChildren(int.Parse(root.Items[i].Value));
                if (children.Count <= 0) continue;
                string prefix = string.Empty;
                while (root.Items[i].Text.StartsWith("|"))
                {
                    prefix += root.Items[i].Text.Substring(0, 3);
                    root.Items[i].Text = root.Items[i].Text.Remove(0, 3);
                }
                root.Items[i].Text = prefix + root.Items[i].Text;
                int index = 1;
                foreach (CoreCategory child in children)
                {
                    ListItem list = new ListItem
                    {
                        Text = prefix + @"|--" + child.Name,
                        Value = child.ItemID.ToString()
                    };
                    root.Items.Insert(root.Items.IndexOf(root.Items[i]) + index, list);
                    index++;
                }
            }
        }

        private void PopulateStatus()
        {
            //Trạng thái duyệt tin bài
            var approve_status = SiteUtils.StringToDictionary(ArticleResources.ArticleApproveStatus.ToString(), ",");
            ddlApproveStatus.DataSource = approve_status;
            ddlApproveStatus.DataTextField = "Value";
            ddlApproveStatus.DataValueField = "Key";
            ddlApproveStatus.DataBind();
            //Trạng thái xuất bản tin bài
            var publish_status = SiteUtils.StringToDictionary(ArticleResources.ArticlePublishStatus.ToString(), ",");
            ddlPublishStatus.DataSource = publish_status;
            ddlPublishStatus.DataTextField = "Value";
            ddlPublishStatus.DataValueField = "Key";
            ddlPublishStatus.DataBind();

            if (siteUser != null)
            {
                if (siteUser.IsInRoles("Admins") || WebUser.IsInRoles(config.RolePost) || WebUser.IsInRoles(config.RoleApproved))
                {
                    if (siteUser.IsInRoles("Admins") || WebUser.IsInRoles(config.RoleApproved))
                    {
                        var all_changestatus = SiteUtils.StringToDictionary(ArticleResources.ArticleChangeStatus_ApproveRole.ToString(), ",");
                        ddlAllStatus.DataSource = all_changestatus;
                        ddlAllStatus.DataTextField = "Value";
                        ddlAllStatus.DataValueField = "Key";
                        ddlAllStatus.DataBind();
                    }
                    else
                    {
                        var all_changestatus = SiteUtils.StringToDictionary(ArticleResources.ArticleChangeStatus_PublishRole.ToString(), ",");
                        ddlAllStatus.DataSource = all_changestatus;
                        ddlAllStatus.DataTextField = "Value";
                        ddlAllStatus.DataValueField = "Key";
                        ddlAllStatus.DataBind();
                    }
                }
            }
        }

        protected virtual void btnSearch_Click(object sender, EventArgs e)
        {
            categoryID = string.IsNullOrEmpty(ddlCategories.SelectedValue) ? -1 : int.Parse(ddlCategories.SelectedValue);
            authorID = string.IsNullOrEmpty(lboxAuthor.SelectedValue) ? Guid.Empty : Guid.Parse(lboxAuthor.SelectedValue);
            categoryID = string.IsNullOrEmpty(ddlCategories.SelectedValue) ? -1 : int.Parse(ddlCategories.SelectedValue);
            string ap_status = ddlApproveStatus.SelectedValue;
            string pu_status = ddlPublishStatus.SelectedValue;
            keyword = txtKeyword.Text;
            if (!string.IsNullOrEmpty(ap_status))
            {
                apStatus = int.Parse(ap_status);
                isApprove = apStatus;
                //if (ap_status == "1")
                //{
                //    isApprove = true;
                //}
                //else if (ap_status == "0")
                //{
                //    isApprove = false;
                //}
            }
            if (!string.IsNullOrEmpty(pu_status))
            {
                puStatus = int.Parse(pu_status);
                isPublish = puStatus;
                //if (pu_status == "1")
                //{
                //    isPublish = true;
                //}
                //else if (pu_status == "0")
                //{
                //    isPublish = false;
                //}
            }
            string pageUrl = SiteRoot + "/article/managepost.aspx"
                    + "?pageid=" + PageId.ToInvariantString()
                    + "&mid=" + ModuleId.ToInvariantString()
                    + "&catid=" + categoryID.ToInvariantString()
                    + "&apstatus=" + apStatus.ToInvariantString()
                    + "&pustatus=" + puStatus.ToInvariantString()
                    + "&credate=" + createDateArticle
                    + "&auid=" + authorID
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
                    WebUtils.SetupRedirect(this, SiteRoot + "/article/postarticle.aspx?pageid=" + PageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString() + "&ItemID=" + itemId);
                }
                else if (e.CommandName.Equals("DeleteItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    if (config.IsDeleteSetting)
                    {
                        Article articleDelete = new Article(itemId);
                        articleDelete.IsDelete = true;
                        articleDelete.Save();
                    }
                    else
                    {
                        Article articleDelete = new Article(itemId);
                        FriendlyUrl.DeleteByPageGuid(articleDelete.ArticleGuid, siteSetting.SiteId, articleDelete.ItemUrl);
                        Article.Delete(itemId);


                    }
                    WebUtils.SetupRedirect(this, SiteRoot + "/article/managepost.aspx?pageid="
                        + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString());
                }
            }
        }

        protected void rptArticles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;
            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                ImageButton ibtnDelete = e.Item.FindControl("ibtnDelete") as ImageButton;
                SiteUtils.AddConfirmButton(ibtnDelete, ArticleResources.DeleteConfirmWarning);
            }
        }

        protected void btnaddnew_Click(object sender, EventArgs e)
        {
            WebUtils.SetupRedirect(this, SiteRoot + "/article/postarticle.aspx?pageid=" + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString());
        }

        protected void btnDelAll_Click(object sender, EventArgs e)
        {
            int deleteNumber = 0;
            if (config.IsDeleteSetting)
            {
                foreach (RepeaterItem rpt in rptArticles.Items)
                {
                    CheckBox chkFlag = rpt.FindControl("chk") as CheckBox;
                    if (chkFlag.Checked)
                    {
                        deleteNumber++;
                        int itemid = Convert.ToInt32((rpt.FindControl("repeaterID") as Literal).Text);
                        Article articleDelete = new Article(itemid);
                        articleDelete.IsDelete = true;
                        articleDelete.Save();

                    }
                }
            }
            else
            {
                foreach (RepeaterItem rpt in rptArticles.Items)
                {
                    CheckBox chkFlag = rpt.FindControl("chk") as CheckBox;
                    if (chkFlag.Checked)
                    {
                        deleteNumber++;
                        int itemid = Convert.ToInt32((rpt.FindControl("repeaterID") as Literal).Text);

                        Article articleDelete = new Article(itemid);
                        FriendlyUrl.DeleteByPageGuid(articleDelete.ArticleGuid,siteSetting.SiteId,articleDelete.ItemUrl);
                        Article.Delete(itemid);
                    }
                }
            }


            string pageUrl = SiteRoot + "/article/managepost.aspx"
                    + "?pageid=" + PageId.ToInvariantString()
                    + "&mid=" + ModuleId.ToInvariantString()
                    + "&catid=" + categoryID.ToInvariantString()
                    + "&apstatus=" + apStatus.ToInvariantString()
                    + "&pustatus=" + puStatus.ToInvariantString()
                    + "&keyword=" + keyword
                    + "&pagenumber=1";
            WebUtils.SetupRedirect(this, pageUrl);

        }

        protected void btnChangeStatus_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlAllStatus.SelectedValue))
            {
                var status = int.Parse(ddlAllStatus.SelectedValue);
                int checkedNumber = 0;
                foreach (RepeaterItem rpt in rptArticles.Items)
                {
                    CheckBox chkFlag = rpt.FindControl("chk") as CheckBox;
                    if (chkFlag.Checked)
                    {
                        checkedNumber++;
                        int itemid = Convert.ToInt32((rpt.FindControl("repeaterID") as Literal).Text);
                        Article article = new Article(itemid);
                        if (status == 1)
                        {
                            article.IsApproved = true;
                            DateTime localTime = DateTime.Parse(dpBeginDate.Text, CultureInfo.CurrentCulture);
                            article.StartDate = timeZone != null ? localTime.ToUtc(timeZone) : localTime.AddHours(-timeOffset);
                            if (!string.IsNullOrEmpty(dpEndDate.Text))
                            {
                                DateTime localEndTime = DateTime.Parse(dpEndDate.Text, CultureInfo.CurrentCulture);
                                article.EndDate = timeZone != null ? localEndTime.ToUtc(timeZone) : localEndTime.AddHours(-timeOffset);
                            }
                            else
                            {
                                article.EndDate = null;
                            }
                            article.ApprovedDate = DateTime.Now;
                            article.ApprovedGuid = siteUser.UserGuid;
                        }
                        else if (status == 2)
                        {
                            article.IsApproved = false;
                            article.CommentByBoss = txtCommentByBoss.Text;
                            article.ApprovedDate = DateTime.Now;
                            article.ApprovedGuid = siteUser.UserGuid;
                        }
                        else if (status == 3)
                        {
                            article.IsPublished = true;
                            article.PublishedDate = DateTime.Now;
                            article.PublishedGuid = siteUser.UserGuid;
                        }
                        else if (status == 4)
                        {
                            article.IsPublished = false;
                            article.PublishedDate = DateTime.Now;
                            article.PublishedGuid = siteUser.UserGuid;
                        }
                        article.Save();
                    }
                }
                if (checkedNumber > 0)
                {
                    string pageUrl = SiteRoot + "/article/managepost.aspx"
                            + "?pageid=" + PageId.ToInvariantString()
                            + "&mid=" + ModuleId.ToInvariantString()
                            + "&catid=" + categoryID.ToInvariantString()
                            + "&apstatus=" + apStatus.ToInvariantString()
                            + "&pustatus=" + puStatus.ToInvariantString()
                            + "&keyword=" + keyword
                            + "&pagenumber=1";
                    WebUtils.SetupRedirect(this, pageUrl);
                }
            }
        }

        protected string ImgLanguage()
        {
            string Imgurl = string.Empty;
            Imgurl = "~/Data/SiteImages/flags/en.gif";
            return Imgurl;
        }
        protected string linkEditRefer(int itemId)
        {
            string link = string.Empty;
            ArticleReference articleRefer = ArticleReference.GetArticleByRootId(itemId);
            if (articleRefer.ItemID != -1)
            {
                link = SiteRoot + "/article/EditPostReference.aspx?pageid=" + PageId + "&ItemID=" + articleRefer.ItemID + "&LangID=" + langRefer + "&mid=" + ModuleId;
            }
            else
            {
                link = SiteRoot + "/article/EditPostReference.aspx?pageid=" + PageId + "&ItemIDRoot=" + itemId + "&LangID=" + langRefer + "&mid=" + ModuleId;
            }
            return link;
        }
        protected string FormatArticleDate(DateTime startDate)
        {
            if (config.DateTimeFormat == string.Empty) return string.Empty;
            string result = "";
            result = string.Format("{0:dd/MM/yyyy HH:mm}", startDate);
            return result;
        }

    }
}
using ArticleFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.SearchIndex;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;

namespace ArticleFeature.UI
{
    public partial class ManageArticleControl : UserControl
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
        private int totalRow = 0;
        protected int type = -1;
        protected int langId = -1;
        protected int langRefer = -1;
        private DateTime? _startDate = null;
        private DateTime? _endDate = null;
        private string startdate = string.Empty;
        private string enddate = string.Empty;
        private bool? _isHot = null;
        private bool? _isHome = null;
        private string isHot = string.Empty;
        private string isHome = string.Empty;
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
        private bool isUserApprove = false;
        private bool isUserPost = false;
        protected int role = -1;
        protected List<int> listCategory = new List<int>();
        private Guid authorID = Guid.Empty;
        private string createDateArticle = string.Empty;
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
            btnChangeCat.Click += btnChangeCat_Click;
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
        private void PopulateLabels()
        {
            btnSearch.Text = ArticleResources.ArticleSearchButton;
            btnDelAll.Text = ArticleResources.ButtonDeleteAll;
            btnaddnew.Text = ArticleResources.ButtonAddNew;
            btnChangeStatus.Text = ArticleResources.ButtonChangeStatus;
            btnChangeCat.Text = ArticleResources.btnChangeCat;
            btnDelAll.OnClientClick = "return confirm(" + "'" + ArticleResources.DeleteAllConfirmWarning + "');";
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
            //var listAuthor = SiteUser.GetUserBySiteGuid(siteSetting.SiteId);
            ////Danh sách tác giả
            //lboxAuthor.DataTextField = "LoginName";
            //lboxAuthor.DataValueField = "UserGuid";
            //lboxAuthor.DataSource = listAuthor;
            //lboxAuthor.DataBind();
            //lboxAuthor.Items.Insert(0, new ListItem { Text = "--Chọn--", Value = "" });
        }
        private void PopulateControls()
        {
            BindArticles();
            BindDDLModules();
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

            if (!string.IsNullOrEmpty(isHot))
            {
                ddlIsHot.SelectedValue = isHot;
            }
            if (!string.IsNullOrEmpty(isHome))
            {
                ddlIsHome.SelectedValue = isHome;
            }
            if (!string.IsNullOrEmpty(startdate))
            {
                txtStartDate.Value = startdate;
            }
            if (!string.IsNullOrEmpty(enddate))
            {
                txtEndDate.Value = enddate;
            }
            //if (authorID != Guid.Empty)
            //{
            //    lboxAuthor.SelectedValue = authorID.ToString();
            //}
            txtCreateDateArticle.Value = createDateArticle;
        }
        private void BindDDLModules()
        {
            //Get Articles Module only
            DBUtilities repository = new DBUtilities();
            var listModule = repository.GetModuleBO(siteSetting.SiteId, new Guid("8bdb1450-91e5-4cb0-af1a-2427d7e7e611"));
            foreach (var item in listModule)
            {
                var pageTitle = new PageSettings(siteSetting.SiteId, item.PageID);
                if (pageTitle != null)
                {
                    item.PageName = pageTitle.PageName;
                }
            }
            foreach (var item in listModule)
            {
                ListItem listItem = new ListItem();
                //listItem.Attributes.Add("PageID", item.PageID.ToString());
                if (!string.IsNullOrEmpty(item.PageName))
                {
                    listItem.Text = string.Format("{0} - (Trang: {1})", item.ModuleTitle, item.PageName);
                }
                else
                {
                    listItem.Text = item.ModuleTitle;

                }
                listItem.Value = string.Format("{0}-{1}", item.ModuleID.ToString(), item.PageID.ToString());
                ddlModuleCat.Items.Add(listItem);
            }
            ddlModuleCat.DataTextField = "Text";
            ddlModuleCat.DataValueField = "Value";
            ddlModuleCat.DataBind();
            ddlModuleCat.Items.Insert(0, new ListItem(ArticleResources.EmptyModule, string.Empty));
        }
        private void FormatModuleTitle()
        {
            foreach (ListItem item in ddlModuleCat.Items)
            {
                if (item.Value == string.Empty) continue;
                if (item.Text.Contains("</span>"))
                {
                    item.Text = FeatureUtilities.RemoveTwoColorModuleTitleText(item.Text);
                }
                Module m = new Module(Convert.ToInt32(item.Value));
                item.Text += @" (Site " + m.SiteId + @")";
            }
        }
        private void BindArticles()
        {
            EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
            DeleteLinkImageUrl = "/Data/SiteImages/article-icon/xoa.png";

            List<Article> reader = new List<Article>();
            var keyword_search = !string.IsNullOrEmpty(keyword) ? keyword.ConvertToVN() : string.Empty;
            string ArticleCategory = string.Empty;
            var userId = Guid.Empty;

            if (CommonArticle.AccessManageArticle)
            {
                ArticleCategory = "all";
            }
            else
            {
                userId = siteUser.UserGuid;
                ArticleCategory = string.Join(",", listCategory.ToArray());
            }

            reader = Article.GetAllArticlePage(siteSettings.SiteId, pageNumber, config.PageSize, categoryID, isApprove, isPublish, _isHot, _isHome, _startDate, _endDate, ArticleCategory, keyword_search, userId, createDateArticle, out totalPages, out totalRow);

            rptArticles.DataSource = reader;
            rptArticles.DataBind();
            lblTotalArticle.Text = string.Format("Tổng số {0} tin bài", totalRow);
            string pageUrl = SiteRoot + "/article/managearticle.aspx"
                   + "?pageid=" + PageId.ToInvariantString()
                   + "&mid=" + ModuleId.ToInvariantString()
                   + "&catid=" + categoryID.ToInvariantString()
                   + "&apstatus=" + apStatus.ToInvariantString()
                   + "&pustatus=" + puStatus.ToInvariantString()
                   + "&credate=" + createDateArticle
                    + "&auid=" + authorID
                     + "&ishot=" + isHot
                   + "&ishome=" + isHome
                  + "&startdate=" + startdate
                   + "&enddate=" + enddate
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
            dateTimeFormat = config.DateTimeFormat.ToString();
            timeOffset = SiteUtils.GetUserTimeOffset();
            authorID = WebUtils.ParseGuidFromQueryString("auid", authorID);
            createDateArticle = WebUtils.ParseStringFromQueryString("credate", createDateArticle);
        }
        protected virtual void LoadSettings()
        {
            if (siteUser.IsInRoles("Admins")
                || (WebUser.IsAdminOrContentAdmin)
                || (SiteUtils.UserIsSiteEditor())
                || CommonArticle.AccessManageArticle
                || CommonArticle.AccessApprovedArticle)
            {

                isAdmin = true;
                role = RoleConstant.isApprove;
                isUserApprove = true;
                isUserPost = true;
            }
            else if (CommonArticle.AccessPublisheedArticle)
            {
                role = RoleConstant.isPost;
                isUserPost = true;
            }
            else
            {
                role = -1;
            }
            hdfRoleArticle.Value = "0";
            if (isUserApprove || isAdmin)
            {
                hdfRoleArticle.Value = "1";
            }

            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new ArticleConfiguration(getModuleSettings);

            if (CommonArticle.AccessManageArticle)
            {
                //continude
            }
            else
            {
                listCategory = CategoryUserArticle.GetCategoryIdByUser(siteUser.UserId);
            }

            siteSettings = CacheHelper.GetCurrentSiteSettings();
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            categoryID = WebUtils.ParseInt32FromQueryString("catid", categoryID);
            string ap_status = WebUtils.ParseStringFromQueryString("apstatus", string.Empty);
            string pu_status = WebUtils.ParseStringFromQueryString("pustatus", string.Empty);
            isHot = WebUtils.ParseStringFromQueryString("ishot", isHot);
            isHome = WebUtils.ParseStringFromQueryString("ishome", isHome);
            startdate = WebUtils.ParseStringFromQueryString("startdate", startdate);
            enddate = WebUtils.ParseStringFromQueryString("enddate", enddate);
            if (!string.IsNullOrEmpty(startdate))
            {
                _startDate = startdate.ToDateTime();
            }
            if (!string.IsNullOrEmpty(enddate))
            {
                _endDate = enddate.ToDateTime();
            }
            if (!string.IsNullOrEmpty(isHot))
            {
                try
                {
                    int result = int.Parse(isHot);

                    if (result == 0)
                    {
                        _isHot = false;
                    }
                    else if (result == 1)
                    {
                        _isHot = true;
                    }
                    else
                    {
                        _isHot = null;
                    }

                }
                catch
                {

                    _isHot = null;
                }
            }
            if (!string.IsNullOrEmpty(isHome))
            {
                try
                {
                    int result = int.Parse(isHome);

                    if (result == 0)
                    {
                        _isHome = false;
                    }
                    else if (result == 1)
                    {
                        _isHome = true;
                    }
                    else
                    {
                        _isHome = null;
                    }

                }
                catch
                {

                    _isHome = null;
                }
            }
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

            if (CommonArticle.AccessManageArticle)
            {
                List<CoreCategory> listChild = CoreCategory.GetChildren(siteSetting.ArticleCategory);
                foreach (var item in listChild)
                {
                    ddlCategories.Items.Add(new ListItem { Text = item.Name, Value = item.ItemID.ToString() });
                }
                PopulateChildNode(ddlCategories);
                ddlCategories.Items.Insert(0, new ListItem(ArticleResources.ParentCategoryChoose, "0"));
            }
            else
            {
                ddlCategories.DataValueField = "CategoryID";
                ddlCategories.DataTextField = "CategoryName";
                ddlCategories.DataSource = CategoryUserArticle.GetListItemByUserBO(siteUser.UserId);
                ddlCategories.DataBind();

            }



            //if (CommonArticle.AccessManageArticle)
            //{
            //    foreach (var item in listChild)
            //    {
            //        ddlCategories.Items.Add(new ListItem { Text = item.Name, Value = item.ItemID.ToString() });
            //    }
            //}
            //else
            //{
            //    var articleCategory = new List<int>();
            //    if (listCategory != null && listCategory.Count > 0)
            //    {
            //        foreach (var item in listCategory)
            //        {
            //            articleCategory.AddRange(CoreCategory.GetListParent(item));
            //        }
            //    }
            //    foreach (var item in listChild)
            //    {
            //        if (articleCategory.Contains(item.ItemID))
            //        {
            //            ddlCategories.Items.Add(new ListItem { Text = item.Name, Value = item.ItemID.ToString() });
            //        }
            //    }
            //}
            
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
            //Trạng thái tin hot
            var ishot_status = SiteUtils.StringToDictionary(ArticleResources.ArticleIsHotStatus.ToString(), ",");
            ddlIsHot.DataSource = ishot_status;
            ddlIsHot.DataTextField = "Value";
            ddlIsHot.DataValueField = "Key";
            ddlIsHot.DataBind();

            //Trang thái tin hiển thị lên trang chủ
            var ishome_status = SiteUtils.StringToDictionary(ArticleResources.ArticleIsHomeStatus.ToString(), ",");
            ddlIsHome.DataSource = ishome_status;
            ddlIsHome.DataTextField = "Value";
            ddlIsHome.DataValueField = "Key";
            ddlIsHome.DataBind();

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
            //authorID = string.IsNullOrEmpty(lboxAuthor.SelectedValue) ? Guid.Empty : Guid.Parse(lboxAuthor.SelectedValue);
            createDateArticle = txtCreateDateArticle.Value;
            string ap_status = ddlApproveStatus.SelectedValue;
            string pu_status = ddlPublishStatus.SelectedValue;
            isHot = ddlIsHot.SelectedValue;
            isHome = ddlIsHome.SelectedValue;
            keyword = txtKeyword.Text;
            startdate = txtStartDate.Value;
            enddate = txtEndDate.Value;

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
            string pageUrl = SiteRoot + "/article/managearticle.aspx"
                    + "?pageid=" + PageId.ToInvariantString()
                    + "&mid=" + ModuleId.ToInvariantString()
                    + "&catid=" + categoryID.ToInvariantString()
                    + "&apstatus=" + apStatus.ToInvariantString()
                    + "&pustatus=" + puStatus.ToInvariantString()
                    + "&credate=" + createDateArticle
                    + "&auid=" + authorID
                     + "&ishot=" + isHot
                   + "&ishome=" + isHome
                   + "&startdate=" + startdate
                   + "&enddate=" + enddate
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
                    Article art = new Article(itemId);
                    WebUtils.SetupRedirect(this, SiteRoot + "/article/postarticle.aspx?pageid=" + PageId.ToInvariantString() + "&mid=" + art.ModuleID.ToInvariantString() + "&ItemID=" + itemId);
                }
                else if (e.CommandName.Equals("DeleteItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());

                    var article = new Article(itemId);
                    if (config.IsDeleteSetting)
                    {
                        article.IsDelete = true;
                        article.Save();
                    }
                    else
                    {
                        article.ContentChanged += new ContentChangedEventHandler(article_ContentChanged);
                        FriendlyUrl.DeleteByPageGuid(article.ArticleGuid, siteSetting.SiteId, string.Empty);
                        SiteUtils.QueueIndexing();
                        Article.Delete(itemId);
                    }
                    WebUtils.SetupRedirect(this, SiteRoot + "/article/managearticle.aspx?pageid="
                        + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString());
                }
            }
        }

        void article_ContentChanged(object sender, ContentChangedEventArgs e)
        {
            IndexBuilderProvider indexBuilder = IndexBuilderManager.Providers["ArticleIndexBuilderProvider"];
            if (indexBuilder != null)
            {
                indexBuilder.ContentChangedHandler(sender, e);
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

                        var articleDelete = new Article(itemid);
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

                        var articleDelete = new Article(itemid);
                        articleDelete.ContentChanged += new ContentChangedEventHandler(article_ContentChanged);
                        FriendlyUrl.DeleteByPageGuid(articleDelete.ArticleGuid, siteSetting.SiteId, articleDelete.ItemUrl);
                        SiteUtils.QueueIndexing();

                        Article.Delete(itemid);
                    }
                }
            }

            if (deleteNumber > 0)
            {
                string pageUrl = SiteRoot + "/article/managearticle.aspx"
                        + "?pageid=" + PageId.ToInvariantString()
                        + "&mid=" + ModuleId.ToInvariantString()
                        + "&catid=" + categoryID.ToInvariantString()
                        + "&apstatus=" + apStatus.ToInvariantString()
                        + "&pustatus=" + puStatus.ToInvariantString()
                        + "&ishot=" + isHot
                        + "&ishome=" + isHome
                        + "&startdate=" + startdate
                        + "&enddate=" + enddate
                        + "&keyword=" + keyword
                        + "&pagenumber=1";
                WebUtils.SetupRedirect(this, pageUrl);
            }
        }
        protected void btnChangeCat_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlModuleCat.SelectedValue))
            {
                var getSelected = ddlModuleCat.SelectedValue.Split('-');
                var moduleID = int.Parse(getSelected[0]);
                Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(moduleID);
                config = new ArticleConfiguration(getModuleSettings);
                int checkedNumber = 0;
                foreach (RepeaterItem rpt in rptArticles.Items)
                {
                    CheckBox chkFlag = rpt.FindControl("chk") as CheckBox;
                    if (chkFlag.Checked)
                    {
                        checkedNumber++;
                        int itemid = Convert.ToInt32((rpt.FindControl("repeaterID") as Literal).Text);
                        var catID = config.ArticleCategoryConfig.ToString().Replace("-", string.Empty);
                        Article article = new Article(itemid);
                        article.ModuleID = moduleID;
                        article.CategoryID = int.Parse(catID);
                        article.SaveCat();

                        FriendlyUrl oldFriendlyUrl = new FriendlyUrl(article.SiteId, article.ItemUrl.Replace("~/", string.Empty));
                        if (oldFriendlyUrl != null)
                        {
                            oldFriendlyUrl.RealUrl = "~/Article/ViewPost.aspx?pageid="
                            + getSelected[1]
                            + "&mid=" + article.ModuleID.ToInvariantString()
                            + "&ItemID=" + article.ItemID.ToInvariantString();
                            oldFriendlyUrl.Save();

                        }
                    }
                }
                if (checkedNumber > 0)
                {
                    string pageUrl = SiteRoot + "/article/managearticle.aspx"
                            + "?pageid=" + PageId.ToInvariantString()
                            + "&mid=" + ModuleId.ToInvariantString()
                            + "&catid=" + categoryID.ToInvariantString()
                            + "&apstatus=" + apStatus.ToInvariantString()
                            + "&pustatus=" + puStatus.ToInvariantString()
                            + "&keyword=" + keyword
                            //                             + "&startDangBai=" + string.Format("{0:dd/MM/yyyy}", txtStartDangBai.Text)
                            //+ "&endDangBai=" + string.Format("{0:dd/MM/yyyy}", txtEndDangBai.Text)
                            //+ "&startHienThi=" + string.Format("{0:dd/MM/yyyy}", txtStartHienThi.Text)
                            //+ "&endHienThi=" + string.Format("{0:dd/MM/yyyy}", txtEndHienThi.Text)
                            //+ "&startHieuLuc=" + string.Format("{0:dd/MM/yyyy}", txtStartHieuLuc.Text)
                            //+ "&endHieuLuc=" + string.Format("{0:dd/MM/yyyy}", txtEndHieuLuc.Text)
                            + "&pagenumber=1";
                    WebUtils.SetupRedirect(this, pageUrl);
                }
            }
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
                    string pageUrl = SiteRoot + "/article/managearticle.aspx"
                            + "?pageid=" + PageId.ToInvariantString()
                            + "&mid=" + ModuleId.ToInvariantString()
                            + "&catid=" + categoryID.ToInvariantString()
                            + "&apstatus=" + apStatus.ToInvariantString()
                            + "&pustatus=" + puStatus.ToInvariantString()
                            + "&ishot=" + isHot
                            + "&ishome=" + isHome
                            + "&startdate=" + startdate
                            + "&enddate=" + enddate
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
                link = SiteRoot + "/article/postarticleReference.aspx?pageid=" + PageId + "&ItemID=" + articleRefer.ItemID + "&LangID=" + langRefer + "&mid=" + ModuleId;
            }
            else
            {
                link = SiteRoot + "/article/postarticleReference.aspx?pageid=" + PageId + "&ItemIDRoot=" + itemId + "&LangID=" + langRefer + "&mid=" + ModuleId;
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
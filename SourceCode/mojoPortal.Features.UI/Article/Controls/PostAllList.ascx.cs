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
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;

namespace ArticleFeature.UI
{
    public partial class PostAllList : UserControl
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
        private int siteID = -1;
        private Guid authorID = Guid.Empty;
        private int apStatus = -1;
        private int puStatus = -1;
        private string keyword = string.Empty;
        protected int type = -1;
        protected int langId = -1;
        protected int langRefer = -1;
        private string _startDate = string.Empty;
        private string _endDate = string.Empty;
        private string startdate = string.Empty;
        private string enddate = string.Empty;
        private bool? _isHot = null;
        private bool? _isHome = null;
        private string isHot = string.Empty;
        private string isHome = string.Empty;
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
        private bool isUserApprove = false;
        private bool isUserPost = false;
        protected int role = -1;
        private int siteSearchId = 0;
        private string authorGuid = string.Empty;
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
            lboxSiteTab_2.SelectedIndexChanged += LboxSiteTab_2_SelectedIndexChanged;
            lboxSiteTab_2.SelectedIndexChanged += LboxSiteTab_3_SelectedIndexChanged;
            btnSearch.Click += btnSearch_Click;
            btnaddnew.Click += btnaddnew_Click;
            btnDelAll.Click += btnDelAll_Click;
            btnChangeCat.Click += btnChangeCat_Click;
            btnChangeStatus.Click += btnChangeStatus_Click;

            //EnableViewState = false;
        }
        private List<CoreCategory> BindDataCategory(int categoryParentId)
        {
            List<CoreCategory> categories = new List<CoreCategory>();

            CoreCategory defaultCat = new CoreCategory(categoryParentId);
            categories.Add(defaultCat);
            PopulateChildItem(categories, 0);
            categories.Remove(defaultCat);
            return categories;
        }
        private void PopulateChildItem(List<CoreCategory> root, int itemId)
        {
            for (int i = 0; i < root.Count; i++)
            {
                List<CoreCategory> children = CoreCategory.GetChildren(root[i].ItemID);
                if (children.Count <= 0) continue;
                string prefix = string.Empty;
                while (root[i].Name.StartsWith("|"))
                {
                    prefix += root[i].Name.Substring(0, 3);
                    root[i].Name = root[i].Name.Remove(0, 3);
                }
                root[i].Name = prefix + root[i].Name;
                int index = 1;
                foreach (CoreCategory child in children)
                {
                    if (child.ItemID.Equals(itemId)) continue;

                    child.Name = prefix + @"|--" + child.Name;
                    root.Insert(root.IndexOf(root[i]) + index, child);
                    index++;
                }
            }
        }
        protected void LboxSiteTab_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var siteTab2 = lboxSiteTab_2.SelectedValue;
            var siteSettingBySite = new SiteSettings(siteTab2.ToIntOrZero());
            var listCategoryBySite = BindDataCategory(siteSettingBySite.ArticleCategory);
            lboxCategoryTab_2.DataValueField = "ItemID";
            lboxCategoryTab_2.DataTextField = "Name";
            lboxCategoryTab_2.DataSource = listCategoryBySite;
            lboxCategoryTab_2.DataBind();
            pnlUpdateCategoryTab_2.Update();

            ScriptManager.RegisterStartupScript(
              pnlUpdateCategoryTab_2,
              this.GetType(),
              "ReloadListBoxTab2",
              "ReloadListBox();",
              true);
        }
        protected void LboxSiteTab_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            var siteTab3 = lboxSiteTab_2.SelectedValue;
            var listAuthorBySite = SiteUser.GetUserBySiteGuid(siteTab3.ToIntOrZero());
            lboxAuthorTab_3.DataValueField = "UserGuid";
            lboxAuthorTab_3.DataTextField = "LoginName";
            lboxAuthorTab_3.DataSource = listAuthorBySite;
            lboxAuthorTab_3.DataBind();
            pnlUpdateCategoryTab_3.Update();


            ScriptManager.RegisterStartupScript(
              pnlUpdateCategoryTab_3,
              this.GetType(),
              "ReloadListBoxTab3",
              "ReloadListBox();",
              true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadParams();
            LoadSettings();
            PopulateLabels();
            if (!Request.IsAuthenticated || siteUser == null)
            {
                SiteUtils.RedirectToLoginPage(this);
                return;
            }
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
        private void PopulateControls()
        {
            BindArticles();
            BindDDLModules();
            //PopulateCategories();
            PopulateStatus();

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


            lboxSiteTab_2.SelectedValue = siteID.ToString();
            if (categoryID > 0)
            {
                lboxCategoryTab_2.SelectedValue = categoryID.ToString();
            }
            if (authorID != Guid.Empty)
            {
                lboxAuthorTab_3.SelectedValue = authorID.ToString();
            }
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
            DeleteLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;

            List<Article> reader = new List<Article>();
            var keyword_search = !string.IsNullOrEmpty(keyword) ? keyword.ConvertToVN() : string.Empty;
            if (siteID <= 0)
            {
                siteID = siteSetting.SiteId;
            }
            var totalRow = 0;
            reader = Article.GetAllPage(siteID, pageNumber, config.PageSize, categoryID, authorID, isApprove, isPublish, _isHot, _isHome, startdate.ToDateTime(), enddate.ToDateTime(), keyword_search, out totalPages, out totalRow);
            rptArticles.DataSource = reader;
            rptArticles.DataBind();
            lblTotalArticle.Text = string.Format("Tổng số {0} tin bài", totalRow);

            string pageUrl = SiteRoot + "/article/manageallpost.aspx"
                   + "?pageid=" + PageId.ToInvariantString()
                   + "&mid=" + ModuleId.ToInvariantString()
                   + "&siteid=" + siteID.ToInvariantString()
                   + "&catid=" + categoryID.ToInvariantString()
                   + "&auid=" + authorID.ToString()
                   + "&apstatus=" + apStatus.ToInvariantString()
                   + "&pustatus=" + puStatus.ToInvariantString()
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


            pgrArticle2.PageURLFormat = pageUrl;
            pgrArticle2.ShowFirstLast = true;
            pgrArticle2.PageSize = config.PageSize;
            pgrArticle2.PageCount = totalPages;
            pgrArticle2.CurrentIndex = pageNumber;
            pnlArticlePager2.Visible = (totalPages > 1) && config.ShowPager;
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            timeZone = SiteUtils.GetUserTimeZone();
            dateTimeFormat = config.DateTimeFormat.ToString();
            timeOffset = SiteUtils.GetUserTimeOffset();
        }
        protected virtual void LoadSettings()
        {
            if (siteUser.IsInRoles("Admins") || (WebUser.IsAdminOrContentAdmin) || (SiteUtils.UserIsSiteEditor())) { isAdmin = true; role = RoleConstant.isApprove; }

            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new ArticleConfiguration(getModuleSettings);
            if (siteUser.IsInRoles("Admins") || WebUser.IsInRoles(config.RoleApproved))
            {
                role = RoleConstant.isApprove;
                isUserApprove = true;
            }
            else if (WebUser.IsInRoles(config.RolePost))
            {
                role = RoleConstant.isPost;
                isUserPost = true;
            }
            else
            {
                role = -1;
            }

            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            siteID = WebUtils.ParseInt32FromQueryString("siteid", siteID);
            categoryID = WebUtils.ParseInt32FromQueryString("catid", categoryID);
            authorID = WebUtils.ParseGuidFromQueryString("auid", authorID);
            string ap_status = WebUtils.ParseStringFromQueryString("apstatus", string.Empty);
            string pu_status = WebUtils.ParseStringFromQueryString("pustatus", string.Empty);
            isHot = WebUtils.ParseStringFromQueryString("ishot", isHot);
            isHome = WebUtils.ParseStringFromQueryString("ishome", isHome);
            startdate = WebUtils.ParseStringFromQueryString("startdate", startdate);
            enddate = WebUtils.ParseStringFromQueryString("enddate", enddate);
            siteID = siteID <= 0 ? siteSetting.SiteId : siteID;


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
            //List<CoreCategory> roots = CoreCategory.GetRootByCat(siteSetting.SiteId, siteSetting.ArticleCategory);
            //foreach (CoreCategory item in roots)
            //{
            //    ListItem list = new ListItem
            //    {
            //        Text = item.Name,
            //        Value = item.ItemID.ToString()
            //    };
            //    ddlCategories.Items.Add(list);
            //}
            //PopulateChildNode(ddlCategories);
            //ddlCategories.Items.Insert(0, new ListItem(ArticleResources.ParentCategoryChoose, "0"));
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

            //Danh sách website
            var sites = SiteSettings.GetListAllSite();
            lboxSiteTab_2.DataValueField = "SiteID";
            lboxSiteTab_2.DataTextField = "SiteName";
            lboxSiteTab_2.DataSource = sites;
            lboxSiteTab_2.DataBind();

            var listCategory = CoreCategory.GetBySite(siteID);

            //Danh sách danh mục
            lboxCategoryTab_2.DataValueField = "ItemID";
            lboxCategoryTab_2.DataTextField = "Name";
            lboxCategoryTab_2.DataSource = listCategory;
            lboxCategoryTab_2.DataBind();

            var listAuthor = SiteUser.GetUserBySiteGuid(siteID);
            //Danh sách tác giả
            lboxAuthorTab_3.DataTextField = "LoginName";
            lboxAuthorTab_3.DataValueField = "UserGuid";
            lboxAuthorTab_3.DataSource = listAuthor;
            lboxAuthorTab_3.DataBind();


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
            categoryID = string.IsNullOrEmpty(lboxCategoryTab_2.SelectedValue) ? -1 : int.Parse(lboxCategoryTab_2.SelectedValue);
            siteID = string.IsNullOrEmpty(lboxSiteTab_2.SelectedValue) ? -1 : int.Parse(lboxSiteTab_2.SelectedValue);
            authorID = string.IsNullOrEmpty(lboxAuthorTab_3.SelectedValue) ? Guid.Empty : Guid.Parse(lboxAuthorTab_3.SelectedValue);
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
            string pageUrl = SiteRoot + "/article/manageallpost.aspx"
                    + "?pageid=" + PageId.ToInvariantString()
                    + "&mid=" + ModuleId.ToInvariantString()
                    + "&siteid=" + siteID.ToInvariantString()
                   + "&catid=" + categoryID.ToInvariantString()
                   + "&auid=" + authorID.ToString()
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

        protected void rptArticles_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;

            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName.Equals("EditItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    Article art = new Article(itemId);
                    WebUtils.SetupRedirect(this, SiteRoot + "/article/editpost.aspx?pageid=" + PageId.ToInvariantString() + "&mid=" + art.ModuleID.ToInvariantString() + "&ItemID=" + itemId);
                }
                else if (e.CommandName.Equals("DeleteItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    Article.Delete(itemId);
                    saveLog(itemId);
                    WebUtils.SetupRedirect(this, SiteRoot + "/article/manageallpost.aspx?pageid="
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
                        saveLog(itemid);
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
                        Article.Delete(itemid);
                        saveLog(itemid);
                    }
                }
            }

            if (deleteNumber > 0)
            {
                string pageUrl = SiteRoot + "/article/manageallpost.aspx"
                        + "?pageid=" + PageId.ToInvariantString()
                        + "&mid=" + ModuleId.ToInvariantString()
                        + "&siteid=" + siteID.ToInvariantString()
                       + "&catid=" + categoryID.ToInvariantString()
                       + "&auid=" + authorID.ToString()
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
                    string pageUrl = SiteRoot + "/article/manageallpost.aspx"
                            + "?pageid=" + PageId.ToInvariantString()
                            + "&mid=" + ModuleId.ToInvariantString()
                            + "&siteid=" + siteID.ToInvariantString()
                   + "&catid=" + categoryID.ToInvariantString()
                   + "&auid=" + authorID.ToString()
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
                    string pageUrl = SiteRoot + "/article/manageallpost.aspx"
                            + "?pageid=" + PageId.ToInvariantString()
                            + "&mid=" + ModuleId.ToInvariantString()
                            + "&siteid=" + siteID.ToInvariantString()
                   + "&catid=" + categoryID.ToInvariantString()
                   + "&auid=" + authorID.ToString()
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
        protected int CountNumber(int number)
        {
            return ((pageNumber - 1) * config.PageSize) + number;
        }
        protected string GetRoot(int siteId, string hostName)
        {
            if (siteId != 1 && !string.IsNullOrEmpty(hostName))
            {
                Uri uri = Request.Url;
                var host = uri.Scheme + Uri.SchemeDelimiter + hostName;
                return host;
            }
            return SiteRoot;
        }

        private void saveLog(int ItemId)
        {
            QLLog qlLog = new QLLog()
            {
                DiaChiIP = Common.findMyIP().ToString(),
                Type = KieuLogConstant.LogQuanTriTin,
                DuongDanThaoTac = HttpContext.Current.Request.Url.PathAndQuery,
                HanhDongThaoTac = KieuLogConstant.XoaDuLieu,
                NoiDung = "Xóa tin bài Id: " + ItemId,
                CreatedBy = siteUser.Name,
                CreatedByUser = siteUser.UserId,
                CreatedDate = DateTime.Now
            };
            qlLog.Save();
        }
    }
}
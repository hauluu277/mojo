using EventFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EventFeature.UI
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
        protected EventConfiguration config = new EventConfiguration();

        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        public SiteSettings siteSettings;
        private int categoryID = -1;
        private int apStatus = -1;
        private int puStatus = -1;
        private int isApprove = -1;
        private int isPublish = -1;
        private int status = -1;
        private string keyword = string.Empty;
        protected int type = -1;
        protected int langId = -1;
        protected int langRefer = -1;

        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkText = SwirlingQuestionResource.ButtonDelete;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkText = SwirlingQuestionResource.ButtonEdit;
        protected string EditLinkImageUrl = string.Empty;
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
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
        public int CategoryId
        {
            get { return categoryID; }
            set { categoryID = value; }
        }
        public int Status
        {
            get { return status; }
            set { status = value; }
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

        public EventConfiguration Config
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
            legendSearchProperty.InnerText = EventResources.ArticleEditSearchPropertyLabel;
            btnSearch.Text = EventResources.ArticleSearchButton;
            btnDelAll.BackColor = System.Drawing.Color.Red;
            btnDelAll.Text = EventResources.ButtonDeleteAll;
            btnaddnew.Text = EventResources.ButtonAddNew;
            btnDelAll.OnClientClick = "return confirm(" + "'" + EventResources.DeleteAllConfirmWarning + "');";
            //dpBeginDate.Text = timeZone != null ? DateTime.Now.ToLocalTime(timeZone).ToString(dateTimeFormat) : DateTimeHelper.LocalizeToCalendar(DateTime.Now.ToString(dateTimeFormat));
            //ToDo?
            //ValidateDeleteAll();
            UIHelper.DisableButtonAfterClick(
                btnSearch,
                EventResources.ButtonDisabledPleaseWait,
                Page.ClientScript.GetPostBackEventReference(btnSearch, string.Empty)
                );
            UIHelper.DisableButtonAfterClick(
                btnDelAll,
                EventResources.ButtonDisabledPleaseWait,
                Page.ClientScript.GetPostBackEventReference(btnDelAll, string.Empty)
                );
        }
        private void PopulateControls()
        {
            BindEvents();
            PopulateStatus();

            if (puStatus >= 0)
            {
                ddlPublishStatus.SelectedValue = puStatus.ToString();
            }
        
            if (!string.IsNullOrEmpty(keyword))
            {
                txtKeyword.Text = keyword;
            }
        }

        private void BindEvents()
        {
            EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
            DeleteLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;

            List<Event> reader = Event.GetPage(siteSettings.SiteId, moduleId, pageNumber, config.PageSize, categoryID, isApprove, isPublish, keyword.ConvertToVN(), out totalPages);
            rptEvents.DataSource = reader;
            rptEvents.DataBind();


            string pageUrl = SiteRoot + "/event/managepost.aspx"
                   + "?pageid=" + PageId.ToInvariantString()
                   + "&amp;mid=" + ModuleId.ToInvariantString()
                   + "&amp;catid=" + categoryID.ToInvariantString()
                   + "&amp;status=" + status.ToInvariantString()
                   + "&amp;keyword=" + keyword
                   + "&amp;pagenumber={0}";

            pgrEvent.PageURLFormat = pageUrl;
            pgrEvent.ShowFirstLast = true;
            pgrEvent.PageSize = config.PageSize;
            pgrEvent.PageCount = totalPages;
            pgrEvent.CurrentIndex = pageNumber;
            pnlEventPager.Visible = (totalPages > 1) && config.ShowPager;
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
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new EventConfiguration(getModuleSettings);

            siteSettings = CacheHelper.GetCurrentSiteSettings();
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            categoryID = WebUtils.ParseInt32FromQueryString("catid", categoryID);
            string ap_status = WebUtils.ParseStringFromQueryString("apstatus", string.Empty);
            string pu_status = WebUtils.ParseStringFromQueryString("pustatus", string.Empty);
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
            //var approve_status = SiteUtils.StringToDictionary(EventResources.EventApproveStatus.ToString(), ",");
            //ddlApproveStatus.DataSource = approve_status;
            //ddlApproveStatus.DataTextField = "Value";
            //ddlApproveStatus.DataValueField = "Key";
            //ddlApproveStatus.DataBind();

            //Trạng thái xuất bản tin bài
            var publish_status = SiteUtils.StringToDictionary(EventResources.EventPublishStatus.ToString(), ",");
            ddlPublishStatus.DataSource = publish_status;
            ddlPublishStatus.DataTextField = "Value";
            ddlPublishStatus.DataValueField = "Key";
            ddlPublishStatus.DataBind();
        }

        protected virtual void btnSearch_Click(object sender, EventArgs e)
        {
            //string ap_status = ddlApproveStatus.SelectedValue;
            string pu_status = ddlPublishStatus.SelectedValue;
            keyword = txtKeyword.Text;
            //if (!string.IsNullOrEmpty(ap_status))
            //{
            //    apStatus = int.Parse(ap_status);
            //    isApprove = apStatus;
            //    //if (ap_status == "1")
            //    //{
            //    //    isApprove = true;
            //    //}
            //    //else if (ap_status == "0")
            //    //{
            //    //    isApprove = false;
            //    //}
            //}
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
            string pageUrl = SiteRoot + "/event/managepost.aspx"
                    + "?pageid=" + PageId.ToInvariantString()
                    + "&mid=" + ModuleId.ToInvariantString()
                    + "&catid=" + categoryID.ToInvariantString()
                    + "&apstatus=" + apStatus.ToInvariantString()
                    + "&pustatus=" + puStatus.ToInvariantString()
                    + "&keyword=" + keyword
                    + "&pagenumber=1";
            WebUtils.SetupRedirect(this, pageUrl);
        }

        protected void rptEvents_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;

            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName.Equals("EditItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    WebUtils.SetupRedirect(this, SiteRoot + "/event/editpost.aspx?pageid=" + PageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString() + "&ItemID=" + itemId);
                }
                else if (e.CommandName.Equals("DeleteItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());

                    var _searchEvent = new Event(itemId);
                    FriendlyUrl.DeleteByPageGuid(_searchEvent.EventGuid,siteSetting.SiteId,_searchEvent.ItemUrl);

                    Event.Delete(itemId);
                    WebUtils.SetupRedirect(this, SiteRoot + "/event/managepost.aspx?pageid="
                        + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString());
                }
            }
        }

        protected void rptEvents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;
            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                ImageButton ibtnDelete = e.Item.FindControl("ibtnDelete") as ImageButton;
                SiteUtils.AddConfirmButton(ibtnDelete, EventResources.DeleteConfirmWarning);
            }
        }

        protected void btnaddnew_Click(object sender, EventArgs e)
        {
            WebUtils.SetupRedirect(this, SiteRoot + "/event/editpost.aspx?pageid=" + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString());

        }

        protected void btnDelAll_Click(object sender, EventArgs e)
        {
            int deleteNumber = 0;
            foreach (RepeaterItem ri in rptEvents.Items)
            {
                CheckBox chkFlag = ri.FindControl("chk") as CheckBox;
                if (chkFlag.Checked)
                {
                    deleteNumber++;
                    int itemid = Convert.ToInt32((ri.FindControl("repeaterID") as Literal).Text);
                    var _searchEvent = new Event(itemid);
                    FriendlyUrl.DeleteByPageGuid(_searchEvent.EventGuid,siteSetting.SiteId,_searchEvent.ItemUrl);
                    Event.Delete(itemid);
                }
            }
            if (deleteNumber > 0)
            {
                string pageUrl = SiteRoot + "/event/managepost.aspx"
                        + "?pageid=" + PageId.ToInvariantString()
                        + "&mid=" + ModuleId.ToInvariantString()
                        + "&catid=" + categoryID.ToInvariantString()
                        + "&status=" + status.ToInvariantString()
                        + "&keyword=" + keyword
                        + "&pagenumber=1";
                WebUtils.SetupRedirect(this, pageUrl);
            }
        }
        protected string ImgLanguage()
        {
            string Imgurl = string.Empty;
            Imgurl = "~/Data/SiteImages/flags/en.gif";
            return Imgurl;
        }

        protected string FormatArticleDate(DateTime startDate)
        {
            if (config.DateTimeFormat == string.Empty) return string.Empty;
            string result = "";
            result = timeZone != null ? TimeZoneInfo.ConvertTimeFromUtc(startDate, timeZone).ToString(config.DateTimeFormat) : startDate.AddHours(timeOffset).ToString(config.DateTimeFormat);
            return result;
        }

    }
}
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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PollFeature.Business;

namespace PollFeature.UI
{
    public partial class QuestionList : UserControl
    {

        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;

        private mojoBasePage basePage;
        private Module module;

        protected PollConfiguration config = new PollConfiguration();
        private Guid pollGuid = Guid.Empty;
        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        private SiteSettings siteSettings;
        private bool? isApprove = null;
        private bool? isPublish = null;
        private int approve = -1;
        private int publish = -1;
        private string keyword = string.Empty;

        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkText = SwirlingQuestionResource.ButtonDelete;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkText = SwirlingQuestionResource.ButtonEdit;
        protected string EditLinkImageUrl = string.Empty;
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
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
        public PollConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }
        public int Publish
        {
            get { return publish; }
            set { publish = value; }
        }
        public int Approve
        {
            get { return approve; }
            set { approve = value; }
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
        public Guid PollGuid
        {
            get { return pollGuid; }
            set { pollGuid = value; }
        }
        public string ImageSiteRoot
        {
            get { return imageSiteRoot; }
            set { imageSiteRoot = value; }
        }
        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            btnSearch.Click += btnSearch_Click;
            btnAddNew.Click += btnAddNew_Click;
            btnRemoveAll.Click += btnRemoveAll_Click;
            rptPoll.ItemCommand += rptPoll_ItemCommand;
            rptPoll.ItemDataBound += rptPoll_ItemDataBound;
            btnActived.Click += btnActived_Click;
        }

        private void btnActived_Click(object sender, EventArgs e)
        {
            string url = SiteRoot + "/Poll/PollChoose.aspx?pageid=" + PageId + "&mid=" + ModuleId;
            WebUtils.SetupRedirect(this, url);
        }
        private void rptPoll_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;
            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {

                ImageButton ibtnApprove = e.Item.FindControl("ibtnApprove") as ImageButton;

                if (ibtnApprove != null)
                {

                    if (WebUser.IsInRoles(config.RoleApprove))
                    {
                        SiteUtils.AddConfirmButton(ibtnApprove, PollResources.PollConfirmApprove);
                    }
                    else
                    {
                        SiteUtils.AddMessageButton(ibtnApprove, PollResources.PollApproveError);
                        return;
                    }
                }

                ImageButton ibtnPublish = e.Item.FindControl("ibtnPublish") as ImageButton;
                if (ibtnPublish != null)
                {
                    SiteUtils.AddConfirmButton(ibtnPublish, PollResources.PollComfirmPublish);
                }
                ImageButton ibtnDelete = e.Item.FindControl("ibtnDelete") as ImageButton;
                if (ibtnDelete != null)
                {
                    SiteUtils.AddConfirmButton(ibtnDelete, PollResources.PollEditDeleteConfirmMessage);
                }
            }
        }
        protected string GetTime(DateTime startDate, DateTime endDate)
        {
            if (endDate < DateTime.Now)
            {
                return "<span style='color:red'>" + PollResources.PollTimeError + "</span>";
            }
            else
            {
                return String.Format("{0:dd/MM/yyyy HH:mm}", startDate) + " " + PollResources.PollToText + " " + String.Format("{0:dd/MM/yyyy HH:mm}", endDate);
            }
        }
        protected void rptPoll_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;

            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName.Equals("EditItem"))
                {
                    pollGuid = Guid.Parse(e.CommandArgument.ToString());
                    WebUtils.SetupRedirect(this, SiteRoot + "/Poll/PollEdit.aspx?PollGuid= " + pollGuid + "&pageid=" + PageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString());
                }
                else if (e.CommandName.Equals("DeleteItem"))
                {
                    pollGuid = Guid.Parse(e.CommandArgument.ToString());
                    Poll poll = new Poll(pollGuid);
                    poll.Delete();
                    WebUtils.SetupRedirect(this, SiteRoot + "/Poll/managepost.aspx?pageid="
                        + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString());
                }
                else if (e.CommandName.Equals("ApproveItem"))
                {
                    if (WebUser.IsInRoles(config.RoleApprove))
                    {
                        pollGuid = Guid.Parse(e.CommandArgument.ToString());
                        Poll poll = new Poll(pollGuid);
                        poll.UpdateApprove();
                        WebUtils.SetupRedirect(this, SiteRoot + "/Poll/managepost.aspx?pageid="
                            + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString());
                    }
                    else
                    {
                        WebUtils.SetupRedirect(this, SiteRoot + "/Poll/managepost.aspx?pageid="
                        + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString());
                    }
                }
                else if (e.CommandName.Equals("PublishItem"))
                {
                    pollGuid = Guid.Parse(e.CommandArgument.ToString());
                    Poll poll = new Poll(pollGuid);
                    poll.UpdatePublish();
                    WebUtils.SetupRedirect(this, SiteRoot + "/Poll/managepost.aspx?pageid="
                        + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString());
                }
            }
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            int deleteNumber = 0;
            foreach (RepeaterItem ri in rptPoll.Items)
            {
                CheckBox chkFlag = ri.FindControl("chk") as CheckBox;
                if (chkFlag.Checked)
                {
                    deleteNumber++;
                    pollGuid = Guid.Parse((ri.FindControl("repeaterID") as Literal).Text);
                    Poll p = new Poll(pollGuid);
                    p.Delete();
                }
            }
            string pageUrl = SiteRoot + "/Poll/managepost.aspx"
                  + "?pageid=" + PageId.ToInvariantString()
                  + "&mid=" + ModuleId.ToInvariantString()
                   + "&publish=" + publish.ToInvariantString()
                  + "&keyword=" + keyword
                  + "&pagenumber=1";
            WebUtils.SetupRedirect(this, pageUrl);
        }

        void btnAddNew_Click(object sender, EventArgs e)
        {
            string pageUrl = SiteRoot + "/Poll/PollEdit.aspx?pageid=" + pageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString();
            WebUtils.SetupRedirect(this, pageUrl);
        }

        void btnSearch_Click(object sender, EventArgs e)
        {
            string _publish = drlIsPublic.SelectedValue;
            keyword = txtSearch.Text;
            if (!string.IsNullOrEmpty(_publish))
            {
                publish = int.Parse(_publish);
                if (_publish == "1")
                {
                    isPublish = true;
                }
                else if (_publish == "0")
                {
                    isPublish = false;
                }
            }
            string _approve = drlIsApprove.SelectedValue;
            if (!string.IsNullOrEmpty(_approve))
            {
                approve = int.Parse(_approve);
                if (_approve == "1")
                {
                    isApprove = true;
                }
                else if (_approve == "0")
                {
                    isApprove = false;
                }
            }
            string pageUrl = SiteRoot + "/Poll/managepost.aspx"
                  + "?pageid=" + PageId.ToInvariantString()
                  + "&mid=" + ModuleId.ToInvariantString()
                  + "&publish=" + publish.ToInvariantString()
                  + "&approve=" + approve.ToInvariantString()
                  + "&keyword=" + keyword
                  + "&pagenumber=" + pageNumber;
            WebUtils.SetupRedirect(this, pageUrl);
        }
        private void PopulateStatus()
        {
            var published = SiteUtils.StringToDictionary(PollResources.PollPublishStatus.ToString(), ",");

            drlIsPublic.DataSource = published;
            drlIsPublic.DataTextField = "Value";
            drlIsPublic.DataValueField = "Key";
            drlIsPublic.DataBind();

            var approved = SiteUtils.StringToDictionary(PollResources.PollApproveStatus.ToString(), ",");

            drlIsApprove.DataSource = approved;
            drlIsApprove.DataTextField = "Value";
            drlIsApprove.DataValueField = "Key";
            drlIsApprove.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadParams();
            LoadSettings();
            PopulateLabels();
            if (!IsPostBack)
            {
                PopulateControls();
            }
        }
        protected bool SetVisibleOpinion(int? totalOponion)
        {
            if (totalOponion.HasValue && totalOponion.Value > 0)
            {
                return true;
            }
            return false;
        }
        protected bool SetVisibleApprove()
        {
            if (WebUser.IsInRoles(config.RoleApprove))
            {
                return true;
            }
            return false;
        }
        protected bool SetVisiblePublish()
        {
            if (WebUser.IsInRoles(config.RolePublish))
            {
                return true;
            }
            return false;
        }

        private void BindPoll()
        {
            EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
            DeleteLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;

        }
        private void PopulateControls()
        {
            BindPoll();
            PopulateStatus();
            if (publish >= 0)
            {
                drlIsPublic.SelectedValue = publish.ToString();
            }
            if (approve >= 0)
            {
                drlIsApprove.SelectedValue = approve.ToString();
            }
            if (!String.IsNullOrEmpty(keyword))
            {
                txtSearch.Text = keyword;
            }
            btnSearch.Text = "Tìm kiếm";

            using (IDataReader reader = Poll.GetPage(siteSettings.SiteId, pageNumber, config.PageSize, isPublish, isApprove, keyword, out totalPages))
            {
                rptPoll.DataSource = reader;
                rptPoll.DataBind();
                if (rptPoll.Items.Count == 0)
                {
                    Pollnull.Visible = true;
                    Pollnull.Text = PollResources.PollTitleNullData;
                    pnlPostList.Visible = false;
                    rptPoll.Visible = false;
                }
            }
            string pageUrl = SiteRoot + "/Poll/managepost.aspx"
                   + "?pageid=" + PageId.ToInvariantString()
                   + "&mid=" + ModuleId.ToInvariantString()
                   + "&publish=" + publish.ToInvariantString()
                   + "&approve=" + approve.ToInvariantString()
                   + "&keyword=" + keyword
                   + "&pagenumber={0}";

            pgrPoll.PageURLFormat = pageUrl;
            pgrPoll.ShowFirstLast = true;
            pgrPoll.PageSize = config.PageSize;
            pgrPoll.PageCount = totalPages;
            pgrPoll.CurrentIndex = pageNumber;
            pgrPoll.Visible = (totalPages > 1) && config.ShowPager;
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
        }
        private void PopulateLabels()
        {
            btnAddNew.Text = PollResources.PollButtonAddNew;
            btnRemoveAll.Text = PollResources.PollButtonRemoveAll;
            lengendPoll.InnerText = PollResources.PollSearchCriteria;
            btnActived.Text = PollResources.PollActivated;
            SiteUtils.AddConfirmButton(btnRemoveAll, "Dữ liệu sẽ bị xóa vĩnh viễn. Bạn có thực sự muốn xóa");
        }
        protected bool SetVisibleTotalVotes(int? totalVodes)
        {
            if (totalVodes.HasValue && totalVodes.Value > 0)
            {
                return true;
            }
            return false;
        }
        protected string GetApprove(bool approve)
        {
            string result = "";
            if (approve == true)
            {
                result = PollResources.PollApproved;
            }
            else
            {
                result = PollResources.PollUnApprove;
            }
            return result;
        }
        protected string GetPublish(bool publish)
        {
            string result = "";
            if (publish == true)
            {
                result = PollResources.PollPublished;
            }
            else
            {
                result = PollResources.PollUnPublish;
            }
            return result;
        }

        protected virtual void LoadSettings()
        {

            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new PollConfiguration(getModuleSettings);

            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            string _publish = WebUtils.ParseStringFromQueryString("publish", string.Empty);
            if (!string.IsNullOrEmpty(_publish))
            {
                publish = int.Parse(_publish);
                if (_publish == "1")
                {
                    isPublish = true;
                }
                else if (_publish == "0")
                {
                    isPublish = false;
                }
            }
            string _approve = WebUtils.ParseStringFromQueryString("approve", string.Empty);
            if (!string.IsNullOrEmpty(_approve))
            {
                approve = int.Parse(_approve);
                if (_approve == "1")
                {
                    isApprove = true;
                }
                else if (_approve == "0")
                {
                    isApprove = false;
                }
            }
            keyword = WebUtils.ParseStringFromQueryString("keyword", keyword);
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
        }



    }
}
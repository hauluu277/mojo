using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using QuestionAnswerFeatures.Business;
using QuestionAnswerFeatures.UI;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuestionAnswersFeatures.UI
{
    public partial class PostList : System.Web.UI.UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;
        private mojoBasePage basePage;
        private Module module;
        protected QuestionAnswerConfiguration config = new QuestionAnswerConfiguration();
        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
        private int groupMediaId = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        private SiteSettings siteSetting;
        private string keyword = string.Empty;
        private int status = -1;
        private int parentID = -1;
        private int loaiLinhVuc = -1;
        private int linhVucID = -1;
        private int orderBy = 1;
        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkImageUrl = string.Empty;
        protected string DetailAnswerIMG = string.Empty;
        protected string AnswerLinkImageUrl = string.Empty;
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        protected string StateLink = SwirlingQuestionResource.StateStatusTitle;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
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
        public QuestionAnswerConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }

        public string Keyword
        {
            get { return keyword; }
            set { keyword = value; }
        }
        public int ParentID
        {
            get { return parentID; }
            set { parentID = value; }
        }
        public string SiteRoot
        {
            get { return siteRoot; }
            set { siteRoot = value; }
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
            btnRemoveAll.Click += btnRemoveAll_Click;
            rptQuestion.ItemDataBound += rptQuestion_ItemDataBound;
            rptQuestion.ItemCommand += rptQuestion_ItemCommand;
            btnSearch.Click += btnSearch_Click;

        }


        void rptQuestion_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;

            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName.Equals("EditItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    WebUtils.SetupRedirect(this, SiteRoot + "/QuestionAnswers/editquestion.aspx?pageid=" + PageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString() + "&itemId=" + itemId);
                }
                else if (e.CommandName.Equals("DetailAnswerItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    WebUtils.SetupRedirect(this, SiteRoot + "/QuestionAnswers/ManagePostAnswer.aspx?pageid=" + PageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString() + "&questionId=" + itemId);
                }
                else if (e.CommandName.Equals("AnswerItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    WebUtils.SetupRedirect(this, SiteRoot + "/QuestionAnswers/editanswer.aspx?pageid=" + PageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString() + "&questionId=" + itemId);
                }
                else if (e.CommandName.Equals("DeleteItem"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    Answer.DeleteQuestionId(itemId);
                    QuestionAnswer.Delete(itemId);

                    linhVucID = string.IsNullOrEmpty(ddlLinhVuc.SelectedValue) ? linhVucID : int.Parse(ddlLinhVuc.SelectedValue);
                    status = string.IsNullOrEmpty(ddlStatus.SelectedValue) ? status : int.Parse(ddlStatus.SelectedValue);
                    orderBy = int.Parse(ddlOrderBy.SelectedValue);
                    keyword = txtSearch.Text;

                    string pageUrl = SiteRoot + "/QuestionAnswers/managepost.aspx"
                       + "?pageid=" + PageId.ToInvariantString()
                       + "&linhvucid=" + linhVucID.ToInvariantString()
                       + "&status=" + status.ToInvariantString()
                       + "&oderby=" + orderBy
                       + "&keyword=" + keyword
                       + "&pagenumber=" + pageNumber;
                    WebUtils.SetupRedirect(this, pageUrl);
                }
                else if (e.CommandName.Equals("EditStateItem"))
                {
                    var qID = int.Parse(e.CommandArgument.ToString());
                    QuestionAnswer doc = new QuestionAnswer(qID);
                    if (doc != null)
                    {
                        if (doc.IsApprove == true)
                        {
                            doc.IsApprove = false;
                            doc.Save();
                        }
                        else
                        {
                            doc.IsApprove = true;
                            doc.Save();
                        }
                    }

                    linhVucID = string.IsNullOrEmpty(ddlLinhVuc.SelectedValue) ? linhVucID : int.Parse(ddlLinhVuc.SelectedValue);
                    status = string.IsNullOrEmpty(ddlStatus.SelectedValue) ? status : int.Parse(ddlStatus.SelectedValue);
                    orderBy = int.Parse(ddlOrderBy.SelectedValue);
                    keyword = txtSearch.Text;

                    string pageUrl = SiteRoot + "/QuestionAnswers/managepost.aspx"
                       + "?pageid=" + PageId.ToInvariantString()
                       + "&linhvucid=" + linhVucID.ToInvariantString()
                       + "&status=" + status.ToInvariantString()
                       + "&oderby=" + orderBy
                       + "&keyword=" + keyword
                       + "&pagenumber=" + pageNumber;
                    WebUtils.SetupRedirect(this, pageUrl);
                }
            }
        }

        void rptQuestion_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;
            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                ImageButton ibtnChangeState = e.Item.FindControl("ibtnChangeState") as ImageButton;
                SiteUtils.AddConfirmButton(ibtnChangeState, "Thay đổi trạng thái kiểm duyệt câu hỏi?");
                ImageButton ibtnDelete = e.Item.FindControl("ibtnDelete") as ImageButton;
                SiteUtils.AddConfirmButton(ibtnDelete, "Dữ liệu này sẽ bị xóa, bạn có chắc chắn muốn tiếp tục?");
            }
        }

        void btnRemoveAll_Click(object sender, EventArgs e)
        {
            int deleteNumber = 0;
            foreach (RepeaterItem ri in rptQuestion.Items)
            {
                CheckBox chkFlag = ri.FindControl("chk") as CheckBox;
                if (chkFlag.Checked)
                {
                    deleteNumber++;
                    int itemid = Convert.ToInt32((ri.FindControl("repeaterID") as Literal).Text);
                    QuestionAnswer.Delete(itemid);
                    Answer.Delete(itemid);
                }
            }
            if (deleteNumber > 0)
            {
                linhVucID = string.IsNullOrEmpty(ddlLinhVuc.SelectedValue) ? linhVucID : int.Parse(ddlLinhVuc.SelectedValue);
                status = string.IsNullOrEmpty(ddlStatus.SelectedValue) ? status : int.Parse(ddlStatus.SelectedValue);
                orderBy = int.Parse(ddlOrderBy.SelectedValue);
                keyword = txtSearch.Text;
                string pageUrl = SiteRoot + "/QuestionAnswers/managepost.aspx"
                   + "?pageid=" + PageId.ToInvariantString()
                   + "&linhvucid=" + linhVucID.ToInvariantString()
                   + "&status=" + status.ToInvariantString()
                   + "&oderby=" + orderBy
                   + "&keyword=" + keyword
                   + "&pagenumber=1";
                WebUtils.SetupRedirect(this, pageUrl);
            }
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
            btnRemoveAll.BackColor = System.Drawing.Color.Red;
            btnRemoveAll.Text = SwirlingQuestionResource.RemoveAllButton;
            SiteUtils.AddConfirmButton(btnRemoveAll, SwirlingQuestionResource.RemoveAllConfirm);
            legendQuestionAnswer.InnerText = SwirlingQuestionResource.QuestionAnswerSearchTitle;
        }
        private void BindOrderBy()
        {
            var orderByStatus = SiteUtils.StringToDictionary(SwirlingQuestionResource.OrderByStatus, ",");
            ddlOrderBy.DataValueField = "Key";
            ddlOrderBy.DataTextField = "Value";
            ddlOrderBy.DataSource = orderByStatus;
            ddlOrderBy.DataBind();
        }
        private void PopulateControls()
        {
            BindQuestion();
            bindLinhVuc();
            PopulateStatus();
            BindOrderBy();
            if (status > 0)
            {
                ddlStatus.SelectedValue = status.ToString();
            }
            if (linhVucID > 0)
            {
                ddlLinhVuc.SelectedValue = linhVucID.ToString();
            }
            if(orderBy > 0)
            {
                ddlOrderBy.SelectedValue = orderBy.ToString();
            }


            if (!string.IsNullOrEmpty(keyword))
            {
                txtSearch.Text = keyword;
            }
        }

        private void BindQuestion()
        {
            EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
            DeleteLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;
            AnswerLinkImageUrl = imageSiteRoot + "/Data/SiteImages/answer24.png";

            DetailAnswerIMG = ImageSiteRoot + "/Data/SiteImages/detailAnswer.png";

            int deptId = -1;
            if (siteUser.IsInRoles("Admins") == false)
            {
                deptId = siteUser.DepartmentId.GetValueOrDefault(0);
            }
            List<QuestionAnswer> reader = QuestionAnswer.GetPage(siteSetting.SiteId, linhVucID, loaiLinhVuc, status, orderBy, keyword.ConvertToFTS(), pageNumber, config.RecentListPageSize, deptId, out totalPages);
            rptQuestion.DataSource = reader;
            rptQuestion.DataBind();
            string pageUrl = SiteRoot + "/QuestionAnswers/managepost.aspx"
               + "?pageid=" + PageId.ToInvariantString()
               + "&linhvucid=" + linhVucID.ToInvariantString()
               + "&loailinhvuc=" + loaiLinhVuc.ToInvariantString()
               + "&status=" + status.ToInvariantString()
               + "&oderby=" + orderBy
               + "&keyword=" + keyword
               + "&pagenumber={0}";

            pgrQuestion.PageURLFormat = pageUrl;
            pgrQuestion.ShowFirstLast = true;
            pgrQuestion.PageSize = config.RecentListPageSize;
            pgrQuestion.PageCount = totalPages;
            pgrQuestion.CurrentIndex = pageNumber;
            pnlQuestion.Visible = (totalPages > 1);
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
        }
        protected virtual void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new QuestionAnswerConfiguration(getModuleSettings);

            siteSetting = CacheHelper.GetCurrentSiteSettings();
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            linhVucID = WebUtils.ParseInt32FromQueryString("linhvucid", linhVucID);
            loaiLinhVuc = WebUtils.ParseInt32FromQueryString("loailinhvuc", loaiLinhVuc);
            orderBy = WebUtils.ParseInt32FromQueryString("oderby", orderBy);
            keyword = WebUtils.ParseStringFromQueryString("keyword", keyword);
            status = WebUtils.ParseInt32FromQueryString("status", status);

            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
        }
        private void PopulateStatus()
        {
            var status = SiteUtils.StringToDictionary(ArticleResources.ArticleStatus.ToString(), ",");

            ddlStatus.DataSource = status;
            ddlStatus.DataTextField = "Value";
            ddlStatus.DataValueField = "Key";
            ddlStatus.DataBind();
        }
        private void bindLinhVuc()
        {
            //get all categories
            int CategoryConfig = siteSetting.CoreLinhVucVanBanQuyPham;
            List<CoreCategory> lstLinhVuc = CoreCategory.GetChildren(siteSetting.CoreLinhVucHoiDap);
            ddlLinhVuc.DataValueField = "ItemID";
            ddlLinhVuc.DataTextField = "Name";
            ddlLinhVuc.DataSource = lstLinhVuc;
            ddlLinhVuc.DataBind();
            ddlLinhVuc.Items.Insert(0, new ListItem("--" + SwirlingQuestionResource.LinhVucTitle + "--", ""));
        }

        protected virtual void btnSearch_Click(object sender, EventArgs e)
        {
            linhVucID = string.IsNullOrEmpty(ddlLinhVuc.SelectedValue) ? linhVucID : int.Parse(ddlLinhVuc.SelectedValue);
            status = string.IsNullOrEmpty(ddlStatus.SelectedValue) ? status : int.Parse(ddlStatus.SelectedValue);
            orderBy = int.Parse(ddlOrderBy.SelectedValue);
            keyword = txtSearch.Text;

            string pageUrl = SiteRoot + "/QuestionAnswers/managepost.aspx"
               + "?pageid=" + PageId.ToInvariantString()
               + "&linhvucid=" + linhVucID.ToInvariantString()
               + "&status=" + status.ToInvariantString()
               + "&oderby=" + orderBy
               + "&keyword=" + keyword
               + "&pagenumber=1";
            WebUtils.SetupRedirect(this, pageUrl);
        }
    }
}
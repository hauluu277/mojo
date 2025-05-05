using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using QuestionAnswerFeatures.Business;
using Resources;
using SurveyFeature.Business;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuestionAnswerFeatures.UI
{
    public partial class PostListAnswer : System.Web.UI.UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;
        private mojoBasePage basePage;
        private Module module;
        protected QuestionAnswerConfiguration config = new QuestionAnswerConfiguration();
        private int pageId = -1;
        private int moduleId = -1;
        private int questionId = -1;
        private int groupMediaId = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        private SiteSettings siteSetting;
        private string keyword = string.Empty;
        private int status = -1;
        private int parentID = -1;
        private int loaiLinhVuc = -1;
        private int linhVucID = -1;
        private string cityCode = string.Empty;
        private string districtCode = string.Empty;
        private int orderBy = 1;
        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkImageUrl = string.Empty;
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        protected string StateLink = SwirlingQuestionResource.StateStatusTitle;
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
            btnQuestionManage.Click += btnQuestionManage_Click;
            btnAnswer.Click += btnAnswer_Click;

        }

        void btnAnswer_Click(object sender, EventArgs e)
        {
            WebUtils.SetupRedirect(this, SiteRoot + "/QuestionAnswers/editanswer.aspx?pageid=" + PageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString() + "&questionId=" + questionId);
        }

        void btnQuestionManage_Click(object sender, EventArgs e)
        {
            string url = SiteRoot + "/QuestionAnswers/ManagePost.aspx?pageid=" + pageId + "&mid=" + moduleId;
            SiteUtils.RedirectToUrl(url);
        }


        void rptQuestion_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;

            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName.Equals("EditItem"))
                {
                    var answerId = int.Parse(e.CommandArgument.ToString());
                    WebUtils.SetupRedirect(this, SiteRoot + "/QuestionAnswers/editanswer.aspx?pageid=" + PageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString() + "&questionId=" + questionId + "&itemId=" + answerId);
                }
                else if (e.CommandName.Equals("DeleteItem"))
                {
                    var answerId = int.Parse(e.CommandArgument.ToString());
                    Answer.Delete(answerId);

                    status = string.IsNullOrEmpty(ddlStatus.SelectedValue) ? status : int.Parse(ddlStatus.SelectedValue);

                    string pageUrl = SiteRoot + "/QuestionAnswers/managepostanswer.aspx"
                       + "?pageid=" + PageId.ToInvariantString()
                       + "&questionId=" + questionId.ToInvariantString()
                       + "&status=" + status.ToInvariantString()
                       + "&pagenumber=1";
                    WebUtils.SetupRedirect(this, pageUrl);
                }
                else if (e.CommandName.Equals("EditStateItem"))
                {
                    var answerId = int.Parse(e.CommandArgument.ToString());
                    Answer doc = new Answer(answerId);
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
                    status = string.IsNullOrEmpty(ddlStatus.SelectedValue) ? status : int.Parse(ddlStatus.SelectedValue);
                    string pageUrl = SiteRoot + "/QuestionAnswers/managepostanswer.aspx"
                       + "?pageid=" + PageId.ToInvariantString()
                       + "&questionId=" + questionId.ToInvariantString()
                       + "&status=" + status.ToInvariantString()
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
                SiteUtils.AddConfirmButton(ibtnChangeState, "Thay đổi trạng thái kiểm duyệt câu trả lời?");
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
                    int questionId = Convert.ToInt32((ri.FindControl("repeaterID") as Literal).Text);
                    Answer.DeleteQuestionId(questionId);
                    Documentation.Delete(questionId);
                }
            }
            if (deleteNumber > 0)
            {
                status = string.IsNullOrEmpty(ddlStatus.SelectedValue) ? status : int.Parse(ddlStatus.SelectedValue);
                string pageUrl = SiteRoot + "/QuestionAnswers/managepostanswer.aspx"
                       + "?pageid=" + PageId.ToInvariantString()
                       + "&questionId=" + questionId.ToInvariantString()
                       + "&status=" + status.ToInvariantString()
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
                LoaiQuestionDetail();
                PopulateControls();
            }
        }
        private void LoaiQuestionDetail()
        {
            Business.QuestionAnswer QA = new Business.QuestionAnswer(questionId);
            if (QA != null)
            {
                lblNameSender.Text = "Người gửi: " + QA.Name;
                lblEmailSender.Text = "Email: " + QA.Email;
                lblDateSend.Text = "Ngày gửi: " + string.Format("{0:dd/MM/yyyy HH:mm}", QA.CreateDate);
                literQuestion.Text = QA.Question;
                literQuestion.Text = QA.ContentQuestion;
            }
        }
        private void PopulateLabels()
        {
            btnSearch.Text = ArticleResources.ArticleSearchButton;
            btnRemoveAll.BackColor = System.Drawing.Color.Red;
            btnRemoveAll.Text = SwirlingQuestionResource.RemoveAllButton;
            SiteUtils.AddConfirmButton(btnRemoveAll, SwirlingQuestionResource.RemoveAllConfirm);
            legendQuestionAnswer.InnerText = SwirlingQuestionResource.QuestionAnswerSearchTitle;
            btnAnswer.Text = "Trả lời";
        }

        private void PopulateControls()
        {
            BindAnswer();
            PopulateStatus();
            ddlStatus.SelectedValue = status.ToString();
        }

        private void BindAnswer()
        {
            EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
            DeleteLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;
            List<Answer> reader = Answer.GetPage(questionId, status, pageNumber, config.RecentListPageSize, out totalPages);
            rptQuestion.DataSource = reader;
            rptQuestion.DataBind();
            string pageUrl = SiteRoot + "/QuestionAnswers/managepostanswer.aspx"
                       + "?pageid=" + PageId.ToInvariantString()
                       + "&questionId=" + questionId.ToInvariantString()
                       + "&status=" + status.ToInvariantString()
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
            keyword = WebUtils.ParseStringFromQueryString("keyword", keyword);
            status = WebUtils.ParseInt32FromQueryString("status", status);
            questionId = WebUtils.ParseInt32FromQueryString("questionId", questionId);
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



        protected virtual void btnSearch_Click(object sender, EventArgs e)
        {
            status = string.IsNullOrEmpty(ddlStatus.SelectedValue) ? status : int.Parse(ddlStatus.SelectedValue);

            string pageUrl = SiteRoot + "/QuestionAnswers/managepostanswer.aspx"
                       + "?pageid=" + PageId.ToInvariantString()
                       + "&questionId=" + questionId.ToInvariantString()
                       + "&status=" + status.ToInvariantString()
                       + "&pagenumber=1";
            WebUtils.SetupRedirect(this, pageUrl);
        }
    }
}
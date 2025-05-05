using mojoPortal.Business;
using mojoPortal.Web;
using QuestionAnswerFeatures.Business;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Web.Framework;
using mojoPortal.Business.WebHelpers;

namespace QuestionAnswerFeatures.UI
{
    public partial class EditAnswer : mojoBasePage
    {
        #region setup private propety
        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
        private int questionId = -1;
        private int pageNumber = 1;
        private int totalPages = 1;
        QuestionAnswerConfiguration config;
        private SiteSettings siteSetting;
        private readonly SiteUser user = SiteUtils.GetCurrentSiteUser();
        private string siteRoot = string.Empty;
        private QuestionAnswer qa = new QuestionAnswer();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        #endregion
        #region setup public propety
        public int PageID
        {
            get { return pageId; }
            set { pageId = value; }
        }
        public int ModuleID
        {
            get { return moduleId; }
            set { moduleId = value; }
        }
        public string SiteRoot
        {
            get { return siteRoot; }
            set { siteRoot = value; }
        }
        public SiteSettings SiteSetting
        {
            get { return siteSetting; }
            set { siteSetting = value; }
        }
        public QuestionAnswerConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }
        public QuestionAnswer QA
        {
            get { return qa; }
            set { qa = value; }
        }
        #endregion
        #region OnInit
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            btnSendAnswer.Click += btnSendAnswer_Click;
            btnApprove.Click += btnApprove_Click;
            btnDelete.Click += btnDelete_Click;
            btnComeBack.Click += btnComeBack_Click;
            SiteUtils.SetupEditor(editAnswer);
        }

        void btnComeBack_Click(object sender, EventArgs e)
        {
            string url = SiteRoot + "/QuestionAnswers/ManagePostAnswer.aspx?pageid=" + pageId + "&questionId=" + questionId;
            SiteUtils.RedirectToUrl(url);
        }

        void btnDelete_Click(object sender, EventArgs e)
        {
            Answer.Delete(itemId);
            string url = SiteRoot + "/QuestionAnswers/ManagePostAnswer.aspx?pageid=" + pageId + "&questionId=" + questionId;
            SiteUtils.RedirectToUrl(url);
        }

        void btnApprove_Click(object sender, EventArgs e)
        {
            Answer answer = new Answer(itemId);
            answer.IsApprove = !answer.IsApprove;
            answer.Save();
            string url = SiteRoot + "/QuestionAnswers/ManagePostAnswer.aspx?pageid=" + pageId + "&questionId=" + questionId;
            SiteUtils.RedirectToUrl(url);
        }
        void btnSendAnswer_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                if (user.IsInRoles("Admins"))
                {
                    string url = SiteRoot + "/QuestionAnswers/ManagePostAnswer.aspx?pageid=" + pageId + "&questionId=" + questionId;
                    SiteUtils.RedirectToUrl(url);
                }
                else
                {
                    string url = QA.ItemUrl.Replace("~", "");
                    SiteUtils.RedirectToUrl(url);
                }
            }
        }

        #endregion
        private bool Save()
        {
            Answer answer = new Answer(itemId);
            if (answer == null)
            {
                answer.UserID = siteUser.UserId;
            }
            answer.Name = txtAnswerSender.Text;
            answer.QuestionID = questionId;
            answer.AnswerName = editAnswer.Text;
            answer.CreateDate = DateTime.Now;
            answer.Email = txtAnswerEmail.Text;

            if (Request.IsAuthenticated)
            {
                if (siteUser != null && siteUser.IsInRoles("Admins"))
                {
                    answer.IsApprove = true;
                }
            }
            answer.Save();
            return true;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                SiteUtils.RedirectToLoginPage(this);
                return;
            }
            if (!WebUser.IsInRoles(WebConfigSettings.RoleManageHoiDap))
            {
                SiteUtils.RedirectToAccessDeniedPage(this);
                return;
            }
            SecurityHelper.DisableBrowserCache();

            LoadParam();
            LoadSettings();
            //if (!checkRole())
            //{
            //    SiteUtils.RedirectToEditAccessDeniedPage();
            //    return;
            //}
            PopulateLabel();
            if (!IsPostBack)
            {
                TitleControl.EditText = "Trả lời hỏi đáp";
                TitleControl.EditUrl = "/";
                TitleControl.LiteralExtraMarkup = "/";
                PopulateControls();
                BindAnswer();
            }
        }
        private bool checkRole()
        {
            if (user.IsInRoles("Admins"))
            {
                return true;
            }
            var answer = new Answer(itemId);
            if (answer != null)
            {
                if (!user.IsInRoles("Admins") & answer.UserID == user.UserId)
                {
                    return true;
                }
            }
            return false;
        }
        private void BindAnswer()
        {
            if (itemId > 0)
            {
                Answer answer = new Answer(itemId);
                if (answer != null)
                {
                    txtAnswerEmail.Text = answer.Email;
                    txtAnswerSender.Text = answer.Name;
                    editAnswer.Text = answer.AnswerName;
                    if (user.IsInRoles("Admins"))
                    {
                        if (answer.IsApprove)
                        {
                            SiteUtils.AddConfirmButton(btnApprove, "Không kiểm duyệt câu trả lời?"); ;
                            btnApprove.Text = "Hủy kiểm duyệt";
                        }
                        else
                        {
                            SiteUtils.AddConfirmButton(btnApprove, "Kiểm duyệt câu trả lời?"); ;
                            btnApprove.Text = "kiểm duyệt";
                        }
                    }
                }
            }
        }
        private void PopulateControls()
        {
            if (QA != null)
            {
                lblSender.Text = QA.Name;
                lblTimeSend.Text = string.Format("{0:dd/MM/yyyy HH:mm}", QA.CreateDate);

                lblTotal.Text = SwirlingQuestionResource.AnswerLabel + " " + Answer.GetCount(qa.ItemID, 1);
                lblTrong.Text = "Trong: ";
                lblView.Text = SwirlingQuestionResource.ViewLabel + " " + QA.Views;
                literContent.Text = QA.ContentQuestion;
                lblQuestion.Text = QA.Question;
                if (Request.IsAuthenticated)
                {
                    if (siteUser != null)
                    {
                        txtAnswerEmail.Text = siteUser.Email;
                        txtAnswerSender.Text = siteUser.Name;
                    }
                }
                if (QA.LoaiLinhVucID > 0)
                {
                    CoreCategory childCategory = new CoreCategory(QA.LoaiLinhVucID);
                    hplChildCategory.ToolTip = childCategory.Name;
                    hplChildCategory.Text = childCategory.Name;
                    hplChildCategory.NavigateUrl = SiteRoot + childCategory.Description;
                }
                else
                {
                    CoreCategory category = new CoreCategory(QA.LinhVucID);
                    hplCategory.ToolTip = category.Name;
                    hplCategory.Text = category.Name;
                    hplCategory.NavigateUrl = SiteRoot + category.Description;
                }

            }
        }

        private void PopulateLabel()
        {
            btnSendAnswer.Text = "Lưu";
            if (itemId <= 0)
            {
                btnDelete.Visible = false;
                btnApprove.Visible = false;
            }
            else
            {
                btnDelete.Text = "Xóa";
            }
            if (!user.IsInRoles("Admins"))
            {
                btnApprove.Visible = false;
                btnComeBack.Visible = false;
            }
            Title = SiteUtils.FormatPageTitle(siteSettings, "Hỏi đáp");
            lblDetailContent.Text = SwirlingQuestionResource.DetailContentLabel;
            lblSenderLabel.Text = SwirlingQuestionResource.SenderLabel;
            lblTimeSendLabel.Text = SwirlingQuestionResource.TimeSenderLabel;
            btnComeBack.Text = "Quay lại";
        }
        private void LoadSettings()
        {
            editAnswer.WebEditor.Height = 150;
            siteSetting = CacheHelper.GetCurrentSiteSettings();
            //editAnswer.WebEditor.ToolBar = ToolBar.Newsletter;
        }
        private void LoadParam()
        {
            itemId = WebUtils.ParseInt32FromQueryString("itemId", itemId);
            pageId = WebUtils.ParseInt32FromQueryString("pageid", itemId);
            questionId = WebUtils.ParseInt32FromQueryString("questionId", questionId);
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            if (questionId > 0)
            {
                QA = new QuestionAnswer(questionId);
            }
        }
    }
}
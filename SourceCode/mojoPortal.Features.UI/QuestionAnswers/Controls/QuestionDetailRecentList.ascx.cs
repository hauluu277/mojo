using mojoPortal.Business;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuestionAnswerFeatures.Business;
using Resources;

namespace QuestionAnswerFeatures.UI
{
    public partial class QuestionDetailRecentList : System.Web.UI.UserControl
    {
        #region setup private propety
        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
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
            SiteUtils.SetupEditor(editAnswer);
        }
        void btnSendAnswer_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                string url = QA.ItemUrl;
                if (!Page.ClientScript.IsStartupScriptRegistered(UniqueID))
                {
                    Page.ClientScript.RegisterStartupScript(
                        GetType(),
                        UniqueID, "<script type='text/javascript'>alert('" + SwirlingQuestionResource.AnswerResult + "'); window.location='" + url + "'</script>");
                }
            }
            else
            {
                if (!Page.ClientScript.IsStartupScriptRegistered(UniqueID))
                {
                    Page.ClientScript.RegisterStartupScript(
                        GetType(),
                        UniqueID, "<script type='text/javascript'> $('.questionAnswerDetail').addClass('answerActive'); $('.questionAnswerDetail').css('display', 'block');</script>");
                }
            }
        }

        #endregion
        private bool Save()
        {
            if (string.IsNullOrEmpty(editAnswer.Text))
            {
                lblAnswerNull.Text = SwirlingQuestionResource.AnswerNull;
                update.Update();
                return false;
            }
            else
            {
                lblAnswerNull.Text = "";
            }
            if (captcha == null)
            {
                lblCaptchaError.Text = SwirlingQuestionResource.CaptchaNull;
                update.Update();
                return false;
            }
            if (!captcha.IsValid)
            {
                lblCaptchaError.Text = SwirlingQuestionResource.CaptchaError;
                update.Update();
                return false;
            }
            lblCaptchaError.Text = "";
            Answer answer = new Answer();
            answer.Name = txtAnswerSender.Text;
            answer.QuestionID = itemId;
            answer.AnswerName = editAnswer.Text;
            answer.CreateDate = DateTime.Now;
            answer.Email = txtAnswerEmail.Text;
            if (Request.IsAuthenticated)
            {
                if (siteUser != null)
                {
                    answer.UserID = siteUser.UserId;
                    if (siteUser.IsInRoles("Admins"))
                    {
                        answer.IsApprove = true;
                    }
                }

            }
            answer.Save();
            return true;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            LoadParam();
            LoadSettings();
            PopulateLabel();
            if (!IsPostBack)
            {
                PopulateControls();
                BindAnswer();
            }
        }
        private void PopulateControls()
        {
            if (QA != null)
            {
                lblSender.Text = QA.Name;
                lblTimeSend.Text = string.Format("{0:dd/MM/yyyy HH:mm}", QA.CreateDate);

                lblTotal.Text = "Phản hồi" + " " + Answer.GetCount(qa.ItemID, 1);
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
                    hplChildCategory.NavigateUrl = SiteRoot + SiteRoot + "/QuestionAnswers/ListQuestionAnswer.aspx?pageid=" + pageId + "&cateId=" + childCategory.ItemID + ""; ;
                }
                else
                {
                    CoreCategory category = new CoreCategory(QA.LinhVucID);
                    hplCategory.ToolTip = category.Name;
                    hplCategory.Text = category.Name;
                    hplCategory.NavigateUrl = SiteRoot + "/QuestionAnswers/ListQuestionAnswer.aspx?pageid=" + pageId + "&cateId=" + category.ItemID + "";
                }

            }
        }
        private void BindAnswer()
        {
            List<Answer> reader = Answer.GetPage(itemId, 1, pageNumber, config.RecentListPageSize, out totalPages);
            rptAnswer.DataSource = reader;
            rptAnswer.DataBind();
            lblTotalAnswer.InnerText = "Thông tin phản hồi (" + reader.Count + ")";
            string pageUrl = SiteRoot + "/QuestionAnswers/QuestionDetail.aspx"
                       + "?pageid=" + pageId.ToInvariantString()
                       + "&item=" + itemId.ToInvariantString()
                       + "&pagenumber={0}";
            pgrQuestion.PageURLFormat = pageUrl;
            pgrQuestion.ShowFirstLast = true;
            pgrQuestion.PageSize = config.RecentListPageSize;
            pgrQuestion.PageCount = totalPages;
            pgrQuestion.CurrentIndex = pageNumber;
            pnlQuestion.Visible = (totalPages > 1);
            pnlListAnswer.Visible = reader.Count > 0;
        }
        private void PopulateLabel()
        {
            lblDetailContent.Text = SwirlingQuestionResource.DetailContentLabel;
            lblSenderLabel.Text = SwirlingQuestionResource.SenderLabel;
            lblTimeSendLabel.Text = SwirlingQuestionResource.TimeSenderLabel;
            hplPostQA.NavigateUrl = SiteRoot + "/dang-cau-hoi";

        }
        private void LoadSettings()
        {
            captcha.ProviderName = siteSetting.CaptchaProvider;
            captcha.Captcha.ControlID = "captcha" + moduleId;
            captcha.RecaptchaPrivateKey = siteSetting.RecaptchaPrivateKey;
            captcha.RecaptchaPublicKey = siteSetting.RecaptchaPublicKey;
            editAnswer.WebEditor.Height = 150;
            //editAnswer.WebEditor.ToolBar = ToolBar.Newsletter;
        }
        private void LoadParam()
        {
            itemId = WebUtils.ParseInt32FromQueryString("itemId", itemId);
            pageId = WebUtils.ParseInt32FromQueryString("pageid", itemId);
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
        }
    }
}
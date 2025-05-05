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
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuestionAnswersFeature.UI
{
    public partial class QuestionAnswerUser : System.Web.UI.UserControl
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
        private readonly SiteUser user = SiteUtils.GetCurrentSiteUser();
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
        private int active = 0;
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
            btnAnswer.Click += btnAnswer_Click;
            btnQuestion.Click += btnQuestion_Click;
            rptAnswer.ItemDataBound += rptAnswer_ItemDataBound;
            rptAnswer.ItemCommand += rptAnswer_ItemCommand;
            rptQuestion.ItemDataBound += rptQuestion_ItemDataBound;
            rptQuestion.ItemCommand += rptQuestion_ItemCommand;
        }

        void rptAnswer_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;

            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName.Equals("EditItemAnswer"))
                {
                    var answerId = int.Parse(e.CommandArgument.ToString());
                    var answer = new Answer(answerId);
                    if (answer != null)
                    {
                        WebUtils.SetupRedirect(this, SiteRoot + "/QuestionAnswers/editanswer.aspx?pageid=" + PageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString() + "&questionId=" + answer.QuestionID + "&itemId=" + answerId);
                    }
                }
                else if (e.CommandName.Equals("DeleteItemAnswer"))
                {
                    var answerId = int.Parse(e.CommandArgument.ToString());
                    Answer.Delete(answerId);

                    string pageUrl = SiteRoot + "/QuestionAnswers/ListQAForUser.aspx"
                      + "?pageid=" + PageId.ToInvariantString()
                      + "&active=" + QuestionAnswerConstant.Answer
                      + "&pagenumber=1";
                    SiteUtils.RedirectToUrl(pageUrl);
                }
            }
        }

        void rptAnswer_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;
            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                ImageButton ibtnDeleteAnswer = e.Item.FindControl("ibtnDeleteAnswer") as ImageButton;
                SiteUtils.AddConfirmButton(ibtnDeleteAnswer, "Dữ liệu này sẽ bị xóa, bạn có chắc chắn muốn tiếp tục?");
            }
        }

        void btnQuestion_Click(object sender, EventArgs e)
        {
            string pageUrl = SiteRoot + "/QuestionAnswers/ListQAForUser.aspx"
               + "?pageid=" + PageId.ToInvariantString()
               + "&active=" + QuestionAnswerConstant.Question
               + "&pagenumber=1";
            SiteUtils.RedirectToUrl(pageUrl);
        }

        void btnAnswer_Click(object sender, EventArgs e)
        {
            string pageUrl = SiteRoot + "/QuestionAnswers/ListQAForUser.aspx"
           + "?pageid=" + PageId.ToInvariantString()
           + "&active=" + QuestionAnswerConstant.Answer
           + "&pagenumber=1";
            SiteUtils.RedirectToUrl(pageUrl);
        }


        void rptQuestion_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ListItemType itemType = e.Item.ItemType;

            if (itemType == ListItemType.Item || itemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName.Equals("EditItemQA"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    WebUtils.SetupRedirect(this, SiteRoot + "/QuestionAnswers/editquestion.aspx?pageid=" + PageId.ToInvariantString() + "&mid=" + moduleId.ToInvariantString() + "&itemId=" + itemId);
                }
                else if (e.CommandName.Equals("DeleteItemQA"))
                {
                    itemId = int.Parse(e.CommandArgument.ToString());
                    Answer.DeleteQuestionId(itemId);
                    QuestionAnswer.Delete(itemId);
                    string pageUrl = SiteRoot + "/QuestionAnswers/ListQAForUser.aspx"
                       + "?pageid=" + PageId.ToInvariantString()
                        + "&active=" + active.ToInvariantString()
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
                ImageButton ibtnDeleteQA = e.Item.FindControl("ibtnDeleteQA") as ImageButton;
                SiteUtils.AddConfirmButton(ibtnDeleteQA, "Dữ liệu này sẽ bị xóa, bạn có chắc chắn muốn tiếp tục?");
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
            if (active == QuestionAnswerConstant.Question)
            {
                btnQuestion.CssClass = "tab-active";
            }
            else
            {
                btnAnswer.CssClass = "tab-active";
            }
        }
        private void PopulateControls()
        {
            pnlShowAnswer.Visible = false;
            pnlShowQuestion.Visible = false;
            if (active == QuestionAnswerConstant.Question)
            {
                BindQuestion();
            }
            else
            {
                BindAnswer();
            }
        }
        private void BindAnswer()
        {
            if (Request.IsAuthenticated)
            {
                EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
                DeleteLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;
                AnswerLinkImageUrl = imageSiteRoot + "/Data/SiteImages/answer24.png";

                DetailAnswerIMG = ImageSiteRoot + "/Data/SiteImages/detailAnswer.png";
                List<Answer> reader = Answer.GetPageForUser(user.UserId, pageNumber, config.RecentListPageSize, out totalPages);
                rptAnswer.DataSource = reader;
                rptAnswer.DataBind();
                string pageUrl = SiteRoot + "/QuestionAnswers/ListQAForUser.aspx"
                   + "?pageid=" + PageId.ToInvariantString()
                   + "&active=" + QuestionAnswerConstant.Answer
                   + "&pagenumber={0}";

                pgrAnswer.PageURLFormat = pageUrl;
                pgrAnswer.ShowFirstLast = true;
                pgrAnswer.PageSize = config.RecentListPageSize;
                pgrAnswer.PageCount = totalPages;
                pgrAnswer.CurrentIndex = pageNumber;
                pnlAnswer.Visible = totalPages > 1;
                pnlShowAnswer.Visible = true;
                pnlShowQuestion.Visible = false;
                lblAnswer.Visible = reader.Count == 0;
            }
        }

        private void BindQuestion()
        {
            if (Request.IsAuthenticated)
            {
                EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
                DeleteLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;
                AnswerLinkImageUrl = imageSiteRoot + "/Data/SiteImages/answer24.png";

                DetailAnswerIMG = ImageSiteRoot + "/Data/SiteImages/detailAnswer.png";
                List<QuestionAnswer> reader = QuestionAnswer.GetPageForUser(siteSetting.SiteId, user.UserId, pageNumber, config.RecentListPageSize, out totalPages);
                rptQuestion.DataSource = reader;
                rptQuestion.DataBind();
                string pageUrl = SiteRoot + "/QuestionAnswers/ListQAForUser.aspx"
                   + "?pageid=" + PageId.ToInvariantString()
                   + "&active=" + QuestionAnswerConstant.Question
                   + "&pagenumber={0}";

                pgrQuestion.PageURLFormat = pageUrl;
                pgrQuestion.ShowFirstLast = true;
                pgrQuestion.PageSize = config.RecentListPageSize;
                pgrQuestion.PageCount = totalPages;
                pgrQuestion.CurrentIndex = pageNumber;
                pnlQuestion.Visible = (totalPages > 1);

                pnlShowAnswer.Visible = false;
                pnlShowQuestion.Visible = true;
                lblQuestionNull.Visible = reader.Count == 0;
            }
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            active = WebUtils.ParseInt32FromQueryString("active", active);
        }
        protected virtual void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new QuestionAnswerConfiguration(getModuleSettings);

            siteSetting = CacheHelper.GetCurrentSiteSettings();
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);

            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
        }
    }
}
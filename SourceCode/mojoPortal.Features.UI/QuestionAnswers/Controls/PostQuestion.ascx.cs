using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuestionAnswerFeatures.Business;
using System.Collections;

namespace QuestionAnswerFeatures.UI
{
    public partial class PostQuestion : System.Web.UI.UserControl
    {

        #region setup private propety
        private int pageId = -1;
        private int moduleId = -1;
        private Guid itemGuid = Guid.Empty;
        protected Double timeOffset;
        private TimeZoneInfo timeZone;
        QuestionAnswerConfiguration config = new QuestionAnswerConfiguration();
        readonly SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
        protected string EditLinkImageUrl = string.Empty;
        private readonly SiteUser user = SiteUtils.GetCurrentSiteUser();
        private bool send = false;
        private Guid guid = Guid.Empty;
        private int state = 1;
        private int itemId = -1;
        private string siteRoot = string.Empty;
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

        #endregion
        #region OnInit
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            btnSend.Click += btnSend_Click;
            ddlCategories.SelectedIndexChanged += ddlCategories_SelectedIndexChanged;
        }



        void ddlCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlTypeCategory.Visible = false;
            if (ddlCategories.SelectedValue != null && int.Parse(ddlCategories.SelectedValue) > 0)
            {
                List<CoreCategory> ListCategory = CoreCategory.GetChildren(int.Parse(ddlCategories.SelectedValue));
                if (ListCategory != null && ListCategory.Count > 0)
                {
                    pnlTypeCategory.Visible = true;
                    ddlTypeCategory.DataValueField = "ItemID";
                    ddlTypeCategory.DataTextField = "Name";
                    ddlTypeCategory.DataSource = ListCategory;
                    ddlTypeCategory.DataBind();

                    ddlTypeCategory.Items.Insert(0, new ListItem { Text = SwirlingQuestionResource.ChooseTypeCategory, Value = "0" });
                }
            }

        }

        void btnSend_Click(object sender, EventArgs e)
        {
            if (!Save()) return;
            if (!Page.ClientScript.IsStartupScriptRegistered(UniqueID))
            {
                Page.ClientScript.RegisterStartupScript(
                    GetType(),
                    UniqueID, "<script type='text/javascript'>alert('" + SwirlingQuestionResource.PostQuestionResult + "'); window.location='" + siteSetting.SiteRoot + "/hoi-dap'</script>");
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSetting();
            PopualateLabel();
            if (!IsPostBack)
            {
                PopulateControl();
            }
        }
        private void PopulateControl()
        {
            LoadCategory();

        }

        private void LoadCategory()
        {
            List<CoreCategory> ListCategory = CoreCategory.GetChildren(siteSetting.CoreLinhVucHoiDap);
            ddlCategories.DataValueField = "ItemID";
            ddlCategories.DataTextField = "Name";
            ddlCategories.DataSource = ListCategory;
            ddlCategories.DataBind();

            ddlCategories.Items.Insert(0, new ListItem { Text = SwirlingQuestionResource.ChooseCategory, Value = "0" });
        }

        private void PopualateLabel()
        {
            //PageTitle = SiteUtils.FormatPageTitle(siteSettings, SwirlingQuestionResource.SwirlingQuestionEditPage);
            rfvTitle.ErrorMessage = SwirlingQuestionResource.TitleQuestionRequired;
            rfvCategories.ErrorMessage = SwirlingQuestionResource.CategoryQuestionRequired;
            rfvCreatedName.ErrorMessage = SwirlingQuestionResource.NameRequiredError;
            rfvCreatedEmail.ErrorMessage = SwirlingQuestionResource.EmailRegularExpression;
            revEmail.ErrorMessage = SwirlingQuestionResource.EmailRequiredError;
            rgePhone.ErrorMessage = SwirlingQuestionResource.PhoneRegularExpression;
            rfvQuestion.ErrorMessage = SwirlingQuestionResource.ContentQuestionRequired;
            btnSend.Text = SwirlingQuestionResource.ButtonSendQuestion;
            if (Request.IsAuthenticated)
            {
                if (user != null)
                {
                    txtCreatedName.Text = user.Name;
                    //txtCreatedPhone.Text = user.Phone;
                }
            }
            //edQuestion.WebEditor.ToolBar = ToolBar.AnonymousUser;
        }

        private bool Save()
        {
            Page.Validate("pageSwirlingQuestion");
            if (!Page.IsValid) { return false; }
            if (captcha == null)
            {
                lblError.Text = "Bạn chưa nhập mã an toàn!";
                updatePanelMessage.Update();
                return false;
            }
            if (!captcha.IsValid)
            {
                lblError.Text = SwirlingQuestionResource.WrongAnswer;
                updatePanelMessage.Update();
                return false;
            }
            #region Trim text Question
            if (!string.IsNullOrEmpty(txtCreatedEmail.Text))
            {
                txtCreatedEmail.Text = txtCreatedEmail.Text.Trim();
            }
            if (!string.IsNullOrEmpty(txtCreatedName.Text))
            {
                txtCreatedName.Text = txtCreatedName.Text.Trim();
            }
            if (!string.IsNullOrEmpty(txtCreatedPhone.Text))
            {
                txtCreatedPhone.Text = txtCreatedPhone.Text.Trim();
            }
            if (!string.IsNullOrEmpty(txtTitle.Text))
            {
                txtTitle.Text = txtTitle.Text.Trim();
            }
            if (!string.IsNullOrEmpty(txtContent.Text))
            {
                txtContent.Text = txtContent.Text.Trim();
            }
            #endregion
            string ItemUrl = SuggestUrl();
            Guid tempGuid = Guid.Empty;
            QuestionAnswer QA = new QuestionAnswer();
            QA.LinhVucID = int.Parse(ddlCategories.SelectedValue);
            if (ddlTypeCategory.SelectedValue != null && ddlTypeCategory.SelectedValue != "")
            {
                QA.LoaiLinhVucID = int.Parse(ddlTypeCategory.SelectedValue);

            }
            QA.ModuleID = moduleId;
            QA.Name = txtCreatedName.Text;
            QA.PageID = pageId;
            QA.Email = txtCreatedEmail.Text;
            QA.Phone = txtCreatedPhone.Text;
            QA.Question = txtTitle.Text;
            QA.SiteID = siteSetting.SiteId;
            if (Request.IsAuthenticated)
            {
                if (user != null)
                {
                    QA.CreateByUser = user.UserId;
                    if (user.IsInRoles("Admins"))
                    {
                        QA.IsApprove = true;
                    }
                }
            }
            QA.ContentQuestion = txtContent.Text;
            QA.FTS = txtTitle.Text.ConvertToFTS() + " " + txtCreatedPhone.Text.ConvertToFTS() + " " + txtCreatedName.Text.ConvertToFTS() + " " + txtCreatedEmail.Text.ConvertToFTS() + " " + txtContent.Text.ConvertToFTS();
            String friendlyUrlString = SiteUtils.RemoveInvalidUrlChars(ItemUrl.Replace("~/", String.Empty));
            FriendlyUrl friendlyUrl = new FriendlyUrl(siteSetting.SiteId, friendlyUrlString);

            string oldUrl = QA.ItemUrl.Replace("~/", string.Empty);
            string newUrl = SiteUtils.RemoveInvalidUrlChars(ItemUrl.Replace("~/", string.Empty));
            ItemUrl = "~/" + newUrl;
            QA.ItemUrl = ItemUrl;
            QA.Save();
            if (!friendlyUrl.FoundFriendlyUrl)
            {
                if ((friendlyUrlString.Length > 0) && (!WebPageInfo.IsPhysicalWebPage("~/" + friendlyUrlString)))
                {
                    FriendlyUrl newFriendlyUrl = new FriendlyUrl
                    {
                        SiteId = siteSetting.SiteId,
                        SiteGuid = siteSetting.SiteGuid,
                        PageGuid = tempGuid,
                        Url = friendlyUrlString,
                        RealUrl = "~/QuestionAnswers/QuestionDetail.aspx?pageid=" + pageId.ToInvariantString()
                                + "&mid=" + moduleId.ToInvariantString()
                                + "&itemId=" + QA.ItemID
                    };

                    newFriendlyUrl.Save();
                }

                //if post was renamed url will change, if url changes we need to redirect from the old url to the new with 301
                if ((oldUrl.Length > 0) && (newUrl.Length > 0) && (!SiteUtils.UrlsMatch(oldUrl, newUrl)))
                {
                    //worry about the risk of a redirect loop if the page is restored to the old url again
                    // don't create it if a redirect for the new url exists
                    if (
                        (!RedirectInfo.Exists(siteSetting.SiteId, oldUrl))
                        && (!RedirectInfo.Exists(siteSetting.SiteId, newUrl))
                        )
                    {
                        RedirectInfo redirect = new RedirectInfo
                        {
                            SiteGuid = siteSetting.SiteGuid,
                            SiteId = siteSetting.SiteId,
                            OldUrl = oldUrl,
                            NewUrl = newUrl
                        };
                        redirect.Save();
                    }
                    // since we have created a redirect we don't need the old friendly url
                    FriendlyUrl oldFriendlyUrl = new FriendlyUrl(siteSetting.SiteId, oldUrl);
                    if ((oldFriendlyUrl.FoundFriendlyUrl) && (oldFriendlyUrl.PageGuid == tempGuid))
                    {
                        FriendlyUrl.DeleteUrl(oldFriendlyUrl.UrlId);
                    }

                }

            }
            return true;
        }
        protected string FormatQuestionUrl(string itemUrl, string item)
        {
            if (itemUrl.Length > 0)
                return SiteRoot + itemUrl.Replace("~", string.Empty);

            return SiteRoot + "/QuestionAnswers/QuestionDetail.aspx?pageid=" + pageId.ToInvariantString()
                + "&mid=" + moduleId.ToInvariantString()
                + "&itemId=" + item;
        }
        private string SuggestUrl()
        {
            if (!string.IsNullOrEmpty(txtTitle.Text))
            {
                txtTitle.Text = txtTitle.Text.Trim();
            }
            string pageName = txtTitle.Text;
            return SiteUtils.SuggestFriendlyUrl(pageName, siteSetting);
            //return SiteUtils.SuggestFriendlyUrl(pageName, siteSettings);
        }

        private void LoadParam()
        {

        }
        private void LoadSetting()
        {
            Hashtable settings = ModuleSettings.GetModuleSettings(moduleId);
            config = new QuestionAnswerConfiguration(settings);
            timeOffset = SiteUtils.GetUserTimeOffset();
            timeZone = SiteUtils.GetUserTimeZone();
            //if (Request.IsAuthenticated)
            //{
            //    captcha.Enabled = false;
            //    captcha.Visible = false;
            //    divCaptcha.Visible = false;
            //}
            //else
            //{
            captcha.ProviderName = siteSetting.CaptchaProvider;
            captcha.Captcha.ControlID = "captcha" + moduleId;
            captcha.RecaptchaPrivateKey = siteSetting.RecaptchaPrivateKey;
            captcha.RecaptchaPublicKey = siteSetting.RecaptchaPublicKey;
            //}
        }
    }
}
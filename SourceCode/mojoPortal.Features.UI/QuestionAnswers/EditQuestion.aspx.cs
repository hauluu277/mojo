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

namespace QuestionAnswerFeatures.UI
{
    public partial class EditQuestion : mojoBasePage
    {
        #region setup private propety
        private int pageId = -1;
        private int moduleId = -1;
        private Guid itemGuid = Guid.Empty;
        protected Double timeOffset;
        private QuestionAnswer QA;
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
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private bool userCanEdit;
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

        override protected void OnInit(EventArgs e)
        {
            LoadParams();
            Load += Page_Load;
            btnSend.Click += btnSend_Click;
            ddlCategories.SelectedIndexChanged += ddlCategories_SelectedIndexChanged;
            btnApprove.Click += btnApprove_Click;
            btnDelete.Click += btnDelete_Click;
            btnComeBack.Click += btnComeBack_Click;
            base.OnInit(e);
        }

        void btnComeBack_Click(object sender, EventArgs e)
        {
            string url = SiteRoot + "/QuestionAnswers/ManagePost.aspx?pageid=" + pageId + "&mid=" + moduleId;
            SiteUtils.RedirectToUrl(url);
        }

        void btnDelete_Click(object sender, EventArgs e)
        {
            if (itemId > 0)
            {
                Answer.DeleteQuestionId(itemId);
                QuestionAnswer.Delete(itemId);
                string url = SiteRoot + "/QuestionAnswers/ManagePost.aspx?pageid=" + pageId + "&mid=" + moduleId;
                SiteUtils.RedirectToUrl(url);
            }
        }

        void btnApprove_Click(object sender, EventArgs e)
        {
            if (itemId > 0)
            {
                QuestionAnswer QA = new QuestionAnswer(itemId);
                if (QA != null)
                {
                    QA.IsApprove = !QA.IsApprove;
                    QA.Save();
                    string url = SiteRoot + "/QuestionAnswers/ManagePost.aspx?pageid=" + pageId + "&mid=" + moduleId;
                    SiteUtils.RedirectToUrl(url);
                }
            }
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
            if (user.IsInRoles("Admins"))
            {
                string url = SiteRoot + "/QuestionAnswers/ManagePost.aspx?pageid=" + pageId + "&mid=" + moduleId;
                SiteUtils.RedirectToUrl(url);
            }
            else
            {
                string url = QA.ItemUrl.Replace("~", "");
                SiteUtils.RedirectToUrl(url);
            }
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
            LoadSettings();
            //if (!checkRole())
            //{
            //    SiteUtils.RedirectToEditAccessDeniedPage();
            //    return;
            //}
            PopulateLabels();
            if (!IsPostBack)
            {
                PopulateControl();
            }
        }
        private void PopulateControl()
        {
            BindDepartment();
            LoadCategory();
            LoadQuestion();

        }
        private bool checkRole()
        {
            if (siteUser.IsInRoles("Admins"))
            {
                return true;
            }
            if (!siteUser.IsInRoles("Admins") && QA.CreateByUser == user.UserId)
            {
                return true;
            }
            return false;
        }

        private void BindDepartment()
        {
            List<CoreCategory> lstCoQuan = CoreCategory.GetChildren(1, WebConfigSettings.DM_Department);
            ddlDepartment.DataValueField = "ItemID";
            ddlDepartment.DataTextField = "Name";
            ddlDepartment.DataSource = lstCoQuan;
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("--Chọn--", "0"));
        }
        private void LoadQuestion()
        {
            if (QA != null)
            {
                txtContent.Text = QA.ContentQuestion;
                txtCreatedEmail.Text = QA.Email;
                txtCreatedName.Text = QA.Name;
                txtCreatedPhone.Text = QA.Phone;
                txtTitle.Text = QA.Question;
                if (QA.DepartmentId.HasValue)
                {
                    ddlDepartment.SelectedValue = QA.DepartmentId.ToString();
                }
                if (QA.IsApprove)
                {
                    btnApprove.Text = "Hủy xuất bản";
                    SiteUtils.AddConfirmButton(btnApprove, "Bạn có chắc chắn muốn hủy xuất bản câu hỏi này");
                }
                else
                {
                    SiteUtils.AddConfirmButton(btnApprove, "Bạn có chắc chắn muốn xuất bản câu hỏi này");
                    btnApprove.Text = "Xuất bản";
                }
                if (QA.LinhVucID > 0)
                {
                    ddlCategories.SelectedValue = QA.LinhVucID.ToString();
                    List<CoreCategory> ListCategory = CoreCategory.GetChildren(QA.LinhVucID);
                    if (ListCategory != null && ListCategory.Count > 0)
                    {
                        ddlTypeCategory.DataSource = ListCategory;
                        ddlTypeCategory.DataBind();
                        ddlTypeCategory.DataValueField = "ItemID";
                        ddlTypeCategory.DataTextField = "Name";
                        ddlTypeCategory.Items.Insert(0, new ListItem { Text = SwirlingQuestionResource.ChooseTypeCategory, Value = "0" });
                        if (QA.LinhVucID > 0)
                        {
                            ddlTypeCategory.SelectedValue = QA.LinhVucID.ToString();
                        }

                    }
                }
            }
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


        private bool Save()
        {
            Page.Validate("validateQuestion");
            if (!Page.IsValid) { return false; }
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
            Guid tempGuid = Guid.Empty;
            string ItemUrl = SuggestUrl(QA.Question, QA.ItemUrl);

            QA.LinhVucID = int.Parse(ddlCategories.SelectedValue);
            if (ddlTypeCategory.SelectedValue != null && ddlTypeCategory.SelectedValue != "")
            {
                QA.LoaiLinhVucID = int.Parse(ddlTypeCategory.SelectedValue);

            }
            if (!string.IsNullOrEmpty(ddlDepartment.SelectedValue))
            {
                QA.DepartmentId = ddlDepartment.SelectedValue.ToIntOrNULL();
            }
            QA.ModuleID = moduleId;
            QA.Name = txtCreatedName.Text;
            QA.PageID = pageId;
            QA.Phone = txtCreatedPhone.Text;
            QA.Question = txtTitle.Text;
            QA.SiteID = siteSetting.SiteId;
            QA.ContentQuestion = txtContent.Text;
            if (Request.IsAuthenticated)
            {
                if (user != null)
                {
                    if (user.IsInRoles("Admins"))
                    {
                        QA.IsApprove = true;
                    }
                }
            }
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
            else
            {
                friendlyUrl.RealUrl = "~/QuestionAnswers/QuestionDetail.aspx?pageid=" + pageId.ToInvariantString()
                + "&mid=" + moduleId.ToInvariantString()
                + "&itemId=" + QA.ItemID;
                friendlyUrl.Save();
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
        private string SuggestUrl(string tieude, string Urlold)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTitle.Text))
                {
                    txtTitle.Text = txtTitle.Text.Trim();
                }
                if (tieude.ToLower().Equals(txtTitle.Text.ToLower()))
                {
                    return Urlold;
                }
                else
                {
                    string pageName = txtTitle.Text;
                    return SiteUtils.SuggestFriendlyUrl(pageName, siteSetting);
                }
            }
            catch (Exception)
            {

                return Urlold;
            }

            //return SiteUtils.SuggestFriendlyUrl(pageName, siteSettings);
        }

        private void PopulateLabels()
        {
            SiteUtils.AddConfirmButton(btnDelete, "Bạn có chắc chắn muốn xóa câu hỏi này?");
            if (itemId <= 0)
            {
                btnDelete.Visible = false;
                btnApprove.Visible = false;
            }
            if (!user.IsInRoles("Admins"))
            {
                btnApprove.Visible = false;
                btnComeBack.Visible = false;
            }
            btnSend.Text = "Lưu";
            btnDelete.Text = "Xóa";

            Title = SiteUtils.FormatPageTitle(siteSettings, QA.Question);
            btnComeBack.Text = "Quay lại";
            //PageTitle = SiteUtils.FormatPageTitle(siteSettings, SwirlingQuestionResource.SwirlingQuestionEditPage);
            rfvTitle.ErrorMessage = SwirlingQuestionResource.TitleQuestionRequired;
            rfvCategories.ErrorMessage = SwirlingQuestionResource.CategoryQuestionRequired;
            rfvCreatedName.ErrorMessage = SwirlingQuestionResource.NameRequiredError;
            rfvCreatedEmail.ErrorMessage = SwirlingQuestionResource.EmailRegularExpression;
            revEmail.ErrorMessage = SwirlingQuestionResource.EmailRequiredError;
            rgePhone.ErrorMessage = SwirlingQuestionResource.PhoneRegularExpression;
            rfvQuestion.ErrorMessage = SwirlingQuestionResource.ContentQuestionRequired;
            //edQuestion.WebEditor.ToolBar = ToolBar.AnonymousUser;
        }

        private void LoadSettings()
        {
            userCanEdit = UserCanEditModule(moduleId);
            QA = new QuestionAnswer(itemId);
        }
        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            itemId = WebUtils.ParseInt32FromQueryString("itemId", itemId);
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(moduleId);
        }
    }
}
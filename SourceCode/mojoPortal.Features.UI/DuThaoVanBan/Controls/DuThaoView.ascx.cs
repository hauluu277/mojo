using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace DuThaoVanBanFeature.UI
{
    public partial class DuThaoView : System.Web.UI.UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;

        private mojoBasePage basePage;
        private Module module;
        protected DuThaoVanBanConfiguration config = new DuThaoVanBanConfiguration();

        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        private SiteSettings siteSettings;
        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkText = SwirlingQuestionResource.ButtonDelete;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkText = SwirlingQuestionResource.ButtonEdit;
        protected string EditLinkImageUrl = string.Empty;
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        protected string CreatedBy = string.Empty;
        protected string TitleComment = string.Empty;
        protected string CreatByDate = string.Empty;
        private TimeZoneInfo timeZone;
        protected Double TimeOffset;
        protected bool isRole = false;
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
        public string SiteRoot
        {
            get { return siteRoot; }
            set { siteRoot = value; }
        }

        public DuThaoVanBanConfiguration Config
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

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            mpComments.Height = 180;
            mpComments.WebEditor.Height = 180;
            SiteUtils.SetupEditor(mpComments);
            btnSubmit.Click += btnSubmit_Click;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSettings();

            PopulateLabels();
            PopulateControls();
        }

        private void PopulateLabels()
        {
            pncomment.Visible = false;
            //lblHeaderTitleComment.Text = DuThaoVanBanResources.HeaderTitleCommentLabel;
            lblHeaderForm.Text = DuThaoVanBanResources.CommentFormLabel;
            CreatByDate = DuThaoVanBanResources.CreateByDateLabel;
            CreatedBy = DuThaoVanBanResources.CreatedByLabel;
            TitleComment = DuThaoVanBanResources.TitleContentLabel;
            btnSubmit.Text = DuThaoVanBanResources.btnSaveLabel;
            //lbtitOrther.Text = DuThaoVanBanResources.TitOrtherLabel;
            rfvName.ErrorMessage = DuThaoVanBanResources.NameRequiredLabel;
            rfvEmail.ErrorMessage = DuThaoVanBanResources.EmailRequiredLabel;
            revEmail.ErrorMessage = DuThaoVanBanResources.FailEmailLabel;
            editLink.Visible = false;
            if (isRole)
            {
                editLink.Visible = true;
            }
        }

        private void PopulateControls()
        {
            DuThaoVanBan duthao = new DuThaoVanBan(itemId);
            if (duthao != null)
            {
                litTitle.Text = duthao.Title;
                litSummary.Text = duthao.Summary;

                lblLoaiVanBan.Text = duthao.LoaiVBName;
                lblLinhVuc.Text = duthao.LinhVucName;
                lblCoQuanBanHanh.Text = duthao.CoQuanBanHanhName;
                string startDate = string.Format("{0:dd/MM/yyyy}", duthao.StartDate);
                string endDate = string.Format("{0:dd/MM/yyyy}", duthao.EndDate);
                lblTime.Text = string.Format("{0} - {1}", startDate, endDate);


                //titDownload.Text = DuThaoVanBanResources.DownloadDocumentLabel;
                PopulateFile();
                PopulateCommentDraft();
                PopulateOrtherDraft();
                editLink.NavigateUrl = DuThaoVanBanUltils.FormatBlogTitleUrl(siteSettings.SiteRoot, duthao.ItemUrl, itemId, pageId, moduleId);



            }
            if (Request.IsAuthenticated)
            {
                txtName.Text = siteUser.Name;
                txtEmail.Text = siteUser.Email;
            }

        }
        private void PopulateFile()
        {
            List<FileDuThao> lstFile = new List<FileDuThao>();
            lstFile = FileDuThao.GetAllByDuThaoId(itemId);
            rptFile.DataSource = lstFile;
            rptFile.DataBind();
        }
        private void PopulateOrtherDraft()
        {
            DuThaoVanBan duthao = new DuThaoVanBan(itemId);
            List<DuThaoVanBan> lstOrther = new List<DuThaoVanBan>();
            lstOrther = DuThaoVanBan.GetOrther(duthao.SiteID, duthao.ModuleID, config.DuThaoOrtherNumber, itemId);
            rptOrther.DataSource = lstOrther;
            rptOrther.DataBind();
        }
        private void PopulateCommentDraft()
        {
            List<CommentsDraft> commentDraft = new List<CommentsDraft>();
            commentDraft = CommentsDraft.GetPage(itemId, 1, 1, pageNumber, config.PageSizeComment, out totalPages);
            if (commentDraft != null && commentDraft.Count > 0)
            {
                pncomment.Visible = true;
            }
            rptComment.DataSource = commentDraft;
            rptComment.DataBind();
            string pageUrl = SiteRoot + "/DuThaoVanBan/ViewPost.aspx"
                   + "?pageid=" + PageId.ToInvariantString()
                   + "&mid=" + ModuleId.ToInvariantString()
                   + "&item=" + itemId.ToInvariantString()
                   + "&pagenumber={0}";

            pgrComment.PageURLFormat = pageUrl;
            pgrComment.ShowFirstLast = true;
            pgrComment.PageSize = config.PageSizeComment;
            pgrComment.PageCount = totalPages;
            pgrComment.CurrentIndex = pageNumber;
            pgrComment.Visible = (totalPages > 1) && config.ShowPagerComment;
        }
        protected virtual void LoadSettings()
        {
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            itemId = WebUtils.ParseInt32FromQueryString("item", itemId);
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new DuThaoVanBanConfiguration(getModuleSettings);
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            DuThaoVanBan duthao = new DuThaoVanBan(itemId);
            if (duthao != null)
            {
                if (!string.IsNullOrEmpty(duthao.EndDate.ToString()) && duthao.EndDate <= DateTime.Now)
                {
                    pncmtdraft.Visible = false;
                }
            }
            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
            if (Request.IsAuthenticated)
            {
                captcha.Enabled = false;
                captcha.Visible = false;
                divCaptcha.Visible = false;
                if (WebUser.IsInRoles(config.RoleApprove))
                {
                    isRole = true;
                }
            }
            else
            {
                captcha.ProviderName = siteSettings.CaptchaProvider;
                captcha.Captcha.ControlID = "captcha" + ModuleId;
                captcha.RecaptchaPrivateKey = siteSettings.RecaptchaPrivateKey;
                captcha.RecaptchaPublicKey = siteSettings.RecaptchaPublicKey;
            }
        }
        protected string FormatBlogDate(DateTime startDate)
        {
            if (timeZone != null)
            {
                return TimeZoneInfo.ConvertTimeFromUtc(startDate, timeZone).ToString(config.DateTimeFormat);
            }
            return startDate.AddHours(TimeOffset).ToString(config.DateTimeFormat);
        }
        private void btnSubmit_Click(object sender, System.EventArgs e)
        {
            if (!Save()) return;
        }
        private bool Save()
        {
            Page.Validate("DongGopDuThaoVanBan");
            if (!Page.IsValid)
            {
                this.lblMessageError.Text = "Vui lòng hoàn thành Form theo hướng dẫn!";
                return false;
            }
            if (divCaptcha.Visible)
            {
                if (!captcha.IsValid)
                {
                    this.lblMessageError.Text = "Mã captcha không đúng!";
                    return false;
                }
            }
            if (string.IsNullOrEmpty(mpComments.Text))
            {
                lblCommentErrorMessage.Text = DuThaoVanBanResources.CommentRequiredLabel;
                return false;
            }
            DuThaoVanBan duthao = new DuThaoVanBan(itemId);
            CommentsDraft cmt = new CommentsDraft();
            cmt.Name = txtName.Text;
            cmt.ModuleID = duthao.PageID;
            cmt.PageID = duthao.ModuleID;
            cmt.SiteID = duthao.SiteID;
            cmt.DuThaoID = itemId;
            cmt.Email = txtEmail.Text;
            cmt.Address = txtAddress.Text;
            cmt.Mobile = txtMobile.Text;
            cmt.FTS = txtName.Text.ConvertToFTS() + " " + txtAddress.Text.ConvertToFTS() + " " + txtEmail.Text.ConvertToFTS() + " " + txtMobile.Text.ConvertToFTS() + " " + mpComments.Text.ConvertToFTS();
            cmt.IsApproved = false;
            cmt.IsPublished = false;
            cmt.DateCreated = DateTime.UtcNow;
            cmt.Comment = mpComments.Text;
            cmt.Save();
            this.pnlGopY.Visible = false;
            pnlSuccess.Visible = true;
            return true;
        }
        protected string BuildEditUrl()
        {
            return SiteRoot + "/DuThaoVanBan/EditPost.aspx?pageid=" + pageId + "&item=" +
                           itemId + "&mid=" + moduleId;
        }
        protected string formatContent(string Content)
        {
            if (!string.IsNullOrEmpty(Content))
            {
                return Regex.Replace(Content, "<(.|\n)*?>", string.Empty);
            }
            else
            {
                return string.Empty;
            }
        }
        protected string FormatArticleDate(DateTime datePromulgate)
        {
            if (config.DateTimeFormat == string.Empty) return string.Empty;
            return datePromulgate.ToString(config.DateTimeFormat);
        }
        //private void SetupScripts()
        //{
        //    if (!Page.ClientScript.IsClientScriptBlockRegistered("sarissa"))
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(GetType(), "sarissa", "<script src=\""
        //            + ResolveUrl("~/ClientScript/sarissa/sarissa.js") + "\" type=\"text/javascript\"></script>");
        //    }

        //    if (!Page.ClientScript.IsClientScriptBlockRegistered("sarissa_ieemu_xpath"))
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(GetType(), "sarissa_ieemu_xpath", "<script src=\""
        //            + ResolveUrl("~/ClientScript/sarissa/sarissa_ieemu_xpath.js") + "\" type=\"text/javascript\"></script>");
        //    }
        //    if (!Page.ClientScript.IsClientScriptBlockRegistered("friendlyurlsuggest"))
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(GetType(), "friendlyurlsuggest", "<script src=\""
        //            + ResolveUrl("~/ClientScript/friendlyurlsuggest_v2.js") + "\" type=\"text/javascript\"></script>");
        //    }

        //    string focusScript = string.Empty;
        //    if (itemId == -1) { focusScript = "document.getElementById('" + txt.ClientID + "').focus();"; }

        //    string hookupInputScript = "<script type=\"text/javascript\">"
        //        + "new UrlHelper( "
        //        + "document.getElementById('" + txtTitle.ClientID + "'),  "
        //        + "document.getElementById('" + txtItemUrl.ClientID + "'), "
        //        + "document.getElementById('" + hdnTitle.ClientID + "'), "
        //        + "document.getElementById('" + spnUrlWarning.ClientID + "'), "
        //        + "\"" + SiteRoot + "/UIUtils/BlogUrlSuggestService.ashx" + "\""
        //        + "); " + focusScript + "</script>";

        //    if (!Page.ClientScript.IsStartupScriptRegistered(UniqueID + "urlscript"))
        //    {
        //        Page.ClientScript.RegisterStartupScript(
        //            GetType(),
        //            UniqueID + "urlscript", hookupInputScript);
        //    }


        //}
    }
}
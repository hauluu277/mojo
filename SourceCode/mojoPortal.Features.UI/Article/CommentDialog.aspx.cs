using ArticleFeature.Business;
using mojoPortal.Business;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArticleFeature.UI
{
    public partial class CommentDialog : mojoDialogBasePage
    {
        protected int pageId = -1;
        protected int moduleId = -1;
        protected int itemId = -1;
        protected Guid commentGuid = Guid.Empty;
        private Article article = null;
        private Module module = null;
        private Hashtable moduleSettings = null;
        private ArticleConfiguration config = null;
        private Comment comment = null;
        private CommentRepository commentRepository = null;
        private SiteUser currentUser = null;
        private bool userCanEdit = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSettings();

            if (
                (!userCanEdit)
                || (commentGuid == Guid.Empty) || (article == null))
            {
                SiteUtils.RedirectToAccessDeniedPage(this);
                return;
            }

            PopulateLabels();

            if (!IsPostBack) { PopulateControls(); }

        }

        private void PopulateControls()
        {

        }

        private bool UserCanEditComment()
        {
            if (comment == null) { return false; }

            if (UserCanEditModule(moduleId, Article.FeatureGuid)) { return true; }

            if ((config.RequireApprovalForComments) && (comment.ModerationStatus == Comment.ModerationApproved)) { return false; } // no edits by user after moderation

            if ((currentUser != null) && (comment.UserGuid == currentUser.UserGuid))
            {
                if ((!config.RequireApprovalForComments) || (comment.ModerationStatus == Comment.ModerationPending))
                {
                    TimeSpan t = DateTime.UtcNow - comment.CreatedUtc;
                    if (t.Minutes < config.AllowedEditMinutesForUnModeratedPosts)
                    {
                        return true;
                    }
                }
            }

            return false;

        }

        private void PopulateLabels()
        {
            //Title = ContactFormResources.ContactFormViewMessagesLink;

        }

        private void LoadSettings()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            itemId = WebUtils.ParseInt32FromQueryString("ItemID", itemId);
            commentGuid = WebUtils.ParseGuidFromQueryString("c", commentGuid);
            if (commentGuid == Guid.Empty) { return; }

            article = new Article(itemId);
            module = GetModule(moduleId, Article.FeatureGuid);
            commentRepository = new CommentRepository();

            if (article.ModuleID != module.ModuleId)
            {
                article = null;
                module = null;
                return;
            }

            comment = commentRepository.Fetch(commentGuid);
            if ((comment.ContentGuid != article.ArticleGuid) || (comment.ModuleGuid != module.ModuleGuid))
            {
                article = null;
                module = null;
                return;
            }

            moduleSettings = ModuleSettings.GetModuleSettings(moduleId);

            config = new ArticleConfiguration(moduleSettings);

            currentUser = SiteUtils.GetCurrentSiteUser();

            userCanEdit = UserCanEditComment();

            commentEditor.SiteGuid = CurrentSite.SiteGuid;
            commentEditor.SiteId = CurrentSite.SiteId;
            commentEditor.SiteRoot = SiteRoot;
            commentEditor.CommentsClosed = false;
            //commentEditor.CommentUrl = Request.RawUrl;
            commentEditor.ContentGuid = article.ArticleGuid;
            //commentEditor.DefaultCommentTitle = defaultCommentTitle;
            commentEditor.FeatureGuid = Blog.FeatureGuid;
            commentEditor.ModuleGuid = module.ModuleGuid;
            //commentEditor.NotificationAddresses = notificationAddresses;
            //commentEditor.NotificationTemplateName = notificationTemplateName;
            commentEditor.RequireCaptcha = false;
            commentEditor.UserCanModerate = userCanEdit;
            //commentEditor.Visible = !commentsClosed;
            commentEditor.CurrentUser = currentUser;
            commentEditor.UserComment = comment;
            commentEditor.ShowRememberMe = false;
            //commentEditor.IncludeIpAddressInNotification = includeIpAddressInNotification;
            //commentEditor.ContainerControl = this;

        }

        override protected void OnInit(EventArgs e)
        {
            this.Load += new EventHandler(this.Page_Load);

            base.OnInit(e);


        }
    }
}
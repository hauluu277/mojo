using ArticleFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Web.Framework;
using Resources;
using Utilities;
using System.Data;
using System.Globalization;
using EventFeature.Business;
using PollFeature.UI.Helpers;
using PollFeature.Business;
using mojoPortal.Web.UI;
using mojoPortal.Web.Editor;
using System.IO;

namespace ArticleFeature.UI
{
    public partial class ArticleViewSite : UserControl, IRefreshAfterPostback, IUpdateCommentStats
    {

        #region Properties

        private Hashtable moduleSettings;
        protected ArticleConfiguration config = new ArticleConfiguration();
        private Article article;
        private ArticleReference articleRefer;
        private List<Article> otherArticles;
        private Module module;
        protected string DeleteLinkImage = "~/Data/SiteImages/" + WebConfigSettings.DeleteLinkImage;
        private const string newWindowMarkup = "onclick=\"window.open(this.href, '', 'resizable=no,status=no,location=no,toolbar=no,menubar=no,fullscreen=no,scrollbars=no,dependent=no');return false;\"";
        protected int PageId = -1;
        protected int ModuleId = -1;
        protected int ItemId = -1;
        protected int ItemIdRefer = -1;
        protected int langId = -1;
        protected int LangID = -1;
        protected int type = -1;
        protected string OrtherArticle = string.Empty;
        protected string DatePost = string.Empty;
        protected string ItemUrl = string.Empty;
        protected bool AllowComments;
        private static readonly SiteSettings siteSettings = CacheHelper.GetCurrentSiteSettings();
        protected string CommentDateTimeFormat;
        protected bool parametersAreInvalid;
        protected Double TimeOffset;
        private TimeZoneInfo timeZone;

        protected bool IsEditable;
        protected string EditContentImage = ConfigurationManager.AppSettings["EditContentImage"];
        protected string GmapApiKey = string.Empty;

        protected string blogAuthor = string.Empty;
        protected string SiteRoot = string.Empty;
        protected string ImageSiteRoot = string.Empty;
        private mojoBasePage basePage;

        private int cat = 0;
        private int categoryID;
        private int siteID;
        private string DisqusSiteShortName = string.Empty;
        private string IntenseDebateAccountId = string.Empty;

        protected string RegexRelativeImageUrlPatern = @"^/.*[_a-zA-Z0-9]+\.(png|jpg|jpeg|gif|PNG|JPG|JPEG|GIF)$";
        public SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
        protected string fileAudio = string.Empty;
        private Guid PollGuid = new Guid();
        private Poll poll;
        private SiteUser currentUser = null;
        private bool userHasVoted = false;
        private bool showMyPollHistoryLink = false;
        private string resultBarColor = "#3082af";

        private CommentsWidget comments = null;
        protected string CommentItemHeaderElement = "h4";
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        private static int mid = -1;
        private static int pageid = -1;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();

        #endregion

        #region OnInit

        override protected void OnInit(EventArgs e)
        {
            //dpOtherArticles.PreRender += dpOtherArticles_PreRender;
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);
            //Load += Page_Load;

            //EnableViewState = UserCanEditPage();
            basePage = Page as mojoBasePage;
            if (basePage == null) return;
            SiteRoot = basePage.SiteRoot;
            ImageSiteRoot = basePage.ImageSiteRoot;

            this.btnPostComment.Click += new EventHandler(btnPostComment_Click);
            this.dlComments.ItemCommand += new RepeaterCommandEventHandler(dlComments_ItemCommand);
            this.dlComments.ItemDataBound += new RepeaterItemEventHandler(dlComments_ItemDataBound);
            SiteUtils.SetupEditor(this.edComment, true, Page);
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }
        #endregion


        private void Page_Load(object sender, EventArgs e)
        {
            //var lang = CultureInfo.CurrentCulture.Name;
            Page.EnableViewState = true;
            LoadParams();

            //if (
            //    (basePage == null)
            //    || (!basePage.UserCanViewPage(ModuleId))
            //)
            //{
            //    SiteUtils.RedirectToAccessDeniedPage();
            //    return;
            //}

            if (parametersAreInvalid)
            {
                AllowComments = false;
                pnlArticle.Visible = false;
                return;
            }

            LoadSettings();
            PopulateLabels();
            if (config.AllowComments)
            {
                SetupJqueryComments();
            }
            //if (IsPostBack || ModuleId <= -1 || (ItemId <= 0 && ItemIdRefer <= 0)) return;
            if (IsPostBack || (ItemId <= 0 && ItemIdRefer <= 0)) return;

            PopulateCategories();
            PopulateControls();

            var userAuthor = SiteUser.GetByGuid(siteSetting, article.UserGuid);
            if (userAuthor != null)
            {
                imgAuthor.ImageUrl = GetInternalAvatarUrl(userAuthor.AvatarUrl);
                lblAuthor.Text = article.CreatedByUser;
            }
        }
        private string GetInternalAvatarUrl(string avatarFile)
        {
            // if we get here we are using our own avatars
            if ((siteSettings.SiteId == -1) || (string.IsNullOrEmpty(avatarFile)) || (avatarFile == "blank.gif"))
            {
                return "/Data/SiteImages/anonymous.png";
            }

            return Page.ResolveUrl("~/Data/Sites/" + siteSettings.SiteId.ToInvariantString() + "/useravatars/" + avatarFile);
        }
        private void PopulateCategories()
        {
            List<CoreCategory> roots = CoreCategory.GetRootByCat(siteSetting.SiteId, siteSetting.ArticleCategory);
            foreach (CoreCategory item in roots)
            {
                ListItem list = new ListItem
                {
                    Text = item.Name,
                    Value = item.ItemID.ToString()
                };
                ddlCategorySearch.Items.Add(list);
            }
            PopulateChildNode(ddlCategorySearch);
            ddlCategorySearch.Items.Insert(0, new ListItem(ArticleResources.ParentCategoryChoose, "0"));
        }
        private void PopulateChildNode(ListControl root)
        {
            for (int i = 0; i < root.Items.Count; i++)
            {
                List<CoreCategory> children = CoreCategory.GetChildren(int.Parse(root.Items[i].Value));
                if (children.Count <= 0) continue;
                string prefix = string.Empty;
                while (root.Items[i].Text.StartsWith("|"))
                {
                    prefix += root.Items[i].Text.Substring(0, 3);
                    root.Items[i].Text = root.Items[i].Text.Remove(0, 3);
                }
                root.Items[i].Text = prefix + root.Items[i].Text;
                int index = 1;
                foreach (CoreCategory child in children)
                {
                    ListItem list = new ListItem
                    {
                        Text = prefix + @"|--" + child.Name,
                        Value = child.ItemID.ToString()
                    };
                    root.Items.Insert(root.Items.IndexOf(root.Items[i]) + index, list);
                    index++;
                }
            }
        }
        #region function for Comment in Article

        #region IRefreshAfterPostback
        public void RefreshAfterPostback()
        {
            PopulateControls();
        }
        #endregion
        public void UpdateCommentStats(Guid contentGuid, int commentCount)
        {
            //Blog.UpdateCommentCount(contentGuid, commentCount);
        }
        private bool ShouldAllowComments()
        {
            //comments closed globally
            if (!config.AllowComments) { return false; }

            // comments not allowed on this post
            if (!article.AllowComment) { return false; }

            if (pnlExcerpt.Visible) { return false; } // should not be able to comment without reading the full article

            return true;
        }

        private bool IsValidComment()
        {
            if (parametersAreInvalid) { return false; }

            //if (!AllowComments) { return false; }

            //if ((config.CommentSystem != "internal") && (article.CommentCount == 0)) { return false; }

            if (edComment.Text.Length == 0) { return false; }
            if (edComment.Text == "<p>&#160;</p>") { return false; }

            bool result = true;

            try
            {
                Page.Validate("articlecomments");
                result = Page.IsValid;

            }
            catch (NullReferenceException)
            {
                //Recaptcha throws nullReference here if it is not visible/disabled
            }
            catch (ArgumentNullException)
            {
                //manipulation can make the Challenge null on recaptcha
            }


            try
            {
                //if ((result) && (config.UseCaptcha))
                if ((config.UseCaptcha) && (pnlAntiSpam.Visible))
                {
                    //result = captcha.IsValid;
                    bool captchaIsValid = captcha.IsValid;
                    if (captchaIsValid)
                    {
                        if (!result)
                        {
                            // they solved the captcha but somehting else is invalid
                            // don't make them solve the captcha again
                            pnlAntiSpam.Visible = false;
                            captcha.Visible = false;
                            captcha.Enabled = false;

                        }

                    }
                    else
                    {
                        //captcha was invalid
                        result = false;
                    }
                }
            }
            catch (NullReferenceException)
            {
                return false;
            }
            catch (ArgumentNullException)
            {
                //manipulation can make the Challenge null on recaptcha
                return false;
            }


            return result;
        }
        private void SetCookies()
        {
            HttpCookie blogUserCookie = new HttpCookie("blogUser", this.txtName.Text);
            blogUserCookie.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Add(blogUserCookie);

            HttpCookie blogUrlCookie = new HttpCookie("LinkUrl", this.txtURL.Text);
            blogUrlCookie.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Add(blogUrlCookie);
        }


        private void btnPostComment_Click(object sender, EventArgs e)
        {
            if (!ShouldAllowComments())
            {
                WebUtils.SetupRedirect(this, Request.RawUrl);
                return;
            }
            if (!IsValidComment())
            {
                //SetupInternalCommentSystem();
                PopulateControls();
                return;
            }
            if (article == null) { return; }

            if (this.chkRememberMe.Checked)
            {
                SetCookies();
            }
            ArticleComment comment = new ArticleComment();
            comment.ModuleID = ModuleId;
            comment.ItemID = ItemId;
            comment.Name = this.txtName.Text;
            comment.Title = this.txtCommentTitle.Text;
            comment.URL = this.txtURL.Text;
            comment.Comment = edComment.Text;
            if (comment.Save())
            {
                pnlFeedback.Visible = false;
                pnlFeedbackDone.Visible = true;
                return;
            }
            else
            {
                WebUtils.SetupRedirect(this, Request.RawUrl);
            }
        }
        private void DisableLegacyArticleComments()
        {
            pnlAntiSpam.Visible = false;
            captcha.Visible = false;
            captcha.Enabled = false;
            pnlFeedback.Visible = false;
        }
        private bool CommentsAreOpen()
        {
            //comments closed globally
            if (!config.AllowComments) { return false; }

            // comments not allowed on this post
            if (!article.AllowComment) { return false; }

            return true;
        }
        private void SetupLegacyBlogComments()
        {
            pnlFeedback.Visible = true;
            fldEnterComments.Visible = CommentsAreOpen();
            pnlCommentsClosed.Visible = !fldEnterComments.Visible;

            if ((!config.UseCaptcha) || (!fldEnterComments.Visible) || (Request.IsAuthenticated))
            {
                pnlAntiSpam.Visible = false;
                captcha.Visible = false;
                captcha.Enabled = false;
            }

            CommentDateTimeFormat = config.DateTimeFormat;

            divCommentUrl.Visible = config.AllowWebSiteUrlForComments;

            //CommentItemHeaderElement = displaySettings.CommentItemHeaderElement;

            if ((config.RequireAuthenticationForComments) && (!Request.IsAuthenticated))
            {
                //AllowComments = false;
                pnlNewComment.Visible = false;
                pnlCommentsRequireAuthentication.Visible = true;
            }

            if (!IsPostBack)
            {
                txtCommentTitle.Text = "re: " + article.Title;

                if (Request.IsAuthenticated)
                {
                    SiteUser currentUser = SiteUtils.GetCurrentSiteUser();
                    this.txtName.Text = currentUser.Name;
                    txtURL.Text = currentUser.WebSiteUrl;
                }
                else
                {
                    if (CookieHelper.CookieExists("blogUser"))
                    {
                        this.txtName.Text = CookieHelper.GetCookieValue("blogUser");
                    }
                    if (CookieHelper.CookieExists("blogUrl"))
                    {
                        this.txtURL.Text = CookieHelper.GetCookieValue("blogUrl");
                    }
                }
            }

            using (IDataReader dataReader = Blog.GetBlogComments(ModuleId, ItemId))
            {
                dlComments.DataSource = dataReader;
                dlComments.DataBind();
            }
        }
        private void SetupCommentSystem()
        {

            if (!ShouldAllowComments())
            {
                pnlNewComment.Visible = false;
                pnlCommentsClosed.Visible = true;
                //divCommentService.Visible = false;

                pnlAntiSpam.Visible = false;
                captcha.Visible = false;
                captcha.Enabled = false;
                //comments.Visible = false;

                return;
            }

            string commentSystem = DetermineCommentSystem();

            comments.Visible = true;
            comments.CommentUrl = FormatArticleUrl(article.ItemUrl, article.ItemID);

            switch (commentSystem)
            {
                case "disqus":
                    if (config.DisqusSiteShortName.Length > 0)
                    {
                        DisqusSiteShortName = config.DisqusSiteShortName;
                    }
                    else
                    {
                        DisqusSiteShortName = basePage.SiteInfo.DisqusSiteShortName;
                    }

                    comments.CommentSystem = commentSystem;
                    comments.DisqusShortName = DisqusSiteShortName;

                    //commented
                    //navTop.ShowCommentCount = false;
                    //navBottom.ShowCommentCount = false;

                    DisableLegacyArticleComments();

                    break;

                case "intensedebate":

                    if (config.IntenseDebateAccountId.Length > 0)
                    {
                        IntenseDebateAccountId = config.IntenseDebateAccountId;
                    }
                    else
                    {
                        IntenseDebateAccountId = basePage.SiteInfo.IntenseDebateAccountId;
                    }


                    comments.IntenseDebateAccountId = IntenseDebateAccountId;
                    comments.CommentSystem = commentSystem;
                    //commented
                    //navTop.ShowCommentCount = false;
                    //navBottom.ShowCommentCount = false;

                    DisableLegacyArticleComments();

                    break;

                case "facebook":

                    comments.CommentSystem = commentSystem;

                    //commented
                    //navTop.ShowCommentCount = false;
                    //navBottom.ShowCommentCount = false;

                    DisableLegacyArticleComments();

                    break;

                case "internal":
                default:
                    if (ArticleConfiguration.UseLegacyCommentSystem)
                    {
                        SetupLegacyBlogComments();
                    }
                    else
                    {
                        DisableLegacyArticleComments();

                        SetupInternalCommentSystem();
                    }

                    break;
            }
        }

        private void SetupInternalCommentSystem()
        {
            basePage.ScriptConfig.IncludeColorBox = true;

            CommentsWidget comments = InternalCommentSystem as CommentsWidget;

            comments.SiteGuid = basePage.SiteInfo.SiteGuid;
            comments.FeatureGuid = Article.FeatureGuid;
            comments.ModuleGuid = module.ModuleGuid;
            comments.ContentGuid = article.ArticleGuid;
            //comments.CommentItemHeaderElement = CommentItemHeaderElement; //displaySettings.CommentItemHeaderElement;
            comments.CommentDateTimeFormat = config.DateTimeFormat;
            comments.CommentsClosed = !CommentsAreOpen();
            comments.CommentsClosedMessage = BlogResources.BlogCommentsClosedMessage;
            comments.ShowJanrainWidetOnSignInPrompt = false;
            comments.AlwaysShowSignInPromptIfNotAuthenticated = true;// displaySettings.AlwaysShowSignInPromptForCommentsIfNotAuthenticated;
            comments.ShowJanrainWidetOnSignInPrompt = true;// displaySettings.ShowJanrainWidetOnCommentSignInPrompt;

            comments.DefaultCommentTitle = "re: " + article.Title;
            comments.IncludeIpAddressInNotification = true;
            comments.RequireCaptcha = config.UseCaptcha && !Request.IsAuthenticated;
            comments.ContainerControl = this;
            comments.UpdateContainerControl = this;
            comments.EditBaseUrl = SiteRoot + "/Article/CommentDialog.aspx?pageid=" + PageId.ToInvariantString()
                + "&mid=" + ModuleId.ToInvariantString() + "&ItemID=" + ItemId.ToInvariantString();

            //if (config.NotifyOnComment)
            //{
            //    if ((config.NotifyEmail.Length > 0) && (Email.IsValidEmailAddressSyntax(config.NotifyEmail)))
            //    {
            //        comments.NotificationAddresses.Add(config.NotifyEmail);
            //    }

            //    //if post author email is not the same as default notification email, add it 
            //    if (config.NotifyEmail != blog.UserEmail)
            //    {
            //        comments.NotificationAddresses.Add(blog.UserEmail);
            //    }

            //} 

            //comments.NotificationTemplateName = "BlogCommentNotificationEmail.config";
            comments.SiteRoot = SiteRoot;
            comments.UserCanModerate = IsEditable;
            comments.AllowedEditMinutesForUnModeratedPosts = config.AllowedEditMinutesForUnModeratedPosts;
            comments.Visible = true;
            comments.UserEditIcon = "/Data/Icon16x16/Modify.png";
            comments.RequireModeration = config.RequireApprovalForComments;
            comments.RequireAuthenticationToPost = config.RequireAuthenticationForComments;
            if (comments.RequireAuthenticationToPost)
            {
                pnlCommentsRequireAuthentication.Visible = true;
            }
            else
            {
                pnlCommentsRequireAuthentication.Visible = false;
            }
            comments.AuthenticationRequiredMessage = BlogResources.CommentsRequireAuthenticationMessage;
            //if (!config.RequireAuthenticationForComments && displaySettings.AlwaysShowSignInPromptForCommentsIfNotAuthenticated)
            //{
            //    comments.AuthenticationRequiredMessage = " ";
            //}
            comments.UseCommentTitle = config.AllowCommentTitle;
            comments.ShowUserUrl = config.AllowWebSiteUrlForComments;
            comments.SortDescending = config.SortCommentsDescending;

        }
        private string DetermineCommentSystem()
        {
            if (article.CommentCount > 0) { return "internal"; }

            return config.CommentSystem;
        }

        #endregion end

        #region function for Poll in Article

        #endregion
        protected bool ShowAuthor()
        {
            return config.ShowPostAuthor;
        }
        private void SetupJqueryComments()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type='text/javascript'>");
            sb.Append("$(document).ready(function(){");
            sb.Append("$('.feedbacktrigger" + ModuleId + "').toggle(function(){");

            sb.Append("$('.feedback" + ModuleId + "').slideToggle();");

            sb.Append("}, function(){");

            sb.Append("$('.feedback" + ModuleId + "').slideToggle();");

            sb.Append("});");
            sb.Append("});");
            sb.Append("</script>");
            if (!Page.ClientScript.IsStartupScriptRegistered(typeof(Page), "feedback" + ModuleId))
            {
                Page.ClientScript.RegisterStartupScript(typeof(Page), "feedback" + ModuleId, sb.ToString());
            }
        }


        void dpOtherArticles_PreRender(object sender, EventArgs e)
        {
            //BindOtherArticles();
        }
        protected string FormatArticleDate(DateTime startDate)
        {
            if (config.DateTimeFormat == string.Empty) return string.Empty;
            string result = "";
            result = timeZone != null ? TimeZoneInfo.ConvertTimeFromUtc(startDate, timeZone).ToString(config.DateTimeFormat) : startDate.AddHours(TimeOffset).ToString(config.DateTimeFormat);
            return result;
        }
        protected bool ShowImage(string imageUrl)
        {
            if (String.IsNullOrEmpty(imageUrl))
            {
                return false;
            }
            else
            {
                if (imageUrl.Contains("http") || imageUrl.Contains("https")) return true;
                string filePath = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["ArticleImagesFolder"] + imageUrl;
                filePath = filePath.Replace("/", "\\");
                if (File.Exists(filePath))
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
        }
        protected virtual void PopulateControls()
        {
            if (parametersAreInvalid)
            {
                AllowComments = false;
                pnlArticle.Visible = false;
                return;
            }
            if (
                ((article.IsApproved.HasValue && !article.IsApproved.Value) || (article.StartDate > DateTime.UtcNow))
                && (!basePage.UserCanEditModule(ModuleId))
                )
            {
                AllowComments = false;
                pnlArticle.Visible = false;
                WebUtils.SetupRedirect(this, SiteUtils.GetCurrentPageUrl());
                return;
            }

            if (IsEditable)
            {
                lnkEdit.Visible = true;
                if (!basePage.UseTextLinksForFeatureSettings)
                {
                    lnkEdit.ImageUrl = basePage.ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
                    lnkEdit.Text = "Chỉnh sửa";
                }
                else
                {
                    lnkEdit.Text = "Chỉnh sửa";
                }
                lnkEdit.NavigateUrl = basePage.SiteRoot + "/Article/PostArticle.aspx?pageid="
                    + PageId.ToInvariantString()
                    + "&ItemID=" + article.ItemID.ToInvariantString()
                    + "&mid=" + article.ModuleID.ToInvariantString();

            }
            basePage.Title = SiteUtils.FormatPageTitle(basePage.SiteInfo, article.Title);
            basePage.MetaDescription = article.MetaDescription;
            basePage.MetaKeywordCsv = article.MetaKeywords;


            //basePage.AdditionalMetaMarkup = article.CompiledMeta;
            if (basePage.AnalyticsSection.Length == 0)
            {
                basePage.AnalyticsSection = ConfigHelper.GetStringProperty("AnalyticsBlogSection", "blog");
            }
            if (article.ImageUrl != string.Empty && config.ShowImageInViewPostSetting)
            {
                string imgUrl = ImageSiteRoot + "/" + ConfigurationManager.AppSettings["ArticleImagesFolder"] + article.ImageUrl;
                //image1.ImageUrl = FeatureUtilities.GetRealImageUrl(imgUrl);
                image1.ImageUrl = imgUrl;
                image1.AlternateText = article.Title;
                image1.Visible = ShowImage(article.ImageUrl);
            }
            else
            {
                image1.Visible = false;
                pnlImageWrapper.Visible = false;
            }

            litTitle.Text = Server.HtmlEncode(article.Title);
            siteID = article.SiteId;
            categoryID = article.CategoryID;
            ItemUrl = article.ItemUrl;
            tweetThis1.TitleToTweet = article.Title;
            pnlFeedback.Visible = article.AllowComment;


            tweetThis1.Visible = config.ShowTweetThisLink;
            fblike.Visible = config.UseFacebookLikeButton;
            fblike.ColorScheme = config.FacebookLikeButtonTheme;
            fblike.ShowFaces = config.FacebookLikeButtonShowFaces;
            fblike.HeightInPixels = config.FacebookLikeButtonHeight;
            fblike.WidthInPixels = config.FacebookLikeButtonWidth;


            ltrGoTop.Text = config.GoToTop;
            imageGoTop.ImageUrl = ImageSiteRoot + "/Data/Icon16x16/back-to-top.png";


            //litStartDate.Text = timeZone != null ? TimeZoneInfo.ConvertTimeFromUtc(article.StartDate, timeZone).ToString(config.DateTimeFormat) : article.StartDate.AddHours(TimeOffset).ToString(config.DateTimeFormat);
            litStartDate.Text = string.Format("{0:dddd, dd/MM/yyyy, HH:mm} (GMT+7)", article.StartDate);
            //litStartDate.Text = FormatArticleDate2(article.StartDate);
            litStartDate.Visible = config.DateTimeFormat != string.Empty;
            if (blogAuthor.Length == 0) { blogAuthor = article.CreatedByUser; }
            //litAuthor.Text = ArticleUtils.FormatPostAuthor(article.UserGuid.ToString(), config);
            lblAuthor.Text = FormatPostAuthor(article.CreatedByUser);
            //litSummary.Text = article.Summary;
            litDescription.Text = article.Description;
            litExcerpt.Text = GetExcerpt(article);
            pnlWCAG.Visible = article.AllowWCAG;

            BindTags();
            BindAttachments();

            pnlLeftMenu.Visible = false;
            var parentCategory = 0;
            var showLeftMenu = false;
            if (cat > 0)
            {
                var category = new CoreCategory(cat);
                if (category.ShowMenuLeft)
                {
                    parentCategory = category.ParentID;
                    showLeftMenu = true;
                    pnlLeftMenu.Visible = true;
                    pnlLeftMenu.CssClass = "left-menu";
                    pnlContentArticle.CssClass = "content-article";
                }
            }
            else
            {
                var listArticleCategory = ArticleCategory.GetList(ItemId);
                if (listArticleCategory != null && listArticleCategory.Count > 0)
                {
                    foreach (var item in listArticleCategory)
                    {
                        var getCategory = new CoreCategory(item.CategoryID);
                        if (getCategory.ShowMenuLeft)
                        {
                            parentCategory = getCategory.ParentID;
                            showLeftMenu = true;
                            pnlLeftMenu.Visible = true;
                            pnlLeftMenu.CssClass = "left-menu";
                            pnlContentArticle.CssClass = "content-article";
                            break;
                        }
                    }
                }
            }

            if (showLeftMenu)
            {
                var listCategory = CoreCategory.GetChildren(parentCategory);
                rptCategory.DataSource = listCategory;
                rptCategory.DataBind();

            }
            pnShowImageOtherArticle.Visible = false;
            pnOrtherArticle.Visible = false;
            pnlArticleReference.Visible = false;
            pnlNewOther.Visible = false;
            if (showLeftMenu == false)
            {
                pnlNewOther.Visible = true;
                pnShowImageOtherArticle.Visible = true;

                pnOrtherArticle.Visible = true;

                bindOrtherList();

                #region Bind Bài viết liên quan
                var listArticleRefenrece = Article.SelectByReference(article.ArticleReference);
                if (listArticleRefenrece != null && listArticleRefenrece.Count > 0)
                {
                    rptActiveReference.DataSource = listArticleRefenrece;
                    rptActiveReference.DataBind();
                    pnlArticleReference.Visible = true;
                }
                #endregion

                bindShowImageOrtherList();
                bindArticleByEvent();
            }





            if (Page.Header == null) { return; }

            Literal link = new Literal
            {
                ID = "articleurl",
                Text = @"<link rel='canonical' href='"
                       + SiteRoot
                       + article.ItemUrl.Replace("~/", "/")
                       + @"' />"
            };

            Page.Header.Controls.Add(link);
            lblHitCountLabel.Text = article.HitCount.ToString();
        }

        protected List<Article> LoadArticle(object categoryId)
        {
            var listArticle = Article.GetArticleTop(Convert.ToInt32(categoryId), 500);
            return listArticle;
        }


        protected string FormatArticleDate2(DateTime startDate)
        {
            if (config.DateTimeFormat == string.Empty) return string.Empty;
            return startDate.ToString(config.DateTimeFormat);
        }
        protected string FormatPostAuthor(string userGuid)
        {
            //return ArticleUtils.FormatPostAuthor(userGuid, config);
            if (config.ShowPostAuthor)
            {
                return userGuid;
            }
            else return string.Empty;
        }

        private void bindArticleByEvent()
        {
            if (ItemId > 0)
            {
                List<int> lstEventID = new List<int>();
                lstEventID = EventMapArticle.GetByArticleID(ItemId);
                if (lstEventID.Count > 0)
                {
                    List<Article> lstArticle = new List<Article>();
                    lstArticle = Article.GetAllByEvent(siteSetting.SiteId);
                    if (lstArticle != null && lstArticle.Count > 1)
                    {
                        lstArticle = lstArticle.Where(x => lstEventID.Contains(x.EventID) && x.ItemID != ItemId).Take(5).ToList();

                        rptEventArticle.DataSource = lstArticle;
                        rptEventArticle.DataBind();
                        pnEventArticle.Visible = lstArticle.Count > 0;
                    }
                }
            }
        }
        private void bindOrtherList()
        {

        }
        private void bindShowImageOrtherList()
        {
            if (mid > 0)
            {
                rptShowImageOrtherArticle.DataSource = Article.GetArticleTopOrther(categoryID, ItemId, config.ShowArticleDetailDisplay, false);
                rptShowImageOrtherArticle.DataBind();
                pnShowImageOtherArticle.Visible = rptShowImageOrtherArticle.Items.Count > 0;
            }
            else
            {
                rptShowImageOrtherArticle.DataSource = Article.GetArticleTopOrther(categoryID, ItemId, 12, false);
                rptShowImageOrtherArticle.DataBind();
                pnShowImageOtherArticle.Visible = rptShowImageOrtherArticle.Items.Count > 0;
            }
        }

        protected string BuildDownloadLink(string id, string name)
        {
            string innerMarkup = name;
            if (config.UseAttachmentDownloadIconSetting)
            {
                innerMarkup = "<img src='" + ImageSiteRoot + "/Data/SiteImages/Download.gif' alt='" + ArticleResources.DownloadLink + "' />";
            }

            return "<a href='" + SiteRoot + "/Article/Download.aspx?pageid=" + PageId.ToInvariantString()
                + "&amp;mid=" + ModuleId.ToInvariantString()
                + "&amp;siteid=" + siteSetting.SiteId
                + "&amp;fileid=" + id.ToString()
                + "&amp;articleid=" + ItemId.ToString() + "' "
                + "title='" + name + "' "
                + newWindowMarkup
                + ">"
                + innerMarkup
                + "</a>";
        }

        private void BindAttachments()
        {
            var listFileDownload = FileAttachments.GetByObject(ItemId);
            var listFilePdf = new List<FileAttachments>();
            var listFile = ArticleAttachment.GetAllObjectByItemID(ItemId);
            List<ArticleAttachmentBO> listArticleAttachment = new List<ArticleAttachmentBO>();
            if (listFile != null && listFile.Count > 0)
            {
                var chuyenmuc = new CoreCategory(article.CategoryID);
                var ngaydang = string.Format("{0:yyyyMMdd}", article.CreatedDate);
                foreach (var item in listFile)
                {
                    string downloadPath = "/Data/Sites/" + article.SiteId + "/media/ArticleAttachments/" + item.ServerFileName;
                    string pathExist = Page.Server.MapPath(downloadPath);
                    if (File.Exists(pathExist))
                    {
                        listArticleAttachment.Add(item);
                        FileAttachments fileAttachments = new FileAttachments();
                        fileAttachments.DownloadCount = item.DownloadCount;
                        fileAttachments.FileName = item.FileName;
                        fileAttachments.FilePath = downloadPath;
                        fileAttachments.ItemID = item.ID;
                        listFileDownload.Add(fileAttachments);
                        if (Path.GetFileName(downloadPath).ToLower().Equals("pdf"))
                        {
                            listFilePdf.Add(fileAttachments);
                        }
                    }
                }
            }
            foreach (var item in listFileDownload)
            {
                if (Path.GetFileName(item.FilePath).ToLower().Equals("pdf"))
                {
                    listFilePdf.Add(item);
                }
            }
            rptReadPDF.DataSource = listFilePdf;
            rptReadPDF.DataBind();
            pnlReadPdf.Visible = listFilePdf.Count > 0;

            rptAttachments.DataSource = listFileDownload;
            rptAttachments.DataBind();

            if (rptAttachments.Items.Count == 0)
            {
                pnlAttachment.Visible = false;
            }
        }
        protected string ReadPDF(object filePath, object height)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"<iframe frameborder='1' height='{height}px' scrolling='yes' ");
            stringBuilder.Append($"src='${filePath}'");
            stringBuilder.Append("width='100%'></iframe>");
            return stringBuilder.ToString();
        }
        private void BindTags()
        {
            rptTags.DataSource = ArticleItemTag.GetAllByArticle(ItemId);
            rptTags.DataBind();
            if (rptTags.Items.Count == 0)
            {
                rptTags.Visible = false;
            }
        }
        private void PopulateLabels()
        {
            if (!string.IsNullOrEmpty(article.AudioUrl))
            {
                fileAudio = SiteRoot + "/" + ConfigurationManager.AppSettings["ArticleAudiosFolder"] + article.AudioUrl;
            }
            //ltrBack.Text = @"<a href='javascript: history.go(-1)'><img src=" + ImageSiteRoot + @"/Data/Icon16x16/Back.png /> " + ArticleResources.ButtonBack + @" </a>";
            lnkEdit.ToolTip = ArticleResources.BlogEditEntryLink;
            lnkEdit.Visible = false;
            //lblViews.Text = ArticleResources.Views;
            pnlImageWrapper.Visible = config.ShowImageInViewPostSetting;


            OrtherArticle = ArticleResources.OrtherArticle;
            DatePost = ArticleResources.DatePost;
            if (ArticleConfiguration.UseLegacyCommentSystem)
            {
                edComment.WebEditor.ToolBar = ToolBar.AnonymousUser;
                edComment.WebEditor.Height = Unit.Pixel(170);

                captcha.ProviderName = basePage.SiteInfo.CaptchaProvider;
                captcha.Captcha.ControlID = "captcha_art_comment" + ModuleId.ToInvariantString();
                //captcha.RecaptchaPrivateKey = basePage.SiteInfo.RecaptchaPrivateKey;
                //captcha.RecaptchaPublicKey = basePage.SiteInfo.RecaptchaPublicKey;

                regexUrl.ErrorMessage = BlogResources.WebSiteUrlRegexWarning;
                commentListHeading.Text = BlogResources.BlogFeedbackLabel;

                btnPostComment.Text = BlogResources.BlogCommentPostCommentButton;
                SiteUtils.SetButtonAccessKey(btnPostComment, BlogResources.BlogCommentPostCommentButtonAccessKey);

                litCommentsClosed.Text = BlogResources.BlogCommentsClosedMessage;
                litCommentsRequireAuthentication.Text = BlogResources.CommentsRequireAuthenticationMessage;
            }

        }
        private void LoadSettings()
        {
            if (ItemId > 0)
            {
                article = new Article(ItemId);
                if (article.PollGuid != new Guid() || article.PollGuid != Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    PollGuid = article.PollGuid;
                }
                article.UpdateHitCount();
                //Nếu bản ghi View của ngày hiện tại đã được tạo thì update tăng lên 1 lượt view
                var dayCurrent = string.Format("{0:yyyy.MM.dd}", DateTime.Now);
                if (ArticleViews.IsExit(ItemId, dayCurrent))
                {
                    ArticleViews.UpdateView(ItemId, dayCurrent);
                }
                else//Nếu bản ghi chưa được tạo thì tạo mới
                {
                    ArticleViews articleView = new ArticleViews();
                    articleView.ArticleID = ItemId;
                    articleView.DayView = DateTime.Now;
                    articleView.TotalView = 1;
                    articleView.Save();
                }
            }
            //module = basePage.GetModule(ModuleId);
            module = new Module(ModuleId);
            comments = InternalCommentSystem as CommentsWidget;
            if (
                 //|| (article.ModuleId == -1)
                 //|| (article.ModuleId != module.ModuleId)
                 basePage.SiteInfo == null)
            {
                // query string params have been manipulated
                pnlArticle.Visible = false;
                AllowComments = false;
                parametersAreInvalid = true;
                return;
            }
            RegexRelativeImageUrlPatern = SecurityHelper.RegexRelativeImageUrlPatern;

            moduleSettings = ModuleSettings.GetModuleSettings(ModuleId);

            config = new ArticleConfiguration(moduleSettings);
            if (Context.User.Identity.IsAuthenticated)
            {
                var listArticleCategory = CategoryUserArticle.GetCategoryIdByUser(siteUser.UserId);
                if (CommonArticle.AccessManageArticle)
                {
                    IsEditable = true;
                }
                else if (listArticleCategory.Contains(article.CategoryID))
                {
                    IsEditable = true;
                }
                else if (WebUser.HasEditPermissions(basePage.SiteInfo.SiteId, ModuleId, basePage.CurrentPage.PageId))
                {
                    IsEditable = true;
                }
                else if (WebUser.IsInRoles(config.RolePost) || WebUser.IsInRoles(config.RoleApproved))
                {
                    IsEditable = true;
                }
            }
            GmapApiKey = SiteUtils.GetGmapApiKey();

            DisqusSiteShortName = config.DisqusSiteShortName.Length > 0 ? config.DisqusSiteShortName : basePage.SiteInfo.DisqusSiteShortName;

            IntenseDebateAccountId = config.IntenseDebateAccountId.Length > 0 ? config.IntenseDebateAccountId : basePage.SiteInfo.IntenseDebateAccountId;

            if (config.InstanceCssClass.Length > 0)
            {
                pnlArticle.CssClass += config.InstanceCssClass.Contains(" ")
                                        ? " " + config.InstanceCssClass.Remove(config.InstanceCssClass.IndexOf(" ")) +
                                          "-detail"
                                        : " " + config.InstanceCssClass + "-detail";
            }

            CommentDateTimeFormat = config.DateTimeFormat;

            divCommentUrl.Visible = config.AllowWebSiteUrlForComments;

            AllowComments = config.AllowComments && article.AllowComment;


            pnlFeedback.Visible = AllowComments;

            if (AllowComments)
            {
                if ((config.RequireAuthenticationForComments) && (!Request.IsAuthenticated))
                {
                    AllowComments = false;
                    pnlNewComment.Visible = false;
                    //pnlCommentsRequireAuthentication.Visible = true;
                }

            }

            if (!config.UseCaptcha)
            {
                pnlAntiSpam.Visible = false;
                captcha.Visible = false;
                captcha.Enabled = false;
                //pnlNewComment.Controls.Remove(captcha);
            }



            if (AllowComments)
            {
                if ((config.CommentSystem == "disqus") && (DisqusSiteShortName.Length > 0))
                {

                    // don't use new external comment system for existing posts that already have comments
                    if (article.CommentCount == 0)
                    {
                        disqus.SiteShortName = DisqusSiteShortName;
                        disqus.RenderWidget = true;
                        //disqus.WidgetPageUrl = FormatBlogUrl(article.ItemUrl, article.ItemId);
                        //disqus.WidgetPageId = blog.BlogGuid.ToString();
                        pnlFeedback.Visible = false;
                        if (config.UseCaptcha)
                        {
                            pnlAntiSpam.Visible = false;
                            captcha.Visible = false;
                            captcha.Enabled = false;
                            //Controls.Remove(pnlAntiSpam); 
                        }
                    }

                    //stats.ShowCommentCount = false;

                }

                if ((config.CommentSystem == "intensedebate") && (IntenseDebateAccountId.Length > 0))
                {
                    if (article.CommentCount == 0)
                    {
                        intenseDebate.AccountId = IntenseDebateAccountId;
                        //intenseDebate.PostUrl = FormatBlogUrl(article.ItemUrl, article.ItemId);
                        pnlFeedback.Visible = false;
                        if (config.UseCaptcha)
                        {
                            pnlAntiSpam.Visible = false;
                            captcha.Visible = false;
                            captcha.Enabled = false;
                            //Controls.Remove(pnlAntiSpam); 
                        }
                    }
                    //stats.ShowCommentCount = false;
                }
            }




            if (Request.IsAuthenticated)
            {
                SiteUser currentUser = SiteUtils.GetCurrentSiteUser();
                txtName.Text = currentUser.Name;
                txtURL.Text = currentUser.WebSiteUrl;

            }
            else
            {
                if ((config.HideDetailsFromUnauthencticated) && (article.Description.Length > config.ExcerptLength))
                {
                    pnlDetails.Visible = false;
                    pnlExcerpt.Visible = true;
                }

                if (CookieHelper.CookieExists("blogUser"))
                {
                    txtName.Text = CookieHelper.GetCookieValue("blogUser");
                }
                if (CookieHelper.CookieExists("blogUrl"))
                {
                    txtURL.Text = CookieHelper.GetCookieValue("blogUrl");
                }
            }
            pnlFeedbackDone.Visible = false;
            SetupCommentSystem();
        }

        private string GetExcerpt(Article itemArticle)
        {
            //if ((itemArticle.Excerpt.Length > 0) && (itemArticle.Excerpt != "<p>&#160;</p>"))
            //{
            //    return itemArticle.Excerpt;
            //}
            itemArticle.Description = SecurityHelper.RemoveMarkup(itemArticle.Description);
            return (itemArticle.Description.Length > config.ExcerptLength) ? UIHelper.CreateExcerpt(itemArticle.Description, config.ExcerptLength, config.ExcerptSuffix) : article.Description;
        }
        private string GetExcerptRefer(ArticleReference itemArticle)
        {
            //if ((itemArticle.Excerpt.Length > 0) && (itemArticle.Excerpt != "<p>&#160;</p>"))
            //{
            //    return itemArticle.Excerpt;
            //}
            itemArticle.Description = SecurityHelper.RemoveMarkup(itemArticle.Description);
            return (itemArticle.Description.Length > config.ExcerptLength) ? UIHelper.CreateExcerpt(itemArticle.Description, config.ExcerptLength, config.ExcerptSuffix) : articleRefer.Description;
        }

        protected string FormatCommentDate(DateTime startDate)
        {
            string result = " (";
            result += timeZone != null ? TimeZoneInfo.ConvertTimeFromUtc(startDate, timeZone).ToString(CommentDateTimeFormat) : startDate.AddHours(TimeOffset).ToString(config.DateTimeFormat);
            result += ")";
            return result;
        }

        protected string FormatArticleUrl(string itemUrl, int itemId)
        {
            if (itemUrl.Length > 0)
                return SiteRoot + itemUrl.Replace("~", string.Empty);

            return SiteRoot + "/Article/ViewPost.aspx?pageid=" + PageId.ToInvariantString()
                + "&ItemID=" + itemId.ToInvariantString()
                + "&mid=" + ModuleId.ToInvariantString();

        }



        /// <summary>
        /// Handles the item command
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void dlComments_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteComment")
            {
                ArticleComment.Delete(int.Parse(e.CommandArgument.ToString()));
                WebUtils.SetupRedirect(this, Request.RawUrl);

            }
        }


        void dlComments_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ImageButton btnDelete = e.Item.FindControl("btnDelete") as ImageButton;
            UIHelper.AddConfirmationDialog(btnDelete, ArticleResources.BlogDeleteCommentWarning);
        }


        protected virtual void SetupRssLink()
        {
            if (WebConfigSettings.DisableBlogRssMetaLink) { return; }

            if (module != null)
            {
                if (Page.Master != null)
                {
                    Control head = Page.Master.FindControl("Head1");
                    if (head != null)
                    {

                        Literal rssLink = new Literal
                        {
                            Text = @"<link rel=""alternate"" type=""application/rss+xml"" title="""
                                   + module.ModuleTitle + @""" href="""
                                   + GetRssUrl() + @""" />"
                        };

                        head.Controls.Add(rssLink);

                    }

                }
            }

        }
        protected string GetUrlArticle(int itemId)
        {

            return string.Format("{0}/Article/ViewDetail.aspx?pageid={1}&mid={2}&itemid={3}", SiteRoot, pageid, ModuleId, itemId);
        }
        private string GetRssUrl()
        {
            if (config.FeedburnerFeedUrl.Length > 0) return config.FeedburnerFeedUrl;

            return SiteRoot + "/blog" + ModuleId.ToInvariantString() + "rss.aspx";

        }

        private void LoadParams()
        {
            WebUtils.GetApplicationRoot();
            TimeOffset = SiteUtils.GetUserTimeOffset();
            timeZone = SiteUtils.GetUserTimeZone();
            PageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            ModuleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            mid = WebUtils.ParseInt32FromQueryString("mid", mid);
            pageid = WebUtils.ParseInt32FromQueryString("pageid", pageid);
            ItemId = WebUtils.ParseInt32FromQueryString("ItemID", -1);
            ItemIdRefer = WebUtils.ParseInt32FromQueryString("ReferItemID", -1);
            type = WebUtils.ParseInt32FromQueryString("Type", -1);
            cat = WebUtils.ParseInt32FromQueryString("cat", -1);

            if (ItemId == -1 && ItemIdRefer == -1) parametersAreInvalid = true;
            //if (!basePage.UserCanViewPage(ModuleId)) { parametersAreInvalid = true; }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if ((Page.IsPostBack) && (!pnlFeedback.Visible))
            {
                //WebUtils.SetupRedirect(this, Request.RawUrl);
                //return;
            }

            base.Render(writer);
        }

        protected string FormatTooltip(string title, string content)
        {
            string result;
            if (config.UseTooltipSettings)
            {
                if (content.Length > config.TooltipMaxCharSettings)
                {
                    content = UIHelper.CreateExcerpt(content, config.TooltipMaxCharSettings, "...");
                }
                result = "<div class='tooltip'><div class='title-l'></div><div class='title-m'>" + title + "</div><div class='title-r'></div><div class='body'>" + content + "</div></div>";
            }
            else
            {
                result = title;
            }
            return result;
        }

        protected string FormatBlogTitleUrl(string itemUrl, int itemId)
        {
            if (itemUrl.Length > 0)
                return SiteRoot + itemUrl.Replace("~", string.Empty);

            return SiteRoot + "/Article/ViewPost.aspx?pageid=" + PageId.ToInvariantString()
                + "&ItemID=" + itemId.ToInvariantString()
                + "&mid=" + ModuleId.ToInvariantString();

        }
    }
}
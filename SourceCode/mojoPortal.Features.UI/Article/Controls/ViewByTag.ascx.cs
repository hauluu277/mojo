using ArticleFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArticleFeature.UI
{
    public partial class ViewByTag : UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;
        private int siteId = -1;
        private mojoBasePage basePage;
        private Module module;
        protected ArticleConfiguration config = new ArticleConfiguration();
        private int tag = -1;
        private int pageId = -1;
        private int moduleId = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        private SiteSettings siteSettings;
        protected string FeedBackLabel = string.Empty;
        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkText = SwirlingQuestionResource.ButtonDelete;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkText = SwirlingQuestionResource.ButtonEdit;
        protected string EditLinkTooltip = ArticleResources.BlogEditEntryLink;
        protected string EditLinkImageUrl = string.Empty;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        protected bool ShowCommentCounts = true;
        protected string IntenseDebateAccountId = string.Empty;
        protected int langId = 1;
        protected bool isAdmin = false;
        List<Article> reader = null;
        public int Tag
        {
            get { return tag; }
            set { tag = value; }
        }
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
        public int SiteId
        {
            get { return siteId; }
            set { siteId = value; }
        }
        public string SiteRoot
        {
            get { return siteRoot; }
            set { siteRoot = value; }
        }

        public ArticleConfiguration Config
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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            //EnableViewState = false;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            var lang = CultureInfo.CurrentCulture.Name;
            langId = lang == "vi-VN" ? LanguageConstant.VN : LanguageConstant.EN;
            LoadSettings();
            PopulateLabels();
            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }

        private void PopulateControls()
        {
            BindArticles();
            PopulateNavigation();
        }

        //public List<Language> BindLanguage()
        //{
        //    List<Language> language = Language.GetAll();
        //    return language;
        //}
        private void BindArticles()
        {
            EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
            DeleteLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + DeleteLinkImage;
            reader = Article.GetPageByTag(siteId, tag, pageNumber, config.PageSize, out totalPages);
            rptRecentArticles.DataSource = reader;
            rptRecentArticles.DataBind();
            string pageUrl = SiteRoot + "/Article/ViewTag.aspx"
                   + "?tag=" + tag.ToInvariantString()
                   + "&pageid=" + pageId.ToInvariantString()
                   + "&amp;mid=" + moduleId.ToInvariantString()
                   + "&amp;pagenumber={0}";

            pgr.PageURLFormat = pageUrl;
            pgr.ShowFirstLast = true;
            pgr.PageSize = config.PageSize;
            pgr.PageCount = totalPages;
            pgr.CurrentIndex = pageNumber;
            pnlArticlePager.Visible = (totalPages > 1) && config.ShowPager;
        }

        protected virtual void PopulateNavigation()
        {
        }
        private void PopulateOtherArticles()
        {
            if (config.ShowOtherArticles)
            {
                List<Article> result = Article.GetOthersPageByModuleId(ModuleId, config.PageSize, pageNumber);
                if (result != null && result.Count > 0)
                {
                    gvOthersArticle.DataSource = result;
                    gvOthersArticle.DataBind();
                    gvOthersArticle.AllowPaging = config.OtherArticlesPageSizeSetting < result.Count;
                }
                lblOtherHeader.Visible = result.Count > 0;
            }
            else
            {
                pnlOthersArticle.Visible = false;
            }
        }
        void gvOthersArticle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOthersArticle.PageIndex = e.NewPageIndex;
            PopulateOtherArticles();
        }
        protected bool VisibleRefer(int itemId)
        {
            bool show = true;
            ArticleReference articleRefer = ArticleReference.GetArticleByRootId(itemId);
            if (articleRefer.ItemID != -1)
            {
                show = false;
            }
            return show;
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
                if (imageUrl.ToLower().Contains("/data/sites"))
                {
                    string decodedUrl = HttpUtility.UrlDecode(imageUrl);
                    var file = Server.MapPath(decodedUrl).Replace("\\", "/");
                    return File.Exists(file);
                }

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
        protected virtual void LoadSettings()
        {
            if ((WebUser.IsAdminOrContentAdmin) || (SiteUtils.UserIsSiteEditor())) { isAdmin = true; }

            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new ArticleConfiguration(getModuleSettings);

            if (WebUser.IsInRoles(config.RolePost) || WebUser.IsInRoles(config.RoleApproved))
            {
                isAdmin = true;
            }

            siteSettings = CacheHelper.GetCurrentSiteSettings();
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);

            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
            IntenseDebateAccountId = config.IntenseDebateAccountId.Length > 0 ? config.IntenseDebateAccountId : siteSettings.IntenseDebateAccountId;

            if (config.AllowComments)
            {
                if ((IntenseDebateAccountId.Length > 0) && (config.CommentSystem == "intensedebate"))
                {
                    ShowCommentCounts = false;
                }
            }
        }

        protected virtual void PopulateLabels()
        {
            pnlScrollable.CssClass = "scrollable" + ModuleId;
            FeedBackLabel = ArticleResources.BlogFeedbackLabel;
            mojoBasePage mojoBasePage = Page as mojoBasePage;
            if (mojoBasePage != null)
            {
                if (!mojoBasePage.UseTextLinksForFeatureSettings)
                {
                    EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
                }

                if (mojoBasePage.AnalyticsSection.Length == 0)
                {
                    mojoBasePage.AnalyticsSection = ConfigHelper.GetStringProperty("AnalyticsBlogSection", "article");
                }
            }

        }
        protected string ImgLanguage()
        {
            string Imgurl = string.Empty;

            Imgurl = "~/Data/SiteImages/flags/en.gif";

            return Imgurl;
        }
        protected string FormatImgUrlLanguage(string code)
        {
            string Imgurl = "~/Data/SiteImages/flags/" + code + ".gif";
            return Imgurl;
        }
        protected string FormatArticleDate(DateTime startDate)
        {
            if (config.DateTimeFormat == string.Empty) return string.Empty;
            return startDate.ToString(config.DateTimeFormat);
        }

        protected string FormatPostAuthor(string userGuid)
        {
            //return ArticleUtils.FormatPostAuthor(userGuid, config);
            if (config.ShowAuthorSignature)
            {
                return userGuid;
            }
            else return string.Empty;
        }


        protected string BuildEditUrl(int itemID)
        {
            return SiteRoot + "/Article/EditPost.aspx?pageid=" + pageId + "&ItemID=" +
                       itemID + "&mid=" + moduleId;

        }
    }
}
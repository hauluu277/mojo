using System;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.Framework;
using mojoPortal.Web;
using Resources;
using ArticleFeature.Business;
using System.Collections.Generic;
using Utilities;
using mojoPortal.Features;

namespace ArticleFeature.UI
{
    public partial class PostListLoader : UserControl
    {
        #region Properties
        private int countOfDrafts;
        private int pageNumber = 1;
        private int totalPages = 1;
        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string EditBlogAltText = "Edit";
        protected Double TimeOffset;
        private TimeZoneInfo timeZone;
        protected DateTime CalendarDate;
        protected bool ShowGoogleMap = true;
        protected string FeedBackLabel = string.Empty;
        protected string GmapApiKey = string.Empty;
        protected bool EnableContentRating;
        protected string disqusFlag = string.Empty;
        protected string IntenseDebateAccountId = string.Empty;
        protected bool ShowCommentCounts = true;
        protected string EditLinkText = ArticleResources.BlogEditEntryLink;
        protected string EditLinkTooltip = ArticleResources.BlogEditEntryLink;
        protected string EditLinkImageUrl = string.Empty;
        private mojoBasePage basePage;
        private Module module;
        protected ArticleConfiguration config = new ArticleConfiguration();
        private int pageId = -1;
        private int moduleId = -1;
        private int pureModuleId = -1;
        protected int type = -1;
        protected int langId = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        private SiteSettings siteSettings;
        private int categoryId = -1;
        private string[] listModuleId;

        public string[] ListModuleId
        {
            get { return listModuleId; }
            set { listModuleId = value; }
        }

        public int PageId
        {
            get { return pageId; }
            set { pageId = value; }
        }

        public int PureModuleId
        {
            get { return pureModuleId; }
            set { pureModuleId = value; }
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

        public string ImageSiteRoot
        {
            get { return imageSiteRoot; }
            set { imageSiteRoot = value; }
        }

        public ArticleConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }

        public bool IsEditable { get; set; }

        public int CategoryID
        {
            get { return categoryId; }
            set { categoryId = value; }
        }

        #endregion


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            gvOthersArticle.PageIndexChanging += gvOthersArticle_PageIndexChanging;
            EnableViewState = false;
            //pgr.Command += new CommandEventHandler(pgr_Command);
        }

        void gvOthersArticle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOthersArticle.PageIndex = e.NewPageIndex;
            PopulateOtherArticles();
        }
        protected string FormatArticleDate(DateTime startDate)
        {
            if (config.DateTimeFormat == string.Empty) return string.Empty;
            string result = "";
            result = timeZone != null ? TimeZoneInfo.ConvertTimeFromUtc(startDate, timeZone).ToString(config.DateTimeFormat) : startDate.AddHours(TimeOffset).ToString(config.DateTimeFormat);
            return result;
        }
        protected virtual void Page_Load(object sender, EventArgs e)
        {
            var lang = CultureInfo.CurrentCulture.Name;
            langId = lang == "vi-VN" ? LanguageConstant.VN : LanguageConstant.EN;
            type = langId;
            LoadSettings();

            //#if !MONO
            //            if (!(Page is mojoPortal.Web.UI.Pages.MyPage))
            //            {
            //                SetupRssLink();
            //            }
            //#endif

            PopulateLabels();
            if (!Page.IsPostBack)
            {
                PopulateControls();
            }

        }

        private void PopulateControls()
        {
            BindBlogs();
            PopulateOtherArticles();
        }

        private void PopulateOtherArticles()
        {
            if (!config.ShowOtherArticles)
            {
                pnlOthersArticle.Visible = false;
                return;
            }
            List<Article> result;
            if (config.UseHotArticle || config.UseMostViewArticle)
            {
                result = config.UseHotArticle
                             ? Article.GetOthersPageHotModule(listModuleId, config.PageSize, pageNumber)
                             : config.UseMostViewArticle
                                   ? Article.GetOthersPageMostViewModule(listModuleId, config.PageSize, pageNumber)
                                   : Article.GetOthersPageModule(listModuleId, config.PageSize, pageNumber);
            }
            else
            {
                result = categoryId.Equals(0)
                             ? Article.GetOthersPageByModuleId(moduleId,
                                                                  config.PageSize,
                                                                  pageNumber)
                             : Article.GetOthersPageCategory(categoryId,
                                                                config.PageSize,
                                                                pageNumber);
            }
            gvOthersArticle.PageSize = config.OtherArticlesPageSizeSetting;
            gvOthersArticle.AllowPaging = config.OtherArticlesPageSizeSetting < result.Count;
            gvOthersArticle.DataSource = result;
            gvOthersArticle.DataBind();
            pnlOtherHeader.Visible = result.Count > 0 && config.OtherArticle != string.Empty;
            lblOtherHeader.Text = config.OtherArticle;
            pnlMoreLink.Visible = config.OtherArticlesShowMoreLinkSetting && (rptBlogs.Items.Count > config.PageSize || gvOthersArticle.Rows.Count > 0);
        }

        private void BindBlogs()
        {
            List<Article> result;
            if (listModuleId != null && listModuleId[0].Length > 0)
            {
                var listModule = Utilities.FeatureUtilities.ConvertStringArrayToString(listModuleId);
                result = config.UseHotArticle
                             ? Article.GetPageHotModule(listModule, config.PageSize, pageNumber, out totalPages)
                             : config.UseMostViewArticle
                                   ? Article.GetPageMostViewModule(listModule, config.PageSize, pageNumber, out totalPages)
                                   : Article.GetPageModule(listModule, config.PageSize, pageNumber, out totalPages);
            }
            else
            {
                if (categoryId.Equals(0))
                    result = Article.GetPageByModuleId(moduleId, config.PageSize,
                                                          pageNumber, out totalPages);
                else
                {
                    //if (config.UseReverse)
                    //    result = Article.GetPageCategoryReverse(moduleId, config.PageSize,
                    //                                               pageNumber, out totalPages);
                    //else
                    result = Article.GetPageCategory(categoryId, config.PageSize,
                                                        pageNumber, out totalPages);
                }
            }
            rptBlogs.DataSource = result;
            rptBlogs.DataBind();
            string pageUrl = SiteRoot + "/ArticleLoader/ViewList.aspx"
                             + "?pageid=" + pageId.ToInvariantString()
                             + "&amp;mid=" + pureModuleId.ToInvariantString()
                             + "&amp;cat=" + categoryId.ToInvariantString()
                             + "&amp;pagenumber={0}";
            pgr.PageURLFormat = pageUrl;
            pgr.ShowFirstLast = true;
            pgr.PageSize = config.PageSize;
            pgr.PageCount = totalPages;
            pgr.CurrentIndex = pageNumber;
            pnlBlogPager.Visible = (totalPages > 1) && config.ShowPager;
        }

        //void pgr_Command(object sender, CommandEventArgs e)
        //{
        //    pageNumber = Convert.ToInt32(e.CommandArgument);
        //    pgr.CurrentIndex = pageNumber;
        //    PopulateControls();
        //    updBlog.Update();
        //}

        protected virtual void PopulateLabels()
        {
            gvOthersArticle.Visible = config.ShowOtherArticles;
            hplMoreLink.NavigateUrl = categoryId > 0
                                          ? siteRoot + "/Article/ViewListByCategory.aspx"
                                            + "?pageid=" + pageId
                                            + "&mid=" + moduleId
                                            + "&cat=" + categoryId
                                          : config.OtherArticlesMoreLinkSetting;
            hplMoreLink.Text = config.OtherArticlesMoreLinkTextSetting;
            pnlMoreLink.Visible = config.OtherArticlesShowMoreLinkSetting;

            EditBlogAltText = ArticleResources.EditImageAltText;
            FeedBackLabel = ArticleResources.BlogFeedbackLabel;

            mojoBasePage mojoBasePage = Page as mojoBasePage;
            if (mojoBasePage == null) return;
            if (!mojoBasePage.UseTextLinksForFeatureSettings)
            {
                EditLinkImageUrl = ImageSiteRoot + "/Data/SiteImages/" + EditContentImage;
            }
        }

        protected string FormatPostAuthor(string userGuid)
        {
            return ArticleUtils.FormatPostAuthor(userGuid, config);
        }

        protected string FormatBlogDate(DateTime startDate)
        {
            return ArticleUtils.FormatBlogDate(startDate, config, timeZone, TimeOffset);
        }

        protected string BuildEditUrl(int itemID)
        {
            return ArticleUtils.BuildEditUrl(itemID, listModuleId, SiteRoot, PageId, ModuleId);
        }

        private string GetRssUrl()
        {
            return ArticleUtils.GetRssUrl(config, SiteRoot, ModuleId);
        }

        protected virtual void LoadSettings()
        {
            SetJQueryScript();
            gvOthersArticle.AllowPaging = config.OtherArticlesPagingSetting;
            pnlOthersArticle.CssClass += " others" + ModuleId.ToInvariantString();
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            TimeOffset = SiteUtils.GetUserTimeOffset();
            timeZone = SiteUtils.GetUserTimeZone();
            GmapApiKey = SiteUtils.GetGmapApiKey();
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);

            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }

            CalendarDate = WebUtils.ParseDateFromQueryString("blogdate", DateTime.UtcNow).Date;

            if (CalendarDate > DateTime.UtcNow.Date)
            {
                CalendarDate = DateTime.UtcNow.Date;
            }

            if ((config.UseExcerpt) && (!config.GoogleMapIncludeWithExcerpt)) { ShowGoogleMap = false; }

            EnableContentRating = config.EnableContentRating;
            if (config.UseExcerpt) { EnableContentRating = false; }

            IntenseDebateAccountId = config.IntenseDebateAccountId.Length > 0 ? config.IntenseDebateAccountId : siteSettings.IntenseDebateAccountId;
            ShowCommentCounts = config.AllowComments;
            if (config.AllowComments)
            {
                if ((IntenseDebateAccountId.Length > 0) && (config.CommentSystem == "intensedebate"))
                {
                    ShowCommentCounts = false;
                }
            }


            if (!config.NavigationOnRight)
            {
                divblog.CssClass = "blogcenter-leftnav";

            }

            if (config.Copyright.Length > 0)
            {
                lblCopyright.Text = config.Copyright;
            }

            //if (IsEditable)
            //{
            //    countOfDrafts = Article.CountOfDrafts(ModuleId);
            //}

        }

        private void SetJQueryScript()
        {
            //StringBuilder sb = ArticleUtils.SetJQueryScript(config, pureModuleId);
            //if (!Page.ClientScript.IsStartupScriptRegistered(typeof(Page), "postlist" + pureModuleId) && sb.ToString() != string.Empty)
            //{
            //    Page.ClientScript.RegisterStartupScript(typeof(Page), "postlist" + pureModuleId, sb.ToString());
            //}
            //if (Page.ClientScript.IsStartupScriptRegistered(typeof(Page), "jquerytool")) return;
            //string script = "<script src='" + imageSiteRoot + "/ClientScript/jqmojo/jquery.tools.min.js'></script>";
            //Page.ClientScript.RegisterStartupScript(typeof(Page), "jquerytool", script);
        }

        protected virtual void SetupRssLink()
        {
            if (WebConfigSettings.DisableBlogRssMetaLink) { return; }
            if (module == null || Page.Master == null) return;
            Control head = Page.Master.FindControl("Head1");
            if (head == null) return;
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
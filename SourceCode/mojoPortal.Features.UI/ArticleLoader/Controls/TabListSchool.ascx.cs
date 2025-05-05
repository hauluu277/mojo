using ArticleFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArticleFeature.UI
{
    public partial class TabListSchool : UserControl
    {
        #region Properties

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
        private int siteId = -1;
        private int pureModuleId = -1;
        protected int langId = -1;
        protected int type = -1;
        private int itemId = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        private SiteSettings siteSettings;
        private string[] listModuleId;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private string moduleHeading = string.Empty;
        protected bool visibleHighlight = false;
        protected bool visibleList = false;
        public string lnkHotArticle_Title = "";
        public string lnkHotArticle_HRef = "";
        public string lnkHotArticle_Summary = "";
        public string lnkHotArticle_Src = "";

        public string[] ListModuleId
        {
            get { return listModuleId; }
            set { listModuleId = value; }
        }

        public int PureModuleId
        {
            get { return pureModuleId; }
            set { pureModuleId = value; }
        }

        public int PageId
        {
            get { return pageId; }
            set { pageId = value; }
        }
        public int SiteId
        {
            get { return siteId; }
            set { siteId = value; }
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

        public string ModuleHeading
        {
            get { return moduleHeading; }
            set { moduleHeading = value; }
        }

        public ArticleConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }

        public bool IsEditable { get; set; }

        public bool DisplayTab { get; set; }

        #endregion


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            EnableViewState = false;
            //pgr.Command += new CommandEventHandler(pgr_Command);
        }

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            var lang = CultureInfo.CurrentCulture.Name;
            langId = lang == "vi-VN" ? LanguageConstant.VN : LanguageConstant.EN;
            type = langId;
            LoadSettings();
            PopulateLabels();
            //SetupRssLink();

            PopulateControls();

        }

        private void PopulateControls()
        {
            pnlUseTab4.Visible = false;
            mp_modulecontent.Visible = false;
            pnlUseTab5.Visible = false;
            if (config.TabSelectorSetting == ArticleConstant.Tab_5)
            {
                pnlUseTab5.Visible = true;
                BindTab5();
            }
            else if (config.TabSelectorSetting == ArticleConstant.Tab_4)
            {
                pnlUseTab4.Visible = true;
                BindCategoriesForTab();
            }
            else
            {
                mp_modulecontent.Visible = true;
                BindCategories();
            }
            //BindOthers();
        }

        public List<Article> BindArticlesTab4(int categoryID, int count = 10)
        {
            List<Article> result = Article.GetOrtherByCategory(categoryID, itemId, count);
            return result;
        }

        private void BindTab5()
        {
            var categories = string.Empty;
            if (!string.IsNullOrEmpty(config.ArticleCategoryConfig))
            {
                categories = config.ArticleCategoryConfig.Replace("-", " ").Trim();
            }
            var listArticle = Article.GetTopArticleHot(siteSettings.SiteId, config.NumberArticleLimit, categories);
            rptTab_5.DataSource = listArticle;
            rptTab_5.DataBind();
        }


        private void BindCategoriesForTab()
        {

            pnlHideLink.Visible = true;
            var listCategoryConfig = string.Empty;
            var module_settings = ModuleSettings.GetModuleSettings(moduleId > 0 ? moduleId : pureModuleId);
            var module_config = new ArticleConfiguration(module_settings);
            List<CoreCategory> listCategory = new List<CoreCategory>();
            List<CoreCategory> listChildrenCategory = new List<CoreCategory>();

            if (module_config != null && !string.IsNullOrEmpty(module_config.ArticleCategoryConfig))
            {
                listCategoryConfig = module_config.ArticleCategoryConfig;
            }
            else
            {
                Module moduleLoadArticle = new Module(moduleId);
                PageSettings pageModuleLoadArticleSettings = CacheHelper.GetPage(moduleLoadArticle.PageId);
                listCategoryConfig = pageModuleLoadArticleSettings.CategoryConfig;
            }

            var list_category_config = listCategoryConfig.Split('-');
            CoreCategory coreCategory = new CoreCategory();
            bool isFirst = true;
            foreach (var item in list_category_config)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    coreCategory = new CoreCategory(int.Parse(item));
                    if (isFirst)
                    {
                        lnkCategory4.NavigateUrl = SiteRoot + coreCategory.Description;
                        lnkCategory4.Text = coreCategory.Name;
                    }
                    //lblPageTitle.Text = coreCategory.Name;
                    listCategory.Add(coreCategory);
                    isFirst = false;
                }
            }
            if (listCategory != null && listCategory.Count > 0)
            {
                int i = 0;
                foreach (var cat in listCategory)
                {

                    i++;

                    List<CoreCategory> children = CoreCategory.GetChildren(cat.ItemID);
                    if (children != null && children.Count > 0)
                    {
                        children = children.Take(config.NumberCategoriesLimit).ToList();
                        listChildrenCategory.AddRange(children);
                    }
                }
            }
            if (listChildrenCategory != null && listChildrenCategory.Count > 0)
            {

                rptTabs2.DataSource = listChildrenCategory;
                rptTabs2.DataBind();
                var listCategories = string.Join("-", listChildrenCategory.Select(x => x.ItemID).ToArray());
                var listArticle = Article.GetTopArticleHot(siteSettings.SiteId, config.NumberArticleLimit, listCategories);
                if (listArticle != null && listArticle.Any())
                {
                    var articleFirst = listArticle[0];

                    lblDateHeightLight.InnerText = string.Format("{0:dd/MM/yyyy}", articleFirst.StartDate);
                    lblSumaryHeightLight.InnerText = articleFirst.Summary;
                    hplHeightLight.Text = articleFirst.Title;
                    hplHeightLight.NavigateUrl = ArticleUtils.FormatBlogTitleUrl(SiteRoot, articleFirst.ItemUrl, Convert.ToInt32(articleFirst.ItemID), PageId, ModuleId);
                    imgHeightlight.ImageUrl = ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], articleFirst.ImageUrl);

                    hplCategory.NavigateUrl = SiteRoot + articleFirst.CategoryUrl;
                    hplCategory.ToolTip = articleFirst.CategoryName;
                    hplCategory.Text = articleFirst.CategoryName;


                    rptArticles.DataSource = listArticle.Skip(1);
                    rptArticles.DataBind();
                }
            }

        }

        private void BindCategories()
        {
            var listCategoryConfig = string.Empty;
            var module_settings = ModuleSettings.GetModuleSettings(moduleId > 0 ? moduleId : pureModuleId);
            var module_config = new ArticleConfiguration(module_settings);
            List<CoreCategory> listCategory = new List<CoreCategory>();
            List<int> listChildrenCategory = new List<int>();
            if (module_config != null && !string.IsNullOrEmpty(module_config.ArticleCategoryConfig))
            {
                listCategoryConfig = module_config.ArticleCategoryConfig;
            }
            else
            {
                Module moduleLoadArticle = new Module(moduleId);
                PageSettings pageModuleLoadArticleSettings = CacheHelper.GetPage(moduleLoadArticle.PageId);
                listCategoryConfig = pageModuleLoadArticleSettings.CategoryConfig;
            }
            var list_category_config = listCategoryConfig.Split('-');
            CoreCategory coreCategory = new CoreCategory();
            if (list_category_config != null && list_category_config.Any() && !string.IsNullOrEmpty(listCategoryConfig))
            {
                coreCategory = new CoreCategory(Convert.ToInt32(list_category_config[0]));
                if (coreCategory != null)
                {
                    lnkCategory.Text = coreCategory.Name;
                    lnkCategory.NavigateUrl = coreCategory.Description;
                }
            }
            foreach (var item in list_category_config)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    List<CoreCategory> children = CoreCategory.GetChildren(int.Parse(item));
                    if (children != null && children.Count > 0)
                    {
                        listChildrenCategory.AddRange(children.Select(o => o.ItemID).ToList());
                    }
                    else
                    {
                        listChildrenCategory.Add(int.Parse(item));
                    }
                }
            }
            var ListAllChildCat = string.Join("-", listChildrenCategory);
            var listArticle = Article.GetTopArticleHot(siteSettings.SiteId, config.NumberArticleLimit, ListAllChildCat);
            if (listArticle != null && listArticle.Count > 0)
            {
                lnkHotArticle_Src = ArticleUtils.FormatImageDialog(ConfigurationManager.AppSettings["ArticleImagesFolder"], listArticle[0].ImageUrl);
                lnkHotArticle_Title = listArticle[0].Title;
                lnkHotArticle_HRef = ArticleUtils.FormatBlogTitleUrl(SiteRoot, listArticle[0].ItemUrl, listArticle[0].ItemID, PageId, ModuleId);
                lnkHotArticle_Summary = listArticle[0].Summary;
            }
            if (config.TabSelectorSetting == ArticleConstant.Tab_1 && listArticle != null && listArticle.Count > 0)
            {
                TabType1.Visible = true;
                rptTab_1.DataSource = listArticle.Skip(1);
                rptTab_1.DataBind();
            }
            else if (config.TabSelectorSetting == ArticleConstant.Tab_2 && listArticle != null && listArticle.Count > 0)
            {
                TabType2.Visible = true;
                rptTab_2.DataSource = listArticle.Skip(1);
                rptTab_2.DataBind();

            }
            else if (config.TabSelectorSetting == ArticleConstant.Tab_3)
            {
                if (listArticle != null && listArticle.Count > 0)
                {
                    rptTab_3.DataSource = listArticle;
                    rptTab_3.DataBind();
                }
            }
            else
            {
                if (listArticle != null && listArticle.Count > 0)
                {
                    rptTab_4.DataSource = listArticle;
                    rptTab_4.DataBind();
                }
            }
        }
        public List<Article> BindArticles(int categoryID)
        {
            List<Article> result = Article.GetOrtherByCategory(categoryID, itemId, config.NumberArticleLimit);
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


        public List<ArticleBO> BindArticlesRight(int categoryID)
        {
            List<ArticleBO> result = ArticleBO.GetPageCategory(categoryID, langId, config.PageSize, pageNumber, out totalPages).Skip(5).Take(5).ToList();
            return result;
        }

        public List<ArticleBO> BindArticleFirst(int categoryID)
        {
            List<ArticleBO> result = ArticleBO.GetPageCategory(categoryID, langId, config.PageSize, pageNumber, out totalPages).Take(1).ToList();
            if (result != null && result.Count > 0)
            {
                itemId = result[0].ItemID;
            }
            return result;
        }

        public List<Article> BindListArticles(int categoryID)
        {
            List<Article> result = Article.GetPageCategory(categoryID, config.PageSize, pageNumber, out totalPages).Take(5).ToList();
            return result;
        }
        protected string FormatArticleDate(DateTime startDate)
        {
            //if (config.DateTimeFormat == string.Empty) return startDate.ToShortDateString();
            //return startDate.ToString(config.DateTimeFormat);
            if (config.DateTimeFormat == string.Empty) return string.Empty;
            string result = "";
            result = timeZone != null ? TimeZoneInfo.ConvertTimeFromUtc(startDate, timeZone).ToString(config.DateTimeFormat) : startDate.AddHours(TimeOffset).ToString(config.DateTimeFormat);
            return result;
        }
        //public List<Article> BindOtherArticles(int categoryID)
        //{
        //    List<Article> result = Article.GetOthersPageCategory(categoryID, config.PageSize,
        //                                     pageNumber);
        //    return result;
        //}

        //public string FormatMoreLink(int categoryID)
        //{
        //    string result = string.Empty;
        //    if (config.OtherArticlesShowMoreLinkSetting)
        //    {
        //        CoreCategory category = new CoreCategory(categoryID);
        //        if (category != null)
        //        {
        //            DBUtilities repositoryUtilities = new DBUtilities();
        //            PageModule pageModule = repositoryUtilities.GetFirstPageByModuleID(category.ModuleID);
        //            if (pageModule != null)
        //            {
        //                result = siteRoot + "/Article/ViewListByCategory.aspx?pageid=" + pageModule.PageId + "&mid=" + pageModule.ModuleId +
        //                         "&cat=" + categoryID;
        //            }
        //        }
        //    }
        //    return result;
        //}

        //void pgr_Command(object sender, CommandEventArgs e)
        //{
        //    pageNumber = Convert.ToInt32(e.CommandArgument);
        //    pgr.CurrentIndex = pageNumber;
        //    PopulateControls();
        //    updBlog.Update();
        //}


        protected virtual void PopulateLabels()
        {
            if (config.UseListArticle)
            {
                visibleList = true;
                visibleHighlight = false;
            }
            else
            {
                visibleList = false;
                visibleHighlight = true;
            }
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
            visibleHighlight = true;
            visibleList = false;
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            siteId = siteSettings.SiteId;
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

            if (config.AllowComments)
            {
                if ((IntenseDebateAccountId.Length > 0) && (config.CommentSystem == "intensedebate"))
                {
                    ShowCommentCounts = false;
                }
            }

            //if (config.Copyright.Length > 0)
            //{
            //    lblCopyright.Text = config.Copyright;
            //}

            if (IsEditable)
            {
                //Article.CountOfDrafts(ModuleId);
            }
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


        protected string FormartDateTime(object startDate)
        {
            if (startDate != null)
            {
                return string.Format("{0:dddd - dd/MM/yyyy}", startDate);
            }
            return string.Empty;
        }

    }

}
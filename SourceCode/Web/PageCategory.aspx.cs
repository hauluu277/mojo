// Author:					    
// Created:				        2005-06-26
//	Last Modified:              2013-01-17
// 
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software. 

using log4net;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.Framework;
using mojoPortal.SearchIndex;
using Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArticleFeature.Business;
using System.IO;
using System.Configuration;
using System.Web;

namespace mojoPortal.Web.UI
{

    public partial class PageCategory : NonCmsBasePage
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(PageCategory));

        private string query = string.Empty;
        private int pageNumber = 1;
        private int pageSize = WebConfigSettings.SearchResultsPageSize;
        private int totalHits = 0;
        private int totalPages = 1;
        private bool indexVerified = false;
        private bool showModuleTitleInResultLink = WebConfigSettings.ShowModuleTitleInSearchResultLink;
        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkText = SwirlingQuestionResource.ButtonDelete;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkText = SwirlingQuestionResource.ButtonEdit;
        protected string EditLinkTooltip = ArticleResources.BlogEditEntryLink;
        protected string EditLinkImageUrl = string.Empty;
        private bool isSiteEditor = false;
        private Guid featureGuid = Guid.Empty;
        private bool queryErrorOccurred = false;
        private DateTime modifiedBeginDate = DateTime.MinValue;
        private DateTime modifiedEndDate = DateTime.MaxValue;
        private TimeZoneInfo timeZone = null;
        private string keyword = string.Empty;
        private string author = string.Empty;
        private string datePublished = string.Empty;
        private string sapo = string.Empty;
        private int category = 0;
        private int typeSearch = TypeSearchArticleConstant.All;
        private int searchWith = TypeSearchArticleConstant.SearchAll;
        public bool IsEditable = false;
        private int pageId = 1;



        #region OnInit
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);

            SuppressMenuSelection();
            SuppressPageMenu();

            if (WebConfigSettings.ShowLeftColumnOnSearchResults) { StyleCombiner.AlwaysShowLeftColumn = true; }
            if (WebConfigSettings.ShowRightColumnOnSearchResults) { StyleCombiner.AlwaysShowRightColumn = true; }
        }

        #endregion

        private void Page_Load(object sender, EventArgs e)
        {
            if (SiteUtils.SslIsAvailable()) { SiteUtils.ForceSsl(); }

            LoadSettings();
            this.query = string.Empty;

            if (siteSettings == null)
            {
                siteSettings = CacheHelper.GetCurrentSiteSettings();
            }
            PopulateLabels();
            if (!IsPostBack)
            {
                pnlContentArticle.Visible = false;
                pnlShowListCategory.Visible = false;


                var coreCategory = new CoreCategory(category);
                lnkCurrentPage.Text = coreCategory.Name;
                lnkCurrentPage.ToolTip = coreCategory.Name;
                lnkCurrentPage.NavigateUrl = SiteRoot + coreCategory.Description;
                if (coreCategory.ShowCategoryChild)
                {
                    //hiển thị danh sách chuyên mục con   
                    pnlShowListCategory.Visible = true;
                    rptCategory.DataSource = CoreCategory.GetChildrenTotalArticle(coreCategory.ItemID);
                    rptCategory.DataBind();
                    lblCategory.Text = coreCategory.Name;
                }
                else
                {
                    //Hiển thị danh sách tin bài
                    pnlContentArticle.Visible = true;
                    var listArticle = Article.GetPageByCategory(pageNumber, pageSize, category.ToString(), out totalPages);
                    rptArticle.DataSource = listArticle;
                    rptArticle.DataBind();

                    string pageUrl = SiteRoot + "/pagecategory.aspx"
                     + "?cat=" + category
                     + "&pagenumber={0}";

                    pgr.PageURLFormat = pageUrl;
                    pgr.ShowFirstLast = true;
                    pgr.PageSize = pageSize;
                    pgr.PageCount = totalPages;
                    pgr.CurrentIndex = pageNumber;
                    pnlArticlePager.Visible = totalPages > 1;
                }


                pnlLeftMenu.Visible = false;
                if (category > 0)
                {

                    var getCategory = new CoreCategory(category);
                    if (getCategory.ShowMenuLeft)
                    {
                        pnlLeftMenu.Visible = true;
                        pnlLeftMenu.CssClass = "left-menu";
                        pnlContentArticle.CssClass = "content-article";
                        var listCategory = CoreCategory.GetChildren(getCategory.ParentID);
                        rptLeftCategory.DataSource = listCategory;
                        rptLeftCategory.DataBind();
                    }
                }
            }
        }
        
        protected List<Article> LoadArticle(object categoryId)
        {
            var listArticle = Article.GetArticleTop(Convert.ToInt32(categoryId), 500);
            return listArticle;
        }
        protected string BuildEditUrl(int itemID)
        {
            return SiteRoot + "/Article/PostArticle.aspx?ItemID=" + itemID;
        }
        private void LoadSettings()
        {
            ///load nội dung trái , phải
            LoadSideContent(false, true);

            isSiteEditor = WebUser.IsAdminOrContentAdmin || (SiteUtils.UserIsSiteEditor());
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            category = WebUtils.ParseInt32FromQueryString("cat", category);
            if (Request.IsAuthenticated)
            {
                SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
                if (siteUser != null)
                {
                    if (siteUser.IsInRoles("Admins") || siteUser.IsInRoles(WebConfigSettings.RoleManageArticle))
                    {
                        IsEditable = true;
                    }
                }
            }
        }


        private void PopulateLabels()
        {
            if (siteSettings == null) return;
            var coreCategory = new CoreCategory(category);
            Title = SiteUtils.FormatPageTitle(siteSettings, coreCategory.Name);
            //heading.Text = Resource.SearchPageTitle;

            MetaDescription = string.Format(CultureInfo.InvariantCulture,
            Resource.MetaDescriptionSearchFormat, siteSettings.SiteName);
            //btnDoSearch.Text = Resource.SearchButtonText;

            //this page has no content other than nav
            SiteUtils.AddNoIndexFollowMeta(Page);

            AddClassToBody("searchresults");
        }
        protected string FormatArticleDate(DateTime startDate)
        {
            return string.Format("{0:dd/MM/yyyy HH:mm}", startDate);
            //if (config.DateTimeFormat == string.Empty) return string.Empty;
            //string result = "";
            //result = timeZone != null ? TimeZoneInfo.ConvertTimeFromUtc(startDate, timeZone).ToString(config.DateTimeFormat) : startDate.AddHours(TimeOffset).ToString(config.DateTimeFormat);
            //return result;
        }
    }
}

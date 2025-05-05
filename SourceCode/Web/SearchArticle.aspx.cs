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

namespace mojoPortal.Web.UI.Pages
{

    public partial class SearchArticle : NonCmsBasePage
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(SearchArticle));

        private string query = string.Empty;
        private int pageNumber = 1;
        private int pageSize = WebConfigSettings.SearchResultsPageSize;
        private int totalHits = 0;
        private int totalPages = 1;
        private bool indexVerified = false;
        private bool showModuleTitleInResultLink = WebConfigSettings.ShowModuleTitleInSearchResultLink;
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



        #region OnInit
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);
            this.btnSearch.Click += new EventHandler(btnSearch_Click);

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
            string primarySearchProvider = SiteUtils.GetPrimarySearchProvider();

            if (!IsPostBack)
            {
                BindSearchResult();
                SetupHeightLightScript();
            }
        }

        private void SetupInternalSearch()
        {


            //got here by a cross page postback from another page if Page.PreviousPage is not null
            // this occurs when the seach input is used in the skin rather than the search link
            if (Page.PreviousPage != null)
            {
                HandleCrossPagePost();
            }
            else
            {
            }


        }

        private void HandleCrossPagePost()
        {

            SearchInput previousPageSearchInput = (SearchInput)PreviousPage.Master.FindControl("SearchInput1");
            // try in page if not found in masterpage
            if (previousPageSearchInput == null) { previousPageSearchInput = (SearchInput)PreviousPage.FindControl("SearchInput1"); }

            if (previousPageSearchInput != null)
            {
                TextBox prevSearchTextBox = (TextBox)previousPageSearchInput.FindControl("txtSearch");
                if ((prevSearchTextBox != null) && (prevSearchTextBox.Text.Length > 0))
                {
                    //this.txtSearchInput.Text = prevSearchTextBox.Text;
                    WebUtils.SetupRedirect(this, SiteRoot + "/SearchResults.aspx?q=" + Server.UrlEncode(prevSearchTextBox.Text));
                    return;
                }
            }
        }

        private void BindTypeSearch()
        {
            ddlTypeSearch.DataTextField = "Text";
            ddlTypeSearch.DataValueField = "Value";
            ddlTypeSearch.DataSource = TypeSearchArticleConstant.GetListItem();
            ddlTypeSearch.DataBind();
        }
        private void BindSearchWith()
        {
            rbtListSearch.DataValueField = "Value";
            rbtListSearch.DataTextField = "Text";
            rbtListSearch.DataSource = TypeSearchArticleConstant.GetListSearch();
            rbtListSearch.DataBind();
        }

        private void BindCategory()
        {
            var coreCategory = new CoreCategory(siteSettings.ArticleCategory);
            ListItem list = new ListItem
            {
                Text = coreCategory.Name,
                Value = siteSettings.ArticleCategory.ToString()
            };
            ddlCategory.Items.Add(list);
            PopulateChildNode(ddlCategory);
            ddlCategory.Items.RemoveAt(0);
            ddlCategory.Items.Insert(0, new ListItem("--Tất cả--", "0"));
        }

        private void PopulateChildNode(ListControl root)
        {
            for (int i = 0; i < root.Items.Count; i++)
            {
                List<CoreCategory> children = CoreCategory.GetChildrenByParent(int.Parse(root.Items[i].Value));
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
                        Value = child.ItemID.ToString(),
                    };
                    root.Items.Insert(root.Items.IndexOf(root.Items[i]) + index, list);
                    index++;
                }
            }
        }

        private void BindSearchResult()
        {
            BindTypeSearch();
            BindSearchWith();
            BindCategory();

            ddlTypeSearch.SelectedValue = typeSearch.ToString();
            rbtListSearch.SelectedValue = searchWith.ToString();
            ddlCategory.SelectedValue = category.ToString();

            //txtAuthor.Text = author;
            txtKeyword.Text = keyword;
            //txtSapo.Text = sapo;
            txtStartDate.Value = datePublished;

            DateTime? date = null;
            if (!string.IsNullOrEmpty(datePublished))
            {
                date = datePublished.ToDateTime();
            }
            var totalArticle = 0;
            DateTime? searchDate = null;
            if (typeSearch > 0)
            {
                if (typeSearch == TypeSearchArticleConstant.OneYear)
                {
                    searchDate = DateTime.Now.AddYears(-1);
                }
                else if (typeSearch == TypeSearchArticleConstant.OneMonth)
                {
                    searchDate = DateTime.Now.AddMonths(-1);
                }
                else if (typeSearch == TypeSearchArticleConstant.ThreeMonth)
                {
                    searchDate = DateTime.Now.AddMonths(-3);
                }
                else if (typeSearch == TypeSearchArticleConstant.SixMonth)
                {
                    searchDate = DateTime.Now.AddMonths(-6);
                }
                else if (typeSearch == TypeSearchArticleConstant.NineMonth)
                {
                    searchDate = DateTime.Now.AddMonths(-9);
                }
                else if (typeSearch == TypeSearchArticleConstant.OneWeek)
                {
                    searchDate = DateTime.Now.AddDays(-7);
                }
            }
            //var keyword_fts = string.Empty;
            //if (!string.IsNullOrEmpty(keyword))
            //{
            //    keyword = keyword.Trim();
            //    keyword_fts = keyword.ConvertToFTS();
            //}
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim();

            }
            var result = ArticleSortBO.GetSearchPublished(siteSettings.SiteId, searchWith, keyword.ConvertToFTS(), category, date, searchDate, pageNumber, pageSize, out totalPages, out totalArticle);
            rptArticle.DataSource = result;
            rptArticle.DataBind();
            string pageUrl = SiteRoot + "/SearchArticle.aspx?q=" + keyword + "&date=" + datePublished + "&typesearch=" + typeSearch + "&pagenumber={0}";

            pgr.PageURLFormat = pageUrl;
            pgr.ShowFirstLast = true;
            pgr.PageSize = pageSize;
            pgr.PageCount = totalPages;
            pgr.CurrentIndex = pageNumber;
            pnlArticlePager.Visible = (totalPages > 1);
            if (!string.IsNullOrEmpty(keyword) || !string.IsNullOrEmpty(datePublished))
            {
                string resultCount = $"<p><strong>{totalArticle}</strong> kết quả tìm kiếm cho từ khóa <strong>{keyword}</strong></p>";
                literSearchResult.Text = resultCount;
            }
        }

        protected string FormatUrlArtcile(string url)
        {
            if (string.IsNullOrEmpty(url)) return string.Empty;
            return SiteRoot + url.Replace("~", string.Empty);
        }


        private void SetupHeightLightScript()
        {

            StringBuilder script = new StringBuilder();
            script.Append("\n<script type='text/javascript'>");
            script.Append("$(document).ready(function() {");
            script.Append("var instance = new Mark(document.querySelector('#search_div'));");
            script.Append("instance.mark('" + keyword + "', {");
            script.Append("'element': 'span',");
            script.Append("'className': 'highlight'");
            script.Append("});");
            script.Append("});");
            script.Append("</script>");
            Page.ClientScript.RegisterStartupScript(typeof(Page), "loadHight", script.ToString());
        }

        protected string FormatArticleDate(DateTime startDate)
        {
            return string.Format("{0:HH:mm | dd/MM/yyyy}", startDate);
            //if (config.DateTimeFormat == string.Empty) return string.Empty;
            //string result = "";
            //result = timeZone != null ? TimeZoneInfo.ConvertTimeFromUtc(startDate, timeZone).ToString(config.DateTimeFormat) : startDate.AddHours(TimeOffset).ToString(config.DateTimeFormat);
            //return result;
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            keyword = txtKeyword.Text;
            //author = txtAuthor.Text;
            datePublished = txtStartDate.Value;
            //sapo = txtSapo.Text;
            typeSearch = ddlTypeSearch.SelectedValue.ToIntOrZero();
            searchWith = rbtListSearch.SelectedValue.ToIntOrZero();
            category = ddlCategory.SelectedValue.ToIntOrZero();
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim();
            }
            var redirectUrl = "/SearchArticle.aspx?q=" + keyword + "&date=" + datePublished + "&typesearch=" + typeSearch + "&searchwith=" + searchWith + "&category=" + category;
            SiteUtils.RedirectToUrl(redirectUrl);

        }


        private void LoadSettings()
        {
            isSiteEditor = WebUser.IsAdminOrContentAdmin || (SiteUtils.UserIsSiteEditor());
            keyword = WebUtils.ParseStringFromQueryString("q", keyword);
            author = WebUtils.ParseStringFromQueryString("author", author);
            datePublished = WebUtils.ParseStringFromQueryString("date", datePublished);
            sapo = WebUtils.ParseStringFromQueryString("sapo", sapo);
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            typeSearch = WebUtils.ParseInt32FromQueryString("typesearch", typeSearch);
            searchWith = WebUtils.ParseInt32FromQueryString("searchwith", searchWith);
            category = WebUtils.ParseInt32FromQueryString("category", category);


            featureGuid = WebUtils.ParseGuidFromQueryString("f", featureGuid);
            modifiedBeginDate = WebUtils.ParseDateFromQueryString("bd", DateTime.MinValue).Date;
            modifiedEndDate = WebUtils.ParseDateFromQueryString("ed", DateTime.MaxValue).Date;
        }


        private void PopulateLabels()
        {
            if (siteSettings == null) return;

            Title = SiteUtils.FormatPageTitle(siteSettings, Resource.SearchPageTitle);
            //heading.Text = Resource.SearchPageTitle;

            MetaDescription = string.Format(CultureInfo.InvariantCulture,
            Resource.MetaDescriptionSearchFormat, siteSettings.SiteName);
            //btnDoSearch.Text = Resource.SearchButtonText;
            SiteUtils.SetButtonAccessKey(btnSearch, AccessKeys.SearchButtonTextAccessKey);

            //this page has no content other than nav
            SiteUtils.AddNoIndexFollowMeta(Page);

            AddClassToBody("searchresults");
        }
    }
}

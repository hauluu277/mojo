/// Author:					    Joe Audette
/// Created:				    2007-08-30
/// Last Modified:			    2009-07-22
/// 
/// The use and distribution terms for this software are covered by the 
/// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)  
/// which can be found in the file CPL.TXT at the root of this distribution.
/// By using this software in any fashion, you are agreeing to be bound by 
/// the terms of this license.
///
/// You must not remove this notice, or any other, from this software.

using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Threading;
using log4net;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web;
using mojoPortal.SearchIndex;
using ArticleFeature.Business;

namespace mojoPortal.Features
{
    public class ArticleIndexBuilderProvider : IndexBuilderProvider
    {
        public ArticleIndexBuilderProvider()
        { }

        private static readonly ILog log
            = LogManager.GetLogger(typeof(ArticleIndexBuilderProvider));

        public override void RebuildIndex(
            PageSettings pageSettings,
            string indexPath)
        {
            if (WebConfigSettings.DisableSearchIndex) { return; }

            if (pageSettings == null)
            {
                if (log.IsErrorEnabled)
                {
                    log.Error("pageSettings object passed to ArticleIndexBuilderProvider.RebuildIndex was null");
                }
                return;
            }

            //don't index pending/unpublished pages
            if (pageSettings.IsPending) { return; }

            log.Info("ArticleIndexBuilderProvider indexing page - "
                + pageSettings.PageName);

            //try
            //{
            Guid articleFeatureGuid = new Guid(ArticleConstant.FEATUREGUID);
            ModuleDefinition articleFeature = new ModuleDefinition(articleFeatureGuid);

            List<PageModule> pageModules
                    = PageModule.GetPageModulesByPage(pageSettings.PageId);

            DataTable dataTable
                = Article.GetArticlesByPage(
                pageSettings.SiteId,
                pageSettings.PageId);

            foreach (DataRow row in dataTable.Rows)
            {
                IndexItem indexItem = new IndexItem();
                indexItem.SiteId = pageSettings.SiteId;
                indexItem.PageId = pageSettings.PageId;
                indexItem.PageName = pageSettings.PageName;
                indexItem.ViewRoles = pageSettings.AuthorizedRoles;
                indexItem.ModuleViewRoles = row["ViewRoles"].ToString();
                indexItem.FeatureId = articleFeatureGuid.ToString();
                indexItem.FeatureName = articleFeature.FeatureName;
                indexItem.FeatureResourceFile = articleFeature.ResourceFile;

                indexItem.ItemId = Convert.ToInt32(row["ItemID"], CultureInfo.InvariantCulture);
                indexItem.ModuleId = Convert.ToInt32(row["ModuleID"], CultureInfo.InvariantCulture);
                indexItem.ModuleTitle = row["ModuleTitle"].ToString();
                indexItem.Title = row["Title"].ToString();
                indexItem.ViewPage = row["ItemUrl"].ToString().Replace("~/", string.Empty);
                //indexItem.ArticleFTS = row["FTS"].ToString();

                indexItem.PageMetaDescription = row["MetaDescription"].ToString();
                indexItem.PageMetaKeywords = row["MetaKeywords"].ToString();

                DateTime articleStart = Convert.ToDateTime(row["StartDate"]);

                if (indexItem.ViewPage.Length > 0)
                {
                    indexItem.UseQueryStringParams = false;
                }
                else
                {
                    indexItem.ViewPage = "Article/ViewPost.aspx";
                }
                indexItem.Content = row["Description"].ToString();
                //int commentCount = Convert.ToInt32(row["CommentCount"]);

                //if (commentCount > 0)
                //{	// index comments
                //    StringBuilder stringBuilder = new StringBuilder();
                //    DataTable comments = Article.GetBlogCommentsTable(indexItem.ModuleId, indexItem.ItemId);

                //    foreach (DataRow commentRow in comments.Rows)
                //    {
                //        stringBuilder.Append("  " + commentRow["Comment"].ToString());
                //        stringBuilder.Append("  " + commentRow["Name"].ToString());

                //        if (log.IsDebugEnabled) log.Debug("ArticleIndexBuilderProvider.RebuildIndex add comment ");

                //    }

                //    indexItem.OtherContent = stringBuilder.ToString();

                //}

                // lookup publish dates
                foreach (PageModule pageModule in pageModules)
                {
                    if (indexItem.ModuleId == pageModule.ModuleId)
                    {
                        indexItem.PublishBeginDate = pageModule.PublishBeginDate;
                        indexItem.PublishEndDate = pageModule.PublishEndDate;
                    }
                }

                if (articleStart > indexItem.PublishBeginDate) { indexItem.PublishBeginDate = articleStart; }



                IndexHelper.RebuildIndex(indexItem, indexPath);

                if (log.IsDebugEnabled) log.Debug("Indexed " + indexItem.Title);

            }
            //}
            //catch (Exception ex)
            //{
            //    log.Error(ex);
            //}


        }


        public override void ContentChangedHandler(
            object sender,
            ContentChangedEventArgs e)
        {
            if (WebConfigSettings.DisableSearchIndex) { return; }
            if (sender == null) return;
            if (!(sender is Article)) return;

            Article article = (Article)sender;
            SiteSettings siteSettings = CacheHelper.GetCurrentSiteSettings();
            article.SiteId = siteSettings.SiteId;
            article.SearchIndexPath = IndexHelper.GetSearchIndexPath(siteSettings.SiteId);


            if (e.IsDeleted)
            {
                // get list of pages where this module is published
                List<PageModule> pageModules
                    = PageModule.GetPageModulesByModule(article.ModuleID);

                foreach (PageModule pageModule in pageModules)
                {
                    IndexHelper.RemoveIndexItem(
                        pageModule.PageId,
                        article.ModuleID,
                        article.ItemID);
                }
            }
            else
            {
                if (ThreadPool.QueueUserWorkItem(new WaitCallback(IndexItem), article))
                {
                    if (log.IsDebugEnabled) log.Debug("ArticleIndexBuilderProvider.IndexItem queued");
                }
                else
                {
                    if (log.IsErrorEnabled) log.Error("Failed to queue a thread for ArticleIndexBuilderProvider.IndexItem");
                }
                //IndexItem(blog);
            }


        }


        private static void IndexItem(object o)
        {
            if (WebConfigSettings.DisableSearchIndex) { return; }
            if (o == null) return;
            if (!(o is Article)) return;

            Article content = o as Article;
            IndexItem(content);

        }


        private static void IndexItem(Article article)
        {
            if (WebConfigSettings.DisableSearchIndex) { return; }
            if (article == null)
            {
                if (log.IsErrorEnabled)
                {
                    log.Error("article object passed to ArticleIndexBuilderProvider.IndexItem was null");
                }
                return;
            }

            Module module = new Module(article.ModuleID);
            Guid articleFeatureGuid = new Guid(ArticleConstant.FEATUREGUID);
            ModuleDefinition articleFeature = new ModuleDefinition(articleFeatureGuid);

            // get comments so  they can be indexed too
            //StringBuilder stringBuilder = new StringBuilder();
            //using (IDataReader comments = Article.GetBlogComments(article.ModuleID, article.ItemID))
            //{
            //    while (comments.Read())
            //    {
            //        stringBuilder.Append("  " + comments["Comment"].ToString());
            //        stringBuilder.Append("  " + comments["Name"].ToString());

            //        if (log.IsDebugEnabled) log.Debug("ArticleIndexBuilderProvider.IndexItem add comment ");

            //    }
            //}

            // get list of pages where this module is published
            List<PageModule> pageModules
                = PageModule.GetPageModulesByModule(article.ModuleID);

            foreach (PageModule pageModule in pageModules)
            {
                PageSettings pageSettings
                    = new PageSettings(
                    article.SiteId,
                    pageModule.PageId);

                //don't index pending/unpublished pages
                if (pageSettings.IsPending) { continue; }

                IndexItem indexItem = new IndexItem();
                if (article.SearchIndexPath.Length > 0)
                {
                    indexItem.IndexPath = article.SearchIndexPath;
                }
                indexItem.SiteId = article.SiteId;
                indexItem.PageId = pageSettings.PageId;
                indexItem.PageName = pageSettings.PageName;
                indexItem.ViewRoles = pageSettings.AuthorizedRoles;
                indexItem.ModuleViewRoles = module.ViewRoles;
                if (article.ItemUrl.Length > 0)
                {
                    indexItem.ViewPage = article.ItemUrl.Replace("~/", string.Empty);
                    indexItem.UseQueryStringParams = false;
                }
                else
                {
                    indexItem.ViewPage = "Article/ViewPost.aspx";
                }

                indexItem.PageMetaDescription = article.MetaDescription;
                indexItem.PageMetaKeywords = article.MetaKeywords;
                indexItem.ItemId = article.ItemID;
                indexItem.ModuleId = article.ModuleID;
                indexItem.ModuleTitle = module.ModuleTitle;
                indexItem.Title = article.Title;
                indexItem.ArticleFTS = article.FTS;
                indexItem.Content = article.Description + " " + article.MetaDescription + " " + article.MetaKeywords;
                indexItem.FeatureId = articleFeatureGuid.ToString();
                indexItem.FeatureName = articleFeature.FeatureName;
                indexItem.FeatureResourceFile = articleFeature.ResourceFile;

                indexItem.OtherContent = string.Empty;// stringBuilder.ToString();
                indexItem.PublishBeginDate = pageModule.PublishBeginDate;
                indexItem.PublishEndDate = pageModule.PublishEndDate;

                if (article.StartDate > pageModule.PublishBeginDate) { indexItem.PublishBeginDate = article.StartDate; }

                IndexHelper.RebuildIndex(indexItem);
            }

            if (log.IsDebugEnabled) log.Debug("Indexed " + article.Title);

        }

    }
}

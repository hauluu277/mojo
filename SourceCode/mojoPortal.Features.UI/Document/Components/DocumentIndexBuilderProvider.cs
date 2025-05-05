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
    public class DocumentIndexBuilderProvider : IndexBuilderProvider
    {
        public DocumentIndexBuilderProvider()
        { }

        private static readonly ILog log
            = LogManager.GetLogger(typeof(DocumentIndexBuilderProvider));

        public override void RebuildIndex(
            PageSettings pageSettings,
            string indexPath)
        {
            if (WebConfigSettings.DisableSearchIndex) { return; }

            if (pageSettings == null)
            {
                if (log.IsErrorEnabled)
                {
                    log.Error("pageSettings object passed to DocumentIndexBuilderProvider.RebuildIndex was null");
                }
                return;
            }

            //don't index pending/unpublished pages
            if (pageSettings.IsPending) { return; }

            log.Info("DocumentIndexBuilderProvider indexing page - "
                + pageSettings.PageName);

            //try
            //{
            Guid documentFeatureGuid = new Guid(DocumentConstant.FEATUREGUID);
            ModuleDefinition documentFeature = new ModuleDefinition(documentFeatureGuid);

            List<PageModule> pageModules
                    = PageModule.GetPageModulesByPage(pageSettings.PageId);

            DataTable dataTable
                = Documentation.GetDocumentByPage(
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
                indexItem.FeatureId = documentFeatureGuid.ToString();
                indexItem.FeatureName = documentFeature.FeatureName;
                indexItem.FeatureResourceFile = documentFeature.ResourceFile;

                indexItem.ItemId = Convert.ToInt32(row["ItemID"], CultureInfo.InvariantCulture);
                indexItem.ModuleId = Convert.ToInt32(row["ModuleID"], CultureInfo.InvariantCulture);
                indexItem.ModuleTitle = row["ModuleTitle"].ToString();
                indexItem.Title = row["Sign"].ToString();
                indexItem.ViewPage = row["ItemUrl"].ToString().Replace("~/", string.Empty);

                //indexItem.PageMetaDescription = row["MetaDescription"].ToString();
                //indexItem.PageMetaKeywords = row["MetaKeywords"].ToString();

                DateTime documentPromulgate = Convert.ToDateTime(row["DatePromulgate"]);

                if (indexItem.ViewPage.Length > 0)
                {
                    indexItem.UseQueryStringParams = false;
                }
                else
                {
                    indexItem.ViewPage = "Document/ViewPost.aspx";
                }
                indexItem.Content = row["Summary"].ToString();
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

                if (documentPromulgate > indexItem.PublishBeginDate) { indexItem.PublishBeginDate = documentPromulgate; }



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

            Documentation document = (Documentation)sender;
            SiteSettings siteSettings = CacheHelper.GetCurrentSiteSettings();
            document.SiteID = siteSettings.SiteId;
            document.SearchIndexPath = IndexHelper.GetSearchIndexPath(siteSettings.SiteId);


            if (e.IsDeleted)
            {
                // get list of pages where this module is published
                List<PageModule> pageModules
                    = PageModule.GetPageModulesByModule(document.ModuleID);

                foreach (PageModule pageModule in pageModules)
                {
                    IndexHelper.RemoveIndexItem(
                        pageModule.PageId,
                        document.ModuleID,
                        document.ItemID);
                }
            }
            else
            {
                if (ThreadPool.QueueUserWorkItem(new WaitCallback(IndexItem), document))
                {
                    if (log.IsDebugEnabled) log.Debug("DocumentIndexBuilderProvider.IndexItem queued");
                }
                else
                {
                    if (log.IsErrorEnabled) log.Error("Failed to queue a thread for DocumentIndexBuilderProvider.IndexItem");
                }
                //IndexItem(blog);
            }


        }


        private static void IndexItem(object o)
        {
            if (WebConfigSettings.DisableSearchIndex) { return; }
            if (o == null) return;
            if (!(o is Documentation)) return;

            Documentation content = o as Documentation;
            IndexItem(content);

        }


        private static void IndexItem(Documentation document)
        {
            if (WebConfigSettings.DisableSearchIndex) { return; }
            if (document == null)
            {
                if (log.IsErrorEnabled)
                {
                    log.Error("document object passed to DocumentIndexBuilderProvider.IndexItem was null");
                }
                return;
            }

            Module module = new Module(document.ModuleID);
            Guid documentFeatureGuid = new Guid(DocumentConstant.FEATUREGUID);
            ModuleDefinition documentFeature = new ModuleDefinition(documentFeatureGuid);

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
                = PageModule.GetPageModulesByModule(document.ModuleID);

            foreach (PageModule pageModule in pageModules)
            {
                PageSettings pageSettings
                    = new PageSettings(
                    document.SiteID,
                    pageModule.PageId);

                //don't index pending/unpublished pages
                if (pageSettings.IsPending) { continue; }

                IndexItem indexItem = new IndexItem();
                if (document.SearchIndexPath.Length > 0)
                {
                    indexItem.IndexPath = document.SearchIndexPath;
                }
                indexItem.SiteId = document.SiteID;
                indexItem.PageId = pageSettings.PageId;
                indexItem.PageName = pageSettings.PageName;
                indexItem.ViewRoles = pageSettings.AuthorizedRoles;
                indexItem.ModuleViewRoles = module.ViewRoles;
                if (document.ItemUrl.Length > 0)
                {
                    indexItem.ViewPage = document.ItemUrl.Replace("~/", string.Empty);
                    indexItem.UseQueryStringParams = false;
                }
                else
                {
                    indexItem.ViewPage = "Article/ViewPost.aspx";
                }

                //indexItem.PageMetaDescription = article.MetaDescription;
                //indexItem.PageMetaKeywords = article.MetaKeywords;
                indexItem.ItemId = document.ItemID;
                indexItem.ModuleId = document.ModuleID;
                indexItem.ModuleTitle = module.ModuleTitle;
                indexItem.Title = document.Sign;
                indexItem.Content = document.Summary;
                indexItem.FeatureId = documentFeature.ToString();
                indexItem.FeatureName = documentFeature.FeatureName;
                indexItem.FeatureResourceFile = documentFeature.ResourceFile;

                indexItem.OtherContent = string.Empty;// stringBuilder.ToString();
                indexItem.PublishBeginDate = pageModule.PublishBeginDate;
                indexItem.PublishEndDate = pageModule.PublishEndDate;
                if (document.DatePromulgate.HasValue)
                {
                    if (document.DatePromulgate > pageModule.PublishBeginDate) { indexItem.PublishBeginDate = document.DatePromulgate.Value; }
                }
                IndexHelper.RebuildIndex(indexItem);
            }

            if (log.IsDebugEnabled) log.Debug("Indexed " + document.Sign);

        }

    }
}

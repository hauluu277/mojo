
// Author:					HiNet JSC
// Created:					2014-7-2
// Last Modified:			2014-7-2
// 
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)  
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software.

using ArticleFeature.Data;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features.Business.QLLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace ArticleFeature.Business
{

    public class Article : IIndexableContent
    {
        private const string featureGuid = "8bdb1450-91e5-4cb0-af1a-2427d7e7e611";

        public static Guid FeatureGuid
        {
            get { return new Guid(featureGuid); }
        }
        #region Constructors

        public Article()
        { }


        public Article(
            int itemID)
        {
            GetArticle(
                itemID);
        }
        /// <summary>
        /// LAY ARTICLE BY EVENT
        /// </summary>
        /// <param name="eventID"></param>
        /// <param name="allow"></param>
        public Article(int eventID, bool allow)
        {
            GetOneArticleByEvent(eventID);
        }
        #endregion

        #region Private Properties

        private int itemID = -1;
        private int eventID = -1;
        private int moduleID = -1;
        private int categoryID = -1;
        private string title = string.Empty;
        private string summary = string.Empty;
        private string description = string.Empty;
        private string imageUrl = string.Empty;
        private string audioUrl = string.Empty;
        private DateTime startDate = DateTime.UtcNow;
        private DateTime? endDate = null;
        private int commentCount = 0;
        private int hitCount = 0;
        private Guid articleGuid = Guid.Empty;
        private Guid moduleGuid = Guid.Empty;
        private string location = string.Empty;
        private Guid userGuid = Guid.Empty;
        private Guid pollGuid = Guid.Empty;
        private string createdByUser = string.Empty;
        private DateTime createdDate = DateTime.UtcNow;
        private Guid lastModUserGuid = Guid.Empty;
        private DateTime lastModUtc = DateTime.UtcNow;
        private string itemUrl = string.Empty;
        private string metaTitle = string.Empty;
        private string metaKeywords = string.Empty;
        private string metaDescription = string.Empty;
        private string compiledMeta = string.Empty;
        private bool? isApproved = null;
        private Guid approvedGuid = Guid.Empty;
        private DateTime approvedDate = DateTime.UtcNow;
        private bool? isPublished = null;
        private Guid publishedGuid = Guid.Empty;
        private DateTime publishedDate = DateTime.UtcNow;
        private bool includeInFeed = false;
        private bool allowComment = false;
        private bool isHot = false;
        private string tag = string.Empty;
        //Full text search
        private string fts = string.Empty;
        private bool isHome = false;
        private int siteId = -1;
        private string commentByBoss = string.Empty;
        private string searchIndexPath = string.Empty;

        private string categoryName = string.Empty;
        private bool allowWCAG = false;
        private string metaCreator = string.Empty;
        private string metaIdentifier = string.Empty;
        private string metaPublisher = string.Empty;
        private DateTime? metaDate = null;
        private bool isDelete = false;
        private string articleReference = string.Empty;
        //private bool isHotNew = false;

        private string categoryUrl = string.Empty;
        private string siteName = string.Empty;
        private string hostName = string.Empty;
        private string titleFTS = string.Empty;
        private string authorFTS = string.Empty;
        private string sapoFTS = string.Empty;
        private DateTime createDateArticle = DateTime.Now;

        public bool isCongThanhVien = false;
        private string viTriHienThiNgayDang = string.Empty;
        private bool isHienThiTacGia = false;
        private string clientName = string.Empty;

        private bool isNew = false;

        #endregion

        #region Public Properties
        public bool IsNew
        {
            get { return isNew; }
            set { isNew = value; }
        }
        public string ClientName
        {
            get { return clientName; }
            set { clientName = value; }
        }
        public bool IsCongThanhVien
        {
            get { return isCongThanhVien; }
            set { isCongThanhVien = value; }
        }
        public DateTime CreateDateArticle { get { return createDateArticle; } set { createDateArticle = value; } }
        public string SapoFTS
        {
            get { return sapoFTS; }
            set { sapoFTS = value; }
        }
        public string AuthorFTS
        {
            get { return authorFTS; }
            set { authorFTS = value; }
        }
        public string TitleFTS
        {
            get { return titleFTS; }
            set { titleFTS = value; }
        }
        public string HostName
        {
            get { return hostName; }
            set { hostName = value; }
        }
        public string SiteName
        {
            get { return siteName; }
            set
            { siteName = value; }
        }
        public string CategoryUrl
        {
            get { return categoryUrl; }
            set { categoryUrl = value; }
        }
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public Guid ArticleGuid
        {
            get { return articleGuid; }

        }
        public int EventID
        {
            get { return eventID; }
            set { eventID = value; }
        }
        public int ModuleID
        {
            get { return moduleID; }
            set { moduleID = value; }
        }
        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Summary
        {
            get { return summary; }
            set { summary = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; }
        }
        public string AudioUrl
        {
            get { return audioUrl; }
            set { audioUrl = value; }
        }
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        public DateTime? EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        public int CommentCount
        {
            get { return commentCount; }
            set { commentCount = value; }
        }
        public int HitCount
        {
            get { return hitCount; }
            set { hitCount = value; }
        }
        public Guid ModuleGuid
        {
            get { return moduleGuid; }
            set { moduleGuid = value; }
        }
        public string Location
        {
            get { return location; }
            set { location = value; }
        }
        public Guid PollGuid
        {
            get { return pollGuid; }
            set { pollGuid = value; }
        }
        public Guid UserGuid
        {
            get { return userGuid; }
            set { userGuid = value; }
        }
        public string CreatedByUser
        {
            get { return createdByUser; }
            set { createdByUser = value; }
        }
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }
        public Guid LastModUserGuid
        {
            get { return lastModUserGuid; }
            set { lastModUserGuid = value; }
        }
        public DateTime LastModUtc
        {
            get { return lastModUtc; }
            set { lastModUtc = value; }
        }
        public string ItemUrl
        {
            get { return itemUrl; }
            set { itemUrl = value; }
        }
        public string MetaTitle
        {
            get { return metaTitle; }
            set { metaTitle = value; }
        }
        public string MetaKeywords
        {
            get { return metaKeywords; }
            set { metaKeywords = value; }
        }
        public string MetaDescription
        {
            get { return metaDescription; }
            set { metaDescription = value; }
        }
        public string CompiledMeta
        {
            get { return compiledMeta; }
            set { compiledMeta = value; }
        }
        public bool? IsApproved
        {
            get { return isApproved; }
            set { isApproved = value; }
        }
        public Guid ApprovedGuid
        {
            get { return approvedGuid; }
            set { approvedGuid = value; }
        }
        public DateTime ApprovedDate
        {
            get { return approvedDate; }
            set { approvedDate = value; }
        }
        public bool? IsPublished
        {
            get { return isPublished; }
            set { isPublished = value; }
        }
        public Guid PublishedGuid
        {
            get { return publishedGuid; }
            set { publishedGuid = value; }
        }
        public DateTime PublishedDate
        {
            get { return publishedDate; }
            set { publishedDate = value; }
        }
        public bool IncludeInFeed
        {
            get { return includeInFeed; }
            set { includeInFeed = value; }
        }
        public bool AllowComment
        {
            get { return allowComment; }
            set { allowComment = value; }
        }
        public bool IsHot
        {
            get { return isHot; }
            set { isHot = value; }
        }
        public string Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        public string FTS
        {
            get { return fts; }
            set { fts = value; }
        }

        public string CommentByBoss
        {
            get { return commentByBoss; }
            set { commentByBoss = value; }
        }
        public bool IsHome
        {
            get { return isHome; }
            set { isHome = value; }
        }
        /// <summary>
        /// This is not persisted to the db. It is only set and used when indexing forum threads in the search index.
        /// Its a convenience because when we queue the task to index on a new thread we can only pass one object.
        /// So we store extra properties here so we don't need any other objects.
        /// </summary>
        public int SiteId
        {
            get { return siteId; }
            set { siteId = value; }
        }

        /// <summary>
        /// This is not persisted to the db. It is only set and used when indexing forum threads in the search index.
        /// Its a convenience because when we queue the task to index on a new thread we can only pass one object.
        /// So we store extra properties here so we don't need any other objects.
        /// </summary>
        public string SearchIndexPath
        {
            get { return searchIndexPath; }
            set { searchIndexPath = value; }
        }
        public bool AllowWCAG
        {
            get { return allowWCAG; }
            set { allowWCAG = value; }
        }
        public string MetaCreator
        {
            get { return metaCreator; }
            set { metaCreator = value; }
        }
        public string MetaIdentifier
        {
            get { return metaIdentifier; }
            set { metaIdentifier = value; }
        }
        public string MetaPublisher
        {
            get { return metaPublisher; }
            set { metaPublisher = value; }
        }
        public DateTime? MetaDate
        {
            get { return metaDate; }
            set { metaDate = value; }
        }
        //public bool IsHotNew
        //{
        //    get { return isHotNew; }
        //    set { isHotNew = value; }
        //}

        public bool IsDelete
        {
            get { return isDelete; }
            set { isDelete = value; }
        }
        public string ArticleReference
        {
            get { return articleReference; }
            set { articleReference = value; }
        }

        public string ViTriHienThiNgayDang
        {
            get { return viTriHienThiNgayDang; }
            set { viTriHienThiNgayDang = value; }
        }
        public bool IsHienThiTacGia
        {
            get { return isHienThiTacGia; }
            set { isHienThiTacGia = value; }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of Article.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetArticle(
            int itemID)
        {
            using (IDataReader reader = DBArticles.GetOne(
                itemID))
            {
                PopulateFromReader(reader);
            }

        }
        private void GetOneArticleByEvent(int eventID)
        {
            using (IDataReader reader = DBArticles.GetOneArticleByEvent(
    eventID))
            {
                PopulateFromReader(reader);
            }
        }

        private static List<Article> PopulateFromReaderAndEventID(IDataReader reader)
        {
            List<Article> articleList = new List<Article>();
            try
            {
                while (reader.Read())
                {
                    Article article = new Article();
                    article.itemID = Convert.ToInt32(reader["ItemID"]);
                    article.moduleID = Convert.ToInt32(reader["ModuleID"]);

                    if (!String.IsNullOrEmpty(reader["CategoryID"].ToString()))
                    {
                        article.categoryID = Convert.ToInt32(reader["CategoryID"]);
                    }

                    article.title = reader["Title"].ToString();
                    article.summary = reader["Summary"].ToString();
                    article.description = reader["Description"].ToString();
                    article.imageUrl = reader["ImageUrl"].ToString();
                    article.startDate = Convert.ToDateTime(reader["StartDate"]);
                    if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                    {
                        article.endDate = Convert.ToDateTime(reader["EndDate"]);
                    }
                    article.commentCount = Convert.ToInt32(reader["CommentCount"]);
                    article.hitCount = Convert.ToInt32(reader["HitCount"]);
                    article.articleGuid = new Guid(reader["ArticleGuid"].ToString());
                    article.moduleGuid = new Guid(reader["ModuleGuid"].ToString());
                    article.location = reader["Location"].ToString();
                    article.userGuid = new Guid(reader["UserGuid"].ToString());
                    article.createdByUser = reader["CreatedByUser"].ToString();
                    article.createdDate = Convert.ToDateTime(reader["CreatedDate"]);


                    if (!string.IsNullOrEmpty(reader["LastModUserGuid"].ToString()))
                    {
                        article.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                    }

                    if (!string.IsNullOrEmpty(reader["LastModUtc"].ToString()))
                    {
                        article.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                    }

                    article.itemUrl = reader["ItemUrl"].ToString();
                    article.metaTitle = reader["MetaTitle"].ToString();
                    article.metaKeywords = reader["MetaKeywords"].ToString();
                    article.metaDescription = reader["MetaDescription"].ToString();
                    if (!string.IsNullOrEmpty(reader["IsApproved"].ToString()))
                    {
                        article.isApproved = Convert.ToBoolean(reader["IsApproved"]);
                    }
                    if (article.isApproved.HasValue && article.isApproved.Value)
                    {
                        article.approvedGuid = new Guid(reader["ApprovedGuid"].ToString());
                        article.approvedDate = Convert.ToDateTime(reader["ApprovedDate"]);
                    }
                    article.allowComment = Convert.ToBoolean(reader["AllowComment"]);
                    article.isHot = Convert.ToBoolean(reader["IsHot"]);
                    article.isHome = Convert.ToBoolean(reader["IsHome"]);
                    article.tag = reader["Tag"].ToString();
                    if (!string.IsNullOrEmpty(reader["IsPublished"].ToString()))
                    {
                        article.isPublished = Convert.ToBoolean(reader["IsPublished"]);
                    }
                    if (article.isPublished.HasValue && article.isPublished.Value)
                    {
                        article.publishedGuid = new Guid(reader["PublishedGuid"].ToString());
                        article.publishedDate = Convert.ToDateTime(reader["PublishedDate"]);
                    }
                    if (!string.IsNullOrEmpty(reader["IncludeInFeed"].ToString()))
                    {
                        article.includeInFeed = Convert.ToBoolean(reader["IncludeInFeed"]);
                    }
                    article.commentByBoss = reader["CommentByBoss"].ToString();
                    if (!String.IsNullOrEmpty(reader["EventID"].ToString()))
                    {
                        article.eventID = int.Parse(reader["EventID"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["ArticleReference"].ToString()))
                    {
                        article.articleReference = reader["ArticleReference"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["TitleFTS"].ToString()))
                    {
                        article.titleFTS = reader["TitleFTS"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["AuthorFTS"].ToString()))
                    {
                        article.authorFTS = reader["AuthorFTS"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["SapoFTS"].ToString()))
                    {
                        article.sapoFTS = reader["SapoFTS"].ToString();
                    }

                    if (!string.IsNullOrEmpty(reader["ViTriHienThiNgayDang"].ToString()))
                    {
                        article.ViTriHienThiNgayDang = reader["ViTriHienThiNgayDang"].ToString();
                    }

                    articleList.Add(article);

                }
            }
            finally
            {
                reader.Close();
            }

            return articleList;


        }
        private void PopulateFromReader(IDataReader reader)
        {
            if (reader.Read())
            {
                this.itemID = Convert.ToInt32(reader["ItemID"]);
                this.moduleID = Convert.ToInt32(reader["ModuleID"]);
                if (!string.IsNullOrEmpty(reader["SiteID"].ToString()))
                {
                    this.siteId = Convert.ToInt32(reader["SiteID"]);
                }
                if (!String.IsNullOrEmpty(reader["CategoryID"].ToString()))
                {
                    this.categoryID = Convert.ToInt32(reader["CategoryID"]);
                }
                this.title = reader["Title"].ToString();
                this.summary = reader["Summary"].ToString();
                this.description = reader["Description"].ToString();
                this.imageUrl = reader["ImageUrl"].ToString();

                if (!string.IsNullOrEmpty(reader["StartDate"].ToString()))
                {
                    this.startDate = Convert.ToDateTime(reader["StartDate"]);
                }


                if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                {
                    this.endDate = Convert.ToDateTime(reader["EndDate"]);
                }
                if (!string.IsNullOrEmpty(reader["IsHienThiTacGia"].ToString()))
                {
                    this.IsHienThiTacGia = Convert.ToBoolean(reader["IsHienThiTacGia"]);
                }
                this.commentCount = Convert.ToInt32(reader["CommentCount"]);
                this.hitCount = Convert.ToInt32(reader["HitCount"]);
                this.articleGuid = new Guid(reader["ArticleGuid"].ToString());
                this.moduleGuid = new Guid(reader["ModuleGuid"].ToString());
                this.location = reader["Location"].ToString();
                this.userGuid = new Guid(reader["UserGuid"].ToString());
                this.createdByUser = reader["CreatedByUser"].ToString();
                this.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                if (!string.IsNullOrEmpty(reader["LastModUserGuid"].ToString()))
                {
                    this.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                }

                if (!string.IsNullOrEmpty(reader["LastModUtc"].ToString()))
                {
                    this.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                }
                this.itemUrl = reader["ItemUrl"].ToString();
                this.metaTitle = reader["MetaTitle"].ToString();
                //this.metaKeywords = reader["MetaKeywords"].ToString();
                this.metaDescription = reader["MetaDescription"].ToString();
                if (!string.IsNullOrEmpty(reader["IsApproved"].ToString()))
                {
                    this.isApproved = Convert.ToBoolean(reader["IsApproved"]);
                }
                if (this.isApproved.HasValue && this.isApproved.Value)
                {
                    this.approvedGuid = new Guid(reader["ApprovedGuid"].ToString());
                    this.approvedDate = Convert.ToDateTime(reader["ApprovedDate"]);
                }
                this.allowComment = Convert.ToBoolean(reader["AllowComment"]);

                if (!string.IsNullOrEmpty(reader["IsHot"].ToString()))
                {
                    this.isHot = Convert.ToBoolean(reader["IsHot"]);
                }

                if (!string.IsNullOrEmpty(reader["IsHome"].ToString()))
                {
                    this.isHome = Convert.ToBoolean(reader["IsHome"]);
                }

                if (!string.IsNullOrEmpty(reader["IsCongThanhVien"].ToString()))
                {
                    this.isCongThanhVien = Convert.ToBoolean(reader["IsCongThanhVien"]);
                }


                if (!string.IsNullOrEmpty(reader["Tag"].ToString()))
                {
                    this.tag = reader["Tag"].ToString();
                }

                if (!string.IsNullOrEmpty(reader["IsPublished"].ToString()))
                {
                    this.isPublished = Convert.ToBoolean(reader["IsPublished"]);
                }
                if (this.isPublished.HasValue && this.isPublished.Value)
                {
                    this.publishedGuid = new Guid(reader["PublishedGuid"].ToString());
                    this.publishedDate = Convert.ToDateTime(reader["PublishedDate"]);
                }
                if (!string.IsNullOrEmpty(reader["IncludeInFeed"].ToString()))
                {
                    this.includeInFeed = Convert.ToBoolean(reader["IncludeInFeed"]);
                }
                this.commentByBoss = reader["CommentByBoss"].ToString();
                this.audioUrl = reader["AudioUrl"].ToString();
                if (!string.IsNullOrEmpty(reader["PollGuid"].ToString()))
                {
                    this.pollGuid = new Guid(reader["PollGuid"].ToString());
                }
                if (!String.IsNullOrEmpty(reader["AllowWCAG"].ToString()))
                {
                    this.allowWCAG = Convert.ToBoolean(reader["AllowWCAG"]);
                }
                if (!String.IsNullOrEmpty(reader["CompiledMeta"].ToString()))
                {
                    this.compiledMeta = reader["CompiledMeta"].ToString();
                }
                if (!String.IsNullOrEmpty(reader["MetaCreator"].ToString()))
                {
                    this.metaCreator = reader["MetaCreator"].ToString();
                }
                if (!String.IsNullOrEmpty(reader["MetaIdentifier"].ToString()))
                {
                    this.metaIdentifier = reader["MetaIdentifier"].ToString();
                }
                if (!String.IsNullOrEmpty(reader["MetaPublisher"].ToString()))
                {
                    this.metaPublisher = reader["MetaPublisher"].ToString();
                }
                if (!String.IsNullOrEmpty(reader["MetaDate"].ToString()))
                {
                    this.metaDate = Convert.ToDateTime(reader["MetaDate"].ToString());
                }
                if (!string.IsNullOrEmpty(reader["ArticleReference"].ToString()))
                {
                    this.articleReference = reader["ArticleReference"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["FTS"].ToString()))
                {
                    this.fts = reader["FTS"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["IsDelete"].ToString()))
                {
                    this.isDelete = Convert.ToBoolean(reader["IsDelete"].ToString());
                }
                if (!string.IsNullOrEmpty(reader["TitleFTS"].ToString()))
                {
                    this.titleFTS = reader["TitleFTS"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["AuthorFTS"].ToString()))
                {
                    this.authorFTS = reader["AuthorFTS"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["SapoFTS"].ToString()))
                {
                    this.sapoFTS = reader["SapoFTS"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["CreateDateArticle"].ToString()))
                {
                    this.createDateArticle = Convert.ToDateTime(reader["CreateDateArticle"]);
                }
                //if (!String.IsNullOrEmpty(reader["IsHotNew"].ToString()))
                //{
                //    this.isHotNew = Convert.ToBoolean(reader["IsHotNew"]);
                //}
                if (!string.IsNullOrEmpty(reader["ViTriHienThiNgayDang"].ToString()))
                {
                    this.viTriHienThiNgayDang = reader["ViTriHienThiNgayDang"].ToString();
                }
            }

        }

        /// <summary>
        /// Persists a new instance of Article. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;
            articleGuid = Guid.NewGuid();
            createDateArticle = DateTime.Now;
            this.hitCount = 0;
            newID = DBArticles.Create(
                this.moduleID,
                this.siteId,
                this.categoryID,
                this.title,
                this.summary,
                this.description,
                this.imageUrl,
                this.startDate,
                this.endDate,
                this.commentCount,
                this.hitCount,
                this.articleGuid,
                this.moduleGuid,
                this.location,
                this.userGuid,
                this.createdByUser,
                this.createdDate,
                this.lastModUserGuid,
                this.lastModUtc,
                this.itemUrl,
                this.metaTitle,
                this.metaDescription,
                this.isApproved,
                this.approvedGuid,
                this.approvedDate,
                this.allowComment,
                this.isHot,
                this.isHome,
                this.tag,
                this.fts,
                this.isPublished,
                this.publishedGuid,
                this.publishedDate,
                this.includeInFeed,
                this.commentByBoss,
                this.audioUrl,
                this.pollGuid,
                this.allowWCAG,
                this.compiledMeta,
                this.metaCreator,
                this.metaIdentifier,
                this.metaPublisher,
                this.metaDate,
                this.isDelete,
                this.articleReference,
                this.titleFTS,
                this.authorFTS,
                this.sapoFTS,
                this.createDateArticle,
                //this.isHotNew
                this.viTriHienThiNgayDang,
                this.isHienThiTacGia
                );

            itemID = newID;

            bool result = (newID > 0);

            //IndexHelper.IndexItem(this);
            if (result)
            {
                ContentChangedEventArgs e = new ContentChangedEventArgs();
                OnContentChanged(e);
            }

            return result;

        }

        /// <summary>
        /// Updates this instance of Article. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            var result = DBArticles.Update(
                this.itemID,
                this.moduleID,
                this.siteId,
                this.categoryID,
                this.title,
                this.summary,
                this.description,
                this.imageUrl,
                this.startDate,
                this.endDate,
                this.commentCount,
                this.hitCount,
                this.moduleGuid,
                this.location,
                this.userGuid,
                this.createdByUser,
                this.createdDate,
                this.lastModUserGuid,
                this.lastModUtc,
                this.itemUrl,
                this.metaTitle,
                this.metaDescription,
                this.isApproved,
                this.approvedGuid,
                this.approvedDate,
                this.allowComment,
                this.isHot,
                this.isHome,
                this.tag,
                this.fts,
                this.isPublished,
                this.publishedGuid,
                this.publishedDate,
                this.includeInFeed,
                this.commentByBoss,
                this.audioUrl,
                this.pollGuid,
                this.allowWCAG,
                this.compiledMeta,
                this.metaCreator,
                this.metaIdentifier,
                this.metaPublisher,
                this.metaDate,
                this.isDelete,
                this.articleReference,
                this.titleFTS,
                this.authorFTS,
                this.sapoFTS,
                this.createDateArticle,
                //this.isHotNew
                this.viTriHienThiNgayDang,
                this.isHienThiTacGia
                );
            //IndexHelper.IndexItem(this);
            ContentChangedEventArgs e = new ContentChangedEventArgs();
            OnContentChanged(e);

            return result;
        }

        private bool UpdateCat()
        {
            return DBArticles.UpdateCat(
                this.itemID,
                this.moduleID,
                this.categoryID);
        }
        public bool UpdateHitCount()
        {
            return DBArticles.UpdateHitCount(this.itemID);
        }
        #endregion
        #region IIndexableContent

        public event ContentChangedEventHandler ContentChanged;

        protected void OnContentChanged(ContentChangedEventArgs e)
        {
            if (ContentChanged != null)
            {
                ContentChanged(this, e);
            }
        }
        #endregion
        #region Public Methods

        /// <summary>
        /// Saves this instance of Article. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        public bool Save()
        {
            QLLog qlLog = new QLLog()
            {
                DiaChiIP = Common.findMyIP().ToString(),
                Type = KieuLogConstant.LogQuanTriTin,
                DuongDanThaoTac = HttpContext.Current.Request.Url.PathAndQuery,
                CreatedBy = this.createdByUser,
                CreatedByUser = -1,
                CreatedDate = DateTime.Now
            };

            if (this.itemID > 0)
            {
                qlLog.NoiDung = "Cập nhập bài viết mã số: " + this.ItemID + ", tiêu đề: " + this.Title;
                qlLog.HanhDongThaoTac = KieuLogConstant.CapNhapDuLieu;
                qlLog.Save();
                return Update();
            }
            else
            {
                qlLog.NoiDung = "Thêm mới bài viết: " + this.Title;
                qlLog.HanhDongThaoTac = KieuLogConstant.ThemMoi;
                qlLog.Save();
                return Create();
            }
        }

        public bool SaveCat()
        {
            if (this.itemID > 0)
            {
                return UpdateCat();
            }
            return false;
        }

        public void CreateHistory(Guid siteGuid)
        {
            if (this.articleGuid == Guid.Empty) { return; }

            Article currentVersion = new Article(this.itemID);
            if (currentVersion.Description == this.Description) { return; }

            ContentHistory history = new ContentHistory();
            history.ContentGuid = currentVersion.articleGuid;
            history.Title = currentVersion.Title;
            history.ContentText = currentVersion.Description;
            history.SiteGuid = siteGuid;
            history.UserGuid = currentVersion.LastModUserGuid;
            history.CreatedUtc = currentVersion.LastModUtc;
            history.Save();

        }
        #endregion

        #region Static Methods

        /// <summary>
        /// Deletes an instance of Article. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBArticles.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of Article. 
        /// </summary>
        public static int GetCount(int siteId, int moduleId, int categoryID, int isApprove, int isPublish, string keyword, Guid userGuid, string createDateArticle)
        {
            return DBArticles.GetCount(siteId, moduleId, categoryID, isApprove, isPublish, keyword, userGuid, createDateArticle);
        }
        public static int GetCountByEvent(int EventID)
        {
            return DBArticles.GetCountByEvent(EventID);
        }
        /// <summary>
        /// Dùng cho thống kê bài viết
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static List<Article> LoadListStatistic(IDataReader reader)
        {
            List<Article> articleList = new List<Article>();
            try
            {
                while (reader.Read())
                {
                    Article article = new Article();
                    article.itemID = Convert.ToInt32(reader["ItemID"]);
                    article.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    if (!String.IsNullOrEmpty(reader["CategoryID"].ToString()))
                    {
                        article.categoryID = Convert.ToInt32(reader["CategoryID"]);
                    }
                    article.title = reader["Title"].ToString();
                    article.summary = reader["Summary"].ToString();
                    article.description = reader["Description"].ToString();
                    article.imageUrl = reader["ImageUrl"].ToString();
                    article.startDate = Convert.ToDateTime(reader["StartDate"]);
                    if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                    {
                        article.endDate = Convert.ToDateTime(reader["EndDate"]);
                    }
                    article.commentCount = Convert.ToInt32(reader["CommentCount"]);
                    article.hitCount = Convert.ToInt32(reader["HitCount"]);
                    article.articleGuid = new Guid(reader["ArticleGuid"].ToString());
                    article.moduleGuid = new Guid(reader["ModuleGuid"].ToString());
                    article.location = reader["Location"].ToString();
                    article.userGuid = new Guid(reader["UserGuid"].ToString());
                    article.createdByUser = reader["CreatedByUser"].ToString();
                    article.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    if (!string.IsNullOrEmpty(reader["LastModUserGuid"].ToString()))
                    {
                        article.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                    }

                    if (!string.IsNullOrEmpty(reader["LastModUtc"].ToString()))
                    {
                        article.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                    }
                    article.itemUrl = reader["ItemUrl"].ToString();
                    article.metaTitle = reader["MetaTitle"].ToString();
                    article.metaKeywords = reader["MetaKeywords"].ToString();
                    article.metaDescription = reader["MetaDescription"].ToString();
                    if (!string.IsNullOrEmpty(reader["IsApproved"].ToString()))
                    {
                        article.isApproved = Convert.ToBoolean(reader["IsApproved"]);
                    }
                    if (article.isApproved.HasValue && article.isApproved.Value)
                    {
                        article.approvedGuid = new Guid(reader["ApprovedGuid"].ToString());
                        article.approvedDate = Convert.ToDateTime(reader["ApprovedDate"]);
                    }
                    article.allowComment = Convert.ToBoolean(reader["AllowComment"]);
                    article.isHot = Convert.ToBoolean(reader["IsHot"]);
                    article.isHome = Convert.ToBoolean(reader["IsHome"]);
                    article.tag = reader["Tag"].ToString();
                    if (!string.IsNullOrEmpty(reader["IsPublished"].ToString()))
                    {
                        article.isPublished = Convert.ToBoolean(reader["IsPublished"]);
                    }
                    if (article.isPublished.HasValue && article.isPublished.Value)
                    {
                        article.publishedGuid = new Guid(reader["PublishedGuid"].ToString());
                        article.publishedDate = Convert.ToDateTime(reader["PublishedDate"]);
                    }
                    if (!string.IsNullOrEmpty(reader["IncludeInFeed"].ToString()))
                    {
                        article.includeInFeed = Convert.ToBoolean(reader["IncludeInFeed"]);
                    }
                    article.commentByBoss = reader["CommentByBoss"].ToString();
                    article.categoryName = reader["CategoryName"].ToString();
                    if (!string.IsNullOrEmpty(reader["ArticleReference"].ToString()))
                    {
                        article.articleReference = reader["ArticleReference"].ToString();
                    }
                    articleList.Add(article);

                }
            }
            finally
            {
                reader.Close();
            }

            return articleList;
        }

        private static List<Article> LoadListReaderArticle(IDataReader reader)
        {
            List<Article> articleList = new List<Article>();
            try
            {
                while (reader.Read())
                {
                    Article article = new Article();
                    article.itemID = Convert.ToInt32(reader["ItemID"]);
                    article.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    if (!String.IsNullOrEmpty(reader["CategoryID"].ToString()))
                    {
                        article.categoryID = Convert.ToInt32(reader["CategoryID"]);
                    }
                    article.title = reader["Title"].ToString();
                    article.summary = reader["Summary"].ToString();
                    article.imageUrl = reader["ImageUrl"].ToString();
                    article.startDate = Convert.ToDateTime(reader["StartDate"]);
                    if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                    {
                        article.endDate = Convert.ToDateTime(reader["EndDate"]);
                    }
                    if (!string.IsNullOrEmpty(reader["IsCongThanhVien"].ToString()))
                    {
                        article.isCongThanhVien = Convert.ToBoolean(reader["IsCongThanhVien"]);
                    }
                    article.hitCount = Convert.ToInt32(reader["HitCount"]);
                    article.articleGuid = new Guid(reader["ArticleGuid"].ToString());
                    article.moduleGuid = new Guid(reader["ModuleGuid"].ToString());
                    article.location = reader["Location"].ToString();
                    article.userGuid = new Guid(reader["UserGuid"].ToString());
                    article.createdByUser = reader["CreatedByUser"].ToString();
                    article.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    if (!string.IsNullOrEmpty(reader["LastModUserGuid"].ToString()))
                    {
                        article.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                    }

                    if (!string.IsNullOrEmpty(reader["LastModUtc"].ToString()))
                    {
                        article.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                    }
                    article.itemUrl = reader["ItemUrl"].ToString();

                    article.allowComment = Convert.ToBoolean(reader["AllowComment"]);
                    article.isHot = Convert.ToBoolean(reader["IsHot"]);
                    article.isHome = Convert.ToBoolean(reader["IsHome"]);
                    article.tag = reader["Tag"].ToString();

                    article.categoryName = reader["CategoryName"].ToString();
                    article.categoryUrl = reader["CategoryUrl"].ToString();
                    articleList.Add(article);

                }
            }
            finally
            {
                reader.Close();
            }

            return articleList;
        }

        private static List<Article> LoadAllListFormReader(IDataReader reader)
        {
            List<Article> articleList = new List<Article>();
            try
            {
                while (reader.Read())
                {
                    Article article = new Article();
                    article.itemID = Convert.ToInt32(reader["ItemID"]);
                    article.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    if (!string.IsNullOrEmpty(reader["SiteID"].ToString()))
                    {
                        article.siteId = Convert.ToInt32(reader["SiteID"]);
                    }
                    if (!string.IsNullOrEmpty(reader["IsCongThanhVien"].ToString()))
                    {
                        article.isCongThanhVien = Convert.ToBoolean(reader["IsCongThanhVien"]);
                    }
                    if (!String.IsNullOrEmpty(reader["CategoryID"].ToString()))
                    {
                        article.categoryID = Convert.ToInt32(reader["CategoryID"]);
                    }
                    article.title = reader["Title"].ToString();
                    article.summary = reader["Summary"].ToString();
                    article.description = reader["Description"].ToString();
                    article.imageUrl = reader["ImageUrl"].ToString();
                    article.startDate = Convert.ToDateTime(reader["StartDate"]);
                    if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                    {
                        article.endDate = Convert.ToDateTime(reader["EndDate"]);
                    }
                    article.commentCount = Convert.ToInt32(reader["CommentCount"]);
                    article.hitCount = Convert.ToInt32(reader["HitCount"]);
                    article.articleGuid = new Guid(reader["ArticleGuid"].ToString());
                    article.moduleGuid = new Guid(reader["ModuleGuid"].ToString());
                    article.location = reader["Location"].ToString();
                    article.userGuid = new Guid(reader["UserGuid"].ToString());
                    article.createdByUser = reader["CreatedByUser"].ToString();
                    article.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    if (!string.IsNullOrEmpty(reader["LastModUserGuid"].ToString()))
                    {
                        article.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                    }

                    if (!string.IsNullOrEmpty(reader["LastModUtc"].ToString()))
                    {
                        article.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                    }
                    article.itemUrl = reader["ItemUrl"].ToString();
                    article.metaTitle = reader["MetaTitle"].ToString();
                    //article.metaKeywords = reader["MetaKeywords"].ToString();
                    article.metaDescription = reader["MetaDescription"].ToString();
                    if (!string.IsNullOrEmpty(reader["IsApproved"].ToString()))
                    {
                        article.isApproved = Convert.ToBoolean(reader["IsApproved"]);
                    }
                    if (article.isApproved.HasValue && article.isApproved.Value)
                    {
                        article.approvedGuid = new Guid(reader["ApprovedGuid"].ToString());
                        article.approvedDate = Convert.ToDateTime(reader["ApprovedDate"]);
                    }
                    article.allowComment = Convert.ToBoolean(reader["AllowComment"]);
                    article.isHot = Convert.ToBoolean(reader["IsHot"]);
                    article.isHome = Convert.ToBoolean(reader["IsHome"]);
                    article.tag = reader["Tag"].ToString();
                    if (!string.IsNullOrEmpty(reader["IsPublished"].ToString()))
                    {
                        article.isPublished = Convert.ToBoolean(reader["IsPublished"]);
                    }
                    if (article.isPublished.HasValue && article.isPublished.Value)
                    {
                        article.publishedGuid = new Guid(reader["PublishedGuid"].ToString());
                        article.publishedDate = Convert.ToDateTime(reader["PublishedDate"]);
                    }
                    if (!string.IsNullOrEmpty(reader["IncludeInFeed"].ToString()))
                    {
                        article.includeInFeed = Convert.ToBoolean(reader["IncludeInFeed"]);
                    }
                    article.commentByBoss = reader["CommentByBoss"].ToString();
                    article.audioUrl = reader["AudioUrl"].ToString();
                    if (!string.IsNullOrEmpty(reader["PollGuid"].ToString()))
                    {
                        article.pollGuid = new Guid(reader["PollGuid"].ToString());
                    }
                    if (!String.IsNullOrEmpty(reader["AllowWCAG"].ToString()))
                    {
                        article.allowWCAG = Convert.ToBoolean(reader["AllowWCAG"]);
                    }
                    if (!String.IsNullOrEmpty(reader["CompiledMeta"].ToString()))
                    {
                        article.compiledMeta = reader["CompiledMeta"].ToString();
                    }
                    if (!String.IsNullOrEmpty(reader["MetaCreator"].ToString()))
                    {
                        article.metaCreator = reader["MetaCreator"].ToString();
                    }
                    if (!String.IsNullOrEmpty(reader["MetaIdentifier"].ToString()))
                    {
                        article.metaIdentifier = reader["MetaIdentifier"].ToString();
                    }
                    if (!String.IsNullOrEmpty(reader["MetaPublisher"].ToString()))
                    {
                        article.metaPublisher = reader["MetaPublisher"].ToString();
                    }
                    if (!String.IsNullOrEmpty(reader["MetaDate"].ToString()))
                    {
                        article.metaDate = Convert.ToDateTime(reader["MetaDate"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["ArticleReference"].ToString()))
                    {
                        article.articleReference = reader["ArticleReference"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["FTS"].ToString()))
                    {
                        article.fts = reader["FTS"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["IsDelete"].ToString()))
                    {
                        article.isDelete = Convert.ToBoolean(reader["IsDelete"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["IsCongThanhVien"].ToString()))
                    {
                        article.isCongThanhVien = Convert.ToBoolean(reader["IsCongThanhVien"]);
                    }
                    //if (!String.IsNullOrEmpty(reader["IsHotNew"].ToString()))
                    //{
                    //    article.isHotNew = Convert.ToBoolean(reader["IsHotNew"]);
                    //}
                    articleList.Add(article);
                }
            }
            finally
            {
                reader.Close();
            }
            return articleList;

        }

        private static List<Article> LoadListFromReaderForSite(IDataReader reader, bool getpage = false)
        {
            List<Article> articleList = new List<Article>();
            try
            {
                while (reader.Read())
                {
                    Article article = new Article();
                    article.itemID = Convert.ToInt32(reader["ItemID"]);
                    article.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    if (!String.IsNullOrEmpty(reader["CategoryID"].ToString()))
                    {
                        article.categoryID = Convert.ToInt32(reader["CategoryID"]);
                    }
                    article.title = reader["Title"].ToString();
                    article.summary = reader["Summary"].ToString();
                    article.description = reader["Description"].ToString();
                    article.imageUrl = reader["ImageUrl"].ToString();
                    article.startDate = Convert.ToDateTime(reader["StartDate"]);
                    if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                    {
                        article.endDate = Convert.ToDateTime(reader["EndDate"]);
                    }
                    if (!string.IsNullOrEmpty(reader["SiteName"].ToString()))
                    {
                        article.siteName = reader["SiteName"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["IsCongThanhVien"].ToString()))
                    {
                        article.isCongThanhVien = Convert.ToBoolean(reader["IsCongThanhVien"]);
                    }
                    article.commentCount = Convert.ToInt32(reader["CommentCount"]);
                    article.hitCount = Convert.ToInt32(reader["HitCount"]);
                    article.articleGuid = new Guid(reader["ArticleGuid"].ToString());
                    article.moduleGuid = new Guid(reader["ModuleGuid"].ToString());
                    article.location = reader["Location"].ToString();
                    article.userGuid = new Guid(reader["UserGuid"].ToString());
                    article.createdByUser = reader["CreatedByUser"].ToString();
                    article.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    if (!string.IsNullOrEmpty(reader["LastModUserGuid"].ToString()))
                    {
                        article.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                    }

                    if (!string.IsNullOrEmpty(reader["LastModUtc"].ToString()))
                    {
                        article.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                    }
                    article.itemUrl = reader["ItemUrl"].ToString();
                    article.metaTitle = reader["MetaTitle"].ToString();
                    article.metaKeywords = reader["MetaKeywords"].ToString();
                    article.metaDescription = reader["MetaDescription"].ToString();
                    if (!string.IsNullOrEmpty(reader["IsApproved"].ToString()))
                    {
                        article.isApproved = Convert.ToBoolean(reader["IsApproved"]);
                    }
                    if (article.isApproved.HasValue && article.isApproved.Value)
                    {
                        article.approvedGuid = new Guid(reader["ApprovedGuid"].ToString());
                        article.approvedDate = Convert.ToDateTime(reader["ApprovedDate"]);
                    }
                    article.allowComment = Convert.ToBoolean(reader["AllowComment"]);
                    article.isHot = Convert.ToBoolean(reader["IsHot"]);
                    article.isHome = Convert.ToBoolean(reader["IsHome"]);
                    article.tag = reader["Tag"].ToString();
                    if (!string.IsNullOrEmpty(reader["IsPublished"].ToString()))
                    {
                        article.isPublished = Convert.ToBoolean(reader["IsPublished"]);
                    }
                    if (article.isPublished.HasValue && article.isPublished.Value)
                    {
                        article.publishedGuid = new Guid(reader["PublishedGuid"].ToString());
                        article.publishedDate = Convert.ToDateTime(reader["PublishedDate"]);
                    }
                    if (!string.IsNullOrEmpty(reader["IncludeInFeed"].ToString()))
                    {
                        article.includeInFeed = Convert.ToBoolean(reader["IncludeInFeed"]);
                    }
                    article.commentByBoss = reader["CommentByBoss"].ToString();
                    if (getpage)
                    {
                        article.categoryName = reader["CategoryName"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["ArticleReference"].ToString()))
                    {
                        article.articleReference = reader["ArticleReference"].ToString();
                    }
                    articleList.Add(article);

                }
            }
            finally
            {
                reader.Close();
            }

            return articleList;

        }
        private static List<Article> LoadListFromReaderAll(IDataReader reader, bool getpage = false, bool getSiteName = false, bool getHostName = false)
        {
            List<Article> articleList = new List<Article>();
            try
            {
                while (reader.Read())
                {
                    var article = new Article();
                    article.itemID = Convert.ToInt32(reader["ItemID"]);
                    article.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    if (!string.IsNullOrEmpty(reader["SiteID"].ToString()))
                    {
                        article.siteId = Convert.ToInt32(reader["SiteID"]);
                    }
                    if (!string.IsNullOrEmpty(reader["IsCongThanhVien"].ToString()))
                    {
                        article.isCongThanhVien = Convert.ToBoolean(reader["IsCongThanhVien"]);
                    }
                    if (!String.IsNullOrEmpty(reader["CategoryID"].ToString()))
                    {
                        article.categoryID = Convert.ToInt32(reader["CategoryID"]);
                    }
                    article.title = reader["Title"].ToString();
                    article.summary = reader["Summary"].ToString();
                    article.description = reader["Description"].ToString();
                    article.imageUrl = reader["ImageUrl"].ToString();
                    article.startDate = Convert.ToDateTime(reader["StartDate"]);
                    if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                    {
                        article.endDate = Convert.ToDateTime(reader["EndDate"]);
                    }
                    article.commentCount = Convert.ToInt32(reader["CommentCount"]);
                    article.hitCount = Convert.ToInt32(reader["HitCount"]);
                    if (!string.IsNullOrEmpty(reader["ArticleGuid"].ToString()))
                    {
                        article.articleGuid = new Guid(reader["ArticleGuid"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["ModuleGuid"].ToString()))
                    {
                        article.moduleGuid = new Guid(reader["ModuleGuid"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["UserGuid"].ToString()))
                    {
                        article.userGuid = new Guid(reader["UserGuid"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["LastModUserGuid"].ToString()))
                    {
                        article.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                    }

                    if (!string.IsNullOrEmpty(reader["LastModUtc"].ToString()))
                    {
                        article.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);

                    }
                    article.location = reader["Location"].ToString();
                    article.createdByUser = reader["CreatedByUser"].ToString();
                    article.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    article.itemUrl = reader["ItemUrl"].ToString();
                    article.metaTitle = reader["MetaTitle"].ToString();
                    //article.metaKeywords = reader["MetaKeywords"].ToString();
                    article.metaDescription = reader["MetaDescription"].ToString();
                    if (!string.IsNullOrEmpty(reader["IsApproved"].ToString()))
                    {
                        article.isApproved = Convert.ToBoolean(reader["IsApproved"]);
                    }
                    if (article.isApproved.HasValue && article.isApproved.Value)
                    {
                        article.approvedGuid = new Guid(reader["ApprovedGuid"].ToString());
                        article.approvedDate = Convert.ToDateTime(reader["ApprovedDate"]);
                    }
                    if (!string.IsNullOrEmpty(reader["AllowComment"].ToString()))
                    {
                        article.allowComment = Convert.ToBoolean(reader["AllowComment"]);
                    }
                    if (!string.IsNullOrEmpty(reader["IsHot"].ToString()))
                    {
                        article.isHot = Convert.ToBoolean(reader["IsHot"]);
                    }
                    if (!string.IsNullOrEmpty(reader["IsHome"].ToString()))
                    {
                        article.isHome = Convert.ToBoolean(reader["IsHome"]);
                    }

                    article.tag = reader["Tag"].ToString();
                    if (!string.IsNullOrEmpty(reader["IsPublished"].ToString()))
                    {
                        article.isPublished = Convert.ToBoolean(reader["IsPublished"]);
                    }
                    if (article.isPublished.HasValue && article.isPublished.Value)
                    {
                        article.publishedGuid = new Guid(reader["PublishedGuid"].ToString());
                        article.publishedDate = Convert.ToDateTime(reader["PublishedDate"]);
                    }
                    if (!string.IsNullOrEmpty(reader["IncludeInFeed"].ToString()))
                    {
                        article.includeInFeed = Convert.ToBoolean(reader["IncludeInFeed"]);
                    }
                    article.commentByBoss = reader["CommentByBoss"].ToString();
                    article.audioUrl = reader["AudioUrl"].ToString();
                    if (!string.IsNullOrEmpty(reader["PollGuid"].ToString()))
                    {
                        article.pollGuid = new Guid(reader["PollGuid"].ToString());
                    }
                    if (!String.IsNullOrEmpty(reader["AllowWCAG"].ToString()))
                    {
                        article.allowWCAG = Convert.ToBoolean(reader["AllowWCAG"]);
                    }
                    if (!String.IsNullOrEmpty(reader["CompiledMeta"].ToString()))
                    {
                        article.compiledMeta = reader["CompiledMeta"].ToString();
                    }
                    if (!String.IsNullOrEmpty(reader["MetaCreator"].ToString()))
                    {
                        article.metaCreator = reader["MetaCreator"].ToString();
                    }
                    if (!String.IsNullOrEmpty(reader["MetaIdentifier"].ToString()))
                    {
                        article.metaIdentifier = reader["MetaIdentifier"].ToString();
                    }
                    if (!String.IsNullOrEmpty(reader["MetaPublisher"].ToString()))
                    {
                        article.metaPublisher = reader["MetaPublisher"].ToString();
                    }
                    if (!String.IsNullOrEmpty(reader["MetaDate"].ToString()))
                    {
                        article.metaDate = Convert.ToDateTime(reader["MetaDate"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["ArticleReference"].ToString()))
                    {
                        article.articleReference = reader["ArticleReference"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["FTS"].ToString()))
                    {
                        article.fts = reader["FTS"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["IsDelete"].ToString()))
                    {
                        article.isDelete = Convert.ToBoolean(reader["IsDelete"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["TitleFTS"].ToString()))
                    {
                        article.titleFTS = reader["TitleFTS"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["AuthorFTS"].ToString()))
                    {
                        article.authorFTS = reader["AuthorFTS"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["SapoFTS"].ToString()))
                    {
                        article.sapoFTS = reader["SapoFTS"].ToString();
                    }

                    articleList.Add(article);
                }
            }
            finally
            {
                reader.Close();
            }
            return articleList;
        }

        private static List<Article> LoadListFromReader(IDataReader reader, bool getpage = false, bool getSiteName = false, bool getHostName = false, bool getClientName = false)
        {
            List<Article> articleList = new List<Article>();
            try
            {
                while (reader.Read())
                {
                    Article article = new Article();
                    article.itemID = Convert.ToInt32(reader["ItemID"]);
                    if (!string.IsNullOrEmpty(reader["ModuleID"].ToString()))
                    {
                        article.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    }
                    if (!string.IsNullOrEmpty(reader["CategoryID"].ToString()))
                    {
                        article.categoryID = Convert.ToInt32(reader["CategoryID"]);
                    }

                    article.title = reader["Title"].ToString();
                    article.summary = reader["Summary"].ToString();
                    article.description = reader["Description"].ToString();
                    article.imageUrl = reader["ImageUrl"].ToString();


                    if (!string.IsNullOrEmpty(reader["StartDate"].ToString()))
                    {
                        article.startDate = Convert.ToDateTime(reader["StartDate"]);
                    }

                    if (getClientName)
                    {
                        if (!string.IsNullOrEmpty(reader["ClientName"].ToString()))
                        {
                            article.clientName = reader["ClientName"].ToString();
                        }
                    }

                    if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                    {
                        article.endDate = Convert.ToDateTime(reader["EndDate"]);
                    }
                    if (!string.IsNullOrEmpty(reader["IsCongThanhVien"].ToString()))
                    {
                        article.isCongThanhVien = Convert.ToBoolean(reader["IsCongThanhVien"]);
                    }
                    article.commentCount = Convert.ToInt32(reader["CommentCount"]);
                    article.hitCount = Convert.ToInt32(reader["HitCount"]);
                    article.articleGuid = new Guid(reader["ArticleGuid"].ToString());
                    article.moduleGuid = new Guid(reader["ModuleGuid"].ToString());
                    article.location = reader["Location"].ToString();
                    article.userGuid = new Guid(reader["UserGuid"].ToString());
                    article.createdByUser = reader["CreatedByUser"].ToString();
                    article.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    if (!string.IsNullOrEmpty(reader["LastModUserGuid"].ToString()))
                    {
                        article.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                    }

                    if (!string.IsNullOrEmpty(reader["LastModUtc"].ToString()))
                    {
                        article.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                    }
                    article.itemUrl = reader["ItemUrl"].ToString();
                    article.metaTitle = reader["MetaTitle"].ToString();
                    article.metaKeywords = reader["MetaKeywords"].ToString();
                    article.metaDescription = reader["MetaDescription"].ToString();
                    if (!string.IsNullOrEmpty(reader["IsApproved"].ToString()))
                    {
                        article.isApproved = Convert.ToBoolean(reader["IsApproved"]);
                    }
                    if (article.isApproved.HasValue && article.isApproved.Value)
                    {
                        article.approvedGuid = new Guid(reader["ApprovedGuid"].ToString());
                        article.approvedDate = Convert.ToDateTime(reader["ApprovedDate"]);
                    }
                    article.allowComment = Convert.ToBoolean(reader["AllowComment"]);

                    if (!string.IsNullOrEmpty(reader["IsHot"].ToString()))
                    {
                        article.isHot = Convert.ToBoolean(reader["IsHot"]);
                    }

                    if (!string.IsNullOrEmpty(reader["IsHome"].ToString()))
                    {
                        article.isHome = Convert.ToBoolean(reader["IsHome"]);
                    }

                    if (!string.IsNullOrEmpty(reader["Tag"].ToString()))
                    {
                        article.tag = reader["Tag"].ToString();
                    }

                    if (!string.IsNullOrEmpty(reader["IsPublished"].ToString()))
                    {
                        article.isPublished = Convert.ToBoolean(reader["IsPublished"]);
                    }
                    if (article.isPublished.HasValue && article.isPublished.Value)
                    {
                        article.publishedGuid = new Guid(reader["PublishedGuid"].ToString());
                        article.publishedDate = Convert.ToDateTime(reader["PublishedDate"]);
                    }
                    if (!string.IsNullOrEmpty(reader["IncludeInFeed"].ToString()))
                    {
                        article.includeInFeed = Convert.ToBoolean(reader["IncludeInFeed"]);
                    }
                    article.commentByBoss = reader["CommentByBoss"].ToString();
                    if (getpage)
                    {
                        article.categoryName = reader["CategoryName"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["ArticleReference"].ToString()))
                    {
                        article.articleReference = reader["ArticleReference"].ToString();
                    }
                    if (getSiteName)
                    {
                        article.siteName = reader["SiteName"].ToString();
                    }
                    if (getHostName)
                    {
                        article.hostName = reader["HostName"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["SiteID"].ToString()))
                    {
                        article.siteId = Convert.ToInt32(reader["SiteID"]);
                    }


                    if (article.startDate.AddDays(5) >= DateTime.Now.ToString("dd/MM/yyyy").ToDateTimeNotNull())
                    {
                        article.IsNew = true;
                    }

                    articleList.Add(article);

                }
            }
            finally
            {
                reader.Close();
            }

            return articleList;
        }


        private static List<Article> LoadListFromReaderForPhongBan(IDataReader reader)
        {
            List<Article> articleList = new List<Article>();
            try
            {
                while (reader.Read())
                {
                    Article article = new Article();
                    article.itemID = Convert.ToInt32(reader["ItemID"]);
                    article.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    article.title = reader["Title"].ToString();
                    if (!string.IsNullOrEmpty(reader["CreateDate"].ToString()))
                    {
                        article.createdDate = Convert.ToDateTime(reader["CreateDate"]);
                    }
                    article.itemUrl = reader["UrlItem"].ToString();
                    if (!string.IsNullOrEmpty(reader["IsPublished"].ToString()))
                    {
                        article.isPublished = Convert.ToBoolean(reader["IsPublished"]);
                    }
                    if (!string.IsNullOrEmpty(reader["SiteID"].ToString()))
                    {
                        article.siteId = Convert.ToInt32(reader["SiteID"]);
                    }
                    articleList.Add(article);

                }
            }
            finally
            {
                reader.Close();
            }

            return articleList;
        }


        public static DataTable GetArticlesByPage(int siteId, int pageId)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ItemID", typeof(int));
            dataTable.Columns.Add("ModuleID", typeof(int));
            dataTable.Columns.Add("CommentCount", typeof(int));
            dataTable.Columns.Add("ModuleTitle", typeof(string));
            dataTable.Columns.Add("Title", typeof(string));
            dataTable.Columns.Add("ItemUrl", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));
            dataTable.Columns.Add("StartDate", typeof(DateTime));
            dataTable.Columns.Add("MetaDescription", typeof(string));
            dataTable.Columns.Add("MetaKeywords", typeof(string));
            dataTable.Columns.Add("MetaTitles", typeof(string));
            dataTable.Columns.Add("ViewRoles", typeof(string));

            using (IDataReader reader = DBArticles.GetArticlesByPage(siteId, pageId))
            {
                while (reader.Read())
                {
                    DataRow row = dataTable.NewRow();

                    row["ItemID"] = reader["ItemID"];
                    row["ModuleID"] = reader["ModuleID"];
                    row["CommentCount"] = reader["CommentCount"];
                    row["ModuleTitle"] = reader["ModuleTitle"];
                    row["Title"] = reader["Title"];
                    row["ItemUrl"] = reader["ItemUrl"];
                    row["Description"] = reader["Description"];
                    row["StartDate"] = Convert.ToDateTime(reader["StartDate"]);
                    row["MetaDescription"] = reader["MetaDescription"];
                    row["MetaKeywords"] = reader["MetaKeywords"];
                    row["MetaTitles"] = reader["MetaTitle"];
                    row["ViewRoles"] = reader["ViewRoles"];

                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
        }

        /// <summary>
        /// Gets an IList with all instances of Article.
        /// </summary>
        public static List<Article> GetAll()
        {
            IDataReader reader = DBArticles.GetAll();
            return LoadListFromReaderAll(reader);
        }

        public static List<Article> GetAllPhongBan()
        {
            IDataReader reader = DBArticles.GetListPhongBan();
            return LoadListFromReaderForPhongBan(reader);
        }

        public static List<Article> GetByCategorySetting(int siteID, int moduleId, string categories)
        {
            IDataReader reader = DBArticles.GetByCategorySetting(siteID, moduleId, categories);
            return LoadListFromReader(reader);
        }
        public static List<Article> GetAllByEvent(int siteID)
        {
            IDataReader reader = DBArticles.GetArticleByEvent(siteID);
            return PopulateFromReaderAndEventID(reader);
        }
        /// <summary>
        /// Gets the posts for this article instance.
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// Gets the articles.
        /// </summary>
        /// <param name="moduleId">The module ID.</param>
        /// <param name="beginDate">The end date.</param>
        /// <returns></returns>
        public static IDataReader GetArticlesForRSS(int moduleId, DateTime beginDate)
        {
            return DBArticles.GetArticlesForRSS(moduleId, beginDate, DateTime.UtcNow);
        }


        public static Dictionary<string, string> GetSearch(int categoryId, int step, int published)
        {
            IDataReader reader = DBArticles.GetSearch(categoryId, step, published);
            Dictionary<string, string> result = new Dictionary<string, string>();
            while (reader.Read())
            {
                result.Add(reader["ArticleGuid"].ToString(), reader["Title"].ToString());
            }
            return result;
        }



        /// <summary>
        /// Gets an IList with page of instances of Article.
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="moduleId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="categoryID"></param>
        /// <param name="isApprove"></param>
        /// <param name="isPublish"></param>
        /// <param name="keyword"></param>
        /// <param name="totalPages"></param>
        /// <returns></returns>
        public static List<Article> GetPage(int siteId, int moduleId, int pageNumber, int pageSize, int categoryID, int isApprove, int isPublish, string keyword, Guid userGuid, string createDateArticle, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBArticles.GetPage(siteId, moduleId, pageNumber, pageSize, categoryID, isApprove, isPublish, keyword, userGuid, createDateArticle, out totalPages);
            return LoadListFromReader(reader, true);
        }
        public static List<Article> GetAllOrtherPage(int siteId, int itemId, int categoryID, DateTime? startDate, string keyword, int pageNumber, int pageSize, out int totalPages, out int totalCount)
        {
            totalPages = 1;
            IDataReader reader = DBArticles.GetAllOrtherPage(siteId, itemId, categoryID, startDate, keyword, pageNumber, pageSize, out totalPages, out totalCount);
            return LoadListFromReader(reader, true);
        }

        public static List<Article> GetAllPage(int siteId, int pageNumber, int pageSize, int categoryID, Guid authorID, int isApprove, int isPublish, bool? isHot, bool? isHome, DateTime? startDate, DateTime? endDate, string keyword, out int totalPages, out int totalRows)
        {
            totalPages = 1;
            IDataReader reader = DBArticles.GetAllPage(siteId, pageNumber, pageSize, categoryID, authorID, isApprove, isPublish, isHot, isHome, startDate, endDate, keyword, out totalPages, out totalRows);
            return LoadListFromReader(reader, true, true, true);
        }

        public static List<Article> GetAllArticlePage(int siteId, int pageNumber, int pageSize, int categoryID, int isApprove, int isPublish, bool? isHot, bool? isHome, DateTime? startDate, DateTime? endDate, string articleCategory, string keyword, Guid userGuid, string createDateArticle, out int totalPages, out int totalRow)
        {
            totalPages = 1;
            IDataReader reader = DBArticles.GetAllArticlePage(siteId, pageNumber, pageSize, categoryID, isApprove, isPublish, isHot, isHome, startDate, endDate, articleCategory, keyword, userGuid, createDateArticle, out totalPages, out totalRow);
            return LoadListFromReader(reader, true);
        }
        public static List<Article> GetAllArticleCTVPage(int siteId, int pageNumber, int pageSize, int categoryID, int isApprove, int isPublish, bool? isHot, bool? isHome, DateTime? startDate, DateTime? endDate, string articleCategory, string keyword, Guid userGuid, string createDateArticle, out int totalPages, out int totalRow)
        {
            totalPages = 1;
            IDataReader reader = DBArticles.GetAllArticleCTVPage(siteId, pageNumber, pageSize, categoryID, isApprove, isPublish, isHot, isHome, startDate, endDate, articleCategory, keyword, userGuid, createDateArticle, out totalPages, out totalRow);
            return LoadListFromReader(reader, true, false, false, true);
        }
        public static List<Article> GetPageForEndUser(int siteId, int moduleId, int pageNumber, int pageSize, string categories, string keyword, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBArticles.GetPageForEndUser(siteId, moduleId, pageNumber, pageSize, categories, keyword, out totalPages);
            return LoadListFromReader(reader, true);
        }


        public static List<Article> GetPageForEndUserCongThanhVien(int siteId, int moduleId, int pageNumber, int pageSize, string categories, string keyword, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBArticles.GetPageForEndUserCongThanhVien(siteId, moduleId, pageNumber, pageSize, categories, keyword, out totalPages);
            return LoadListFromReader(reader, true, false, false, true);
        }



        public static List<Article> GetPageForCategory(int siteId, int categoryId, int pageNumber, int pageSize, string keyword, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBArticles.GetPageForCategory(siteId, categoryId, pageNumber, pageSize, keyword, out totalPages);
            return LoadListFromReader(reader, true);
        }

        public static List<Article> GetPageByCategory(int pageNumber, int pageSize, string categories, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBArticles.GetPageByCategory(pageNumber, pageSize, categories, out totalPages);
            return LoadListFromReader(reader, true, true);
        }

        public static List<Article> GetTopCategory(int top, string categories)
        {
            IDataReader reader = DBArticles.GetTopByCategory(top, categories);
            return LoadListFromReader(reader, true);
        }

        public static List<Article> GetPageByEvent(int pageNumber, int pageSize, int eventID, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBArticles.GetPageByEvent(pageNumber, pageSize, eventID, out totalPages);
            return LoadListFromReader(reader, true);
        }
        //GetPageByTag
        public static List<Article> GetPageByTag(int siteId, int tagId, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBArticles.GetPageByTag(siteId, tagId, pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader, true);
        }

        public static List<Article> GetArticleTopNew(string categories, int Top, int siteID, bool isLayTinPortalKhac = false)
        {
            IDataReader reader = DBArticles.GetArticleTopNew(categories, Top, siteID, isLayTinPortalKhac);
            return LoadListFromReader(reader);
        }

        public static List<Article> GetArticleTopHitCount(string categories, int Top, int siteID, bool isLayTinPortalKhac = false)
        {
            IDataReader reader = DBArticles.GetArticleTopHitCount(categories, Top, siteID, isLayTinPortalKhac);
            return LoadListFromReader(reader);
        }


        public static List<Article> GetArticleTop(int categoryID, int Top)
        {
            IDataReader reader = DBArticles.GetArticleTop(categoryID, Top);
            return LoadListFromReader(reader);
        }

        public static List<Article> GetArticleHotRight(int siteID, int number, int readMost)
        {
            IDataReader reader = DBArticles.GetArticleHotRight(siteID, number, readMost);
            return LoadListFromReader(reader);
        }
        public static List<Article> GetArticleHotNew(int siteID, int number, int readMost)
        {
            IDataReader reader = DBArticles.GetArticleHotNew(siteID, number, readMost);
            return LoadListFromReader(reader);
        }
        public static List<Article> GetArticleTopNew(int siteID, int number)
        {
            IDataReader reader = DBArticles.GetArticleTopNew(siteID, number);
            return LoadListFromReader(reader);
        }

        public static List<Article> GetArticleHotByCategory(int siteID, string categories, int number, int readMost, bool isHotCat = true)
        {
            IDataReader reader = DBArticles.GetArticleHotByCategory(siteID, categories, number, readMost, isHotCat);
            return LoadListFromReader(reader);
        }
        public static List<Article> GetArticleIsHomeByCategory(int siteID, string categories, int number, int readMost)
        {
            IDataReader reader = DBArticles.GetArticleIsHomeByCategory(siteID, categories, number, readMost);
            return LoadListFromReader(reader);
        }

        public static List<Article> GetArticleIsHomeHotByCategory(int siteID, string categories, int number, int readMost, bool isLayTinPortalKhac = false)
        {
            IDataReader reader = DBArticles.GetArticleIsHomeHotByCategory(siteID, categories, number, readMost, isLayTinPortalKhac);
            return LoadListFromReader(reader);
        }

        public static List<Article> GetArticleEventHot(int siteID, int number)
        {
            IDataReader reader = DBArticles.GetEventHot(siteID, number);
            return LoadListFromReader(reader);
        }

        public static List<Article> GetArticleByCategory(int categoryId)
        {
            IDataReader reader = DBArticles.GetArticleByCategory(categoryId);
            return LoadListFromReader(reader);
        }


        public static List<Article> GetArticleByID(int itemID)
        {
            IDataReader reader = DBArticles.GetArticleByID(itemID);
            return LoadListFromReader(reader);
        }
        public static List<Article> GetByModule(int siteId, int moduleId)
        {
            IDataReader reader = DBArticles.GetByModule(siteId, moduleId);
            return LoadListFromReader(reader);
        }
        public static List<Article> SelectByReference(string reference)
        {
            IDataReader reader = DBArticles.SelectByReference(reference);
            return LoadListFromReader(reader);
        }
        public static List<Article> GetArticleTopOrther(int categoryID, int ItemId, int Top, bool Hot)
        {
            IDataReader reader = DBArticles.GetArticleTopOrther(categoryID, ItemId, Top, Hot);
            return LoadListFromReader(reader);
        }
        public static List<Article> GetArticleTopHot(int Top, int siteID)
        {
            IDataReader reader = DBArticles.GetArticleTopHot(Top, siteID);
            return LoadListFromReader(reader);
        }
        public static List<Article> GetArticleTopHotOrther(int itemId, int Top, int siteID)
        {
            IDataReader reader = DBArticles.GetArticleTopHotOrther(itemId, Top, siteID);
            return LoadListFromReader(reader);
        }
        public static bool DeleteByModule(int moduleId)
        {
            return DBArticles.DeleteByModule(moduleId);
        }

        public static List<Article> GetOthersPageHotModule(string[] listModuleID, int pageSize, int currentPage)
        {
            return new List<Article>();


            //IDataReader reader = DBArticles.GetPageOther(listModuleID, pageSize, currentPage);
            //return LoadListFromReader(reader, true);
        }

        public static List<Article> GetOthersPageMostViewModule(string[] listModuleID, int pageSize, int currentPage)
        {
            IDataReader reader = DBArticles.GetPageOtherMostView(listModuleID, pageSize, currentPage);
            return LoadListFromReader(reader, true);
        }

        public static List<Article> GetOthersPageModule(string[] listModuleID, int pageSize, int currentPage)
        {
            IDataReader reader = DBArticles.GetOthersPageModule(listModuleID, pageSize, currentPage);
            return LoadListFromReader(reader, true);
        }

        public static List<Article> GetOthersPageByModuleId(int moduleID, int pageSize, int currentPage)
        {
            IDataReader reader = DBArticles.GetOthersPageByModuleId(moduleID, pageSize, currentPage);
            return LoadListFromReader(reader, true);
        }
        public static List<Article> GetOthersPageCategory(int categoryID, int pageSize, int currentPage)
        {
            IDataReader reader = DBArticles.GetOthersPageCategory(categoryID, pageSize, currentPage);
            return LoadListFromReader(reader, true);
        }

        public static List<Article> GetTopArticleHot(int siteID, int top, string categories)
        {
            IDataReader reader = DBArticles.GetTopArticleHot(siteID, top, categories);
            return LoadListReaderArticle(reader);
        }
        public static List<Article> GetBySite(int siteID)
        {
            IDataReader reader = DBArticles.GetBySite(siteID);
            return LoadAllListFormReader(reader);
        }


        public static List<Article> GetPageHotModule(string listModuleID, int pageSize, int pageNumber, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBArticles.GetPageHotModule(listModuleID, pageSize, pageNumber, out totalPages);
            return LoadListFromReader(reader, true);
        }

        public static List<Article> GetPageMostViewModule(string listModuleID, int pageSize, int pageNumber, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBArticles.GetPageMostViewModule(listModuleID, pageSize, pageNumber, out totalPages);
            return LoadListFromReader(reader, true);
        }

        public static List<Article> GetPageModule(string listModuleID, int pageSize, int pageNumber, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBArticles.GetPageModule(listModuleID, pageSize, pageNumber, out totalPages);
            return LoadListFromReader(reader, true);
        }

        public static List<Article> GetPageByModuleId(int moduleID, int pageSize, int pageNumber, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBArticles.GetPageModule(moduleID, pageSize, pageNumber, out totalPages);
            return LoadListFromReader(reader, true);
        }

        public static List<Article> GetPageCategory(int categoryID, int pageSize, int pageNumber, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBArticles.GetPageCategory(categoryID, pageSize, pageNumber, out totalPages);
            return LoadListFromReader(reader, true);
        }
        public static List<Article> GetOrtherByCategory(int categoryID, int itemID, int top)
        {
            IDataReader reader = DBArticles.GetOrtherByCategory(categoryID, itemID, top);
            return LoadListFromReader(reader);
        }


        public static List<Article> GetTopArticleCategoryHot(int siteId, int top, string categories)
        {
            IDataReader reader = DBArticles.GetTopArticleCategoryHot(siteId, top, categories);
            return LoadListFromReader(reader);
        }

        #endregion


        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of Article.
        /// </summary>
        public static int CompareByItemID(Article article1, Article article2)
        {
            return article1.ItemID.CompareTo(article2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of Article.
        /// </summary>
        public static int CompareByModuleID(Article article1, Article article2)
        {
            return article1.ModuleID.CompareTo(article2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of Article.
        /// </summary>
        public static int CompareByCategoryID(Article article1, Article article2)
        {
            return article1.CategoryID.CompareTo(article2.CategoryID);
        }
        /// <summary>
        /// Compares 2 instances of Article.
        /// </summary>
        public static int CompareByTitle(Article article1, Article article2)
        {
            return article1.Title.CompareTo(article2.Title);
        }
        /// <summary>
        /// Compares 2 instances of Article.
        /// </summary>
        public static int CompareBySummary(Article article1, Article article2)
        {
            return article1.Summary.CompareTo(article2.Summary);
        }
        /// <summary>
        /// Compares 2 instances of Article.
        /// </summary>
        public static int CompareByDescription(Article article1, Article article2)
        {
            return article1.Description.CompareTo(article2.Description);
        }
        /// <summary>
        /// Compares 2 instances of Article.
        /// </summary>
        public static int CompareByImageUrl(Article article1, Article article2)
        {
            return article1.ImageUrl.CompareTo(article2.ImageUrl);
        }
        /// <summary>
        /// Compares 2 instances of Article.
        /// </summary>
        public static int CompareByStartDate(Article article1, Article article2)
        {
            return article1.StartDate.CompareTo(article2.StartDate);
        }
        /// <summary>
        /// Compares 2 instances of Article.
        /// </summary>
        //public static int CompareByEndDate(Article article1, Article article2)
        //{
        //    return article1.EndDate.CompareTo(article2.EndDate);
        //}
        /// <summary>
        /// Compares 2 instances of Article.
        /// </summary>
        public static int CompareByCommentCount(Article article1, Article article2)
        {
            return article1.CommentCount.CompareTo(article2.CommentCount);
        }
        /// <summary>
        /// Compares 2 instances of Article.
        /// </summary>
        public static int CompareByHitCount(Article article1, Article article2)
        {
            return article1.HitCount.CompareTo(article2.HitCount);
        }
        /// <summary>
        /// Compares 2 instances of Article.
        /// </summary>
        public static int CompareByLocation(Article article1, Article article2)
        {
            return article1.Location.CompareTo(article2.Location);
        }
        /// <summary>
        /// Compares 2 instances of Article.
        /// </summary>
        public static int CompareByCreatedByUser(Article article1, Article article2)
        {
            return article1.CreatedByUser.CompareTo(article2.CreatedByUser);
        }
        /// <summary>
        /// Compares 2 instances of Article.
        /// </summary>
        public static int CompareByCreatedDate(Article article1, Article article2)
        {
            return article1.CreatedDate.CompareTo(article2.CreatedDate);
        }
        /// <summary>
        /// Compares 2 instances of Article.
        /// </summary>
        public static int CompareByLastModUtc(Article article1, Article article2)
        {
            return article1.LastModUtc.CompareTo(article2.LastModUtc);
        }
        /// <summary>
        /// Compares 2 instances of Article.
        /// </summary>
        public static int CompareByItemUrl(Article article1, Article article2)
        {
            return article1.ItemUrl.CompareTo(article2.ItemUrl);
        }
        /// <summary>
        /// Compares 2 instances of Article.
        /// </summary>
        public static int CompareByMetaTitle(Article article1, Article article2)
        {
            return article1.MetaTitle.CompareTo(article2.MetaTitle);
        }
        /// <summary>
        /// Compares 2 instances of Article.
        /// </summary>
        public static int CompareByMetaKeywords(Article article1, Article article2)
        {
            return article1.MetaKeywords.CompareTo(article2.MetaKeywords);
        }
        /// <summary>
        /// Compares 2 instances of Article.
        /// </summary>
        public static int CompareByMetaDescription(Article article1, Article article2)
        {
            return article1.MetaDescription.CompareTo(article2.MetaDescription);
        }
        /// <summary>
        /// Compares 2 instances of Article.
        /// </summary>
        public static int CompareByApprovedDate(Article article1, Article article2)
        {
            return article1.ApprovedDate.CompareTo(article2.ApprovedDate);
        }
        /// <summary>
        /// Compares 2 instances of Article.
        /// </summary>
        public static int CompareByTag(Article article1, Article article2)
        {
            return article1.Tag.CompareTo(article2.Tag);
        }

        #endregion


    }
    public class ArticleStatisticBO
    {
        private int itemId = -1;
        private string title = string.Empty;
        private int categoryId = -1;
        private int siteId = -1;
        private Guid userGuid = Guid.Empty;
        public DateTime? startDate = null;
        public DateTime? endDate = null;
        public DateTime? createdDate = null;
        public string categoryName = string.Empty;
        public int ItemID { get { return itemId; } set { itemId = value; } }
        public string Title { get { return title; } set { title = value; } }
        public int SiteID { get { return siteId; } set { siteId = value; } }
        public int CategoryID { get { return categoryId; } set { categoryId = value; } }
        public Guid UserGuid { get { return userGuid; } set { userGuid = value; } }
        public string CategoryName { get { return categoryName; } set { categoryName = value; } }
        public DateTime? StartDate { get { return startDate; } set { startDate = value; } }
        public DateTime? EndDate { get { return endDate; } set { endDate = value; } }
        public DateTime? CreatedDate { get { return createdDate; } set { createdDate = value; } }

        /// <summary>
        /// Lấy số lượng bài viết lọc theo năm hoặc tháng, categories
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static List<ArticleStatisticBO> GetTotalByYear(int year, string yearMonth, string categories, int siteId)
        {
            return LoadListFromReader(DBArticles.GetTotalByYear(year, yearMonth, categories, siteId));
        }

        public static List<ArticleStatisticBO> GetStaticForTab1(int year, int month, string categories, string siteId)
        {
            return LoadListFromReader(DBArticles.GetStaticForTab1(year, month, categories, siteId));
        }
        /// <summary>
        /// Thống kê tin bài lọc theo datetime và categories
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="categories"></param>
        /// <returns></returns>
        public static List<ArticleStatisticBO> StatisticTotalByCategory(int SiteId, DateTime? startDate, DateTime? endDate, string userGuid = "", string categories = "")
        {
            return LoadListFromReader(DBArticles.StatisticTotalByCategory(SiteId, startDate, endDate, userGuid, categories));
        }
        public static List<ArticleStatisticBO> StatisticTotalAllSite(string SiteId, DateTime? startDate, DateTime? endDate)
        {
            return LoadListFromReader(DBArticles.StatisticTotalAllSite(SiteId, startDate, endDate));
        }

        private static List<ArticleStatisticBO> LoadListFromReader(IDataReader reader)
        {
            List<ArticleStatisticBO> articleList = new List<ArticleStatisticBO>();
            try
            {
                while (reader.Read())
                {
                    ArticleStatisticBO article = new ArticleStatisticBO();
                    article.itemId = int.Parse(reader["ItemID"].ToString());
                    article.title = reader["Title"].ToString();
                    //article.eventId = int.Parse(reader["EventID"].ToString());
                    if (!string.IsNullOrEmpty(reader["CategoryID"].ToString()))
                    {
                        article.categoryId = int.Parse(reader["CategoryID"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["SiteID"].ToString()))
                    {
                        article.siteId = int.Parse(reader["SiteID"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["UserGuid"].ToString()))
                    {
                        article.userGuid = Guid.Parse(reader["UserGuid"].ToString());
                    }
                    //if (!string.IsNullOrEmpty(reader["CategoryName"].ToString()))
                    //{
                    //    article.categoryName = reader["CategoryName"].ToString();
                    //}
                    if (!string.IsNullOrEmpty(reader["StartDate"].ToString()))
                    {
                        article.startDate = Convert.ToDateTime(reader["StartDate"]);
                    }
                    if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                    {
                        article.endDate = DateTime.Parse(reader["EndDate"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["CreatedDate"].ToString()))
                    {
                        article.createdDate = DateTime.Parse(reader["CreatedDate"].ToString());
                    }

                    articleList.Add(article);

                }
            }
            finally
            {
                reader.Close();
            }

            return articleList;

        }
    }

    public class ArticleReferenceBO
    {
        private int itemId = -1;
        private string title = string.Empty;
        private int eventId = -1;
        private int categoryId = -1;
        public string startDate = string.Empty;
        public DateTime? endDate = null;
        public DateTime? createdDate = null;
        public string categoryName = string.Empty;
        private string fts = string.Empty;

        public int ItemID { get { return itemId; } set { itemId = value; } }
        public string Title { get { return title; } set { title = value; } }
        public int EventID { get { return eventId; } set { eventId = value; } }
        public int CategoryID { get { return categoryId; } set { categoryId = value; } }
        public string CategoryName { get { return categoryName; } set { categoryName = value; } }
        public string StartDate { get { return startDate; } set { startDate = value; } }
        public DateTime? EndDate { get { return endDate; } set { endDate = value; } }
        public DateTime? CreatedDate { get { return createdDate; } set { createdDate = value; } }
        public string FTS { get { return fts; } set { fts = value; } }



        public static PagedList<ArticleReferenceBO> GetPageReference(int pageIndex = 1, int pageSize = 10, int categoryId = 0, string keyword = "", DateTime? searchDate = null)
        {
            var totalPage = 0;
            var totalCount = 0;
            var listItem = LoadListFromReader(DBArticles.GetPageForReference(pageIndex, pageSize, categoryId, keyword, searchDate, out totalPage, out totalCount));
            var pagedList = new PagedList<ArticleReferenceBO>();
            pagedList.ListItem = listItem;
            pagedList.PageIndex = pageIndex;
            pagedList.PageSize = pageSize;
            pagedList.TotalCount = totalCount;
            pagedList.TotalPage = totalPage;
            return pagedList;
        }





        public static List<ArticleReferenceBO> GetList(int siteID, int itemID)
        {
            return LoadListFromReader(DBArticles.GetArticleReference(siteID, itemID));
        }

        private static List<ArticleReferenceBO> LoadListFromReader(IDataReader reader)
        {
            List<ArticleReferenceBO> articleList = new List<ArticleReferenceBO>();
            try
            {
                while (reader.Read())
                {
                    ArticleReferenceBO article = new ArticleReferenceBO();
                    article.itemId = int.Parse(reader["ItemID"].ToString());
                    article.title = reader["Title"].ToString();
                    //article.eventId = int.Parse(reader["EventID"].ToString());
                    if (!string.IsNullOrEmpty(reader["CategoryID"].ToString()))
                    {
                        article.categoryId = int.Parse(reader["CategoryID"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["CategoryName"].ToString()))
                    {
                        article.categoryName = reader["CategoryName"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["StartDate"].ToString()))
                    {
                        article.startDate = string.Format("{0:HH:mm dddd dd/MM/yyyy}", reader["StartDate"]);
                    }
                    if (!string.IsNullOrEmpty(reader["FTS"].ToString()))
                    {
                        article.fts = reader["FTS"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                    {
                        article.endDate = DateTime.Parse(reader["EndDate"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["CreatedDate"].ToString()))
                    {
                        article.createdDate = DateTime.Parse(reader["CreatedDate"].ToString());
                    }
                    articleList.Add(article);

                }
            }
            finally
            {
                reader.Close();
            }

            return articleList;

        }

    }
    public class ArticleSortBO
    {
        private int itemId = -1;
        private string title = string.Empty;
        private int categoryId = -1;
        public DateTime? startDate = null;
        public DateTime? endDate = null;
        public DateTime? createdDate = null;
        public string categoryName = string.Empty;
        private string imageUrl = string.Empty;
        private string itemUrl = string.Empty;
        private string categoryUrl = string.Empty;
        private string summary = string.Empty;
        private int hitCount = 0;
        private string titleHienThi = string.Empty;
        private int liveItem = 0;
        private int typeItem = 0;
        private bool isKetThucLive = false;
        private bool isLiveStream = false;
        private int loaiTinBai = 0;
        private string createdByUser = string.Empty;
        public string CreatedByUser
        {
            get { return createdByUser; }
            set { createdByUser = value; }
        }

        public int LoaiTinBai { get { return loaiTinBai; } set { loaiTinBai = value; } }
        public int LiveItem { get { return liveItem; } set { liveItem = value; } }
        public int TypeItem { get { return typeItem; } set { typeItem = value; } }
        public bool IsKetThucLive { get { return isKetThucLive; } set { isKetThucLive = value; } }
        public bool IsLiveStream { get { return isLiveStream; } set { isLiveStream = value; } }

        public string TitleHienThi { get { return titleHienThi; } set { titleHienThi = value; } }

        public int HitCount { get { return hitCount; } set { hitCount = value; } }
        public string Summary
        {
            get { return summary; }
            set { summary = value; }
        }

        public string CategoryUrl { get { return categoryUrl; } set { categoryUrl = value; } }
        public string ItemUrl { get { return itemUrl; } set { itemUrl = value; } }
        public string ImageUrl { get { return imageUrl; } set { imageUrl = value; } }
        public int ItemID { get { return itemId; } set { itemId = value; } }
        public string Title { get { return title; } set { title = value; } }
        public int CategoryID { get { return categoryId; } set { categoryId = value; } }
        public string CategoryName { get { return categoryName; } set { categoryName = value; } }
        public DateTime? StartDate { get { return startDate; } set { startDate = value; } }
        public DateTime? EndDate { get { return endDate; } set { endDate = value; } }
        public DateTime? CreatedDate { get { return createdDate; } set { createdDate = value; } }
        public int DelegateID { get; set; }
        public string DelegateName { get; set; }

        public static List<ArticleSortBO> GetArticleHot(int siteId, string categories, int top, bool hasLive = false)
        {
            IDataReader reader = DBArticles.GetArticleHot(siteId, categories, top);
            return LoadListFromReader(reader, true, true, false, false, true, hasLive);
        }

        public static List<ArticleSortBO> GetSearchPublished(int siteId, int searchWith, string keyword, int category, DateTime? date, DateTime? searchDate, int pageNumber, int pageSize, out int outTotalPage, out int outTotalArticle)
        {
            IDataReader reader = DBArticles.GetSearchPublished(siteId, searchWith, keyword, category, date, searchDate, pageNumber, pageSize, out outTotalPage, out outTotalArticle);
            return LoadListFromReaderPublished(reader);
        }
        private static List<ArticleSortBO> LoadListFromReaderPublished(IDataReader reader)
        {
            List<ArticleSortBO> articleList = new List<ArticleSortBO>();
            try
            {
                while (reader.Read())
                {
                    ArticleSortBO article = new ArticleSortBO();
                    article.itemId = int.Parse(reader["ItemID"].ToString());
                    article.title = reader["Title"].ToString();

                    if (!string.IsNullOrEmpty(reader["Summary"].ToString()))
                    {
                        article.summary = reader["Summary"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["CategoryID"].ToString()))
                    {
                        article.CategoryID = Convert.ToInt32(reader["CategoryID"]);
                    }

                    if (!string.IsNullOrEmpty(reader["StartDate"].ToString()))
                    {
                        article.startDate = Convert.ToDateTime(reader["StartDate"]);
                    }
                    if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                    {
                        article.endDate = DateTime.Parse(reader["EndDate"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["CreatedDate"].ToString()))
                    {
                        article.createdDate = DateTime.Parse(reader["CreatedDate"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["ImageUrl"].ToString()))
                    {
                        article.imageUrl = reader["ImageUrl"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["ItemUrl"].ToString()))
                    {
                        article.itemUrl = reader["ItemUrl"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["CreatedByUser"].ToString()))
                    {
                        article.createdByUser = reader["CreatedByUser"].ToString();
                    }
                    articleList.Add(article);
                }
            }
            finally
            {
                reader.Close();
            }

            return articleList;

        }
        private static List<ArticleSortBO> LoadListFromReaderNew(IDataReader reader)
        {
            List<ArticleSortBO> articleList = new List<ArticleSortBO>();
            try
            {
                while (reader.Read())
                {
                    ArticleSortBO article = new ArticleSortBO();
                    article.itemId = int.Parse(reader["ItemID"].ToString());
                    article.title = reader["Title"].ToString();
                    if (!string.IsNullOrEmpty(reader["CategoryID"].ToString()))
                    {
                        article.categoryId = int.Parse(reader["CategoryID"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["Summary"].ToString()))
                    {
                        article.summary = reader["Summary"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["CategoryName"].ToString()))
                    {
                        article.categoryName = reader["CategoryName"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["CategoryUrl"].ToString()))
                    {
                        article.categoryUrl = reader["CategoryUrl"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["DelegateID"].ToString()))
                    {
                        article.DelegateID = Convert.ToInt32(reader["DelegateID"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["DelegateName"].ToString()))
                    {
                        article.DelegateName = reader["DelegateName"].ToString();
                    }

                    if (!string.IsNullOrEmpty(reader["StartDate"].ToString()))
                    {
                        article.startDate = Convert.ToDateTime(reader["StartDate"]);
                    }
                    if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                    {
                        article.endDate = DateTime.Parse(reader["EndDate"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["CreatedDate"].ToString()))
                    {
                        article.createdDate = DateTime.Parse(reader["CreatedDate"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["ImageUrl"].ToString()))
                    {
                        article.imageUrl = reader["ImageUrl"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["ItemUrl"].ToString()))
                    {
                        article.itemUrl = reader["ItemUrl"].ToString();
                    }

                    articleList.Add(article);

                }
            }
            finally
            {
                reader.Close();
            }

            return articleList;

        }


        private static List<ArticleSortBO> LoadListFromReader(IDataReader reader, bool hasCategory = false, bool hasSummary = false, bool hasDelegate = false, bool hasHitCount = false, bool hasTitle = false, bool hasLive = false)
        {
            List<ArticleSortBO> articleList = new List<ArticleSortBO>();
            try
            {
                while (reader.Read())
                {
                    ArticleSortBO article = new ArticleSortBO();
                    article.itemId = int.Parse(reader["ItemID"].ToString());
                    article.title = reader["Title"].ToString();
                    if (!string.IsNullOrEmpty(reader["CategoryID"].ToString()))
                    {
                        article.categoryId = int.Parse(reader["CategoryID"].ToString());
                    }
                    if (hasSummary)
                    {
                        if (!string.IsNullOrEmpty(reader["Summary"].ToString()))
                        {
                            article.summary = reader["Summary"].ToString();
                        }
                    }
                    if (hasCategory)
                    {
                        if (!string.IsNullOrEmpty(reader["CategoryName"].ToString()))
                        {
                            article.categoryName = reader["CategoryName"].ToString();
                        }
                        if (!string.IsNullOrEmpty(reader["CategoryUrl"].ToString()))
                        {
                            article.categoryUrl = reader["CategoryUrl"].ToString();
                        }
                    }
                    if (hasDelegate)
                    {
                        if (!string.IsNullOrEmpty(reader["DelegateID"].ToString()))
                        {
                            article.DelegateID = Convert.ToInt32(reader["DelegateID"].ToString());
                        }
                        if (!string.IsNullOrEmpty(reader["DelegateName"].ToString()))
                        {
                            article.DelegateName = reader["DelegateName"].ToString();
                        }
                    }
                    if (hasHitCount)
                    {
                        if (!string.IsNullOrEmpty(reader["HitCount"].ToString()))
                        {
                            article.hitCount = Convert.ToInt32(reader["HitCount"]);
                        }
                    }
                    if (!string.IsNullOrEmpty(reader["StartDate"].ToString()))
                    {
                        article.startDate = Convert.ToDateTime(reader["StartDate"]);
                    }
                    if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                    {
                        article.endDate = DateTime.Parse(reader["EndDate"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["CreatedDate"].ToString()))
                    {
                        article.createdDate = DateTime.Parse(reader["CreatedDate"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["ImageUrl"].ToString()))
                    {
                        article.imageUrl = reader["ImageUrl"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["ItemUrl"].ToString()))
                    {
                        article.itemUrl = reader["ItemUrl"].ToString();
                    }
                    if (hasTitle)
                    {
                        if (!string.IsNullOrEmpty(reader["TitleHienThi"].ToString()))
                        {
                            article.titleHienThi = reader["TitleHienThi"].ToString();
                        }
                    }
                    if (hasLive)
                    {
                        if (!string.IsNullOrEmpty(reader["LiveItem"].ToString()))
                        {
                            article.liveItem = Convert.ToInt32(reader["LiveItem"]);
                        }
                        if (!string.IsNullOrEmpty(reader["TypeItem"].ToString()))
                        {
                            article.typeItem = Convert.ToInt32(reader["TypeItem"]);
                        }
                        if (!string.IsNullOrEmpty(reader["IsKetThucLive"].ToString()))
                        {
                            article.isKetThucLive = Convert.ToBoolean(reader["IsKetThucLive"]);
                        }

                        if (!string.IsNullOrEmpty(reader["IsLiveStream"].ToString()))
                        {
                            article.isLiveStream = Convert.ToBoolean(reader["IsLiveStream"]);
                        }

                    }
                    if (!string.IsNullOrEmpty(reader["LoaiTinBai"].ToString()))
                    {
                        article.loaiTinBai = Convert.ToInt32(reader["LoaiTinBai"]);
                    }
                    articleList.Add(article);
                }
            }
            finally
            {
                reader.Close();
            }

            return articleList;

        }
    }

}






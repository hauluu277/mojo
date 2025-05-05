using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mojoPortal.Data;
using System.Data;

namespace mojoPortal.Business
{
    public class ArticleBuilder
    {
        private const string featureGuid = "9392aca9-495a-447e-9e0c-de1f23d760f3";

        public static Guid FeatureGuid
        {
            get { return new Guid(featureGuid); }
        }
        #region Constructors

        public ArticleBuilder()
        { }


        public ArticleBuilder(
            int itemID)
        {
            GetArticle(
                itemID);
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



        #endregion

        #region Public Properties

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
        #endregion
        #region Private Methods
        private void GetArticle(
    int itemID)
        {
            using (IDataReader reader = DBArticleBuilder.GetOne(
                itemID))
            {
                PopulateFromReader(reader);
            }

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
                this.categoryID = Convert.ToInt32(reader["CategoryID"]);
                this.title = reader["Title"].ToString();
                this.summary = reader["Summary"].ToString();
                this.description = reader["Description"].ToString();
                this.imageUrl = reader["ImageUrl"].ToString();
                this.startDate = Convert.ToDateTime(reader["StartDate"]);
                if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                {
                    this.endDate = Convert.ToDateTime(reader["EndDate"]);
                }
                this.commentCount = Convert.ToInt32(reader["CommentCount"]);
                this.hitCount = Convert.ToInt32(reader["HitCount"]);
                this.articleGuid = new Guid(reader["ArticleGuid"].ToString());
                this.moduleGuid = new Guid(reader["ModuleGuid"].ToString());
                this.location = reader["Location"].ToString();
                this.userGuid = new Guid(reader["UserGuid"].ToString());
                this.createdByUser = reader["CreatedByUser"].ToString();
                this.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                this.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                this.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
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
                this.isHot = Convert.ToBoolean(reader["IsHot"]);
                this.isHome = Convert.ToBoolean(reader["IsHome"]);
                this.tag = reader["Tag"].ToString();
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
                //if (!String.IsNullOrEmpty(reader["IsHotNew"].ToString()))
                //{
                //    this.isHotNew = Convert.ToBoolean(reader["IsHotNew"]);
                //}
            }

        }

        private bool Create()
        {
            int newID = 0;
            articleGuid = Guid.NewGuid();
            this.hitCount = 0;
            newID = DBArticleBuilder.Create(
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
                this.articleReference
                //this.isHotNew
                );

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of Article. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBArticleBuilder.Update(
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
                this.articleReference
                //this.isHotNew
                );

        }
        #endregion

        #region Public Methods
        public bool Save()
        {
            if (this.itemID > 0)
            {
                return Update();
            }
            else
            {
                return Create();
            }
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
            return DBArticleBuilder.Delete(
                itemID);
        }

        /// <summary>
        /// Gets an IList with all instances of Article.
        /// </summary>
        public static List<ArticleBuilder> GetByModule(int moduleID)
        {
            IDataReader reader = DBArticleBuilder.GetByModule(moduleID);
            return LoadListFromReader(reader);
        }

        private static List<ArticleBuilder> LoadListFromReader(IDataReader reader, bool getpage = false)
        {
            List<ArticleBuilder> articleList = new List<ArticleBuilder>();
            try
            {
                while (reader.Read())
                {
                    ArticleBuilder article = new ArticleBuilder();
                    article.itemID = Convert.ToInt32(reader["ItemID"]);
                    article.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    article.categoryID = Convert.ToInt32(reader["CategoryID"]);
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
                    article.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                    article.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
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
        #endregion
    }
}

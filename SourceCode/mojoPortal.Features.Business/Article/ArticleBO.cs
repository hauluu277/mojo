using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using ArticleFeature.Data;
using mojoPortal.Business;
using EventFeature.Business;

namespace ArticleFeature.Business
{

    public class ArticleBO
    {

        #region Constructors

        public ArticleBO()
        { }


        #endregion

        #region Private Properties

        private int itemID = -1;
        private int moduleID = -1;
        private int categoryID = -1;
        private string title = string.Empty;
        private string summary = string.Empty;
        private string description = string.Empty;
        private string imageUrl = string.Empty;
        private DateTime startDate = DateTime.UtcNow;
        private DateTime? endDate = null;
        private int commentCount = 0;
        private int hitCount = 0;
        private Guid articleGuid = Guid.Empty;
        private Guid moduleGuid = Guid.Empty;
        private string location = string.Empty;
        private Guid userGuid = Guid.Empty;
        private string createdByUser = string.Empty;
        private DateTime createdDate = DateTime.UtcNow;
        private Guid lastModUserGuid = Guid.Empty;
        private DateTime lastModUtc = DateTime.UtcNow;
        private string itemUrl = string.Empty;
        private string metaTitle = string.Empty;
        private string metaKeywords = string.Empty;
        private string metaDescription = string.Empty;
        private bool isApproved = false;
        private Guid approvedGuid = Guid.Empty;
        private DateTime approvedDate = DateTime.UtcNow;
        private bool allowComment = false;
        private bool isHot = false;
        private string tag = string.Empty;
        //Full text search
        private string fts = string.Empty;
        private bool isHome = false;
        private int siteId = -1;
        private string searchIndexPath = string.Empty;

        private string categoryName = string.Empty;
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
        public bool IsApproved
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
        public bool IsHome
        {
            get { return isHome; }
            set { isHome = value; }
        }
        private List<Lookup> listTag = null;
        public List<Lookup> ListTag
        {
            get { return listTag; }
            set { listTag = value; }
        }
        private List<Event> listEvent = null;
        public List<Event> ListEvent
        {
            get { return listEvent; }
            set { listEvent = value; }
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

        #endregion
        private static List<ArticleBO> LoadListFromReader(IDataReader reader, bool getpage = false)
        {
            List<ArticleBO> articleList = new List<ArticleBO>();
            try
            {
                while (reader.Read())
                {
                    ArticleBO article = new ArticleBO();
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
                    article.isApproved = Convert.ToBoolean(reader["IsApproved"]);
                    article.approvedGuid = new Guid(reader["ApprovedGuid"].ToString());
                    article.approvedDate = Convert.ToDateTime(reader["ApprovedDate"]);
                    article.allowComment = Convert.ToBoolean(reader["AllowComment"]);
                    article.isHot = Convert.ToBoolean(reader["IsHot"]);
                    article.isHome = Convert.ToBoolean(reader["IsHome"]);
                    article.tag = reader["Tag"].ToString();
                    if (getpage)
                    {
                        article.categoryName = reader["CategoryName"].ToString();
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
        public static List<ArticleBO> GetArticleHot(int Top, int siteId, int categoryId)
        {
            IDataReader reader = DBArticleBO.GetArticleHot(Top, siteId, categoryId);
            return LoadListFromReader(reader);
        }
        public static List<ArticleBO> GetArticleTopHotOrther(int itemId, int Top, int langId, int siteId)
        {
            IDataReader reader = DBArticleBO.GetArticleTopHotOrther(itemId, Top, langId, siteId);
            return LoadListFromReader(reader);
        }
        public static List<ArticleBO> GetPageCategory(int categoryID, int langId, int pageSize, int pageNumber, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBArticleBO.GetPageCategory(categoryID, langId, pageSize, pageNumber, out totalPages);
            return LoadListFromReader(reader, true);
        }
        public static List<ArticleBO> GetArticleTopOrther(int categoryID, int langId, int ItemId, int Top, bool Hot)
        {
            IDataReader reader = DBArticleBO.GetArticleTopOrther(categoryID, ItemId, langId, Top, Hot);
            return LoadListFromReader(reader);
        }
        public static List<ArticleBO> GetArticleTop(int categoryID, int Top, int langId)
        {
            IDataReader reader = DBArticleBO.GetArticleTop(categoryID, Top, langId);
            return LoadListFromReader(reader);
        }
    }
}
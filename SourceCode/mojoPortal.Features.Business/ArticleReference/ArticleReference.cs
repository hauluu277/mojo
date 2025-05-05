
// Author:					HiNet
// Created:					2015-8-12
// Last Modified:			2015-8-12
// 
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)  
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using ArticleFeature.Data;
using mojoPortal.Data;

namespace ArticleFeature.Business
{

    public class ArticleReference
    {

        #region Constructors

        public ArticleReference()
        { }


        public ArticleReference(
            int itemID)
        {
            GetArticleReference(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int moduleID = -1;
        private int siteID = -1;
        private int categoryID = -1;
        private int rootArticleID = -1;
        private string title = string.Empty;
        private string summary = string.Empty;
        private string description = string.Empty;
        private DateTime startDate = DateTime.UtcNow;
        private DateTime? endDate = DateTime.UtcNow;
        private int commentCount = -1;
        private int hitCount = -1;
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
        private int langId = -1;
        private string metaKeywords = string.Empty;
        private string metaDescription = string.Empty;
        private bool isApproved = false;
        private Guid approvedGuid = Guid.Empty;
        private DateTime approvedDate = DateTime.UtcNow;
        private bool allowComment = false;
        private bool isHot = false;
        private bool isHome = false;
        private string tag = string.Empty;
        private string fTS = string.Empty;
        private string imageUrl = string.Empty;
        private string categoryName = string.Empty;

        #endregion

        #region Public Properties

        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public int ModuleID
        {
            get { return moduleID; }
            set { moduleID = value; }
        }
        public int SiteID
        {
            get { return siteID; }
            set { siteID = value; }
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
        public int RootArticleID
        {
            get { return rootArticleID; }
            set { rootArticleID = value; }
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
        public Guid ArticleGuid
        {
            get { return articleGuid; }
            set { articleGuid = value; }
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
        public int LangID
        {
            get { return langId; }
            set { langId = value; }
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
        public bool IsHome
        {
            get { return isHome; }
            set { isHome = value; }
        }
        public string Tag
        {
            get { return tag; }
            set { tag = value; }
        }
        public string FTS
        {
            get { return fTS; }
            set { fTS = value; }
        }
        public string ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of ArticleReference.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetArticleReference(
            int itemID)
        {
            using (IDataReader reader = DBArticleReference.GetOne(
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
                this.siteID = Convert.ToInt32(reader["SiteID"]);
                this.categoryID = Convert.ToInt32(reader["CategoryID"]);
                this.rootArticleID = Convert.ToInt32(reader["RootArticleID"]);
                this.title = reader["Title"].ToString();
                this.summary = reader["Summary"].ToString();
                this.description = reader["Description"].ToString();
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
                this.langId = Convert.ToInt32(reader["LangID"]);
                this.metaKeywords = reader["MetaKeywords"].ToString();
                this.metaDescription = reader["MetaDescription"].ToString();
                this.isApproved = Convert.ToBoolean(reader["IsApproved"]);
                this.approvedGuid = new Guid(reader["ApprovedGuid"].ToString());
                this.approvedDate = Convert.ToDateTime(reader["ApprovedDate"]);
                this.allowComment = Convert.ToBoolean(reader["AllowComment"]);
                this.isHot = Convert.ToBoolean(reader["IsHot"]);
                this.isHome = Convert.ToBoolean(reader["IsHome"]);
                this.tag = reader["Tag"].ToString();
                this.fTS = reader["FTS"].ToString();
                this.imageUrl = reader["ImageUrl"].ToString();

            }

        }

        /// <summary>
        /// Persists a new instance of ArticleReference. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBArticleReference.Create(
                this.moduleID,
                this.siteID,
                this.categoryID,
                this.rootArticleID,
                this.title,
                this.summary,
                this.description,
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
                this.langId,
                this.metaKeywords,
                this.metaDescription,
                this.isApproved,
                this.approvedGuid,
                this.approvedDate,
                this.allowComment,
                this.isHot,
                this.isHome,
                this.tag,
                this.fTS,
                this.imageUrl);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of ArticleReference. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBArticleReference.Update(
                this.itemID,
                this.moduleID,
                this.siteID,
                this.categoryID,
                this.rootArticleID,
                this.title,
                this.summary,
                this.description,
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
                this.langId,
                this.metaKeywords,
                this.metaDescription,
                this.isApproved,
                this.approvedGuid,
                this.approvedDate,
                this.allowComment,
                this.isHot,
                this.isHome,
                this.tag,
                this.fTS,
                this.imageUrl);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of ArticleReference. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
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
        /// Deletes an instance of ArticleReference. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBArticleReference.Delete(
                itemID);
        }

        public static List<ArticleReference> GetOthersPageByModuleId(int moduleID, int pageSize, int currentPage)
        {
            IDataReader reader = DBArticles.GetOthersPageByModuleId(moduleID, pageSize, currentPage);
            return LoadListFromReader(reader, true);
        }
        /// <summary>
        /// Gets a count of ArticleReference. 
        /// </summary>
        public static int GetCount(int siteId, int moduleId, int categoryID, bool? status, string keyword)
        {
            return DBArticleReference.GetCount(siteId, moduleId, categoryID, status, keyword);
        }

        private static List<ArticleReference> LoadListFromReader(IDataReader reader, bool getpage = false)
        {
            List<ArticleReference> ArticleReferenceList = new List<ArticleReference>();
            try
            {
                while (reader.Read())
                {
                    ArticleReference ArticleReference = new ArticleReference();
                    ArticleReference.itemID = Convert.ToInt32(reader["ItemID"]);
                    ArticleReference.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    ArticleReference.siteID = Convert.ToInt32(reader["SiteID"]);
                    ArticleReference.categoryID = Convert.ToInt32(reader["CategoryID"]);
                    ArticleReference.rootArticleID = Convert.ToInt32(reader["RootArticleID"]);
                    ArticleReference.title = reader["Title"].ToString();
                    ArticleReference.summary = reader["Summary"].ToString();
                    ArticleReference.description = reader["Description"].ToString();
                    ArticleReference.startDate = Convert.ToDateTime(reader["StartDate"]);
                    if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                    {
                        ArticleReference.endDate = Convert.ToDateTime(reader["EndDate"]);
                    }
                    ArticleReference.commentCount = Convert.ToInt32(reader["CommentCount"]);
                    ArticleReference.hitCount = Convert.ToInt32(reader["HitCount"]);
                    ArticleReference.articleGuid = new Guid(reader["ArticleGuid"].ToString());
                    ArticleReference.moduleGuid = new Guid(reader["ModuleGuid"].ToString());
                    ArticleReference.location = reader["Location"].ToString();
                    ArticleReference.userGuid = new Guid(reader["UserGuid"].ToString());
                    ArticleReference.createdByUser = reader["CreatedByUser"].ToString();
                    ArticleReference.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    ArticleReference.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                    ArticleReference.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                    ArticleReference.itemUrl = reader["ItemUrl"].ToString();
                    ArticleReference.metaTitle = reader["MetaTitle"].ToString();
                    ArticleReference.langId = Convert.ToInt32(reader["LangID"]);
                    ArticleReference.metaKeywords = reader["MetaKeywords"].ToString();
                    ArticleReference.metaDescription = reader["MetaDescription"].ToString();
                    ArticleReference.isApproved = Convert.ToBoolean(reader["IsApproved"]);
                    ArticleReference.approvedGuid = new Guid(reader["ApprovedGuid"].ToString());
                    ArticleReference.approvedDate = Convert.ToDateTime(reader["ApprovedDate"]);
                    ArticleReference.allowComment = Convert.ToBoolean(reader["AllowComment"]);
                    ArticleReference.isHot = Convert.ToBoolean(reader["IsHot"]);
                    ArticleReference.isHome = Convert.ToBoolean(reader["IsHome"]);
                    ArticleReference.tag = reader["Tag"].ToString();
                    ArticleReference.fTS = reader["FTS"].ToString();
                    ArticleReference.imageUrl = reader["ImageUrl"].ToString();
                    if (getpage)
                    {
                        ArticleReference.categoryName = reader["CategoryName"].ToString();
                    }
                    ArticleReferenceList.Add(ArticleReference);

                }
            }
            finally
            {
                reader.Close();
            }

            return ArticleReferenceList;

        }

        /// <summary>
        /// Gets an IList with all instances of ArticleReference.
        /// </summary>
        public static List<ArticleReference> GetAll()
        {
            IDataReader reader = DBArticleReference.GetAll();
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of ArticleReference.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<ArticleReference> GetPage(int siteId, int moduleId, int pageNumber, int pageSize, int categoryID, bool? status, string keyword, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBArticleReference.GetPage(siteId, moduleId, pageNumber, pageSize, categoryID, status, keyword, out totalPages);
            return LoadListFromReader(reader, true);
        }
        public static ArticleReference GetArticleByRootId(int RootItemId)
        {
            IDataReader reader = DBArticleReference.GetArticleByRootId(RootItemId);
            return LoadFromReader(reader);
        }
        public static List<ArticleReference> GetArticleTopOrther(int categoryID, int ItemId, int Top, bool Hot)
        {
            IDataReader reader = DBArticleReference.GetArticleTopOrther(categoryID, ItemId, Top, Hot);
            return LoadListFromReader(reader);
        }
        public static List<ArticleReference> GetArticleTopHot(int Top)
        {
            IDataReader reader = DBArticleReference.GetArticleTopHot(Top);
            return LoadListFromReader(reader);
        }
        public static List<ArticleReference> GetArticleTopHotOrther(int itemId, int Top)
        {
            IDataReader reader = DBArticleReference.GetArticleTopHotOrther(itemId, Top);
            return LoadListFromReader(reader);
        }
        public static ArticleReference GetArticleByID(int itemId)
        {
            IDataReader reader = DBArticleReference.GetArticleByID(itemId);
            return LoadFromReader(reader);
        }
        public static List<ArticleReference> GetArticleTop(int categoryID, int Top)
        {
            IDataReader reader = DBArticleReference.GetArticleTop(categoryID, Top);
            return LoadListFromReader(reader);
        }
        public static List<ArticleReference> GetPageCategory(int categoryID, int pageSize, int pageNumber, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBArticleReference.GetPageCategory(categoryID, pageSize, pageNumber, out totalPages);
            return LoadListFromReader(reader, true);
        }
        private static ArticleReference LoadFromReader(IDataReader reader)
        {
            ArticleReference ArticleReference = new ArticleReference();
            try
            {
                while (reader.Read())
                {
                    ArticleReference.itemID = Convert.ToInt32(reader["ItemID"]);
                    ArticleReference.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    ArticleReference.siteID = Convert.ToInt32(reader["SiteID"]);
                    ArticleReference.categoryID = Convert.ToInt32(reader["CategoryID"]);
                    ArticleReference.rootArticleID = Convert.ToInt32(reader["RootArticleID"]);
                    ArticleReference.title = reader["Title"].ToString();
                    ArticleReference.summary = reader["Summary"].ToString();
                    ArticleReference.description = reader["Description"].ToString();
                    ArticleReference.startDate = Convert.ToDateTime(reader["StartDate"]);
                    if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                    {
                        ArticleReference.endDate = Convert.ToDateTime(reader["EndDate"]);
                    }
                    ArticleReference.commentCount = Convert.ToInt32(reader["CommentCount"]);
                    ArticleReference.hitCount = Convert.ToInt32(reader["HitCount"]);
                    ArticleReference.articleGuid = new Guid(reader["ArticleGuid"].ToString());
                    ArticleReference.moduleGuid = new Guid(reader["ModuleGuid"].ToString());
                    ArticleReference.location = reader["Location"].ToString();
                    ArticleReference.userGuid = new Guid(reader["UserGuid"].ToString());
                    ArticleReference.createdByUser = reader["CreatedByUser"].ToString();
                    ArticleReference.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    ArticleReference.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                    ArticleReference.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                    ArticleReference.itemUrl = reader["ItemUrl"].ToString();
                    ArticleReference.metaTitle = reader["MetaTitle"].ToString();
                    ArticleReference.langId = Convert.ToInt32(reader["LangID"]);
                    ArticleReference.metaKeywords = reader["MetaKeywords"].ToString();
                    ArticleReference.metaDescription = reader["MetaDescription"].ToString();
                    ArticleReference.isApproved = Convert.ToBoolean(reader["IsApproved"]);
                    ArticleReference.approvedGuid = new Guid(reader["ApprovedGuid"].ToString());
                    ArticleReference.approvedDate = Convert.ToDateTime(reader["ApprovedDate"]);
                    ArticleReference.allowComment = Convert.ToBoolean(reader["AllowComment"]);
                    ArticleReference.isHot = Convert.ToBoolean(reader["IsHot"]);
                    ArticleReference.isHome = Convert.ToBoolean(reader["IsHome"]);
                    ArticleReference.tag = reader["Tag"].ToString();
                    ArticleReference.fTS = reader["FTS"].ToString();
                    ArticleReference.imageUrl = reader["ImageUrl"].ToString();
                }
            }
            finally
            {
                reader.Close();
            }

            return ArticleReference;

        }

        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of ArticleReference.
        /// </summary>
        public static int CompareByItemID(ArticleReference ArticleReference1, ArticleReference ArticleReference2)
        {
            return ArticleReference1.ItemID.CompareTo(ArticleReference2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of ArticleReference.
        /// </summary>
        public static int CompareByModuleID(ArticleReference ArticleReference1, ArticleReference ArticleReference2)
        {
            return ArticleReference1.ModuleID.CompareTo(ArticleReference2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of ArticleReference.
        /// </summary>
        public static int CompareBySiteID(ArticleReference ArticleReference1, ArticleReference ArticleReference2)
        {
            return ArticleReference1.SiteID.CompareTo(ArticleReference2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of ArticleReference.
        /// </summary>
        public static int CompareByCategoryID(ArticleReference ArticleReference1, ArticleReference ArticleReference2)
        {
            return ArticleReference1.CategoryID.CompareTo(ArticleReference2.CategoryID);
        }
        /// <summary>
        /// Compares 2 instances of ArticleReference.
        /// </summary>
        public static int CompareByRootArticleID(ArticleReference ArticleReference1, ArticleReference ArticleReference2)
        {
            return ArticleReference1.RootArticleID.CompareTo(ArticleReference2.RootArticleID);
        }
        /// <summary>
        /// Compares 2 instances of ArticleReference.
        /// </summary>
        public static int CompareByTitle(ArticleReference ArticleReference1, ArticleReference ArticleReference2)
        {
            return ArticleReference1.Title.CompareTo(ArticleReference2.Title);
        }
        /// <summary>
        /// Compares 2 instances of ArticleReference.
        /// </summary>
        public static int CompareBySumary(ArticleReference ArticleReference1, ArticleReference ArticleReference2)
        {
            return ArticleReference1.Summary.CompareTo(ArticleReference2.Summary);
        }
        /// <summary>
        /// Compares 2 instances of ArticleReference.
        /// </summary>
        public static int CompareByDescription(ArticleReference ArticleReference1, ArticleReference ArticleReference2)
        {
            return ArticleReference1.Description.CompareTo(ArticleReference2.Description);
        }
        /// <summary>
        /// Compares 2 instances of ArticleReference.
        /// </summary>
        public static int CompareByStartDate(ArticleReference ArticleReference1, ArticleReference ArticleReference2)
        {
            return ArticleReference1.StartDate.CompareTo(ArticleReference2.StartDate);
        }
        /// <summary>
        /// Compares 2 instances of ArticleReference.
        /// </summary>
        /// <summary>
        /// Compares 2 instances of ArticleReference.
        /// </summary>
        public static int CompareByCommentCount(ArticleReference ArticleReference1, ArticleReference ArticleReference2)
        {
            return ArticleReference1.CommentCount.CompareTo(ArticleReference2.CommentCount);
        }
        /// <summary>
        /// Compares 2 instances of ArticleReference.
        /// </summary>
        public static int CompareByHitCount(ArticleReference ArticleReference1, ArticleReference ArticleReference2)
        {
            return ArticleReference1.HitCount.CompareTo(ArticleReference2.HitCount);
        }
        /// <summary>
        /// Compares 2 instances of ArticleReference.
        /// </summary>
        public static int CompareByLocation(ArticleReference ArticleReference1, ArticleReference ArticleReference2)
        {
            return ArticleReference1.Location.CompareTo(ArticleReference2.Location);
        }
        /// <summary>
        /// Compares 2 instances of ArticleReference.
        /// </summary>
        public static int CompareByCreatedByUser(ArticleReference ArticleReference1, ArticleReference ArticleReference2)
        {
            return ArticleReference1.CreatedByUser.CompareTo(ArticleReference2.CreatedByUser);
        }
        /// <summary>
        /// Compares 2 instances of ArticleReference.
        /// </summary>
        public static int CompareByCreatedDate(ArticleReference ArticleReference1, ArticleReference ArticleReference2)
        {
            return ArticleReference1.CreatedDate.CompareTo(ArticleReference2.CreatedDate);
        }
        /// <summary>
        /// Compares 2 instances of ArticleReference.
        /// </summary>
        public static int CompareByLastModUtc(ArticleReference ArticleReference1, ArticleReference ArticleReference2)
        {
            return ArticleReference1.LastModUtc.CompareTo(ArticleReference2.LastModUtc);
        }
        /// <summary>
        /// Compares 2 instances of ArticleReference.
        /// </summary>
        public static int CompareByItemUrl(ArticleReference ArticleReference1, ArticleReference ArticleReference2)
        {
            return ArticleReference1.ItemUrl.CompareTo(ArticleReference2.ItemUrl);
        }
        /// <summary>
        /// Compares 2 instances of ArticleReference.
        /// </summary>
        public static int CompareByMetaTitle(ArticleReference ArticleReference1, ArticleReference ArticleReference2)
        {
            return ArticleReference1.MetaTitle.CompareTo(ArticleReference2.MetaTitle);
        }
        /// <summary>
        /// Compares 2 instances of ArticleReference.
        /// </summary>
        public static int CompareByMetaKeywords(ArticleReference ArticleReference1, ArticleReference ArticleReference2)
        {
            return ArticleReference1.MetaKeywords.CompareTo(ArticleReference2.MetaKeywords);
        }
        /// <summary>
        /// Compares 2 instances of ArticleReference.
        /// </summary>
        public static int CompareByMetaDescription(ArticleReference ArticleReference1, ArticleReference ArticleReference2)
        {
            return ArticleReference1.MetaDescription.CompareTo(ArticleReference2.MetaDescription);
        }
        /// <summary>
        /// Compares 2 instances of ArticleReference.
        /// </summary>
        public static int CompareByApprovedDate(ArticleReference ArticleReference1, ArticleReference ArticleReference2)
        {
            return ArticleReference1.ApprovedDate.CompareTo(ArticleReference2.ApprovedDate);
        }
        /// <summary>
        /// Compares 2 instances of ArticleReference.
        /// </summary>
        public static int CompareByTag(ArticleReference ArticleReference1, ArticleReference ArticleReference2)
        {
            return ArticleReference1.Tag.CompareTo(ArticleReference2.Tag);
        }
        /// <summary>
        /// Compares 2 instances of ArticleReference.
        /// </summary>
        public static int CompareByFTS(ArticleReference ArticleReference1, ArticleReference ArticleReference2)
        {
            return ArticleReference1.FTS.CompareTo(ArticleReference2.FTS);
        }

        #endregion


    }

}






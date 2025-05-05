
// Author:					NAMDV
// Created:					2015-12-14
// Last Modified:			2015-12-14

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using ArticleFeature.Data;

namespace ArticleFeature.Business
{

    public class ArticleComment
    {

        #region Constructors

        public ArticleComment()
        { }


        public ArticleComment(
            int articleCommentID)
        {
            GetArticleComment(
                articleCommentID);
        }

        #endregion

        #region Private Properties

        private int articleCommentID = -1;
        private int moduleID = -1;
        private int itemID = -1;
        private string comment = string.Empty;
        private string title = string.Empty;
        private string name = string.Empty;
        private string uRL = string.Empty;
        private DateTime dateCreated = DateTime.UtcNow;
        private bool? isApproved = null;
        private Guid approvedGuid = Guid.Empty;
        private DateTime approvedDate = DateTime.UtcNow;
        private bool? isPublised = null;
        private Guid publishedGuid = Guid.Empty;
        private DateTime publishedDate = DateTime.UtcNow;

        #endregion

        #region Public Properties

        public int ArticleCommentID
        {
            get { return articleCommentID; }
            set { articleCommentID = value; }
        }
        public int ModuleID
        {
            get { return moduleID; }
            set { moduleID = value; }
        }
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string URL
        {
            get { return uRL; }
            set { uRL = value; }
        }
        public DateTime DateCreated
        {
            get { return dateCreated; }
            set { dateCreated = value; }
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
        public bool? IsPublised
        {
            get { return isPublised; }
            set { isPublised = value; }
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

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of ArticleComment.
        /// </summary>
        /// <param name="articleCommentID"> articleCommentID </param>
        private void GetArticleComment(
            int articleCommentID)
        {
            using (IDataReader reader = DBArticleComments.GetOne(
                articleCommentID))
            {
                PopulateFromReader(reader);
            }

        }


        private void PopulateFromReader(IDataReader reader)
        {
            if (reader.Read())
            {
                this.articleCommentID = Convert.ToInt32(reader["ArticleCommentID"]);
                this.moduleID = Convert.ToInt32(reader["ModuleID"]);
                this.itemID = Convert.ToInt32(reader["ItemID"]);
                this.comment = reader["Comment"].ToString();
                this.title = reader["Title"].ToString();
                this.name = reader["Name"].ToString();
                this.uRL = reader["URL"].ToString();
                this.dateCreated = Convert.ToDateTime(reader["DateCreated"]);
                this.isApproved = Convert.ToBoolean(reader["IsApproved"]);
                this.approvedGuid = new Guid(reader["ApprovedGuid"].ToString());
                this.approvedDate = Convert.ToDateTime(reader["ApprovedDate"]);
                this.isPublised = Convert.ToBoolean(reader["IsPublised"]);
                this.publishedGuid = new Guid(reader["PublishedGuid"].ToString());
                this.publishedDate = Convert.ToDateTime(reader["PublishedDate"]);

            }

        }

        /// <summary>
        /// Persists a new instance of ArticleComment. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBArticleComments.Create(
                this.moduleID,
                this.itemID,
                this.comment,
                this.title,
                this.name,
                this.uRL,
                this.dateCreated,
                this.isApproved,
                this.approvedGuid,
                this.approvedDate,
                this.isPublised,
                this.publishedGuid,
                this.publishedDate);

            this.articleCommentID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of ArticleComment. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBArticleComments.Update(
                this.articleCommentID,
                this.moduleID,
                this.itemID,
                this.comment,
                this.title,
                this.name,
                this.uRL,
                this.dateCreated,
                this.isApproved,
                this.approvedGuid,
                this.approvedDate,
                this.isPublised,
                this.publishedGuid,
                this.publishedDate);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of ArticleComment. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        public bool Save()
        {
            if (this.articleCommentID > 0)
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
        /// Deletes an instance of ArticleComment. Returns true on success.
        /// </summary>
        /// <param name="articleCommentID"> articleCommentID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int articleCommentID)
        {
            return DBArticleComments.Delete(
                articleCommentID);
        }


        /// <summary>
        /// Gets a count of ArticleComment. 
        /// </summary>
        public static int GetCount()
        {
            return DBArticleComments.GetCount();
        }

        private static List<ArticleComment> LoadListFromReader(IDataReader reader)
        {
            List<ArticleComment> articleCommentList = new List<ArticleComment>();
            try
            {
                while (reader.Read())
                {
                    ArticleComment articleComment = new ArticleComment();
                    articleComment.articleCommentID = Convert.ToInt32(reader["ArticleCommentID"]);
                    articleComment.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    articleComment.itemID = Convert.ToInt32(reader["ItemID"]);
                    articleComment.comment = reader["Comment"].ToString();
                    articleComment.title = reader["Title"].ToString();
                    articleComment.name = reader["Name"].ToString();
                    articleComment.uRL = reader["URL"].ToString();
                    articleComment.dateCreated = Convert.ToDateTime(reader["DateCreated"]);
                    articleComment.isApproved = Convert.ToBoolean(reader["IsApproved"]);
                    articleComment.approvedGuid = new Guid(reader["ApprovedGuid"].ToString());
                    articleComment.approvedDate = Convert.ToDateTime(reader["ApprovedDate"]);
                    articleComment.isPublised = Convert.ToBoolean(reader["IsPublised"]);
                    articleComment.publishedGuid = new Guid(reader["PublishedGuid"].ToString());
                    articleComment.publishedDate = Convert.ToDateTime(reader["PublishedDate"]);
                    articleCommentList.Add(articleComment);

                }
            }
            finally
            {
                reader.Close();
            }

            return articleCommentList;

        }

        /// <summary>
        /// Gets an IList with all instances of ArticleComment.
        /// </summary>
        public static List<ArticleComment> GetAll()
        {
            IDataReader reader = DBArticleComments.GetAll();
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of ArticleComment.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<ArticleComment> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBArticleComments.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of ArticleComment.
        /// </summary>
        public static int CompareByArticleCommentID(ArticleComment articleComment1, ArticleComment articleComment2)
        {
            return articleComment1.ArticleCommentID.CompareTo(articleComment2.ArticleCommentID);
        }
        /// <summary>
        /// Compares 2 instances of ArticleComment.
        /// </summary>
        public static int CompareByModuleID(ArticleComment articleComment1, ArticleComment articleComment2)
        {
            return articleComment1.ModuleID.CompareTo(articleComment2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of ArticleComment.
        /// </summary>
        public static int CompareByItemID(ArticleComment articleComment1, ArticleComment articleComment2)
        {
            return articleComment1.ItemID.CompareTo(articleComment2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of ArticleComment.
        /// </summary>
        public static int CompareByComment(ArticleComment articleComment1, ArticleComment articleComment2)
        {
            return articleComment1.Comment.CompareTo(articleComment2.Comment);
        }
        /// <summary>
        /// Compares 2 instances of ArticleComment.
        /// </summary>
        public static int CompareByTitle(ArticleComment articleComment1, ArticleComment articleComment2)
        {
            return articleComment1.Title.CompareTo(articleComment2.Title);
        }
        /// <summary>
        /// Compares 2 instances of ArticleComment.
        /// </summary>
        public static int CompareByName(ArticleComment articleComment1, ArticleComment articleComment2)
        {
            return articleComment1.Name.CompareTo(articleComment2.Name);
        }
        /// <summary>
        /// Compares 2 instances of ArticleComment.
        /// </summary>
        public static int CompareByURL(ArticleComment articleComment1, ArticleComment articleComment2)
        {
            return articleComment1.URL.CompareTo(articleComment2.URL);
        }
        /// <summary>
        /// Compares 2 instances of ArticleComment.
        /// </summary>
        public static int CompareByDateCreated(ArticleComment articleComment1, ArticleComment articleComment2)
        {
            return articleComment1.DateCreated.CompareTo(articleComment2.DateCreated);
        }
        /// <summary>
        /// Compares 2 instances of ArticleComment.
        /// </summary>
        public static int CompareByApprovedDate(ArticleComment articleComment1, ArticleComment articleComment2)
        {
            return articleComment1.ApprovedDate.CompareTo(articleComment2.ApprovedDate);
        }
        /// <summary>
        /// Compares 2 instances of ArticleComment.
        /// </summary>
        public static int CompareByPublishedDate(ArticleComment articleComment1, ArticleComment articleComment2)
        {
            return articleComment1.PublishedDate.CompareTo(articleComment2.PublishedDate);
        }

        #endregion


    }

}






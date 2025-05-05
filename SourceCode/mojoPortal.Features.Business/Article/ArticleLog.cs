
// Author:					Joe Audette
// Created:					2017-10-30
// Last Modified:			2017-10-30
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
using mojoPortal.Data;

namespace mojoPortal.Business
{

    public class ArticleLog
    {

        #region Constructors

        public ArticleLog()
        { }


        public ArticleLog(
            int itemID)
        {
            GetArticleLog(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int articleID = -1;
        private int userID = -1;
        private string userName = string.Empty;
        private DateTime? postDate = null;
        private DateTime? startDate = null;
        private DateTime? endDate = null;
        private bool? isPublic = null;
        private bool? isApprove = null;
        private string comment = string.Empty;
        private DateTime createDate = DateTime.Now;

        #endregion

        #region Public Properties

        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public int ArticleID
        {
            get { return articleID; }
            set { articleID = value; }
        }
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public DateTime? PostDate
        {
            get { return postDate; }
            set { postDate = value; }
        }
        public DateTime? StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        public DateTime? EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        public bool? IsPublic
        {
            get { return isPublic; }
            set { isPublic = value; }
        }
        public bool? IsApprove
        {
            get { return isApprove; }
            set { isApprove = value; }
        }
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of ArticleLog.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetArticleLog(
            int itemID)
        {
            using (IDataReader reader = DBArticleLog.GetOne(
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
                this.articleID = Convert.ToInt32(reader["ArticleID"]);
                this.userID = Convert.ToInt32(reader["UserID"]);
                this.postDate = Convert.ToDateTime(reader["PostDate"]);
                this.startDate = Convert.ToDateTime(reader["StartDate"]);
                this.endDate = Convert.ToDateTime(reader["EndDate"]);
                this.isPublic = Convert.ToBoolean(reader["IsPublic"]);
                this.isApprove = Convert.ToBoolean(reader["IsApprove"]);
                this.comment = reader["Comment"].ToString();
                this.createDate = Convert.ToDateTime(reader["CreateDate"]);
            }

        }

        /// <summary>
        /// Persists a new instance of ArticleLog. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBArticleLog.Create(
                this.articleID,
                this.userID,
                this.postDate,
                this.startDate,
                this.endDate,
                this.isPublic,
                this.isApprove,
                this.comment,
                this.createDate);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of ArticleLog. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBArticleLog.Update(
                this.itemID,
                this.articleID,
                this.userID,
                this.postDate,
                this.startDate,
                this.endDate,
                this.isPublic,
                this.isApprove,
                this.comment,
                this.createDate);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of ArticleLog. Returns true on success.
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
        /// Deletes an instance of ArticleLog. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBArticleLog.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of ArticleLog. 
        /// </summary>
        public static int GetCount()
        {
            return DBArticleLog.GetCount();
        }

        private static List<ArticleLog> LoadListFromReader(IDataReader reader)
        {
            List<ArticleLog> ArticleLogList = new List<ArticleLog>();
            try
            {
                while (reader.Read())
                {
                    ArticleLog ArticleLog = new ArticleLog();
                    ArticleLog.itemID = Convert.ToInt32(reader["ItemID"]);
                    ArticleLog.articleID = Convert.ToInt32(reader["ArticleID"]);
                    ArticleLog.userID = Convert.ToInt32(reader["UserID"]);
                    if (!string.IsNullOrEmpty(reader["UserName"].ToString()))
                    {
                        ArticleLog.userName = reader["UserName"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["PostDate"].ToString()))
                    {
                        ArticleLog.postDate = Convert.ToDateTime(reader["PostDate"]);
                    }
                    if (!string.IsNullOrEmpty(reader["StartDate"].ToString()))
                    {
                        ArticleLog.startDate = Convert.ToDateTime(reader["StartDate"]);
                    }
                    if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                    {
                        ArticleLog.endDate = Convert.ToDateTime(reader["EndDate"]);
                    }
                    if (!string.IsNullOrEmpty(reader["IsPublic"].ToString()))
                    {
                        ArticleLog.isPublic = Convert.ToBoolean(reader["IsPublic"]);
                    }
                    if (!string.IsNullOrEmpty(reader["IsApprove"].ToString()))
                    {
                        ArticleLog.isApprove = Convert.ToBoolean(reader["IsApprove"]);
                    }
                    ArticleLog.comment = reader["Comment"].ToString();
                    if (!string.IsNullOrEmpty(reader["CreateDate"].ToString()))
                    {
                        ArticleLog.createDate = Convert.ToDateTime(reader["CreateDate"]);
                    }
                    ArticleLogList.Add(ArticleLog);

                }
            }
            finally
            {
                reader.Close();
            }

            return ArticleLogList;

        }

        /// <summary>
        /// Gets an IList with all instances of ArticleLog.
        /// </summary>
        public static List<ArticleLog> GetAll()
        {
            IDataReader reader = DBArticleLog.GetAll();
            return LoadListFromReader(reader);

        }
        /// <summary>
        /// Lấy danh sách ArticeLog By AticleID
        /// </summary>
        public static List<ArticleLog> GetListByArticle(int articleID)
        {
            IDataReader reader = DBArticleLog.GetListByArticle(articleID);
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of ArticleLog.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<ArticleLog> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBArticleLog.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of ArticleLog.
        /// </summary>
        public static int CompareByItemID(ArticleLog ArticleLog1, ArticleLog ArticleLog2)
        {
            return ArticleLog1.ItemID.CompareTo(ArticleLog2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of ArticleLog.
        /// </summary>
        public static int CompareByArticleID(ArticleLog ArticleLog1, ArticleLog ArticleLog2)
        {
            return ArticleLog1.ArticleID.CompareTo(ArticleLog2.ArticleID);
        }
        /// <summary>
        /// Compares 2 instances of ArticleLog.
        /// </summary>
        public static int CompareByUserID(ArticleLog ArticleLog1, ArticleLog ArticleLog2)
        {
            return ArticleLog1.UserID.CompareTo(ArticleLog2.UserID);
        }
        /// <summary>
        /// Compares 2 instances of ArticleLog.
        /// </summary>



        /// <summary>
        /// Compares 2 instances of ArticleLog.
        /// </summary>
        public static int CompareByComment(ArticleLog ArticleLog1, ArticleLog ArticleLog2)
        {
            return ArticleLog1.Comment.CompareTo(ArticleLog2.Comment);
        }

        #endregion


    }

}






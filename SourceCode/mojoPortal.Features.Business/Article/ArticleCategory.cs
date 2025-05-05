
// Author:					Mr Hậu
// Created:					2020-10-4
// Last Modified:			2020-10-4
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

    public class ArticleCategory
    {

        #region Constructors

        public ArticleCategory()
        { }


        public ArticleCategory(
            long itemID)
        {
            GetArticleCategory(
                itemID);
        }

        #endregion

        #region Private Properties

        private long itemID;
        private long articleID;
        private int siteID = -1;
        private int categoryId = -1;

        #endregion

        #region Public Properties
        public int CategoryID
        {
            get { return categoryId; }
            set { categoryId = value; }
        }
        public int SiteID
        {
            get { return siteID; }
            set { siteID = value; }
        }

        public long ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public long ArticleID
        {
            get { return articleID; }
            set { articleID = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of ArticleCategory.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetArticleCategory(
            long itemID)
        {
            using (IDataReader reader = DBArticleCategory.GetOne(
                itemID))
            {
                PopulateFromReader(reader);
            }

        }


        private void PopulateFromReader(IDataReader reader)
        {
            if (reader.Read())
            {
                this.itemID = Convert.ToInt64(reader["ItemID"]);
                this.articleID = Convert.ToInt64(reader["ArticleID"]);
                this.siteID = Convert.ToInt32(reader["SiteID"]);
                this.categoryId = Convert.ToInt32(reader["CategoryID"]);
            }

        }

        /// <summary>
        /// Persists a new instance of ArticleCategory. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBArticleCategory.Create(
                this.articleID,
                this.siteID,
                this.categoryId);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of ArticleCategory. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBArticleCategory.Update(
                this.itemID,
                this.articleID,
                this.siteID,
                this.categoryId);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of ArticleCategory. Returns true on success.
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
        /// Deletes an instance of ArticleCategory. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            long itemID)
        {
            return DBArticleCategory.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of ArticleCategory. 
        /// </summary>
        public static int GetCount()
        {
            return DBArticleCategory.GetCount();
        }

        private static List<ArticleCategory> LoadListFromReader(IDataReader reader)
        {
            List<ArticleCategory> articleCategoryList = new List<ArticleCategory>();
            try
            {
                while (reader.Read())
                {
                    ArticleCategory articleCategory = new ArticleCategory();
                    articleCategory.itemID = Convert.ToInt64(reader["ItemID"]);
                    articleCategory.articleID = Convert.ToInt64(reader["ArticleID"]);
                    if (!string.IsNullOrEmpty(reader["CategoryID"].ToString()))
                    {
                        articleCategory.categoryId = Convert.ToInt32(reader["CategoryID"]);
                    }
                    if (!string.IsNullOrEmpty(reader["SiteID"].ToString()))
                    {
                        articleCategory.siteID = Convert.ToInt32(reader["SiteID"]);
                    }
                    articleCategoryList.Add(articleCategory);

                }
            }
            finally
            {
                reader.Close();
            }

            return articleCategoryList;

        }
        public static List<ArticleCategory> GetList(long articleId)
        {
            IDataReader reader = DBArticleCategory.GetList(articleId);
            return LoadListFromReader(reader);
        }
        /// <summary>
        /// Gets an IList with all instances of ArticleCategory.
        /// </summary>
        public static List<ArticleCategory> GetAll()
        {
            IDataReader reader = DBArticleCategory.GetAll();
            return LoadListFromReader(reader);

        }
        public static bool DeleteAll(long articleID)
        {

            return DBArticleCategory.DeleteAll(articleID);
        }

        /// <summary>
        /// Gets an IList with page of instances of ArticleCategory.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<ArticleCategory> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBArticleCategory.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion




    }

}






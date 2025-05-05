
// Author:					Mr Hau
// Created:					2018-6-26
// Last Modified:			2018-6-26
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
using System.Linq;
using mojoPortal.Data;

namespace mojoPortal.Business
{

    public class CategoryUserArticle
    {

        #region Constructors

        public CategoryUserArticle()
        { }


        public CategoryUserArticle(
            int itemID)
        {
            GetCategoryUserArticle(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int userID = -1;
        private int categoryID = -1;
        private int typeRole = -1;
        private string categoryName = string.Empty;

        #endregion

        #region Public Properties
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }


        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }
        public int TypeRole
        {
            get { return typeRole; }
            set { typeRole = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of CategoryUserArticle.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetCategoryUserArticle(
            int itemID)
        {
            using (IDataReader reader = DBCategoryUserArticle.GetOne(
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
                this.userID = Convert.ToInt32(reader["UserID"]);
                this.categoryID = Convert.ToInt32(reader["CategoryID"]);
                this.typeRole = Convert.ToInt32(reader["TypeRole"]);

            }

        }

        /// <summary>
        /// Persists a new instance of CategoryUserArticle. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBCategoryUserArticle.Create(
                this.userID,
                this.categoryID,
                this.typeRole);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of CategoryUserArticle. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBCategoryUserArticle.Update(
                this.itemID,
                this.userID,
                this.categoryID,
                this.typeRole);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of CategoryUserArticle. Returns true on success.
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
        /// Deletes an instance of CategoryUserArticle. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBCategoryUserArticle.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of CategoryUserArticle. 
        /// </summary>
        public static int GetCount()
        {
            return DBCategoryUserArticle.GetCount();
        }
        public static string GetListItemByUser(int userId)
        {
            List<int> listItem = new List<int>();
            IDataReader reader = DBCategoryUserArticle.GetListByUser(userId);
            try
            {
                while (reader.Read())
                {
                    if (!string.IsNullOrEmpty(reader["CategoryID"].ToString()))
                    {
                        listItem.Add(Convert.ToInt32(reader["CategoryID"].ToString()));
                    }
                }
            }
            finally
            {

            }
            var result = listItem.Distinct().ToArray();
            return string.Join(",", result);
        }
        public static List<CategoryUserArticle> GetListItemByUserBO(int userId)
        {
            IDataReader reader = DBCategoryUserArticle.GetListByUser(userId);
            return LoadListFromReaderBO(reader);
        }


        private static List<CategoryUserArticle> LoadListFromReader(IDataReader reader)
        {
            List<CategoryUserArticle> categoryUserArticleList = new List<CategoryUserArticle>();
            try
            {
                while (reader.Read())
                {
                    CategoryUserArticle categoryUserArticle = new CategoryUserArticle();
                    categoryUserArticle.itemID = Convert.ToInt32(reader["ItemID"]);
                    categoryUserArticle.userID = Convert.ToInt32(reader["UserID"]);
                    categoryUserArticle.categoryID = Convert.ToInt32(reader["CategoryID"]);

                    if (!string.IsNullOrEmpty(reader["TypeRole"].ToString()))
                    {
                        categoryUserArticle.typeRole = Convert.ToInt32(reader["TypeRole"]);
                    }
                    categoryUserArticleList.Add(categoryUserArticle);

                }
            }
            finally
            {
                reader.Close();
            }

            return categoryUserArticleList;

        }

        private static List<CategoryUserArticle> LoadListFromReaderBO(IDataReader reader)
        {
            List<CategoryUserArticle> categoryUserArticleList = new List<CategoryUserArticle>();
            try
            {
                while (reader.Read())
                {
                    CategoryUserArticle categoryUserArticle = new CategoryUserArticle();
                    categoryUserArticle.itemID = Convert.ToInt32(reader["ItemID"]);
                    categoryUserArticle.userID = Convert.ToInt32(reader["UserID"]);
                    categoryUserArticle.categoryID = Convert.ToInt32(reader["CategoryID"]);
                    if (!string.IsNullOrEmpty(reader["TypeRole"].ToString()))
                    {
                        categoryUserArticle.typeRole = Convert.ToInt32(reader["TypeRole"]);
                    }

                    if (!string.IsNullOrEmpty(reader["CategoryName"].ToString()))
                    {
                        categoryUserArticle.categoryName = reader["CategoryName"].ToString();
                    }
                    categoryUserArticleList.Add(categoryUserArticle);

                }
            }
            finally
            {
                reader.Close();
            }

            return categoryUserArticleList;

        }

        /// <summary>
        /// Gets an IList with all instances of CategoryUserArticle.
        /// </summary>
        public static List<CategoryUserArticle> GetAll()
        {
            IDataReader reader = DBCategoryUserArticle.GetAll();
            return LoadListFromReader(reader);

        }

        public static List<int> GetCategoryIdByUser(int userID)
        {
            List<int> result = new List<int>();
            using (IDataReader reader = DBCategoryUserArticle.GetCategoryByUser(userID))
            {
                while (reader.Read())
                {
                    result.Add(Convert.ToInt32(reader["Category"]));
                }
            }
            return result;
        }

        public static List<CategoryUserArticle> GetSelected(int categoryId, int typeRole)
        {
            return LoadListFromReader(DBCategoryUserArticle.GetSelected(categoryId, typeRole));
        }

        /// <summary>
        /// Gets an IList with page of instances of CategoryUserArticle.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<CategoryUserArticle> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBCategoryUserArticle.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of CategoryUserArticle.
        /// </summary>
        public static int CompareByItemID(CategoryUserArticle categoryUserArticle1, CategoryUserArticle categoryUserArticle2)
        {
            return categoryUserArticle1.ItemID.CompareTo(categoryUserArticle2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of CategoryUserArticle.
        /// </summary>
        public static int CompareByUserID(CategoryUserArticle categoryUserArticle1, CategoryUserArticle categoryUserArticle2)
        {
            return categoryUserArticle1.UserID.CompareTo(categoryUserArticle2.UserID);
        }
        /// <summary>
        /// Compares 2 instances of CategoryUserArticle.
        /// </summary>
        public static int CompareByCategoryID(CategoryUserArticle categoryUserArticle1, CategoryUserArticle categoryUserArticle2)
        {
            return categoryUserArticle1.CategoryID.CompareTo(categoryUserArticle2.CategoryID);
        }
        /// <summary>
        /// Compares 2 instances of CategoryUserArticle.
        /// </summary>
        public static int CompareByTypeRole(CategoryUserArticle categoryUserArticle1, CategoryUserArticle categoryUserArticle2)
        {
            return categoryUserArticle1.TypeRole.CompareTo(categoryUserArticle2.TypeRole);
        }

        #endregion


    }

}






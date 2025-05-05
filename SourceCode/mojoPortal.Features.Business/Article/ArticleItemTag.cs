
// Author:					NAMDV
// Created:					2015-11-5
// Last Modified:			2015-11-5
// 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using ArticleFeature.Data;

namespace ArticleFeature.Business
{

    public class ArticleItemTag
    {

        #region Constructors

        public ArticleItemTag()
        { }


        public ArticleItemTag(
            int id)
        {
            GetArticleItemTag(
                id);
        }

        #endregion

        #region Private Properties

        private int iD = -1;
        private int moduleID = -1;
        private int itemID = -1;
        private int tagID = -1;
        private string name = string.Empty;
        #endregion

        #region Public Properties

        public int ID
        {
            get { return iD; }
            set { iD = value; }
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
        public int TagID
        {
            get { return tagID; }
            set { tagID = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of ArticleItemTag.
        /// </summary>
        /// <param name="id"> id </param>
        private void GetArticleItemTag(
            int id)
        {
            using (IDataReader reader = DBArticleItemTag.GetOne(
                id))
            {
                PopulateFromReader(reader);
            }

        }


        private void PopulateFromReader(IDataReader reader)
        {
            if (reader.Read())
            {
                this.iD = Convert.ToInt32(reader["ID"]);
                this.moduleID = Convert.ToInt32(reader["ModuleID"]);
                this.itemID = Convert.ToInt32(reader["ItemID"]);
                this.tagID = Convert.ToInt32(reader["TagID"]);

            }

        }

        /// <summary>
        /// Persists a new instance of ArticleItemTag. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBArticleItemTag.Create(
                this.moduleID,
                this.itemID,
                this.tagID);

            this.iD = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of ArticleItemTag. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBArticleItemTag.Update(
                this.iD,
                this.moduleID,
                this.itemID,
                this.tagID);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of ArticleItemTag. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        public bool Save()
        {
            if (this.iD > 0)
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
        /// Deletes an instance of ArticleItemTag. Returns true on success.
        /// </summary>
        /// <param name="id"> id </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int id)
        {
            return DBArticleItemTag.Delete(
                id);
        }


        /// <summary>
        /// Gets a count of ArticleItemTag. 
        /// </summary>
        public static int GetCount()
        {
            return DBArticleItemTag.GetCount();
        }

        private static List<ArticleItemTag> LoadListFromReader(IDataReader reader)
        {
            List<ArticleItemTag> articleItemTagList = new List<ArticleItemTag>();
            try
            {
                while (reader.Read())
                {
                    ArticleItemTag articleItemTag = new ArticleItemTag();
                    articleItemTag.iD = Convert.ToInt32(reader["ID"]);
                    articleItemTag.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    articleItemTag.itemID = Convert.ToInt32(reader["ItemID"]);
                    articleItemTag.tagID = Convert.ToInt32(reader["TagID"]);
                    if (!string.IsNullOrEmpty(reader["Name"].ToString()))
                    {
                        articleItemTag.Name = reader["Name"].ToString();
                    }
                    articleItemTagList.Add(articleItemTag);

                }
            }
            finally
            {
                reader.Close();
            }

            return articleItemTagList;

        }

        /// <summary>
        /// Gets an IList with all instances of ArticleItemTag.
        /// </summary>
        public static List<ArticleItemTag> GetAll()
        {
            IDataReader reader = DBArticleItemTag.GetAll();
            return LoadListFromReader(reader);

        }
        public static List<ArticleItemTag> GetAllByArticle(int articleID)
        {
            IDataReader reader = DBArticleItemTag.GetAllByArticle(articleID);
            return LoadListFromReader(reader);

        }

        public static bool DeleteAllByArticle(
           int articleID)
        {
            return DBArticleItemTag.DeleteAllByArticle(
                articleID);
        }
        /// <summary>
        /// Gets an IList with page of instances of ArticleItemTag.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<ArticleItemTag> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBArticleItemTag.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of ArticleItemTag.
        /// </summary>
        public static int CompareByID(ArticleItemTag articleItemTag1, ArticleItemTag articleItemTag2)
        {
            return articleItemTag1.ID.CompareTo(articleItemTag2.ID);
        }
        /// <summary>
        /// Compares 2 instances of ArticleItemTag.
        /// </summary>
        public static int CompareByModuleID(ArticleItemTag articleItemTag1, ArticleItemTag articleItemTag2)
        {
            return articleItemTag1.ModuleID.CompareTo(articleItemTag2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of ArticleItemTag.
        /// </summary>
        public static int CompareByItemID(ArticleItemTag articleItemTag1, ArticleItemTag articleItemTag2)
        {
            return articleItemTag1.ItemID.CompareTo(articleItemTag2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of ArticleItemTag.
        /// </summary>
        public static int CompareByTagID(ArticleItemTag articleItemTag1, ArticleItemTag articleItemTag2)
        {
            return articleItemTag1.TagID.CompareTo(articleItemTag2.TagID);
        }

        #endregion


    }

}






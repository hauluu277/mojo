
// Author:					NAMDV
// Created:					2015-9-21
// Last Modified:			2015-9-21


using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using ArticleFeature.Data;

namespace ArticleFeature.Business
{

    public class ArticleItemAttachment
    {

        #region Constructors

        public ArticleItemAttachment()
        { }


        public ArticleItemAttachment(
            int id)
        {
            GetArticleItemAttachment(
                id);
        }

        #endregion

        #region Private Properties

        private int iD = -1;
        private int moduleID = -1;
        private int itemID = -1;
        private int fileID = -1;

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
        public int FileID
        {
            get { return fileID; }
            set { fileID = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of ArticleItemAttachment.
        /// </summary>
        /// <param name="id"> id </param>
        private void GetArticleItemAttachment(
            int id)
        {
            using (IDataReader reader = DBArticleItemAttachment.GetOne(
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
                this.fileID = Convert.ToInt32(reader["FileID"]);

            }

        }

        /// <summary>
        /// Persists a new instance of ArticleItemAttachment. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBArticleItemAttachment.Create(
                this.moduleID,
                this.itemID,
                this.fileID);

            this.iD = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of ArticleItemAttachment. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBArticleItemAttachment.Update(
                this.iD,
                this.moduleID,
                this.itemID,
                this.fileID);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of ArticleItemAttachment. Returns true on success.
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
        /// Deletes an instance of ArticleItemAttachment. Returns true on success.
        /// </summary>
        /// <param name="id"> id </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int id)
        {
            return DBArticleItemAttachment.Delete(
                id);
        }


        /// <summary>
        /// Gets a count of ArticleItemAttachment. 
        /// </summary>
        public static int GetCount()
        {
            return DBArticleItemAttachment.GetCount();
        }

        private static List<ArticleItemAttachment> LoadListFromReader(IDataReader reader)
        {
            List<ArticleItemAttachment> articleItemAttachmentList = new List<ArticleItemAttachment>();
            try
            {
                while (reader.Read())
                {
                    ArticleItemAttachment articleItemAttachment = new ArticleItemAttachment();
                    articleItemAttachment.iD = Convert.ToInt32(reader["ID"]);
                    articleItemAttachment.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    articleItemAttachment.itemID = Convert.ToInt32(reader["ItemID"]);
                    articleItemAttachment.fileID = Convert.ToInt32(reader["FileID"]);
                    articleItemAttachmentList.Add(articleItemAttachment);

                }
            }
            finally
            {
                reader.Close();
            }

            return articleItemAttachmentList;

        }

        /// <summary>
        /// Gets an IList with all instances of ArticleItemAttachment.
        /// </summary>
        public static List<ArticleItemAttachment> GetAll()
        {
            IDataReader reader = DBArticleItemAttachment.GetAll();
            return LoadListFromReader(reader);

        }
        /// <summary>
        /// Gets an IList with all instances of ArticleItemAttachment By ServerFileName.
        /// </summary>
        /// <param name="ServerFileName">The ServerFileName</param>
        /// <returns></returns>
        public static List<ArticleItemAttachment> GetAllByServerFileName(string ServerFileName)
        {
            IDataReader reader = DBArticleItemAttachment.GetAllByServerFileName(ServerFileName);
            return LoadListFromReader(reader);
        }
        /// <summary>
        /// Gets an IList with all instances of ArticleItemAttachment By ItemID.
        /// </summary>
        /// <param name="ItemID">The ItemID</param>
        /// <returns></returns>
        public static List<ArticleItemAttachment> GetAllByItemID(int ItemID)
        {
            IDataReader reader = DBArticleItemAttachment.GetAllByItemID(ItemID);
            return LoadListFromReader(reader);
        }

        /// <summary>
        /// Gets an IList with all instances of ArticleItemAttachment By FileID.
        /// </summary>
        /// <param name="FileID">The FileID</param>
        /// <returns></returns>
        public static List<ArticleItemAttachment> GetAllByFileID(int FileID)
        {
            IDataReader reader = DBArticleItemAttachment.GetAllByFileID(FileID);
            return LoadListFromReader(reader);
        }

        /// <summary>
        /// Gets an IList with page of instances of ArticleItemAttachment.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<ArticleItemAttachment> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBArticleItemAttachment.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }

        /// <summary>
        /// Delete an IList with all instances of ArticleItemAttachment By ServerFileName.
        /// </summary>
        /// <param name="ServerFileName"></param>
        /// <returns></returns>
        public static bool DeleteItemAttachmentByServerFileName(string ServerFileName)
        {
            try
            {
                var listItem = GetAllByServerFileName(ServerFileName);
                if (listItem != null && listItem.Count > 0)
                {
                    foreach (var item in listItem)
                    {
                        Delete(item.ID);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of ArticleItemAttachment.
        /// </summary>
        public static int CompareByID(ArticleItemAttachment articleItemAttachment1, ArticleItemAttachment articleItemAttachment2)
        {
            return articleItemAttachment1.ID.CompareTo(articleItemAttachment2.ID);
        }
        /// <summary>
        /// Compares 2 instances of ArticleItemAttachment.
        /// </summary>
        public static int CompareByModuleID(ArticleItemAttachment articleItemAttachment1, ArticleItemAttachment articleItemAttachment2)
        {
            return articleItemAttachment1.ModuleID.CompareTo(articleItemAttachment2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of ArticleItemAttachment.
        /// </summary>
        public static int CompareByItemID(ArticleItemAttachment articleItemAttachment1, ArticleItemAttachment articleItemAttachment2)
        {
            return articleItemAttachment1.ItemID.CompareTo(articleItemAttachment2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of ArticleItemAttachment.
        /// </summary>
        public static int CompareByFileID(ArticleItemAttachment articleItemAttachment1, ArticleItemAttachment articleItemAttachment2)
        {
            return articleItemAttachment1.FileID.CompareTo(articleItemAttachment2.FileID);
        }

        #endregion


    }

}






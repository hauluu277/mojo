
// Author:					NamDV
// Created:					2015-9-21
// Last Modified:			2015-9-21


using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using ArticleFeature.Data;

namespace ArticleFeature.Business
{

    public class ArticleAttachment
    {

        #region Constructors

        public ArticleAttachment()
        { }


        public ArticleAttachment(
            int fileID)
        {
            GetArticleAttachment(
                fileID);
        }

        #endregion

        #region Private Properties

        private int fileID = -1;
        private int moduleID = -1;
        private string fileName = string.Empty;
        private string serverFileName = string.Empty;
        private int sizeInKB = -1;
        private int downloadCount = -1;
        private DateTime lastModified = DateTime.UtcNow;

        #endregion

        #region Public Properties

        public int FileID
        {
            get { return fileID; }
            set { fileID = value; }
        }
        public int ModuleID
        {
            get { return moduleID; }
            set { moduleID = value; }
        }
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        public string ServerFileName
        {
            get { return serverFileName; }
            set { serverFileName = value; }
        }
        public int SizeInKB
        {
            get { return sizeInKB; }
            set { sizeInKB = value; }
        }
        public int DownloadCount
        {
            get { return downloadCount; }
            set { downloadCount = value; }
        }
        public DateTime LastModified
        {
            get { return lastModified; }
            set { lastModified = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of ArticleAttachment.
        /// </summary>
        /// <param name="fileID"> fileID </param>
        private void GetArticleAttachment(
            int fileID)
        {
            using (IDataReader reader = DBArticleAttachment.GetOne(
                fileID))
            {
                PopulateFromReader(reader);
            }

        }


        private void PopulateFromReader(IDataReader reader)
        {
            if (reader.Read())
            {
                this.fileID = Convert.ToInt32(reader["FileID"]);
                this.moduleID = Convert.ToInt32(reader["ModuleID"]);
                this.fileName = reader["FileName"].ToString();
                this.serverFileName = reader["ServerFileName"].ToString();
                this.sizeInKB = Convert.ToInt32(reader["SizeInKB"]);
                this.downloadCount = Convert.ToInt32(reader["DownloadCount"]);
                this.lastModified = Convert.ToDateTime(reader["LastModified"]);

            }

        }

        /// <summary>
        /// Persists a new instance of ArticleAttachment. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBArticleAttachment.Create(
                this.moduleID,
                this.fileName,
                this.serverFileName,
                this.sizeInKB,
                this.downloadCount,
                this.lastModified);

            this.fileID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of ArticleAttachment. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBArticleAttachment.Update(
                this.fileID,
                this.moduleID,
                this.fileName,
                this.serverFileName,
                this.sizeInKB,
                this.downloadCount,
                this.lastModified);

        }


        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of ArticleAttachment. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        public bool Save()
        {
            if (this.fileID > 0)
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
        /// Deletes an instance of ArticleAttachment. Returns true on success.
        /// </summary>
        /// <param name="fileID"> fileID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int fileID)
        {
            return DBArticleAttachment.Delete(
                fileID);
        }


        /// <summary>
        /// Gets a count of ArticleAttachment. 
        /// </summary>
        public static int GetCount()
        {
            return DBArticleAttachment.GetCount();
        }

        private static List<ArticleAttachment> LoadListFromReader(IDataReader reader)
        {
            List<ArticleAttachment> articleAttachmentList = new List<ArticleAttachment>();
            try
            {
                while (reader.Read())
                {
                    ArticleAttachment articleAttachment = new ArticleAttachment();
                    articleAttachment.fileID = Convert.ToInt32(reader["FileID"]);
                    articleAttachment.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    articleAttachment.fileName = reader["FileName"].ToString();
                    articleAttachment.serverFileName = reader["ServerFileName"].ToString();
                    articleAttachment.sizeInKB = Convert.ToInt32(reader["SizeInKB"]);
                    articleAttachment.downloadCount = Convert.ToInt32(reader["DownloadCount"]);
                    articleAttachment.lastModified = Convert.ToDateTime(reader["LastModified"]);
                    articleAttachmentList.Add(articleAttachment);

                }
            }
            finally
            {
                reader.Close();
            }

            return articleAttachmentList;

        }

        private static List<ArticleAttachmentBO> LoadListBOFromReader(IDataReader reader)
        {
            List<ArticleAttachmentBO> articleAttachmentList = new List<ArticleAttachmentBO>();
            try
            {
                while (reader.Read())
                {
                    ArticleAttachmentBO articleAttachment = new ArticleAttachmentBO();
                    articleAttachment.FileID = Convert.ToInt32(reader["FileID"]);
                    articleAttachment.ModuleID = Convert.ToInt32(reader["ModuleID"]);
                    articleAttachment.FileName = reader["FileName"].ToString();
                    articleAttachment.ServerFileName = reader["ServerFileName"].ToString();
                    articleAttachment.SizeInKB = Convert.ToInt32(reader["SizeInKB"]);
                    articleAttachment.DownloadCount = Convert.ToInt32(reader["DownloadCount"]);
                    articleAttachment.ID = Convert.ToInt32(reader["ID"]);
                    articleAttachment.ItemID = Convert.ToInt32(reader["ItemID"]);
                    articleAttachment.LastModified = Convert.ToDateTime(reader["LastModified"]);
                    articleAttachmentList.Add(articleAttachment);
                }
            }
            finally
            {
                reader.Close();
            }

            return articleAttachmentList;

        }

        /// <summary>
        /// Gets an IList with all instances of ArticleAttachment.
        /// </summary>
        public static List<ArticleAttachment> GetAll()
        {
            IDataReader reader = DBArticleAttachment.GetAll();
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with all instances of ArticleAttachment by ServerFileName.
        /// </summary>
        /// <param name="ServerFileName">The ServerFileName</param>
        /// <returns></returns>
        public static List<ArticleAttachment> GetAllByServerFileName(string ServerFileName)
        {
            IDataReader reader = DBArticleAttachment.GetAllByServerFileName(ServerFileName);
            return LoadListFromReader(reader);
        }

        /// <summary>
        /// Gets an IList with all instances of ArticleAttachment by ItemID.
        /// </summary>
        /// <param name="ItemID">The ItemID</param>
        /// <returns></returns>
        public static List<ArticleAttachmentBO> GetAllObjectByItemID(int ItemID)
        {
            IDataReader reader = DBArticleAttachment.GetAllObjectByItemID(ItemID);
            return LoadListBOFromReader(reader);
        }

        /// <summary>
        /// Gets an IList with page of instances of ArticleAttachment.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<ArticleAttachment> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBArticleAttachment.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }

        /// <summary>
        /// Delete an IList with all instances of ArticleItemAttachment By ServerFileName.
        /// </summary>
        /// <param name="ServerFileName"></param>
        /// <returns></returns>
        public static bool DeleteAttachmentByServerFileName(string ServerFileName)
        {
            try
            {
                var listItem = GetAllByServerFileName(ServerFileName);
                if (listItem != null && listItem.Count > 0)
                {
                    foreach (var item in listItem)
                    {
                        Delete(item.FileID);
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
        /// Compares 2 instances of ArticleAttachment.
        /// </summary>
        public static int CompareByFileID(ArticleAttachment articleAttachment1, ArticleAttachment articleAttachment2)
        {
            return articleAttachment1.FileID.CompareTo(articleAttachment2.FileID);
        }
        /// <summary>
        /// Compares 2 instances of ArticleAttachment.
        /// </summary>
        public static int CompareByModuleID(ArticleAttachment articleAttachment1, ArticleAttachment articleAttachment2)
        {
            return articleAttachment1.ModuleID.CompareTo(articleAttachment2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of ArticleAttachment.
        /// </summary>
        public static int CompareByFileName(ArticleAttachment articleAttachment1, ArticleAttachment articleAttachment2)
        {
            return articleAttachment1.FileName.CompareTo(articleAttachment2.FileName);
        }
        /// <summary>
        /// Compares 2 instances of ArticleAttachment.
        /// </summary>
        public static int CompareByServerFileName(ArticleAttachment articleAttachment1, ArticleAttachment articleAttachment2)
        {
            return articleAttachment1.ServerFileName.CompareTo(articleAttachment2.ServerFileName);
        }
        /// <summary>
        /// Compares 2 instances of ArticleAttachment.
        /// </summary>
        public static int CompareBySizeInKB(ArticleAttachment articleAttachment1, ArticleAttachment articleAttachment2)
        {
            return articleAttachment1.SizeInKB.CompareTo(articleAttachment2.SizeInKB);
        }
        /// <summary>
        /// Compares 2 instances of ArticleAttachment.
        /// </summary>
        public static int CompareByDownloadCount(ArticleAttachment articleAttachment1, ArticleAttachment articleAttachment2)
        {
            return articleAttachment1.DownloadCount.CompareTo(articleAttachment2.DownloadCount);
        }
        /// <summary>
        /// Compares 2 instances of ArticleAttachment.
        /// </summary>
        public static int CompareByLastModified(ArticleAttachment articleAttachment1, ArticleAttachment articleAttachment2)
        {
            return articleAttachment1.LastModified.CompareTo(articleAttachment2.LastModified);
        }

        #endregion


    }

}






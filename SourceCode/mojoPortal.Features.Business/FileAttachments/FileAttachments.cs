
// Author:					Mr Hậu
// Created:					2020-8-3
// Last Modified:			2020-8-3
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

    public class FileAttachments
    {

        #region Constructors

        public FileAttachments()
        { }


        public FileAttachments(
            int itemID)
        {
            GetFileAttachments(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int objectID = -1;
        private int typeItem = -1;
        private string fileName = string.Empty;
        private string filePath = string.Empty;
        private string fileExtensions = string.Empty;
        private int downloadCount = -1;
        private DateTime createdDate = DateTime.UtcNow;

        #endregion

        #region Public Properties

        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public int ObjectID
        {
            get { return objectID; }
            set { objectID = value; }
        }
        public int TypeItem
        {
            get { return typeItem; }
            set { typeItem = value; }
        }
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }
        public string FileExtensions
        {
            get { return fileExtensions; }
            set { fileExtensions = value; }
        }
        public int DownloadCount
        {
            get { return downloadCount; }
            set { downloadCount = value; }
        }
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of FileAttachments.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetFileAttachments(
            int itemID)
        {
            using (IDataReader reader = DBFileAttachments.GetOne(
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
                this.objectID = Convert.ToInt32(reader["ObjectID"]);
                this.typeItem = Convert.ToInt32(reader["TypeItem"]);
                this.fileName = reader["FileName"].ToString();
                this.filePath = reader["FilePath"].ToString();
                this.fileExtensions = reader["FileExtensions"].ToString();
                this.downloadCount = Convert.ToInt32(reader["DownloadCount"]);
                this.createdDate = Convert.ToDateTime(reader["CreatedDate"]);

            }

        }

        /// <summary>
        /// Persists a new instance of FileAttachments. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBFileAttachments.Create(
                this.objectID,
                this.typeItem,
                this.fileName,
                this.filePath,
                this.fileExtensions,
                this.downloadCount,
                this.createdDate);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of FileAttachments. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBFileAttachments.Update(
                this.itemID,
                this.objectID,
                this.typeItem,
                this.fileName,
                this.filePath,
                this.fileExtensions,
                this.downloadCount,
                this.createdDate);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of FileAttachments. Returns true on success.
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
        /// Deletes an instance of FileAttachments. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBFileAttachments.Delete(
                itemID);
        }

        public static bool DeleteByObject(
          int objectID)
        {
            return DBFileAttachments.DeleteByObject(objectID);
        }


        /// <summary>
        /// Gets a count of FileAttachments. 
        /// </summary>
        public static int GetCount()
        {
            return DBFileAttachments.GetCount();
        }

        private static List<FileAttachments> LoadListFromReader(IDataReader reader)
        {
            List<FileAttachments> FileAttachmentsList = new List<FileAttachments>();
            try
            {
                while (reader.Read())
                {
                    FileAttachments FileAttachments = new FileAttachments();
                    FileAttachments.itemID = Convert.ToInt32(reader["ItemID"]);
                    FileAttachments.objectID = Convert.ToInt32(reader["ObjectID"]);
                    FileAttachments.typeItem = Convert.ToInt32(reader["TypeItem"]);
                    FileAttachments.fileName = reader["FileName"].ToString();
                    FileAttachments.filePath = reader["FilePath"].ToString();
                    FileAttachments.fileExtensions = reader["FileExtensions"].ToString();
                    FileAttachments.downloadCount = Convert.ToInt32(reader["DownloadCount"]);
                    FileAttachments.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    FileAttachmentsList.Add(FileAttachments);

                }
            }
            finally
            {
                reader.Close();
            }

            return FileAttachmentsList;

        }

        /// <summary>
        /// Gets an IList with all instances of FileAttachments.
        /// </summary>
        public static List<FileAttachments> GetAll()
        {
            IDataReader reader = DBFileAttachments.GetAll();
            return LoadListFromReader(reader);

        }
        public static List<FileAttachments> GetByObject(int objectId)
        {
            IDataReader reader = DBFileAttachments.GetByObject(objectId);
            return LoadListFromReader(reader);

        }
        /// <summary>
        /// Gets an IList with page of instances of FileAttachments.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<FileAttachments> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBFileAttachments.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of FileAttachments.
        /// </summary>
        public static int CompareByItemID(FileAttachments FileAttachments1, FileAttachments FileAttachments2)
        {
            return FileAttachments1.ItemID.CompareTo(FileAttachments2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of FileAttachments.
        /// </summary>
        public static int CompareByObjectID(FileAttachments FileAttachments1, FileAttachments FileAttachments2)
        {
            return FileAttachments1.ObjectID.CompareTo(FileAttachments2.ObjectID);
        }
        /// <summary>
        /// Compares 2 instances of FileAttachments.
        /// </summary>
        public static int CompareByTypeItem(FileAttachments FileAttachments1, FileAttachments FileAttachments2)
        {
            return FileAttachments1.TypeItem.CompareTo(FileAttachments2.TypeItem);
        }
        /// <summary>
        /// Compares 2 instances of FileAttachments.
        /// </summary>
        public static int CompareByFileName(FileAttachments FileAttachments1, FileAttachments FileAttachments2)
        {
            return FileAttachments1.FileName.CompareTo(FileAttachments2.FileName);
        }
        /// <summary>
        /// Compares 2 instances of FileAttachments.
        /// </summary>
        public static int CompareByFilePath(FileAttachments FileAttachments1, FileAttachments FileAttachments2)
        {
            return FileAttachments1.FilePath.CompareTo(FileAttachments2.FilePath);
        }
        /// <summary>
        /// Compares 2 instances of FileAttachments.
        /// </summary>
        public static int CompareByFileExtensions(FileAttachments FileAttachments1, FileAttachments FileAttachments2)
        {
            return FileAttachments1.FileExtensions.CompareTo(FileAttachments2.FileExtensions);
        }
        /// <summary>
        /// Compares 2 instances of FileAttachments.
        /// </summary>
        public static int CompareByDownloadCount(FileAttachments FileAttachments1, FileAttachments FileAttachments2)
        {
            return FileAttachments1.DownloadCount.CompareTo(FileAttachments2.DownloadCount);
        }
        /// <summary>
        /// Compares 2 instances of FileAttachments.
        /// </summary>
        public static int CompareByCreatedDate(FileAttachments FileAttachments1, FileAttachments FileAttachments2)
        {
            return FileAttachments1.CreatedDate.CompareTo(FileAttachments2.CreatedDate);
        }

        #endregion


    }

}






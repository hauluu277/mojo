
// Author:					hauld
// Created:					2023-7-6
// Last Modified:			2023-7-6
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

    public class md_AudioFile
    {

        #region Constructors

        public md_AudioFile()
        { }


        public md_AudioFile(
            int itemID)
        {
            Getmd_AudioFile(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int moduleID = -1;
        private int siteID = -1;
        private int audioAlbumID = -1;
        private string nameFile = string.Empty;
        private string description = string.Empty;
        private string itemUrl = string.Empty;
        private string filePath = string.Empty;
        private bool isPublish = false;
        private Guid userGuid = Guid.Empty;
        private string createdByUser = string.Empty;
        private DateTime createdDate = DateTime.UtcNow;
        private Guid lastModUserGuid = Guid.Empty;
        private DateTime lastModUtc = DateTime.UtcNow;

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
        public int AudioAlbumID
        {
            get { return audioAlbumID; }
            set { audioAlbumID = value; }
        }
        public string NameFile
        {
            get { return nameFile; }
            set { nameFile = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string ItemUrl
        {
            get { return itemUrl; }
            set { itemUrl = value; }
        }
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }
        public bool IsPublish
        {
            get { return isPublish; }
            set { isPublish = value; }
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

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of md_AudioFile.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void Getmd_AudioFile(
            int itemID)
        {
            using (IDataReader reader = DBmd_AudioFiles.GetOne(
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
                this.audioAlbumID = Convert.ToInt32(reader["AudioAlbumID"]);
                this.nameFile = reader["NameFile"].ToString();
                this.description = reader["Description"].ToString();
                this.itemUrl = reader["ItemUrl"].ToString();
                this.filePath = reader["FilePath"].ToString();
                this.isPublish = Convert.ToBoolean(reader["IsPublish"]);
                this.userGuid = new Guid(reader["UserGuid"].ToString());
                this.createdByUser = reader["CreatedByUser"].ToString();
                this.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                this.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                this.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);

            }

        }

        /// <summary>
        /// Persists a new instance of md_AudioFile. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBmd_AudioFiles.Create(
                this.moduleID,
                this.siteID,
                this.audioAlbumID,
                this.nameFile,
                this.description,
                this.itemUrl,
                this.filePath,
                this.isPublish,
                this.userGuid,
                this.createdByUser,
                this.createdDate,
                this.lastModUserGuid,
                this.lastModUtc);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of md_AudioFile. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBmd_AudioFiles.Update(
                this.itemID,
                this.moduleID,
                this.siteID,
                this.audioAlbumID,
                this.nameFile,
                this.description,
                this.itemUrl,
                this.filePath,
                this.isPublish,
                this.userGuid,
                this.createdByUser,
                this.createdDate,
                this.lastModUserGuid,
                this.lastModUtc);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of md_AudioFile. Returns true on success.
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
        /// Deletes an instance of md_AudioFile. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBmd_AudioFiles.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of md_AudioFile. 
        /// </summary>
        public static int GetCount()
        {
            return DBmd_AudioFiles.GetCount();
        }

        private static List<md_AudioFile> LoadListFromReader(IDataReader reader)
        {
            List<md_AudioFile> md_AudioFileList = new List<md_AudioFile>();
            try
            {
                while (reader.Read())
                {
                    md_AudioFile md_AudioFile = new md_AudioFile();
                    md_AudioFile.itemID = Convert.ToInt32(reader["ItemID"]);
                    md_AudioFile.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    md_AudioFile.siteID = Convert.ToInt32(reader["SiteID"]);
                    md_AudioFile.audioAlbumID = Convert.ToInt32(reader["AudioAlbumID"]);
                    md_AudioFile.nameFile = reader["NameFile"].ToString();
                    md_AudioFile.description = reader["Description"].ToString();
                    md_AudioFile.itemUrl = reader["ItemUrl"].ToString();
                    md_AudioFile.filePath = reader["FilePath"].ToString();
                    md_AudioFile.isPublish = Convert.ToBoolean(reader["IsPublish"]);
                    md_AudioFile.userGuid = new Guid(reader["UserGuid"].ToString());
                    md_AudioFile.createdByUser = reader["CreatedByUser"].ToString();
                    md_AudioFile.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    md_AudioFile.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                    md_AudioFile.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                    md_AudioFileList.Add(md_AudioFile);

                }
            }
            finally
            {
                reader.Close();
            }

            return md_AudioFileList;

        }

        /// <summary>
        /// Gets an IList with all instances of md_AudioFile.
        /// </summary>
        public static List<md_AudioFile> GetAll()
        {
            IDataReader reader = DBmd_AudioFiles.GetAll();
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of md_AudioFile.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<md_AudioFile> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBmd_AudioFiles.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of md_AudioFile.
        /// </summary>
        public static int CompareByItemID(md_AudioFile md_AudioFile1, md_AudioFile md_AudioFile2)
        {
            return md_AudioFile1.ItemID.CompareTo(md_AudioFile2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioFile.
        /// </summary>
        public static int CompareByModuleID(md_AudioFile md_AudioFile1, md_AudioFile md_AudioFile2)
        {
            return md_AudioFile1.ModuleID.CompareTo(md_AudioFile2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioFile.
        /// </summary>
        public static int CompareBySiteID(md_AudioFile md_AudioFile1, md_AudioFile md_AudioFile2)
        {
            return md_AudioFile1.SiteID.CompareTo(md_AudioFile2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioFile.
        /// </summary>
        public static int CompareByAudioAlbumID(md_AudioFile md_AudioFile1, md_AudioFile md_AudioFile2)
        {
            return md_AudioFile1.AudioAlbumID.CompareTo(md_AudioFile2.AudioAlbumID);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioFile.
        /// </summary>
        public static int CompareByNameFile(md_AudioFile md_AudioFile1, md_AudioFile md_AudioFile2)
        {
            return md_AudioFile1.NameFile.CompareTo(md_AudioFile2.NameFile);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioFile.
        /// </summary>
        public static int CompareByDescription(md_AudioFile md_AudioFile1, md_AudioFile md_AudioFile2)
        {
            return md_AudioFile1.Description.CompareTo(md_AudioFile2.Description);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioFile.
        /// </summary>
        public static int CompareByItemUrl(md_AudioFile md_AudioFile1, md_AudioFile md_AudioFile2)
        {
            return md_AudioFile1.ItemUrl.CompareTo(md_AudioFile2.ItemUrl);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioFile.
        /// </summary>
        public static int CompareByFilePath(md_AudioFile md_AudioFile1, md_AudioFile md_AudioFile2)
        {
            return md_AudioFile1.FilePath.CompareTo(md_AudioFile2.FilePath);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioFile.
        /// </summary>
        public static int CompareByCreatedByUser(md_AudioFile md_AudioFile1, md_AudioFile md_AudioFile2)
        {
            return md_AudioFile1.CreatedByUser.CompareTo(md_AudioFile2.CreatedByUser);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioFile.
        /// </summary>
        public static int CompareByCreatedDate(md_AudioFile md_AudioFile1, md_AudioFile md_AudioFile2)
        {
            return md_AudioFile1.CreatedDate.CompareTo(md_AudioFile2.CreatedDate);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioFile.
        /// </summary>
        public static int CompareByLastModUtc(md_AudioFile md_AudioFile1, md_AudioFile md_AudioFile2)
        {
            return md_AudioFile1.LastModUtc.CompareTo(md_AudioFile2.LastModUtc);
        }

        #endregion


    }

}






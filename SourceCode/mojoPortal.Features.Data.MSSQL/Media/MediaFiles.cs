
// Author:					HAULD
// Created:					2015-10-26
// Last Modified:			2015-10-26
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
using MediaFilesFeature.Data;

namespace MediaFilesFeature.Business
{

    public class MediaFiles
    {

        #region Constructors

        public MediaFiles()
        { }


        public MediaFiles(
            int itemID)
        {
            GetMediaFiles(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int moduleID = -1;
        private int siteID = -1;
        private int mediaAlbumID = -1;
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
        public int MediaAlbumID
        {
            get { return mediaAlbumID; }
            set { mediaAlbumID = value; }
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
        /// Gets an instance of MediaFiles.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetMediaFiles(
            int itemID)
        {
            using (IDataReader reader = DBMediaFiles.GetOne(
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
                this.mediaAlbumID = Convert.ToInt32(reader["MediaAlbumID"]);
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
        /// Persists a new instance of MediaFiles. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBMediaFiles.Create(
                this.moduleID,
                this.siteID,
                this.mediaAlbumID,
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
        /// Updates this instance of MediaFiles. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBMediaFiles.Update(
                this.itemID,
                this.moduleID,
                this.siteID,
                this.mediaAlbumID,
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
        /// Saves this instance of MediaFiles. Returns true on success.
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
        /// Deletes an instance of MediaFiles. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBMediaFiles.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of MediaFiles. 
        /// </summary>
        public static int GetCount()
        {
            return DBMediaFiles.GetCount();
        }

        private static List<MediaFiles> LoadListFromReader(IDataReader reader)
        {
            List<MediaFiles> MediaFilesList = new List<MediaFiles>();
            try
            {
                while (reader.Read())
                {
                    MediaFiles MediaFiles = new MediaFiles();
                    MediaFiles.itemID = Convert.ToInt32(reader["ItemID"]);
                    MediaFiles.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    MediaFiles.siteID = Convert.ToInt32(reader["SiteID"]);
                    MediaFiles.mediaAlbumID = Convert.ToInt32(reader["MediaAlbumID"]);
                    MediaFiles.nameFile = reader["NameFile"].ToString();
                    MediaFiles.description = reader["Description"].ToString();
                    MediaFiles.itemUrl = reader["ItemUrl"].ToString();
                    MediaFiles.filePath = reader["FilePath"].ToString();
                    MediaFiles.isPublish = Convert.ToBoolean(reader["IsPublish"]);
                    MediaFiles.userGuid = new Guid(reader["UserGuid"].ToString());
                    MediaFiles.createdByUser = reader["CreatedByUser"].ToString();
                    MediaFiles.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    MediaFiles.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                    MediaFiles.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                    MediaFilesList.Add(MediaFiles);

                }
            }
            finally
            {
                reader.Close();
            }

            return MediaFilesList;

        }

        /// <summary>
        /// Gets an IList with all instances of MediaFiles.
        /// </summary>
        public static List<MediaFiles> GetAll()
        {
            IDataReader reader = DBMediaFiles.GetAll();
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of MediaFiles.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<MediaFiles> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBMediaFiles.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of MediaFiles.
        /// </summary>
        public static int CompareByItemID(MediaFiles MediaFiles1, MediaFiles MediaFiles2)
        {
            return MediaFiles1.ItemID.CompareTo(MediaFiles2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of MediaFiles.
        /// </summary>
        public static int CompareByModuleID(MediaFiles MediaFiles1, MediaFiles MediaFiles2)
        {
            return MediaFiles1.ModuleID.CompareTo(MediaFiles2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of MediaFiles.
        /// </summary>
        public static int CompareBySiteID(MediaFiles MediaFiles1, MediaFiles MediaFiles2)
        {
            return MediaFiles1.SiteID.CompareTo(MediaFiles2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of MediaFiles.
        /// </summary>
        public static int CompareByMediaAlbumID(MediaFiles MediaFiles1, MediaFiles MediaFiles2)
        {
            return MediaFiles1.MediaAlbumID.CompareTo(MediaFiles2.MediaAlbumID);
        }
        /// <summary>
        /// Compares 2 instances of MediaFiles.
        /// </summary>
        public static int CompareByNameFile(MediaFiles MediaFiles1, MediaFiles MediaFiles2)
        {
            return MediaFiles1.NameFile.CompareTo(MediaFiles2.NameFile);
        }
        /// <summary>
        /// Compares 2 instances of MediaFiles.
        /// </summary>
        public static int CompareByDescription(MediaFiles MediaFiles1, MediaFiles MediaFiles2)
        {
            return MediaFiles1.Description.CompareTo(MediaFiles2.Description);
        }
        /// <summary>
        /// Compares 2 instances of MediaFiles.
        /// </summary>
        public static int CompareByItemUrl(MediaFiles MediaFiles1, MediaFiles MediaFiles2)
        {
            return MediaFiles1.ItemUrl.CompareTo(MediaFiles2.ItemUrl);
        }
        /// <summary>
        /// Compares 2 instances of MediaFiles.
        /// </summary>
        public static int CompareByFilePath(MediaFiles MediaFiles1, MediaFiles MediaFiles2)
        {
            return MediaFiles1.FilePath.CompareTo(MediaFiles2.FilePath);
        }
        /// <summary>
        /// Compares 2 instances of MediaFiles.
        /// </summary>
        public static int CompareByCreatedByUser(MediaFiles MediaFiles1, MediaFiles MediaFiles2)
        {
            return MediaFiles1.CreatedByUser.CompareTo(MediaFiles2.CreatedByUser);
        }
        /// <summary>
        /// Compares 2 instances of MediaFiles.
        /// </summary>
        public static int CompareByCreatedDate(MediaFiles MediaFiles1, MediaFiles MediaFiles2)
        {
            return MediaFiles1.CreatedDate.CompareTo(MediaFiles2.CreatedDate);
        }
        /// <summary>
        /// Compares 2 instances of MediaFiles.
        /// </summary>
        public static int CompareByLastModUtc(MediaFiles MediaFiles1, MediaFiles MediaFiles2)
        {
            return MediaFiles1.LastModUtc.CompareTo(MediaFiles2.LastModUtc);
        }

        #endregion


    }

}






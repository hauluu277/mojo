
// Author:					HAULD
// Created:					2015-10-27
// Last Modified:			2015-10-27
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
using MediaAlbumFeature.Data;

namespace MediaAlbumFeature.Business
{

    public class MediaAlbum
    {

        #region Constructors

        public MediaAlbum()
        { }


        public MediaAlbum(
            int itemID)
        {
            GetMediaAlbum(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int moduleID = -1;
        private int siteID = -1;
        private int groupMediaID = -1;
        private int categoryID = -1;
        private string fileName = string.Empty;
        private string description = string.Empty;
        private string itemUrl = string.Empty;
        private int totalView = 0;
        private int sizeInKB = 0;
        private int typeData = -1;
        private string filePath = string.Empty;
        private string imageVideo = string.Empty;
        private bool featured = false;
        private bool isPublish = false;
        private Guid userGuid = Guid.Empty;
        private string createdByUser = string.Empty;
        private DateTime createdDate = DateTime.UtcNow;
        private Guid lastModUserGuid = Guid.Empty;
        private DateTime lastModUtc = DateTime.UtcNow;
        private string categoryName = string.Empty;
        private string groupName = string.Empty;
        private int albumOrder = 0;
        private string embedCode = string.Empty;
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
        public int GroupMediaID
        {
            get { return groupMediaID; }
            set { groupMediaID = value; }
        }
        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
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
        public int TotalView
        {
            get { return totalView; }
            set { totalView = value; }
        }
        public int SizeInKB
        {
            get { return sizeInKB; }
            set { sizeInKB = value; }
        }
        public int TypeData
        {
            get { return typeData; }
            set { typeData = value; }
        }
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }
        public string ImageVideo
        {
            get { return imageVideo; }
            set { imageVideo = value; }
        }
        public bool Featured
        {
            get { return featured; }
            set { featured = value; }
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
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }
        public string GroupName
        {
            get { return groupName; }
            set { groupName = value; }
        }
        public int AlbumOrder
        {
            get { return albumOrder; }
            set { albumOrder = value; }
        }
        public string EmbedCode
        {
            get { return embedCode; }
            set { embedCode = value; }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of MediaAlbum.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetMediaAlbum(
            int itemID)
        {
            using (IDataReader reader = DBMediaAlbum.GetOne(
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
                this.groupMediaID = Convert.ToInt32(reader["GroupMediaID"]);
                //this.categoryID = Convert.ToInt32(reader["CategoryID"]);
                this.fileName = reader["FileName"].ToString();
                this.description = reader["Description"].ToString();
                this.itemUrl = reader["ItemUrl"].ToString();
                this.totalView = Convert.ToInt32(reader["TotalView"]);
                this.sizeInKB = Convert.ToInt32(reader["SizeInKB"]);
                this.typeData = Convert.ToInt32(reader["TypeData"]);
                this.filePath = reader["FilePath"].ToString();
                if (!String.IsNullOrEmpty(reader["ImageVideo"].ToString()))
                {
                    this.imageVideo = reader["ImageVideo"].ToString();
                }
                this.featured = Convert.ToBoolean(reader["Featured"]);
                this.isPublish = Convert.ToBoolean(reader["IsPublish"]);
                if (!String.IsNullOrEmpty(reader["UserGuid"].ToString()))
                {
                    this.userGuid = new Guid(reader["UserGuid"].ToString());
                }
                if (!String.IsNullOrEmpty(reader["CreatedByUser"].ToString()))
                {
                    this.createdByUser = reader["CreatedByUser"].ToString();
                }
                if (!String.IsNullOrEmpty(reader["CreatedDate"].ToString()))
                {
                    this.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                }
                if (!String.IsNullOrEmpty(reader["LastModUserGuid"].ToString()))
                {
                    this.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                }
                if (!String.IsNullOrEmpty(reader["LastModUtc"].ToString()))
                {
                    this.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                }
                if (!String.IsNullOrEmpty(reader["AlbumOrder"].ToString()))
                {
                    this.albumOrder = Convert.ToInt32(reader["AlbumOrder"]);
                }
                if (reader["EmbedCode"] != DBNull.Value)
                {
                    this.embedCode = reader["EmbedCode"].ToString();
                }
            }

        }

        /// <summary>
        /// Persists a new instance of MediaAlbum. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBMediaAlbum.Create(
                this.moduleID,
                this.siteID,
                this.groupMediaID,
                this.fileName,
                this.description,
                this.itemUrl,
                this.totalView,
                this.sizeInKB,
                this.typeData,
                this.filePath,
                this.imageVideo,
                this.featured,
                this.isPublish,
                this.userGuid,
                this.createdByUser,
                this.createdDate,
                this.albumOrder,
                this.embedCode);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of MediaAlbum. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBMediaAlbum.Update(
                this.itemID,
                this.moduleID,
                this.siteID,
                this.groupMediaID,
                this.fileName,
                this.description,
                this.itemUrl,
                this.totalView,
                this.sizeInKB,
                this.typeData,
                this.filePath,
                this.imageVideo,
                this.featured,
                this.isPublish,
                this.lastModUserGuid,
                this.lastModUtc,
                this.albumOrder,
                this.embedCode);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of MediaAlbum. Returns true on success.
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

        public static bool UpdateViews(int itemID)
        {
            return DBMediaAlbum.UppdateViews(itemID);
        }


        #endregion

        #region Static Methods

        /// <summary>
        /// Deletes an instance of MediaAlbum. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBMediaAlbum.Delete(
                itemID);
        }

        public static bool DeleteByGroupID(int itemID)
        {
            return DBMediaAlbum.DeleteByGroupID(itemID);
        }
        /// <summary>
        /// Gets a count of MediaAlbum. 
        /// </summary>
        public static int GetCount(int siteID, int moduleID, int groupMediaID, int featured, bool? publish, string keyword)
        {
            return DBMediaAlbum.GetCount(siteID, moduleID, groupMediaID, featured, publish, keyword);
        }

        private static List<MediaAlbum> LoadListFormReader2(IDataReader reader)
        {
            List<MediaAlbum> mediaAlbumList = new List<MediaAlbum>();
            try
            {
                while (reader.Read())
                {
                    MediaAlbum mediaAlbum = new MediaAlbum();
                    mediaAlbum.itemID = Convert.ToInt32(reader["ItemID"]);
                    mediaAlbum.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    mediaAlbum.siteID = Convert.ToInt32(reader["SiteID"]);
                    mediaAlbum.groupMediaID = Convert.ToInt32(reader["GroupMediaID"]);
                    mediaAlbum.fileName = reader["FileName"].ToString();
                    mediaAlbum.description = reader["Description"].ToString();
                    mediaAlbum.itemUrl = reader["ItemUrl"].ToString();
                    mediaAlbum.totalView = Convert.ToInt32(reader["TotalView"]);
                    mediaAlbum.sizeInKB = Convert.ToInt32(reader["SizeInKB"]);
                    mediaAlbum.typeData = Convert.ToInt32(reader["TypeData"]);
                    mediaAlbum.filePath = reader["FilePath"].ToString();
                    mediaAlbum.featured = Convert.ToBoolean(reader["Featured"]);
                    if (!String.IsNullOrEmpty(reader["ImageVideo"].ToString()))
                    {
                        mediaAlbum.imageVideo = reader["ImageVideo"].ToString();
                    }
                    mediaAlbum.isPublish = Convert.ToBoolean(reader["IsPublish"]);
                    mediaAlbum.userGuid = new Guid(reader["UserGuid"].ToString());
                    mediaAlbum.createdByUser = reader["CreatedByUser"].ToString();
                    if (!String.IsNullOrEmpty(reader["CreatedDate"].ToString()))
                    {
                        mediaAlbum.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    }
                    if (!String.IsNullOrEmpty(reader["LastModUserGuid"].ToString()))
                    {
                        mediaAlbum.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                    }
                    if (!String.IsNullOrEmpty(reader["LastModUtc"].ToString()))
                    {
                        mediaAlbum.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                    }
                    //if (!String.IsNullOrEmpty(reader["CategoryName"].ToString()))
                    //{
                    //    mediaAlbum.categoryName = reader["CategoryName"].ToString();
                    //}
                    if (!String.IsNullOrEmpty(reader["GroupName"].ToString()))
                    {
                        mediaAlbum.groupName = reader["GroupName"].ToString();
                    }
                    if (!String.IsNullOrEmpty(reader["AlbumOrder"].ToString()))
                    {
                        mediaAlbum.albumOrder = Convert.ToInt32(reader["AlbumOrder"]);
                    }
                    if (!String.IsNullOrEmpty(reader["EmbedCode"].ToString()))
                    {
                        mediaAlbum.embedCode = reader["EmbedCode"].ToString();
                    }
                    mediaAlbumList.Add(mediaAlbum);

                }
            }
            finally
            {
                reader.Close();
            }

            return mediaAlbumList;
        }
        private static List<MediaAlbum> LoadListFromReader(IDataReader reader)
        {
            List<MediaAlbum> mediaAlbumList = new List<MediaAlbum>();
            try
            {
                while (reader.Read())
                {
                    MediaAlbum mediaAlbum = new MediaAlbum();
                    mediaAlbum.itemID = Convert.ToInt32(reader["ItemID"]);
                    mediaAlbum.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    mediaAlbum.siteID = Convert.ToInt32(reader["SiteID"]);
                    mediaAlbum.groupMediaID = Convert.ToInt32(reader["GroupMediaID"]);
                    //mediaAlbum.categoryID = Convert.ToInt32(reader["CategoryID"]);
                    mediaAlbum.fileName = reader["FileName"].ToString();
                    mediaAlbum.description = reader["Description"].ToString();
                    mediaAlbum.itemUrl = reader["ItemUrl"].ToString();
                    mediaAlbum.totalView = Convert.ToInt32(reader["TotalView"]);
                    mediaAlbum.sizeInKB = Convert.ToInt32(reader["SizeInKB"]);
                    mediaAlbum.typeData = Convert.ToInt32(reader["TypeData"]);
                    mediaAlbum.filePath = reader["FilePath"].ToString();
                    if (!String.IsNullOrEmpty(reader["ImageVideo"].ToString()))
                    {
                        mediaAlbum.imageVideo = reader["ImageVideo"].ToString();
                    }
                    mediaAlbum.featured = Convert.ToBoolean(reader["Featured"]);
                    mediaAlbum.isPublish = Convert.ToBoolean(reader["IsPublish"]);
                    mediaAlbum.userGuid = new Guid(reader["UserGuid"].ToString());
                    mediaAlbum.createdByUser = reader["CreatedByUser"].ToString();
                    if (!String.IsNullOrEmpty(reader["CreatedDate"].ToString()))
                    {
                        mediaAlbum.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    }
                    if (!String.IsNullOrEmpty(reader["LastModUserGuid"].ToString()))
                    {
                        mediaAlbum.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                    }
                    if (!String.IsNullOrEmpty(reader["LastModUtc"].ToString()))
                    {
                        mediaAlbum.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                    }
                    if (!String.IsNullOrEmpty(reader["AlbumOrder"].ToString()))
                    {
                        mediaAlbum.albumOrder = Convert.ToInt32(reader["AlbumOrder"]);
                    }
                    if (!String.IsNullOrEmpty(reader["EmbedCode"].ToString()))
                    {
                        mediaAlbum.embedCode = reader["EmbedCode"].ToString();
                    }
                    mediaAlbumList.Add(mediaAlbum);

                }
            }
            finally
            {
                reader.Close();
            }

            return mediaAlbumList;

        }

        /// <summary>
        /// Gets an IList with all instances of MediaAlbum.
        /// </summary>
        public static List<MediaAlbum> GetAll()
        {
            IDataReader reader = DBMediaAlbum.GetAll();
            return LoadListFromReader(reader);

        }

        public static List<MediaAlbum> GetAllByModule(int moduleID, int groupID)
        {
            IDataReader reader = DBMediaAlbum.GetAllByModule(moduleID, groupID);
            return LoadListFromReader(reader);

        }
        public static List<MediaAlbum> GetByTop(int siteID, int top)
        {
            IDataReader reader = DBMediaAlbum.SelectByTop(siteID, top);
            return LoadListFromReader(reader);

        }

        public static List<MediaAlbum> GetByGroup(int siteID, int groupID,int number=0)
        {
            IDataReader reader = DBMediaAlbum.GetByGroup(siteID, groupID,number);
            return LoadListFromReader(reader);
        }
        /// <summary>
        /// Gets an IList with page of instances of MediaAlbum.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<MediaAlbum> GetPage(int siteID, int moduleID, int pageNumber, int pageSize, int groupMediaID, int featured, int order, bool? publish, string keyword, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBMediaAlbum.GetPage(siteID, moduleID, pageNumber, pageSize, groupMediaID, featured, order, publish, keyword, out totalPages);
            return LoadListFormReader2(reader);
        }

        public static void DeleteAll(int newID)
        {
            var listNew = GetList(newID);
            if (listNew != null && listNew.Count > 0)
            {
                foreach (var item in listNew)
                {
                    Delete(item.itemID);
                }
            }

        }
        public static List<MediaAlbum> GetList(int newPaperID)
        {
            IDataReader reader = DBMediaAlbum.GetList(newPaperID);
            return LoadListFromReader(reader);

        }

        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of MediaAlbum.
        /// </summary>
        public static int CompareByItemID(MediaAlbum mediaAlbum1, MediaAlbum mediaAlbum2)
        {
            return mediaAlbum1.ItemID.CompareTo(mediaAlbum2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of MediaAlbum.
        /// </summary>
        public static int CompareByModuleID(MediaAlbum mediaAlbum1, MediaAlbum mediaAlbum2)
        {
            return mediaAlbum1.ModuleID.CompareTo(mediaAlbum2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of MediaAlbum.
        /// </summary>
        public static int CompareBySiteID(MediaAlbum mediaAlbum1, MediaAlbum mediaAlbum2)
        {
            return mediaAlbum1.SiteID.CompareTo(mediaAlbum2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of MediaAlbum.
        /// </summary>
        public static int CompareByGroupMediaID(MediaAlbum mediaAlbum1, MediaAlbum mediaAlbum2)
        {
            return mediaAlbum1.GroupMediaID.CompareTo(mediaAlbum2.GroupMediaID);
        }
        /// <summary>
        /// Compares 2 instances of MediaAlbum.
        /// </summary>
        public static int CompareByCategoryID(MediaAlbum mediaAlbum1, MediaAlbum mediaAlbum2)
        {
            return mediaAlbum1.CategoryID.CompareTo(mediaAlbum2.CategoryID);
        }
        /// <summary>
        /// Compares 2 instances of MediaAlbum.
        /// </summary>
        public static int CompareByFileName(MediaAlbum mediaAlbum1, MediaAlbum mediaAlbum2)
        {
            return mediaAlbum1.FileName.CompareTo(mediaAlbum2.FileName);
        }
        /// <summary>
        /// Compares 2 instances of MediaAlbum.
        /// </summary>
        public static int CompareByDescription(MediaAlbum mediaAlbum1, MediaAlbum mediaAlbum2)
        {
            return mediaAlbum1.Description.CompareTo(mediaAlbum2.Description);
        }
        /// <summary>
        /// Compares 2 instances of MediaAlbum.
        /// </summary>
        public static int CompareByItemUrl(MediaAlbum mediaAlbum1, MediaAlbum mediaAlbum2)
        {
            return mediaAlbum1.ItemUrl.CompareTo(mediaAlbum2.ItemUrl);
        }
        /// <summary>
        /// Compares 2 instances of MediaAlbum.
        /// </summary>
        public static int CompareByTotalView(MediaAlbum mediaAlbum1, MediaAlbum mediaAlbum2)
        {
            return mediaAlbum1.TotalView.CompareTo(mediaAlbum2.TotalView);
        }
        /// <summary>
        /// Compares 2 instances of MediaAlbum.
        /// </summary>
        public static int CompareBySizeInKB(MediaAlbum mediaAlbum1, MediaAlbum mediaAlbum2)
        {
            return mediaAlbum1.SizeInKB.CompareTo(mediaAlbum2.SizeInKB);
        }
        /// <summary>
        /// Compares 2 instances of MediaAlbum.
        /// </summary>
        public static int CompareByFilePath(MediaAlbum mediaAlbum1, MediaAlbum mediaAlbum2)
        {
            return mediaAlbum1.FilePath.CompareTo(mediaAlbum2.FilePath);
        }
        /// <summary>
        /// Compares 2 instances of MediaAlbum.
        /// </summary>
        public static int CompareByCreatedByUser(MediaAlbum mediaAlbum1, MediaAlbum mediaAlbum2)
        {
            return mediaAlbum1.CreatedByUser.CompareTo(mediaAlbum2.CreatedByUser);
        }
        /// <summary>
        /// Compares 2 instances of MediaAlbum.
        /// </summary>
        public static int CompareByCreatedDate(MediaAlbum mediaAlbum1, MediaAlbum mediaAlbum2)
        {
            return mediaAlbum1.CreatedDate.CompareTo(mediaAlbum2.CreatedDate);
        }
        /// <summary>
        /// Compares 2 instances of MediaAlbum.
        /// </summary>
        public static int CompareByLastModUtc(MediaAlbum mediaAlbum1, MediaAlbum mediaAlbum2)
        {
            return mediaAlbum1.LastModUtc.CompareTo(mediaAlbum2.LastModUtc);
        }

        #endregion


    }

}






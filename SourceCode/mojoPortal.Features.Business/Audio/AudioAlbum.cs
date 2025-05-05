
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

    public class md_AudioAlbum
    {

        #region Constructors

        public md_AudioAlbum()
        { }


        public md_AudioAlbum(
            int itemID)
        {
            Getmd_AudioAlbum(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int moduleID = -1;
        private int siteID = -1;
        private int groupAudioID = -1;
        private int categoryID = -1;
        private string fileName = string.Empty;
        private string description = string.Empty;
        private string itemUrl = string.Empty;
        private int totalView = -1;
        private int sizeInKB = -1;
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
        private int albumOrder = -1;
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
        public int GroupAudioID
        {
            get { return groupAudioID; }
            set { groupAudioID = value; }
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
        /// Gets an instance of md_AudioAlbum.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void Getmd_AudioAlbum(
            int itemID)
        {
            using (IDataReader reader = DBmd_AudioAlbum.GetOne(
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
                this.groupAudioID = Convert.ToInt32(reader["GroupAudioID"]);
                this.categoryID = Convert.ToInt32(reader["CategoryID"]);
                this.fileName = reader["FileName"].ToString();
                this.description = reader["Description"].ToString();
                this.itemUrl = reader["ItemUrl"].ToString();
                this.totalView = Convert.ToInt32(reader["TotalView"]);
                this.sizeInKB = Convert.ToInt32(reader["SizeInKB"]);
                this.typeData = Convert.ToInt32(reader["TypeData"]);
                this.filePath = reader["FilePath"].ToString();
                this.imageVideo = reader["ImageVideo"].ToString();
                this.featured = Convert.ToBoolean(reader["Featured"]);
                this.isPublish = Convert.ToBoolean(reader["IsPublish"]);
                this.userGuid = new Guid(reader["UserGuid"].ToString());
                this.createdByUser = reader["CreatedByUser"].ToString();
                this.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                this.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                this.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                this.albumOrder = Convert.ToInt32(reader["AlbumOrder"]);
                this.embedCode = reader["EmbedCode"].ToString();

            }

        }

        /// <summary>
        /// Persists a new instance of md_AudioAlbum. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBmd_AudioAlbum.Create(
                this.moduleID,
                this.siteID,
                this.groupAudioID,
                this.categoryID,
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
                this.lastModUserGuid,
                this.lastModUtc,
                this.albumOrder,
                this.embedCode);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of md_AudioAlbum. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBmd_AudioAlbum.Update(
                this.itemID,
                this.moduleID,
                this.siteID,
                this.groupAudioID,
                this.categoryID,
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
                this.lastModUserGuid,
                this.lastModUtc,
                this.albumOrder,
                this.embedCode);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of md_AudioAlbum. Returns true on success.
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
        /// Deletes an instance of md_AudioAlbum. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBmd_AudioAlbum.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of md_AudioAlbum. 
        /// </summary>
        public static int GetCount()
        {
            return DBmd_AudioAlbum.GetCount();
        }

        private static List<md_AudioAlbum> LoadListFromReader(IDataReader reader)
        {
            List<md_AudioAlbum> md_AudioAlbumList = new List<md_AudioAlbum>();
            try
            {
                while (reader.Read())
                {
                    md_AudioAlbum md_AudioAlbum = new md_AudioAlbum();
                    md_AudioAlbum.itemID = Convert.ToInt32(reader["ItemID"]);
                    md_AudioAlbum.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    md_AudioAlbum.siteID = Convert.ToInt32(reader["SiteID"]);
                    md_AudioAlbum.groupAudioID = Convert.ToInt32(reader["GroupAudioID"]);
                    md_AudioAlbum.categoryID = Convert.ToInt32(reader["CategoryID"]);
                    md_AudioAlbum.fileName = reader["FileName"].ToString();
                    md_AudioAlbum.description = reader["Description"].ToString();
                    md_AudioAlbum.itemUrl = reader["ItemUrl"].ToString();
                    md_AudioAlbum.totalView = Convert.ToInt32(reader["TotalView"]);
                    md_AudioAlbum.sizeInKB = Convert.ToInt32(reader["SizeInKB"]);
                    md_AudioAlbum.typeData = Convert.ToInt32(reader["TypeData"]);
                    md_AudioAlbum.filePath = reader["FilePath"].ToString();
                    md_AudioAlbum.imageVideo = reader["ImageVideo"].ToString();
                    md_AudioAlbum.featured = Convert.ToBoolean(reader["Featured"]);
                    md_AudioAlbum.isPublish = Convert.ToBoolean(reader["IsPublish"]);
                    md_AudioAlbum.userGuid = new Guid(reader["UserGuid"].ToString());
                    md_AudioAlbum.createdByUser = reader["CreatedByUser"].ToString();
                    md_AudioAlbum.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    md_AudioAlbum.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                    md_AudioAlbum.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                    md_AudioAlbum.albumOrder = Convert.ToInt32(reader["AlbumOrder"]);
                    md_AudioAlbum.embedCode = reader["EmbedCode"].ToString();
                    md_AudioAlbumList.Add(md_AudioAlbum);

                }
            }
            finally
            {
                reader.Close();
            }

            return md_AudioAlbumList;

        }

        public static void DeleteBy(int groupAudioId)
        {
            DBmd_AudioAlbum.DeleteBy(groupAudioId);
        }


        /// <summary>
        /// Gets an IList with all instances of md_AudioAlbum.
        /// </summary>
        public static List<md_AudioAlbum> GetAll()
        {
            IDataReader reader = DBmd_AudioAlbum.GetAll();
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of md_AudioAlbum.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<md_AudioAlbum> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBmd_AudioAlbum.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }


        public static List<md_AudioAlbum> GetByGroup(int siteID, int groupID)
        {
            IDataReader reader = DBmd_AudioAlbum.GetByGroup(siteID, groupID);
            return LoadListFromReader(reader);
        }


        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of md_AudioAlbum.
        /// </summary>
        public static int CompareByItemID(md_AudioAlbum md_AudioAlbum1, md_AudioAlbum md_AudioAlbum2)
        {
            return md_AudioAlbum1.ItemID.CompareTo(md_AudioAlbum2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioAlbum.
        /// </summary>
        public static int CompareByModuleID(md_AudioAlbum md_AudioAlbum1, md_AudioAlbum md_AudioAlbum2)
        {
            return md_AudioAlbum1.ModuleID.CompareTo(md_AudioAlbum2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioAlbum.
        /// </summary>
        public static int CompareBySiteID(md_AudioAlbum md_AudioAlbum1, md_AudioAlbum md_AudioAlbum2)
        {
            return md_AudioAlbum1.SiteID.CompareTo(md_AudioAlbum2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioAlbum.
        /// </summary>
        public static int CompareByGroupAudioID(md_AudioAlbum md_AudioAlbum1, md_AudioAlbum md_AudioAlbum2)
        {
            return md_AudioAlbum1.GroupAudioID.CompareTo(md_AudioAlbum2.GroupAudioID);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioAlbum.
        /// </summary>
        public static int CompareByCategoryID(md_AudioAlbum md_AudioAlbum1, md_AudioAlbum md_AudioAlbum2)
        {
            return md_AudioAlbum1.CategoryID.CompareTo(md_AudioAlbum2.CategoryID);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioAlbum.
        /// </summary>
        public static int CompareByFileName(md_AudioAlbum md_AudioAlbum1, md_AudioAlbum md_AudioAlbum2)
        {
            return md_AudioAlbum1.FileName.CompareTo(md_AudioAlbum2.FileName);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioAlbum.
        /// </summary>
        public static int CompareByDescription(md_AudioAlbum md_AudioAlbum1, md_AudioAlbum md_AudioAlbum2)
        {
            return md_AudioAlbum1.Description.CompareTo(md_AudioAlbum2.Description);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioAlbum.
        /// </summary>
        public static int CompareByItemUrl(md_AudioAlbum md_AudioAlbum1, md_AudioAlbum md_AudioAlbum2)
        {
            return md_AudioAlbum1.ItemUrl.CompareTo(md_AudioAlbum2.ItemUrl);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioAlbum.
        /// </summary>
        public static int CompareByTotalView(md_AudioAlbum md_AudioAlbum1, md_AudioAlbum md_AudioAlbum2)
        {
            return md_AudioAlbum1.TotalView.CompareTo(md_AudioAlbum2.TotalView);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioAlbum.
        /// </summary>
        public static int CompareBySizeInKB(md_AudioAlbum md_AudioAlbum1, md_AudioAlbum md_AudioAlbum2)
        {
            return md_AudioAlbum1.SizeInKB.CompareTo(md_AudioAlbum2.SizeInKB);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioAlbum.
        /// </summary>
        public static int CompareByTypeData(md_AudioAlbum md_AudioAlbum1, md_AudioAlbum md_AudioAlbum2)
        {
            return md_AudioAlbum1.TypeData.CompareTo(md_AudioAlbum2.TypeData);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioAlbum.
        /// </summary>
        public static int CompareByFilePath(md_AudioAlbum md_AudioAlbum1, md_AudioAlbum md_AudioAlbum2)
        {
            return md_AudioAlbum1.FilePath.CompareTo(md_AudioAlbum2.FilePath);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioAlbum.
        /// </summary>
        public static int CompareByImageVideo(md_AudioAlbum md_AudioAlbum1, md_AudioAlbum md_AudioAlbum2)
        {
            return md_AudioAlbum1.ImageVideo.CompareTo(md_AudioAlbum2.ImageVideo);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioAlbum.
        /// </summary>
        public static int CompareByCreatedByUser(md_AudioAlbum md_AudioAlbum1, md_AudioAlbum md_AudioAlbum2)
        {
            return md_AudioAlbum1.CreatedByUser.CompareTo(md_AudioAlbum2.CreatedByUser);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioAlbum.
        /// </summary>
        public static int CompareByCreatedDate(md_AudioAlbum md_AudioAlbum1, md_AudioAlbum md_AudioAlbum2)
        {
            return md_AudioAlbum1.CreatedDate.CompareTo(md_AudioAlbum2.CreatedDate);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioAlbum.
        /// </summary>
        public static int CompareByLastModUtc(md_AudioAlbum md_AudioAlbum1, md_AudioAlbum md_AudioAlbum2)
        {
            return md_AudioAlbum1.LastModUtc.CompareTo(md_AudioAlbum2.LastModUtc);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioAlbum.
        /// </summary>
        public static int CompareByAlbumOrder(md_AudioAlbum md_AudioAlbum1, md_AudioAlbum md_AudioAlbum2)
        {
            return md_AudioAlbum1.AlbumOrder.CompareTo(md_AudioAlbum2.AlbumOrder);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioAlbum.
        /// </summary>
        public static int CompareByEmbedCode(md_AudioAlbum md_AudioAlbum1, md_AudioAlbum md_AudioAlbum2)
        {
            return md_AudioAlbum1.EmbedCode.CompareTo(md_AudioAlbum2.EmbedCode);
        }

        #endregion


    }

}






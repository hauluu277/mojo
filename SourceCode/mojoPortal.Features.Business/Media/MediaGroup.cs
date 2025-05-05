
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
using MediaGroupFeature.Data;

namespace MediaGroupFeature.Business
{

    public class MediaGroup
    {

        #region Constructors

        public MediaGroup()
        { }

        public MediaGroup(bool? isNew = false)
        {
            if (isNew.Value)
            {
                GetOneNew();
            }
        }
        public MediaGroup(
            int itemID)
        {
            GetMediaGroup(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int moduleID = -1;
        private int siteID = -1;
        private string nameGroup = string.Empty;
        private string itemUrl = string.Empty;
        private string filePath = string.Empty;
        private bool? isPublish = null;
        private bool? isHome = null;
        private Guid userGuid = Guid.Empty;
        private string createdByUser = string.Empty;
        private DateTime createdDate = DateTime.UtcNow;
        private Guid lastModUserGuid = Guid.Empty;
        private DateTime lastModUtc = DateTime.UtcNow;
        private int groupOrder = 0;
        private int category = -1;
        private string categoryName = string.Empty;
        private int createByID = -1;
        private int step = 0;
        private string sapo = string.Empty;


        #endregion

        #region Public Properties
        public string Sapo
        {
            get { return sapo; }
            set { sapo = value; }
        }
        public int CreateByID { get { return createByID; } set { createByID = value; } }
        public int Step { get { return step; } set { step = value; } }

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
        public string NameGroup
        {
            get { return nameGroup; }
            set { nameGroup = value; }
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
        public bool? IsPublish
        {
            get { return isPublish; }
            set { isPublish = value; }
        }
        public bool? IsHome
        {
            get { return isHome; }
            set { isHome = value; }
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
        public int GroupOrder
        {
            get { return groupOrder; }
            set { groupOrder = value; }
        }
        public int Category
        {
            get { return category; }
            set { category = value; }
        }
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of MediaGroup.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetMediaGroup(
            int itemID)
        {
            using (IDataReader reader = DBMediaGroup.GetOne(
                itemID))
            {
                PopulateFromReader(reader);
            }

        }

        private void GetOneNew()
        {
            using (IDataReader reader = DBMediaGroup.GetOneNew())
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
                this.nameGroup = reader["NameGroup"].ToString();
                this.itemUrl = reader["ItemUrl"].ToString();
                this.filePath = reader["FilePath"].ToString();
                if (!string.IsNullOrEmpty(reader["IsPublish"].ToString()))
                {
                    this.isPublish = Convert.ToBoolean(reader["IsPublish"]);
                }
                if (!string.IsNullOrEmpty(reader["IsHome"].ToString()))
                {
                    this.isHome = Convert.ToBoolean(reader["IsHome"]);
                }
                if (reader["UserGuid"] != DBNull.Value)
                {
                    this.userGuid = new Guid(reader["UserGuid"].ToString());
                }
                this.createdByUser = reader["CreatedByUser"].ToString();
                this.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                if (reader["LastModUserGuid"] != DBNull.Value)
                {
                    this.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                }
                if (reader["LastModUtc"] != DBNull.Value)
                {
                    this.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                }
                if (reader["GroupOrder"] != DBNull.Value)
                {
                    this.groupOrder = Convert.ToInt32(reader["GroupOrder"]);
                }
                if (reader["Category"] != DBNull.Value)
                {
                    this.category = Convert.ToInt32(reader["Category"]);
                }


                if (reader["CreateByID"] != DBNull.Value)
                {
                    this.createByID = Convert.ToInt32(reader["CreateByID"]);
                }

                if (reader["Step"] != DBNull.Value)
                {
                    this.step = Convert.ToInt32(reader["Step"]);
                }
                if (!string.IsNullOrEmpty(reader["Sapo"].ToString()))
                {
                    this.sapo = reader["Sapo"].ToString();
                }

            }

        }

        /// <summary>
        /// Persists a new instance of MediaGroup. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBMediaGroup.Create(
                this.moduleID,
                this.siteID,
                this.nameGroup,
                this.itemUrl,
                this.filePath,
                this.isPublish,
                this.isHome,
                this.userGuid,
                this.createdByUser,
                this.createdDate,
                this.lastModUserGuid,
                this.lastModUtc,
                this.groupOrder,
                this.category,
                this.createByID,
                this.step,
                this.sapo);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of MediaGroup. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBMediaGroup.Update(
                this.itemID,
                this.moduleID,
                this.siteID,
                this.nameGroup,
                this.itemUrl,
                this.filePath,
                this.isPublish,
                this.isHome,
                this.userGuid,
                this.createdByUser,
                this.createdDate,
                this.lastModUserGuid,
                this.lastModUtc,
                this.groupOrder,
                this.category,
                this.createByID,
                this.step,
                this.sapo);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of MediaGroup. Returns true on success.
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
        /// Deletes an instance of MediaGroup. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBMediaGroup.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of MediaGroup. 
        /// </summary>
        public static int GetCount(int siteID, int moduleID, string keyword, int parentId)
        {
            return DBMediaGroup.GetCount(siteID, moduleID, keyword, parentId);
        }
        private static List<MediaGroup> LoadListFromReaderFix(IDataReader reader, bool hasCategory = false)
        {
            List<MediaGroup> mediaGroupList = new List<MediaGroup>();
            try
            {
                while (reader.Read())
                {
                    MediaGroup mediaGroup = new MediaGroup();
                    mediaGroup.itemID = Convert.ToInt32(reader["ItemID"]);
                    mediaGroup.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    mediaGroup.siteID = Convert.ToInt32(reader["SiteID"]);
                    if (!String.IsNullOrEmpty(reader["NameGroup"].ToString()))
                    {
                        mediaGroup.nameGroup = reader["NameGroup"].ToString();
                    }
                    mediaGroup.itemUrl = reader["ItemUrl"].ToString();
                    mediaGroup.filePath = reader["FilePath"].ToString();
                    if (!string.IsNullOrEmpty(reader["IsPublish"].ToString()))
                    {
                        mediaGroup.isPublish = Convert.ToBoolean(reader["IsPublish"]);
                    }
                    if (!string.IsNullOrEmpty(reader["IsHome"].ToString()))
                    {
                        mediaGroup.isHome = Convert.ToBoolean(reader["IsHome"]);
                    }
                    if (reader["UserGuid"] != DBNull.Value)
                    {
                        mediaGroup.userGuid = new Guid(reader["UserGuid"].ToString());
                    }
                    mediaGroup.createdByUser = reader["CreatedByUser"].ToString();
                    mediaGroup.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    if (reader["LastModUserGuid"] != DBNull.Value)
                    {
                        mediaGroup.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                    }
                    if (reader["LastModUtc"] != DBNull.Value)
                    {
                        mediaGroup.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                    }
                    if (reader["GroupOrder"] != DBNull.Value)
                    {
                        mediaGroup.groupOrder = Convert.ToInt32(reader["GroupOrder"]);
                    }
                    if (reader["Category"] != DBNull.Value)
                    {
                        mediaGroup.category = Convert.ToInt32(reader["Category"]);
                    }
                    if (hasCategory)
                    {
                        if (!string.IsNullOrEmpty(reader["CategoryName"].ToString()))
                        {
                            mediaGroup.CategoryName = reader["CategoryName"].ToString();
                        }
                    }

                    if (reader["CreateByID"] != DBNull.Value)
                    {
                        mediaGroup.createByID = Convert.ToInt32(reader["CreateByID"]);
                    }

                    if (reader["Step"] != DBNull.Value)
                    {
                        mediaGroup.step = Convert.ToInt32(reader["Step"]);
                    }

                    mediaGroupList.Add(mediaGroup);

                }
            }
            finally
            {
                reader.Close();
            }

            return mediaGroupList;

        }


        private static List<MediaGroup> LoadListFromReader(IDataReader reader, bool loadCategoryName = false)
        {
            List<MediaGroup> mediaGroupList = new List<MediaGroup>();
            try
            {
                while (reader.Read())
                {
                    MediaGroup mediaGroup = new MediaGroup();
                    mediaGroup.itemID = Convert.ToInt32(reader["ItemID"]);
                    if (!String.IsNullOrEmpty(reader["ModuleID"].ToString()))
                    {
                        mediaGroup.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    }

                    if (!String.IsNullOrEmpty(reader["SiteID"].ToString()))
                    {
                        mediaGroup.siteID = Convert.ToInt32(reader["SiteID"]);
                    }
                    if (!String.IsNullOrEmpty(reader["NameGroup"].ToString()))
                    {
                        mediaGroup.nameGroup = reader["NameGroup"].ToString();
                    }
                    if (!String.IsNullOrEmpty(reader["ItemUrl"].ToString()))
                    {
                        mediaGroup.itemUrl = reader["ItemUrl"].ToString();
                    }
                    if (!String.IsNullOrEmpty(reader["FilePath"].ToString()))
                    {
                        mediaGroup.filePath = reader["FilePath"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["IsPublish"].ToString()))
                    {
                        mediaGroup.isPublish = Convert.ToBoolean(reader["IsPublish"]);
                    }
                    if (!string.IsNullOrEmpty(reader["IsHome"].ToString()))
                    {
                        mediaGroup.isHome = Convert.ToBoolean(reader["IsHome"]);
                    }
                    if (reader["UserGuid"] != DBNull.Value)
                    {
                        mediaGroup.userGuid = new Guid(reader["UserGuid"].ToString());
                    }
                    if (!String.IsNullOrEmpty(reader["CreatedByUser"].ToString()))
                    {
                        mediaGroup.createdByUser = reader["CreatedByUser"].ToString();
                    }

                    if (reader["LastModUserGuid"] != DBNull.Value)
                    {
                        mediaGroup.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                    }
                    if (reader["LastModUtc"] != DBNull.Value)
                    {
                        mediaGroup.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                    }
                    if (reader["GroupOrder"] != DBNull.Value)
                    {
                        mediaGroup.groupOrder = Convert.ToInt32(reader["GroupOrder"]);
                    }
                    if (reader["Category"] != DBNull.Value)
                    {
                        mediaGroup.category = Convert.ToInt32(reader["Category"]);
                    }
                    if (loadCategoryName && !string.IsNullOrEmpty(reader["CategoryName"].ToString()))
                    {
                        mediaGroup.categoryName = reader["CategoryName"].ToString();
                    }

                    if (reader["CreateByID"] != DBNull.Value)
                    {
                        mediaGroup.createByID = Convert.ToInt32(reader["CreateByID"]);
                    }

                    if (reader["Step"] != DBNull.Value)
                    {
                        mediaGroup.step = Convert.ToInt32(reader["Step"]);
                    }

                    mediaGroupList.Add(mediaGroup);

                }
            }
            finally
            {
                reader.Close();
            }

            return mediaGroupList;

        }
        private static List<MediaGroup> LoadListFromReader2(IDataReader reader)
        {
            List<MediaGroup> mediaGroupList = new List<MediaGroup>();
            try
            {
                while (reader.Read())
                {
                    MediaGroup mediaGroup = new MediaGroup();
                    mediaGroup.itemID = Convert.ToInt32(reader["ItemID"]);
                    mediaGroup.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    mediaGroup.siteID = Convert.ToInt32(reader["SiteID"]);
                    if (!String.IsNullOrEmpty(reader["NameGroup"].ToString()))
                    {
                        mediaGroup.nameGroup = reader["NameGroup"].ToString();
                    }
                    mediaGroup.itemUrl = reader["ItemUrl"].ToString();
                    mediaGroup.filePath = reader["FilePath"].ToString();
                    mediaGroup.isPublish = Convert.ToBoolean(reader["IsPublish"]);
                    mediaGroup.isHome = Convert.ToBoolean(reader["IsHome"]);
                    if (reader["UserGuid"] != DBNull.Value)
                    {
                        mediaGroup.userGuid = new Guid(reader["UserGuid"].ToString());
                    }
                    mediaGroup.createdByUser = reader["CreatedByUser"].ToString();
                    mediaGroup.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    if (reader["LastModUserGuid"] != DBNull.Value)
                    {
                        mediaGroup.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                    }
                    if (reader["LastModUtc"] != DBNull.Value)
                    {
                        mediaGroup.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                    }
                    if (reader["GroupOrder"] != DBNull.Value)
                    {
                        mediaGroup.groupOrder = Convert.ToInt32(reader["GroupOrder"]);
                    }
                    if (reader["Category"] != DBNull.Value)
                    {
                        mediaGroup.category = Convert.ToInt32(reader["Category"]);
                    }
                    if (reader["CategoryName"] != DBNull.Value)
                    {
                        mediaGroup.CategoryName = reader["CategoryName"].ToString();
                    }


                    if (reader["CreateByID"] != DBNull.Value)
                    {
                        mediaGroup.createByID = Convert.ToInt32(reader["CreateByID"]);
                    }

                    if (reader["Step"] != DBNull.Value)
                    {
                        mediaGroup.step = Convert.ToInt32(reader["Step"]);
                    }

                    mediaGroupList.Add(mediaGroup);

                }
            }
            finally
            {
                reader.Close();
            }

            return mediaGroupList;

        }
        /// <summary>
        /// Gets an IList with all instances of MediaGroup.
        /// </summary>
        public static List<MediaGroup> GetAll()
        {
            IDataReader reader = DBMediaGroup.GetAll();
            return LoadListFromReader(reader);

        }


        public static List<MediaGroup> GetGroupOther(int siteID, int itemID, int top)
        {
            IDataReader reader = DBMediaGroup.GetGroupOther(siteID, itemID, top);
            return LoadListFromReader(reader);

        }
        public static List<MediaGroup> GetMediaByCategory(int siteID, int categoryID, int top)
        {
            IDataReader reader = DBMediaGroup.GetMediaByCategory(siteID, categoryID, top);
            return LoadListFromReader(reader);

        }
        public static List<MediaGroup> GetAllBySite(int siteId)
        {
            IDataReader reader = DBMediaGroup.GetAllBySite(siteId);
            return LoadListFromReaderFix(reader);

        }
        public static List<MediaGroup> GetAllBySitePublish(int siteId)
        {
            IDataReader reader = DBMediaGroup.GetAllBySitePublish(siteId);
            return LoadListFromReader(reader);

        }

        public static List<MediaGroup> GetAllByModule(int moduleID)
        {
            IDataReader reader = DBMediaGroup.GetAllByModule(moduleID);
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of MediaGroup.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<MediaGroup> GetPage(int siteID, int moduleID, int pageNumber, int pageSize, string keyword, int parentId, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBMediaGroup.GetPage(siteID, moduleID, pageNumber, pageSize, keyword, parentId, out totalPages);
            return LoadListFromReader(reader);
        }

        public static List<MediaGroup> GetPageManage(string createByUser, DateTime? createDate, int siteID, int moduleID, int pageNumber, int pageSize, string keyword, int parentId, bool? isPublished, int userId, int roleAccess, int step, bool isCaNhan, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBMediaGroup.GetPageManage(createByUser, createDate, siteID, moduleID, pageNumber, pageSize, keyword, parentId, isPublished, userId, roleAccess, step, isCaNhan, out totalPages);
            return LoadListFromReader(reader, true);
        }
        public static List<MediaGroup> GetPageParent(string createByUser, DateTime? createDate, int siteID, int moduleID, int pageNumber, int pageSize, string keyword, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBMediaGroup.GetPageParent(createByUser, createDate, siteID, moduleID, pageNumber, pageSize, keyword, out totalPages);
            return LoadListFromReader(reader);
        }

        public static List<MediaGroup> GetPagePublish(int siteID, int categoryId, int moduleID, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBMediaGroup.GetPagePublish(siteID, categoryId, moduleID, pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }


        public static List<MediaGroup> GetListParent(int siteID, int top)
        {
            IDataReader reader = DBMediaGroup.GetListParent(siteID, top);
            return LoadListFromReader(reader);
        }
        public static List<MediaGroup> GetList(int siteID, int top)
        {
            IDataReader reader = DBMediaGroup.GetList(siteID, top);
            return LoadListFromReader(reader);
        }
        public static List<MediaGroup> GetListItem(int siteID, string listItem)
        {
            IDataReader reader = DBMediaGroup.GetListItem(siteID, listItem);
            return LoadListFromReader(reader);
        }

        public static List<MediaGroup> GetTopHot(int siteId, int top)
        {
            IDataReader reader = DBMediaGroup.GetTopHot(siteId, top);
            return LoadListFromReader(reader);
        }


        public static List<MediaGroup> GetPage2(int siteID, int moduleID, int pageNumber, int pageSize, bool? publish, string keyword, int category, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBMediaGroup.GetPage2(siteID, moduleID, pageNumber, pageSize, publish, keyword, category, out totalPages);
            return LoadListFromReader2(reader);
        }

        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of MediaGroup.
        /// </summary>
        public static int CompareByItemID(MediaGroup mediaGroup1, MediaGroup mediaGroup2)
        {
            return mediaGroup1.ItemID.CompareTo(mediaGroup2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of MediaGroup.
        /// </summary>
        public static int CompareByModuleID(MediaGroup mediaGroup1, MediaGroup mediaGroup2)
        {
            return mediaGroup1.ModuleID.CompareTo(mediaGroup2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of MediaGroup.
        /// </summary>
        public static int CompareBySiteID(MediaGroup mediaGroup1, MediaGroup mediaGroup2)
        {
            return mediaGroup1.SiteID.CompareTo(mediaGroup2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of MediaGroup.
        /// </summary>
        public static int CompareByNameGroup(MediaGroup mediaGroup1, MediaGroup mediaGroup2)
        {
            return mediaGroup1.NameGroup.CompareTo(mediaGroup2.NameGroup);
        }
        /// <summary>
        /// Compares 2 instances of MediaGroup.
        /// </summary>
        public static int CompareByItemUrl(MediaGroup mediaGroup1, MediaGroup mediaGroup2)
        {
            return mediaGroup1.ItemUrl.CompareTo(mediaGroup2.ItemUrl);
        }
        /// <summary>
        /// Compares 2 instances of MediaGroup.
        /// </summary>
        public static int CompareByFilePath(MediaGroup mediaGroup1, MediaGroup mediaGroup2)
        {
            return mediaGroup1.FilePath.CompareTo(mediaGroup2.FilePath);
        }
        /// <summary>
        /// Compares 2 instances of MediaGroup.
        /// </summary>
        public static int CompareByCreatedByUser(MediaGroup mediaGroup1, MediaGroup mediaGroup2)
        {
            return mediaGroup1.CreatedByUser.CompareTo(mediaGroup2.CreatedByUser);
        }
        /// <summary>
        /// Compares 2 instances of MediaGroup.
        /// </summary>
        public static int CompareByCreatedDate(MediaGroup mediaGroup1, MediaGroup mediaGroup2)
        {
            return mediaGroup1.CreatedDate.CompareTo(mediaGroup2.CreatedDate);
        }
        /// <summary>
        /// Compares 2 instances of MediaGroup.
        /// </summary>
        public static int CompareByLastModUtc(MediaGroup mediaGroup1, MediaGroup mediaGroup2)
        {
            return mediaGroup1.LastModUtc.CompareTo(mediaGroup2.LastModUtc);
        }

        #endregion


    }

}






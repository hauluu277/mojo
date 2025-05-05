
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

    public class md_AudioGroup
    {

        #region Constructors

        public md_AudioGroup()
        { }


        public md_AudioGroup(
            int itemID)
        {
            Getmd_AudioGroup(
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
        private bool isPublish = false;
        private Guid userGuid = Guid.Empty;
        private string createdByUser = string.Empty;
        private DateTime createdDate = DateTime.UtcNow;
        private Guid lastModUserGuid = Guid.Empty;
        private DateTime lastModUtc = DateTime.UtcNow;
        private int category = -1;
        private int groupOrder = -1;
        private int createByID = -1;
        private int step = -1;
        private string sapo = string.Empty;
        private bool isHome = false;
        private int counter = -1;

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
        public int Category
        {
            get { return category; }
            set { category = value; }
        }
        public int GroupOrder
        {
            get { return groupOrder; }
            set { groupOrder = value; }
        }
        public int CreateByID
        {
            get { return createByID; }
            set { createByID = value; }
        }
        public int Step
        {
            get { return step; }
            set { step = value; }
        }
        public string Sapo
        {
            get { return sapo; }
            set { sapo = value; }
        }
        public bool IsHome
        {
            get { return isHome; }
            set { isHome = value; }
        }
        public int Counter
        {
            get { return counter; }
            set { counter = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of md_AudioGroup.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void Getmd_AudioGroup(
            int itemID)
        {
            using (IDataReader reader = DBmd_AudioGroup.GetOne(
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
                this.nameGroup = reader["NameGroup"].ToString();
                this.itemUrl = reader["ItemUrl"].ToString();
                this.filePath = reader["FilePath"].ToString();
                this.isPublish = Convert.ToBoolean(reader["IsPublish"]);
                this.userGuid = new Guid(reader["UserGuid"].ToString());
                this.createdByUser = reader["CreatedByUser"].ToString();
                this.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                this.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                this.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                this.category = Convert.ToInt32(reader["Category"]);
                this.groupOrder = Convert.ToInt32(reader["GroupOrder"]);
                this.createByID = Convert.ToInt32(reader["CreateByID"]);
                this.step = Convert.ToInt32(reader["Step"]);
                this.sapo = reader["Sapo"].ToString();
                this.isHome = Convert.ToBoolean(reader["IsHome"]);
                this.counter = Convert.ToInt32(reader["Counter"]);

            }

        }

        /// <summary>
        /// Persists a new instance of md_AudioGroup. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBmd_AudioGroup.Create(
                this.moduleID,
                this.siteID,
                this.nameGroup,
                this.itemUrl,
                this.filePath,
                this.isPublish,
                this.userGuid,
                this.createdByUser,
                this.createdDate,
                this.lastModUserGuid,
                this.lastModUtc,
                this.category,
                this.groupOrder,
                this.createByID,
                this.step,
                this.sapo,
                this.isHome,
                this.counter);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of md_AudioGroup. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBmd_AudioGroup.Update(
                this.itemID,
                this.moduleID,
                this.siteID,
                this.nameGroup,
                this.itemUrl,
                this.filePath,
                this.isPublish,
                this.userGuid,
                this.createdByUser,
                this.createdDate,
                this.lastModUserGuid,
                this.lastModUtc,
                this.category,
                this.groupOrder,
                this.createByID,
                this.step,
                this.sapo,
                this.isHome,
                this.counter);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of md_AudioGroup. Returns true on success.
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


        public static List<md_AudioGroup> GetGroupOther(int siteID, int itemID, int top)
        {
            IDataReader reader = DBmd_AudioGroup.GetGroupOther(siteID, itemID, top);
            return LoadListFromReader(reader);

        }


        #endregion

        #region Static Methods

        /// <summary>
        /// Deletes an instance of md_AudioGroup. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBmd_AudioGroup.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of md_AudioGroup. 
        /// </summary>
        public static int GetCount(int siteId)
        {
            return DBmd_AudioGroup.GetCount(siteId);
        }

        private static List<md_AudioGroup> LoadListFromReader(IDataReader reader)
        {
            List<md_AudioGroup> md_AudioGroupList = new List<md_AudioGroup>();
            try
            {
                while (reader.Read())
                {
                    md_AudioGroup md_AudioGroup = new md_AudioGroup();
                    md_AudioGroup.itemID = Convert.ToInt32(reader["ItemID"]);
                    md_AudioGroup.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    md_AudioGroup.siteID = Convert.ToInt32(reader["SiteID"]);
                    md_AudioGroup.nameGroup = reader["NameGroup"].ToString();
                    md_AudioGroup.itemUrl = reader["ItemUrl"].ToString();
                    md_AudioGroup.filePath = reader["FilePath"].ToString();
                    md_AudioGroup.isPublish = Convert.ToBoolean(reader["IsPublish"]);
                    md_AudioGroup.userGuid = new Guid(reader["UserGuid"].ToString());
                    md_AudioGroup.createdByUser = reader["CreatedByUser"].ToString();
                    md_AudioGroup.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    md_AudioGroup.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                    md_AudioGroup.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                    md_AudioGroup.category = Convert.ToInt32(reader["Category"]);
                    md_AudioGroup.groupOrder = Convert.ToInt32(reader["GroupOrder"]);
                    md_AudioGroup.createByID = Convert.ToInt32(reader["CreateByID"]);
                    md_AudioGroup.step = Convert.ToInt32(reader["Step"]);
                    md_AudioGroup.sapo = reader["Sapo"].ToString();
                    md_AudioGroup.isHome = Convert.ToBoolean(reader["IsHome"]);
                    md_AudioGroup.counter = Convert.ToInt32(reader["Counter"]);
                    md_AudioGroupList.Add(md_AudioGroup);

                }
            }
            finally
            {
                reader.Close();
            }

            return md_AudioGroupList;

        }


        private static List<md_AudioGroup> LoadListFromReader(IDataReader reader, bool loadCategoryName = false)
        {
            List<md_AudioGroup> mediaGroupList = new List<md_AudioGroup>();
            try
            {
                while (reader.Read())
                {
                    md_AudioGroup mediaGroup = new md_AudioGroup();
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
                    if (loadCategoryName && !string.IsNullOrEmpty(reader["CategoryName"].ToString()))
                    {
                        //mediaGroup.categoryName = reader["CategoryName"].ToString();
                    }
                    if (reader["Sapo"] != DBNull.Value)
                    {
                        mediaGroup.Sapo = reader["Sapo"].ToString();
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


        public static List<md_AudioGroup> GetAllByModule(int moduleID)
        {
            IDataReader reader = DBmd_AudioGroup.GetAllByModule(moduleID);
            return LoadListFromReader(reader);

        }



        /// <summary>
        /// Gets an IList with all instances of md_AudioGroup.
        /// </summary>
        public static List<md_AudioGroup> GetAll()
        {
            IDataReader reader = DBmd_AudioGroup.GetAll();
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of md_AudioGroup.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<md_AudioGroup> GetPage(int siteID, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBmd_AudioGroup.GetPage(siteID, pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }

        public static List<md_AudioGroup> GetPageManage(string createByUser, DateTime? createDate, int siteID, int moduleID, int pageNumber, int pageSize, string keyword, int parentId, bool? isPublished, int userId, int roleAccess, int step, bool isCaNhan, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBmd_AudioGroup.GetPageManage(createByUser, createDate, siteID, moduleID, pageNumber, pageSize, keyword, parentId, isPublished, userId, roleAccess, step, isCaNhan, out totalPages);
            return LoadListFromReader(reader, true);
        }


        public static List<md_AudioGroup> GetAllBySitePublish(int siteId)
        {
            IDataReader reader = DBmd_AudioGroup.GetAllBySitePublish(siteId);
            return LoadListFromReader(reader);

        }

        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of md_AudioGroup.
        /// </summary>
        public static int CompareByItemID(md_AudioGroup md_AudioGroup1, md_AudioGroup md_AudioGroup2)
        {
            return md_AudioGroup1.ItemID.CompareTo(md_AudioGroup2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioGroup.
        /// </summary>
        public static int CompareByModuleID(md_AudioGroup md_AudioGroup1, md_AudioGroup md_AudioGroup2)
        {
            return md_AudioGroup1.ModuleID.CompareTo(md_AudioGroup2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioGroup.
        /// </summary>
        public static int CompareBySiteID(md_AudioGroup md_AudioGroup1, md_AudioGroup md_AudioGroup2)
        {
            return md_AudioGroup1.SiteID.CompareTo(md_AudioGroup2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioGroup.
        /// </summary>
        public static int CompareByNameGroup(md_AudioGroup md_AudioGroup1, md_AudioGroup md_AudioGroup2)
        {
            return md_AudioGroup1.NameGroup.CompareTo(md_AudioGroup2.NameGroup);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioGroup.
        /// </summary>
        public static int CompareByItemUrl(md_AudioGroup md_AudioGroup1, md_AudioGroup md_AudioGroup2)
        {
            return md_AudioGroup1.ItemUrl.CompareTo(md_AudioGroup2.ItemUrl);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioGroup.
        /// </summary>
        public static int CompareByFilePath(md_AudioGroup md_AudioGroup1, md_AudioGroup md_AudioGroup2)
        {
            return md_AudioGroup1.FilePath.CompareTo(md_AudioGroup2.FilePath);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioGroup.
        /// </summary>
        public static int CompareByCreatedByUser(md_AudioGroup md_AudioGroup1, md_AudioGroup md_AudioGroup2)
        {
            return md_AudioGroup1.CreatedByUser.CompareTo(md_AudioGroup2.CreatedByUser);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioGroup.
        /// </summary>
        public static int CompareByCreatedDate(md_AudioGroup md_AudioGroup1, md_AudioGroup md_AudioGroup2)
        {
            return md_AudioGroup1.CreatedDate.CompareTo(md_AudioGroup2.CreatedDate);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioGroup.
        /// </summary>
        public static int CompareByLastModUtc(md_AudioGroup md_AudioGroup1, md_AudioGroup md_AudioGroup2)
        {
            return md_AudioGroup1.LastModUtc.CompareTo(md_AudioGroup2.LastModUtc);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioGroup.
        /// </summary>
        public static int CompareByCategory(md_AudioGroup md_AudioGroup1, md_AudioGroup md_AudioGroup2)
        {
            return md_AudioGroup1.Category.CompareTo(md_AudioGroup2.Category);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioGroup.
        /// </summary>
        public static int CompareByGroupOrder(md_AudioGroup md_AudioGroup1, md_AudioGroup md_AudioGroup2)
        {
            return md_AudioGroup1.GroupOrder.CompareTo(md_AudioGroup2.GroupOrder);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioGroup.
        /// </summary>
        public static int CompareByCreateByID(md_AudioGroup md_AudioGroup1, md_AudioGroup md_AudioGroup2)
        {
            return md_AudioGroup1.CreateByID.CompareTo(md_AudioGroup2.CreateByID);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioGroup.
        /// </summary>
        public static int CompareByStep(md_AudioGroup md_AudioGroup1, md_AudioGroup md_AudioGroup2)
        {
            return md_AudioGroup1.Step.CompareTo(md_AudioGroup2.Step);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioGroup.
        /// </summary>
        public static int CompareBySapo(md_AudioGroup md_AudioGroup1, md_AudioGroup md_AudioGroup2)
        {
            return md_AudioGroup1.Sapo.CompareTo(md_AudioGroup2.Sapo);
        }
        /// <summary>
        /// Compares 2 instances of md_AudioGroup.
        /// </summary>
        public static int CompareByCounter(md_AudioGroup md_AudioGroup1, md_AudioGroup md_AudioGroup2)
        {
            return md_AudioGroup1.Counter.CompareTo(md_AudioGroup2.Counter);
        }

        #endregion


    }

}







// Author:					Hậu sky
// Created:					2018-1-10
// Last Modified:			2018-1-10
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

    public class HomeCategory
    {

        #region Constructors

        public HomeCategory()
        { }


        public HomeCategory(
            int itemID)
        {
            GetHomeCategory(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int siteID = -1;
        private int moduleID = -1;
        private string title = string.Empty;
        private string itemUrl = string.Empty;
        private string description = string.Empty;
        private bool isPublish = false;
        private DateTime dateCreate = DateTime.Now;
        private int orderBy = -1;
        private string itemIcon = string.Empty;

        #endregion

        #region Public Properties

        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public int SiteID
        {
            get { return siteID; }
            set { siteID = value; }
        }
        public int ModuleID
        {
            get { return moduleID; }
            set { moduleID = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string ItemUrl
        {
            get { return itemUrl; }
            set { itemUrl = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public bool IsPublish
        {
            get { return isPublish; }
            set { isPublish = value; }
        }
        public DateTime DateCreate
        {
            get { return dateCreate; }
            set { dateCreate = value; }
        }
        public int OrderBy
        {
            get { return orderBy; }
            set { orderBy = value; }
        }
        public string ItemIcon
        {
            get { return itemIcon; }
            set { itemIcon = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of HomeCategory.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetHomeCategory(
            int itemID)
        {
            using (IDataReader reader = DBHomeCategory.GetOne(
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
                this.siteID = Convert.ToInt32(reader["SiteID"]);
                this.moduleID = Convert.ToInt32(reader["ModuleID"]);
                this.title = reader["Title"].ToString();
                this.itemUrl = reader["ItemUrl"].ToString();
                this.description = reader["Description"].ToString();
                this.isPublish = Convert.ToBoolean(reader["IsPublish"]);
                this.dateCreate = Convert.ToDateTime(reader["DateCreate"]);
                this.orderBy = Convert.ToInt32(reader["OrderBy"]);
                this.itemIcon = reader["ItemIcon"].ToString();

            }

        }

        /// <summary>
        /// Persists a new instance of HomeCategory. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBHomeCategory.Create(
                this.siteID,
                this.moduleID,
                this.title,
                this.itemUrl,
                this.description,
                this.isPublish,
                this.dateCreate,
                this.orderBy,
                this.itemIcon);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of HomeCategory. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBHomeCategory.Update(
                this.itemID,
                this.siteID,
                this.moduleID,
                this.title,
                this.itemUrl,
                this.description,
                this.isPublish,
                this.dateCreate,
                this.orderBy,
                this.itemIcon);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of HomeCategory. Returns true on success.
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
        /// Deletes an instance of HomeCategory. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBHomeCategory.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of HomeCategory. 
        /// </summary>
        public static int GetCount(int siteID, int moduleID)
        {
            return DBHomeCategory.GetCount(siteID, moduleID);
        }

        private static List<HomeCategory> LoadListFromReader(IDataReader reader)
        {
            List<HomeCategory> homeCategoryList = new List<HomeCategory>();
            try
            {
                while (reader.Read())
                {
                    HomeCategory homeCategory = new HomeCategory();
                    homeCategory.itemID = Convert.ToInt32(reader["ItemID"]);
                    homeCategory.siteID = Convert.ToInt32(reader["SiteID"]);
                    homeCategory.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    homeCategory.title = reader["Title"].ToString();
                    homeCategory.itemUrl = reader["ItemUrl"].ToString();
                    homeCategory.description = reader["Description"].ToString();
                    homeCategory.isPublish = Convert.ToBoolean(reader["IsPublish"]);
                    homeCategory.dateCreate = Convert.ToDateTime(reader["DateCreate"]);
                    homeCategory.orderBy = Convert.ToInt32(reader["OrderBy"]);
                    homeCategory.itemIcon = reader["ItemIcon"].ToString();
                    homeCategoryList.Add(homeCategory);

                }
            }
            finally
            {
                reader.Close();
            }

            return homeCategoryList;

        }

        /// <summary>
        /// Gets an IList with all instances of HomeCategory.
        /// </summary>
        public static List<HomeCategory> GetAll()
        {
            IDataReader reader = DBHomeCategory.GetAll();
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of HomeCategory.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<HomeCategory> GetPage(int siteId, int moduleId, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBHomeCategory.GetPage(siteId, moduleId, pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of HomeCategory.
        /// </summary>
        public static int CompareByItemID(HomeCategory homeCategory1, HomeCategory homeCategory2)
        {
            return homeCategory1.ItemID.CompareTo(homeCategory2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of HomeCategory.
        /// </summary>
        public static int CompareBySiteID(HomeCategory homeCategory1, HomeCategory homeCategory2)
        {
            return homeCategory1.SiteID.CompareTo(homeCategory2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of HomeCategory.
        /// </summary>
        public static int CompareByModuleID(HomeCategory homeCategory1, HomeCategory homeCategory2)
        {
            return homeCategory1.ModuleID.CompareTo(homeCategory2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of HomeCategory.
        /// </summary>
        public static int CompareByTitle(HomeCategory homeCategory1, HomeCategory homeCategory2)
        {
            return homeCategory1.Title.CompareTo(homeCategory2.Title);
        }
        /// <summary>
        /// Compares 2 instances of HomeCategory.
        /// </summary>
        public static int CompareByItemUrl(HomeCategory homeCategory1, HomeCategory homeCategory2)
        {
            return homeCategory1.ItemUrl.CompareTo(homeCategory2.ItemUrl);
        }
        /// <summary>
        /// Compares 2 instances of HomeCategory.
        /// </summary>
        public static int CompareByDescription(HomeCategory homeCategory1, HomeCategory homeCategory2)
        {
            return homeCategory1.Description.CompareTo(homeCategory2.Description);
        }
        /// <summary>
        /// Compares 2 instances of HomeCategory.
        /// </summary>
        public static int CompareByDateCreate(HomeCategory homeCategory1, HomeCategory homeCategory2)
        {
            return homeCategory1.DateCreate.CompareTo(homeCategory2.DateCreate);
        }
        /// <summary>
        /// Compares 2 instances of HomeCategory.
        /// </summary>
        public static int CompareByOrderBy(HomeCategory homeCategory1, HomeCategory homeCategory2)
        {
            return homeCategory1.OrderBy.CompareTo(homeCategory2.OrderBy);
        }
        /// <summary>
        /// Compares 2 instances of HomeCategory.
        /// </summary>
        public static int CompareByItemIcon(HomeCategory homeCategory1, HomeCategory homeCategory2)
        {
            return homeCategory1.ItemIcon.CompareTo(homeCategory2.ItemIcon);
        }

        #endregion


    }

}






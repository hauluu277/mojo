
// Author:					HAULD
// Created:					2015-10-28
// Last Modified:			2015-10-28
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
using MenuFeature.Data;

namespace MenuFeature.Business
{

    public class MenuVertical
    {

        #region Constructors

        public MenuVertical()
        { }


        public MenuVertical(
            int itemID)
        {
            GetMenuVertical(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int moduleID = -1;
        private int siteID = -1;
        private string name = string.Empty;
        private string linkUrl = string.Empty;
        private int ordinal = -1;
        private int parentID = -1;
        private Guid userGuid = Guid.Empty;
        private string createdByUser = string.Empty;
        private DateTime createdDate = DateTime.UtcNow;
        private Guid lastModUserGuid = Guid.Empty;
        private DateTime lastModUtc = DateTime.UtcNow;
        private string parentName = string.Empty;
        private bool typeOther = false;
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
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string LinkUrl
        {
            get { return linkUrl; }
            set { linkUrl = value; }
        }
        public int Ordinal
        {
            get { return ordinal; }
            set { ordinal = value; }
        }
        public int ParentID
        {
            get { return parentID; }
            set { parentID = value; }
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
        public string ParentName
        {
            get { return parentName; }
            set { parentName = value; }
        }
        public bool TypeOther
        {
            get { return typeOther; }
            set { typeOther = value; }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of Menu.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetMenuVertical(
            int itemID)
        {
            using (IDataReader reader = DBMenu.GetOne(
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
                this.name = reader["Name"].ToString();
                this.linkUrl = reader["LinkUrl"].ToString();
                this.ordinal = Convert.ToInt32(reader["Ordinal"]);
                this.parentID = Convert.ToInt32(reader["ParentID"]);
                this.userGuid = new Guid(reader["UserGuid"].ToString());
                this.createdByUser = reader["CreatedByUser"].ToString();
                this.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                this.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                this.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                if (!String.IsNullOrEmpty(reader["TypeOther"].ToString()))
                {
                    this.typeOther = Convert.ToBoolean(reader["TypeOther"].ToString());
                }
            }

        }

        /// <summary>
        /// Persists a new instance of Menu. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBMenu.Create(
                this.moduleID,
                this.siteID,
                this.name,
                this.linkUrl,
                this.ordinal,
                this.parentID,
                this.userGuid,
                this.createdByUser,
                this.createdDate,
                this.lastModUserGuid,
                this.lastModUtc,
                this.TypeOther);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of Menu. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBMenu.Update(
                this.itemID,
                this.moduleID,
                this.siteID,
                this.name,
                this.linkUrl,
                this.ordinal,
                this.parentID,
                this.userGuid,
                this.createdByUser,
                this.createdDate,
                this.lastModUserGuid,
                this.lastModUtc,
                this.typeOther);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of Menu. Returns true on success.
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
        /// Deletes an instance of Menu. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBMenu.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of Menu. 
        /// </summary>
        public static int GetCount(int siteID, int moduleID, int parentID, string keyword)
        {
            return DBMenu.GetCount(siteID, moduleID, parentID, keyword);
        }
        private static List<MenuVertical> LoadListFromReader(IDataReader reader)
        {
            List<MenuVertical> menuList = new List<MenuVertical>();
            try
            {
                while (reader.Read())
                {
                    MenuVertical menu = new MenuVertical();
                    menu.itemID = Convert.ToInt32(reader["ItemID"]);
                    menu.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    menu.siteID = Convert.ToInt32(reader["SiteID"]);
                    menu.name = reader["Name"].ToString();
                    menu.linkUrl = reader["LinkUrl"].ToString();
                    menu.ordinal = Convert.ToInt32(reader["Ordinal"]);
                    menu.parentID = Convert.ToInt32(reader["ParentID"]);
                    menu.userGuid = new Guid(reader["UserGuid"].ToString());
                    menu.createdByUser = reader["CreatedByUser"].ToString();
                    menu.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    menu.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                    menu.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                    if (!String.IsNullOrEmpty(reader["TypeOther"].ToString()))
                    {
                        menu.typeOther = Convert.ToBoolean(reader["TypeOther"].ToString());
                    }
                    else
                    {
                        menu.typeOther = false;
                    }
                    menuList.Add(menu);

                }
            }
            finally
            {
                reader.Close();
            }

            return menuList;

        }
        private static List<MenuVertical> LoadListFromReader2(IDataReader reader)
        {
            List<MenuVertical> menuList = new List<MenuVertical>();
            try
            {
                while (reader.Read())
                {
                    MenuVertical menu = new MenuVertical();
                    menu.itemID = Convert.ToInt32(reader["ItemID"]);
                    menu.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    menu.siteID = Convert.ToInt32(reader["SiteID"]);
                    menu.name = reader["Name"].ToString();
                    menu.linkUrl = reader["LinkUrl"].ToString();
                    menu.ordinal = Convert.ToInt32(reader["Ordinal"]);
                    menu.parentID = Convert.ToInt32(reader["ParentID"]);
                    menu.userGuid = new Guid(reader["UserGuid"].ToString());
                    menu.createdByUser = reader["CreatedByUser"].ToString();
                    menu.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    menu.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                    menu.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                    if (!String.IsNullOrEmpty(reader["ParentName"].ToString()))
                    {
                        menu.parentName = reader["ParentName"].ToString();
                    }
                    if (!String.IsNullOrEmpty(reader["TypeOther"].ToString()))
                    {
                        menu.typeOther = Convert.ToBoolean(reader["TypeOther"].ToString());
                    }
                    else
                    {
                        menu.typeOther = false;
                    }
                    menuList.Add(menu);

                }
            }
            finally
            {
                reader.Close();
            }

            return menuList;

        }
        /// <summary>
        /// Gets an IList with all instances of Menu.
        /// </summary>
        public static List<MenuVertical> GetAll(int siteID, int moduleID)
        {
            IDataReader reader = DBMenu.GetAll(siteID, moduleID);
            return LoadListFromReader(reader);

        }
        public static List<MenuVertical> GetAllParent(int siteID, int moduleID)
        {
            IDataReader reader = DBMenu.GetAllParent(siteID, moduleID);
            return LoadListFromReader(reader);
        }

        public static List<MenuVertical> GetParentByModuleID(int siteID, int moduleID)
        {
            IDataReader reader = DBMenu.GetParentByModuleID(siteID, moduleID);
            return LoadListFromReader(reader);
        }

        public static List<MenuVertical> GetByModuleID(int siteID, int moduleID)
        {
            IDataReader reader = DBMenu.GetAllByModuleID(siteID, moduleID);
            return LoadListFromReader(reader);
        }
        /// <summary>
        /// Gets an IList with page of instances of Menu.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<MenuVertical> GetPage(int siteID, int moduleID, int pageNumber, int pageSize, int parentID, string keyword, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBMenu.GetPage(siteID, moduleID, pageNumber, pageSize, parentID, keyword, out totalPages);
            return LoadListFromReader2(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of Menu.
        /// </summary>
        public static int CompareByItemID(MenuVertical menu1, MenuVertical menu2)
        {
            return menu1.ItemID.CompareTo(menu2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of Menu.
        /// </summary>
        public static int CompareByModuleID(MenuVertical menu1, MenuVertical menu2)
        {
            return menu1.ModuleID.CompareTo(menu2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of Menu.
        /// </summary>
        public static int CompareBySiteID(MenuVertical menu1, MenuVertical menu2)
        {
            return menu1.SiteID.CompareTo(menu2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of Menu.
        /// </summary>
        public static int CompareByName(MenuVertical menu1, MenuVertical menu2)
        {
            return menu1.Name.CompareTo(menu2.Name);
        }
        /// <summary>
        /// Compares 2 instances of Menu.
        /// </summary>
        public static int CompareByLinkUrl(MenuVertical menu1, MenuVertical menu2)
        {
            return menu1.LinkUrl.CompareTo(menu2.LinkUrl);
        }
        /// <summary>
        /// Compares 2 instances of Menu.
        /// </summary>
        public static int CompareByOrdinal(MenuVertical menu1, MenuVertical menu2)
        {
            return menu1.Ordinal.CompareTo(menu2.Ordinal);
        }
        /// <summary>
        /// Compares 2 instances of Menu.
        /// </summary>
        public static int CompareByParentID(MenuVertical menu1, MenuVertical menu2)
        {
            return menu1.ParentID.CompareTo(menu2.ParentID);
        }
        /// <summary>
        /// Compares 2 instances of Menu.
        /// </summary>
        public static int CompareByCreatedByUser(MenuVertical menu1, MenuVertical menu2)
        {
            return menu1.CreatedByUser.CompareTo(menu2.CreatedByUser);
        }
        /// <summary>
        /// Compares 2 instances of Menu.
        /// </summary>
        public static int CompareByCreatedDate(MenuVertical menu1, MenuVertical menu2)
        {
            return menu1.CreatedDate.CompareTo(menu2.CreatedDate);
        }
        /// <summary>
        /// Compares 2 instances of Menu.
        /// </summary>
        public static int CompareByLastModUtc(MenuVertical menu1, MenuVertical menu2)
        {
            return menu1.LastModUtc.CompareTo(menu2.LastModUtc);
        }

        #endregion


    }

}







// Author:					Mr Hậu
// Created:					2020-1-16
// Last Modified:			2020-1-16
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

    public class core_SettingMenu
    {

        #region Constructors

        public core_SettingMenu()
        { }


        public core_SettingMenu(
            int itemID)
        {
            Getcore_SettingMenu(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int siteID = -1;
        private string urlIMG = string.Empty;
        private string urlItem = string.Empty;
        private int typeIMG = -1;

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
        public string UrlIMG
        {
            get { return urlIMG; }
            set { urlIMG = value; }
        }
        public string UrlItem
        {
            get { return urlItem; }
            set { urlItem = value; }
        }
        public int TypeIMG
        {
            get { return typeIMG; }
            set { typeIMG = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of core_SettingMenu.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void Getcore_SettingMenu(
            int itemID)
        {
            using (IDataReader reader = DBcore_SettingMenu.GetOne(
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
                this.urlIMG = reader["UrlIMG"].ToString();
                this.urlItem = reader["UrlItem"].ToString();
                this.typeIMG = Convert.ToInt32(reader["TypeIMG"]);

            }

        }

        /// <summary>
        /// Persists a new instance of core_SettingMenu. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBcore_SettingMenu.Create(
                this.siteID,
                this.urlIMG,
                this.urlItem,
                this.typeIMG);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of core_SettingMenu. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {
            
            return DBcore_SettingMenu.Update(
                this.itemID,
                this.siteID,
                this.urlIMG,
                this.urlItem,
                this.typeIMG);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of core_SettingMenu. Returns true on success.
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
        /// Deletes an instance of core_SettingMenu. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBcore_SettingMenu.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of core_SettingMenu. 
        /// </summary>
        public static int GetCount()
        {
            return DBcore_SettingMenu.GetCount();
        }

        private static List<core_SettingMenu> LoadListFromReader(IDataReader reader)
        {
            List<core_SettingMenu> core_SettingMenuList = new List<core_SettingMenu>();
            try
            {
                while (reader.Read())
                {
                    core_SettingMenu core_SettingMenu = new core_SettingMenu();
                    core_SettingMenu.itemID = Convert.ToInt32(reader["ItemID"]);
                    core_SettingMenu.siteID = Convert.ToInt32(reader["SiteID"]);
                    core_SettingMenu.urlIMG = reader["UrlIMG"].ToString();
                    core_SettingMenu.urlItem = reader["UrlItem"].ToString();
                    core_SettingMenu.typeIMG = Convert.ToInt32(reader["TypeIMG"]);
                    core_SettingMenuList.Add(core_SettingMenu);

                }
            }
            finally
            {
                reader.Close();
            }

            return core_SettingMenuList;

        }

        /// <summary>
        /// Gets an IList with all instances of core_SettingMenu.
        /// </summary>
        public static List<core_SettingMenu> GetAll()
        {
            IDataReader reader = DBcore_SettingMenu.GetAll();
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of core_SettingMenu.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<core_SettingMenu> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBcore_SettingMenu.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of core_SettingMenu.
        /// </summary>
        public static int CompareByItemID(core_SettingMenu core_SettingMenu1, core_SettingMenu core_SettingMenu2)
        {
            return core_SettingMenu1.ItemID.CompareTo(core_SettingMenu2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of core_SettingMenu.
        /// </summary>
        public static int CompareBySiteID(core_SettingMenu core_SettingMenu1, core_SettingMenu core_SettingMenu2)
        {
            return core_SettingMenu1.SiteID.CompareTo(core_SettingMenu2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of core_SettingMenu.
        /// </summary>
        public static int CompareByUrlIMG(core_SettingMenu core_SettingMenu1, core_SettingMenu core_SettingMenu2)
        {
            return core_SettingMenu1.UrlIMG.CompareTo(core_SettingMenu2.UrlIMG);
        }
        /// <summary>
        /// Compares 2 instances of core_SettingMenu.
        /// </summary>
        public static int CompareByUrlItem(core_SettingMenu core_SettingMenu1, core_SettingMenu core_SettingMenu2)
        {
            return core_SettingMenu1.UrlItem.CompareTo(core_SettingMenu2.UrlItem);
        }
        /// <summary>
        /// Compares 2 instances of core_SettingMenu.
        /// </summary>
        public static int CompareByTypeIMG(core_SettingMenu core_SettingMenu1, core_SettingMenu core_SettingMenu2)
        {
            return core_SettingMenu1.TypeIMG.CompareTo(core_SettingMenu2.TypeIMG);
        }

        #endregion


    }

}






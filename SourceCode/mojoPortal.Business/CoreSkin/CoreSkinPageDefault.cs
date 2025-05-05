
// Author:					Joe Audette
// Created:					2017-12-17
// Last Modified:			2017-12-17
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

    public class CoreSkinPageDefault : IComparable
    {

        #region Constructors

        public CoreSkinPageDefault()
        { }


        public CoreSkinPageDefault(
            int itemID)
        {
            GetSkinPageDefault(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int siteID = -1;
        private int skinID = -1;
        private int skinPageID = -1;
        private int moduleID = -1;
        private string moduleTitle = string.Empty;
        private string paneName = string.Empty;
        private int moduleOrder = -1;

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
        public int SkinID
        {
            get { return skinID; }
            set { skinID = value; }
        }
        public int SkinPageID
        {
            get { return skinPageID; }
            set { skinPageID = value; }
        }
        public int ModuleID
        {
            get { return moduleID; }
            set { moduleID = value; }
        }
        public string ModuleTitle
        {
            get { return moduleTitle; }
            set { moduleTitle = value; }
        }
        public string PaneName
        {
            get { return paneName; }
            set { paneName = value; }
        }
        public int ModuleOrder
        {
            get { return moduleOrder; }
            set { moduleOrder = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of SkinPageDefault.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetSkinPageDefault(
            int itemID)
        {
            using (IDataReader reader = DBCoreSkinPageDefault.GetOne(
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
                this.skinID = Convert.ToInt32(reader["SkinID"]);
                this.skinPageID = Convert.ToInt32(reader["SkinPageID"]);
                this.moduleID = Convert.ToInt32(reader["ModuleID"]);
                this.moduleTitle = reader["ModuleTitle"].ToString();
                this.paneName = reader["PaneName"].ToString();
                this.moduleOrder = Convert.ToInt32(reader["ModuleOrder"]);

            }

        }

        /// <summary>
        /// Persists a new instance of SkinPageDefault. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBCoreSkinPageDefault.Create(
                this.siteID,
                this.skinID,
                this.skinPageID,
                this.moduleID,
                this.moduleTitle,
                this.paneName,
                this.moduleOrder);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of SkinPageDefault. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBCoreSkinPageDefault.Update(
                this.itemID,
                this.siteID,
                this.skinID,
                this.skinPageID,
                this.moduleID,
                this.moduleTitle,
                this.paneName,
                this.moduleOrder);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of SkinPageDefault. Returns true on success.
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
        /// Deletes an instance of SkinPageDefault. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBCoreSkinPageDefault.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of SkinPageDefault. 
        /// </summary>
        public static int GetCount()
        {
            return DBCoreSkinPageDefault.GetCount();
        }

        private static List<CoreSkinPageDefault> LoadListFromReader(IDataReader reader)
        {
            List<CoreSkinPageDefault> skinPageDefaultList = new List<CoreSkinPageDefault>();
            try
            {
                while (reader.Read())
                {
                    CoreSkinPageDefault skinPageDefault = new CoreSkinPageDefault();
                    skinPageDefault.itemID = Convert.ToInt32(reader["ItemID"]);
                    skinPageDefault.siteID = Convert.ToInt32(reader["SiteID"]);
                    skinPageDefault.skinID = Convert.ToInt32(reader["SkinID"]);
                    skinPageDefault.skinPageID = Convert.ToInt32(reader["SkinPageID"]);
                    skinPageDefault.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    skinPageDefault.moduleTitle = reader["ModuleTitle"].ToString();
                    skinPageDefault.paneName = reader["PaneName"].ToString();
                    skinPageDefault.moduleOrder = Convert.ToInt32(reader["ModuleOrder"]);
                    skinPageDefaultList.Add(skinPageDefault);

                }
            }
            finally
            {
                reader.Close();
            }

            return skinPageDefaultList;

        }

        /// <summary>
        /// Gets an IList with all instances of SkinPageDefault.
        /// </summary>
        public static List<CoreSkinPageDefault> GetAll()
        {
            IDataReader reader = DBCoreSkinPageDefault.GetAll();
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of SkinPageDefault.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<CoreSkinPageDefault> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBCoreSkinPageDefault.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }

        public static IDataReader GetSkinPageDefaultForPage(int skinPageID)
        {
            return DBCoreSkinPageDefault.GetSkinPageDefaultForPage(skinPageID);
        }
        public static bool UpdateDefaultModuleOrder(int itemId, int moduleOrder, string paneName)
        {
            return DBCoreSkinPageDefault.UpdateDefaultModuleOrder(itemId, moduleOrder, paneName);
        }

        public static bool DeleteDefaultModule(int itemId)
        {
            return DBCoreSkinPageDefault.DeleteDefaultModule(itemId);
        }

        public static ArrayList RefreshDefaultModules(int skinPageID)
        {
            ArrayList modules = new ArrayList();
            using (IDataReader reader = GetSkinPageDefaultForPage(skinPageID))
            {
                while (reader.Read())
                {
                    CoreSkinPageDefault m = new CoreSkinPageDefault();
                    m.ItemID = Convert.ToInt32(reader["ItemID"]);
                    m.ModuleID = Convert.ToInt32(reader["ModuleID"]);
                    m.PaneName = reader["PaneName"].ToString();
                    m.ModuleTitle = reader["DefautlTitle"].ToString();
                    m.ModuleOrder = Convert.ToInt32(reader["ModuleOrder"]);
                    m.SkinPageID = Convert.ToInt32(reader["SkinPageID"]);
                    m.SkinID = Convert.ToInt32(reader["SkinID"]);
                    modules.Add(m);
                }
            }
            return modules;
        }

        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of SkinPageDefault.
        /// </summary>
        public static int CompareByItemID(CoreSkinPageDefault skinPageDefault1, CoreSkinPageDefault skinPageDefault2)
        {
            return skinPageDefault1.ItemID.CompareTo(skinPageDefault2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of SkinPageDefault.
        /// </summary>
        public static int CompareBySiteID(CoreSkinPageDefault skinPageDefault1, CoreSkinPageDefault skinPageDefault2)
        {
            return skinPageDefault1.SiteID.CompareTo(skinPageDefault2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of SkinPageDefault.
        /// </summary>
        public static int CompareBySkinID(CoreSkinPageDefault skinPageDefault1, CoreSkinPageDefault skinPageDefault2)
        {
            return skinPageDefault1.SkinID.CompareTo(skinPageDefault2.SkinID);
        }
        /// <summary>
        /// Compares 2 instances of SkinPageDefault.
        /// </summary>
        public static int CompareBySkinPageID(CoreSkinPageDefault skinPageDefault1, CoreSkinPageDefault skinPageDefault2)
        {
            return skinPageDefault1.SkinPageID.CompareTo(skinPageDefault2.SkinPageID);
        }
        /// <summary>
        /// Compares 2 instances of SkinPageDefault.
        /// </summary>
        public static int CompareByModuleID(CoreSkinPageDefault skinPageDefault1, CoreSkinPageDefault skinPageDefault2)
        {
            return skinPageDefault1.ModuleID.CompareTo(skinPageDefault2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of SkinPageDefault.
        /// </summary>
        public static int CompareByModuleTitle(CoreSkinPageDefault skinPageDefault1, CoreSkinPageDefault skinPageDefault2)
        {
            return skinPageDefault1.ModuleTitle.CompareTo(skinPageDefault2.ModuleTitle);
        }
        /// <summary>
        /// Compares 2 instances of SkinPageDefault.
        /// </summary>
        public static int CompareByPaneName(CoreSkinPageDefault skinPageDefault1, CoreSkinPageDefault skinPageDefault2)
        {
            return skinPageDefault1.PaneName.CompareTo(skinPageDefault2.PaneName);
        }
        /// <summary>
        /// Compares 2 instances of SkinPageDefault.
        /// </summary>
        public static int CompareByModuleOrder(CoreSkinPageDefault skinPageDefault1, CoreSkinPageDefault skinPageDefault2)
        {
            return skinPageDefault1.ModuleOrder.CompareTo(skinPageDefault2.ModuleOrder);
        }

        public int CompareTo(object value)
        {
            if (value == null) return 1;

            int compareOrder = ((CoreSkinPageDefault)value).ModuleOrder;

            if (this.ModuleOrder == compareOrder) return 0;
            if (this.ModuleOrder < compareOrder) return -1;
            if (this.ModuleOrder > compareOrder) return 1;
            return 0;
        }

        #endregion


    }

}






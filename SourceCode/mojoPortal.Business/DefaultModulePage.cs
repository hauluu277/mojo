// Author:					Joe Audette
// Created:				    2004-12-26
// Last Modified:			2013-04-23
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
using System.Data;
using mojoPortal.Data;

namespace mojoPortal.Business
{
    /// <summary>
    /// Represents an instance of a feature
    /// </summary>
    public class DefaultModulePage : IComparable
    {

        #region Constructors

        public DefaultModulePage()
        { }

        public DefaultModulePage(int itemId)
        {
            GetDefaultModule(itemId);
        }

        #endregion

        #region Private Properties
        private int itemID = -1;
        private int moduleID = -1;
        private int siteID = 0;
        private int moduleOrder = 999;
        private string paneName = string.Empty;
        private string moduleTitle = string.Empty;
        #endregion

        #region Public Properties

        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public int ModuleId
        {
            get { return moduleID; }
            set { moduleID = value; }
        }
        public int SiteId
        {
            get { return siteID; }
            set { siteID = value; }
        }
        public int ModuleOrder
        {
            get { return moduleOrder; }
            set { moduleOrder = value; }
        }
        public string PaneName
        {
            get { return paneName; }
            set { paneName = value; }
        }
        public string ModuleTitle
        {
            get { return moduleTitle; }
            set { moduleTitle = value; }
        }
        #endregion

        #region Private Methods

        private void PopulateFromReader(IDataReader reader)
        {
            if (reader.Read())
            {
                this.itemID = Convert.ToInt32(reader["ItemID"]);
                this.moduleID = Convert.ToInt32(reader["ModuleID"]);
                this.siteID = Convert.ToInt32(reader["SiteID"]);
                this.moduleOrder = Convert.ToInt32(reader["ModuleOrder"]);
                this.paneName = reader["PaneName"].ToString();
                this.moduleTitle = reader["ModuleTitle"].ToString();
            }
        }

        private void GetDefaultModule(int itemId)
        {
            using (IDataReader reader = DBModule.GetDefaultModule(itemId))
            {
                PopulateFromReader(reader);
            }
        }


        #endregion

        #region Public Methods

        public bool Save()
        {
            if (this.itemID > -1)
            {
                return Update();
            }
            else
            {
                return Create();
            }
        }

        private bool Create()
        {
            int newID = -1;
            newID = DBModule.AddDefaultModule(this.siteID, this.moduleID, this.moduleTitle, this.paneName, this.moduleOrder);
            this.itemID = newID;
            return newID > -1;
        }

        private bool Update()
        {
            return DBModule.UpdateDefaultModule(this.itemID, this.siteID, this.moduleID, this.moduleTitle, this.paneName, this.moduleOrder);
        }

        #endregion


        #region Static Methods

        /// <summary>
        /// Returns a DataReader of published pagemodules
        /// </summary>
        /// <param name="pageID"></param>
        /// <returns></returns>
        public static IDataReader GetPageModules(int pageId)
        {
            return DBModule.GetPageModules(pageId);
        }

        public static DataTable GetPageModulesTable(int moduleId)
        {
            return DBModule.PageModuleGetByModule(moduleId);
        }

        public static void DeleteDefaultModule(int itemId)
        {
            DBModule.DeleteDefaultModule(itemId);
        }

        public static bool UpdateModuleOrder(int pageId, int moduleId, int moduleOrder, string paneName)
        {
            return DBModule.UpdateModuleOrder(pageId, moduleId, moduleOrder, paneName);
        }

        #endregion

        public static IDataReader GetDefaultModuleInPage4Site(int siteId)
        {
            return DBModule.GetDefaultModuleInPage4Site(siteId);
        }

        public static ArrayList RefreshDefaultModules(int siteId)
        {
            ArrayList modules = new ArrayList();
            using (IDataReader reader = GetDefaultModuleInPage4Site(siteId))
            {
                while (reader.Read())
                {
                    DefaultModulePage m = new DefaultModulePage();
                    m.ItemID = Convert.ToInt32(reader["ItemID"]);
                    m.ModuleId = Convert.ToInt32(reader["ModuleID"]);
                    m.PaneName = reader["PaneName"].ToString();
                    m.ModuleTitle = reader["DefautlTitle"].ToString();
                    m.ModuleOrder = Convert.ToInt32(reader["ModuleOrder"]);
                    modules.Add(m);
                }
            }
            return modules;
        }

        public static bool UpdateDefaultModuleOrder(int itemId, int moduleOrder, string paneName)
        {
            return DBModule.UpdateDefaultModuleOrder(itemId, moduleOrder, paneName);
        }

        public int CompareTo(object value)
        {

            if (value == null) return 1;

            int compareOrder = ((DefaultModulePage)value).ModuleOrder;

            if (this.ModuleOrder == compareOrder) return 0;
            if (this.ModuleOrder < compareOrder) return -1;
            if (this.ModuleOrder > compareOrder) return 1;
            return 0;
        }

    }

}

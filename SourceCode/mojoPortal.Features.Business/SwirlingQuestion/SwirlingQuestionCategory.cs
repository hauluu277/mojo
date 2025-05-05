
// Author:					HiNet JSC
// Created:					2014-6-16
// Last Modified:			2014-6-16
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
using SwirlingQuestionFeature.Data;

namespace SwirlingQuestionFeature.Business
{

    public class SwirlingQuestionCategory
    {

        #region Constructors

        public SwirlingQuestionCategory()
        { }


        public SwirlingQuestionCategory(
            int itemID)
        {
            GetSwirlingQuestionCategory(itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int parentID = 0;
        private int moduleID = -1;
        private string categoryName = string.Empty;
        private string description = string.Empty;
        private int sortOrder = -1;
        private DateTime lastModified = DateTime.UtcNow;

        #endregion

        #region Public Properties

        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public int ParentID
        {
            get { return parentID; }
            set { parentID = value; }
        }
        public int ModuleID
        {
            get { return moduleID; }
            set { moduleID = value; }
        }
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public int SortOrder
        {
            get { return sortOrder; }
            set { sortOrder = value; }
        }
        public DateTime LastModified
        {
            get { return lastModified; }
            set { lastModified = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of SwirlingQuestionCategories.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetSwirlingQuestionCategory(
            int itemID)
        {
            using (IDataReader reader = DBSwirlingQuestionCategories.GetOne(
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
                this.parentID = Convert.ToInt32(reader["ParentID"]);
                this.CategoryName = reader["CategoryName"].ToString();
                this.description = reader["Description"].ToString();
                this.sortOrder = Convert.ToInt32(reader["SortOrder"]);
                this.lastModified = Convert.ToDateTime(reader["LastModified"]);

            }

        }

        /// <summary>
        /// Persists a new instance of SwirlingQuestionCategories. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBSwirlingQuestionCategories.Create(
                this.parentID,
                this.moduleID,
                this.categoryName,
                this.description,
                this.sortOrder,
                this.lastModified);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of SwirlingQuestionCategories. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBSwirlingQuestionCategories.Update(
                this.itemID,
                this.parentID,
                this.moduleID,
                this.categoryName,
                this.description,
                this.sortOrder,
                this.lastModified);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of SwirlingQuestionCategories. Returns true on success.
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
        /// Deletes an instance of SwirlingQuestionCategories. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBSwirlingQuestionCategories.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of SwirlingQuestionCategories. 
        /// </summary>
        public static int GetCount()
        {
            return DBSwirlingQuestionCategories.GetCount();
        }

        private static List<SwirlingQuestionCategory> LoadListFromReader(IDataReader reader)
        {
            List<SwirlingQuestionCategory> SwirlingQuestionCategoriesList = new List<SwirlingQuestionCategory>();
            try
            {
                while (reader.Read())
                {
                    SwirlingQuestionCategory SwirlingQuestionCategories = new SwirlingQuestionCategory();
                    SwirlingQuestionCategories.itemID = Convert.ToInt32(reader["ItemID"]);
                    SwirlingQuestionCategories.parentID = Convert.ToInt32(reader["ParentID"]);
                    SwirlingQuestionCategories.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    SwirlingQuestionCategories.CategoryName = reader["CategoryName"].ToString();
                    SwirlingQuestionCategories.description = reader["Description"].ToString();
                    SwirlingQuestionCategories.sortOrder = Convert.ToInt32(reader["SortOrder"]);
                    SwirlingQuestionCategories.lastModified = Convert.ToDateTime(reader["LastModified"]);
                    SwirlingQuestionCategoriesList.Add(SwirlingQuestionCategories);

                }
            }
            finally
            {
                reader.Close();
            }

            return SwirlingQuestionCategoriesList;

        }

        /// <summary>
        /// Gets an IList with all instances of SwirlingQuestionCategories.
        /// </summary>
        public static List<SwirlingQuestionCategory> GetAll()
        {
            IDataReader reader = DBSwirlingQuestionCategories.GetAll();
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of SwirlingQuestionCategories.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<SwirlingQuestionCategory> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBSwirlingQuestionCategories.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }


        public static List<SwirlingQuestionCategory> GetRoot(int moduleID)
        {
            IDataReader reader = DBSwirlingQuestionCategories.GetRoot(moduleID);
            return LoadListFromReader(reader);
        }

        public static List<SwirlingQuestionCategory> GetChildren(int categoryId)
        {
            IDataReader reader = DBSwirlingQuestionCategories.GetChildren(categoryId);
            return LoadListFromReader(reader);
        }

        public static SwirlingQuestionCategory GetParent(int categoryId)
        {
            SwirlingQuestionCategory item = new SwirlingQuestionCategory(categoryId);
            SwirlingQuestionCategory result = new SwirlingQuestionCategory();
            if (item != null)
            {
                IDataReader reader = DBSwirlingQuestionCategories.GetParent(item.ParentID);
                var listResult = LoadListFromReader(reader);
                if (listResult != null && listResult.Count > 0)
                    result = LoadListFromReader(reader)[0];
            }
            return result;
        }

        public static void GetParentCategory(int categoryID, ref List<SwirlingQuestionCategory> listCategory)
        {
            SwirlingQuestionCategory parent = GetParent(categoryID);
            if (parent != null && parent.ItemID > 0)
            {
                listCategory.Add(parent);
                GetParentCategory(parent.ItemID, ref listCategory);
            }
        }


        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of SwirlingQuestionCategories.
        /// </summary>
        public static int CompareByItemID(SwirlingQuestionCategory SwirlingQuestionCategories1, SwirlingQuestionCategory SwirlingQuestionCategories2)
        {
            return SwirlingQuestionCategories1.ItemID.CompareTo(SwirlingQuestionCategories2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of SwirlingQuestionCategories.
        /// </summary>
        public static int CompareByModuleID(SwirlingQuestionCategory SwirlingQuestionCategories1, SwirlingQuestionCategory SwirlingQuestionCategories2)
        {
            return SwirlingQuestionCategories1.ModuleID.CompareTo(SwirlingQuestionCategories2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of SwirlingQuestionCategories.
        /// </summary>
        public static int CompareBySwirlingQuestionCategory(SwirlingQuestionCategory SwirlingQuestionCategories1, SwirlingQuestionCategory SwirlingQuestionCategories2)
        {
            return SwirlingQuestionCategories1.CategoryName.CompareTo(SwirlingQuestionCategories2.CategoryName);
        }
        /// <summary>
        /// Compares 2 instances of SwirlingQuestionCategories.
        /// </summary>
        public static int CompareByDescription(SwirlingQuestionCategory SwirlingQuestionCategories1, SwirlingQuestionCategory SwirlingQuestionCategories2)
        {
            return SwirlingQuestionCategories1.Description.CompareTo(SwirlingQuestionCategories2.Description);
        }
        /// <summary>
        /// Compares 2 instances of SwirlingQuestionCategories.
        /// </summary>
        public static int CompareBySortOrder(SwirlingQuestionCategory SwirlingQuestionCategories1, SwirlingQuestionCategory SwirlingQuestionCategories2)
        {
            return SwirlingQuestionCategories1.SortOrder.CompareTo(SwirlingQuestionCategories2.SortOrder);
        }
        /// <summary>
        /// Compares 2 instances of SwirlingQuestionCategories.
        /// </summary>
        public static int CompareByLastModified(SwirlingQuestionCategory SwirlingQuestionCategories1, SwirlingQuestionCategory SwirlingQuestionCategories2)
        {
            return SwirlingQuestionCategories1.LastModified.CompareTo(SwirlingQuestionCategories2.LastModified);
        }

        #endregion


    }

}






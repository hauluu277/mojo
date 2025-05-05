
// Author:					Joe Audette
// Created:					2017-12-23
// Last Modified:			2017-12-23
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

    public class CoreSkinCategory
    {

        #region Constructors

        public CoreSkinCategory()
        { }


        public CoreSkinCategory(
            int itemID)
        {
            GetSkinCategory(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int parentID = -1;
        private string categoryName = string.Empty;
        private int skinID = -1;
        private string urlDescription = string.Empty;
        private int orderBy = -1;
        private bool isCategoryTemplate = false;
        private DateTime dateCreate = DateTime.Now;
        private int createBy = -1;
        private string parentName = string.Empty;

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
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }
        public int SkinID
        {
            get { return skinID; }
            set { skinID = value; }
        }
        public string UrlDescription
        {
            get { return urlDescription; }
            set { urlDescription = value; }
        }
        public int OrderBy
        {
            get { return orderBy; }
            set { orderBy = value; }
        }
        public bool IsCategoryTemplate
        {
            get { return isCategoryTemplate; }
            set { isCategoryTemplate = value; }
        }
        public DateTime DateCreate
        {
            get { return dateCreate; }
            set { dateCreate = value; }
        }
        public int CreateBy
        {
            get { return createBy; }
            set { createBy = value; }
        }

        public string ParentName
        {
            get { return parentName;}
            set { parentName = value; }
        }


        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of SkinCategory.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetSkinCategory(
            int itemID)
        {
            using (IDataReader reader = DBCoreSkinCategory.GetOne(
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
                this.parentID = Convert.ToInt32(reader["ParentID"]);
                this.categoryName = reader["CategoryName"].ToString();
                this.skinID = Convert.ToInt32(reader["SkinID"]);
                this.urlDescription = reader["UrlDescription"].ToString();
                this.orderBy = Convert.ToInt32(reader["OrderBy"]);
                this.isCategoryTemplate = Convert.ToBoolean(reader["IsCategoryTemplate"]);
                this.dateCreate = Convert.ToDateTime(reader["DateCreate"]);
                this.createBy = Convert.ToInt32(reader["CreateBy"]);


            }

        }

        /// <summary>
        /// Persists a new instance of SkinCategory. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBCoreSkinCategory.Create(
                this.parentID,
                this.categoryName,
                this.skinID,
                this.urlDescription,
                this.orderBy,
                this.isCategoryTemplate,
                this.dateCreate,
                this.createBy);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of SkinCategory. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBCoreSkinCategory.Update(
                this.itemID,
                this.parentID,
                this.categoryName,
                this.skinID,
                this.urlDescription,
                this.orderBy,
                this.isCategoryTemplate,
                this.dateCreate,
                this.createBy);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of SkinCategory. Returns true on success.
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
        /// Deletes an instance of SkinCategory. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBCoreSkinCategory.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of SkinCategory. 
        /// </summary>
        public static int GetCount()
        {
            return DBCoreSkinCategory.GetCount();
        }
        private static List<CoreSkinCategory> LoadListFromReaderParent(IDataReader reader)
        {
            List<CoreSkinCategory> skinCategoryList = new List<CoreSkinCategory>();
            try
            {
                while (reader.Read())
                {
                    CoreSkinCategory skinCategory = new CoreSkinCategory();
                    skinCategory.itemID = Convert.ToInt32(reader["ItemID"]);
                    skinCategory.parentID = Convert.ToInt32(reader["ParentID"]);
                    skinCategory.categoryName = reader["CategoryName"].ToString();
                    skinCategory.skinID = Convert.ToInt32(reader["SkinID"]);
                    skinCategory.urlDescription = reader["UrlDescription"].ToString();
                    skinCategory.orderBy = Convert.ToInt32(reader["OrderBy"]);
                    skinCategory.isCategoryTemplate = Convert.ToBoolean(reader["IsCategoryTemplate"]);
                    skinCategory.dateCreate = Convert.ToDateTime(reader["DateCreate"]);
                    skinCategory.createBy = Convert.ToInt32(reader["CreateBy"]);

                    if (!string.IsNullOrEmpty(reader["ParentName"].ToString()))
                    {
                        skinCategory.parentName = reader["ParentName"].ToString();
                    }
                    skinCategoryList.Add(skinCategory);

                }
            }
            finally
            {
                reader.Close();
            }

            return skinCategoryList;

        }
        private static List<CoreSkinCategory> LoadListFromReader(IDataReader reader)
        {
            List<CoreSkinCategory> skinCategoryList = new List<CoreSkinCategory>();
            try
            {
                while (reader.Read())
                {
                    CoreSkinCategory skinCategory = new CoreSkinCategory();
                    skinCategory.itemID = Convert.ToInt32(reader["ItemID"]);
                    skinCategory.parentID = Convert.ToInt32(reader["ParentID"]);
                    skinCategory.categoryName = reader["CategoryName"].ToString();
                    skinCategory.skinID = Convert.ToInt32(reader["SkinID"]);
                    skinCategory.urlDescription = reader["UrlDescription"].ToString();
                    skinCategory.orderBy = Convert.ToInt32(reader["OrderBy"]);
                    skinCategory.isCategoryTemplate = Convert.ToBoolean(reader["IsCategoryTemplate"]);
                    skinCategory.dateCreate = Convert.ToDateTime(reader["DateCreate"]);
                    skinCategory.createBy = Convert.ToInt32(reader["CreateBy"]);
                    skinCategoryList.Add(skinCategory);

                }
            }
            finally
            {
                reader.Close();
            }

            return skinCategoryList;

        }

        /// <summary>
        /// Gets an IList with all instances of SkinCategory.
        /// </summary>
        public static List<CoreSkinCategory> GetAll()
        {
            IDataReader reader = DBCoreSkinCategory.GetAll();
            return LoadListFromReaderParent(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of SkinCategory.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<CoreSkinCategory> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBCoreSkinCategory.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of SkinCategory.
        /// </summary>
        public static int CompareByItemID(CoreSkinCategory skinCategory1, CoreSkinCategory skinCategory2)
        {
            return skinCategory1.ItemID.CompareTo(skinCategory2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of SkinCategory.
        /// </summary>
        public static int CompareByParentID(CoreSkinCategory skinCategory1, CoreSkinCategory skinCategory2)
        {
            return skinCategory1.ParentID.CompareTo(skinCategory2.ParentID);
        }
        /// <summary>
        /// Compares 2 instances of SkinCategory.
        /// </summary>
        public static int CompareByCategoryName(CoreSkinCategory skinCategory1, CoreSkinCategory skinCategory2)
        {
            return skinCategory1.CategoryName.CompareTo(skinCategory2.CategoryName);
        }
        /// <summary>
        /// Compares 2 instances of SkinCategory.
        /// </summary>
        public static int CompareBySkinID(CoreSkinCategory skinCategory1, CoreSkinCategory skinCategory2)
        {
            return skinCategory1.SkinID.CompareTo(skinCategory2.SkinID);
        }
        /// <summary>
        /// Compares 2 instances of SkinCategory.
        /// </summary>
        public static int CompareByUrlDescription(CoreSkinCategory skinCategory1, CoreSkinCategory skinCategory2)
        {
            return skinCategory1.UrlDescription.CompareTo(skinCategory2.UrlDescription);
        }
        /// <summary>
        /// Compares 2 instances of SkinCategory.
        /// </summary>
        public static int CompareByOrderBy(CoreSkinCategory skinCategory1, CoreSkinCategory skinCategory2)
        {
            return skinCategory1.OrderBy.CompareTo(skinCategory2.OrderBy);
        }
        /// <summary>
        /// Compares 2 instances of SkinCategory.
        /// </summary>
        public static int CompareByDateCreate(CoreSkinCategory skinCategory1, CoreSkinCategory skinCategory2)
        {
            return skinCategory1.DateCreate.CompareTo(skinCategory2.DateCreate);
        }
        /// <summary>
        /// Compares 2 instances of SkinCategory.
        /// </summary>
        public static int CompareByCreateBy(CoreSkinCategory skinCategory1, CoreSkinCategory skinCategory2)
        {
            return skinCategory1.CreateBy.CompareTo(skinCategory2.CreateBy);
        }

        #endregion


    }

}







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

    public class CoreSkinPage
    {

        #region Constructors

        public CoreSkinPage()
        { }


        public CoreSkinPage(
            int itemID)
        {
            GetSkinPage(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int parentID = -1;
        private int skinID = -1;
        private string title = string.Empty;
        private int orderBy = -1;
        private DateTime createDate = DateTime.UtcNow;
        private int userCreate = -1;
        private string parentName = string.Empty;
        private string categoryName = string.Empty;
        private int categoryID = -1;

        #endregion

        #region Public Properties

        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }
        public string ParentName
        {
            get { return parentName; }
            set { parentName = value; }
        }
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
        public int SkinID
        {
            get { return skinID; }
            set { skinID = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public int OrderBy
        {
            get { return orderBy; }
            set { orderBy = value; }
        }
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
        public int UserCreate
        {
            get { return userCreate; }
            set { userCreate = value; }
        }

        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of SkinPage.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetSkinPage(
            int itemID)
        {
            using (IDataReader reader = DBCoreSkinPage.GetOne(
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
                this.skinID = Convert.ToInt32(reader["SkinID"]);
                this.title = reader["Title"].ToString();
                this.orderBy = Convert.ToInt32(reader["OrderBy"]);
                this.createDate = Convert.ToDateTime(reader["CreateDate"]);
                this.userCreate = Convert.ToInt32(reader["UserCreate"]);
                if (!string.IsNullOrEmpty(reader["CategoryID"].ToString()))
                {
                    this.categoryID = Convert.ToInt32(reader["CategoryID"].ToString());
                }
            }

        }

        /// <summary>
        /// Persists a new instance of SkinPage. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBCoreSkinPage.Create(
                this.parentID,
                this.skinID,
                this.title,
                this.orderBy,
                this.createDate,
                this.userCreate,
                this.categoryID);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of SkinPage. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBCoreSkinPage.Update(
                this.itemID,
                this.parentID,
                this.skinID,
                this.title,
                this.orderBy,
                this.createDate,
                this.userCreate,
                this.categoryID);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of SkinPage. Returns true on success.
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
        /// Deletes an instance of SkinPage. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBCoreSkinPage.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of SkinPage. 
        /// </summary>
        public static int GetCount()
        {
            return DBCoreSkinPage.GetCount();
        }
        private static List<CoreSkinPage> LoadListFromReaderForSkin(IDataReader reader)
        {
            List<CoreSkinPage> skinPageList = new List<CoreSkinPage>();
            try
            {
                while (reader.Read())
                {
                    CoreSkinPage skinPage = new CoreSkinPage();
                    skinPage.itemID = Convert.ToInt32(reader["ItemID"]);
                    if (!string.IsNullOrEmpty(reader["ParentName"].ToString()))
                    {
                        skinPage.parentName = reader["ParentName"].ToString();
                    }
                    skinPage.parentID = Convert.ToInt32(reader["ParentID"]);
                    skinPage.skinID = Convert.ToInt32(reader["SkinID"]);
                    skinPage.title = reader["Title"].ToString();
                    skinPage.orderBy = Convert.ToInt32(reader["OrderBy"]);
                    skinPage.createDate = Convert.ToDateTime(reader["CreateDate"]);
                    skinPage.userCreate = Convert.ToInt32(reader["UserCreate"]);
                    if (!string.IsNullOrEmpty(reader["CategoryName"].ToString()))
                    {
                        skinPage.categoryName = reader["CategoryName"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["CategoryID"].ToString()))
                    {
                        skinPage.categoryID = Convert.ToInt32(reader["CategoryID"].ToString());
                    }
                    skinPageList.Add(skinPage);

                }
            }
            finally
            {
                reader.Close();
            }

            return skinPageList;

        }

        private static List<CoreSkinPage> LoadListFromReader(IDataReader reader)
        {
            List<CoreSkinPage> skinPageList = new List<CoreSkinPage>();
            try
            {
                while (reader.Read())
                {
                    CoreSkinPage skinPage = new CoreSkinPage();
                    skinPage.itemID = Convert.ToInt32(reader["ItemID"]);

                    skinPage.parentID = Convert.ToInt32(reader["ParentID"]);
                    skinPage.skinID = Convert.ToInt32(reader["SkinID"]);
                    skinPage.title = reader["Title"].ToString();
                    skinPage.orderBy = Convert.ToInt32(reader["OrderBy"]);
                    skinPage.createDate = Convert.ToDateTime(reader["CreateDate"]);
                    skinPage.userCreate = Convert.ToInt32(reader["UserCreate"]);
                    if (!string.IsNullOrEmpty(reader["CategoryID"].ToString()))
                    {
                        skinPage.categoryID = Convert.ToInt32(reader["CategoryID"].ToString());
                    }
                    skinPageList.Add(skinPage);

                }
            }
            finally
            {
                reader.Close();
            }

            return skinPageList;

        }

        /// <summary>
        /// Gets an IList with all instances of SkinPage.
        /// </summary>
        public static List<CoreSkinPage> GetAll()
        {
            IDataReader reader = DBCoreSkinPage.GetAll();
            return LoadListFromReader(reader);

        }
        public static List<CoreSkinPage> GetAllBySkin(int skinID)
        {
            IDataReader reader = DBCoreSkinPage.GetAllBySkin(skinID);
            return LoadListFromReaderForSkin(reader);
        }
        /// <summary>
        /// Gets an IList with page of instances of SkinPage.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<CoreSkinPage> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBCoreSkinPage.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of SkinPage.
        /// </summary>
        public static int CompareByItemID(CoreSkinPage skinPage1, CoreSkinPage skinPage2)
        {
            return skinPage1.ItemID.CompareTo(skinPage2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of SkinPage.
        /// </summary>
        public static int CompareByParentID(CoreSkinPage skinPage1, CoreSkinPage skinPage2)
        {
            return skinPage1.ParentID.CompareTo(skinPage2.ParentID);
        }
        /// <summary>
        /// Compares 2 instances of SkinPage.
        /// </summary>
        public static int CompareBySkinID(CoreSkinPage skinPage1, CoreSkinPage skinPage2)
        {
            return skinPage1.SkinID.CompareTo(skinPage2.SkinID);
        }
        /// <summary>
        /// Compares 2 instances of SkinPage.
        /// </summary>
        public static int CompareByTitle(CoreSkinPage skinPage1, CoreSkinPage skinPage2)
        {
            return skinPage1.Title.CompareTo(skinPage2.Title);
        }
        /// <summary>
        /// Compares 2 instances of SkinPage.
        /// </summary>
        public static int CompareByOrderBy(CoreSkinPage skinPage1, CoreSkinPage skinPage2)
        {
            return skinPage1.OrderBy.CompareTo(skinPage2.OrderBy);
        }
        /// <summary>
        /// Compares 2 instances of SkinPage.
        /// </summary>
        public static int CompareByCreateDate(CoreSkinPage skinPage1, CoreSkinPage skinPage2)
        {
            return skinPage1.CreateDate.CompareTo(skinPage2.CreateDate);
        }
        /// <summary>
        /// Compares 2 instances of SkinPage.
        /// </summary>
        public static int CompareByUserCreate(CoreSkinPage skinPage1, CoreSkinPage skinPage2)
        {
            return skinPage1.UserCreate.CompareTo(skinPage2.UserCreate);
        }

        #endregion


    }

}







// Author:					Joe Audette
// Created:					2017-12-21
// Last Modified:			2017-12-21
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

    public class CoreSkin
    {

        #region Constructors

        public CoreSkin()
        { }


        public CoreSkin(
            int itemID)
        {
            GetSkin(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int skinType = -1;
        private string title = string.Empty;
        private int orderBy = -1;
        private DateTime createDate = DateTime.UtcNow;
        private int userCreate = -1;
        private string skinTypeName = string.Empty;
        private int categoryArticle = -1;
        private string categoryName = string.Empty;

        #endregion

        #region Public Properties

        public string SkinTypeName
        {
            get { return skinTypeName; }
            set { skinTypeName = value; }
        }

        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public int SkinType
        {
            get { return skinType; }
            set { skinType = value; }
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

        public int CategoryArticle
        {

            get { return categoryArticle; }
            set { categoryArticle = value; }
        }

        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of Skin.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetSkin(
            int itemID)
        {
            using (IDataReader reader = DBCoreSkin.GetOne(
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
                this.skinType = Convert.ToInt32(reader["SkinType"]);
                this.title = reader["Title"].ToString();
                this.orderBy = Convert.ToInt32(reader["OrderBy"]);
                this.createDate = Convert.ToDateTime(reader["CreateDate"]);
                this.userCreate = Convert.ToInt32(reader["UserCreate"]);
                if (!string.IsNullOrEmpty(reader["CategoryArticle"].ToString()))
                {
                    this.categoryArticle = Convert.ToInt32(reader["CategoryArticle"].ToString());
                }

            }

        }

        /// <summary>
        /// Persists a new instance of Skin. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBCoreSkin.Create(
                this.skinType,
                this.title,
                this.orderBy,
                this.createDate,
                this.userCreate,
                this.categoryArticle);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of Skin. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBCoreSkin.Update(
                this.itemID,
                this.skinType,
                this.title,
                this.orderBy,
                this.createDate,
                this.userCreate,
                this.categoryArticle);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of Skin. Returns true on success.
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
        /// Deletes an instance of Skin. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBCoreSkin.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of Skin. 
        /// </summary>
        public static int GetCount()
        {
            return DBCoreSkin.GetCount();
        }


        private static List<CoreSkin> LoadListFromReader(IDataReader reader)
        {
            List<CoreSkin> skinList = new List<CoreSkin>();
            try
            {
                while (reader.Read())
                {
                    CoreSkin skin = new CoreSkin();
                    skin.itemID = Convert.ToInt32(reader["ItemID"]);
                    skin.skinType = Convert.ToInt32(reader["SkinType"]);
                    skin.title = reader["Title"].ToString();
                    skin.orderBy = Convert.ToInt32(reader["OrderBy"]);
                    skin.createDate = Convert.ToDateTime(reader["CreateDate"]);
                    skin.userCreate = Convert.ToInt32(reader["UserCreate"]);
                    if (!string.IsNullOrEmpty(reader["CategoryArticle"].ToString()))
                    {
                        skin.categoryArticle = Convert.ToInt32(reader["CategoryArticle"].ToString());
                    }
                    skinList.Add(skin);

                }
            }
            finally
            {
                reader.Close();
            }

            return skinList;

        }

        private static List<CoreSkin> LoadListFromReaderAll(IDataReader reader)
        {
            List<CoreSkin> skinList = new List<CoreSkin>();
            try
            {
                while (reader.Read())
                {
                    CoreSkin skin = new CoreSkin();
                    skin.itemID = Convert.ToInt32(reader["ItemID"]);
                    skin.skinType = Convert.ToInt32(reader["SkinType"]);
                    skin.title = reader["Title"].ToString();
                    skin.orderBy = Convert.ToInt32(reader["OrderBy"]);
                    skin.createDate = Convert.ToDateTime(reader["CreateDate"]);
                    skin.userCreate = Convert.ToInt32(reader["UserCreate"]);
                    if (!string.IsNullOrEmpty(reader["CategoryArticle"].ToString()))
                    {
                        skin.categoryArticle = Convert.ToInt32(reader["CategoryArticle"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["CategoryName"].ToString()))
                    {
                        skin.categoryName =reader["CategoryName"].ToString();
                    }
                    skinList.Add(skin);

                }
            }
            finally
            {
                reader.Close();
            }

            return skinList;

        }

    /// <summary>
    /// Gets an IList with all instances of Skin.
    /// </summary>
    public static List<CoreSkin> GetAll()
        {
            IDataReader reader = DBCoreSkin.GetAll();
            return LoadListFromReaderAll(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of Skin.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<CoreSkin> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBCoreSkin.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of Skin.
        /// </summary>
        public static int CompareByItemID(CoreSkin skin1, CoreSkin skin2)
        {
            return skin1.ItemID.CompareTo(skin2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of Skin.
        /// </summary>
        public static int CompareBySkin(CoreSkin skin1, CoreSkin skin2)
        {
            return skin1.SkinType.CompareTo(skin2.SkinType);
        }
        /// <summary>
        /// Compares 2 instances of Skin.
        /// </summary>
        public static int CompareByTitle(CoreSkin skin1, CoreSkin skin2)
        {
            return skin1.Title.CompareTo(skin2.Title);
        }
        /// <summary>
        /// Compares 2 instances of Skin.
        /// </summary>
        public static int CompareByOrderBy(CoreSkin skin1, CoreSkin skin2)
        {
            return skin1.OrderBy.CompareTo(skin2.OrderBy);
        }
        /// <summary>
        /// Compares 2 instances of Skin.
        /// </summary>
        public static int CompareByCreateDate(CoreSkin skin1, CoreSkin skin2)
        {
            return skin1.CreateDate.CompareTo(skin2.CreateDate);
        }
        /// <summary>
        /// Compares 2 instances of Skin.
        /// </summary>
        public static int CompareByUserCreate(CoreSkin skin1, CoreSkin skin2)
        {
            return skin1.UserCreate.CompareTo(skin2.UserCreate);
        }

        #endregion


    }

}







// Author:					HiNet
// Created:					2015-3-18
// Last Modified:			2015-3-18
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

    public class CategoryLink
    {

        #region Constructors

        public CategoryLink()
        { }


        public CategoryLink(
            int itemID)
        {
            Getcore_CategoryLink(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int siteID = -1;
        private string name = string.Empty;
        private string nameEN = string.Empty;
        private DateTime createdUtc = DateTime.UtcNow;
        private Guid createdBy = Guid.Empty;
        private int orderBy = 0;

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
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string NameEN
        {
            get { return nameEN; }
            set { nameEN = value; }
        }
        public DateTime CreatedUtc
        {
            get { return createdUtc; }
            set { createdUtc = value; }
        }
        public Guid CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }
        public int OrderBy
        {
            get { return orderBy; }
            set { orderBy = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of core_CategoryLink.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void Getcore_CategoryLink(
            int itemID)
        {
            using (IDataReader reader = DBCategoryLink.GetOne(
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
                this.name = reader["Name"].ToString();
                this.nameEN = reader["NameEN"].ToString();
                this.createdUtc = Convert.ToDateTime(reader["CreatedUtc"]);
                this.createdBy = new Guid(reader["CreatedBy"].ToString());
                if (!string.IsNullOrEmpty(reader["OrderBy"].ToString()))
                {
                    this.orderBy = Convert.ToInt32(reader["OrderBy"]);
                }

            }

        }

        /// <summary>
        /// Persists a new instance of core_CategoryLink. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBCategoryLink.Create(
                this.siteID,
                this.name,
                this.nameEN,
                this.createdUtc,
                this.createdBy,
                this.orderBy);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of core_CategoryLink. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBCategoryLink.Update(
                this.itemID,
                this.siteID,
                this.name,
                this.nameEN,
                this.createdUtc,
                this.createdBy,
                this.orderBy);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of core_CategoryLink. Returns true on success.
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
        /// Deletes an instance of core_CategoryLink. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBCategoryLink.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of core_CategoryLink. 
        /// </summary>
        public static int GetCount(int siteId, string keyword)
        {
            return DBCategoryLink.GetCount(siteId, keyword);
        }

        private static List<CategoryLink> LoadListFromReader(IDataReader reader)
        {
            List<CategoryLink> core_CategoryLinkList = new List<CategoryLink>();
            try
            {
                while (reader.Read())
                {
                    CategoryLink core_CategoryLink = new CategoryLink();
                    core_CategoryLink.itemID = Convert.ToInt32(reader["ItemID"]);
                    core_CategoryLink.siteID = Convert.ToInt32(reader["SiteID"]);
                    core_CategoryLink.name = reader["Name"].ToString();
                    core_CategoryLink.nameEN = reader["NameEN"].ToString();
                    core_CategoryLink.createdUtc = Convert.ToDateTime(reader["CreatedUtc"]);
                    core_CategoryLink.createdBy = new Guid(reader["CreatedBy"].ToString());
                    if (!string.IsNullOrEmpty(reader["OrderBy"].ToString()))
                    {
                        core_CategoryLink.orderBy = Convert.ToInt32(reader["OrderBy"]);
                    }
                    core_CategoryLinkList.Add(core_CategoryLink);

                }
            }
            finally
            {
                reader.Close();
            }

            return core_CategoryLinkList;

        }

        /// <summary>
        /// Gets an IList with all instances of core_CategoryLink.
        /// </summary>
        public static List<CategoryLink> GetAll(int siteID)
        {
            IDataReader reader = DBCategoryLink.GetAll(siteID);
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of core_CategoryLink.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<CategoryLink> GetPage(int siteId, int pageNumber, int pageSize, string keyword, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBCategoryLink.GetPage(siteId, pageNumber, pageSize, keyword, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of core_CategoryLink.
        /// </summary>
        public static int CompareByItemID(CategoryLink core_CategoryLink1, CategoryLink core_CategoryLink2)
        {
            return core_CategoryLink1.ItemID.CompareTo(core_CategoryLink2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of core_CategoryLink.
        /// </summary>
        public static int CompareBySiteID(CategoryLink core_CategoryLink1, CategoryLink core_CategoryLink2)
        {
            return core_CategoryLink1.SiteID.CompareTo(core_CategoryLink2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of core_CategoryLink.
        /// </summary>
        public static int CompareByName(CategoryLink core_CategoryLink1, CategoryLink core_CategoryLink2)
        {
            return core_CategoryLink1.Name.CompareTo(core_CategoryLink2.Name);
        }
        /// <summary>
        /// Compares 2 instances of core_CategoryLink.
        /// </summary>
        public static int CompareByCreatedUtc(CategoryLink core_CategoryLink1, CategoryLink core_CategoryLink2)
        {
            return core_CategoryLink1.CreatedUtc.CompareTo(core_CategoryLink2.CreatedUtc);
        }

        #endregion


    }

}







// Author:					HiNet
// Created:					2015-8-31
// Last Modified:			2015-8-31
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

    public class CoreLink
    {

        #region Constructors

        public CoreLink()
        { }


        public CoreLink(
            int itemID)
        {
            GetCoreLink(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int siteID = -1;
        private int categoryID = -1;
        private string name = string.Empty;
        private string nameEN = string.Empty;
        private string description = string.Empty;
        private string url = string.Empty;
        private int itemCount = -1;
        private DateTime createdUtc = DateTime.UtcNow;
        private Guid createdBy = Guid.Empty;
        private DateTime modifiedUtc = DateTime.UtcNow;
        private Guid modifiedBy = Guid.Empty;
        private int priority = -1;
        private string categoryName = string.Empty;
        private string categoryNameEN = string.Empty;

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
        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
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
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        public int ItemCount
        {
            get { return itemCount; }
            set { itemCount = value; }
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
        public DateTime ModifiedUtc
        {
            get { return modifiedUtc; }
            set { modifiedUtc = value; }
        }
        public Guid ModifiedBy
        {
            get { return modifiedBy; }
            set { modifiedBy = value; }
        }
        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }
        public string CategoryNameEN
        {
            get { return categoryNameEN; }
            set { categoryNameEN = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of CoreLink.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetCoreLink(
            int itemID)
        {
            using (IDataReader reader = DBCoreLink.GetOne(
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
                this.categoryID = Convert.ToInt32(reader["CategoryID"]);
                this.name = reader["Name"].ToString();
                this.nameEN = reader["NameEN"].ToString();
                this.description = reader["Description"].ToString();
                this.url = reader["Url"].ToString();
                this.itemCount = Convert.ToInt32(reader["ItemCount"]);
                this.createdUtc = Convert.ToDateTime(reader["CreatedUtc"]);
                this.createdBy = new Guid(reader["CreatedBy"].ToString());
                this.modifiedUtc = Convert.ToDateTime(reader["ModifiedUtc"]);
                this.modifiedBy = new Guid(reader["ModifiedBy"].ToString());
                this.priority = Convert.ToInt32(reader["Priority"]);

            }

        }

        /// <summary>
        /// Persists a new instance of CoreLink. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBCoreLink.Create(
                this.siteID,
                this.categoryID,
                this.name,
                this.nameEN,
                this.description,
                this.url,
                this.itemCount,
                this.createdUtc,
                this.createdBy,
                this.modifiedUtc,
                this.modifiedBy,
                this.priority);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of CoreLink. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBCoreLink.Update(
                this.itemID,
                this.siteID,
                this.categoryID,
                this.name,
                this.nameEN,
                this.description,
                this.url,
                this.itemCount,
                this.createdUtc,
                this.createdBy,
                this.modifiedUtc,
                this.modifiedBy,
                this.priority);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of CoreLink. Returns true on success.
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
        /// Deletes an instance of CoreLink. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBCoreLink.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of CoreLink. 
        /// </summary>
        public static int GetCount(int siteId, int categoryId, string keyword)
        {
            return DBCoreLink.GetCount(siteId, categoryId, keyword);
        }

        private static List<CoreLink> LoadListFromReader(IDataReader reader, bool getpage = false, bool getbycatid = false)
        {
            List<CoreLink> CoreLinkList = new List<CoreLink>();
            try
            {
                while (reader.Read())
                {
                    CoreLink CoreLink = new CoreLink();
                    CoreLink.itemID = Convert.ToInt32(reader["ItemID"]);
                    CoreLink.siteID = Convert.ToInt32(reader["SiteID"]);
                    CoreLink.categoryID = Convert.ToInt32(reader["CategoryID"]);
                    CoreLink.name = reader["Name"].ToString();
                    CoreLink.nameEN = reader["NameEN"].ToString();
                    if (!string.IsNullOrEmpty(reader["Description"].ToString()))
                    {
                        CoreLink.description = reader["Description"].ToString();
                    }
                    CoreLink.url = reader["Url"].ToString();
                    CoreLink.itemCount = Convert.ToInt32(reader["ItemCount"]);
                    CoreLink.createdUtc = Convert.ToDateTime(reader["CreatedUtc"]);
                    CoreLink.createdBy = new Guid(reader["CreatedBy"].ToString());
                    CoreLink.modifiedUtc = Convert.ToDateTime(reader["ModifiedUtc"]);
                    CoreLink.modifiedBy = new Guid(reader["ModifiedBy"].ToString());
                    CoreLink.priority = Convert.ToInt32(reader["Priority"]);
                    if (getpage)
                    {
                        CoreLink.categoryName = reader["CategoryName"].ToString();
                    }
                    CoreLinkList.Add(CoreLink);
                }
            }
            finally
            {
                reader.Close();
            }

            return CoreLinkList;

        }

        /// <summary>
        /// Gets an IList with all instances of CoreLink.
        /// </summary>
        public static List<CoreLink> GetAll()
        {
            IDataReader reader = DBCoreLink.GetAll();
            return LoadListFromReader(reader);

        }

        public static List<CoreLink> GetByCatIdOrderBy(int categoryId, int oderBy)
        {
            IDataReader reader = DBCoreLink.GetByCatIdOrderBy(categoryId, oderBy);
            return LoadListFromReader(reader, true);

        }
        public static List<CoreLink> GetByCatId(int categoryId)
        {
            IDataReader reader = DBCoreLink.GetByCatId(categoryId);
            return LoadListFromReader(reader, true);

        }
        public static List<CoreLink> GetChildByCatId(int categoryId, int parentId, int SiteId)
        {
            IDataReader reader = DBCoreLink.GetChildByCatId(categoryId, parentId, SiteId);
            return LoadListFromReader(reader, true);

        }

        /// <summary>
        /// Gets an IList with page of instances of CoreLink.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<CoreLink> GetPage(int siteId, int categoryId, int pageNumber, int pageSize, string keyword, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBCoreLink.GetPage(siteId, categoryId, pageNumber, pageSize, keyword, out totalPages);
            return LoadListFromReader(reader, true);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of CoreLink.
        /// </summary>
        public static int CompareByItemID(CoreLink CoreLink1, CoreLink CoreLink2)
        {
            return CoreLink1.ItemID.CompareTo(CoreLink2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of CoreLink.
        /// </summary>
        public static int CompareBySiteID(CoreLink CoreLink1, CoreLink CoreLink2)
        {
            return CoreLink1.SiteID.CompareTo(CoreLink2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of CoreLink.
        /// </summary>
        public static int CompareByName(CoreLink CoreLink1, CoreLink CoreLink2)
        {
            return CoreLink1.Name.CompareTo(CoreLink2.Name);
        }
        /// <summary>
        /// Compares 2 instances of CoreLink.
        /// </summary>
        public static int CompareByNameEN(CoreLink CoreLink1, CoreLink CoreLink2)
        {
            return CoreLink1.NameEN.CompareTo(CoreLink2.NameEN);
        }
        /// <summary>
        /// Compares 2 instances of CoreLink.
        /// </summary>
        public static int CompareByDescription(CoreLink CoreLink1, CoreLink CoreLink2)
        {
            return CoreLink1.Description.CompareTo(CoreLink2.Description);
        }
        /// <summary>
        /// Compares 2 instances of CoreLink.
        /// </summary>
        public static int CompareByUrl(CoreLink CoreLink1, CoreLink CoreLink2)
        {
            return CoreLink1.Url.CompareTo(CoreLink2.Url);
        }
        /// <summary>
        /// Compares 2 instances of CoreLink.
        /// </summary>
        public static int CompareByItemCount(CoreLink CoreLink1, CoreLink CoreLink2)
        {
            return CoreLink1.ItemCount.CompareTo(CoreLink2.ItemCount);
        }
        /// <summary>
        /// Compares 2 instances of CoreLink.
        /// </summary>
        public static int CompareByCreatedUtc(CoreLink CoreLink1, CoreLink CoreLink2)
        {
            return CoreLink1.CreatedUtc.CompareTo(CoreLink2.CreatedUtc);
        }
        /// <summary>
        /// Compares 2 instances of CoreLink.
        /// </summary>
        public static int CompareByModifiedUtc(CoreLink CoreLink1, CoreLink CoreLink2)
        {
            return CoreLink1.ModifiedUtc.CompareTo(CoreLink2.ModifiedUtc);
        }
        /// <summary>
        /// Compares 2 instances of CoreLink.
        /// </summary>
        public static int CompareByPriority(CoreLink CoreLink1, CoreLink CoreLink2)
        {
            return CoreLink1.Priority.CompareTo(CoreLink2.Priority);
        }

        #endregion


    }

}






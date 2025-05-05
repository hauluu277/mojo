
// Author:					HAULD
// Created:					2015-10-2
// Last Modified:			2015-10-2
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

namespace mojoportalf.Business
{

    public class TagArticle
    {

        #region Constructors

        public TagArticle()
        { }


        public TagArticle(
            int itemID)
        {
            GetLookup(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int moduleID = -1;
        private int siteID = -1;
        private string name = string.Empty;
        private string description = string.Empty;
        private string interpretation = string.Empty;
        private bool censorship = false;
        private bool isPublic = false;
        private string keyword = string.Empty;
        private string alphabet = string.Empty;
        private int userCreate = -1;
        private DateTime dateCreate = DateTime.UtcNow;
        private int userApprove = -1;
        private DateTime dateApprove = DateTime.UtcNow;
        private string itemUrl = string.Empty;
        private int pageID = -1;
        private int totalDeSuat = 0;
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
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string Interpretation
        {
            get { return interpretation; }
            set { interpretation = value; }
        }
        public bool Censorship
        {
            get { return censorship; }
            set { censorship = value; }
        }
        public bool IsPublic
        {
            get { return isPublic; }
            set { isPublic = value; }
        }
        public int UserCreate
        {
            get { return userCreate; }
            set { userCreate = value; }
        }
        public DateTime DateCreate
        {
            get { return dateCreate; }
            set { dateCreate = value; }
        }
        public int UserApprove
        {
            get { return userApprove; }
            set { userApprove = value; }
        }
        public DateTime DateApprove
        {
            get { return dateApprove; }
            set { dateApprove = value; }
        }
        public string ItemUrl
        {
            get { return itemUrl; }
            set { itemUrl = value; }
        }
        public int PageID
        {
            get { return pageID; }
            set { pageID = value; }
        }
        public int TotalDeSuat
        {
            get { return totalDeSuat; }
            set { totalDeSuat = value; }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of Lookup.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetLookup(
            int itemID)
        {
            using (IDataReader reader = DBTagArticle.GetOne(
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
                this.description = reader["Description"].ToString();
                this.interpretation = reader["Interpretation"].ToString();
                this.censorship = Convert.ToBoolean(reader["Censorship"]);
                this.isPublic = Convert.ToBoolean(reader["IsPublic"]);
                this.userCreate = Convert.ToInt32(reader["UserCreate"]);
                this.dateCreate = Convert.ToDateTime(reader["DateCreate"]);
                this.userApprove = Convert.ToInt32(reader["UserApprove"]);
                this.dateApprove = Convert.ToDateTime(reader["DateApprove"]);
                this.itemUrl = reader["ItemUrl"].ToString();
                this.pageID = Convert.ToInt32(reader["PageID"]);

            }

        }

        /// <summary>
        /// Persists a new instance of Lookup. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBTagArticle.Create(
                this.moduleID,
                this.siteID,
                this.name,
                this.description,
                this.interpretation,
                this.censorship,
                this.isPublic,
                this.userCreate,
                this.dateCreate,
                this.userApprove,
                this.dateApprove,
                this.itemUrl,
                this.pageID);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of Lookup. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBTagArticle.Update(
                this.itemID,
                this.moduleID,
                this.siteID,
                this.name,
                this.description,
                this.interpretation,
                this.censorship,
                this.isPublic,
                this.userCreate,
                this.dateCreate,
                this.userApprove,
                this.dateApprove,
                this.itemUrl,
                this.pageID);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of Lookup. Returns true on success.
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

        public static List<TagArticle> GetAllPublic()
        {
            IDataReader reader = DBTagArticle.GetAllPublic();
            return LoadListFromReader(reader);

        }



        #endregion

        #region Static Methods

        /// <summary>
        /// Deletes an instance of Lookup. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBTagArticle.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of Lookup. 
        /// </summary>
        public static int GetCount(int siteId,
            int pageNumber,
            int pageSize,
            bool? isApprove,
            bool? isPublish,
            string keyword,
            string alphabe)
        {
            return DBTagArticle.GetCount(siteId,
            isApprove,
            keyword,
            alphabe);
        }

        private static List<TagArticle> LoadListFromReader(IDataReader reader)
        {
            List<TagArticle> lookupList = new List<TagArticle>();
            try
            {
                while (reader.Read())
                {
                    TagArticle lookup = new TagArticle();
                    lookup.itemID = Convert.ToInt32(reader["ItemID"]);
                    lookup.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    lookup.siteID = Convert.ToInt32(reader["SiteID"]);
                    lookup.name = reader["Name"].ToString();
                    lookup.description = reader["Description"].ToString();
                    lookup.interpretation = reader["Interpretation"].ToString();
                    lookup.censorship = Convert.ToBoolean(reader["Censorship"]);
                    lookup.isPublic = Convert.ToBoolean(reader["IsPublic"]);
                    lookup.userCreate = Convert.ToInt32(reader["UserCreate"]);
                    lookup.dateCreate = Convert.ToDateTime(reader["DateCreate"]);
                    lookup.userApprove = Convert.ToInt32(reader["UserApprove"]);
                    lookup.dateApprove = Convert.ToDateTime(reader["DateApprove"]);
                    lookup.itemUrl = reader["ItemUrl"].ToString();
                    lookup.pageID = Convert.ToInt32(reader["PageID"]);
                    lookupList.Add(lookup);

                }
            }
            finally
            {
                reader.Close();
            }

            return lookupList;

        }
        private static List<TagArticle> LoadListFromReader2(IDataReader reader)
        {
            List<TagArticle> lookupList = new List<TagArticle>();
            try
            {
                while (reader.Read())
                {
                    TagArticle lookup = new TagArticle();
                    lookup.itemID = Convert.ToInt32(reader["ItemID"]);
                    lookup.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    lookup.siteID = Convert.ToInt32(reader["SiteID"]);
                    lookup.name = reader["Name"].ToString();
                    lookup.description = reader["Description"].ToString();
                    lookup.interpretation = reader["Interpretation"].ToString();
                    lookup.censorship = Convert.ToBoolean(reader["Censorship"]);
                    lookup.isPublic = Convert.ToBoolean(reader["IsPublic"]);
                    lookup.userCreate = Convert.ToInt32(reader["UserCreate"]);
                    lookup.dateCreate = Convert.ToDateTime(reader["DateCreate"]);
                    lookup.userApprove = Convert.ToInt32(reader["UserApprove"]);
                    lookup.dateApprove = Convert.ToDateTime(reader["DateApprove"]);
                    lookup.itemUrl = reader["ItemUrl"].ToString();
                    lookup.pageID = Convert.ToInt32(reader["PageID"]);
                    if (reader["TotalDeSuat"] != DBNull.Value)
                    {
                        lookup.totalDeSuat = Convert.ToInt32(reader["TotalDeSuat"].ToString());
                    }
                    lookupList.Add(lookup);

                }
            }
            finally
            {
                reader.Close();
            }

            return lookupList;

        }
        /// <summary>
        /// Gets an IList with all instances of Lookup.
        /// </summary>
        public static List<TagArticle> GetAll()
        {
            IDataReader reader = DBTagArticle.GetAll();
            return LoadListFromReader(reader);

        }

        public static List<TagArticle> GetOne(int itemId)
        {
            IDataReader reader = DBTagArticle.GetOne(itemId);
            return LoadListFromReader(reader);
        }

        /// <summary>
        /// Gets an IList with page of instances of Lookup.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<TagArticle> GetPage(int siteID, int pageNumber, int pageSize, bool? isApprove, string keyword, string alphabet, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBTagArticle.GetPage(siteID, pageNumber, pageSize, isApprove, keyword, alphabet, out totalPages);
            return LoadListFromReader2(reader);
        }

        public static List<TagArticle> GetPageFix(int siteID,string keyword, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBTagArticle.GetPageFix(siteID,keyword, pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }

        public static List<TagArticle> GetMany(string keyword, string alphabet)
        {
            IDataReader reader = DBTagArticle.GetMany(keyword, alphabet);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of Lookup.
        /// </summary>
        public static int CompareByItemID(TagArticle lookup1, TagArticle lookup2)
        {
            return lookup1.ItemID.CompareTo(lookup2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of Lookup.
        /// </summary>
        public static int CompareByModuleID(TagArticle lookup1, TagArticle lookup2)
        {
            return lookup1.ModuleID.CompareTo(lookup2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of Lookup.
        /// </summary>
        public static int CompareBySiteID(TagArticle lookup1, TagArticle lookup2)
        {
            return lookup1.SiteID.CompareTo(lookup2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of Lookup.
        /// </summary>
        public static int CompareByName(TagArticle lookup1, TagArticle lookup2)
        {
            return lookup1.Name.CompareTo(lookup2.Name);
        }
        /// <summary>
        /// Compares 2 instances of Lookup.
        /// </summary>
        public static int CompareByDescription(TagArticle lookup1, TagArticle lookup2)
        {
            return lookup1.Description.CompareTo(lookup2.Description);
        }
        /// <summary>
        /// Compares 2 instances of Lookup.
        /// </summary>
        public static int CompareByInterpretation(TagArticle lookup1, TagArticle lookup2)
        {
            return lookup1.Interpretation.CompareTo(lookup2.Interpretation);
        }
        /// <summary>
        /// Compares 2 instances of Lookup.
        /// </summary>
        public static int CompareByUserCreate(TagArticle lookup1, TagArticle lookup2)
        {
            return lookup1.UserCreate.CompareTo(lookup2.UserCreate);
        }
        /// <summary>
        /// Compares 2 instances of Lookup.
        /// </summary>
        public static int CompareByDateCreate(TagArticle lookup1, TagArticle lookup2)
        {
            return lookup1.DateCreate.CompareTo(lookup2.DateCreate);
        }
        /// <summary>
        /// Compares 2 instances of Lookup.
        /// </summary>
        public static int CompareByUserApprove(TagArticle lookup1, TagArticle lookup2)
        {
            return lookup1.UserApprove.CompareTo(lookup2.UserApprove);
        }
        /// <summary>
        /// Compares 2 instances of Lookup.
        /// </summary>
        public static int CompareByDateApprove(TagArticle lookup1, TagArticle lookup2)
        {
            return lookup1.DateApprove.CompareTo(lookup2.DateApprove);
        }
        /// <summary>
        /// Compares 2 instances of Lookup.
        /// </summary>
        public static int CompareByItemUrl(TagArticle lookup1, TagArticle lookup2)
        {
            return lookup1.ItemUrl.CompareTo(lookup2.ItemUrl);
        }
        /// <summary>
        /// Compares 2 instances of Lookup.
        /// </summary>
        public static int CompareByPageID(TagArticle lookup1, TagArticle lookup2)
        {
            return lookup1.PageID.CompareTo(lookup2.PageID);
        }

        #endregion


    }

}






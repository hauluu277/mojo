

// Author:					Viet Nguyen
// Created:					2015-11-11
// Last Modified:			2015-11-11
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
using DictionaryFeature.Data;

namespace DictionaryFeature.Business
{

    public class DictionaryMean
    {

        #region Constructors

        public DictionaryMean()
        { }


        public DictionaryMean(
            int itemID)
        {
            Getmd_LookupMean(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int moduleID = -1;
        private int siteID = -1;
        private int lookupID = -1;
        private string interpretation = string.Empty;
        private bool censorship = false;
        private bool isPublic = false;
        private int userCreate = -1;
        private DateTime dateCreate = DateTime.UtcNow;
        private int userApprove = -1;
        private DateTime dateApprove = DateTime.UtcNow;

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
        public int LookupID
        {
            get { return lookupID; }
            set { lookupID = value; }
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

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of md_LookupMean.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void Getmd_LookupMean(
            int itemID)
        {
            using (IDataReader reader = DBDictionaryMean.GetOne(
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
                this.lookupID = Convert.ToInt32(reader["LookupID"]);
                this.interpretation = reader["Interpretation"].ToString();
                this.censorship = Convert.ToBoolean(reader["Censorship"]);
                this.isPublic = Convert.ToBoolean(reader["IsPublic"]);
                this.userCreate = Convert.ToInt32(reader["UserCreate"]);
                this.dateCreate = Convert.ToDateTime(reader["DateCreate"]);
                this.userApprove = Convert.ToInt32(reader["UserApprove"]);
                this.dateApprove = Convert.ToDateTime(reader["DateApprove"]);

            }

        }

        /// <summary>
        /// Persists a new instance of md_LookupMean. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBDictionaryMean.Create(
                this.moduleID,
                this.siteID,
                this.lookupID,
                this.interpretation,
                this.censorship,
                this.isPublic,
                this.userCreate,
                this.dateCreate,
                this.userApprove,
                this.dateApprove);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of md_LookupMean. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBDictionaryMean.Update(
                this.itemID,
                this.moduleID,
                this.siteID,
                this.lookupID,
                this.interpretation,
                this.censorship,
                this.isPublic,
                this.userCreate,
                this.dateCreate,
                this.userApprove,
                this.dateApprove);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of md_LookupMean. Returns true on success.
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
        /// Deletes an instance of md_LookupMean. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBDictionaryMean.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of md_LookupMean. 
        /// </summary>
        public static int GetCount(int siteId, int pageNumber, int pageSize, bool? isApprove, bool? isPublish, int lookupId)
        {
            return DBDictionaryMean.GetCount(siteId, pageNumber, pageSize, isApprove, isPublish, lookupId);
        }

        private static List<DictionaryMean> LoadListFromReader(IDataReader reader)
        {
            List<DictionaryMean> md_LookupMeanList = new List<DictionaryMean>();
            try
            {
                while (reader.Read())
                {
                    DictionaryMean md_LookupMean = new DictionaryMean();
                    md_LookupMean.itemID = Convert.ToInt32(reader["ItemID"]);
                    md_LookupMean.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    md_LookupMean.siteID = Convert.ToInt32(reader["SiteID"]);
                    md_LookupMean.lookupID = Convert.ToInt32(reader["LookupID"]);
                    md_LookupMean.interpretation = reader["Interpretation"].ToString();
                    md_LookupMean.censorship = Convert.ToBoolean(reader["Censorship"]);
                    md_LookupMean.isPublic = Convert.ToBoolean(reader["IsPublic"]);
                    md_LookupMean.userCreate = Convert.ToInt32(reader["UserCreate"]);
                    md_LookupMean.dateCreate = Convert.ToDateTime(reader["DateCreate"]);
                    md_LookupMean.userApprove = Convert.ToInt32(reader["UserApprove"]);
                    md_LookupMean.dateApprove = Convert.ToDateTime(reader["DateApprove"]);
                    md_LookupMeanList.Add(md_LookupMean);

                }
            }
            finally
            {
                reader.Close();
            }

            return md_LookupMeanList;

        }

        /// <summary>
        /// Gets an IList with all instances of md_LookupMean.
        /// </summary>
        public static List<DictionaryMean> GetAll(int siteId, int lookupId, bool? isApprove, bool? isPublish)
        {
            IDataReader reader = DBDictionaryMean.GetAll(siteId, isApprove, isPublish, lookupId);
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of md_LookupMean.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<DictionaryMean> GetPage(int siteId, int pageNumber, int pageSize, bool? isApprove, bool? isPublish, int lookupId, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBDictionaryMean.GetPage(siteId, pageNumber, pageSize, isApprove, isPublish, lookupId, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of md_LookupMean.
        /// </summary>
        public static int CompareByItemID(DictionaryMean md_LookupMean1, DictionaryMean md_LookupMean2)
        {
            return md_LookupMean1.ItemID.CompareTo(md_LookupMean2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of md_LookupMean.
        /// </summary>
        public static int CompareByModuleID(DictionaryMean md_LookupMean1, DictionaryMean md_LookupMean2)
        {
            return md_LookupMean1.ModuleID.CompareTo(md_LookupMean2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of md_LookupMean.
        /// </summary>
        public static int CompareBySiteID(DictionaryMean md_LookupMean1, DictionaryMean md_LookupMean2)
        {
            return md_LookupMean1.SiteID.CompareTo(md_LookupMean2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of md_LookupMean.
        /// </summary>
        public static int CompareByLookupID(DictionaryMean md_LookupMean1, DictionaryMean md_LookupMean2)
        {
            return md_LookupMean1.LookupID.CompareTo(md_LookupMean2.LookupID);
        }
        /// <summary>
        /// Compares 2 instances of md_LookupMean.
        /// </summary>
        public static int CompareByInterpretation(DictionaryMean md_LookupMean1, DictionaryMean md_LookupMean2)
        {
            return md_LookupMean1.Interpretation.CompareTo(md_LookupMean2.Interpretation);
        }
        /// <summary>
        /// Compares 2 instances of md_LookupMean.
        /// </summary>
        public static int CompareByUserCreate(DictionaryMean md_LookupMean1, DictionaryMean md_LookupMean2)
        {
            return md_LookupMean1.UserCreate.CompareTo(md_LookupMean2.UserCreate);
        }
        /// <summary>
        /// Compares 2 instances of md_LookupMean.
        /// </summary>
        public static int CompareByDateCreate(DictionaryMean md_LookupMean1, DictionaryMean md_LookupMean2)
        {
            return md_LookupMean1.DateCreate.CompareTo(md_LookupMean2.DateCreate);
        }
        /// <summary>
        /// Compares 2 instances of md_LookupMean.
        /// </summary>
        public static int CompareByUserApprove(DictionaryMean md_LookupMean1, DictionaryMean md_LookupMean2)
        {
            return md_LookupMean1.UserApprove.CompareTo(md_LookupMean2.UserApprove);
        }
        /// <summary>
        /// Compares 2 instances of md_LookupMean.
        /// </summary>
        public static int CompareByDateApprove(DictionaryMean md_LookupMean1, DictionaryMean md_LookupMean2)
        {
            return md_LookupMean1.DateApprove.CompareTo(md_LookupMean2.DateApprove);
        }

        #endregion


    }

}






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

    public class LinhVuc
    {

        #region Constructors

        public LinhVuc()
        { }


        public LinhVuc(
            int itemID)
        {
            Getcore_Field(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int siteID = -1;
        private string name = string.Empty;
        private string nameEN = string.Empty;
        private string description = string.Empty;
        private DateTime createdUtc = DateTime.UtcNow;
        private Guid createdBy = Guid.Empty;

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
        public string Description
        {
            get { return description; }
            set { description = value; }
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

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of core_Field.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void Getcore_Field(
            int itemID)
        {
            using (IDataReader reader = DBLinhVuc.GetOne(
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
                this.description = reader["Description"].ToString();
                this.createdUtc = Convert.ToDateTime(reader["CreatedUtc"]);
                this.createdBy = new Guid(reader["CreatedBy"].ToString());

            }

        }

        /// <summary>
        /// Persists a new instance of core_Field. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBLinhVuc.Create(
                this.siteID,
                this.name,
                this.nameEN,
                this.description,
                this.createdUtc,
                this.createdBy);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of core_Field. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBLinhVuc.Update(
                this.itemID,
                this.siteID,
                this.name,
                this.nameEN,
                this.description,
                this.createdUtc,
                this.createdBy);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of core_Field. Returns true on success.
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
        /// Deletes an instance of core_Field. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBLinhVuc.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of core_Field. 
        /// </summary>
        public static int GetCount(int siteId, string keyword)
        {
            return DBLinhVuc.GetCount(siteId, keyword);
        }

        private static List<LinhVuc> LoadListFromReader(IDataReader reader)
        {
            List<LinhVuc> core_FieldList = new List<LinhVuc>();
            try
            {
                while (reader.Read())
                {
                    LinhVuc core_Field = new LinhVuc();
                    core_Field.itemID = Convert.ToInt32(reader["ItemID"]);
                    core_Field.siteID = Convert.ToInt32(reader["SiteID"]);
                    core_Field.name = reader["Name"].ToString();
                    core_Field.nameEN = reader["NameEN"].ToString();
                    core_Field.description = reader["Description"].ToString();
                    core_Field.createdUtc = Convert.ToDateTime(reader["CreatedUtc"]);
                    core_Field.createdBy = new Guid(reader["CreatedBy"].ToString());
                    core_FieldList.Add(core_Field);

                }
            }
            finally
            {
                reader.Close();
            }

            return core_FieldList;

        }

        /// <summary>
        /// Gets an IList with all instances of core_Field.
        /// </summary>
        public static List<LinhVuc> GetAll(int siteID)
        {
            IDataReader reader = DBLinhVuc.GetAll(siteID);
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of core_Field.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<LinhVuc> GetPage(int siteId, int pageNumber, int pageSize, string keyword, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBLinhVuc.GetPage(siteId, pageNumber, pageSize, keyword, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of core_Field.
        /// </summary>
        public static int CompareByItemID(LinhVuc core_Field1, LinhVuc core_Field2)
        {
            return core_Field1.ItemID.CompareTo(core_Field2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of core_Field.
        /// </summary>
        public static int CompareBySiteID(LinhVuc core_Field1, LinhVuc core_Field2)
        {
            return core_Field1.SiteID.CompareTo(core_Field2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of core_Field.
        /// </summary>
        public static int CompareByName(LinhVuc core_Field1, LinhVuc core_Field2)
        {
            return core_Field1.Name.CompareTo(core_Field2.Name);
        }
        /// <summary>
        /// Compares 2 instances of core_Field.
        /// </summary>
        public static int CompareByDescription(LinhVuc core_Field1, LinhVuc core_Field2)
        {
            return core_Field1.Description.CompareTo(core_Field2.Description);
        }
        /// <summary>
        /// Compares 2 instances of core_Field.
        /// </summary>
        public static int CompareByCreatedUtc(LinhVuc core_Field1, LinhVuc core_Field2)
        {
            return core_Field1.CreatedUtc.CompareTo(core_Field2.CreatedUtc);
        }

        #endregion


    }

}






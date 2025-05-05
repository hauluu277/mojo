
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

    public class CoQuan
    {

        #region Constructors

        public CoQuan()
        { }


        public CoQuan(
            int itemID)
        {
            Getcore_CoQuan(
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
        /// Gets an instance of core_CoQuan.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void Getcore_CoQuan(
            int itemID)
        {
            using (IDataReader reader = DBCoQuan.GetOne(
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
        /// Persists a new instance of core_CoQuan. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBCoQuan.Create(
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
        /// Updates this instance of core_CoQuan. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBCoQuan.Update(
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
        /// Saves this instance of core_CoQuan. Returns true on success.
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
        /// Deletes an instance of core_CoQuan. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBCoQuan.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of core_CoQuan. 
        /// </summary>
        public static int GetCount(int siteId, string keyword)
        {
            return DBLinhVuc.GetCount(siteId, keyword);
        }

        private static List<CoQuan> LoadListFromReader(IDataReader reader)
        {
            List<CoQuan> core_CoQuanList = new List<CoQuan>();
            try
            {
                while (reader.Read())
                {
                    CoQuan core_CoQuan = new CoQuan();
                    core_CoQuan.itemID = Convert.ToInt32(reader["ItemID"]);
                    core_CoQuan.siteID = Convert.ToInt32(reader["SiteID"]);
                    core_CoQuan.name = reader["Name"].ToString();
                    core_CoQuan.nameEN = reader["NameEN"].ToString();
                    core_CoQuan.description = reader["Description"].ToString();
                    core_CoQuan.createdUtc = Convert.ToDateTime(reader["CreatedUtc"]);
                    core_CoQuan.createdBy = new Guid(reader["CreatedBy"].ToString());
                    core_CoQuanList.Add(core_CoQuan);

                }
            }
            finally
            {
                reader.Close();
            }

            return core_CoQuanList;

        }

        /// <summary>
        /// Gets an IList with all instances of core_CoQuan.
        /// </summary>
        public static List<CoQuan> GetAll(int siteID)
        {
            IDataReader reader = DBCoQuan.GetAll(siteID);
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of core_CoQuan.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<CoQuan> GetPage(int siteId, int pageNumber, int pageSize, string keyword, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBCoQuan.GetPage(siteId, pageNumber, pageSize, keyword, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of core_CoQuan.
        /// </summary>
        public static int CompareByItemID(CoQuan core_CoQuan1, CoQuan core_CoQuan2)
        {
            return core_CoQuan1.ItemID.CompareTo(core_CoQuan2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of core_CoQuan.
        /// </summary>
        public static int CompareBySiteID(CoQuan core_CoQuan1, CoQuan core_CoQuan2)
        {
            return core_CoQuan1.SiteID.CompareTo(core_CoQuan2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of core_CoQuan.
        /// </summary>
        public static int CompareByName(CoQuan core_CoQuan1, CoQuan core_CoQuan2)
        {
            return core_CoQuan1.Name.CompareTo(core_CoQuan2.Name);
        }
        /// <summary>
        /// Compares 2 instances of core_CoQuan.
        /// </summary>
        public static int CompareByDescription(CoQuan core_CoQuan1, CoQuan core_CoQuan2)
        {
            return core_CoQuan1.Description.CompareTo(core_CoQuan2.Description);
        }
        /// <summary>
        /// Compares 2 instances of core_CoQuan.
        /// </summary>
        public static int CompareByCreatedUtc(CoQuan core_CoQuan1, CoQuan core_CoQuan2)
        {
            return core_CoQuan1.CreatedUtc.CompareTo(core_CoQuan2.CreatedUtc);
        }

        #endregion


    }

}






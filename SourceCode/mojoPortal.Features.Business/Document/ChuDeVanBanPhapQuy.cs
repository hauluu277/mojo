
// Author:					Trieubv
// Created:					2015-12-30
// Last Modified:			2015-12-30
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

    public class ChuDeVanBanPhapQuy
    {

        #region Constructors

        public ChuDeVanBanPhapQuy()
        { }


        public ChuDeVanBanPhapQuy(
            int itemID)
        {
            GetChuDeVanBanPhapQuy(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int siteID = -1;
        private int pageID = -1;
        private int moduleID = -1;
        private int chuDeID = -1;
        private int vanBanID = -1;

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
        public int PageID
        {
            get { return pageID; }
            set { pageID = value; }
        }
        public int ModuleID
        {
            get { return moduleID; }
            set { moduleID = value; }
        }
        public int ChuDeID
        {
            get { return chuDeID; }
            set { chuDeID = value; }
        }
        public int VanBanID
        {
            get { return vanBanID; }
            set { vanBanID = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of ChuDeVanBanPhapQuy.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetChuDeVanBanPhapQuy(
            int itemID)
        {
            using (IDataReader reader = DBChuDeVanBanPhapQuy.GetOne(
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
                this.pageID = Convert.ToInt32(reader["PageID"]);
                this.moduleID = Convert.ToInt32(reader["ModuleID"]);
                this.chuDeID = Convert.ToInt32(reader["ChuDeID"]);
                this.vanBanID = Convert.ToInt32(reader["VanBanID"]);

            }

        }

        /// <summary>
        /// Persists a new instance of ChuDeVanBanPhapQuy. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBChuDeVanBanPhapQuy.Create(
                this.siteID,
                this.pageID,
                this.moduleID,
                this.chuDeID,
                this.vanBanID);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of ChuDeVanBanPhapQuy. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBChuDeVanBanPhapQuy.Update(
                this.itemID,
                this.siteID,
                this.pageID,
                this.moduleID,
                this.chuDeID,
                this.vanBanID);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of ChuDeVanBanPhapQuy. Returns true on success.
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
        /// Deletes an instance of ChuDeVanBanPhapQuy. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBChuDeVanBanPhapQuy.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of ChuDeVanBanPhapQuy. 
        /// </summary>
        public static int GetCount()
        {
            return DBChuDeVanBanPhapQuy.GetCount();
        }

        private static List<ChuDeVanBanPhapQuy> LoadListFromReader(IDataReader reader)
        {
            List<ChuDeVanBanPhapQuy> chuDeVanBanPhapQuyList = new List<ChuDeVanBanPhapQuy>();
            try
            {
                while (reader.Read())
                {
                    ChuDeVanBanPhapQuy chuDeVanBanPhapQuy = new ChuDeVanBanPhapQuy();
                    chuDeVanBanPhapQuy.itemID = Convert.ToInt32(reader["ItemID"]);
                    chuDeVanBanPhapQuy.siteID = Convert.ToInt32(reader["SiteID"]);
                    chuDeVanBanPhapQuy.pageID = Convert.ToInt32(reader["PageID"]);
                    chuDeVanBanPhapQuy.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    chuDeVanBanPhapQuy.chuDeID = Convert.ToInt32(reader["ChuDeID"]);
                    chuDeVanBanPhapQuy.vanBanID = Convert.ToInt32(reader["VanBanID"]);
                    chuDeVanBanPhapQuyList.Add(chuDeVanBanPhapQuy);

                }
            }
            finally
            {
                reader.Close();
            }

            return chuDeVanBanPhapQuyList;

        }

        /// <summary>
        /// Gets an IList with all instances of ChuDeVanBanPhapQuy.
        /// </summary>
        public static List<ChuDeVanBanPhapQuy> GetAll()
        {
            IDataReader reader = DBChuDeVanBanPhapQuy.GetAll();
            return LoadListFromReader(reader);

        }
        public static List<ChuDeVanBanPhapQuy> GetAllByDocId(int docId)
        {
            IDataReader reader = DBChuDeVanBanPhapQuy.GetAllByDocId(docId);
            return LoadListFromReader(reader);

        }
        /// <summary>
        /// Gets an IList with page of instances of ChuDeVanBanPhapQuy.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<ChuDeVanBanPhapQuy> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBChuDeVanBanPhapQuy.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of ChuDeVanBanPhapQuy.
        /// </summary>
        public static int CompareByItemID(ChuDeVanBanPhapQuy chuDeVanBanPhapQuy1, ChuDeVanBanPhapQuy chuDeVanBanPhapQuy2)
        {
            return chuDeVanBanPhapQuy1.ItemID.CompareTo(chuDeVanBanPhapQuy2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of ChuDeVanBanPhapQuy.
        /// </summary>
        public static int CompareBySiteID(ChuDeVanBanPhapQuy chuDeVanBanPhapQuy1, ChuDeVanBanPhapQuy chuDeVanBanPhapQuy2)
        {
            return chuDeVanBanPhapQuy1.SiteID.CompareTo(chuDeVanBanPhapQuy2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of ChuDeVanBanPhapQuy.
        /// </summary>
        public static int CompareByPageID(ChuDeVanBanPhapQuy chuDeVanBanPhapQuy1, ChuDeVanBanPhapQuy chuDeVanBanPhapQuy2)
        {
            return chuDeVanBanPhapQuy1.PageID.CompareTo(chuDeVanBanPhapQuy2.PageID);
        }
        /// <summary>
        /// Compares 2 instances of ChuDeVanBanPhapQuy.
        /// </summary>
        public static int CompareByModuleID(ChuDeVanBanPhapQuy chuDeVanBanPhapQuy1, ChuDeVanBanPhapQuy chuDeVanBanPhapQuy2)
        {
            return chuDeVanBanPhapQuy1.ModuleID.CompareTo(chuDeVanBanPhapQuy2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of ChuDeVanBanPhapQuy.
        /// </summary>
        public static int CompareByChuDeID(ChuDeVanBanPhapQuy chuDeVanBanPhapQuy1, ChuDeVanBanPhapQuy chuDeVanBanPhapQuy2)
        {
            return chuDeVanBanPhapQuy1.ChuDeID.CompareTo(chuDeVanBanPhapQuy2.ChuDeID);
        }
        /// <summary>
        /// Compares 2 instances of ChuDeVanBanPhapQuy.
        /// </summary>
        public static int CompareByVanBanID(ChuDeVanBanPhapQuy chuDeVanBanPhapQuy1, ChuDeVanBanPhapQuy chuDeVanBanPhapQuy2)
        {
            return chuDeVanBanPhapQuy1.VanBanID.CompareTo(chuDeVanBanPhapQuy2.VanBanID);
        }

        #endregion


    }

}






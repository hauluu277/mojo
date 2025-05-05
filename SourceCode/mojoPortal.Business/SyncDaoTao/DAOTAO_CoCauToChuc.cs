
// Author:					Mr Hậu
// Created:					2020-12-28
// Last Modified:			2020-12-28
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

    public class DAOTAO_CoCauToChuc
    {

        #region Constructors

        public DAOTAO_CoCauToChuc()
        { }


        public DAOTAO_CoCauToChuc(
            long itemID)
        {
            GetDAOTAO_CoCauToChuc(
                itemID);
        }

        #endregion

        #region Private Properties

        private long itemID;
        private string dAOTAO_COCAUTOCHUC_ID = string.Empty;
        private string dAOTAO_COCAUTOCHUC_TEN = string.Empty;
        private string dAOTAO_COCAUTOCHUC_MA = string.Empty;

        #endregion

        #region Public Properties

        public long ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public string DAOTAO_COCAUTOCHUC_ID
        {
            get { return dAOTAO_COCAUTOCHUC_ID; }
            set { dAOTAO_COCAUTOCHUC_ID = value; }
        }
        public string DAOTAO_COCAUTOCHUC_TEN
        {
            get { return dAOTAO_COCAUTOCHUC_TEN; }
            set { dAOTAO_COCAUTOCHUC_TEN = value; }
        }
        public string DAOTAO_COCAUTOCHUC_MA
        {
            get { return dAOTAO_COCAUTOCHUC_MA; }
            set { dAOTAO_COCAUTOCHUC_MA = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of DAOTAO_CoCauToChuc.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetDAOTAO_CoCauToChuc(
            long itemID)
        {
            using (IDataReader reader = DBDAOTAO_CoCauToChuc.GetOne(
                itemID))
            {
                PopulateFromReader(reader);
            }

        }


        private void PopulateFromReader(IDataReader reader)
        {
            if (reader.Read())
            {
                this.itemID = Convert.ToInt64(reader["ItemID"]);
                this.dAOTAO_COCAUTOCHUC_ID = reader["DAOTAO_COCAUTOCHUC_ID"].ToString();
                this.dAOTAO_COCAUTOCHUC_TEN = reader["DAOTAO_COCAUTOCHUC_TEN"].ToString();
                this.dAOTAO_COCAUTOCHUC_MA = reader["DAOTAO_COCAUTOCHUC_MA"].ToString();

            }

        }

        /// <summary>
        /// Persists a new instance of DAOTAO_CoCauToChuc. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBDAOTAO_CoCauToChuc.Create(
                this.dAOTAO_COCAUTOCHUC_ID,
                this.dAOTAO_COCAUTOCHUC_TEN,
                this.dAOTAO_COCAUTOCHUC_MA);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of DAOTAO_CoCauToChuc. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBDAOTAO_CoCauToChuc.Update(
                this.itemID,
                this.dAOTAO_COCAUTOCHUC_ID,
                this.dAOTAO_COCAUTOCHUC_TEN,
                this.dAOTAO_COCAUTOCHUC_MA);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of DAOTAO_CoCauToChuc. Returns true on success.
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
        /// Deletes an instance of DAOTAO_CoCauToChuc. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            long itemID)
        {
            return DBDAOTAO_CoCauToChuc.Delete(
                itemID);
        }

        public static void DeleteAll()
        {
            var listAll = GetAll();
            foreach (var item in listAll)
            {
                Delete(item.itemID);
            }
        }


        /// <summary>
        /// Gets a count of DAOTAO_CoCauToChuc. 
        /// </summary>
        public static int GetCount()
        {
            return DBDAOTAO_CoCauToChuc.GetCount();
        }

        private static List<DAOTAO_CoCauToChuc> LoadListFromReader(IDataReader reader)
        {
            List<DAOTAO_CoCauToChuc> dAOTAO_CoCauToChucList = new List<DAOTAO_CoCauToChuc>();
            try
            {
                while (reader.Read())
                {
                    DAOTAO_CoCauToChuc dAOTAO_CoCauToChuc = new DAOTAO_CoCauToChuc();
                    dAOTAO_CoCauToChuc.itemID = Convert.ToInt64(reader["ItemID"]);
                    dAOTAO_CoCauToChuc.dAOTAO_COCAUTOCHUC_ID = reader["DAOTAO_COCAUTOCHUC_ID"].ToString();
                    dAOTAO_CoCauToChuc.dAOTAO_COCAUTOCHUC_TEN = reader["DAOTAO_COCAUTOCHUC_TEN"].ToString();
                    dAOTAO_CoCauToChuc.dAOTAO_COCAUTOCHUC_MA = reader["DAOTAO_COCAUTOCHUC_MA"].ToString();
                    dAOTAO_CoCauToChucList.Add(dAOTAO_CoCauToChuc);

                }
            }
            finally
            {
                reader.Close();
            }

            return dAOTAO_CoCauToChucList;

        }

        /// <summary>
        /// Gets an IList with all instances of DAOTAO_CoCauToChuc.
        /// </summary>
        public static List<DAOTAO_CoCauToChuc> GetAll()
        {
            IDataReader reader = DBDAOTAO_CoCauToChuc.GetAll();
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of DAOTAO_CoCauToChuc.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<DAOTAO_CoCauToChuc> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBDAOTAO_CoCauToChuc.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of DAOTAO_CoCauToChuc.
        /// </summary>
        public static int CompareByDAOTAO_COCAUTOCHUC_ID(DAOTAO_CoCauToChuc dAOTAO_CoCauToChuc1, DAOTAO_CoCauToChuc dAOTAO_CoCauToChuc2)
        {
            return dAOTAO_CoCauToChuc1.DAOTAO_COCAUTOCHUC_ID.CompareTo(dAOTAO_CoCauToChuc2.DAOTAO_COCAUTOCHUC_ID);
        }
        /// <summary>
        /// Compares 2 instances of DAOTAO_CoCauToChuc.
        /// </summary>
        public static int CompareByDAOTAO_COCAUTOCHUC_TEN(DAOTAO_CoCauToChuc dAOTAO_CoCauToChuc1, DAOTAO_CoCauToChuc dAOTAO_CoCauToChuc2)
        {
            return dAOTAO_CoCauToChuc1.DAOTAO_COCAUTOCHUC_TEN.CompareTo(dAOTAO_CoCauToChuc2.DAOTAO_COCAUTOCHUC_TEN);
        }
        /// <summary>
        /// Compares 2 instances of DAOTAO_CoCauToChuc.
        /// </summary>
        public static int CompareByDAOTAO_COCAUTOCHUC_MA(DAOTAO_CoCauToChuc dAOTAO_CoCauToChuc1, DAOTAO_CoCauToChuc dAOTAO_CoCauToChuc2)
        {
            return dAOTAO_CoCauToChuc1.DAOTAO_COCAUTOCHUC_MA.CompareTo(dAOTAO_CoCauToChuc2.DAOTAO_COCAUTOCHUC_MA);
        }

        #endregion


    }

}






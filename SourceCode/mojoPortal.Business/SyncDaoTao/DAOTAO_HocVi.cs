
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

    public class DAOTAO_HocVi
    {

        #region Constructors

        public DAOTAO_HocVi()
        { }


        public DAOTAO_HocVi(
            long itemID)
        {
            GetDAOTAO_HocVi(
                itemID);
        }

        #endregion

        #region Private Properties

        private long itemID;
        private string hOCVI_ID = string.Empty;
        private string hOCVI_TEN = string.Empty;
        private string hOCVI_MA = string.Empty;

        #endregion

        #region Public Properties

        public long ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public string HOCVI_ID
        {
            get { return hOCVI_ID; }
            set { hOCVI_ID = value; }
        }
        public string HOCVI_TEN
        {
            get { return hOCVI_TEN; }
            set { hOCVI_TEN = value; }
        }
        public string HOCVI_MA
        {
            get { return hOCVI_MA; }
            set { hOCVI_MA = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of DAOTAO_HocVi.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetDAOTAO_HocVi(
            long itemID)
        {
            using (IDataReader reader = DBDAOTAO_HocVi.GetOne(
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
                this.hOCVI_ID = reader["HOCVI_ID"].ToString();
                this.hOCVI_TEN = reader["HOCVI_TEN"].ToString();
                this.hOCVI_MA = reader["HOCVI_MA"].ToString();

            }

        }

        /// <summary>
        /// Persists a new instance of DAOTAO_HocVi. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBDAOTAO_HocVi.Create(
                this.hOCVI_ID,
                this.hOCVI_TEN,
                this.hOCVI_MA);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of DAOTAO_HocVi. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBDAOTAO_HocVi.Update(
                this.itemID,
                this.hOCVI_ID,
                this.hOCVI_TEN,
                this.hOCVI_MA);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of DAOTAO_HocVi. Returns true on success.
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
        /// Deletes an instance of DAOTAO_HocVi. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            long itemID)
        {
            return DBDAOTAO_HocVi.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of DAOTAO_HocVi. 
        /// </summary>
        public static int GetCount()
        {
            return DBDAOTAO_HocVi.GetCount();
        }

        private static List<DAOTAO_HocVi> LoadListFromReader(IDataReader reader)
        {
            List<DAOTAO_HocVi> dAOTAO_HocViList = new List<DAOTAO_HocVi>();
            try
            {
                while (reader.Read())
                {
                    DAOTAO_HocVi dAOTAO_HocVi = new DAOTAO_HocVi();
                    dAOTAO_HocVi.itemID = Convert.ToInt64(reader["ItemID"]);
                    dAOTAO_HocVi.hOCVI_ID = reader["HOCVI_ID"].ToString();
                    dAOTAO_HocVi.hOCVI_TEN = reader["HOCVI_TEN"].ToString();
                    dAOTAO_HocVi.hOCVI_MA = reader["HOCVI_MA"].ToString();
                    dAOTAO_HocViList.Add(dAOTAO_HocVi);

                }
            }
            finally
            {
                reader.Close();
            }

            return dAOTAO_HocViList;

        }

        /// <summary>
        /// Gets an IList with all instances of DAOTAO_HocVi.
        /// </summary>
        public static List<DAOTAO_HocVi> GetAll()
        {
            IDataReader reader = DBDAOTAO_HocVi.GetAll();
            return LoadListFromReader(reader);

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
        /// Gets an IList with page of instances of DAOTAO_HocVi.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<DAOTAO_HocVi> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBDAOTAO_HocVi.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of DAOTAO_HocVi.
        /// </summary>
        public static int CompareByHOCVI_ID(DAOTAO_HocVi dAOTAO_HocVi1, DAOTAO_HocVi dAOTAO_HocVi2)
        {
            return dAOTAO_HocVi1.HOCVI_ID.CompareTo(dAOTAO_HocVi2.HOCVI_ID);
        }
        /// <summary>
        /// Compares 2 instances of DAOTAO_HocVi.
        /// </summary>
        public static int CompareByHOCVI_TEN(DAOTAO_HocVi dAOTAO_HocVi1, DAOTAO_HocVi dAOTAO_HocVi2)
        {
            return dAOTAO_HocVi1.HOCVI_TEN.CompareTo(dAOTAO_HocVi2.HOCVI_TEN);
        }
        /// <summary>
        /// Compares 2 instances of DAOTAO_HocVi.
        /// </summary>
        public static int CompareByHOCVI_MA(DAOTAO_HocVi dAOTAO_HocVi1, DAOTAO_HocVi dAOTAO_HocVi2)
        {
            return dAOTAO_HocVi1.HOCVI_MA.CompareTo(dAOTAO_HocVi2.HOCVI_MA);
        }

        #endregion


    }

}






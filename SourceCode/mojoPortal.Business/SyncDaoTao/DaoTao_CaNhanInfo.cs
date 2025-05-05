
// Author:					HauLd
// Created:					2020-12-19
// Last Modified:			2020-12-19
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

    public class DAOTAO_CaNhanInfo
    {

        #region Constructors

        public DAOTAO_CaNhanInfo()
        { }


        public DAOTAO_CaNhanInfo(
            int itemID)
        {
            GetDAOTAO_CaNhanInfo(
                itemID);
        }
        public DAOTAO_CaNhanInfo(string idNhanSu)
        {
            GetDAOTAO_CaNhanInfo(idNhanSu);
        }

        #endregion

        #region Private Properties

        private long itemID = -1;
        private string nhanSuID = string.Empty;
        private string emailSort = string.Empty;
        private string maCaNhan = string.Empty;
        private DateTime createdDate = DateTime.UtcNow;
        private DateTime updatedDate = DateTime.UtcNow;
        private string emailFull = string.Empty;
        private string phone = string.Empty;

        #endregion

        #region Public Properties
        public string EmailFull
        {
            get { return emailFull; }
            set { emailFull = value; }
        }
        public string Phone
        {
            set { phone = value; }
            get { return phone; }
        }
        public long ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public string NhanSuID
        {
            get { return nhanSuID; }
            set { nhanSuID = value; }
        }
        public string EmailSort
        {
            get { return emailSort; }
            set { emailSort = value; }
        }
        public string MaCaNhan
        {
            get { return maCaNhan; }
            set { maCaNhan = value; }
        }
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }
        public DateTime UpdatedDate
        {
            get { return updatedDate; }
            set { updatedDate = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of DAOTAO_CaNhanInfo.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetDAOTAO_CaNhanInfo(
            int itemID)
        {
            using (IDataReader reader = DBDAOTAO_CaNhanInfo.GetOne(
                itemID))
            {
                PopulateFromReader(reader);
            }

        }
        private void GetDAOTAO_CaNhanInfo( string idNhanSu)
        {
            using (IDataReader reader = DBDAOTAO_CaNhanInfo.GetByItem(idNhanSu))
            {
                PopulateFromReader(reader);
            }

        }

        private void PopulateFromReader(IDataReader reader)
        {
            if (reader.Read())
            {
                this.itemID = Convert.ToInt32(reader["ItemID"]);
                this.nhanSuID = reader["NhanSuID"].ToString();
                this.emailSort = reader["EmailSort"].ToString();
                this.emailFull = reader["EmailFull"].ToString();
                this.phone = reader["Phone"].ToString();
                this.maCaNhan = reader["MaCaNhan"].ToString();
                this.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                this.updatedDate = Convert.ToDateTime(reader["UpdatedDate"]);

            }

        }

        /// <summary>
        /// Persists a new instance of DAOTAO_CaNhanInfo. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBDAOTAO_CaNhanInfo.Create(
                this.nhanSuID,
                this.emailSort,
                this.maCaNhan,
                this.createdDate,
                this.updatedDate,
                this.emailFull,
                this.phone);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of DAOTAO_CaNhanInfo. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBDAOTAO_CaNhanInfo.Update(
                this.itemID,
                this.nhanSuID,
                this.emailSort,
                this.maCaNhan,
                this.createdDate,
                this.updatedDate,
                this.emailFull,
                this.phone);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of DAOTAO_CaNhanInfo. Returns true on success.
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
        /// Deletes an instance of DAOTAO_CaNhanInfo. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            long itemID)
        {
            return DBDAOTAO_CaNhanInfo.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of DAOTAO_CaNhanInfo. 
        /// </summary>
        public static int GetCount()
        {
            return DBDAOTAO_CaNhanInfo.GetCount();
        }

        private static List<DAOTAO_CaNhanInfo> LoadListFromReader(IDataReader reader)
        {
            List<DAOTAO_CaNhanInfo> dAOTAO_CaNhanInfoList = new List<DAOTAO_CaNhanInfo>();
            try
            {
                while (reader.Read())
                {
                    DAOTAO_CaNhanInfo dAOTAO_CaNhanInfo = new DAOTAO_CaNhanInfo();
                    dAOTAO_CaNhanInfo.itemID = Convert.ToInt32(reader["ItemID"]);
                    dAOTAO_CaNhanInfo.nhanSuID = reader["NhanSuID"].ToString();
                    dAOTAO_CaNhanInfo.emailSort = reader["EmailSort"].ToString();
                    dAOTAO_CaNhanInfo.maCaNhan = reader["MaCaNhan"].ToString();
                    dAOTAO_CaNhanInfo.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    dAOTAO_CaNhanInfo.updatedDate = Convert.ToDateTime(reader["UpdatedDate"]);
                    dAOTAO_CaNhanInfo.emailFull = reader["EmailFull"].ToString();
                    dAOTAO_CaNhanInfo.phone = reader["Phone"].ToString();
                    dAOTAO_CaNhanInfoList.Add(dAOTAO_CaNhanInfo);

                }
            }
            finally
            {
                reader.Close();
            }

            return dAOTAO_CaNhanInfoList;

        }

        /// <summary>
        /// Gets an IList with all instances of DAOTAO_CaNhanInfo.
        /// </summary>
        public static List<DAOTAO_CaNhanInfo> GetAll()
        {
            IDataReader reader = DBDAOTAO_CaNhanInfo.GetAll();
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of DAOTAO_CaNhanInfo.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<DAOTAO_CaNhanInfo> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBDAOTAO_CaNhanInfo.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of DAOTAO_CaNhanInfo.
        /// </summary>
        public static int CompareByItemID(DAOTAO_CaNhanInfo dAOTAO_CaNhanInfo1, DAOTAO_CaNhanInfo dAOTAO_CaNhanInfo2)
        {
            return dAOTAO_CaNhanInfo1.ItemID.CompareTo(dAOTAO_CaNhanInfo2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of DAOTAO_CaNhanInfo.
        /// </summary>
     
        /// <summary>
        /// Compares 2 instances of DAOTAO_CaNhanInfo.
        /// </summary>
        public static int CompareByMaCaNhan(DAOTAO_CaNhanInfo dAOTAO_CaNhanInfo1, DAOTAO_CaNhanInfo dAOTAO_CaNhanInfo2)
        {
            return dAOTAO_CaNhanInfo1.MaCaNhan.CompareTo(dAOTAO_CaNhanInfo2.MaCaNhan);
        }
        /// <summary>
        /// Compares 2 instances of DAOTAO_CaNhanInfo.
        /// </summary>
        public static int CompareByCreatedDate(DAOTAO_CaNhanInfo dAOTAO_CaNhanInfo1, DAOTAO_CaNhanInfo dAOTAO_CaNhanInfo2)
        {
            return dAOTAO_CaNhanInfo1.CreatedDate.CompareTo(dAOTAO_CaNhanInfo2.CreatedDate);
        }
        /// <summary>
        /// Compares 2 instances of DAOTAO_CaNhanInfo.
        /// </summary>
        public static int CompareByUpdatedDate(DAOTAO_CaNhanInfo dAOTAO_CaNhanInfo1, DAOTAO_CaNhanInfo dAOTAO_CaNhanInfo2)
        {
            return dAOTAO_CaNhanInfo1.UpdatedDate.CompareTo(dAOTAO_CaNhanInfo2.UpdatedDate);
        }

        #endregion


    }

}






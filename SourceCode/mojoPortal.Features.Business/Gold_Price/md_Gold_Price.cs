
// Author:					Joe Audette
// Created:					2025-5-15
// Last Modified:			2025-5-15
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
using mojoPortal.Features.Data.Gold_Price;

namespace mojoPortal.Business
{

    public class md_Gold_Price
    {

        #region Constructors

        public md_Gold_Price()
        { }


        public md_Gold_Price(
            int itemID)
        {
            Getmd_Gold_Price(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private string tenLoaiVang = string.Empty;
        private double giaMuaHomNay;
        private double giaBanHomNay;
        private double giaMuaHomTruoc;
        private double giaBanHomTruoc;
        private string nganHang = string.Empty;
        private double thang1;
        private double thang3;
        private double thang6;
        private double thang9;
        private double thang12;
        private DateTime createdDate = DateTime.UtcNow;
        private DateTime editedDate = DateTime.UtcNow;
        private int createdBy = -1;
        private int editedBy = -1;

        #endregion

        #region Public Properties

        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public string TenLoaiVang
        {
            get { return tenLoaiVang; }
            set { tenLoaiVang = value; }
        }
        public double GiaMuaHomNay
        {
            get { return giaMuaHomNay; }
            set { giaMuaHomNay = value; }
        }
        public double GiaBanHomNay
        {
            get { return giaBanHomNay; }
            set { giaBanHomNay = value; }
        }
        public double GiaMuaHomTruoc
        {
            get { return giaMuaHomTruoc; }
            set { giaMuaHomTruoc = value; }
        }
        public double GiaBanHomTruoc
        {
            get { return giaBanHomTruoc; }
            set { giaBanHomTruoc = value; }
        }
        public string NganHang
        {
            get { return nganHang; }
            set { nganHang = value; }
        }
        public double Thang1
        {
            get { return thang1; }
            set { thang1 = value; }
        }
        public double Thang3
        {
            get { return thang3; }
            set { thang3 = value; }
        }
        public double Thang6
        {
            get { return thang6; }
            set { thang6 = value; }
        }
        public double Thang9
        {
            get { return thang9; }
            set { thang9 = value; }
        }
        public double Thang12
        {
            get { return thang12; }
            set { thang12 = value; }
        }
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }
        public DateTime EditedDate
        {
            get { return editedDate; }
            set { editedDate = value; }
        }
        public int CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }
        public int EditedBy
        {
            get { return editedBy; }
            set { editedBy = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of md_Gold_Price.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void Getmd_Gold_Price(
            int itemID)
        {
            using (IDataReader reader = DBmd_Gold_Price.GetOne(
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
                this.tenLoaiVang = reader["TenLoaiVang"].ToString();
                this.giaMuaHomNay = Convert.ToDouble(reader["GiaMuaHomNay"]);
                this.giaBanHomNay = Convert.ToDouble(reader["GiaBanHomNay"]);
                this.giaMuaHomTruoc = Convert.ToDouble(reader["GiaMuaHomTruoc"]);
                this.giaBanHomTruoc = Convert.ToDouble(reader["GiaBanHomTruoc"]);
                this.nganHang = reader["NganHang"].ToString();
                this.thang1 = Convert.ToDouble(reader["Thang1"]);
                this.thang3 = Convert.ToDouble(reader["Thang3"]);
                this.thang6 = Convert.ToDouble(reader["Thang6"]);
                this.thang9 = Convert.ToDouble(reader["Thang9"]);
                this.thang12 = Convert.ToDouble(reader["Thang12"]);
                this.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                this.editedDate = Convert.ToDateTime(reader["EditedDate"]);
                this.createdBy = Convert.ToInt32(reader["CreatedBy"]);
                this.editedBy = Convert.ToInt32(reader["EditedBy"]);

            }

        }

        /// <summary>
        /// Persists a new instance of md_Gold_Price. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBmd_Gold_Price.Create(
                this.tenLoaiVang,
                this.giaMuaHomNay,
                this.giaBanHomNay,
                this.giaMuaHomTruoc,
                this.giaBanHomTruoc,
                this.nganHang,
                this.thang1,
                this.thang3,
                this.thang6,
                this.thang9,
                this.thang12,
                this.createdDate,
                this.editedDate,
                this.createdBy,
                this.editedBy);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of md_Gold_Price. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBmd_Gold_Price.Update(
                this.itemID,
                this.tenLoaiVang,
                this.giaMuaHomNay,
                this.giaBanHomNay,
                this.giaMuaHomTruoc,
                this.giaBanHomTruoc,
                this.nganHang,
                this.thang1,
                this.thang3,
                this.thang6,
                this.thang9,
                this.thang12,
                this.createdDate,
                this.editedDate,
                this.createdBy,
                this.editedBy);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of md_Gold_Price. Returns true on success.
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
        /// Deletes an instance of md_Gold_Price. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBmd_Gold_Price.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of md_Gold_Price. 
        /// </summary>
        public static int GetCount()
        {
            return DBmd_Gold_Price.GetCount();
        }

        private static List<md_Gold_Price> LoadListFromReader(IDataReader reader)
        {
            List<md_Gold_Price> md_Gold_PriceList = new List<md_Gold_Price>();
            try
            {
                while (reader.Read())
                {
                    md_Gold_Price md_Gold_Price = new md_Gold_Price();
                    md_Gold_Price.itemID = Convert.ToInt32(reader["ItemID"]);
                    md_Gold_Price.tenLoaiVang = reader["TenLoaiVang"].ToString();
                    md_Gold_Price.giaMuaHomNay = Convert.ToDouble(reader["GiaMuaHomNay"]);
                    md_Gold_Price.giaBanHomNay = Convert.ToDouble(reader["GiaBanHomNay"]);
                    md_Gold_Price.giaMuaHomTruoc = Convert.ToDouble(reader["GiaMuaHomTruoc"]);
                    md_Gold_Price.giaBanHomTruoc = Convert.ToDouble(reader["GiaBanHomTruoc"]);
                    md_Gold_Price.nganHang = reader["NganHang"].ToString();
                    md_Gold_Price.thang1 = Convert.ToDouble(reader["Thang1"]);
                    md_Gold_Price.thang3 = Convert.ToDouble(reader["Thang3"]);
                    md_Gold_Price.thang6 = Convert.ToDouble(reader["Thang6"]);
                    md_Gold_Price.thang9 = Convert.ToDouble(reader["Thang9"]);
                    md_Gold_Price.thang12 = Convert.ToDouble(reader["Thang12"]);
                    md_Gold_Price.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    md_Gold_Price.editedDate = Convert.ToDateTime(reader["EditedDate"]);
                    md_Gold_Price.createdBy = Convert.ToInt32(reader["CreatedBy"]);
                    md_Gold_Price.editedBy = Convert.ToInt32(reader["EditedBy"]);
                    md_Gold_PriceList.Add(md_Gold_Price);

                }
            }
            finally
            {
                reader.Close();
            }

            return md_Gold_PriceList;

        }

        /// <summary>
        /// Gets an IList with all instances of md_Gold_Price.
        /// </summary>
        public static List<md_Gold_Price> GetAll()
        {
            IDataReader reader = DBmd_Gold_Price.GetAll();
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of md_Gold_Price.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<md_Gold_Price> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBmd_Gold_Price.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of md_Gold_Price.
        /// </summary>
        public static int CompareByItemID(md_Gold_Price md_Gold_Price1, md_Gold_Price md_Gold_Price2)
        {
            return md_Gold_Price1.ItemID.CompareTo(md_Gold_Price2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of md_Gold_Price.
        /// </summary>
        public static int CompareByTenLoaiVang(md_Gold_Price md_Gold_Price1, md_Gold_Price md_Gold_Price2)
        {
            return md_Gold_Price1.TenLoaiVang.CompareTo(md_Gold_Price2.TenLoaiVang);
        }
        /// <summary>
        /// Compares 2 instances of md_Gold_Price.
        /// </summary>
        public static int CompareByNganHang(md_Gold_Price md_Gold_Price1, md_Gold_Price md_Gold_Price2)
        {
            return md_Gold_Price1.NganHang.CompareTo(md_Gold_Price2.NganHang);
        }
        /// <summary>
        /// Compares 2 instances of md_Gold_Price.
        /// </summary>
        public static int CompareByCreatedDate(md_Gold_Price md_Gold_Price1, md_Gold_Price md_Gold_Price2)
        {
            return md_Gold_Price1.CreatedDate.CompareTo(md_Gold_Price2.CreatedDate);
        }
        /// <summary>
        /// Compares 2 instances of md_Gold_Price.
        /// </summary>
        public static int CompareByEditedDate(md_Gold_Price md_Gold_Price1, md_Gold_Price md_Gold_Price2)
        {
            return md_Gold_Price1.EditedDate.CompareTo(md_Gold_Price2.EditedDate);
        }
        /// <summary>
        /// Compares 2 instances of md_Gold_Price.
        /// </summary>
        public static int CompareByCreatedBy(md_Gold_Price md_Gold_Price1, md_Gold_Price md_Gold_Price2)
        {
            return md_Gold_Price1.CreatedBy.CompareTo(md_Gold_Price2.CreatedBy);
        }
        /// <summary>
        /// Compares 2 instances of md_Gold_Price.
        /// </summary>
        public static int CompareByEditedBy(md_Gold_Price md_Gold_Price1, md_Gold_Price md_Gold_Price2)
        {
            return md_Gold_Price1.EditedBy.CompareTo(md_Gold_Price2.EditedBy);
        }

        #endregion


    }

}






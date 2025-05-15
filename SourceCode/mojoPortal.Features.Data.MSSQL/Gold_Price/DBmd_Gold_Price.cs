 
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
using System.IO;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using mojoPortal.Data;
	
namespace mojoPortal.Features.Data.Gold_Price
{

    public static class DBmd_Gold_Price
    {


        /// <summary>
        /// Inserts a row in the md_Gold_Price table. Returns new integer id.
        /// </summary>
        /// <param name="tenLoaiVang"> tenLoaiVang </param>
        /// <param name="giaMuaHomNay"> giaMuaHomNay </param>
        /// <param name="giaBanHomNay"> giaBanHomNay </param>
        /// <param name="giaMuaHomTruoc"> giaMuaHomTruoc </param>
        /// <param name="giaBanHomTruoc"> giaBanHomTruoc </param>
        /// <param name="nganHang"> nganHang </param>
        /// <param name="thang1"> thang1 </param>
        /// <param name="thang3"> thang3 </param>
        /// <param name="thang6"> thang6 </param>
        /// <param name="thang9"> thang9 </param>
        /// <param name="thang12"> thang12 </param>
        /// <param name="createdDate"> createdDate </param>
        /// <param name="editedDate"> editedDate </param>
        /// <param name="createdBy"> createdBy </param>
        /// <param name="editedBy"> editedBy </param>
        /// <returns>int</returns>
        public static int Create(
            string tenLoaiVang,
            double giaMuaHomNay,
            double giaBanHomNay,
            double giaMuaHomTruoc,
            double giaBanHomTruoc,
            string nganHang,
            double thang1,
            double thang3,
            double thang6,
            double thang9,
            double thang12,
            DateTime createdDate,
            DateTime editedDate,
            int createdBy,
            int editedBy)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Gold_Price_Insert", 15);
            sph.DefineSqlParameter("@TenLoaiVang", SqlDbType.NVarChar, 550, ParameterDirection.Input, tenLoaiVang);
            sph.DefineSqlParameter("@GiaMuaHomNay", SqlDbType.Float, ParameterDirection.Input, giaMuaHomNay);
            sph.DefineSqlParameter("@GiaBanHomNay", SqlDbType.Float, ParameterDirection.Input, giaBanHomNay);
            sph.DefineSqlParameter("@GiaMuaHomTruoc", SqlDbType.Float, ParameterDirection.Input, giaMuaHomTruoc);
            sph.DefineSqlParameter("@GiaBanHomTruoc", SqlDbType.Float, ParameterDirection.Input, giaBanHomTruoc);
            sph.DefineSqlParameter("@NganHang", SqlDbType.NVarChar, 550, ParameterDirection.Input, nganHang);
            sph.DefineSqlParameter("@Thang1", SqlDbType.Float, ParameterDirection.Input, thang1);
            sph.DefineSqlParameter("@Thang3", SqlDbType.Float, ParameterDirection.Input, thang3);
            sph.DefineSqlParameter("@Thang6", SqlDbType.Float, ParameterDirection.Input, thang6);
            sph.DefineSqlParameter("@Thang9", SqlDbType.Float, ParameterDirection.Input, thang9);
            sph.DefineSqlParameter("@Thang12", SqlDbType.Float, ParameterDirection.Input, thang12);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@EditedDate", SqlDbType.DateTime, ParameterDirection.Input, editedDate);
            sph.DefineSqlParameter("@CreatedBy", SqlDbType.Int, ParameterDirection.Input, createdBy);
            sph.DefineSqlParameter("@EditedBy", SqlDbType.Int, ParameterDirection.Input, editedBy);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_Gold_Price table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="tenLoaiVang"> tenLoaiVang </param>
        /// <param name="giaMuaHomNay"> giaMuaHomNay </param>
        /// <param name="giaBanHomNay"> giaBanHomNay </param>
        /// <param name="giaMuaHomTruoc"> giaMuaHomTruoc </param>
        /// <param name="giaBanHomTruoc"> giaBanHomTruoc </param>
        /// <param name="nganHang"> nganHang </param>
        /// <param name="thang1"> thang1 </param>
        /// <param name="thang3"> thang3 </param>
        /// <param name="thang6"> thang6 </param>
        /// <param name="thang9"> thang9 </param>
        /// <param name="thang12"> thang12 </param>
        /// <param name="createdDate"> createdDate </param>
        /// <param name="editedDate"> editedDate </param>
        /// <param name="createdBy"> createdBy </param>
        /// <param name="editedBy"> editedBy </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            string tenLoaiVang,
            double giaMuaHomNay,
            double giaBanHomNay,
            double giaMuaHomTruoc,
            double giaBanHomTruoc,
            string nganHang,
            double thang1,
            double thang3,
            double thang6,
            double thang9,
            double thang12,
            DateTime createdDate,
            DateTime editedDate,
            int createdBy,
            int editedBy)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Gold_Price_Update", 16);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@TenLoaiVang", SqlDbType.NVarChar, 550, ParameterDirection.Input, tenLoaiVang);
            sph.DefineSqlParameter("@GiaMuaHomNay", SqlDbType.Float, ParameterDirection.Input, giaMuaHomNay);
            sph.DefineSqlParameter("@GiaBanHomNay", SqlDbType.Float, ParameterDirection.Input, giaBanHomNay);
            sph.DefineSqlParameter("@GiaMuaHomTruoc", SqlDbType.Float, ParameterDirection.Input, giaMuaHomTruoc);
            sph.DefineSqlParameter("@GiaBanHomTruoc", SqlDbType.Float, ParameterDirection.Input, giaBanHomTruoc);
            sph.DefineSqlParameter("@NganHang", SqlDbType.NVarChar, 550, ParameterDirection.Input, nganHang);
            sph.DefineSqlParameter("@Thang1", SqlDbType.Float, ParameterDirection.Input, thang1);
            sph.DefineSqlParameter("@Thang3", SqlDbType.Float, ParameterDirection.Input, thang3);
            sph.DefineSqlParameter("@Thang6", SqlDbType.Float, ParameterDirection.Input, thang6);
            sph.DefineSqlParameter("@Thang9", SqlDbType.Float, ParameterDirection.Input, thang9);
            sph.DefineSqlParameter("@Thang12", SqlDbType.Float, ParameterDirection.Input, thang12);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@EditedDate", SqlDbType.DateTime, ParameterDirection.Input, editedDate);
            sph.DefineSqlParameter("@CreatedBy", SqlDbType.Int, ParameterDirection.Input, createdBy);
            sph.DefineSqlParameter("@EditedBy", SqlDbType.Int, ParameterDirection.Input, editedBy);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_Gold_Price table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Gold_Price_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_Gold_Price table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Gold_Price_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_Gold_Price table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_Gold_Price_GetCount",
                null));

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_Gold_Price table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_Gold_Price_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the md_Gold_Price table.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static IDataReader GetPage(
            int pageNumber,
            int pageSize,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCount();

            if (pageSize > 0) totalPages = totalRows / pageSize;

            if (totalRows <= pageSize)
            {
                totalPages = 1;
            }
            else
            {
                int remainder;
                Math.DivRem(totalRows, pageSize, out remainder);
                if (remainder > 0)
                {
                    totalPages += 1;
                }
            }

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Gold_Price_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



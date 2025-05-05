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
using System.IO;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using mojoPortal.Data;

namespace mojoPortal.Data
{

    public static class DBDAOTAO_CaNhanInfo
    {


        /// <summary>
        /// Inserts a row in the DAOTAO_CaNhanInfo table. Returns new integer id.
        /// </summary>
        /// <param name="nhanSuID"> nhanSuID </param>
        /// <param name="email"> email </param>
        /// <param name="maCaNhan"> maCaNhan </param>
        /// <param name="createdDate"> createdDate </param>
        /// <param name="updatedDate"> updatedDate </param>
        /// <returns>int</returns>
        public static int Create(
            string nhanSuID,
            string emailSort,
            string maCaNhan,
            DateTime createdDate,
            DateTime updatedDate,
            string emailFull,
            string phone)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "DAOTAO_CaNhanInfo_Insert", 7);
            sph.DefineSqlParameter("@NhanSuID", SqlDbType.NVarChar, 250, ParameterDirection.Input, nhanSuID);
            sph.DefineSqlParameter("@EmailSort", SqlDbType.NVarChar, 350, ParameterDirection.Input, emailSort);
            sph.DefineSqlParameter("@MaCaNhan", SqlDbType.NVarChar, 350, ParameterDirection.Input, maCaNhan);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@UpdatedDate", SqlDbType.DateTime, ParameterDirection.Input, updatedDate);
            sph.DefineSqlParameter("@EmailFull", SqlDbType.NVarChar, 550, ParameterDirection.Input, emailFull);
            sph.DefineSqlParameter("@Phone", SqlDbType.NVarChar, 150, ParameterDirection.Input, phone);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the DAOTAO_CaNhanInfo table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="nhanSuID"> nhanSuID </param>
        /// <param name="email"> email </param>
        /// <param name="maCaNhan"> maCaNhan </param>
        /// <param name="createdDate"> createdDate </param>
        /// <param name="updatedDate"> updatedDate </param>
        /// <returns>bool</returns>
        public static bool Update(
            long itemID,
            string nhanSuID,
            string emailSort,
            string maCaNhan,
            DateTime createdDate,
            DateTime updatedDate,
            string emailFull,
            string phone)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "DAOTAO_CaNhanInfo_Update", 8);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@NhanSuID", SqlDbType.NVarChar, 250, ParameterDirection.Input, nhanSuID);
            sph.DefineSqlParameter("@EmailSort", SqlDbType.NVarChar, 350, ParameterDirection.Input, emailSort);
            sph.DefineSqlParameter("@MaCaNhan", SqlDbType.NVarChar, 350, ParameterDirection.Input, maCaNhan);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@UpdatedDate", SqlDbType.DateTime, ParameterDirection.Input, updatedDate);
            sph.DefineSqlParameter("@EmailFull", SqlDbType.NVarChar, 550, ParameterDirection.Input, emailFull);
            sph.DefineSqlParameter("@Phone", SqlDbType.NVarChar, 150, ParameterDirection.Input, phone);

            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the DAOTAO_CaNhanInfo table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            long itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "DAOTAO_CaNhanInfo_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the DAOTAO_CaNhanInfo table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "DAOTAO_CaNhanInfo_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();
        }

        public static IDataReader GetByItem(string idNhanSu)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "DAOTAO_CaNhanInfo_SelectByItem", 1);
            sph.DefineSqlParameter("@IdNhanSu", SqlDbType.NVarChar, 250, ParameterDirection.Input, idNhanSu);
            return sph.ExecuteReader();
        }

        /// <summary>
        /// Gets a count of rows in the DAOTAO_CaNhanInfo table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "DAOTAO_CaNhanInfo_GetCount",
                null));

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the DAOTAO_CaNhanInfo table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "DAOTAO_CaNhanInfo_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the DAOTAO_CaNhanInfo table.
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "DAOTAO_CaNhanInfo_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



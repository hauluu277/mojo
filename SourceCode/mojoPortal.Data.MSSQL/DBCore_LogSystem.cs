// Author:					Mr Hậu
// Created:					2021-1-20
// Last Modified:			2021-1-20
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

    public static class DBcore_LogSystem
    {


        /// <summary>
        /// Inserts a row in the core_LogSystem table. Returns new integer id.
        /// </summary>
        /// <param name="userID"> userID </param>
        /// <param name="startLogin"> startLogin </param>
        /// <param name="endLogin"> endLogin </param>
        /// <param name="countLogin"> countLogin </param>
        /// <param name="createdDate"> createdDate </param>
        /// <returns>int</returns>
        public static int Create(
            int userID,
            DateTime startLogin,
            DateTime endLogin,
            int countLogin,
            DateTime createdDate)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_LogSystem_Insert", 5);
            sph.DefineSqlParameter("@UserID", SqlDbType.Int, ParameterDirection.Input, userID);
            sph.DefineSqlParameter("@StartLogin", SqlDbType.DateTime, ParameterDirection.Input, startLogin);
            sph.DefineSqlParameter("@EndLogin", SqlDbType.DateTime, ParameterDirection.Input, endLogin);
            sph.DefineSqlParameter("@CountLogin", SqlDbType.Int, ParameterDirection.Input, countLogin);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the core_LogSystem table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="userID"> userID </param>
        /// <param name="startLogin"> startLogin </param>
        /// <param name="endLogin"> endLogin </param>
        /// <param name="countLogin"> countLogin </param>
        /// <param name="createdDate"> createdDate </param>
        /// <returns>bool</returns>
        public static bool Update(
            long itemID,
            int userID,
            DateTime startLogin,
            DateTime endLogin,
            int countLogin,
            DateTime createdDate)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_LogSystem_Update", 6);
            sph.DefineSqlParameter("@ItemID", SqlDbType.BigInt, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@UserID", SqlDbType.Int, ParameterDirection.Input, userID);
            sph.DefineSqlParameter("@StartLogin", SqlDbType.DateTime, ParameterDirection.Input, startLogin);
            sph.DefineSqlParameter("@EndLogin", SqlDbType.DateTime, ParameterDirection.Input, endLogin);
            sph.DefineSqlParameter("@CountLogin", SqlDbType.Int, ParameterDirection.Input, countLogin);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the core_LogSystem table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            long itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_LogSystem_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.BigInt, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the core_LogSystem table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            long itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_LogSystem_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.BigInt, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }
        public static IDataReader GetByUser(int userId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_LogSystem_GetByUser", 1);
            sph.DefineSqlParameter("@UserID", SqlDbType.Int, ParameterDirection.Input, userId);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the core_LogSystem table.
        /// </summary>
        public static int GetCount(string account,
            string fullName,
            DateTime? dateFrom,
            DateTime? dateTo,
            DateTime? startLoginFrom,
            DateTime? startLoginTo,
            DateTime? endLoginFrom,
            DateTime? endLoginTo)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_LogSystem_GetCount", 8);
            sph.DefineSqlParameter("@LoginName", SqlDbType.NVarChar, 255, ParameterDirection.Input, account);
            sph.DefineSqlParameter("@FullName", SqlDbType.NVarChar, 255, ParameterDirection.Input, fullName);
            sph.DefineSqlParameter("@DateFrom", SqlDbType.DateTime, ParameterDirection.Input, dateFrom);
            sph.DefineSqlParameter("@DateTo", SqlDbType.DateTime, ParameterDirection.Input, dateTo);
            sph.DefineSqlParameter("@StartLoginFrom", SqlDbType.DateTime, ParameterDirection.Input, startLoginFrom);
            sph.DefineSqlParameter("@StartLoginTo", SqlDbType.DateTime, ParameterDirection.Input, startLoginFrom);
            sph.DefineSqlParameter("@EndLoginForm", SqlDbType.DateTime, ParameterDirection.Input, endLoginFrom);
            sph.DefineSqlParameter("@EndLoginTo", SqlDbType.DateTime, ParameterDirection.Input, endLoginTo);
            return Convert.ToInt32(sph.ExecuteScalar());

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the core_LogSystem table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "core_LogSystem_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the core_LogSystem table.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static IDataReader GetPage(
            string account,
            string fullName,
            DateTime? dateFrom,
            DateTime? dateTo,
            DateTime? startLoginFrom,
            DateTime? startLoginTo,
            DateTime? endLoginFrom,
            DateTime? endLoginTo,
            int pageNumber,
            int pageSize,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCount(account, fullName, dateFrom, dateTo, startLoginFrom, startLoginTo, endLoginFrom, endLoginTo);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_LogSystem_SelectPage", 10);
            sph.DefineSqlParameter("@LoginName", SqlDbType.NVarChar, 255, ParameterDirection.Input, account);
            sph.DefineSqlParameter("@FullName", SqlDbType.NVarChar, 255, ParameterDirection.Input, fullName);
            sph.DefineSqlParameter("@DateFrom", SqlDbType.DateTime, ParameterDirection.Input, dateFrom);
            sph.DefineSqlParameter("@DateTo", SqlDbType.DateTime, ParameterDirection.Input, dateTo);
            sph.DefineSqlParameter("@StartLoginFrom", SqlDbType.DateTime, ParameterDirection.Input, startLoginFrom);
            sph.DefineSqlParameter("@StartLoginTo", SqlDbType.DateTime, ParameterDirection.Input, startLoginFrom);
            sph.DefineSqlParameter("@EndLoginForm", SqlDbType.DateTime, ParameterDirection.Input, endLoginFrom);
            sph.DefineSqlParameter("@EndLoginTo", SqlDbType.DateTime, ParameterDirection.Input, endLoginTo);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



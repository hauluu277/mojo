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
using System.IO;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using mojoPortal.Data;

namespace mojoPortal.Data
{

    public static class DBChuDeVanBanPhapQuy
    {


        /// <summary>
        /// Inserts a row in the md_ChuDeVanBanPhapQuy table. Returns new integer id.
        /// </summary>
        /// <param name="siteID"> siteID </param>
        /// <param name="pageID"> pageID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="chuDeID"> chuDeID </param>
        /// <param name="vanBanID"> vanBanID </param>
        /// <returns>int</returns>
        public static int Create(
            int siteID,
            int pageID,
            int moduleID,
            int chuDeID,
            int vanBanID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ChuDeVanBanPhapQuy_Insert", 5);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@PageID", SqlDbType.Int, ParameterDirection.Input, pageID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@ChuDeID", SqlDbType.Int, ParameterDirection.Input, chuDeID);
            sph.DefineSqlParameter("@VanBanID", SqlDbType.Int, ParameterDirection.Input, vanBanID);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_ChuDeVanBanPhapQuy table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="pageID"> pageID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="chuDeID"> chuDeID </param>
        /// <param name="vanBanID"> vanBanID </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int siteID,
            int pageID,
            int moduleID,
            int chuDeID,
            int vanBanID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ChuDeVanBanPhapQuy_Update", 6);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@PageID", SqlDbType.Int, ParameterDirection.Input, pageID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@ChuDeID", SqlDbType.Int, ParameterDirection.Input, chuDeID);
            sph.DefineSqlParameter("@VanBanID", SqlDbType.Int, ParameterDirection.Input, vanBanID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_ChuDeVanBanPhapQuy table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ChuDeVanBanPhapQuy_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_ChuDeVanBanPhapQuy table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ChuDeVanBanPhapQuy_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_ChuDeVanBanPhapQuy table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_ChuDeVanBanPhapQuy_GetCount",
                null));

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_ChuDeVanBanPhapQuy table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_ChuDeVanBanPhapQuy_SelectAll",
                null);

        }

        public static IDataReader GetAllByDocId(int docId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ChuDeVanBanPhapQuy_SelectAllByDocId", 1);
            sph.DefineSqlParameter("@DocID", SqlDbType.Int, ParameterDirection.Input, docId);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a page of data from the md_ChuDeVanBanPhapQuy table.
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ChuDeVanBanPhapQuy_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



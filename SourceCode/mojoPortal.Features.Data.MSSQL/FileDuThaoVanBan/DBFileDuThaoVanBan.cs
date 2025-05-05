// Author:					Trieubv
// Created:					2015-10-27
// Last Modified:			2015-10-27
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

    public static class DBFileDuThao
    {


        /// <summary>
        /// Inserts a row in the md_FileDuThao table. Returns new integer id.
        /// </summary>
        /// <param name="siteID"> siteID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="pageID"> pageID </param>
        /// <param name="duThaoID"> duThaoID </param>
        /// <param name="name"> name </param>
        /// <param name="createdByUserID"> createdByUserID </param>
        /// <param name="createdDate"> createdDate </param>
        /// <param name="lastModUserID"> lastModUserID </param>
        /// <param name="lastModUtc"> lastModUtc </param>
        /// <returns>int</returns>
        public static int Create(
            int siteID,
            int moduleID,
            int pageID,
            int duThaoID,
            string name,
            string filePath,
            int createdByUserID,
            DateTime createdDate,
            string lastModUserID,
            DateTime lastModUtc)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_FileDuThao_Insert", 10);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@PageID", SqlDbType.Int, ParameterDirection.Input, pageID);
            sph.DefineSqlParameter("@DuThaoID", SqlDbType.Int, ParameterDirection.Input, duThaoID);
            sph.DefineSqlParameter("@Name", SqlDbType.NVarChar, 255, ParameterDirection.Input, name);
            sph.DefineSqlParameter("@FilePath", SqlDbType.NVarChar, 255, ParameterDirection.Input, filePath);
            sph.DefineSqlParameter("@CreatedByUserID", SqlDbType.Int, ParameterDirection.Input, createdByUserID);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@LastModUserID", SqlDbType.NChar, 10, ParameterDirection.Input, lastModUserID);
            sph.DefineSqlParameter("@LastModUtc", SqlDbType.DateTime, ParameterDirection.Input, lastModUtc);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_FileDuThao table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="pageID"> pageID </param>
        /// <param name="duThaoID"> duThaoID </param>
        /// <param name="name"> name </param>
        /// <param name="createdByUserID"> createdByUserID </param>
        /// <param name="createdDate"> createdDate </param>
        /// <param name="lastModUserID"> lastModUserID </param>
        /// <param name="lastModUtc"> lastModUtc </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int siteID,
            int moduleID,
            int pageID,
            int duThaoID,
            string name,
            string filePath,
            int createdByUserID,
            DateTime createdDate,
            string lastModUserID,
            DateTime lastModUtc)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_FileDuThao_Update", 11);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@PageID", SqlDbType.Int, ParameterDirection.Input, pageID);
            sph.DefineSqlParameter("@DuThaoID", SqlDbType.Int, ParameterDirection.Input, duThaoID);
            sph.DefineSqlParameter("@Name", SqlDbType.NVarChar, 255, ParameterDirection.Input, name);
            sph.DefineSqlParameter("@FilePath", SqlDbType.NVarChar, 255, ParameterDirection.Input, filePath);
            sph.DefineSqlParameter("@CreatedByUserID", SqlDbType.Int, ParameterDirection.Input, createdByUserID);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@LastModUserID", SqlDbType.NChar, 10, ParameterDirection.Input, lastModUserID);
            sph.DefineSqlParameter("@LastModUtc", SqlDbType.DateTime, ParameterDirection.Input, lastModUtc);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_FileDuThao table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_FileDuThao_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_FileDuThao table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_FileDuThao_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_FileDuThao table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_FileDuThao_GetCount",
                null));

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_FileDuThao table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_FileDuThao_SelectAll",
                null);

        }
        public static IDataReader GetAllByDuThaoId(int duThaoID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_FileDuThao_SelectByDuThaoId", 1);
            sph.DefineSqlParameter("@DuThaoID", SqlDbType.Int, ParameterDirection.Input, duThaoID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a page of data from the md_FileDuThao table.
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_FileDuThao_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



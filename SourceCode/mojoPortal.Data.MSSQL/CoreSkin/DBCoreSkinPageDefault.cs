// Author:					Joe Audette
// Created:					2017-12-17
// Last Modified:			2017-12-17
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

    public static class DBCoreSkinPageDefault
    {


        /// <summary>
        /// Inserts a row in the core_SkinPageDefault table. Returns new integer id.
        /// </summary>
        /// <param name="siteID"> siteID </param>
        /// <param name="skinID"> skinID </param>
        /// <param name="skinPageID"> skinPageID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="moduleTitle"> moduleTitle </param>
        /// <param name="paneName"> paneName </param>
        /// <param name="moduleOrder"> moduleOrder </param>
        /// <returns>int</returns>
        public static int Create(
            int siteID,
            int skinID,
            int skinPageID,
            int moduleID,
            string moduleTitle,
            string paneName,
            int moduleOrder)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_SkinPageDefault_Insert", 7);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@SkinID", SqlDbType.Int, ParameterDirection.Input, skinID);
            sph.DefineSqlParameter("@SkinPageID", SqlDbType.Int, ParameterDirection.Input, skinPageID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@ModuleTitle", SqlDbType.NVarChar, 255, ParameterDirection.Input, moduleTitle);
            sph.DefineSqlParameter("@PaneName", SqlDbType.NVarChar, 50, ParameterDirection.Input, paneName);
            sph.DefineSqlParameter("@ModuleOrder", SqlDbType.Int, ParameterDirection.Input, moduleOrder);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the core_SkinPageDefault table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="skinID"> skinID </param>
        /// <param name="skinPageID"> skinPageID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="moduleTitle"> moduleTitle </param>
        /// <param name="paneName"> paneName </param>
        /// <param name="moduleOrder"> moduleOrder </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int siteID,
            int skinID,
            int skinPageID,
            int moduleID,
            string moduleTitle,
            string paneName,
            int moduleOrder)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_SkinPageDefault_Update", 8);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@SkinID", SqlDbType.Int, ParameterDirection.Input, skinID);
            sph.DefineSqlParameter("@SkinPageID", SqlDbType.Int, ParameterDirection.Input, skinPageID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@ModuleTitle", SqlDbType.NVarChar, 255, ParameterDirection.Input, moduleTitle);
            sph.DefineSqlParameter("@PaneName", SqlDbType.NVarChar, 50, ParameterDirection.Input, paneName);
            sph.DefineSqlParameter("@ModuleOrder", SqlDbType.Int, ParameterDirection.Input, moduleOrder);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the core_SkinPageDefault table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_SkinPageDefault_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the core_SkinPageDefault table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_SkinPageDefault_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }
        public static IDataReader GetSkinPageDefaultForPage(int skinPageID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_SkinPageDefault_ForPage", 1);
            sph.DefineSqlParameter("@SkinPageID", SqlDbType.Int, ParameterDirection.Input, skinPageID);
            return sph.ExecuteReader();
        }

        /// <summary>
        /// Gets a count of rows in the core_SkinPageDefault table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "core_SkinPageDefault_GetCount",
                null));

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the core_SkinPageDefault table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "core_SkinPageDefault_SelectAll",
                null);

        }

        public static bool DeleteDefaultModule(int itemId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_SkinPageDefault_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemId);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > -1);
        }

        public static bool UpdateDefaultModuleOrder(int itemId, int moduleOrder, string paneName)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_SkinPageDefault_UpdateModuleOrder", 3);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemId);
            sph.DefineSqlParameter("@ModuleOrder", SqlDbType.Int, ParameterDirection.Input, moduleOrder);
            sph.DefineSqlParameter("@PaneName", SqlDbType.NVarChar, 50, ParameterDirection.Input, paneName);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > -1);
        }

        /// <summary>
        /// Gets a page of data from the core_SkinPageDefault table.
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_SkinPageDefault_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



// Author:					Hậu sky
// Created:					2018-1-10
// Last Modified:			2018-1-10
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

    public static class DBHomeCategory
    {


        /// <summary>
        /// Inserts a row in the md_HomeCategory table. Returns new integer id.
        /// </summary>
        /// <param name="siteID"> siteID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="title"> title </param>
        /// <param name="itemUrl"> itemUrl </param>
        /// <param name="description"> description </param>
        /// <param name="isPublish"> isPublish </param>
        /// <param name="dateCreate"> dateCreate </param>
        /// <param name="orderBy"> orderBy </param>
        /// <param name="itemIcon"> itemIcon </param>
        /// <returns>int</returns>
        public static int Create(
            int siteID,
            int moduleID,
            string title,
            string itemUrl,
            string description,
            bool isPublish,
            DateTime dateCreate,
            int orderBy,
            string itemIcon)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_HomeCategory_Insert", 9);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 350, ParameterDirection.Input, title);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NVarChar, 250, ParameterDirection.Input, itemUrl);
            sph.DefineSqlParameter("@Description", SqlDbType.NText, ParameterDirection.Input, description);
            sph.DefineSqlParameter("@IsPublish", SqlDbType.Bit, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@DateCreate", SqlDbType.DateTime, ParameterDirection.Input, dateCreate);
            sph.DefineSqlParameter("@OrderBy", SqlDbType.Int, ParameterDirection.Input, orderBy);
            sph.DefineSqlParameter("@ItemIcon", SqlDbType.NVarChar, 250, ParameterDirection.Input, itemIcon);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_HomeCategory table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="title"> title </param>
        /// <param name="itemUrl"> itemUrl </param>
        /// <param name="description"> description </param>
        /// <param name="isPublish"> isPublish </param>
        /// <param name="dateCreate"> dateCreate </param>
        /// <param name="orderBy"> orderBy </param>
        /// <param name="itemIcon"> itemIcon </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int siteID,
            int moduleID,
            string title,
            string itemUrl,
            string description,
            bool isPublish,
            DateTime dateCreate,
            int orderBy,
            string itemIcon)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_HomeCategory_Update", 10);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 350, ParameterDirection.Input, title);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NVarChar, 250, ParameterDirection.Input, itemUrl);
            sph.DefineSqlParameter("@Description", SqlDbType.NText, ParameterDirection.Input, description);
            sph.DefineSqlParameter("@IsPublish", SqlDbType.Bit, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@DateCreate", SqlDbType.DateTime, ParameterDirection.Input, dateCreate);
            sph.DefineSqlParameter("@OrderBy", SqlDbType.Int, ParameterDirection.Input, orderBy);
            sph.DefineSqlParameter("@ItemIcon", SqlDbType.NVarChar, 250, ParameterDirection.Input, itemIcon);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_HomeCategory table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_HomeCategory_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_HomeCategory table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_HomeCategory_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_HomeCategory table.
        /// </summary>
        public static int GetCount(int siteID, int moduleID)
        {

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_HomeCategory_GetCount", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            return Convert.ToInt32(sph.ExecuteScalar());
        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_HomeCategory table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_HomeCategory_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the md_HomeCategory table.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static IDataReader GetPage(
            int siteID,
            int moduleID,
            int pageNumber,
            int pageSize,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCount(siteID,moduleID);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_HomeCategory_SelectPage", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



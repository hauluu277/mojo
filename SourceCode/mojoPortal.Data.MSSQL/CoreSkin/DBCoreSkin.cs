// Author:					Joe Audette
// Created:					2017-12-21
// Last Modified:			2017-12-21
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

    public static class DBCoreSkin
    {


        /// <summary>
        /// Inserts a row in the core_Skins table. Returns new integer id.
        /// </summary>
        /// <param name="skin"> skin </param>
        /// <param name="title"> title </param>
        /// <param name="orderBy"> orderBy </param>
        /// <param name="createDate"> createDate </param>
        /// <param name="userCreate"> userCreate </param>
        /// <returns>int</returns>
        public static int Create(
            int skinType,
            string title,
            int orderBy,
            DateTime createDate,
            int userCreate,
            int categoryArticle)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_Skins_Insert", 6);
            sph.DefineSqlParameter("@SkinType", SqlDbType.Int, ParameterDirection.Input, skinType);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 255, ParameterDirection.Input, title);
            sph.DefineSqlParameter("@OrderBy", SqlDbType.Int, ParameterDirection.Input, orderBy);
            sph.DefineSqlParameter("@CreateDate", SqlDbType.DateTime, ParameterDirection.Input, createDate);
            sph.DefineSqlParameter("@UserCreate", SqlDbType.Int, ParameterDirection.Input, userCreate);
            sph.DefineSqlParameter("@CategoryArticle", SqlDbType.Int, ParameterDirection.Input, categoryArticle);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the core_Skins table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="skin"> skin </param>
        /// <param name="title"> title </param>
        /// <param name="orderBy"> orderBy </param>
        /// <param name="createDate"> createDate </param>
        /// <param name="userCreate"> userCreate </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int skinType,
            string title,
            int orderBy,
            DateTime createDate,
            int userCreate,
            int categoryArticle)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_Skins_Update", 7);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@SkinType", SqlDbType.Int, ParameterDirection.Input, skinType);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 255, ParameterDirection.Input, title);
            sph.DefineSqlParameter("@OrderBy", SqlDbType.Int, ParameterDirection.Input, orderBy);
            sph.DefineSqlParameter("@CreateDate", SqlDbType.DateTime, ParameterDirection.Input, createDate);
            sph.DefineSqlParameter("@UserCreate", SqlDbType.Int, ParameterDirection.Input, userCreate);
            sph.DefineSqlParameter("@CategoryArticle", SqlDbType.Int, ParameterDirection.Input, categoryArticle);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the core_Skins table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_Skins_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the core_Skins table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Skins_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }


        /// <summary>
        /// Gets a count of rows in the core_Skins table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "core_Skins_GetCount",
                null));

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the core_Skins table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "core_Skins_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the core_Skins table.
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Skins_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



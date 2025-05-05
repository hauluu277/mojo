// Author:					Mr Hau
// Created:					2018-6-26
// Last Modified:			2018-6-26
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

    public static class DBCategoryUserArticle
    {


        /// <summary>
        /// Inserts a row in the core_CategoryUserArticle table. Returns new integer id.
        /// </summary>
        /// <param name="userID"> userID </param>
        /// <param name="categoryID"> categoryID </param>
        /// <param name="typeRole"> typeRole </param>
        /// <returns>int</returns>
        public static int Create(
            int userID,
            int categoryID,
            int typeRole)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_CategoryUserArticle_Insert", 3);
            sph.DefineSqlParameter("@UserID", SqlDbType.Int, ParameterDirection.Input, userID);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@TypeRole", SqlDbType.Int, ParameterDirection.Input, typeRole);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the core_CategoryUserArticle table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="userID"> userID </param>
        /// <param name="categoryID"> categoryID </param>
        /// <param name="typeRole"> typeRole </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int userID,
            int categoryID,
            int typeRole)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_CategoryUserArticle_Update", 4);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@UserID", SqlDbType.Int, ParameterDirection.Input, userID);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@TypeRole", SqlDbType.Int, ParameterDirection.Input, typeRole);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the core_CategoryUserArticle table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_CategoryUserArticle_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the core_CategoryUserArticle table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_CategoryUserArticle_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }

        public static IDataReader GetSelected(int categoryId, int typeRole)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_CategoryUserArticle_Selected", 2);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryId);
            sph.DefineSqlParameter("@TypeRole", SqlDbType.Int, ParameterDirection.Input, typeRole);
            return sph.ExecuteReader();
        }

        public static IDataReader GetCategoryByUser(int userID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_CategoryUserArticle_SelectCategory", 1);
            sph.DefineSqlParameter("@UserID", SqlDbType.Int, ParameterDirection.Input, userID);
            return sph.ExecuteReader();
        }

        /// <summary>
        /// Gets a count of rows in the core_CategoryUserArticle table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "core_CategoryUserArticle_GetCount",
                null));

        }
        public static IDataReader GetListByUser(int userId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_SelectItemByUser", 1);
            sph.DefineSqlParameter("@UserID", SqlDbType.Int, ParameterDirection.Input, userId);
            return sph.ExecuteReader();
        }
        /// <summary>
        /// Gets an IDataReader with all rows in the core_CategoryUserArticle table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "core_CategoryUserArticle_SelectAll",
                null);

        }


        /// <summary>
        /// Gets a page of data from the core_CategoryUserArticle table.
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_CategoryUserArticle_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



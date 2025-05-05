// Author:					Mr Hậu
// Created:					2020-10-4
// Last Modified:			2020-10-4
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

    public static class DBArticleCategory
    {


        /// <summary>
        /// Inserts a row in the md_ArticleCategory table. Returns new integer id.
        /// </summary>
        /// <param name="articleID"> articleID </param>
        /// <param name="categoryParentID"> categoryParentID </param>
        /// <param name="categoryChildID"> categoryChildID </param>
        /// <param name="isHotCatParent"> isHotCatParent </param>
        /// <param name="isHotCatChild"> isHotCatChild </param>
        /// <param name="isNotDisplayParent"> isNotDisplayParent </param>
        /// <returns>int</returns>
        public static int Create(
            long articleID,
            int siteID,
            int categoryID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleCategory_Insert", 3);
            sph.DefineSqlParameter("@ArticleID", SqlDbType.BigInt, ParameterDirection.Input, articleID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_ArticleCategory table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="articleID"> articleID </param>
        /// <param name="categoryParentID"> categoryParentID </param>
        /// <param name="categoryChildID"> categoryChildID </param>
        /// <param name="isHotCatParent"> isHotCatParent </param>
        /// <param name="isHotCatChild"> isHotCatChild </param>
        /// <param name="isNotDisplayParent"> isNotDisplayParent </param>
        /// <returns>bool</returns>
        public static bool Update(
            long itemID,
            long articleID,
            int siteID,
            int categoryID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleCategory_Update", 4);
            sph.DefineSqlParameter("@ItemID", SqlDbType.BigInt, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@ArticleID", SqlDbType.BigInt, ParameterDirection.Input, articleID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_ArticleCategory table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            long itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleCategory_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.BigInt, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_ArticleCategory table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            long itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleCategory_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.BigInt, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_ArticleCategory table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_ArticleCategory_GetCount",
                null));

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_ArticleCategory table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_ArticleCategory_SelectAll",
                null);

        }

        public static bool DeleteAll(
 long articleID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleCategory_DeleteAll", 1);
            sph.DefineSqlParameter("@ArticleID", SqlDbType.BigInt, ParameterDirection.Input, articleID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);
        }
        public static IDataReader GetList(
            long articleId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleCategory_SelectList", 1);
            sph.DefineSqlParameter("@ArticleID", SqlDbType.BigInt, ParameterDirection.Input, articleId);
            return sph.ExecuteReader();
        }

        /// <summary>
        /// Gets a page of data from the md_ArticleCategory table.
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleCategory_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



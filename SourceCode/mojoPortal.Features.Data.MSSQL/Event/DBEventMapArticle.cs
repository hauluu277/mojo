// Author:					NamDV
// Created:					2015-9-16
// Last Modified:			2015-9-16
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

namespace EventFeature.Data
{

    public static class DBEventMapArticle
    {


        /// <summary>
        /// Inserts a row in the md_EventMapArticle table. Returns new integer id.
        /// </summary>
        /// <param name="articleID"> articleID </param>
        /// <param name="eventID"> eventID </param>
        /// <returns>int</returns>
        public static int Create(
            int articleID,
            int eventID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_EventMapArticle_Insert", 2);
            sph.DefineSqlParameter("@ArticleID", SqlDbType.Int, ParameterDirection.Input, articleID);
            sph.DefineSqlParameter("@EventID", SqlDbType.Int, ParameterDirection.Input, eventID);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_EventMapArticle table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="articleID"> articleID </param>
        /// <param name="eventID"> eventID </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int articleID,
            int eventID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_EventMapArticle_Update", 3);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@ArticleID", SqlDbType.Int, ParameterDirection.Input, articleID);
            sph.DefineSqlParameter("@EventID", SqlDbType.Int, ParameterDirection.Input, eventID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_EventMapArticle table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_EventMapArticle_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        //DeleteAllByArticle
        public static bool DeleteAllByArticle(
            int articleID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_EventMapArticle_Delete_ByArticle", 1);
            sph.DefineSqlParameter("@ArticleID", SqlDbType.Int, ParameterDirection.Input, articleID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }
        /// <summary>
        /// Gets an IDataReader with one row from the md_EventMapArticle table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_EventMapArticle_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }
        public static IDataReader GetByArticleID(
    int articleID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_EventMapArticle_SelectByArticleID", 1);
            sph.DefineSqlParameter("@ArticleID", SqlDbType.Int, ParameterDirection.Input, articleID);
            return sph.ExecuteReader();

        }
        /// <summary>
        /// Gets a count of rows in the md_EventMapArticle table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_EventMapArticle_GetCount",
                null));

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_EventMapArticle table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_EventMapArticle_SelectAll",
                null);

        }

        public static IDataReader GetAllByArticle(int articleID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_EventMapArticle_SelectAll_ByArticle", 1);
            sph.DefineSqlParameter("@ArticleID", SqlDbType.Int, ParameterDirection.Input, articleID);
            return sph.ExecuteReader();
        }
        /// <summary>
        /// Gets a page of data from the md_EventMapArticle table.
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_EventMapArticle_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



// Author:					NAMDV
// Created:					2015-11-5
// Last Modified:			2015-11-5
// 

using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using mojoPortal.Data;

namespace ArticleFeature.Data
{

    public static class DBArticleItemTag
    {
        /// <summary>
        /// Inserts a row in the md_ArticleItemTag table. Returns new integer id.
        /// </summary>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="itemID"> itemID </param>
        /// <param name="tagID"> tagID </param>
        /// <returns>int</returns>
        public static int Create(
            int moduleID,
            int itemID,
            int tagID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleItemTag_Insert", 3);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@TagID", SqlDbType.Int, ParameterDirection.Input, tagID);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_ArticleItemTag table. Returns true if row updated.
        /// </summary>
        /// <param name="id"> id </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="itemID"> itemID </param>
        /// <param name="tagID"> tagID </param>
        /// <returns>bool</returns>
        public static bool Update(
            int id,
            int moduleID,
            int itemID,
            int tagID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleItemTag_Update", 4);
            sph.DefineSqlParameter("@ID", SqlDbType.Int, ParameterDirection.Input, id);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@TagID", SqlDbType.Int, ParameterDirection.Input, tagID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_ArticleItemTag table. Returns true if row deleted.
        /// </summary>
        /// <param name="id"> id </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int id)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleItemTag_Delete", 1);
            sph.DefineSqlParameter("@ID", SqlDbType.Int, ParameterDirection.Input, id);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_ArticleItemTag table.
        /// </summary>
        /// <param name="id"> id </param>
        public static IDataReader GetOne(
            int id)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleItemTag_SelectOne", 1);
            sph.DefineSqlParameter("@ID", SqlDbType.Int, ParameterDirection.Input, id);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_ArticleItemTag table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_ArticleItemTag_GetCount",
                null));

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_ArticleItemTag table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_ArticleItemTag_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the md_ArticleItemTag table.
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleItemTag_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

        public static IDataReader GetAllByArticle(int articleID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleItemTag_SelectAll_ByArticle", 1);
            sph.DefineSqlParameter("@ArticleID", SqlDbType.Int, ParameterDirection.Input, articleID);
            return sph.ExecuteReader();
        }

        public static bool DeleteAllByArticle(
            int articleID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleItemTag_Delete_ByArticle", 1);
            sph.DefineSqlParameter("@ArticleID", SqlDbType.Int, ParameterDirection.Input, articleID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }
    }

}



// Author:					NamDV
// Created:					2015-9-21
// Last Modified:			2015-9-21
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

    public static class DBArticleItemAttachment
    {


        /// <summary>
        /// Inserts a row in the md_ArticleItemAttachments table. Returns new integer id.
        /// </summary>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="itemID"> itemID </param>
        /// <param name="fileID"> fileID </param>
        /// <returns>int</returns>
        public static int Create(
            int moduleID,
            int itemID,
            int fileID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleItemAttachments_Insert", 3);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@FileID", SqlDbType.Int, ParameterDirection.Input, fileID);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_ArticleItemAttachments table. Returns true if row updated.
        /// </summary>
        /// <param name="id"> id </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="itemID"> itemID </param>
        /// <param name="fileID"> fileID </param>
        /// <returns>bool</returns>
        public static bool Update(
            int id,
            int moduleID,
            int itemID,
            int fileID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleItemAttachments_Update", 4);
            sph.DefineSqlParameter("@ID", SqlDbType.Int, ParameterDirection.Input, id);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@FileID", SqlDbType.Int, ParameterDirection.Input, fileID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_ArticleItemAttachments table. Returns true if row deleted.
        /// </summary>
        /// <param name="id"> id </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int id)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleItemAttachments_Delete", 1);
            sph.DefineSqlParameter("@ID", SqlDbType.Int, ParameterDirection.Input, id);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_ArticleItemAttachments table.
        /// </summary>
        /// <param name="id"> id </param>
        public static IDataReader GetOne(
            int id)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleItemAttachments_SelectOne", 1);
            sph.DefineSqlParameter("@ID", SqlDbType.Int, ParameterDirection.Input, id);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_ArticleItemAttachments table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_ArticleItemAttachments_GetCount",
                null));

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_ArticleItemAttachments table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_ArticleItemAttachments_SelectAll",
                null);

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_ArticleItemAttachments by ServerFileName table.
        /// </summary>
        /// <param name="ServerFileName">The ServerFileName</param>
        /// <returns></returns>
        public static IDataReader GetAllByServerFileName(string ServerFileName)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleItemAttachments_SelectAll_ByServerFileName", 1);
            sph.DefineSqlParameter("@ServerFileName", SqlDbType.NVarChar, ParameterDirection.Input, ServerFileName);
            return sph.ExecuteReader();
        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_ArticleItemAttachments by ItemID table.
        /// </summary>
        /// <param name="ItemID">The ItemID</param>
        /// <returns></returns>
        public static IDataReader GetAllByItemID(int ItemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleItemAttachments_SelectAll_ByItemID", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, ItemID);
            return sph.ExecuteReader();
        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_ArticleItemAttachments by FileID table.
        /// </summary>
        /// <param name="FileID">The FileID</param>
        /// <returns></returns>
        public static IDataReader GetAllByFileID(int FileID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleItemAttachments_SelectAll_ByFileID", 1);
            sph.DefineSqlParameter("@FileID", SqlDbType.Int, ParameterDirection.Input, FileID);
            return sph.ExecuteReader();
        }

        /// <summary>
        /// Gets a page of data from the md_ArticleItemAttachments table.
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleItemAttachments_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



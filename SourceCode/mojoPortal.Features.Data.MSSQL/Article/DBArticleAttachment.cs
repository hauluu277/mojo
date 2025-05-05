// Author:					NamDV
// Created:					2015-9-21
// Last Modified:			2015-9-21


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

    public static class DBArticleAttachment
    {


        /// <summary>
        /// Inserts a row in the md_ArticleAttachments table. Returns new integer id.
        /// </summary>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="fileName"> fileName </param>
        /// <param name="serverFileName"> serverFileName </param>
        /// <param name="sizeInKB"> sizeInKB </param>
        /// <param name="downloadCount"> downloadCount </param>
        /// <param name="lastModified"> lastModified </param>
        /// <returns>int</returns>
        public static int Create(
            int moduleID,
            string fileName,
            string serverFileName,
            int sizeInKB,
            int downloadCount,
            DateTime lastModified)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleAttachments_Insert", 6);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@FileName", SqlDbType.NVarChar, 255, ParameterDirection.Input, fileName);
            sph.DefineSqlParameter("@ServerFileName", SqlDbType.NVarChar, 255, ParameterDirection.Input, serverFileName);
            sph.DefineSqlParameter("@SizeInKB", SqlDbType.Int, ParameterDirection.Input, sizeInKB);
            sph.DefineSqlParameter("@DownloadCount", SqlDbType.Int, ParameterDirection.Input, downloadCount);
            sph.DefineSqlParameter("@LastModified", SqlDbType.DateTime, ParameterDirection.Input, lastModified);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_ArticleAttachments table. Returns true if row updated.
        /// </summary>
        /// <param name="fileID"> fileID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="fileName"> fileName </param>
        /// <param name="serverFileName"> serverFileName </param>
        /// <param name="sizeInKB"> sizeInKB </param>
        /// <param name="downloadCount"> downloadCount </param>
        /// <param name="lastModified"> lastModified </param>
        /// <returns>bool</returns>
        public static bool Update(
            int fileID,
            int moduleID,
            string fileName,
            string serverFileName,
            int sizeInKB,
            int downloadCount,
            DateTime lastModified)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleAttachments_Update", 7);
            sph.DefineSqlParameter("@FileID", SqlDbType.Int, ParameterDirection.Input, fileID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@FileName", SqlDbType.NVarChar, 255, ParameterDirection.Input, fileName);
            sph.DefineSqlParameter("@ServerFileName", SqlDbType.NVarChar, 255, ParameterDirection.Input, serverFileName);
            sph.DefineSqlParameter("@SizeInKB", SqlDbType.Int, ParameterDirection.Input, sizeInKB);
            sph.DefineSqlParameter("@DownloadCount", SqlDbType.Int, ParameterDirection.Input, downloadCount);
            sph.DefineSqlParameter("@LastModified", SqlDbType.DateTime, ParameterDirection.Input, lastModified);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_ArticleAttachments table. Returns true if row deleted.
        /// </summary>
        /// <param name="fileID"> fileID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int fileID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleAttachments_Delete", 1);
            sph.DefineSqlParameter("@FileID", SqlDbType.Int, ParameterDirection.Input, fileID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_ArticleAttachments table.
        /// </summary>
        /// <param name="fileID"> fileID </param>
        public static IDataReader GetOne(
            int fileID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleAttachments_SelectOne", 1);
            sph.DefineSqlParameter("@FileID", SqlDbType.Int, ParameterDirection.Input, fileID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_ArticleAttachments table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_ArticleAttachments_GetCount",
                null));

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_ArticleAttachments table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_ArticleAttachments_SelectAll",
                null);

        }
        /// <summary>
        /// Gets an IDataReader with all rows in the md_ArticleAttachments table by ServerFileName.
        /// </summary>
        /// <param name="ServerFileName">The ServerFileName</param>
        /// <returns></returns>
        public static IDataReader GetAllByServerFileName(string ServerFileName)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleAttachments_SelectAll_ByServerFileName", 1);
            sph.DefineSqlParameter("@ServerFileName", SqlDbType.NVarChar, ParameterDirection.Input, ServerFileName);
            return sph.ExecuteReader();
        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_ArticleAttachments And md_ArticleItemAttachments table by ItemID.
        /// </summary>
        /// <param name="ItemID">The ItemID</param>
        /// <returns></returns>
        public static IDataReader GetAllObjectByItemID(int ItemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleAttachments_SelectAll_ByItemID", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, ItemID);
            return sph.ExecuteReader();
        }
        /// <summary>
        /// Gets a page of data from the md_ArticleAttachments table.
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleAttachments_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



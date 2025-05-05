// Author:					NAMDV
// Created:					2015-12-14
// Last Modified:			2015-12-14


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

    public static class DBArticleComments
    {


        /// <summary>
        /// Inserts a row in the md_ArticleComments table. Returns new integer id.
        /// </summary>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="itemID"> itemID </param>
        /// <param name="comment"> comment </param>
        /// <param name="title"> title </param>
        /// <param name="name"> name </param>
        /// <param name="uRL"> uRL </param>
        /// <param name="dateCreated"> dateCreated </param>
        /// <param name="isApproved"> isApproved </param>
        /// <param name="approvedGuid"> approvedGuid </param>
        /// <param name="approvedDate"> approvedDate </param>
        /// <param name="isPublised"> isPublised </param>
        /// <param name="publishedGuid"> publishedGuid </param>
        /// <param name="publishedDate"> publishedDate </param>
        /// <returns>int</returns>
        public static int Create(
            int moduleID,
            int itemID,
            string comment,
            string title,
            string name,
            string uRL,
            DateTime dateCreated,
            bool? isApproved,
            Guid approvedGuid,
            DateTime approvedDate,
            bool? isPublised,
            Guid publishedGuid,
            DateTime publishedDate)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleComments_Insert", 13);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@Comment", SqlDbType.NVarChar, -1, ParameterDirection.Input, comment);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 100, ParameterDirection.Input, title);
            sph.DefineSqlParameter("@Name", SqlDbType.NVarChar, 100, ParameterDirection.Input, name);
            sph.DefineSqlParameter("@URL", SqlDbType.NVarChar, 200, ParameterDirection.Input, uRL);
            sph.DefineSqlParameter("@DateCreated", SqlDbType.DateTime, ParameterDirection.Input, dateCreated);
            sph.DefineSqlParameter("@IsApproved", SqlDbType.Bit, ParameterDirection.Input, isApproved);
            sph.DefineSqlParameter("@ApprovedGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, approvedGuid);
            sph.DefineSqlParameter("@ApprovedDate", SqlDbType.DateTime, ParameterDirection.Input, approvedDate);
            sph.DefineSqlParameter("@IsPublised", SqlDbType.Bit, ParameterDirection.Input, isPublised);
            sph.DefineSqlParameter("@PublishedGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, publishedGuid);
            sph.DefineSqlParameter("@PublishedDate", SqlDbType.DateTime, ParameterDirection.Input, publishedDate);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_ArticleComments table. Returns true if row updated.
        /// </summary>
        /// <param name="articleCommentID"> articleCommentID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="itemID"> itemID </param>
        /// <param name="comment"> comment </param>
        /// <param name="title"> title </param>
        /// <param name="name"> name </param>
        /// <param name="uRL"> uRL </param>
        /// <param name="dateCreated"> dateCreated </param>
        /// <param name="isApproved"> isApproved </param>
        /// <param name="approvedGuid"> approvedGuid </param>
        /// <param name="approvedDate"> approvedDate </param>
        /// <param name="isPublised"> isPublised </param>
        /// <param name="publishedGuid"> publishedGuid </param>
        /// <param name="publishedDate"> publishedDate </param>
        /// <returns>bool</returns>
        public static bool Update(
            int articleCommentID,
            int moduleID,
            int itemID,
            string comment,
            string title,
            string name,
            string uRL,
            DateTime dateCreated,
            bool? isApproved,
            Guid approvedGuid,
            DateTime approvedDate,
            bool? isPublised,
            Guid publishedGuid,
            DateTime publishedDate)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleComments_Update", 14);
            sph.DefineSqlParameter("@ArticleCommentID", SqlDbType.Int, ParameterDirection.Input, articleCommentID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@Comment", SqlDbType.NVarChar, -1, ParameterDirection.Input, comment);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 100, ParameterDirection.Input, title);
            sph.DefineSqlParameter("@Name", SqlDbType.NVarChar, 100, ParameterDirection.Input, name);
            sph.DefineSqlParameter("@URL", SqlDbType.NVarChar, 200, ParameterDirection.Input, uRL);
            sph.DefineSqlParameter("@DateCreated", SqlDbType.DateTime, ParameterDirection.Input, dateCreated);
            sph.DefineSqlParameter("@IsApproved", SqlDbType.Bit, ParameterDirection.Input, isApproved);
            sph.DefineSqlParameter("@ApprovedGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, approvedGuid);
            sph.DefineSqlParameter("@ApprovedDate", SqlDbType.DateTime, ParameterDirection.Input, approvedDate);
            sph.DefineSqlParameter("@IsPublised", SqlDbType.Bit, ParameterDirection.Input, isPublised);
            sph.DefineSqlParameter("@PublishedGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, publishedGuid);
            sph.DefineSqlParameter("@PublishedDate", SqlDbType.DateTime, ParameterDirection.Input, publishedDate);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_ArticleComments table. Returns true if row deleted.
        /// </summary>
        /// <param name="articleCommentID"> articleCommentID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int articleCommentID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleComments_Delete", 1);
            sph.DefineSqlParameter("@ArticleCommentID", SqlDbType.Int, ParameterDirection.Input, articleCommentID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_ArticleComments table.
        /// </summary>
        /// <param name="articleCommentID"> articleCommentID </param>
        public static IDataReader GetOne(
            int articleCommentID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleComments_SelectOne", 1);
            sph.DefineSqlParameter("@ArticleCommentID", SqlDbType.Int, ParameterDirection.Input, articleCommentID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_ArticleComments table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_ArticleComments_GetCount",
                null));

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_ArticleComments table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_ArticleComments_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the md_ArticleComments table.
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleComments_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



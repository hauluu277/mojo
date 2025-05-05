// Author:					Hau Xoac
// Created:					2017-10-30
// Last Modified:			2017-10-30
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

    public static class DBArticleLog
    {


        /// <summary>
        /// Inserts a row in the md_ArticleLog table. Returns new integer id.
        /// </summary>
        /// <param name="articleID"> articleID </param>
        /// <param name="userID"> userID </param>
        /// <param name="postDate"> postDate </param>
        /// <param name="startDate"> startDate </param>
        /// <param name="endDate"> endDate </param>
        /// <param name="isPublic"> isPublic </param>
        /// <param name="isApprove"> isApprove </param>
        /// <param name="comment"> comment </param>
        /// <returns>int</returns>
        public static int Create(
            int articleID,
            int userID,
            DateTime? postDate,
            DateTime? startDate,
            DateTime? endDate,
            bool? isPublic,
            bool? isApprove,
            string comment,
            DateTime createDate)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleLog_Insert", 9);
            sph.DefineSqlParameter("@ArticleID", SqlDbType.Int, ParameterDirection.Input, articleID);
            sph.DefineSqlParameter("@UserID", SqlDbType.Int, ParameterDirection.Input, userID);
            sph.DefineSqlParameter("@PostDate", SqlDbType.DateTime, ParameterDirection.Input, postDate);
            sph.DefineSqlParameter("@StartDate", SqlDbType.DateTime, ParameterDirection.Input, startDate);
            sph.DefineSqlParameter("@EndDate", SqlDbType.DateTime, ParameterDirection.Input, endDate);
            sph.DefineSqlParameter("@IsPublic", SqlDbType.Bit, ParameterDirection.Input, isPublic);
            sph.DefineSqlParameter("@IsApprove", SqlDbType.Bit, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@Comment", SqlDbType.NVarChar, 550, ParameterDirection.Input, comment);
            sph.DefineSqlParameter("@CreateDate", SqlDbType.DateTime, ParameterDirection.Input, createDate);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_ArticleLog table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="articleID"> articleID </param>
        /// <param name="userID"> userID </param>
        /// <param name="postDate"> postDate </param>
        /// <param name="startDate"> startDate </param>
        /// <param name="endDate"> endDate </param>
        /// <param name="isPublic"> isPublic </param>
        /// <param name="isApprove"> isApprove </param>
        /// <param name="comment"> comment </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int articleID,
            int userID,
            DateTime? postDate,
            DateTime? startDate,
            DateTime? endDate,
            bool? isPublic,
            bool? isApprove,
            string comment,
            DateTime createDate)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleLog_Update", 10);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@ArticleID", SqlDbType.Int, ParameterDirection.Input, articleID);
            sph.DefineSqlParameter("@UserID", SqlDbType.Int, ParameterDirection.Input, userID);
            sph.DefineSqlParameter("@PostDate", SqlDbType.DateTime, ParameterDirection.Input, postDate);
            sph.DefineSqlParameter("@StartDate", SqlDbType.DateTime, ParameterDirection.Input, startDate);
            sph.DefineSqlParameter("@EndDate", SqlDbType.DateTime, ParameterDirection.Input, endDate);
            sph.DefineSqlParameter("@IsPublic", SqlDbType.Bit, ParameterDirection.Input, isPublic);
            sph.DefineSqlParameter("@IsApprove", SqlDbType.Bit, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@Comment", SqlDbType.NVarChar, 550, ParameterDirection.Input, comment);
            sph.DefineSqlParameter("@CreateDate", SqlDbType.DateTime, ParameterDirection.Input, createDate);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_ArticleLog table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleLog_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_ArticleLog table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleLog_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }
        /// <summary>
        /// /Lấy danh sách ArticeLog By AticleID
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns></returns>
        public static IDataReader GetListByArticle(
           int articleID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleLog_SelectByArticle", 1);
            sph.DefineSqlParameter("@ArticleID", SqlDbType.Int, ParameterDirection.Input, articleID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_ArticleLog table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_ArticleLog_GetCount",
                null));

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_ArticleLog table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_ArticleLog_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the md_ArticleLog table.
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleLog_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



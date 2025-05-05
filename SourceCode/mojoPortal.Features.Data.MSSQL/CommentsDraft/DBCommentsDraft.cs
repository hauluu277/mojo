// Author:					Trieubv
// Created:					2015-12-2
// Last Modified:			2015-12-2
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

    public static class DBCommentsDraft
    {


        /// <summary>
        /// Inserts a row in the md_CommentsDraft table. Returns new integer id.
        /// </summary>
        /// <param name="siteID"> siteID </param>
        /// <param name="pageID"> pageID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="name"> name </param>
        /// <param name="email"> email </param>
        /// <param name="address"> address </param>
        /// <param name="mobile"> mobile </param>
        /// <param name="comment"> comment </param>
        /// <param name="filePath"> filePath </param>
        /// <param name="dateCreated"> dateCreated </param>
        /// <param name="fTS"> fTS </param>
        /// <param name="isApproved"> isApproved </param>
        /// <param name="isPublished"> isPublished </param>
        /// <param name="dateApproved"> dateApproved </param>
        /// <param name="datePublished"> datePublished </param>
        /// <param name="userApproved"> userApproved </param>
        /// <param name="userPublished"> userPublished </param>
        /// <returns>int</returns>
        public static int Create(
            int siteID,
            int pageID,
            int moduleID,
            string name,
            string email,
            string address,
            string mobile,
            string comment,
            string filePath,
            DateTime dateCreated,
            string fTS,
            bool isApproved,
            bool isPublished,
            DateTime dateApproved,
            bool datePublished,
            int userApproved,
            int userPublished,
            int duThaoID,
            string itemUrl)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_CommentsDraft_Insert", 19);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@PageID", SqlDbType.Int, ParameterDirection.Input, pageID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@Name", SqlDbType.NVarChar, 255, ParameterDirection.Input, name);
            sph.DefineSqlParameter("@Email", SqlDbType.NVarChar, 255, ParameterDirection.Input, email);
            sph.DefineSqlParameter("@Address", SqlDbType.NVarChar, 255, ParameterDirection.Input, address);
            sph.DefineSqlParameter("@Mobile", SqlDbType.NVarChar, 50, ParameterDirection.Input, mobile);
            sph.DefineSqlParameter("@Comment", SqlDbType.NText, ParameterDirection.Input, comment);
            sph.DefineSqlParameter("@FilePath", SqlDbType.NVarChar, 255, ParameterDirection.Input, filePath);
            sph.DefineSqlParameter("@DateCreated", SqlDbType.DateTime, ParameterDirection.Input, dateCreated);
            sph.DefineSqlParameter("@FTS", SqlDbType.NText, ParameterDirection.Input, fTS);
            sph.DefineSqlParameter("@IsApproved", SqlDbType.Bit, ParameterDirection.Input, isApproved);
            sph.DefineSqlParameter("@IsPublished", SqlDbType.Bit, ParameterDirection.Input, isPublished);
            sph.DefineSqlParameter("@DateApproved", SqlDbType.DateTime, ParameterDirection.Input, dateApproved);
            sph.DefineSqlParameter("@DatePublished", SqlDbType.Bit, ParameterDirection.Input, datePublished);
            sph.DefineSqlParameter("@UserApproved", SqlDbType.Int, ParameterDirection.Input, userApproved);
            sph.DefineSqlParameter("@UserPublished", SqlDbType.Int, ParameterDirection.Input, userPublished);
            sph.DefineSqlParameter("@DuThaoID", SqlDbType.Int, ParameterDirection.Input, duThaoID);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, itemUrl);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_CommentsDraft table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="pageID"> pageID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="name"> name </param>
        /// <param name="email"> email </param>
        /// <param name="address"> address </param>
        /// <param name="mobile"> mobile </param>
        /// <param name="comment"> comment </param>
        /// <param name="filePath"> filePath </param>
        /// <param name="dateCreated"> dateCreated </param>
        /// <param name="fTS"> fTS </param>
        /// <param name="isApproved"> isApproved </param>
        /// <param name="isPublished"> isPublished </param>
        /// <param name="dateApproved"> dateApproved </param>
        /// <param name="datePublished"> datePublished </param>
        /// <param name="userApproved"> userApproved </param>
        /// <param name="userPublished"> userPublished </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int siteID,
            int pageID,
            int moduleID,
            string name,
            string email,
            string address,
            string mobile,
            string comment,
            string filePath,
            DateTime dateCreated,
            string fTS,
            bool isApproved,
            bool isPublished,
            DateTime dateApproved,
            bool datePublished,
            int userApproved,
            int userPublished,
            int duThaoID,
            string itemUrl)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_CommentsDraft_Update", 20);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@PageID", SqlDbType.Int, ParameterDirection.Input, pageID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@Name", SqlDbType.NVarChar, 255, ParameterDirection.Input, name);
            sph.DefineSqlParameter("@Email", SqlDbType.NVarChar, 255, ParameterDirection.Input, email);
            sph.DefineSqlParameter("@Address", SqlDbType.NVarChar, 255, ParameterDirection.Input, address);
            sph.DefineSqlParameter("@Mobile", SqlDbType.NVarChar, 50, ParameterDirection.Input, mobile);
            sph.DefineSqlParameter("@Comment", SqlDbType.NText, ParameterDirection.Input, comment);
            sph.DefineSqlParameter("@FilePath", SqlDbType.NVarChar, 255, ParameterDirection.Input, filePath);
            sph.DefineSqlParameter("@DateCreated", SqlDbType.DateTime, ParameterDirection.Input, dateCreated);
            sph.DefineSqlParameter("@FTS", SqlDbType.NText, ParameterDirection.Input, fTS);
            sph.DefineSqlParameter("@IsApproved", SqlDbType.Bit, ParameterDirection.Input, isApproved);
            sph.DefineSqlParameter("@IsPublished", SqlDbType.Bit, ParameterDirection.Input, isPublished);
            sph.DefineSqlParameter("@DateApproved", SqlDbType.DateTime, ParameterDirection.Input, dateApproved);
            sph.DefineSqlParameter("@DatePublished", SqlDbType.Bit, ParameterDirection.Input, datePublished);
            sph.DefineSqlParameter("@UserApproved", SqlDbType.Int, ParameterDirection.Input, userApproved);
            sph.DefineSqlParameter("@UserPublished", SqlDbType.Int, ParameterDirection.Input, userPublished);
            sph.DefineSqlParameter("@DuThaoID", SqlDbType.Int, ParameterDirection.Input, duThaoID);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, itemUrl);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_CommentsDraft table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_CommentsDraft_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_CommentsDraft table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_CommentsDraft_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_CommentsDraft table.
        /// </summary>
        public static int GetCount(int duThaoID, int isApprove, int isPublished)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_CommentsDraft_GetCount", 3);
            sph.DefineSqlParameter("@DuThaoID", SqlDbType.Int, ParameterDirection.Input, duThaoID);
            sph.DefineSqlParameter("@IsApprove", SqlDbType.Int, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@IsPublished", SqlDbType.Int, ParameterDirection.Input, isPublished);
            return Convert.ToInt32(sph.ExecuteScalar());
        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_CommentsDraft table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_CommentsDraft_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the md_CommentsDraft table.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static IDataReader GetPage(
            int duThaoID,
            int isApprove, 
            int isPublished,
            int pageNumber,
            int pageSize,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCount(duThaoID, isApprove, isPublished);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_CommentsDraft_SelectPage", 5);
            sph.DefineSqlParameter("@DuThaoID", SqlDbType.Int, ParameterDirection.Input, duThaoID);
            sph.DefineSqlParameter("@IsApprove", SqlDbType.Int, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@IsPublished", SqlDbType.Int, ParameterDirection.Input, isPublished);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



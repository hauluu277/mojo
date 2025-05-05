// Author:					HAU XOAC
// Created:					2017-11-2
// Last Modified:			2017-11-2
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

    public static class DBArticleViews
    {


        /// <summary>
        /// Inserts a row in the md_ArticleView table. Returns new integer id.
        /// </summary>
        /// <param name="articleID"> articleID </param>
        /// <param name="totalView"> totalView </param>
        /// <param name="dayView"> dayView </param>
        /// <returns>int</returns>
        public static int Create(
            int articleID,
            int totalView,
            DateTime dayView)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleView_Insert", 3);
            sph.DefineSqlParameter("@ArticleID", SqlDbType.Int, ParameterDirection.Input, articleID);
            sph.DefineSqlParameter("@TotalView", SqlDbType.Int, ParameterDirection.Input, totalView);
            sph.DefineSqlParameter("@DayView", SqlDbType.DateTime, ParameterDirection.Input, dayView);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_ArticleView table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="articleID"> articleID </param>
        /// <param name="totalView"> totalView </param>
        /// <param name="dayView"> dayView </param>
        /// <returns>bool</returns>
        public static bool Update(
            decimal itemID,
            int articleID,
            int totalView,
            DateTime dayView)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleView_Update", 4);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Decimal, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@ArticleID", SqlDbType.Int, ParameterDirection.Input, articleID);
            sph.DefineSqlParameter("@TotalView", SqlDbType.Int, ParameterDirection.Input, totalView);
            sph.DefineSqlParameter("@DayView", SqlDbType.DateTime, ParameterDirection.Input, dayView);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);
        }
        public static bool UpdateView(int articleID, string currentDate)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleView_UpdateView", 2);
            sph.DefineSqlParameter("@ArticleID", SqlDbType.Int, ParameterDirection.Input, articleID);
            sph.DefineSqlParameter("@CurrentDay", SqlDbType.VarChar, 10, ParameterDirection.Input, currentDate);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);
        }

        /// <summary>
        /// Deletes a row from the md_ArticleView table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            decimal itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleView_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Decimal, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_ArticleView table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            decimal itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleView_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Decimal, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();
        }

        public static bool IsExit(int articleID, string currentDate)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleView_SelectIsExit", 2);
            sph.DefineSqlParameter("@ArticleID", SqlDbType.Int, ParameterDirection.Input, articleID);
            sph.DefineSqlParameter("@CurrentDay", SqlDbType.VarChar, 10, ParameterDirection.Input, currentDate);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID > 0;
            //return sph.ExecuteReader();
        }

        /// <summary>
        /// Gets a count of rows in the md_ArticleView table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_ArticleView_GetCount",
                null));

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_ArticleView table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_ArticleView_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the md_ArticleView table.
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleView_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



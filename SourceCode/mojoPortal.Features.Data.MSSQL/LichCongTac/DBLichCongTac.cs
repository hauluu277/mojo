// Author:					Trieubv
// Created:					2016-1-4
// Last Modified:			2016-1-4
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

    public static class DBLichCongTac
    {


        /// <summary>
        /// Inserts a row in the md_LichCongTac table. Returns new integer id.
        /// </summary>
        /// <param name="siteID"> siteID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="pageID"> pageID </param>
        /// <param name="startTime"> startTime </param>
        /// <param name="endTime"> endTime </param>
        /// <param name="summary"> summary </param>
        /// <param name="startDate"> startDate </param>
        /// <param name="endDate"> endDate </param>
        /// <param name="week"> week </param>
        /// <param name="dateCreate"> dateCreate </param>
        /// <param name="createBy"> createBy </param>
        /// <param name="fTS"> fTS </param>
        /// <returns>int</returns>
        public static int Create(
            int siteID,
            int moduleID,
            int pageID,
            string startTime,
            string endTime,
            string buoiSang,
            string buoiChieu,
            string buoiToi,
            DateTime startDate,
            DateTime? endDate,
            int week,
            DateTime dateCreate,
            int createBy,
            string fTS,
            DateTime startWeek,
            DateTime endWeek,
            int nam,
            int dayId,
            string diaDiem,
            string categoryAuthor)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_LichCongTac_Insert", 20);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@PageID", SqlDbType.Int, ParameterDirection.Input, pageID);
            sph.DefineSqlParameter("@StartTime", SqlDbType.NVarChar, 50, ParameterDirection.Input, startTime);
            sph.DefineSqlParameter("@EndTime", SqlDbType.NVarChar, 50, ParameterDirection.Input, endTime);
            sph.DefineSqlParameter("@BuoiSang", SqlDbType.NText, ParameterDirection.Input, buoiSang);
            sph.DefineSqlParameter("@BuoiChieu", SqlDbType.NText, ParameterDirection.Input, buoiChieu);
            sph.DefineSqlParameter("@BuoiToi", SqlDbType.NText, ParameterDirection.Input, buoiToi);
            sph.DefineSqlParameter("@StartDate", SqlDbType.DateTime, ParameterDirection.Input, startDate);
            sph.DefineSqlParameter("@EndDate", SqlDbType.DateTime, ParameterDirection.Input, endDate);
            sph.DefineSqlParameter("@Week", SqlDbType.Int, ParameterDirection.Input, week);
            sph.DefineSqlParameter("@DateCreate", SqlDbType.DateTime, ParameterDirection.Input, dateCreate);
            sph.DefineSqlParameter("@CreateBy", SqlDbType.Int, ParameterDirection.Input, createBy);
            sph.DefineSqlParameter("@FTS", SqlDbType.NText, ParameterDirection.Input, fTS);
            sph.DefineSqlParameter("@StartWeek", SqlDbType.DateTime, ParameterDirection.Input, startWeek);
            sph.DefineSqlParameter("@EndWeek", SqlDbType.DateTime, ParameterDirection.Input, endWeek);
            sph.DefineSqlParameter("@Nam", SqlDbType.Int, ParameterDirection.Input, nam);
            sph.DefineSqlParameter("@DayID", SqlDbType.Int, ParameterDirection.Input, dayId);
            sph.DefineSqlParameter("@DiaDiem", SqlDbType.NText, ParameterDirection.Input, diaDiem);
            sph.DefineSqlParameter("@CategoryAuthor", SqlDbType.NVarChar, 250, ParameterDirection.Input, categoryAuthor);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_LichCongTac table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="pageID"> pageID </param>
        /// <param name="startTime"> startTime </param>
        /// <param name="endTime"> endTime </param>
        /// <param name="summary"> summary </param>
        /// <param name="startDate"> startDate </param>
        /// <param name="endDate"> endDate </param>
        /// <param name="week"> week </param>
        /// <param name="dateCreate"> dateCreate </param>
        /// <param name="createBy"> createBy </param>
        /// <param name="fTS"> fTS </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int siteID,
            int moduleID,
            int pageID,
            string startTime,
            string endTime,
            string buoiSang,
            string buoiChieu,
            string buoiToi,
            DateTime startDate,
            DateTime? endDate,
            int week,
            DateTime dateCreate,
            int createBy,
            string fTS,
            DateTime startWeek,
            DateTime endWeek,
            int nam,
            int dayId,
            string diaDiem,
            string categoryAuthor)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_LichCongTac_Update", 21);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@PageID", SqlDbType.Int, ParameterDirection.Input, pageID);
            sph.DefineSqlParameter("@StartTime", SqlDbType.NVarChar, 50, ParameterDirection.Input, startTime);
            sph.DefineSqlParameter("@EndTime", SqlDbType.NVarChar, 50, ParameterDirection.Input, endTime);
            sph.DefineSqlParameter("@BuoiSang", SqlDbType.NText, ParameterDirection.Input, buoiSang);
            sph.DefineSqlParameter("@BuoiChieu", SqlDbType.NText, ParameterDirection.Input, buoiChieu);
            sph.DefineSqlParameter("@BuoiToi", SqlDbType.NText, ParameterDirection.Input, buoiToi);
            sph.DefineSqlParameter("@StartDate", SqlDbType.DateTime, ParameterDirection.Input, startDate);
            sph.DefineSqlParameter("@EndDate", SqlDbType.DateTime, ParameterDirection.Input, endDate);
            sph.DefineSqlParameter("@Week", SqlDbType.Int, ParameterDirection.Input, week);
            sph.DefineSqlParameter("@DateCreate", SqlDbType.DateTime, ParameterDirection.Input, dateCreate);
            sph.DefineSqlParameter("@CreateBy", SqlDbType.Int, ParameterDirection.Input, createBy);
            sph.DefineSqlParameter("@FTS", SqlDbType.NText, ParameterDirection.Input, fTS);
            sph.DefineSqlParameter("@StartWeek", SqlDbType.DateTime, ParameterDirection.Input, startWeek);
            sph.DefineSqlParameter("@EndWeek", SqlDbType.DateTime, ParameterDirection.Input, endWeek);
            sph.DefineSqlParameter("@Nam", SqlDbType.Int, ParameterDirection.Input, nam);
            sph.DefineSqlParameter("@DayID", SqlDbType.Int, ParameterDirection.Input, dayId);
            sph.DefineSqlParameter("@DiaDiem", SqlDbType.NText, ParameterDirection.Input, diaDiem);
            sph.DefineSqlParameter("@CategoryAuthor", SqlDbType.NVarChar, 250, ParameterDirection.Input, categoryAuthor);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_LichCongTac table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_LichCongTac_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_LichCongTac table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_LichCongTac_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_LichCongTac table.
        /// </summary>
        public static int GetCount(int siteID, int moduleID, int week, int year, int dayId, string author, string keyword)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_LichCongTac_GetCount", 7);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@Week", SqlDbType.Int, ParameterDirection.Input, week);
            sph.DefineSqlParameter("@Year", SqlDbType.Int, ParameterDirection.Input, year);
            sph.DefineSqlParameter("@DayID", SqlDbType.Int, ParameterDirection.Input, dayId);
            sph.DefineSqlParameter("@CategoryAuthor", SqlDbType.NVarChar,250, ParameterDirection.Input, author);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);
            return Convert.ToInt32(sph.ExecuteScalar());

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_LichCongTac table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_LichCongTac_SelectAll",
                null);

        }

        public static IDataReader GetPageFix(
    int siteID,
    int moduleID,
    int week,
    int year,
    int dayId,
    string categoryAuthor,
    string keyword,
    int pageNumber,
    int pageSize,
    out int totalPages,
    out int totalCount)
        {
            totalPages = 1;
            int totalRows
                = GetCount(siteID, moduleID, week, year, dayId, categoryAuthor, keyword);
            totalCount = totalRows;
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_LichCongTac_SelectPage", 9);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@Week", SqlDbType.Int, ParameterDirection.Input, week);
            sph.DefineSqlParameter("@Year", SqlDbType.Int, ParameterDirection.Input, year);
            sph.DefineSqlParameter("@DayID", SqlDbType.Int, ParameterDirection.Input, dayId);
            sph.DefineSqlParameter("@CategoryAuthor", SqlDbType.NVarChar,250, ParameterDirection.Input, categoryAuthor);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();
        }


        /// <summary>
        /// Gets a page of data from the md_LichCongTac table.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static IDataReader GetPage(
            int siteID,
            int moduleID,
            int week,
            int year,
            int dayId,
            string author,
            string keyword,
            int pageNumber,
            int pageSize,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCount(siteID, moduleID, week, year, dayId, author, keyword);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_LichCongTac_SelectPage", 9);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@Week", SqlDbType.Int, ParameterDirection.Input, week);
            sph.DefineSqlParameter("@Year", SqlDbType.Int, ParameterDirection.Input, year);
            sph.DefineSqlParameter("@DayID", SqlDbType.Int, ParameterDirection.Input, dayId);
            sph.DefineSqlParameter("@CategoryAuthor", SqlDbType.NVarChar,250, ParameterDirection.Input, author);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



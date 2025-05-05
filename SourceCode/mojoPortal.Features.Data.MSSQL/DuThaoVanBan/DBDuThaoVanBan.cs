// Author:					Trieubv
// Created:					2015-10-27
// Last Modified:			2015-10-27
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

    public static class DBDuThaoVanBan
    {


        /// <summary>
        /// Inserts a row in the md_DuThaoVanBan table. Returns new integer id.
        /// </summary>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="pageID"> pageID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="linhVucID"> linhVucID </param>
        /// <param name="loaiVanBanID"> loaiVanBanID </param>
        /// <param name="title"> title </param>
        /// <param name="summary"> summary </param>
        /// <param name="fileID"> fileID </param>
        /// <param name="createdByUser"> createdByUser </param>
        /// <param name="createdDate"> createdDate </param>
        /// <param name="lastModUserGuid"> lastModUserGuid </param>
        /// <param name="lastModUtc"> lastModUtc </param>
        /// <param name="itemUrl"> itemUrl </param>
        /// <param name="fTS"> fTS </param>
        /// <returns>int</returns>
        public static int Create(
            int moduleID,
            int pageID,
            int siteID,
            int linhVucID,
            int loaiVanBanID,
            string title,
            string summary,
            int fileID,
            string createdByUser,
            DateTime createdDate,
            Guid lastModUserGuid,
            DateTime lastModUtc,
            string itemUrl,
            string fTS,
            bool isPublic,
            int publicByUser,
            DateTime publicDate,
            DateTime startDate,
            DateTime? endDate,
            int? coQuanBanHanhId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_DuThaoVanBan_Insert", 20);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@PageID", SqlDbType.Int, ParameterDirection.Input, pageID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@LinhVucID", SqlDbType.Int, ParameterDirection.Input, linhVucID);
            sph.DefineSqlParameter("@LoaiVanBanID", SqlDbType.Int, ParameterDirection.Input, loaiVanBanID);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 255, ParameterDirection.Input, title);
            sph.DefineSqlParameter("@Summary", SqlDbType.NText, ParameterDirection.Input, summary);
            sph.DefineSqlParameter("@FileID", SqlDbType.Int, ParameterDirection.Input, fileID);
            sph.DefineSqlParameter("@CreatedByUser", SqlDbType.NVarChar, 255, ParameterDirection.Input, createdByUser);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@LastModUserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, lastModUserGuid);
            sph.DefineSqlParameter("@LastModUtc", SqlDbType.DateTime, ParameterDirection.Input, lastModUtc);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, itemUrl);
            sph.DefineSqlParameter("@FTS", SqlDbType.NText, ParameterDirection.Input, fTS);
            sph.DefineSqlParameter("@IsPublic", SqlDbType.Bit, ParameterDirection.Input, isPublic);
            sph.DefineSqlParameter("@PublicByUser", SqlDbType.Int, ParameterDirection.Input, publicByUser);
            sph.DefineSqlParameter("@PublicDate", SqlDbType.DateTime, ParameterDirection.Input, publicDate);
            sph.DefineSqlParameter("@StartDate", SqlDbType.DateTime, ParameterDirection.Input, startDate);
            sph.DefineSqlParameter("@EndDate", SqlDbType.DateTime, ParameterDirection.Input, endDate);
            sph.DefineSqlParameter("@CoQuanBanHanhID", SqlDbType.Int, ParameterDirection.Input, coQuanBanHanhId);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }

        /// <summary>
        /// Updates a row in the md_DuThaoVanBan table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="pageID"> pageID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="linhVucID"> linhVucID </param>
        /// <param name="loaiVanBanID"> loaiVanBanID </param>
        /// <param name="title"> title </param>
        /// <param name="summary"> summary </param>
        /// <param name="fileID"> fileID </param>
        /// <param name="createdByUser"> createdByUser </param>
        /// <param name="createdDate"> createdDate </param>
        /// <param name="lastModUserGuid"> lastModUserGuid </param>
        /// <param name="lastModUtc"> lastModUtc </param>
        /// <param name="itemUrl"> itemUrl </param>
        /// <param name="fTS"> fTS </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int moduleID,
            int pageID,
            int siteID,
            int linhVucID,
            int loaiVanBanID,
            string title,
            string summary,
            int fileID,
            string createdByUser,
            DateTime createdDate,
            Guid lastModUserGuid,
            DateTime lastModUtc,
            string itemUrl,
            string fTS,
            bool isPublic,
            int publicByUser,
            DateTime publicDate,
            DateTime startDate,
            DateTime? endDate,
            int? coQuanBanHanhId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_DuThaoVanBan_Update", 21);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@PageID", SqlDbType.Int, ParameterDirection.Input, pageID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@LinhVucID", SqlDbType.Int, ParameterDirection.Input, linhVucID);
            sph.DefineSqlParameter("@LoaiVanBanID", SqlDbType.Int, ParameterDirection.Input, loaiVanBanID);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 255, ParameterDirection.Input, title);
            sph.DefineSqlParameter("@Summary", SqlDbType.NText, ParameterDirection.Input, summary);
            sph.DefineSqlParameter("@FileID", SqlDbType.Int, ParameterDirection.Input, fileID);
            sph.DefineSqlParameter("@CreatedByUser", SqlDbType.NVarChar, 255, ParameterDirection.Input, createdByUser);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@LastModUserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, lastModUserGuid);
            sph.DefineSqlParameter("@LastModUtc", SqlDbType.DateTime, ParameterDirection.Input, lastModUtc);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, itemUrl);
            sph.DefineSqlParameter("@FTS", SqlDbType.NText, ParameterDirection.Input, fTS);
            sph.DefineSqlParameter("@IsPublic", SqlDbType.Bit, ParameterDirection.Input, isPublic);
            sph.DefineSqlParameter("@PublicByUser", SqlDbType.Int, ParameterDirection.Input, publicByUser);
            sph.DefineSqlParameter("@PublicDate", SqlDbType.DateTime, ParameterDirection.Input, publicDate);
            sph.DefineSqlParameter("@StartDate", SqlDbType.DateTime, ParameterDirection.Input, startDate);
            sph.DefineSqlParameter("@EndDate", SqlDbType.DateTime, ParameterDirection.Input, endDate);
            sph.DefineSqlParameter("@CoQuanBanHanhID", SqlDbType.Int, ParameterDirection.Input, coQuanBanHanhId);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_DuThaoVanBan table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_DuThaoVanBan_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_DuThaoVanBan table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_DuThaoVanBan_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_DuThaoVanBan table.
        /// </summary>
        public static int GetCount(int siteID, int moduleID, int linhvucID, int loaiID, string keyword, bool? status, int? dateExpires, DateTime? date)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_DuThaoVanBan_GetCount", 8);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@LinhVucID", SqlDbType.Int, ParameterDirection.Input, linhvucID);
            sph.DefineSqlParameter("@LoaiID", SqlDbType.Int, ParameterDirection.Input, loaiID);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@Status", SqlDbType.Bit, ParameterDirection.Input, status);
            sph.DefineSqlParameter("@DateExpires", SqlDbType.Int, ParameterDirection.Input, dateExpires);
            sph.DefineSqlParameter("@Date", SqlDbType.DateTime, ParameterDirection.Input, date);
            return Convert.ToInt32(sph.ExecuteScalar());
        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_DuThaoVanBan table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_DuThaoVanBan_SelectAll",
                null);

        }
        public static IDataReader GetOrther(int siteID, int moduleID, int top, int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_DuThaoVanBan_SelectOrther", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, top);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a page of data from the md_DuThaoVanBan table.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static IDataReader GetPage(
            int siteID, 
            int moduleID, 
            int linhvucID, 
            int loaiID, 
            string keyword,
            bool? status,
            int? dateExpires,
            DateTime? date,
            int pageNumber,
            int pageSize,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCount(siteID, moduleID, linhvucID, loaiID, keyword, status, dateExpires, date);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_DuThaoVanBan_SelectPage", 10);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@LinhVucID", SqlDbType.Int, ParameterDirection.Input, linhvucID);
            sph.DefineSqlParameter("@LoaiID", SqlDbType.Int, ParameterDirection.Input, loaiID);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@Status", SqlDbType.Bit, ParameterDirection.Input, status);
            sph.DefineSqlParameter("@DateExpires", SqlDbType.Int, ParameterDirection.Input, dateExpires);
            sph.DefineSqlParameter("@Date", SqlDbType.DateTime, ParameterDirection.Input, date);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



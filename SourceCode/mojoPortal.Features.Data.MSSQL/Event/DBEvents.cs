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

    public static class DBEvents
    {


        /// <summary>
        /// Inserts a row in the md_Events table. Returns new integer id.
        /// </summary>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="categoryID"> categoryID </param>
        /// <param name="title"> title </param>
        /// <param name="summary"> summary </param>
        /// <param name="description"> description </param>
        /// <param name="imageUrl"> imageUrl </param>
        /// <param name="startDate"> startDate </param>
        /// <param name="endDate"> endDate </param>
        /// <param name="commentCount"> commentCount </param>
        /// <param name="hitCount"> hitCount </param>
        /// <param name="eventGuid"> eventGuid </param>
        /// <param name="moduleGuid"> moduleGuid </param>
        /// <param name="location"> location </param>
        /// <param name="userGuid"> userGuid </param>
        /// <param name="createdByUser"> createdByUser </param>
        /// <param name="createdDate"> createdDate </param>
        /// <param name="lastModUserGuid"> lastModUserGuid </param>
        /// <param name="lastModUtc"> lastModUtc </param>
        /// <param name="itemUrl"> itemUrl </param>
        /// <param name="metaTitle"> metaTitle </param>
        /// <param name="metaKeywords"> metaKeywords </param>
        /// <param name="metaDescription"> metaDescription </param>
        /// <param name="isApproved"> isApproved </param>
        /// <param name="approvedGuid"> approvedGuid </param>
        /// <param name="approvedDate"> approvedDate </param>
        /// <param name="isPublished"> isPublished </param>
        /// <param name="publishedGuid"> publishedGuid </param>
        /// <param name="publishedDate"> publishedDate </param>
        /// <param name="allowComment"> allowComment </param>
        /// <param name="isHot"> isHot </param>
        /// <param name="isHome"> isHome </param>
        /// <param name="tag"> tag </param>
        /// <param name="fTS"> fTS </param>
        /// <param name="langID"> langID </param>
        /// <returns>int</returns>
        public static int Create(
            int moduleID,
            int siteID,
            int categoryID,
            string title,
            string summary,
            string description,
            string imageUrl,
            DateTime startDate,
            DateTime? endDate,
            int commentCount,
            int hitCount,
            Guid eventGuid,
            Guid moduleGuid,
            string location,
            Guid userGuid,
            string createdByUser,
            DateTime createdDate,
            Guid lastModUserGuid,
            DateTime lastModUtc,
            string itemUrl,
            string metaTitle,
            string metaKeywords,
            string metaDescription,
            bool? isApproved,
            Guid approvedGuid,
            DateTime approvedDate,
            bool? isPublished,
            Guid publishedGuid,
            DateTime publishedDate,
            bool allowComment,
            bool isHot,
            bool isHome,
            string tag,
            string fTS,
            int langID,
            string commentByBoss,
            DateTime? startTime,
            DateTime? endTime)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Events_Insert", 38);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 450, ParameterDirection.Input, title);
            sph.DefineSqlParameter("@Summary", SqlDbType.NVarChar, 1000, ParameterDirection.Input, summary);
            sph.DefineSqlParameter("@Description", SqlDbType.NVarChar, -1, ParameterDirection.Input, description);
            sph.DefineSqlParameter("@ImageUrl", SqlDbType.NVarChar, 550, ParameterDirection.Input, imageUrl);
            sph.DefineSqlParameter("@StartDate", SqlDbType.DateTime, ParameterDirection.Input, startDate);
            sph.DefineSqlParameter("@EndDate", SqlDbType.DateTime, ParameterDirection.Input, endDate);
            sph.DefineSqlParameter("@CommentCount", SqlDbType.Int, ParameterDirection.Input, commentCount);
            sph.DefineSqlParameter("@HitCount", SqlDbType.Int, ParameterDirection.Input, hitCount);
            sph.DefineSqlParameter("@EventGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, eventGuid);
            sph.DefineSqlParameter("@ModuleGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, moduleGuid);
            sph.DefineSqlParameter("@Location", SqlDbType.NText, ParameterDirection.Input, location);
            sph.DefineSqlParameter("@UserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, userGuid);
            sph.DefineSqlParameter("@CreatedByUser", SqlDbType.NVarChar, 100, ParameterDirection.Input, createdByUser);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@LastModUserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, lastModUserGuid);
            sph.DefineSqlParameter("@LastModUtc", SqlDbType.DateTime, ParameterDirection.Input, lastModUtc);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, itemUrl);
            sph.DefineSqlParameter("@MetaTitle", SqlDbType.NVarChar, 255, ParameterDirection.Input, metaTitle);
            sph.DefineSqlParameter("@MetaKeywords", SqlDbType.NVarChar, 255, ParameterDirection.Input, metaKeywords);
            sph.DefineSqlParameter("@MetaDescription", SqlDbType.NVarChar, 255, ParameterDirection.Input, metaDescription);
            sph.DefineSqlParameter("@IsApproved", SqlDbType.Bit, ParameterDirection.Input, isApproved);
            sph.DefineSqlParameter("@ApprovedGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, approvedGuid);
            sph.DefineSqlParameter("@ApprovedDate", SqlDbType.DateTime, ParameterDirection.Input, approvedDate);
            sph.DefineSqlParameter("@IsPublished", SqlDbType.Bit, ParameterDirection.Input, isPublished);
            sph.DefineSqlParameter("@PublishedGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, publishedGuid);
            sph.DefineSqlParameter("@PublishedDate", SqlDbType.DateTime, ParameterDirection.Input, publishedDate);
            sph.DefineSqlParameter("@AllowComment", SqlDbType.Bit, ParameterDirection.Input, allowComment);
            sph.DefineSqlParameter("@IsHot", SqlDbType.Bit, ParameterDirection.Input, isHot);
            sph.DefineSqlParameter("@IsHome", SqlDbType.Bit, ParameterDirection.Input, isHome);
            sph.DefineSqlParameter("@Tag", SqlDbType.NVarChar, -1, ParameterDirection.Input, tag);
            sph.DefineSqlParameter("@FTS", SqlDbType.NText, ParameterDirection.Input, fTS);
            sph.DefineSqlParameter("@LangID", SqlDbType.Int, ParameterDirection.Input, langID);
            sph.DefineSqlParameter("@CommentByBoss", SqlDbType.NVarChar, 1500, ParameterDirection.Input, commentByBoss);
            sph.DefineSqlParameter("@StartTime", SqlDbType.DateTime, ParameterDirection.Input, startTime);
            sph.DefineSqlParameter("@EndTime", SqlDbType.DateTime, ParameterDirection.Input, endTime);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_Events table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="categoryID"> categoryID </param>
        /// <param name="title"> title </param>
        /// <param name="summary"> summary </param>
        /// <param name="description"> description </param>
        /// <param name="imageUrl"> imageUrl </param>
        /// <param name="startDate"> startDate </param>
        /// <param name="endDate"> endDate </param>
        /// <param name="commentCount"> commentCount </param>
        /// <param name="hitCount"> hitCount </param>
        /// <param name="eventGuid"> eventGuid </param>
        /// <param name="moduleGuid"> moduleGuid </param>
        /// <param name="location"> location </param>
        /// <param name="userGuid"> userGuid </param>
        /// <param name="createdByUser"> createdByUser </param>
        /// <param name="createdDate"> createdDate </param>
        /// <param name="lastModUserGuid"> lastModUserGuid </param>
        /// <param name="lastModUtc"> lastModUtc </param>
        /// <param name="itemUrl"> itemUrl </param>
        /// <param name="metaTitle"> metaTitle </param>
        /// <param name="metaKeywords"> metaKeywords </param>
        /// <param name="metaDescription"> metaDescription </param>
        /// <param name="isApproved"> isApproved </param>
        /// <param name="approvedGuid"> approvedGuid </param>
        /// <param name="approvedDate"> approvedDate </param>
        /// <param name="isPublished"> isPublished </param>
        /// <param name="publishedGuid"> publishedGuid </param>
        /// <param name="publishedDate"> publishedDate </param>
        /// <param name="allowComment"> allowComment </param>
        /// <param name="isHot"> isHot </param>
        /// <param name="isHome"> isHome </param>
        /// <param name="tag"> tag </param>
        /// <param name="fTS"> fTS </param>
        /// <param name="langID"> langID </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int moduleID,
            int siteID,
            int categoryID,
            string title,
            string summary,
            string description,
            string imageUrl,
            DateTime startDate,
            DateTime? endDate,
            int commentCount,
            int hitCount,
            Guid eventGuid,
            Guid moduleGuid,
            string location,
            Guid userGuid,
            string createdByUser,
            DateTime createdDate,
            Guid lastModUserGuid,
            DateTime lastModUtc,
            string itemUrl,
            string metaTitle,
            string metaKeywords,
            string metaDescription,
            bool? isApproved,
            Guid approvedGuid,
            DateTime approvedDate,
            bool? isPublished,
            Guid publishedGuid,
            DateTime publishedDate,
            bool allowComment,
            bool isHot,
            bool isHome,
            string tag,
            string fTS,
            int langID,
            string commentByBoss,
            DateTime? startTime,
            DateTime? endTime)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Events_Update", 39);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 450, ParameterDirection.Input, title);
            sph.DefineSqlParameter("@Summary", SqlDbType.NVarChar, 1000, ParameterDirection.Input, summary);
            sph.DefineSqlParameter("@Description", SqlDbType.NText, ParameterDirection.Input, description);
            sph.DefineSqlParameter("@ImageUrl", SqlDbType.NVarChar, 550, ParameterDirection.Input, imageUrl);
            sph.DefineSqlParameter("@StartDate", SqlDbType.DateTime, ParameterDirection.Input, startDate);
            sph.DefineSqlParameter("@EndDate", SqlDbType.DateTime, ParameterDirection.Input, endDate);
            sph.DefineSqlParameter("@CommentCount", SqlDbType.Int, ParameterDirection.Input, commentCount);
            sph.DefineSqlParameter("@HitCount", SqlDbType.Int, ParameterDirection.Input, hitCount);
            sph.DefineSqlParameter("@EventGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, eventGuid);
            sph.DefineSqlParameter("@ModuleGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, moduleGuid);
            sph.DefineSqlParameter("@Location", SqlDbType.NText, ParameterDirection.Input, location);
            sph.DefineSqlParameter("@UserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, userGuid);
            sph.DefineSqlParameter("@CreatedByUser", SqlDbType.NVarChar, 100, ParameterDirection.Input, createdByUser);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@LastModUserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, lastModUserGuid);
            sph.DefineSqlParameter("@LastModUtc", SqlDbType.DateTime, ParameterDirection.Input, lastModUtc);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, itemUrl);
            sph.DefineSqlParameter("@MetaTitle", SqlDbType.NVarChar, 255, ParameterDirection.Input, metaTitle);
            sph.DefineSqlParameter("@MetaKeywords", SqlDbType.NVarChar, 255, ParameterDirection.Input, metaKeywords);
            sph.DefineSqlParameter("@MetaDescription", SqlDbType.NVarChar, 255, ParameterDirection.Input, metaDescription);
            sph.DefineSqlParameter("@IsApproved", SqlDbType.Bit, ParameterDirection.Input, isApproved);
            sph.DefineSqlParameter("@ApprovedGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, approvedGuid);
            sph.DefineSqlParameter("@ApprovedDate", SqlDbType.DateTime, ParameterDirection.Input, approvedDate);
            sph.DefineSqlParameter("@IsPublished", SqlDbType.Bit, ParameterDirection.Input, isPublished);
            sph.DefineSqlParameter("@PublishedGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, publishedGuid);
            sph.DefineSqlParameter("@PublishedDate", SqlDbType.DateTime, ParameterDirection.Input, publishedDate);
            sph.DefineSqlParameter("@AllowComment", SqlDbType.Bit, ParameterDirection.Input, allowComment);
            sph.DefineSqlParameter("@IsHot", SqlDbType.Bit, ParameterDirection.Input, isHot);
            sph.DefineSqlParameter("@IsHome", SqlDbType.Bit, ParameterDirection.Input, isHome);
            sph.DefineSqlParameter("@Tag", SqlDbType.NVarChar, -1, ParameterDirection.Input, tag);
            sph.DefineSqlParameter("@FTS", SqlDbType.NText, ParameterDirection.Input, fTS);
            sph.DefineSqlParameter("@LangID", SqlDbType.Int, ParameterDirection.Input, langID);
            sph.DefineSqlParameter("@CommentByBoss", SqlDbType.NVarChar, 1500, ParameterDirection.Input, commentByBoss);
            sph.DefineSqlParameter("@StartTime", SqlDbType.DateTime, ParameterDirection.Input, startTime);
            sph.DefineSqlParameter("@EndTime", SqlDbType.DateTime, ParameterDirection.Input, endTime);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_Events table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Events_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }
        public static IDataReader GetEventHot(int siteID, int number)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Events_SelectEventHot", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@Number", SqlDbType.Int, ParameterDirection.Input, number);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_Events table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Events_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_Events table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_Events_GetCount",
                null));

        }

        public static int GetCount(
            int siteId,
            int moduleId,
             int categoryID,
            int isApprove,
            int isPublish,
            string keyword
            )
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Events_GetCount_ByParameter", 6);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@IsApprove", SqlDbType.Int, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@IsPublish", SqlDbType.Int, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, ParameterDirection.Input, keyword);
            return Convert.ToInt32(sph.ExecuteScalar());
        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_Events table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_Events_SelectAll",
                null);

        }

        public static IDataReader GetAllForArticle(int siteID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Events_SelectAll_ForArticle", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            return sph.ExecuteReader();
        }

        /// <summary>
        /// Gets a page of data from the md_Events table.
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Events_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }


        public static IDataReader GetPage(
            int siteId,
            int moduleId,
            int pageNumber,
            int pageSize,
            int categoryID,
            int isApprove,
            int isPublish,
            string keyword,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCount(siteId, moduleId, categoryID, isApprove, isPublish, keyword);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Events_SelectPage_ByParameter", 8);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@IsApprove", SqlDbType.Int, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@IsPublish", SqlDbType.Int, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, ParameterDirection.Input, keyword);
            return sph.ExecuteReader();

        }
        public static int GetCountForEndUser(
            int siteId,
            int moduleId,
            int categoryID,
            string keyword
            )
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Events_GetCount_ForEndUser", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, ParameterDirection.Input, keyword);
            return Convert.ToInt32(sph.ExecuteScalar());
        }
        public static IDataReader GetPageForEndUser(
            int siteId,
            int moduleId,
            int pageNumber,
            int pageSize,
            int categoryID,
            string keyword,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCountForEndUser(siteId, moduleId, categoryID, keyword);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Events_SelectPage_ForEndUser", 6);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, ParameterDirection.Input, keyword);
            return sph.ExecuteReader();

        }


        public static IDataReader GetOthersPageByModuleId(
          int moduleID,
          int pageSize,
          int currentPage)
        {

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Events_Others_SelectPage_ByModuleID", 3);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, currentPage);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            return sph.ExecuteReader();

        }



        public static IDataReader GetTopHot(
        int top, int siteId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Events_SelectTopHot", 2);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, top);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            return sph.ExecuteReader();
        }

        public static IDataReader GetTopOrther(
        int top, int siteId, string skips)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Events_SelectTopSkip", 3);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, top);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@Skips", SqlDbType.NVarChar, 250, ParameterDirection.Input, skips);
            return sph.ExecuteReader();
        }

        public static IDataReader GetTopSkipId(
        int top, int siteId, int itemId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Events_SelectTopSkipID", 3);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, top);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemId);
            return sph.ExecuteReader();
        }

        public static IDataReader GetEventTopOrther(
           int CategoryID, int ItemId, int Top, bool Hot)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Events_SelectTopOrther", 4);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, CategoryID);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, ItemId);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, Top);
            sph.DefineSqlParameter("@Hot", SqlDbType.Bit, ParameterDirection.Input, Hot);
            return sph.ExecuteReader();

        }
    }

}




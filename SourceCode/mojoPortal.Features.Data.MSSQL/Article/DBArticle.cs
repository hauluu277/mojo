// Author:					HiNet JSC
// Created:					2014-7-2
// Last Modified:			2014-7-2
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

namespace ArticleFeature.Data
{

    public static class DBArticles
    {

        /// <summary>
        /// Inserts a row in the md_Articles table. Returns new integer id.
        /// </summary>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="categoryID"> categoryID </param>
        /// <param name="title"> title </param>
        /// <param name="summary"> summary </param>
        /// <param name="description"> description </param>
        /// <param name="imageUrl"> imageUrl </param>
        /// <param name="startDate"> startDate </param>
        /// <param name="endDate"> endDate </param>
        /// <param name="commentCount"> commentCount </param>
        /// <param name="hitCount"> hitCount </param>
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
        /// <param name="allowComment"> allowComment </param>
        /// <param name="isHot"> isHot </param>
        /// <param name="tag"> tag </param>
        /// <returns>int</returns>
        public static int Create(
            int moduleID,
            int siteId,
            int categoryID,
            string title,
            string summary,
            string description,
            string imageUrl,
            DateTime startDate,
            DateTime? endDate,
            int commentCount,
            int hitCount,
            Guid articleGuid,
            Guid moduleGuid,
            string location,
            Guid userGuid,
            string createdByUser,
            DateTime createdDate,
            Guid lastModUserGuid,
            DateTime lastModUtc,
            string itemUrl,
            string metaTitle,
            string metaDescription,
            bool? isApproved,
            Guid approvedGuid,
            DateTime approvedDate,
            bool allowComment,
            bool isHot,
            bool isHome,
            string tag,
            string fts,
            bool? isPublished,
            Guid publishedGuid,
            DateTime publishedDate,
            bool includeInFeed,
            string commentByBoss,
            string audioUrl,
            Guid pollGuid,
            bool allowWCAG,
            string compiledMeta,
            string metaCreator,
            string metaIdentifier,
            string metaPublisher,
            DateTime? metaDate,
            bool isDelete,
            string articleReference,
            string titleFTS,
            string authorFTS,
            string sapoFTS,
            DateTime createDateArticle,
            //bool hotNew
            string viTriHienThiNgayDang,
            bool isHienThiTacGia
            )
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Articles_Insert", 51);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 450, ParameterDirection.Input, title);
            sph.DefineSqlParameter("@Summary", SqlDbType.NVarChar, 1000, ParameterDirection.Input, summary);
            sph.DefineSqlParameter("@Description", SqlDbType.NVarChar, -1, ParameterDirection.Input, description);
            sph.DefineSqlParameter("@ImageUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, imageUrl);
            sph.DefineSqlParameter("@StartDate", SqlDbType.DateTime, ParameterDirection.Input, startDate);
            sph.DefineSqlParameter("@EndDate", SqlDbType.DateTime, ParameterDirection.Input, endDate);
            sph.DefineSqlParameter("@CommentCount", SqlDbType.Int, ParameterDirection.Input, commentCount);
            sph.DefineSqlParameter("@HitCount", SqlDbType.Int, ParameterDirection.Input, hitCount);
            sph.DefineSqlParameter("@ArticleGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, articleGuid);
            sph.DefineSqlParameter("@ModuleGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, moduleGuid);
            sph.DefineSqlParameter("@Location", SqlDbType.NVarChar, -1, ParameterDirection.Input, location);
            sph.DefineSqlParameter("@UserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, userGuid);
            sph.DefineSqlParameter("@CreatedByUser", SqlDbType.NVarChar, 100, ParameterDirection.Input, createdByUser);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@LastModUserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, lastModUserGuid);
            sph.DefineSqlParameter("@LastModUtc", SqlDbType.DateTime, ParameterDirection.Input, lastModUtc);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, itemUrl);
            sph.DefineSqlParameter("@MetaTitle", SqlDbType.NVarChar, 255, ParameterDirection.Input, metaTitle);
            sph.DefineSqlParameter("@MetaDescription", SqlDbType.NVarChar, 255, ParameterDirection.Input, metaDescription);
            sph.DefineSqlParameter("@IsApproved", SqlDbType.Bit, ParameterDirection.Input, isApproved);
            sph.DefineSqlParameter("@ApprovedGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, approvedGuid);
            sph.DefineSqlParameter("@ApprovedDate", SqlDbType.DateTime, ParameterDirection.Input, approvedDate);
            sph.DefineSqlParameter("@AllowComment", SqlDbType.Bit, ParameterDirection.Input, allowComment);
            sph.DefineSqlParameter("@IsHot", SqlDbType.Bit, ParameterDirection.Input, isHot);
            sph.DefineSqlParameter("@IsHome", SqlDbType.Bit, ParameterDirection.Input, isHome);
            sph.DefineSqlParameter("@Tag", SqlDbType.NVarChar, -1, ParameterDirection.Input, tag);
            sph.DefineSqlParameter("@FTS", SqlDbType.NVarChar, -1, ParameterDirection.Input, fts);
            sph.DefineSqlParameter("@IsPublished", SqlDbType.Bit, ParameterDirection.Input, isPublished);
            sph.DefineSqlParameter("@PublishedGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, publishedGuid);
            sph.DefineSqlParameter("@PublishedDate", SqlDbType.DateTime, ParameterDirection.Input, publishedDate);
            sph.DefineSqlParameter("@IncludeInFeed", SqlDbType.Bit, ParameterDirection.Input, includeInFeed);
            sph.DefineSqlParameter("@CommentByBoss", SqlDbType.NVarChar, -1, ParameterDirection.Input, commentByBoss);
            sph.DefineSqlParameter("@AudioUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, audioUrl);
            sph.DefineSqlParameter("@PollGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, pollGuid);
            sph.DefineSqlParameter("@AllowWCAG", SqlDbType.Bit, ParameterDirection.Input, allowWCAG);
            sph.DefineSqlParameter("@CompiledMeta", SqlDbType.NVarChar, -1, ParameterDirection.Input, compiledMeta);
            sph.DefineSqlParameter("@MetaCreator", SqlDbType.NVarChar, 255, ParameterDirection.Input, metaCreator);
            sph.DefineSqlParameter("@MetaIdentifier", SqlDbType.NVarChar, 255, ParameterDirection.Input, metaIdentifier);
            sph.DefineSqlParameter("@MetaPublisher", SqlDbType.NVarChar, 255, ParameterDirection.Input, metaPublisher);
            sph.DefineSqlParameter("@MetaDate", SqlDbType.DateTime, ParameterDirection.Input, metaDate);
            sph.DefineSqlParameter("@IsDelete", SqlDbType.Bit, ParameterDirection.Input, isDelete);
            sph.DefineSqlParameter("@ArticleReference", SqlDbType.NVarChar, 255, ParameterDirection.Input, articleReference);
            sph.DefineSqlParameter("@TitleFTS", SqlDbType.NVarChar, -1, ParameterDirection.Input, titleFTS);
            sph.DefineSqlParameter("@AuthorFTS", SqlDbType.NVarChar, -1, ParameterDirection.Input, authorFTS);
            sph.DefineSqlParameter("@SapoFTS", SqlDbType.NVarChar, -1, ParameterDirection.Input, sapoFTS);
            sph.DefineSqlParameter("@CreateDateArticle", SqlDbType.DateTime, ParameterDirection.Input, createDateArticle);
            //sph.DefineSqlParameter("@IsHotNew", SqlDbType.Bit, ParameterDirection.Input, hotNew);
            sph.DefineSqlParameter("@ViTriHienThiNgayDang", SqlDbType.NVarChar, -1, ParameterDirection.Input, viTriHienThiNgayDang);
            sph.DefineSqlParameter("@IsHienThiTacGia", SqlDbType.Bit, ParameterDirection.Input, isHienThiTacGia);

            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        public static IDataReader GetSearch(int categoryId, int step, int isPublished)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectSearch", 3);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryId);
            sph.DefineSqlParameter("@Step", SqlDbType.Int, ParameterDirection.Input, step);
            sph.DefineSqlParameter("@IsPublished", SqlDbType.Int, ParameterDirection.Input, isPublished);
            return sph.ExecuteReader();
        }


        public static int GetCountForReference(
           int categoryId,
           string keyword,
           DateTime? searchDate
   )
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_GetCount_ForReference", 3);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryId);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@SearchDate", SqlDbType.DateTime, ParameterDirection.Input, searchDate);
            return Convert.ToInt32(sph.ExecuteScalar());
        }



        public static IDataReader GetPageForReference(
   int pageNumber,
   int pageSize,
   int categoryId,
   string keyword,
   DateTime? searchDate,
   out int totalPages,
   out int totalCount)
        {
            totalPages = 1;
            int totalRows
                = GetCountForReference(categoryId, keyword, searchDate);
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectPage_ArticleReference", 5);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryId);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@SearchDate", SqlDbType.DateTime, ParameterDirection.Input, searchDate);
            return sph.ExecuteReader();
        }



        /// <summary>
        /// Updates a row in the md_Articles table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="categoryID"> categoryID </param>
        /// <param name="title"> title </param>
        /// <param name="summary"> summary </param>
        /// <param name="description"> description </param>
        /// <param name="imageUrl"> imageUrl </param>
        /// <param name="startDate"> startDate </param>
        /// <param name="endDate"> endDate </param>
        /// <param name="commentCount"> commentCount </param>
        /// <param name="hitCount"> hitCount </param>
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
        /// <param name="allowComment"> allowComment </param>
        /// <param name="isHot"> isHot </param>
        /// <param name="tag"> tag </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int moduleID,
            int siteId,
            int categoryID,
            string title,
            string summary,
            string description,
            string imageUrl,
            DateTime startDate,
            DateTime? endDate,
            int commentCount,
            int hitCount,
            Guid moduleGuid,
            string location,
            Guid userGuid,
            string createdByUser,
            DateTime createdDate,
            Guid lastModUserGuid,
            DateTime lastModUtc,
            string itemUrl,
            string metaTitle,
            string metaDescription,
            bool? isApproved,
            Guid approvedGuid,
            DateTime approvedDate,
            bool allowComment,
            bool isHot,
            bool isHome,
            string tag,
            string fts,
            bool? isPublished,
            Guid publishedGuid,
            DateTime publishedDate,
            bool includeInFeed,
            string commentByBoss,
            string audioUrl,
            Guid pollGuid,
            bool allowWCAG,
            string compiledMeta,
            string metaCreator,
            string metaIdentifier,
            string metaPublisher,
            DateTime? metaDate,
            bool isDelete,
            string articleReference,
            string titleFTS,
            string authorFTS,
            string sapoFTS,
            DateTime createDateArticle,
            //bool hotNew
            string viTriHienThiNgayDang,
            bool isHienThiTacGia
            )
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Articles_Update", 51);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 450, ParameterDirection.Input, title);
            sph.DefineSqlParameter("@Summary", SqlDbType.NVarChar, 1000, ParameterDirection.Input, summary);
            sph.DefineSqlParameter("@Description", SqlDbType.NVarChar, -1, ParameterDirection.Input, description);
            sph.DefineSqlParameter("@ImageUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, imageUrl);
            sph.DefineSqlParameter("@StartDate", SqlDbType.DateTime, ParameterDirection.Input, startDate);
            sph.DefineSqlParameter("@EndDate", SqlDbType.DateTime, ParameterDirection.Input, endDate);
            sph.DefineSqlParameter("@CommentCount", SqlDbType.Int, ParameterDirection.Input, commentCount);
            sph.DefineSqlParameter("@HitCount", SqlDbType.Int, ParameterDirection.Input, hitCount);
            sph.DefineSqlParameter("@ModuleGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, moduleGuid);
            sph.DefineSqlParameter("@Location", SqlDbType.NVarChar, -1, ParameterDirection.Input, location);
            sph.DefineSqlParameter("@UserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, userGuid);
            sph.DefineSqlParameter("@CreatedByUser", SqlDbType.NVarChar, 100, ParameterDirection.Input, createdByUser);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@LastModUserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, lastModUserGuid);
            sph.DefineSqlParameter("@LastModUtc", SqlDbType.DateTime, ParameterDirection.Input, lastModUtc);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, itemUrl);
            sph.DefineSqlParameter("@MetaTitle", SqlDbType.NVarChar, 255, ParameterDirection.Input, metaTitle);
            sph.DefineSqlParameter("@MetaDescription", SqlDbType.NVarChar, 255, ParameterDirection.Input, metaDescription);
            sph.DefineSqlParameter("@IsApproved", SqlDbType.Bit, ParameterDirection.Input, isApproved);
            sph.DefineSqlParameter("@ApprovedGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, approvedGuid);
            sph.DefineSqlParameter("@ApprovedDate", SqlDbType.DateTime, ParameterDirection.Input, approvedDate);
            sph.DefineSqlParameter("@AllowComment", SqlDbType.Bit, ParameterDirection.Input, allowComment);
            sph.DefineSqlParameter("@IsHot", SqlDbType.Bit, ParameterDirection.Input, isHot);
            sph.DefineSqlParameter("@IsHome", SqlDbType.Bit, ParameterDirection.Input, isHome);
            sph.DefineSqlParameter("@Tag", SqlDbType.NVarChar, -1, ParameterDirection.Input, tag);
            sph.DefineSqlParameter("@FTS", SqlDbType.NVarChar, -1, ParameterDirection.Input, fts);
            sph.DefineSqlParameter("@IsPublished", SqlDbType.Bit, ParameterDirection.Input, isPublished);
            sph.DefineSqlParameter("@PublishedGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, publishedGuid);
            sph.DefineSqlParameter("@PublishedDate", SqlDbType.DateTime, ParameterDirection.Input, publishedDate);
            sph.DefineSqlParameter("@IncludeInFeed", SqlDbType.Bit, ParameterDirection.Input, includeInFeed);
            sph.DefineSqlParameter("@CommentByBoss", SqlDbType.NVarChar, -1, ParameterDirection.Input, commentByBoss);
            sph.DefineSqlParameter("@AudioUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, audioUrl);
            sph.DefineSqlParameter("@PollGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, pollGuid);
            sph.DefineSqlParameter("@AllowWCAG", SqlDbType.Bit, ParameterDirection.Input, allowWCAG);
            sph.DefineSqlParameter("@CompiledMeta", SqlDbType.NVarChar, -1, ParameterDirection.Input, compiledMeta);
            sph.DefineSqlParameter("@MetaCreator", SqlDbType.NVarChar, 255, ParameterDirection.Input, metaCreator);
            sph.DefineSqlParameter("@MetaIdentifier", SqlDbType.NVarChar, 255, ParameterDirection.Input, metaIdentifier);
            sph.DefineSqlParameter("@MetaPublisher", SqlDbType.NVarChar, 255, ParameterDirection.Input, metaPublisher);
            sph.DefineSqlParameter("@MetaDate", SqlDbType.DateTime, ParameterDirection.Input, metaDate);
            sph.DefineSqlParameter("@IsDelete", SqlDbType.Bit, ParameterDirection.Input, isDelete);
            sph.DefineSqlParameter("@ArticleReference", SqlDbType.NVarChar, 255, ParameterDirection.Input, articleReference);
            sph.DefineSqlParameter("@TitleFTS", SqlDbType.NVarChar, -1, ParameterDirection.Input, titleFTS);
            sph.DefineSqlParameter("@AuthorFTS", SqlDbType.NVarChar, -1, ParameterDirection.Input, authorFTS);
            sph.DefineSqlParameter("@SapoFTS", SqlDbType.NVarChar, -1, ParameterDirection.Input, sapoFTS);
            sph.DefineSqlParameter("@CreateDateArticle", SqlDbType.DateTime, ParameterDirection.Input, createDateArticle);
            sph.DefineSqlParameter("@ViTriHienThiNgayDang", SqlDbType.NVarChar, -1, ParameterDirection.Input, viTriHienThiNgayDang);
            sph.DefineSqlParameter("@IsHienThiTacGia", SqlDbType.Bit, ParameterDirection.Input, isHienThiTacGia);
            //sph.DefineSqlParameter("@IsHotNew", SqlDbType.Bit, ParameterDirection.Input, hotNew);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        public static bool UpdateCat(
            int itemID,
            int moduleID,
            int categoryID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Articles_UpdateCat", 3);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);
        }


        public static IDataReader GetTopArticleHot(int siteId, int top, string categories)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectTopArticleHot", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, top);
            sph.DefineSqlParameter("@Categories", SqlDbType.NVarChar, 250, ParameterDirection.Input, categories);
            return sph.ExecuteReader();
        }
        public static bool UpdateHitCount(int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Articles_Update_HitCount", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);
        }
        /// <summary>
        /// Deletes a row from the md_Articles table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Articles_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_Articles table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();
        }


        public static IDataReader GetArticleHot(int siteId, string categories, int Top)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectTopHotNew", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@Categories", SqlDbType.NVarChar, 255, ParameterDirection.Input, categories);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, Top);
            return sph.ExecuteReader();
        }

        public static int GetCountPublished(int siteId, int searchWith, string keyword, int category, DateTime? date, DateTime? searchDate)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectCountAllPublished", 6);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@SearchWith", SqlDbType.Int, ParameterDirection.Input, searchWith);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, 250, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@DatePublished", SqlDbType.DateTime, ParameterDirection.Input, date);
            sph.DefineSqlParameter("@Category", SqlDbType.Int, ParameterDirection.Input, category);
            sph.DefineSqlParameter("@SearchDate", SqlDbType.DateTime, ParameterDirection.Input, searchDate);
            return Convert.ToInt32(sph.ExecuteScalar());
        }

        public static IDataReader GetSearchPublished(int siteId, int searchWith, string keyword, int category, DateTime? date, DateTime? searchDate, int pageNumber, int pageSize, out int totalPages, out int totalArticle)
        {
            totalPages = 1;
            int totalRows
                = GetCountPublished(siteId, searchWith, keyword, category, date, searchDate);
            totalArticle = totalRows;
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
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectAllPublished", 8);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@SearchWith", SqlDbType.Int, ParameterDirection.Input, searchWith);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, 250, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@DatePublished", SqlDbType.DateTime, ParameterDirection.Input, date);
            sph.DefineSqlParameter("@Category", SqlDbType.Int, ParameterDirection.Input, category);
            sph.DefineSqlParameter("@SearchDate", SqlDbType.DateTime, ParameterDirection.Input, searchDate);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);

            return sph.ExecuteReader();
        }



        public static IDataReader GetByCategorySetting(
            int siteID,
            int moduleID,
            string categories)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectByCategorySetting", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@Categories", SqlDbType.NVarChar, 100, ParameterDirection.Input, categories);
            return sph.ExecuteReader();

        }


        public static IDataReader GetArticleTopHitCount(
        string categories, int Top, int siteId, bool isLayTinPortalKhac = false)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectTopHitCount", 4);
            sph.DefineSqlParameter("@Categories", SqlDbType.NVarChar, 150, ParameterDirection.Input, categories);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, Top);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@IsCongThanhVien", SqlDbType.Bit, ParameterDirection.Input, isLayTinPortalKhac);
            return sph.ExecuteReader();
        }


        public static IDataReader GetArticleTopNew(
        string categories, int Top, int siteId, bool isLayTinPortalKhac = false)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectTopNew", 4);
            sph.DefineSqlParameter("@Categories", SqlDbType.NVarChar, 150, ParameterDirection.Input, categories);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, Top);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@IsCongThanhVien", SqlDbType.Bit, ParameterDirection.Input, isLayTinPortalKhac);
            return sph.ExecuteReader();
        }



        public static IDataReader GetArticleTop(
           int CategoryID, int Top)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectTop", 2);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, CategoryID);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, Top);
            return sph.ExecuteReader();
        }


        public static IDataReader GetArticleHotRight(
           int siteID, int number, int readMost)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectHotRight", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@Number", SqlDbType.Int, ParameterDirection.Input, number);
            sph.DefineSqlParameter("@ReadMost", SqlDbType.Int, ParameterDirection.Input, readMost);
            return sph.ExecuteReader();

        }
        public static IDataReader GetArticleHotNew(int siteID, int number, int readMost)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectHotNew", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@Number", SqlDbType.Int, ParameterDirection.Input, number);
            sph.DefineSqlParameter("@ReadMost", SqlDbType.Int, ParameterDirection.Input, readMost);
            return sph.ExecuteReader();
        }


        public static IDataReader GetArticleTopNew(int siteID, int number)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectTopNewArticle", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@Number", SqlDbType.Int, ParameterDirection.Input, number);
            return sph.ExecuteReader();
        }

        public static IDataReader GetArticleIsHomeByCategory(int siteID, string categories, int number, int readMost)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectIsHomeByCategory", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@Categories", SqlDbType.NVarChar, 250, ParameterDirection.Input, categories);
            sph.DefineSqlParameter("@Number", SqlDbType.Int, ParameterDirection.Input, number);
            sph.DefineSqlParameter("@ReadMost", SqlDbType.Int, ParameterDirection.Input, readMost);
            return sph.ExecuteReader();
        }

        public static IDataReader GetArticleIsHomeHotByCategory(int siteID, string categories, int number, int readMost, bool isLayTinPortalKhac = false)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectIsHomeByCategory", 5);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@Categories", SqlDbType.NVarChar, 250, ParameterDirection.Input, categories);
            sph.DefineSqlParameter("@Number", SqlDbType.Int, ParameterDirection.Input, number);
            sph.DefineSqlParameter("@ReadMost", SqlDbType.Int, ParameterDirection.Input, readMost);
            sph.DefineSqlParameter("@IsCongThanhVien", SqlDbType.Bit, ParameterDirection.Input, isLayTinPortalKhac);
            return sph.ExecuteReader();
        }

        public static IDataReader GetArticleHotByCategory(int siteID, string categories, int number, int readMost, bool isHotCat = true)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectHotByCategory", 5);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@Categories", SqlDbType.NVarChar, 250, ParameterDirection.Input, categories);
            sph.DefineSqlParameter("@Number", SqlDbType.Int, ParameterDirection.Input, number);
            sph.DefineSqlParameter("@ReadMost", SqlDbType.Int, ParameterDirection.Input, readMost);
            sph.DefineSqlParameter("@IsHotCat", SqlDbType.Bit, ParameterDirection.Input, isHotCat);
            return sph.ExecuteReader();
        }

        public static IDataReader GetEventHot(
            int siteID, int number)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectEvenHot", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@Number", SqlDbType.Int, ParameterDirection.Input, number);
            return sph.ExecuteReader();

        }
        public static IDataReader GetArticleByID(
           int itemId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectByID", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemId);
            return sph.ExecuteReader();
        }
        public static IDataReader GetByModule(int siteId, int moduleId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectBySiteModule", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            return sph.ExecuteReader();
        }

        public static IDataReader GetListPhongBan()
        {
            return SqlHelper.ExecuteReader(
            ConnectionString.GetReadConnectionString(),
            CommandType.StoredProcedure,
            "md_Articles_SelectAllPhongBan",
            null);
        }


        public static IDataReader GetArticleByCategory(int categoryID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectByCategory", 1);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            return sph.ExecuteReader();
        }

        public static IDataReader GetOneArticleByEvent(
         int eventID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectOneByEvent", 1);
            sph.DefineSqlParameter("@EventID", SqlDbType.Int, ParameterDirection.Input, eventID);
            return sph.ExecuteReader();

        }
        public static IDataReader SelectByReference(
 string reference)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Article_SelectByReference", 1);
            sph.DefineSqlParameter("@ActiveReference", SqlDbType.NVarChar, 250, ParameterDirection.Input, reference);
            return sph.ExecuteReader();

        }
        public static IDataReader GetArticleTopOrther(
           int CategoryID, int ItemId, int Top, bool Hot)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectTopOrther", 4);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, CategoryID);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, ItemId);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, Top);
            sph.DefineSqlParameter("@Hot", SqlDbType.Bit, ParameterDirection.Input, Hot);
            return sph.ExecuteReader();

        }
        public static IDataReader GetArticleByEvent(
   int eventID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectAllByEvent", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, eventID);
            return sph.ExecuteReader();

        }
        public static IDataReader GetArticleTopHot(int Top, int siteID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectTop_Hot", 2);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, Top);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            return sph.ExecuteReader();
        }


        public static IDataReader GetBySite(int siteID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectBySite", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            return sph.ExecuteReader();
        }


        public static IDataReader GetArticleTopHotOrther(int ItemId, int Top, int siteID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectTop_HotOrther", 3);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, ItemId);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, Top);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            return sph.ExecuteReader();
        }

        /// <summary>
        /// Lấy số lượng bài viết lọc theo năm hoặc tháng, categories
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static IDataReader GetTotalByYear(int year, string yearMonth, string categories, int siteId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectTotalByYear", 4);
            sph.DefineSqlParameter("@Year", SqlDbType.Int, ParameterDirection.Input, year);
            sph.DefineSqlParameter("@YearMonth", SqlDbType.VarChar, 10, ParameterDirection.Input, yearMonth);
            sph.DefineSqlParameter("@Categories", SqlDbType.VarChar, 200, ParameterDirection.Input, categories);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            return sph.ExecuteReader();
        }


        public static IDataReader GetStaticForTab1(int year, int month, string categories, string siteId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectStaticForTab1", 4);
            sph.DefineSqlParameter("@Year", SqlDbType.Int, ParameterDirection.Input, year);
            sph.DefineSqlParameter("@Month", SqlDbType.Int, ParameterDirection.Input, month);
            sph.DefineSqlParameter("@Categories", SqlDbType.NVarChar, 250, ParameterDirection.Input, categories);
            sph.DefineSqlParameter("@SiteID", SqlDbType.NVarChar, 250, ParameterDirection.Input, siteId);
            return sph.ExecuteReader();
        }




        public static IDataReader StatisticTotalByCategory(int SiteId, DateTime? startDate, DateTime? endDate, string userGuid = "", string categories = "")
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_StatisticDatetime", 5);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, SiteId);
            sph.DefineSqlParameter("@StartDate", SqlDbType.DateTime, ParameterDirection.Input, startDate);
            sph.DefineSqlParameter("@EndDate", SqlDbType.DateTime, ParameterDirection.Input, endDate);
            sph.DefineSqlParameter("@UserGuid", SqlDbType.VarChar, 200, ParameterDirection.Input, userGuid);
            sph.DefineSqlParameter("@Categories", SqlDbType.VarChar, 200, ParameterDirection.Input, categories);
            return sph.ExecuteReader();
        }


        public static IDataReader StatisticTotalAllSite(string SiteId, DateTime? startDate, DateTime? endDate)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_StatisticAllSite", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.NVarChar, 550, ParameterDirection.Input, SiteId);
            sph.DefineSqlParameter("@StartDate", SqlDbType.DateTime, ParameterDirection.Input, startDate);
            sph.DefineSqlParameter("@EndDate", SqlDbType.DateTime, ParameterDirection.Input, endDate);
            return sph.ExecuteReader();
        }




        public static IDataReader GetArticleTopHotByCat(int catId, int Top)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectTopHotByCat", 2);
            sph.DefineSqlParameter("@CatID", SqlDbType.Int, ParameterDirection.Input, catId);
            sph.DefineSqlParameter("@Number", SqlDbType.Int, ParameterDirection.Input, Top);
            return sph.ExecuteReader();

        }

        public static IDataReader GetArticleReference(int siteId, int itemId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectArticleReference", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemId);
            return sph.ExecuteReader();

        }
        /// <summary>
        /// Gets a count of rows in the md_Articles table.
        /// </summary>
        public static int GetCount(
            int siteId,
            int moduleId,
            int categoryID,
            int isApprove,
            int isPublish,
            string keyword,
            Guid userGuid,
            string createDateArticle
            )
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_GetCount", 8);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@IsApprove", SqlDbType.Int, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@IsPublish", SqlDbType.Int, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@UserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, userGuid);
            sph.DefineSqlParameter("@CreateDateArticle", SqlDbType.NVarChar, 250, ParameterDirection.Input, createDateArticle);
            //sph.DefineSqlParameter("@IsAdmin", SqlDbType.Bit, ParameterDirection.Input, isAdmin);
            return Convert.ToInt32(sph.ExecuteScalar());
        }
        public static int GetAllCount(
            int siteId,
            int categoryID,
            Guid authorID,
            int isApprove,
            int isPublish,
            bool? isHot,
            bool? isHome,
            DateTime? startDate,
            DateTime? endDate,
            string keyword
            )
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_GetAllCount", 10);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@UserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, authorID);
            sph.DefineSqlParameter("@IsApprove", SqlDbType.Int, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@IsPublish", SqlDbType.Int, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@IsHot", SqlDbType.Bit, ParameterDirection.Input, isHot);
            sph.DefineSqlParameter("@IsHome", SqlDbType.Bit, ParameterDirection.Input, isHome);
            sph.DefineSqlParameter("@StartDate", SqlDbType.DateTime, ParameterDirection.Input, startDate);
            sph.DefineSqlParameter("@EndDate", SqlDbType.DateTime, ParameterDirection.Input, endDate);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, ParameterDirection.Input, keyword);
            //sph.DefineSqlParameter("@IsAdmin", SqlDbType.Bit, ParameterDirection.Input, isAdmin);
            return Convert.ToInt32(sph.ExecuteScalar());
        }

        public static int GetAllOrtherCount(
          int siteId,
          int itemId,
          int categoryId,
          DateTime? startDate,
          string keyword
          )
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_GetAllOrtherCount", 5);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemId);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryId);
            sph.DefineSqlParameter("@StartDate", SqlDbType.DateTime, ParameterDirection.Input, startDate);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, ParameterDirection.Input, keyword);
            return Convert.ToInt32(sph.ExecuteScalar());
        }

        public static int GetCountForEndUser(
            int siteId,
            int moduleId,
            string categories,
            string keyword
            )
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_GetCount_ForEndUser", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            sph.DefineSqlParameter("@Categories", SqlDbType.NVarChar, 150, ParameterDirection.Input, categories);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, ParameterDirection.Input, keyword);
            return Convert.ToInt32(sph.ExecuteScalar());
        }


        public static int GetCountForEndUserCongThanhVien(
          int siteId,
          int moduleId,
          string categories,
          string keyword
          )
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_GetCount_CongThanhVien", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            sph.DefineSqlParameter("@Categories", SqlDbType.NVarChar, 150, ParameterDirection.Input, categories);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, ParameterDirection.Input, keyword);
            return Convert.ToInt32(sph.ExecuteScalar());
        }


        public static int GetCountForCategory(
    int siteId,
    int categoryId,
    string keyword
    )
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_GetCount_ForCategory", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryId);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, ParameterDirection.Input, keyword);
            return Convert.ToInt32(sph.ExecuteScalar());
        }


        public static int GetCountByCategory(
    string categories
    )
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_GetCount_ByCategory", 1);
            sph.DefineSqlParameter("@Categories", SqlDbType.NVarChar, 150, ParameterDirection.Input, categories);
            return Convert.ToInt32(sph.ExecuteScalar());
        }


        public static int GetCountByEvent(
            int eventID
    )
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_GetCount_ByEvent", 1);
            sph.DefineSqlParameter("@EventID", SqlDbType.Int, ParameterDirection.Input, eventID);
            return Convert.ToInt32(sph.ExecuteScalar());
        }

        public static int GetCountByTag(
            int siteId,
            int tagId
            )
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_GetCount_ByTag", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@TagId", SqlDbType.Int, ParameterDirection.Input, tagId);
            return Convert.ToInt32(sph.ExecuteScalar());
        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_Articles table.
        /// </summary>
        public static IDataReader GetAll()
        {
            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_Articles_SelectAll",
                null);
        }
        public static IDataReader GetAllByEvent(int siteID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectAllByEvent", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            return sph.ExecuteReader();
        }
        public static IDataReader GetArticlesForRSS(
            int moduleId,
            DateTime beginDate,
            DateTime currentTime)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectForRSS", 3);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            sph.DefineSqlParameter("@BeginDate", SqlDbType.DateTime, ParameterDirection.Input, beginDate);
            sph.DefineSqlParameter("@CurrentTime", SqlDbType.DateTime, ParameterDirection.Input, currentTime);
            return sph.ExecuteReader();
        }

        /// <summary>
        /// Gets a page of data from the md_Articles table.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static IDataReader GetPage(
            int siteId,
            int moduleId,
            int pageNumber,
            int pageSize,
            int categoryID,
            int isApprove,
            int isPublish,
            string keyword,
            Guid userGuid,
            string createDateArticle,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCount(siteId, moduleId, categoryID, isApprove, isPublish, keyword, userGuid, createDateArticle);

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
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectPage", 10);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            //sph.DefineSqlParameter("@IsApprove", SqlDbType.Bit, ParameterDirection.Input, isApprove);
            //sph.DefineSqlParameter("@IsPublish", SqlDbType.Bit, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@IsApprove", SqlDbType.Int, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@IsPublish", SqlDbType.Int, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@UserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, userGuid);
            sph.DefineSqlParameter("@CreateDateArticle", SqlDbType.NVarChar, 250, ParameterDirection.Input, createDateArticle);
            //sph.DefineSqlParameter("@IsAdmin", SqlDbType.Bit, ParameterDirection.Input, isAdmin);
            return sph.ExecuteReader();

        }

        public static IDataReader GetAllOrtherPage(
           int siteId,
           int itemId,
           int categoryId,
           DateTime? startDate,
           string keyword,
           int pageNumber,
           int pageSize,
           out int totalPages,
           out int totalCount)
        {
            totalPages = 1;
            int totalRows
                = GetAllOrtherCount(siteId, itemId, categoryId, startDate, keyword);
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
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectAllOrtherPage", 7);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemId);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryId);
            sph.DefineSqlParameter("@StartDate", SqlDbType.DateTime, ParameterDirection.Input, startDate);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();
        }


        public static IDataReader GetAllPage(
            int siteId,
            int pageNumber,
            int pageSize,
            int categoryID,
            Guid authorID,
            int isApprove,
            int isPublish,
            bool? isHot,
            bool? isHome,
            DateTime? startDate,
            DateTime? endDate,
            string keyword,
            out int totalPages,
            out int toalRow)
        {
            totalPages = 1;
            int totalRows
                = GetAllCount(siteId, categoryID, authorID, isApprove, isPublish, isHot, isHome, startDate, endDate, keyword);
            toalRow = totalRows;
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
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectAllPage", 12);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@UserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, authorID);
            //sph.DefineSqlParameter("@IsApprove", SqlDbType.Bit, ParameterDirection.Input, isApprove);
            //sph.DefineSqlParameter("@IsPublish", SqlDbType.Bit, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@IsApprove", SqlDbType.Int, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@IsPublish", SqlDbType.Int, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@IsHot", SqlDbType.Bit, ParameterDirection.Input, isHot);
            sph.DefineSqlParameter("@IsHome", SqlDbType.Bit, ParameterDirection.Input, isHome);
            sph.DefineSqlParameter("@StartDate", SqlDbType.DateTime, ParameterDirection.Input, startDate);
            sph.DefineSqlParameter("@EndDate", SqlDbType.DateTime, ParameterDirection.Input, endDate);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, ParameterDirection.Input, keyword);
            //sph.DefineSqlParameter("@IsAdmin", SqlDbType.Bit, ParameterDirection.Input, isAdmin);

            return sph.ExecuteReader();
        }
        public static int GetAllArticleCount(
            int siteId,
            int categoryID,
            int isApprove,
            int isPublish,
            bool? isHot,
            bool? isHome,
            DateTime? startDate,
            DateTime? endDate,
            string articleCategory,
            string keyword,
            Guid userGuid,
             string createDateArticle
            )
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_GetAllArticleCount", 12);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@IsApprove", SqlDbType.Int, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@IsPublish", SqlDbType.Int, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@IsHot", SqlDbType.Bit, ParameterDirection.Input, isHot);
            sph.DefineSqlParameter("@IsHome", SqlDbType.Bit, ParameterDirection.Input, isHome);
            sph.DefineSqlParameter("@StartDate", SqlDbType.DateTime, ParameterDirection.Input, startDate);
            sph.DefineSqlParameter("@EndDate", SqlDbType.DateTime, ParameterDirection.Input, endDate);
            sph.DefineSqlParameter("@ArticleCategories", SqlDbType.NVarChar, 250, ParameterDirection.Input, articleCategory);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@UserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, userGuid);
            sph.DefineSqlParameter("@CreateDateArticle", SqlDbType.NVarChar, 250, ParameterDirection.Input, createDateArticle);
            //sph.DefineSqlParameter("@IsAdmin", SqlDbType.Bit, ParameterDirection.Input, isAdmin);
            return Convert.ToInt32(sph.ExecuteScalar());
        }


        public static IDataReader GetAllArticlePage(
        int siteId,
        int pageNumber,
        int pageSize,
        int categoryID,
        int isApprove,
        int isPublish,
        bool? isHot,
        bool? isHome,
        DateTime? startDate,
        DateTime? endDate,
        string articleCategory,
        string keyword,
        Guid userGuid,
        string createDateArticle,
        out int totalPages,
        out int totalRow
        )
        {
            if (!string.IsNullOrEmpty(createDateArticle))
            {
                createDateArticle.Trim();
            }
            totalPages = 1;
            int totalRows
                = GetAllArticleCount(siteId, categoryID, isApprove, isPublish, isHot, isHome, startDate, endDate, articleCategory, keyword, userGuid, createDateArticle);
            totalRow = totalRows;
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
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectAllArticlePage", 14);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@IsApprove", SqlDbType.Int, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@IsPublish", SqlDbType.Int, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@IsHot", SqlDbType.Bit, ParameterDirection.Input, isHot);
            sph.DefineSqlParameter("@IsHome", SqlDbType.Bit, ParameterDirection.Input, isHome);
            sph.DefineSqlParameter("@StartDate", SqlDbType.DateTime, ParameterDirection.Input, startDate);
            sph.DefineSqlParameter("@EndDate", SqlDbType.DateTime, ParameterDirection.Input, endDate);
            sph.DefineSqlParameter("@ArticleCategories", SqlDbType.NVarChar, 250, ParameterDirection.Input, articleCategory);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@UserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, userGuid);
            sph.DefineSqlParameter("@CreateDateArticle", SqlDbType.NVarChar, 250, ParameterDirection.Input, createDateArticle);

            return sph.ExecuteReader();
        }


        public static IDataReader GetAllArticleCTVPage(
    int siteId,
    int pageNumber,
    int pageSize,
    int categoryID,
    int isApprove,
    int isPublish,
    bool? isHot,
    bool? isHome,
    DateTime? startDate,
    DateTime? endDate,
    string articleCategory,
    string keyword,
    Guid userGuid,
    string createDateArticle,
    out int totalPages,
    out int totalRow
    )
        {
            if (!string.IsNullOrEmpty(createDateArticle))
            {
                createDateArticle.Trim();
            }
            totalPages = 1;
            int totalRows
                = GetAllArticleCountCTV(siteId, categoryID, isApprove, isPublish, isHot, isHome, startDate, endDate, articleCategory, keyword, userGuid, createDateArticle);
            totalRow = totalRows;
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
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectAllArticleCTVPage", 14);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@IsApprove", SqlDbType.Int, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@IsPublish", SqlDbType.Int, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@IsHot", SqlDbType.Bit, ParameterDirection.Input, isHot);
            sph.DefineSqlParameter("@IsHome", SqlDbType.Bit, ParameterDirection.Input, isHome);
            sph.DefineSqlParameter("@StartDate", SqlDbType.DateTime, ParameterDirection.Input, startDate);
            sph.DefineSqlParameter("@EndDate", SqlDbType.DateTime, ParameterDirection.Input, endDate);
            sph.DefineSqlParameter("@ArticleCategories", SqlDbType.NVarChar, 250, ParameterDirection.Input, articleCategory);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@UserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, userGuid);
            sph.DefineSqlParameter("@CreateDateArticle", SqlDbType.NVarChar, 250, ParameterDirection.Input, createDateArticle);
            //sph.DefineSqlParameter("@IsAdmin", SqlDbType.Bit, ParameterDirection.Input, isAdmin);

            return sph.ExecuteReader();
        }

        public static int GetAllArticleCountCTV(
            int siteId,
            int categoryID,
            int isApprove,
            int isPublish,
            bool? isHot,
            bool? isHome,
            DateTime? startDate,
            DateTime? endDate,
            string articleCategory,
            string keyword,
            Guid userGuid,
             string createDateArticle
            )
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_GetAllArticleCTVCount", 12);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@IsApprove", SqlDbType.Int, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@IsPublish", SqlDbType.Int, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@IsHot", SqlDbType.Bit, ParameterDirection.Input, isHot);
            sph.DefineSqlParameter("@IsHome", SqlDbType.Bit, ParameterDirection.Input, isHome);
            sph.DefineSqlParameter("@StartDate", SqlDbType.DateTime, ParameterDirection.Input, startDate);
            sph.DefineSqlParameter("@EndDate", SqlDbType.DateTime, ParameterDirection.Input, endDate);
            sph.DefineSqlParameter("@ArticleCategories", SqlDbType.NVarChar, 250, ParameterDirection.Input, articleCategory);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@UserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, userGuid);
            sph.DefineSqlParameter("@CreateDateArticle", SqlDbType.NVarChar, 250, ParameterDirection.Input, createDateArticle);
            //sph.DefineSqlParameter("@IsAdmin", SqlDbType.Bit, ParameterDirection.Input, isAdmin);
            return Convert.ToInt32(sph.ExecuteScalar());
        }







        public static IDataReader GetPageForCategory(
        int siteId,
        int categoryId,
        int pageNumber,
        int pageSize,
        string keyword,
        out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCountForCategory(siteId, categoryId, keyword);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectPage_ForCategory", 5);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryId);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, ParameterDirection.Input, keyword);
            return sph.ExecuteReader();
        }




        public static IDataReader GetPageForEndUser(
            int siteId,
            int moduleId,
            int pageNumber,
            int pageSize,
            string categories,
            string keyword,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCountForEndUser(siteId, moduleId, categories, keyword);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectPage_ForEndUser", 6);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@Categories", SqlDbType.NVarChar, 150, ParameterDirection.Input, categories);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, ParameterDirection.Input, keyword);
            return sph.ExecuteReader();
        }


        public static IDataReader GetPageForEndUserCongThanhVien(
           int siteId,
           int moduleId,
           int pageNumber,
           int pageSize,
           string categories,
           string keyword,
           out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCountForEndUserCongThanhVien(siteId, moduleId, categories, keyword);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectPage_CongThanhVien", 6);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@Categories", SqlDbType.NVarChar, 150, ParameterDirection.Input, categories);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, ParameterDirection.Input, keyword);
            return sph.ExecuteReader();
        }




        public static IDataReader GetTopByCategory(
        int top,
        string categories)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectTopByCategory", 2);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, top);
            sph.DefineSqlParameter("@Categories", SqlDbType.NVarChar, 250, ParameterDirection.Input, categories);
            return sph.ExecuteReader();
        }


        public static IDataReader GetPageByCategory(
        int pageNumber,
        int pageSize,
        string categories,
        out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCountByCategory(categories);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectPage_ByCategory", 3);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@Categories", SqlDbType.NVarChar, 150, ParameterDirection.Input, categories);
            return sph.ExecuteReader();
        }




        public static IDataReader GetPageByEvent(
            int pageNumber,
            int pageSize,
            int eventID,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCountByEvent(eventID);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectPage_ByEvent", 3);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@EventID", SqlDbType.Int, ParameterDirection.Input, eventID);
            return sph.ExecuteReader();

        }

        public static IDataReader GetPageByTag(
            int siteId,
            int tagId,
            int pageNumber,
            int pageSize,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCountByTag(siteId, tagId);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectPage_ByTag", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@TagID", SqlDbType.Int, ParameterDirection.Input, tagId);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

        public static IDataReader GetArticlesByPage(int siteId, int pageId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Article_SelectByPage", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@PageID", SqlDbType.Int, ParameterDirection.Input, pageId);
            return sph.ExecuteReader();
        }

        public static bool DeleteByModule(int moduleId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Article_DeleteByModule", 1);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > -1);

        }

        public static IDataReader GetPageOther(
            string[] listModuleID,
            int pageSize,
            int currentPage)
        {

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_Others_Hot_SelectPage", 3);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, currentPage);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@ListModuleId", SqlDbType.NVarChar, ParameterDirection.Input, listModuleID);
            return sph.ExecuteReader();

        }

        public static IDataReader GetPageOtherMostView(
           string[] listModuleID,
           int pageSize,
           int currentPage)
        {

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_Others_MostView_SelectPage", 3);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, currentPage);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@ListModuleId", SqlDbType.NVarChar, ParameterDirection.Input, listModuleID);
            return sph.ExecuteReader();

        }

        public static IDataReader GetOthersPageModule(
          string[] listModuleID,
          int pageSize,
          int currentPage)
        {

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_Others_SelectPage", 3);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, currentPage);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@ListModuleId", SqlDbType.NVarChar, ParameterDirection.Input, listModuleID);
            return sph.ExecuteReader();

        }

        public static IDataReader GetOthersPageByModuleId(
          int moduleID,
          int pageSize,
          int currentPage)
        {

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_Others_SelectPage_ByModuleID", 3);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, currentPage);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            return sph.ExecuteReader();

        }

        public static IDataReader GetOthersPageCategory(
          int categoryID,
          int pageSize,
          int currentPage)
        {

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_Others_SelectPage_ByCategoryID", 3);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, currentPage);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            return sph.ExecuteReader();

        }

        public static int GetPageHotModuleCount(string listModuleID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_Others_Hot_SelectPage_Count", 1);
            sph.DefineSqlParameter("@ListModuleId", SqlDbType.NVarChar, ParameterDirection.Input, listModuleID);
            return Convert.ToInt32(sph.ExecuteScalar());
        }
        public static IDataReader GetPageHotModule(
            string listModuleID,
            int pageSize,
            int pageNumber,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetPageHotModuleCount(listModuleID);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_Others_Hot_SelectPage", 3);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@ListModuleId", SqlDbType.NVarChar, ParameterDirection.Input, listModuleID);
            return sph.ExecuteReader();

        }

        public static int GetPageMostViewModuleCount(string listModuleID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_Others_MostView_SelectPage_Count", 1);
            sph.DefineSqlParameter("@ListModuleId", SqlDbType.NVarChar, ParameterDirection.Input, listModuleID);
            return Convert.ToInt32(sph.ExecuteScalar());
        }
        public static IDataReader GetPageMostViewModule(
            string listModuleID,
            int pageSize,
            int pageNumber,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetPageMostViewModuleCount(listModuleID);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_Others_MostView_SelectPage", 3);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@ListModuleId", SqlDbType.NVarChar, ParameterDirection.Input, listModuleID);
            return sph.ExecuteReader();

        }


        public static int GetPageModuleCount(string listModuleID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_Others_SelectPage_Count", 1);
            sph.DefineSqlParameter("@ListModuleId", SqlDbType.NVarChar, ParameterDirection.Input, listModuleID);
            return Convert.ToInt32(sph.ExecuteScalar());
        }
        public static IDataReader GetPageModule(
            string listModuleID,
            int pageSize,
            int pageNumber,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetPageModuleCount(listModuleID);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_Others_SelectPage", 3);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@ListModuleId", SqlDbType.NVarChar, ParameterDirection.Input, listModuleID);
            return sph.ExecuteReader();

        }

        public static int GetPageByModuleIdCount(int moduleID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_Others_SelectPage_ByModuleID_Count", 1);
            sph.DefineSqlParameter("@ModuleId", SqlDbType.Int, ParameterDirection.Input, moduleID);
            return Convert.ToInt32(sph.ExecuteScalar());
        }
        public static IDataReader GetPageModule(
            int moduleID,
            int pageSize,
            int pageNumber,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetPageByModuleIdCount(moduleID);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_Others_SelectPage_ByModuleID", 3);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            return sph.ExecuteReader();

        }

        public static int GetPageCategoryCount(int categoryID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_Others_SelectPage_ByCategoryID_Count", 1);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            return Convert.ToInt32(sph.ExecuteScalar());
        }

        public static IDataReader GetPageCategory(
            int categoryID,
            int pageSize,
            int pageNumber,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetPageCategoryCount(categoryID);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_Others_SelectPage_ByCategoryID", 3);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            return sph.ExecuteReader();

        }
        public static IDataReader GetOrtherByCategory(int categoryID, int itemID, int top)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_TopOthers_ByCat", 3);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, top);
            return sph.ExecuteReader();
        }


        public static IDataReader GetTopArticleCategoryHot(int siteId, int top, string categories)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_TopArticleCategoryHot", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, top);
            sph.DefineSqlParameter("@Categories", SqlDbType.NVarChar, 100, ParameterDirection.Input, categories);
            return sph.ExecuteReader();
        }

    }

}



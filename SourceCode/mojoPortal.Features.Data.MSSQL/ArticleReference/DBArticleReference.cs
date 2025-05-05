// Author:					HiNet
// Created:					2015-8-12
// Last Modified:			2015-8-12
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

    public static class DBArticleReference
    {


        /// <summary>
        /// Inserts a row in the md_ArticleReference table. Returns new integer id.
        /// </summary>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="categoryID"> categoryID </param>
        /// <param name="rootArticleID"> rootArticleID </param>
        /// <param name="title"> title </param>
        /// <param name="sumary"> sumary </param>
        /// <param name="description"> description </param>
        /// <param name="startDate"> startDate </param>
        /// <param name="endDate"> endDate </param>
        /// <param name="commentCount"> commentCount </param>
        /// <param name="hitCount"> hitCount </param>
        /// <param name="articleGuid"> articleGuid </param>
        /// <param name="moduleGuid"> moduleGuid </param>
        /// <param name="location"> location </param>
        /// <param name="userGuid"> userGuid </param>
        /// <param name="createdByUser"> createdByUser </param>
        /// <param name="createdDate"> createdDate </param>
        /// <param name="lastModUserGuid"> lastModUserGuid </param>
        /// <param name="lastModUtc"> lastModUtc </param>
        /// <param name="itemUrl"> itemUrl </param>
        /// <param name="metaTitle"> metaTitle </param>
        /// <param name="langGuid"> langGuid </param>
        /// <param name="metaKeywords"> metaKeywords </param>
        /// <param name="metaDescription"> metaDescription </param>
        /// <param name="isApproved"> isApproved </param>
        /// <param name="approvedGuid"> approvedGuid </param>
        /// <param name="approvedDate"> approvedDate </param>
        /// <param name="allowComment"> allowComment </param>
        /// <param name="isHot"> isHot </param>
        /// <param name="isHome"> isHome </param>
        /// <param name="tag"> tag </param>
        /// <param name="fTS"> fTS </param>
        /// <returns>int</returns>
        public static int Create(
            int moduleID,
            int siteID,
            int categoryID,
            int rootArticleID,
            string title,
            string summary,
            string description,
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
            int langId,
            string metaKeywords,
            string metaDescription,
            bool isApproved,
            Guid approvedGuid,
            DateTime approvedDate,
            bool allowComment,
            bool isHot,
            bool isHome,
            string tag,
            string fTS,
            string imageUrl)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleReference_Insert", 33);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@RootArticleID", SqlDbType.Int, ParameterDirection.Input, rootArticleID);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 255, ParameterDirection.Input, title);
            sph.DefineSqlParameter("@Summary", SqlDbType.NVarChar, 1000, ParameterDirection.Input, summary);
            sph.DefineSqlParameter("@Description", SqlDbType.NText, ParameterDirection.Input, description);
            sph.DefineSqlParameter("@StartDate", SqlDbType.DateTime, ParameterDirection.Input, startDate);
            sph.DefineSqlParameter("@EndDate", SqlDbType.DateTime, ParameterDirection.Input, endDate);
            sph.DefineSqlParameter("@CommentCount", SqlDbType.Int, ParameterDirection.Input, commentCount);
            sph.DefineSqlParameter("@HitCount", SqlDbType.Int, ParameterDirection.Input, hitCount);
            sph.DefineSqlParameter("@ArticleGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, articleGuid);
            sph.DefineSqlParameter("@ModuleGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, moduleGuid);
            sph.DefineSqlParameter("@Location", SqlDbType.NVarChar, 255, ParameterDirection.Input, location);
            sph.DefineSqlParameter("@UserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, userGuid);
            sph.DefineSqlParameter("@CreatedByUser", SqlDbType.NVarChar, 100, ParameterDirection.Input, createdByUser);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@LastModUserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, lastModUserGuid);
            sph.DefineSqlParameter("@LastModUtc", SqlDbType.DateTime, ParameterDirection.Input, lastModUtc);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, itemUrl);
            sph.DefineSqlParameter("@MetaTitle", SqlDbType.NVarChar, 255, ParameterDirection.Input, metaTitle);
            sph.DefineSqlParameter("@LangID", SqlDbType.Int, ParameterDirection.Input, langId);
            sph.DefineSqlParameter("@MetaKeywords", SqlDbType.NVarChar, 255, ParameterDirection.Input, metaKeywords);
            sph.DefineSqlParameter("@MetaDescription", SqlDbType.NVarChar, 255, ParameterDirection.Input, metaDescription);
            sph.DefineSqlParameter("@IsApproved", SqlDbType.Bit, ParameterDirection.Input, isApproved);
            sph.DefineSqlParameter("@ApprovedGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, approvedGuid);
            sph.DefineSqlParameter("@ApprovedDate", SqlDbType.DateTime, ParameterDirection.Input, approvedDate);
            sph.DefineSqlParameter("@AllowComment", SqlDbType.Bit, ParameterDirection.Input, allowComment);
            sph.DefineSqlParameter("@IsHot", SqlDbType.Bit, ParameterDirection.Input, isHot);
            sph.DefineSqlParameter("@IsHome", SqlDbType.Bit, ParameterDirection.Input, isHome);
            sph.DefineSqlParameter("@Tag", SqlDbType.NText, ParameterDirection.Input, tag);
            sph.DefineSqlParameter("@FTS", SqlDbType.NText, ParameterDirection.Input, fTS);
            sph.DefineSqlParameter("@ImageUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, imageUrl);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_ArticleReference table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="categoryID"> categoryID </param>
        /// <param name="rootArticleID"> rootArticleID </param>
        /// <param name="title"> title </param>
        /// <param name="sumary"> sumary </param>
        /// <param name="description"> description </param>
        /// <param name="startDate"> startDate </param>
        /// <param name="endDate"> endDate </param>
        /// <param name="commentCount"> commentCount </param>
        /// <param name="hitCount"> hitCount </param>
        /// <param name="articleGuid"> articleGuid </param>
        /// <param name="moduleGuid"> moduleGuid </param>
        /// <param name="location"> location </param>
        /// <param name="userGuid"> userGuid </param>
        /// <param name="createdByUser"> createdByUser </param>
        /// <param name="createdDate"> createdDate </param>
        /// <param name="lastModUserGuid"> lastModUserGuid </param>
        /// <param name="lastModUtc"> lastModUtc </param>
        /// <param name="itemUrl"> itemUrl </param>
        /// <param name="metaTitle"> metaTitle </param>
        /// <param name="langGuid"> langGuid </param>
        /// <param name="metaKeywords"> metaKeywords </param>
        /// <param name="metaDescription"> metaDescription </param>
        /// <param name="isApproved"> isApproved </param>
        /// <param name="approvedGuid"> approvedGuid </param>
        /// <param name="approvedDate"> approvedDate </param>
        /// <param name="allowComment"> allowComment </param>
        /// <param name="isHot"> isHot </param>
        /// <param name="isHome"> isHome </param>
        /// <param name="tag"> tag </param>
        /// <param name="fTS"> fTS </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int moduleID,
            int siteID,
            int categoryID,
            int rootArticleID,
            string title,
            string summary,
            string description,
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
            int langId,
            string metaKeywords,
            string metaDescription,
            bool isApproved,
            Guid approvedGuid,
            DateTime approvedDate,
            bool allowComment,
            bool isHot,
            bool isHome,
            string tag,
            string fTS,
            string imageUrl)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleReference_Update", 34);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@RootArticleID", SqlDbType.Int, ParameterDirection.Input, rootArticleID);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 255, ParameterDirection.Input, title);
            sph.DefineSqlParameter("@Summary", SqlDbType.NVarChar, 1000, ParameterDirection.Input, summary);
            sph.DefineSqlParameter("@Description", SqlDbType.NText, ParameterDirection.Input, description);
            sph.DefineSqlParameter("@StartDate", SqlDbType.DateTime, ParameterDirection.Input, startDate);
            sph.DefineSqlParameter("@EndDate", SqlDbType.DateTime, ParameterDirection.Input, endDate);
            sph.DefineSqlParameter("@CommentCount", SqlDbType.Int, ParameterDirection.Input, commentCount);
            sph.DefineSqlParameter("@HitCount", SqlDbType.Int, ParameterDirection.Input, hitCount);
            sph.DefineSqlParameter("@ArticleGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, articleGuid);
            sph.DefineSqlParameter("@ModuleGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, moduleGuid);
            sph.DefineSqlParameter("@Location", SqlDbType.NText, ParameterDirection.Input, location);
            sph.DefineSqlParameter("@UserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, userGuid);
            sph.DefineSqlParameter("@CreatedByUser", SqlDbType.NVarChar, 100, ParameterDirection.Input, createdByUser);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@LastModUserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, lastModUserGuid);
            sph.DefineSqlParameter("@LastModUtc", SqlDbType.DateTime, ParameterDirection.Input, lastModUtc);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, itemUrl);
            sph.DefineSqlParameter("@MetaTitle", SqlDbType.NVarChar, 255, ParameterDirection.Input, metaTitle);
            sph.DefineSqlParameter("@LangID", SqlDbType.Int, ParameterDirection.Input, langId);
            sph.DefineSqlParameter("@MetaKeywords", SqlDbType.NVarChar, 255, ParameterDirection.Input, metaKeywords);
            sph.DefineSqlParameter("@MetaDescription", SqlDbType.NVarChar, 255, ParameterDirection.Input, metaDescription);
            sph.DefineSqlParameter("@IsApproved", SqlDbType.Bit, ParameterDirection.Input, isApproved);
            sph.DefineSqlParameter("@ApprovedGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, approvedGuid);
            sph.DefineSqlParameter("@ApprovedDate", SqlDbType.DateTime, ParameterDirection.Input, approvedDate);
            sph.DefineSqlParameter("@AllowComment", SqlDbType.Bit, ParameterDirection.Input, allowComment);
            sph.DefineSqlParameter("@IsHot", SqlDbType.Bit, ParameterDirection.Input, isHot);
            sph.DefineSqlParameter("@IsHome", SqlDbType.Bit, ParameterDirection.Input, isHome);
            sph.DefineSqlParameter("@Tag", SqlDbType.NText, ParameterDirection.Input, tag);
            sph.DefineSqlParameter("@FTS", SqlDbType.NText, ParameterDirection.Input, fTS);
            sph.DefineSqlParameter("@ImageUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, imageUrl);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_ArticleReference table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ArticleReference_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_ArticleReference table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleReference_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }
        public static IDataReader GetArticleByRootId(
            int rootItemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleReference_SelectArticleByRootId", 1);
            sph.DefineSqlParameter("@RootArticleID", SqlDbType.Int, ParameterDirection.Input, rootItemID);
            return sph.ExecuteReader();

        }
        public static IDataReader GetArticleTop(
           int CategoryID, int Top)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleReference_SelectTop", 2);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, CategoryID);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, Top);
            return sph.ExecuteReader();

        }
        public static IDataReader GetArticleByID(
           int itemId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleReference_SelectById", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemId);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_ArticleReference table.
        /// </summary>
        public static int GetCount(
            int siteId,
            int moduleId,
             int categoryID,
            bool? status,
            string keyword
            )
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleReference_GetCount", 5);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@Status", SqlDbType.Bit, ParameterDirection.Input, status);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, ParameterDirection.Input, keyword);
            return Convert.ToInt32(sph.ExecuteScalar());
        }

        public static IDataReader GetOthersPageModule(
          string[] listModuleID,
          int pageSize,
          int currentPage)
        {

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleReference_Others_SelectPage_ByModuleID", 3);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, currentPage);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@ListModuleId", SqlDbType.NVarChar, ParameterDirection.Input, listModuleID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_ArticleReference table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_ArticleReference_SelectAll",
                null);

        }

        public static IDataReader GetArticleTopOrther(
           int CategoryID, int ItemId, int Top, bool Hot)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleReference_SelectTopOrther", 4);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, CategoryID);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, ItemId);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, Top);
            sph.DefineSqlParameter("@Hot", SqlDbType.Bit, ParameterDirection.Input, Hot);
            return sph.ExecuteReader();
        }
        public static IDataReader GetArticleTopHot(int Top)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleReference_SelectTop_Hot", 1);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, Top);
            return sph.ExecuteReader();

        }
        public static IDataReader GetArticleTopHotOrther(int ItemId, int Top)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleReference_SelectTop_HotOrther", 2);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, ItemId);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, Top);
            return sph.ExecuteReader();

        }
        /// <summary>
        /// Gets a page of data from the md_ArticleReference table.
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
            bool? status,
            string keyword,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCount(siteId, moduleId, categoryID, status, keyword);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleReference_SelectPage", 7);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@Status", SqlDbType.Bit, ParameterDirection.Input, status);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, ParameterDirection.Input, keyword);
            return sph.ExecuteReader();

        }
        public static int GetPageCategoryCount(int categoryID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleReference_Others_SelectPage_ByCategoryID_Count", 1);
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleReference_Others_SelectPage_ByCategoryID", 3);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            return sph.ExecuteReader();

        }

    }

}



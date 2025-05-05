using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Data
{
    public class DBArticleBuilder
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
            string articleReference
            //bool hotNew
            )
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Articles_Insert", 45);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 250, ParameterDirection.Input, title);
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
            //sph.DefineSqlParameter("@IsHotNew", SqlDbType.Bit, ParameterDirection.Input, hotNew);

            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
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
            string articleReference
            //bool hotNew
            )
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Articles_Update", 45);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 250, ParameterDirection.Input, title);
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
            //sph.DefineSqlParameter("@IsHotNew", SqlDbType.Bit, ParameterDirection.Input, hotNew);
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

        public static IDataReader GetByModule(int moduleId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectByModule", 1);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            return sph.ExecuteReader();
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
    }
}

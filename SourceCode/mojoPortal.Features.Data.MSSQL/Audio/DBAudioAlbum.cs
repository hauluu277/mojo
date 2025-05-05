// Author:					hauld
// Created:					2023-7-6
// Last Modified:			2023-7-6
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

    public static class DBmd_AudioAlbum
    {


        /// <summary>
        /// Inserts a row in the md_AudioAlbum table. Returns new integer id.
        /// </summary>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="groupAudioID"> groupAudioID </param>
        /// <param name="categoryID"> categoryID </param>
        /// <param name="fileName"> fileName </param>
        /// <param name="description"> description </param>
        /// <param name="itemUrl"> itemUrl </param>
        /// <param name="totalView"> totalView </param>
        /// <param name="sizeInKB"> sizeInKB </param>
        /// <param name="typeData"> typeData </param>
        /// <param name="filePath"> filePath </param>
        /// <param name="imageVideo"> imageVideo </param>
        /// <param name="featured"> featured </param>
        /// <param name="isPublish"> isPublish </param>
        /// <param name="userGuid"> userGuid </param>
        /// <param name="createdByUser"> createdByUser </param>
        /// <param name="createdDate"> createdDate </param>
        /// <param name="lastModUserGuid"> lastModUserGuid </param>
        /// <param name="lastModUtc"> lastModUtc </param>
        /// <param name="albumOrder"> albumOrder </param>
        /// <param name="embedCode"> embedCode </param>
        /// <returns>int</returns>
        public static int Create(
            int moduleID,
            int siteID,
            int groupAudioID,
            int categoryID,
            string fileName,
            string description,
            string itemUrl,
            int totalView,
            int sizeInKB,
            int typeData,
            string filePath,
            string imageVideo,
            bool featured,
            bool isPublish,
            Guid userGuid,
            string createdByUser,
            DateTime createdDate,
            Guid lastModUserGuid,
            DateTime lastModUtc,
            int albumOrder,
            string embedCode)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_AudioAlbum_Insert", 21);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@GroupAudioID", SqlDbType.Int, ParameterDirection.Input, groupAudioID);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@FileName", SqlDbType.NVarChar, 255, ParameterDirection.Input, fileName);
            sph.DefineSqlParameter("@Description", SqlDbType.NVarChar, 500, ParameterDirection.Input, description);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, itemUrl);
            sph.DefineSqlParameter("@TotalView", SqlDbType.Int, ParameterDirection.Input, totalView);
            sph.DefineSqlParameter("@SizeInKB", SqlDbType.Int, ParameterDirection.Input, sizeInKB);
            sph.DefineSqlParameter("@TypeData", SqlDbType.Int, ParameterDirection.Input, typeData);
            sph.DefineSqlParameter("@FilePath", SqlDbType.NVarChar, 255, ParameterDirection.Input, filePath);
            sph.DefineSqlParameter("@ImageVideo", SqlDbType.NVarChar, 255, ParameterDirection.Input, imageVideo);
            sph.DefineSqlParameter("@Featured", SqlDbType.Bit, ParameterDirection.Input, featured);
            sph.DefineSqlParameter("@IsPublish", SqlDbType.Bit, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@UserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, userGuid);
            sph.DefineSqlParameter("@CreatedByUser", SqlDbType.NVarChar, 100, ParameterDirection.Input, createdByUser);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@LastModUserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, lastModUserGuid);
            sph.DefineSqlParameter("@LastModUtc", SqlDbType.DateTime, ParameterDirection.Input, lastModUtc);
            sph.DefineSqlParameter("@AlbumOrder", SqlDbType.Int, ParameterDirection.Input, albumOrder);
            sph.DefineSqlParameter("@EmbedCode", SqlDbType.NVarChar, -1, ParameterDirection.Input, embedCode);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_AudioAlbum table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="groupAudioID"> groupAudioID </param>
        /// <param name="categoryID"> categoryID </param>
        /// <param name="fileName"> fileName </param>
        /// <param name="description"> description </param>
        /// <param name="itemUrl"> itemUrl </param>
        /// <param name="totalView"> totalView </param>
        /// <param name="sizeInKB"> sizeInKB </param>
        /// <param name="typeData"> typeData </param>
        /// <param name="filePath"> filePath </param>
        /// <param name="imageVideo"> imageVideo </param>
        /// <param name="featured"> featured </param>
        /// <param name="isPublish"> isPublish </param>
        /// <param name="userGuid"> userGuid </param>
        /// <param name="createdByUser"> createdByUser </param>
        /// <param name="createdDate"> createdDate </param>
        /// <param name="lastModUserGuid"> lastModUserGuid </param>
        /// <param name="lastModUtc"> lastModUtc </param>
        /// <param name="albumOrder"> albumOrder </param>
        /// <param name="embedCode"> embedCode </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int moduleID,
            int siteID,
            int groupAudioID,
            int categoryID,
            string fileName,
            string description,
            string itemUrl,
            int totalView,
            int sizeInKB,
            int typeData,
            string filePath,
            string imageVideo,
            bool featured,
            bool isPublish,
            Guid userGuid,
            string createdByUser,
            DateTime createdDate,
            Guid lastModUserGuid,
            DateTime lastModUtc,
            int albumOrder,
            string embedCode)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_AudioAlbum_Update", 22);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@GroupAudioID", SqlDbType.Int, ParameterDirection.Input, groupAudioID);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@FileName", SqlDbType.NVarChar, 255, ParameterDirection.Input, fileName);
            sph.DefineSqlParameter("@Description", SqlDbType.NVarChar, 500, ParameterDirection.Input, description);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, itemUrl);
            sph.DefineSqlParameter("@TotalView", SqlDbType.Int, ParameterDirection.Input, totalView);
            sph.DefineSqlParameter("@SizeInKB", SqlDbType.Int, ParameterDirection.Input, sizeInKB);
            sph.DefineSqlParameter("@TypeData", SqlDbType.Int, ParameterDirection.Input, typeData);
            sph.DefineSqlParameter("@FilePath", SqlDbType.NVarChar, 255, ParameterDirection.Input, filePath);
            sph.DefineSqlParameter("@ImageVideo", SqlDbType.NVarChar, 255, ParameterDirection.Input, imageVideo);
            sph.DefineSqlParameter("@Featured", SqlDbType.Bit, ParameterDirection.Input, featured);
            sph.DefineSqlParameter("@IsPublish", SqlDbType.Bit, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@UserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, userGuid);
            sph.DefineSqlParameter("@CreatedByUser", SqlDbType.NVarChar, 100, ParameterDirection.Input, createdByUser);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@LastModUserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, lastModUserGuid);
            sph.DefineSqlParameter("@LastModUtc", SqlDbType.DateTime, ParameterDirection.Input, lastModUtc);
            sph.DefineSqlParameter("@AlbumOrder", SqlDbType.Int, ParameterDirection.Input, albumOrder);
            sph.DefineSqlParameter("@EmbedCode", SqlDbType.NVarChar, -1, ParameterDirection.Input, embedCode);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }



        public static IDataReader GetByGroup(int siteID, int GroupID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_AudioAlbum_SelectByGroup", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@GroupID", SqlDbType.Int, ParameterDirection.Input, GroupID);
            return sph.ExecuteReader();
        }

        /// <summary>
        /// Deletes a row from the md_AudioAlbum table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_AudioAlbum_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_AudioAlbum table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_AudioAlbum_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_AudioAlbum table.
        /// </summary>
        public static int GetCount()
        {
            

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_AudioAlbum_GetCount",
                null));

        }


        public static int DeleteBy(int groupAudioId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_AudioAlbum_DeleteBy", 1);
            sph.DefineSqlParameter("@GroupAudioID", SqlDbType.Int, ParameterDirection.Input, groupAudioId);
            return Convert.ToInt32(sph.ExecuteScalar());

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_AudioAlbum table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_AudioAlbum_SelectAll",
                null);

        }



        /// <summary>
        /// Gets a page of data from the md_AudioAlbum table.
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_AudioAlbum_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



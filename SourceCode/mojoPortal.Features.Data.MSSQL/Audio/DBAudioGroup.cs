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

    public static class DBmd_AudioGroup
    {


        /// <summary>
        /// Inserts a row in the md_AudioGroup table. Returns new integer id.
        /// </summary>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="nameGroup"> nameGroup </param>
        /// <param name="itemUrl"> itemUrl </param>
        /// <param name="filePath"> filePath </param>
        /// <param name="isPublish"> isPublish </param>
        /// <param name="userGuid"> userGuid </param>
        /// <param name="createdByUser"> createdByUser </param>
        /// <param name="createdDate"> createdDate </param>
        /// <param name="lastModUserGuid"> lastModUserGuid </param>
        /// <param name="lastModUtc"> lastModUtc </param>
        /// <param name="category"> category </param>
        /// <param name="groupOrder"> groupOrder </param>
        /// <param name="createByID"> createByID </param>
        /// <param name="step"> step </param>
        /// <param name="sapo"> sapo </param>
        /// <param name="isHome"> isHome </param>
        /// <param name="counter"> counter </param>
        /// <returns>int</returns>
        public static int Create(
            int moduleID,
            int siteID,
            string nameGroup,
            string itemUrl,
            string filePath,
            bool isPublish,
            Guid userGuid,
            string createdByUser,
            DateTime createdDate,
            Guid lastModUserGuid,
            DateTime lastModUtc,
            int category,
            int groupOrder,
            int createByID,
            int step,
            string sapo,
            bool isHome,
            int counter)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_AudioGroup_Insert", 18);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@NameGroup", SqlDbType.NVarChar, 255, ParameterDirection.Input, nameGroup);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, itemUrl);
            sph.DefineSqlParameter("@FilePath", SqlDbType.NVarChar, 255, ParameterDirection.Input, filePath);
            sph.DefineSqlParameter("@IsPublish", SqlDbType.Bit, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@UserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, userGuid);
            sph.DefineSqlParameter("@CreatedByUser", SqlDbType.NVarChar, 100, ParameterDirection.Input, createdByUser);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@LastModUserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, lastModUserGuid);
            sph.DefineSqlParameter("@LastModUtc", SqlDbType.DateTime, ParameterDirection.Input, lastModUtc);
            sph.DefineSqlParameter("@Category", SqlDbType.Int, ParameterDirection.Input, category);
            sph.DefineSqlParameter("@GroupOrder", SqlDbType.Int, ParameterDirection.Input, groupOrder);
            sph.DefineSqlParameter("@CreateByID", SqlDbType.Int, ParameterDirection.Input, createByID);
            sph.DefineSqlParameter("@Step", SqlDbType.Int, ParameterDirection.Input, step);
            sph.DefineSqlParameter("@Sapo", SqlDbType.NText, ParameterDirection.Input, sapo);
            sph.DefineSqlParameter("@IsHome", SqlDbType.Bit, ParameterDirection.Input, isHome);
            sph.DefineSqlParameter("@Counter", SqlDbType.Int, ParameterDirection.Input, counter);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_AudioGroup table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="nameGroup"> nameGroup </param>
        /// <param name="itemUrl"> itemUrl </param>
        /// <param name="filePath"> filePath </param>
        /// <param name="isPublish"> isPublish </param>
        /// <param name="userGuid"> userGuid </param>
        /// <param name="createdByUser"> createdByUser </param>
        /// <param name="createdDate"> createdDate </param>
        /// <param name="lastModUserGuid"> lastModUserGuid </param>
        /// <param name="lastModUtc"> lastModUtc </param>
        /// <param name="category"> category </param>
        /// <param name="groupOrder"> groupOrder </param>
        /// <param name="createByID"> createByID </param>
        /// <param name="step"> step </param>
        /// <param name="sapo"> sapo </param>
        /// <param name="isHome"> isHome </param>
        /// <param name="counter"> counter </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int moduleID,
            int siteID,
            string nameGroup,
            string itemUrl,
            string filePath,
            bool isPublish,
            Guid userGuid,
            string createdByUser,
            DateTime createdDate,
            Guid lastModUserGuid,
            DateTime lastModUtc,
            int category,
            int groupOrder,
            int createByID,
            int step,
            string sapo,
            bool isHome,
            int counter)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_AudioGroup_Update", 19);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@NameGroup", SqlDbType.NVarChar, 255, ParameterDirection.Input, nameGroup);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, itemUrl);
            sph.DefineSqlParameter("@FilePath", SqlDbType.NVarChar, 255, ParameterDirection.Input, filePath);
            sph.DefineSqlParameter("@IsPublish", SqlDbType.Bit, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@UserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, userGuid);
            sph.DefineSqlParameter("@CreatedByUser", SqlDbType.NVarChar, 100, ParameterDirection.Input, createdByUser);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@LastModUserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, lastModUserGuid);
            sph.DefineSqlParameter("@LastModUtc", SqlDbType.DateTime, ParameterDirection.Input, lastModUtc);
            sph.DefineSqlParameter("@Category", SqlDbType.Int, ParameterDirection.Input, category);
            sph.DefineSqlParameter("@GroupOrder", SqlDbType.Int, ParameterDirection.Input, groupOrder);
            sph.DefineSqlParameter("@CreateByID", SqlDbType.Int, ParameterDirection.Input, createByID);
            sph.DefineSqlParameter("@Step", SqlDbType.Int, ParameterDirection.Input, step);
            sph.DefineSqlParameter("@Sapo", SqlDbType.NText, ParameterDirection.Input, sapo);
            sph.DefineSqlParameter("@IsHome", SqlDbType.Bit, ParameterDirection.Input, isHome);
            sph.DefineSqlParameter("@Counter", SqlDbType.Int, ParameterDirection.Input, counter);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_AudioGroup table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_AudioGroup_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_AudioGroup table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_AudioGroup_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_AudioGroup table.
        /// </summary>
        public static int GetCount(int siteId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_AudioGroup_GetCount", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            return Convert.ToInt32(sph.ExecuteScalar());
        }

        public static IDataReader GetAllBySitePublish(int siteID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_AudioGroup_SelectAllBySitePublish", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            return sph.ExecuteReader();
        }






        /// <summary>
        /// Gets an IDataReader with all rows in the md_AudioGroup table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_AudioGroup_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the md_AudioGroup table.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static IDataReader GetPage(
            int siteId,
            int pageNumber,
            int pageSize,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCount(siteId);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_AudioGroup_SelectPage", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

        public static IDataReader GetAllByModule(int ModuleID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_AudioGroup_SelectAllByModule", 1);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, ModuleID);
            return sph.ExecuteReader();

        }



        /// <summary>
        /// Gets a page of data from the md_MediaGroup table.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static IDataReader GetPageManage(
            string createByUser,
            DateTime? createDate,
            int siteID,
            int moduleID,
            int pageNumber,
            int pageSize,
            string keyword,
            int parentId,
            bool? isPublished,
            int userId,
            int roleAccess,
            int step,
            bool isCaNhan,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCountManage(createByUser, createDate, siteID, moduleID, keyword, parentId, isPublished, userId, roleAccess, step, isCaNhan);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_AudioGroup_SelectPageManage", 13);
            sph.DefineSqlParameter("@CreatedByUser", SqlDbType.NVarChar, 255, ParameterDirection.Input, createByUser);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createDate);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@ParentID", SqlDbType.Int, ParameterDirection.Input, parentId);
            sph.DefineSqlParameter("@IsPublished", SqlDbType.Bit, ParameterDirection.Input, isPublished);
            sph.DefineSqlParameter("@UserID", SqlDbType.Int, ParameterDirection.Input, userId);
            sph.DefineSqlParameter("@RoleAccess", SqlDbType.Int, ParameterDirection.Input, roleAccess);
            sph.DefineSqlParameter("@Step", SqlDbType.Int, ParameterDirection.Input, step);
            sph.DefineSqlParameter("@IsCaNhan", SqlDbType.Bit, ParameterDirection.Input, isCaNhan);
            return sph.ExecuteReader();
        }


        public static int GetCountManage(string createByUser, DateTime? createDate, int siteID, int moduleID, string keyword, int parentId, bool? isPublished, int userId, int roleAccess, int step, bool isCaNhan)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_AudioGroup_GetCountManage", 11);
            sph.DefineSqlParameter("@CreatedByUser", SqlDbType.NVarChar, 255, ParameterDirection.Input, createByUser);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createDate);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@ParentID", SqlDbType.Int, ParameterDirection.Input, parentId);

            sph.DefineSqlParameter("@IsPublished", SqlDbType.Bit, ParameterDirection.Input, isPublished);
            sph.DefineSqlParameter("@UserID", SqlDbType.Int, ParameterDirection.Input, userId);
            sph.DefineSqlParameter("@RoleAccess", SqlDbType.Int, ParameterDirection.Input, roleAccess);
            sph.DefineSqlParameter("@Step", SqlDbType.Int, ParameterDirection.Input, step);
            sph.DefineSqlParameter("@IsCaNhan", SqlDbType.Bit, ParameterDirection.Input, isCaNhan);
            return Convert.ToInt32(sph.ExecuteScalar());
        }



        public static IDataReader GetGroupOther(int siteID, int itemID, int top)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_AudioGroup_SelectOther", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, top);
            return sph.ExecuteReader();

        }

    }

}



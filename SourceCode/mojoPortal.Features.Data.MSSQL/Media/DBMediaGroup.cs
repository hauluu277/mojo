// Author:					HAULD
// Created:					2015-10-26
// Last Modified:			2015-10-26
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

namespace MediaGroupFeature.Data
{

    public static class DBMediaGroup
    {


        /// <summary>
        /// Inserts a row in the md_MediaGroup table. Returns new integer id.
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
        /// <returns>int</returns>
        public static int Create(
            int moduleID,
            int siteID,
            string nameGroup,
            string itemUrl,
            string filePath,
            bool? isPublish,
            bool? isHome,
            Guid userGuid,
            string createdByUser,
            DateTime createdDate,
            Guid lastModUserGuid,
            DateTime lastModUtc,
            int groupOrder,
            int category,
            int createByID,
            int step,
            string sapo)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_MediaGroup_Insert", 17);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@NameGroup", SqlDbType.NVarChar, 255, ParameterDirection.Input, nameGroup);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, itemUrl);
            sph.DefineSqlParameter("@FilePath", SqlDbType.NVarChar, 255, ParameterDirection.Input, filePath);
            sph.DefineSqlParameter("@IsPublish", SqlDbType.Bit, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@IsHome", SqlDbType.Bit, ParameterDirection.Input, isHome);
            sph.DefineSqlParameter("@UserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, userGuid);
            sph.DefineSqlParameter("@CreatedByUser", SqlDbType.NVarChar, 100, ParameterDirection.Input, createdByUser);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@LastModUserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, lastModUserGuid);
            sph.DefineSqlParameter("@LastModUtc", SqlDbType.DateTime, ParameterDirection.Input, lastModUtc);
            sph.DefineSqlParameter("@GroupOrder", SqlDbType.Int, ParameterDirection.Input, groupOrder);
            sph.DefineSqlParameter("@Category", SqlDbType.Int, ParameterDirection.Input, category);
            sph.DefineSqlParameter("@CreateByID", SqlDbType.Int, ParameterDirection.Input, createByID);
            sph.DefineSqlParameter("@Step", SqlDbType.Int, ParameterDirection.Input, step);
            sph.DefineSqlParameter("@Sapo", SqlDbType.NVarChar, -1, ParameterDirection.Input, sapo);


            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_MediaGroup table. Returns true if row updated.
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
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int moduleID,
            int siteID,
            string nameGroup,
            string itemUrl,
            string filePath,
            bool? isPublish,
            bool? isHome,
            Guid userGuid,
            string createdByUser,
            DateTime createdDate,
            Guid lastModUserGuid,
            DateTime lastModUtc,
            int groupOrder,
            int category,
            int createByID,
            int step,
            string sapo)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_MediaGroup_Update", 18);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@NameGroup", SqlDbType.NVarChar, 255, ParameterDirection.Input, nameGroup);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, itemUrl);
            sph.DefineSqlParameter("@FilePath", SqlDbType.NVarChar, 255, ParameterDirection.Input, filePath);
            sph.DefineSqlParameter("@IsPublish", SqlDbType.Bit, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@IsHome", SqlDbType.Bit, ParameterDirection.Input, isHome);
            sph.DefineSqlParameter("@UserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, userGuid);
            sph.DefineSqlParameter("@CreatedByUser", SqlDbType.NVarChar, 100, ParameterDirection.Input, createdByUser);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@LastModUserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, lastModUserGuid);
            sph.DefineSqlParameter("@LastModUtc", SqlDbType.DateTime, ParameterDirection.Input, lastModUtc);
            sph.DefineSqlParameter("@GroupOrder", SqlDbType.Int, ParameterDirection.Input, groupOrder);
            sph.DefineSqlParameter("@Category", SqlDbType.Int, ParameterDirection.Input, category);
            sph.DefineSqlParameter("@CreateByID", SqlDbType.Int, ParameterDirection.Input, createByID);
            sph.DefineSqlParameter("@Step", SqlDbType.Int, ParameterDirection.Input, step);
            sph.DefineSqlParameter("@Sapo", SqlDbType.NVarChar, -1, ParameterDirection.Input, sapo);

            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_MediaGroup table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_MediaGroup_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_MediaGroup table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_MediaGroup_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();
        }

        public static IDataReader GetMediaByCategory(int siteId, int categoryID, int top)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ArticleMediaGllery_SelectCategory", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@Category", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, top);
            return sph.ExecuteReader();
        }
        public static IDataReader GetOneNew()
        {
            return SqlHelper.ExecuteReader(ConnectionString.GetReadConnectionString(), CommandType.StoredProcedure, "md_MediaGroup_SelectOneNew", null);
        }

        public static IDataReader GetGroupOther(int siteID, int itemID, int top)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_MediaGroup_SelectOther", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, top);
            return sph.ExecuteReader();

        }
        public static int GetCount2(int siteID, int moduleID, bool? publish, string keyword, int category)
        {

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_MediaGroup_GetCount2", 5);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@Publish", SqlDbType.Bit, ParameterDirection.Input, publish);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, publish);
            sph.DefineSqlParameter("@Category", SqlDbType.Int, ParameterDirection.Input, category);


            return Convert.ToInt32(sph.ExecuteScalar());
        }
        /// <summary>
        /// Gets a count of rows in the md_MediaGroup table.
        /// </summary>
        public static int GetCount(int siteID, int moduleID, string keyword, int parentId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_MediaGroup_GetCount", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@ParentID", SqlDbType.Int, ParameterDirection.Input, parentId);
            return Convert.ToInt32(sph.ExecuteScalar());
        }

        public static int GetCountManage(string createByUser, DateTime? createDate, int siteID, int moduleID, string keyword, int parentId, bool? isPublished, int userId, int roleAccess, int step, bool isCaNhan)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_MediaGroup_GetCountManage", 11);
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


        public static int GetCountParent(string createByUser, DateTime? createDate, int siteID, int moduleID, string keyword)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_MediaGroup_GetCountParent", 5);
            sph.DefineSqlParameter("@CreatedByUser", SqlDbType.NVarChar, 255, ParameterDirection.Input, createByUser);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createDate);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);

            return Convert.ToInt32(sph.ExecuteScalar());
        }



        public static int GetCountPublish(int siteId, int categoryId, int moduleId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_MediaGroup_GetCountPublish", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryId);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);

            return Convert.ToInt32(sph.ExecuteScalar());
        }


        /// <summary>
        /// Gets an IDataReader with all rows in the md_MediaGroup table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_MediaGroup_SelectAll",
                null);


        }
        public static IDataReader GetAllByModule(int ModuleID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_MediaGroup_SelectAllByModule", 1);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, ModuleID);
            return sph.ExecuteReader();

        }
        public static IDataReader GetAllBySite(int siteID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_MediaGroup_SelectAllBySite", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            return sph.ExecuteReader();
        }
        public static IDataReader GetAllBySitePublish(int siteID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_MediaGroup_SelectAllBySitePublish", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            return sph.ExecuteReader();
        }


        public static IDataReader GetPage2(
          int siteID,
          int moduleID,
          int pageNumber,
          int pageSize,
          bool? publish,
          string keyword,
            int category,
          out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCount2(siteID, moduleID, publish, keyword, category);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_MediaGroup_SelectPage2", 7);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@Publish", SqlDbType.Bit, ParameterDirection.Input, publish);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@Category", SqlDbType.Int, ParameterDirection.Input, category);
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_MediaGroup_SelectPageManage", 13);
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

        public static IDataReader GetPage(
           int siteID,
           int moduleID,
           int pageNumber,
           int pageSize,
           string keyword,
           int parentId,

           out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCount(siteID, moduleID, keyword, parentId);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_MediaGroup_SelectPage", 6);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@ParentID", SqlDbType.Int, ParameterDirection.Input, parentId);
            return sph.ExecuteReader();
        }


        public static IDataReader GetList(
int siteID,
int top)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_MediaGroup_SelectList", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, top);
            return sph.ExecuteReader();
        }

        public static IDataReader GetListItem(
        int siteID,
        string listItem)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_MediaGroup_SelectListItem", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ListItem", SqlDbType.NVarChar, 255, ParameterDirection.Input, listItem);
            return sph.ExecuteReader();
        }
        public static IDataReader GetListParent(
      int siteID,
      int top)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_MediaGroup_SelectListParent", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, top);
            return sph.ExecuteReader();
        }

        public static IDataReader GetPageParent(
           string createByUser,
           DateTime? createDate,
           int siteID,
           int moduleID,
           int pageNumber,
           int pageSize,
           string keyword,
           out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCountParent(createByUser, createDate, siteID, moduleID, keyword);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_MediaGroup_SelectPageParent", 7);
            sph.DefineSqlParameter("@CreatedByUser", SqlDbType.NVarChar, 255, ParameterDirection.Input, createByUser);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createDate);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);

            return sph.ExecuteReader();

        }



        public static IDataReader GetPagePublish(
     int siteId,
     int categoryId,
     int moduleID,
     int pageNumber,
     int pageSize,
     out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCountPublish(siteId, categoryId, moduleID);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_MediaGroup_SelectPagePublish", 5);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryId);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);

            return sph.ExecuteReader();

        }

        public static IDataReader GetTopHot(int siteId, int top)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_MediaGroup_SelectTop", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, top);
            return sph.ExecuteReader();
        }


    }

}



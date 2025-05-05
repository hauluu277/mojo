// Author:					Mr Hậu
// Created:					2020-3-13
// Last Modified:			2020-3-13
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

    public static class DBcore_Menu
    {


        /// <summary>
        /// Inserts a row in the core_Menu table. Returns new integer id.
        /// </summary>
        /// <param name="siteID"> siteID </param>
        /// <param name="parentID"> parentID </param>
        /// <param name="name"> name </param>
        /// <param name="linkMenu"> linkMenu </param>
        /// <param name="imageUrl"> imageUrl </param>
        /// <param name="orderBy"> orderBy </param>
        /// <param name="createdDate"> createdDate </param>
        /// <param name="createdBy"> createdBy </param>
        /// <param name="updatedDate"> updatedDate </param>
        /// <param name="updateBy"> updateBy </param>
        /// <returns>int</returns>
        public static int Create(
            int siteID,
            int parentID,
            string name,
            string linkMenu,
            string imageUrl,
            int orderBy,
            DateTime createdDate,
            int createdBy,
            DateTime updatedDate,
            int updateBy,
            string styleCss,
            int typeMenu,
            bool? show,
            bool? isDisplayLink,
            bool? isPhongBan,
            bool? isEnglish,
            int? typeLink,
            long? itemLink,
            bool isLogin,
            bool noClick,
            bool targetBlank)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_Menu_Insert", 21);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ParentID", SqlDbType.Int, ParameterDirection.Input, parentID);
            sph.DefineSqlParameter("@Name", SqlDbType.NVarChar, 350, ParameterDirection.Input, name);
            sph.DefineSqlParameter("@LinkMenu", SqlDbType.NVarChar, 550, ParameterDirection.Input, linkMenu);
            sph.DefineSqlParameter("@ImageUrl", SqlDbType.NVarChar, -1, ParameterDirection.Input, imageUrl);
            sph.DefineSqlParameter("@OrderBy", SqlDbType.Int, ParameterDirection.Input, orderBy);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@CreatedBy", SqlDbType.Int, ParameterDirection.Input, createdBy);
            sph.DefineSqlParameter("@UpdatedDate", SqlDbType.DateTime, ParameterDirection.Input, updatedDate);
            sph.DefineSqlParameter("@UpdateBy", SqlDbType.Int, ParameterDirection.Input, updateBy);
            sph.DefineSqlParameter("@StyleCss", SqlDbType.NVarChar, 250, ParameterDirection.Input, styleCss);
            sph.DefineSqlParameter("@TypeMenu", SqlDbType.Int, ParameterDirection.Input, typeMenu);
            sph.DefineSqlParameter("@Show", SqlDbType.Bit, ParameterDirection.Input, show);
            sph.DefineSqlParameter("@IsDisplayLink", SqlDbType.Bit, ParameterDirection.Input, isDisplayLink);
            sph.DefineSqlParameter("@IsPhongBan", SqlDbType.Bit, ParameterDirection.Input, isPhongBan);
            sph.DefineSqlParameter("@IsEnglish", SqlDbType.Bit, ParameterDirection.Input, isEnglish);
            sph.DefineSqlParameter("@TypeLink", SqlDbType.Int, ParameterDirection.Input, typeLink);
            sph.DefineSqlParameter("@ItemLink", SqlDbType.BigInt, ParameterDirection.Input, itemLink);
            sph.DefineSqlParameter("@IsLogin", SqlDbType.Bit, ParameterDirection.Input, isLogin);
            sph.DefineSqlParameter("@NoClick", SqlDbType.Bit, ParameterDirection.Input, noClick);
            sph.DefineSqlParameter("@TargetBlank", SqlDbType.Bit, ParameterDirection.Input, targetBlank);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the core_Menu table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="parentID"> parentID </param>
        /// <param name="name"> name </param>
        /// <param name="linkMenu"> linkMenu </param>
        /// <param name="imageUrl"> imageUrl </param>
        /// <param name="orderBy"> orderBy </param>
        /// <param name="createdDate"> createdDate </param>
        /// <param name="createdBy"> createdBy </param>
        /// <param name="updatedDate"> updatedDate </param>
        /// <param name="updateBy"> updateBy </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int siteID,
            int parentID,
            string name,
            string linkMenu,
            string imageUrl,
            int orderBy,
            DateTime createdDate,
            int createdBy,
            DateTime updatedDate,
            int updateBy,
            string styleCss,
            int typeMenu,
            bool? show,
            bool? isDisplayLink,
            bool isPhongBan,
            bool? isEnglish,
            int? typeLink,
            long? itemLink,
            bool isLogin,
            bool noclick,
            bool targetBlank)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_Menu_Update", 22);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ParentID", SqlDbType.Int, ParameterDirection.Input, parentID);
            sph.DefineSqlParameter("@Name", SqlDbType.NVarChar, 350, ParameterDirection.Input, name);
            sph.DefineSqlParameter("@LinkMenu", SqlDbType.NVarChar, 550, ParameterDirection.Input, linkMenu);
            sph.DefineSqlParameter("@ImageUrl", SqlDbType.NVarChar, -1, ParameterDirection.Input, imageUrl);
            sph.DefineSqlParameter("@OrderBy", SqlDbType.Int, ParameterDirection.Input, orderBy);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@CreatedBy", SqlDbType.Int, ParameterDirection.Input, createdBy);
            sph.DefineSqlParameter("@UpdatedDate", SqlDbType.DateTime, ParameterDirection.Input, updatedDate);
            sph.DefineSqlParameter("@UpdateBy", SqlDbType.Int, ParameterDirection.Input, updateBy);
            sph.DefineSqlParameter("@StyleCss", SqlDbType.NVarChar, 250, ParameterDirection.Input, styleCss);
            sph.DefineSqlParameter("@TypeMenu", SqlDbType.Int, ParameterDirection.Input, typeMenu);
            sph.DefineSqlParameter("@Show", SqlDbType.Bit, ParameterDirection.Input, show);
            sph.DefineSqlParameter("@IsDisplayLink", SqlDbType.Bit, ParameterDirection.Input, isDisplayLink);
            sph.DefineSqlParameter("@IsPhongBan", SqlDbType.Bit, ParameterDirection.Input, isPhongBan);
            sph.DefineSqlParameter("@IsEnglish", SqlDbType.Bit, ParameterDirection.Input, isEnglish);
            sph.DefineSqlParameter("@TypeLink", SqlDbType.Int, ParameterDirection.Input, typeLink);
            sph.DefineSqlParameter("@ItemLink", SqlDbType.BigInt, ParameterDirection.Input, itemLink);
            sph.DefineSqlParameter("@IsLogin", SqlDbType.Bit, ParameterDirection.Input, isLogin);
            sph.DefineSqlParameter("@NoClick", SqlDbType.Bit, ParameterDirection.Input, noclick);
            sph.DefineSqlParameter("@TargetBlank", SqlDbType.Bit, ParameterDirection.Input, targetBlank);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the core_Menu table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_Menu_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the core_Menu table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Menu_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();
        }


        public static IDataReader GetBySite(
            int siteId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Menu_SelectBySite", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            return sph.ExecuteReader();
        }

        public static IDataReader GetRoot(
             int siteId,
             int typeMenu,
             bool? isEnglish)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Menu_SelectRoot", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@TypeMenu", SqlDbType.Int, ParameterDirection.Input, typeMenu);
            sph.DefineSqlParameter("@IsEnglish", SqlDbType.Bit, ParameterDirection.Input, isEnglish);
            return sph.ExecuteReader();
        }

        public static IDataReader GetByParent(
            int parentId,
            bool? isEnglish,
            bool? isShow)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Menu_SelectByParent", 3);
            sph.DefineSqlParameter("@ParentID", SqlDbType.Int, ParameterDirection.Input, parentId);
            sph.DefineSqlParameter("@IsEnglish", SqlDbType.Bit, ParameterDirection.Input, isEnglish);
            sph.DefineSqlParameter("@IsShow", SqlDbType.Bit, ParameterDirection.Input, isShow);
            return sph.ExecuteReader();
        }


        public static IDataReader GetParentRoot(int siteId, int typeMenu, bool? isEnglish)
        {

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Menu_SelectRootParent", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@TypeMenu", SqlDbType.Int, ParameterDirection.Input, typeMenu);
            sph.DefineSqlParameter("@IsEnglish", SqlDbType.Bit, ParameterDirection.Input, isEnglish);
            return sph.ExecuteReader();

            //return SqlHelper.ExecuteReader(
            //    ConnectionString.GetReadConnectionString(),
            //    CommandType.StoredProcedure,
            //    "core_Menu_SelectRootParent",
            //    null);
        }

        /// <summary>
        /// Gets a count of rows in the core_Menu table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "core_Menu_GetCount",
                null));

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the core_Menu table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "core_Menu_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the core_Menu table.
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Menu_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



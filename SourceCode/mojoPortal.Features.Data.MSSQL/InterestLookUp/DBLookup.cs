// Author:					HiNet
// Created:					2014-9-3
// Last Modified:			2014-9-3
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

    public static class DBLookup
    {
        /// <summary>
        /// Inserts a row in the md_Lookup table. Returns new integer id.
        /// </summary>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="name"> name </param>
        /// <param name="description"> description </param>
        /// <param name="interpretation"> interpretation </param>
        /// <param name="isPublic"> isPublic </param>
        /// <param name="userCreate"> userCreate </param>
        /// <param name="dateCreate"> dateCreate </param>
        /// <param name="userApprove"> userApprove </param>
        /// <param name="dateApprove"> dateApprove </param>
        /// <returns>int</returns>
        public static int Create(
            int moduleID,
            int siteID,
            string name,
            string description,
            string interpretation,
            bool censorship,
            bool isPublic,
            int userCreate,
            DateTime dateCreate,
            int userApprove,
            DateTime dateApprove,
            string itemUrl,
            int pageID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Lookup_Insert", 13);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@Name", SqlDbType.NVarChar, 250, ParameterDirection.Input, name);
            sph.DefineSqlParameter("@Description", SqlDbType.NVarChar, 500, ParameterDirection.Input, description);
            sph.DefineSqlParameter("@Interpretation", SqlDbType.NText, ParameterDirection.Input, interpretation);
            sph.DefineSqlParameter("@Censorship", SqlDbType.Bit, ParameterDirection.Input, censorship);
            sph.DefineSqlParameter("@IsPublic", SqlDbType.Bit, ParameterDirection.Input, isPublic);
            sph.DefineSqlParameter("@UserCreate", SqlDbType.Int, ParameterDirection.Input, userCreate);
            sph.DefineSqlParameter("@DateCreate", SqlDbType.DateTime, ParameterDirection.Input, dateCreate);
            sph.DefineSqlParameter("@UserApprove", SqlDbType.Int, ParameterDirection.Input, userApprove);
            sph.DefineSqlParameter("@DateApprove", SqlDbType.DateTime, ParameterDirection.Input, dateApprove);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, itemUrl);
            sph.DefineSqlParameter("@PageID", SqlDbType.Int, ParameterDirection.Input, pageID);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_Lookup table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="name"> name </param>
        /// <param name="description"> description </param>
        /// <param name="interpretation"> interpretation </param>
        /// <param name="isPublic"> isPublic </param>
        /// <param name="userCreate"> userCreate </param>
        /// <param name="dateCreate"> dateCreate </param>
        /// <param name="userApprove"> userApprove </param>
        /// <param name="dateApprove"> dateApprove </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int moduleID,
            int siteID,
            string name,
            string description,
            string interpretation,
            bool censorship,
            bool isPublic,
            int userCreate,
            DateTime dateCreate,
            int userApprove,
            DateTime dateApprove,
            string itemUrl,
            int pageID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Lookup_Update", 14);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@Name", SqlDbType.NVarChar, 250, ParameterDirection.Input, name);
            sph.DefineSqlParameter("@Description", SqlDbType.NVarChar, 500, ParameterDirection.Input, description);
            sph.DefineSqlParameter("@Interpretation", SqlDbType.NText, ParameterDirection.Input, interpretation);
            sph.DefineSqlParameter("@Censorship", SqlDbType.Bit, ParameterDirection.Input, censorship);
            sph.DefineSqlParameter("@IsPublic", SqlDbType.Bit, ParameterDirection.Input, isPublic);
            sph.DefineSqlParameter("@UserCreate", SqlDbType.Int, ParameterDirection.Input, userCreate);
            sph.DefineSqlParameter("@DateCreate", SqlDbType.DateTime, ParameterDirection.Input, dateCreate);
            sph.DefineSqlParameter("@UserApprove", SqlDbType.Int, ParameterDirection.Input, userApprove);
            sph.DefineSqlParameter("@DateApprove", SqlDbType.DateTime, ParameterDirection.Input, dateApprove);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, itemUrl);
            sph.DefineSqlParameter("@PageID", SqlDbType.Int, ParameterDirection.Input, pageID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_Lookup table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Lookup_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_Lookup table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Lookup_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }
        public static IDataReader GetByTop(
            int Top)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Lookup_SelectByTop", 1);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, Top);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_Lookup table.
        /// </summary>
        public static int GetCount(string Keyword)
        {

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Lookup_GetCount", 1);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, 250, ParameterDirection.Input, Keyword);
            return Convert.ToInt32(sph.ExecuteScalar());

        }
        public static int GetCountByModule(int moduleId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Lookup_GetCountByModule", 1);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            return Convert.ToInt32(sph.ExecuteScalar());

        }

        public static IDataReader GetOrther(int itemId, int Top)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Lookup_SelectOrther", 2);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemId);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, Top);
            return sph.ExecuteReader();
        }
        /// <summary>
        /// Gets an IDataReader with all rows in the md_Lookup table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_Lookup_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the md_Lookup table.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>

        public static IDataReader GetPageByModule(
            int moduleId,
            int pageNumber,
            int pageSize,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCountByModule(moduleId);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Lookup_SelectPageByModule", 3);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }


        public static IDataReader GetPage(
            bool role,
            string keyword,
            int pageNumber,
            int pageSize,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCount(keyword);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Lookup_SelectPageZone", 4);
            sph.DefineSqlParameter("@Role", SqlDbType.Bit, ParameterDirection.Input, role);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, 250, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();
        }
    }

}



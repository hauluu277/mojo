// Author:					HAULD
// Created:					2015-10-2
// Last Modified:			2015-10-2
// 

using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using mojoPortal.Data;

namespace DictionaryFeature.Data
{

    public static class DBDictionary
    {


        /// <summary>
        /// Inserts a row in the md_Lookup table. Returns new integer id.
        /// </summary>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="name"> name </param>
        /// <param name="description"> description </param>
        /// <param name="interpretation"> interpretation </param>
        /// <param name="censorship"> censorship </param>
        /// <param name="isPublic"> isPublic </param>
        /// <param name="userCreate"> userCreate </param>
        /// <param name="dateCreate"> dateCreate </param>
        /// <param name="userApprove"> userApprove </param>
        /// <param name="dateApprove"> dateApprove </param>
        /// <param name="itemUrl"> itemUrl </param>
        /// <param name="pageID"> pageID </param>
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
        /// <param name="censorship"> censorship </param>
        /// <param name="isPublic"> isPublic </param>
        /// <param name="userCreate"> userCreate </param>
        /// <param name="dateCreate"> dateCreate </param>
        /// <param name="userApprove"> userApprove </param>
        /// <param name="dateApprove"> dateApprove </param>
        /// <param name="itemUrl"> itemUrl </param>
        /// <param name="pageID"> pageID </param>
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

        public static IDataReader GetMany(
            string keyword,
            string alphabe)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Lookup_Select", 2);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@alphabet", SqlDbType.NVarChar, 5, ParameterDirection.Input, alphabe);
            return sph.ExecuteReader();
        }

        /// <summary>
        /// Gets a count of rows in the md_Lookup table.
        /// </summary>
        public static int GetCount(int siteId,
            bool? isApprove,
            string keyword,
            string alphabe)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Lookup_GetCount", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@Censorship", SqlDbType.Bit, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@alphabet", SqlDbType.NVarChar, 5, ParameterDirection.Input, alphabe);
            return Convert.ToInt32(sph.ExecuteScalar());
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

        public static IDataReader GetAllPublic()
        {
            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_Lookup_SelectAll_Active",
                null);

        }

        /// <summary>
        /// Gets a page of data from the md_Lookup table.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static IDataReader GetPage(
            int siteId,
            int pageNumber,
            int pageSize,
            bool? isApprove,
            string keyword,
            string alphabe,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCount(siteId,
            isApprove,
            keyword,
            alphabe);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Lookup_SelectPage", 6);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@Censorship", SqlDbType.Bit, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@alphabet", SqlDbType.NVarChar, 5, ParameterDirection.Input, alphabe);
            return sph.ExecuteReader();

        }

    }

}



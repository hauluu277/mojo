// Author:					Viet Nguyen
// Created:					2015-11-11
// Last Modified:			2015-11-11
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

namespace DictionaryFeature.Data
{

    public static class DBDictionaryMean
    {


        /// <summary>
        /// Inserts a row in the md_LookupMean table. Returns new integer id.
        /// </summary>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="lookupID"> lookupID </param>
        /// <param name="interpretation"> interpretation </param>
        /// <param name="censorship"> censorship </param>
        /// <param name="isPublic"> isPublic </param>
        /// <param name="userCreate"> userCreate </param>
        /// <param name="dateCreate"> dateCreate </param>
        /// <param name="userApprove"> userApprove </param>
        /// <param name="dateApprove"> dateApprove </param>
        /// <returns>int</returns>
        public static int Create(
            int moduleID,
            int siteID,
            int lookupID,
            string interpretation,
            bool censorship,
            bool isPublic,
            int userCreate,
            DateTime dateCreate,
            int userApprove,
            DateTime dateApprove)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_LookupMean_Insert", 10);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@LookupID", SqlDbType.Int, ParameterDirection.Input, lookupID);
            sph.DefineSqlParameter("@Interpretation", SqlDbType.NText, ParameterDirection.Input, interpretation);
            sph.DefineSqlParameter("@Censorship", SqlDbType.Bit, ParameterDirection.Input, censorship);
            sph.DefineSqlParameter("@IsPublic", SqlDbType.Bit, ParameterDirection.Input, isPublic);
            sph.DefineSqlParameter("@UserCreate", SqlDbType.Int, ParameterDirection.Input, userCreate);
            sph.DefineSqlParameter("@DateCreate", SqlDbType.DateTime, ParameterDirection.Input, dateCreate);
            sph.DefineSqlParameter("@UserApprove", SqlDbType.Int, ParameterDirection.Input, userApprove);
            sph.DefineSqlParameter("@DateApprove", SqlDbType.DateTime, ParameterDirection.Input, dateApprove);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_LookupMean table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="lookupID"> lookupID </param>
        /// <param name="interpretation"> interpretation </param>
        /// <param name="censorship"> censorship </param>
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
            int lookupID,
            string interpretation,
            bool censorship,
            bool isPublic,
            int userCreate,
            DateTime dateCreate,
            int userApprove,
            DateTime dateApprove)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_LookupMean_Update", 11);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@LookupID", SqlDbType.Int, ParameterDirection.Input, lookupID);
            sph.DefineSqlParameter("@Interpretation", SqlDbType.NText, ParameterDirection.Input, interpretation);
            sph.DefineSqlParameter("@Censorship", SqlDbType.Bit, ParameterDirection.Input, censorship);
            sph.DefineSqlParameter("@IsPublic", SqlDbType.Bit, ParameterDirection.Input, isPublic);
            sph.DefineSqlParameter("@UserCreate", SqlDbType.Int, ParameterDirection.Input, userCreate);
            sph.DefineSqlParameter("@DateCreate", SqlDbType.DateTime, ParameterDirection.Input, dateCreate);
            sph.DefineSqlParameter("@UserApprove", SqlDbType.Int, ParameterDirection.Input, userApprove);
            sph.DefineSqlParameter("@DateApprove", SqlDbType.DateTime, ParameterDirection.Input, dateApprove);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_LookupMean table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_LookupMean_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_LookupMean table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_LookupMean_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_LookupMean table.
        /// </summary>
        public static int GetCount(int siteId,
            int pageNumber,
            int pageSize,
            bool? isApprove,
            bool? isPublish,
            int lookupId)
        {

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_LookupMean_GetCount", 6);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@Censorship", SqlDbType.Bit, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@IsPublic", SqlDbType.Bit, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@LookupID", SqlDbType.Int, ParameterDirection.Input, lookupId);
            return Convert.ToInt32(sph.ExecuteScalar());

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_LookupMean table.
        /// </summary>
        public static IDataReader GetAll(int siteId,
            bool? isApprove,
            bool? isPublish,
            int lookupId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_LookupMean_SelectAll", 4);
            //return SqlHelper.ExecuteReader(
            //    ConnectionString.GetReadConnectionString(),
            //    CommandType.StoredProcedure,
            //    "md_LookupMean_SelectAll",
            //    null);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@LookupID", SqlDbType.Int, ParameterDirection.Input, lookupId);
            sph.DefineSqlParameter("@Censorship", SqlDbType.Bit, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@IsPublic", SqlDbType.Bit, ParameterDirection.Input, isPublish);
            return sph.ExecuteReader();
        }

        /// <summary>
        /// Gets a page of data from the md_LookupMean table.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static IDataReader GetPage(
            int siteId,
            int pageNumber,
            int pageSize,
             bool? isApprove,
            bool? isPublish,
            int lookupId,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCount(siteId,
                pageNumber,
                pageSize,
                isApprove,
                isPublish,
                lookupId);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_LookupMean_SelectPage", 6);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@Censorship", SqlDbType.Bit, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@IsPublic", SqlDbType.Bit, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@LookupID", SqlDbType.Int, ParameterDirection.Input, lookupId);
            return sph.ExecuteReader();

        }

    }

}


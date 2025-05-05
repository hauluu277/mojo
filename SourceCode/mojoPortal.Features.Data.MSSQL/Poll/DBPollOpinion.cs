// Author:					HAULD
// Created:					2015-10-21
// Last Modified:			2015-10-21
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

namespace PollFeature.Data
{

    public static class DBPollOpinion
    {


        /// <summary>
        /// Inserts a row in the mp_PollOpinion table. Returns new integer id.
        /// </summary>
        /// <param name="siteID"> siteID </param>
        /// <param name="opinion"> opinion </param>
        /// <param name="createByUser"> createByUser </param>
        /// <param name="pollGuid"> pollGuid </param>
        /// <returns>int</returns>
        public static int Create(
            int siteID,
            string opinion,
            int createByUser,
            Guid pollGuid)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "mp_PollOpinion_Insert", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@Opinion", SqlDbType.NVarChar, 500, ParameterDirection.Input, opinion);
            sph.DefineSqlParameter("@CreateByUser", SqlDbType.Int, ParameterDirection.Input, createByUser);
            sph.DefineSqlParameter("@PollGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, pollGuid);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the mp_PollOpinion table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="opinion"> opinion </param>
        /// <param name="createByUser"> createByUser </param>
        /// <param name="pollGuid"> pollGuid </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int siteID,
            string opinion,
            int createByUser,
            Guid pollGuid)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "mp_PollOpinion_Update", 5);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@Opinion", SqlDbType.NVarChar, 500, ParameterDirection.Input, opinion);
            sph.DefineSqlParameter("@CreateByUser", SqlDbType.Int, ParameterDirection.Input, createByUser);
            sph.DefineSqlParameter("@PollGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, pollGuid);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the mp_PollOpinion table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "mp_PollOpinion_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the mp_PollOpinion table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_PollOpinion_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }
        public static IDataReader GetByPollGuid(Guid pollGuid)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_PollOpinion_SelectByPolls", 1);
            sph.DefineSqlParameter("@PollGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, pollGuid);
            return sph.ExecuteReader();
        }

        /// <summary>
        /// Gets a count of rows in the mp_PollOpinion table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "mp_PollOpinion_GetCount",
                null));

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the mp_PollOpinion table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "mp_PollOpinion_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the mp_PollOpinion table.
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_PollOpinion_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



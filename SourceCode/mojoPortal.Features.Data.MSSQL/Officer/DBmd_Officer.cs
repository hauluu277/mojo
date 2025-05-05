// Author:					Manhnd
// Created:					2020-4-11
// Last Modified:			2020-4-11
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

    public static class DBmd_Officer
    {


        /// <summary>
        /// Inserts a row in the md_Officer table. Returns new integer id.
        /// </summary>
        /// <param name="name"> name </param>
        /// <param name="position"> position </param>
        /// <param name="email"> email </param>
        /// <param name="phone"> phone </param>
        /// <param name="missionOfficer"> missionOfficer </param>
        /// <param name="urlImage"> urlImage </param>
        /// <param name="officerID"> officerID </param>
        /// <returns>int</returns>
        public static int Create(
            string name,
            string position,
            string email,
            string phone,
            string missionOfficer,
            string urlImage,
            int officerID,
            int count,
            int isTop,
            int orderByOfficer)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Officer_Insert", 10);
            sph.DefineSqlParameter("@Name", SqlDbType.NVarChar, 250, ParameterDirection.Input, name);
            sph.DefineSqlParameter("@Position", SqlDbType.NVarChar, 250, ParameterDirection.Input, position);
            sph.DefineSqlParameter("@Email", SqlDbType.NVarChar, 250, ParameterDirection.Input, email);
            sph.DefineSqlParameter("@Phone", SqlDbType.NVarChar, 250, ParameterDirection.Input, phone);
            sph.DefineSqlParameter("@MissionOfficer", SqlDbType.NText, ParameterDirection.Input, missionOfficer);
            sph.DefineSqlParameter("@UrlImage", SqlDbType.NVarChar, 500, ParameterDirection.Input, urlImage);
            sph.DefineSqlParameter("@OfficerID", SqlDbType.Int, ParameterDirection.Input, officerID);
            sph.DefineSqlParameter("@Count", SqlDbType.Int, ParameterDirection.Input, count);
            sph.DefineSqlParameter("@IsTop", SqlDbType.Int, ParameterDirection.Input, isTop);
            sph.DefineSqlParameter("@OrderByOfficer", SqlDbType.Int, ParameterDirection.Input, orderByOfficer);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_Officer table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="name"> name </param>
        /// <param name="position"> position </param>
        /// <param name="email"> email </param>
        /// <param name="phone"> phone </param>
        /// <param name="missionOfficer"> missionOfficer </param>
        /// <param name="urlImage"> urlImage </param>
        /// <param name="officerID"> officerID </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            string name,
            string position,
            string email,
            string phone,
            string missionOfficer,
            string urlImage,
            int officerID,
            int count,
            int isTop,
            int orderByOfficer)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Officer_Update", 11);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@Name", SqlDbType.NVarChar, 250, ParameterDirection.Input, name);
            sph.DefineSqlParameter("@Position", SqlDbType.NVarChar, 250, ParameterDirection.Input, position);
            sph.DefineSqlParameter("@Email", SqlDbType.NVarChar, 250, ParameterDirection.Input, email);
            sph.DefineSqlParameter("@Phone", SqlDbType.NVarChar, 250, ParameterDirection.Input, phone);
            sph.DefineSqlParameter("@MissionOfficer", SqlDbType.NText, ParameterDirection.Input, missionOfficer);
            sph.DefineSqlParameter("@UrlImage", SqlDbType.NVarChar, 500, ParameterDirection.Input, urlImage);
            sph.DefineSqlParameter("@OfficerID", SqlDbType.Int, ParameterDirection.Input, officerID);
            sph.DefineSqlParameter("@Count", SqlDbType.Int, ParameterDirection.Input, count);
            sph.DefineSqlParameter("@IsTop", SqlDbType.Int, ParameterDirection.Input, isTop);
            sph.DefineSqlParameter("@OrderByOfficer", SqlDbType.Int, ParameterDirection.Input, orderByOfficer);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_Officer table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Officer_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_Officer table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Officer_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_Officer table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_Officer_GetCount",
                null));

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_Officer table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_Officer_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the md_Officer table.
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Officer_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

        public static IDataReader GetList(int officerId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Officer_SelectList", 1);
            sph.DefineSqlParameter("@OfficerID", SqlDbType.Int, ParameterDirection.Input, officerId);
            return sph.ExecuteReader();
        }
        public static IDataReader GetList_ld(int officerId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Officer_SelectList_ld", 1);
            sph.DefineSqlParameter("@OfficerID", SqlDbType.Int, ParameterDirection.Input, officerId);
            return sph.ExecuteReader();
        }


    }

}



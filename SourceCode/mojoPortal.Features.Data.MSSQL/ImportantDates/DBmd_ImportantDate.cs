// Author:					Manh Dtr
// Created:					2020-1-6
// Last Modified:			2020-1-6
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

    public static class DBmd_ImportantDate
    {


        /// <summary>
        /// Inserts a row in the md_ImportantDates table. Returns new integer id.
        /// </summary>
        /// <param name="siteID"> siteID </param>
        /// <param name="pageID"> pageID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="titleImportant"> titleImportant </param>
        /// <param name="dateImportant1"> dateImportant1 </param>
        /// <param name="dateImportant2"> dateImportant2 </param>
        /// <param name="dateImportant3"> dateImportant3 </param>
        /// <param name="dateImportant4"> dateImportant4 </param>
        /// <returns>int</returns>
        public static int Create(
            int siteID,
            int pageID,
            int moduleID,
            string titleImportant,
            DateTime? dateImportant1,
            DateTime? dateImportant2,
            DateTime? dateImportant3,
            DateTime? dateImportant4)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ImportantDates_Insert", 8);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@PageID", SqlDbType.Int, ParameterDirection.Input, pageID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@TitleImportant", SqlDbType.NVarChar, 250, ParameterDirection.Input, titleImportant);
            sph.DefineSqlParameter("@DateImportant1", SqlDbType.DateTime, ParameterDirection.Input, dateImportant1);
            sph.DefineSqlParameter("@DateImportant2", SqlDbType.DateTime, ParameterDirection.Input, dateImportant2);
            sph.DefineSqlParameter("@DateImportant3", SqlDbType.DateTime, ParameterDirection.Input, dateImportant3);
            sph.DefineSqlParameter("@DateImportant4", SqlDbType.DateTime, ParameterDirection.Input, dateImportant4);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_ImportantDates table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="pageID"> pageID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="titleImportant"> titleImportant </param>
        /// <param name="dateImportant1"> dateImportant1 </param>
        /// <param name="dateImportant2"> dateImportant2 </param>
        /// <param name="dateImportant3"> dateImportant3 </param>
        /// <param name="dateImportant4"> dateImportant4 </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int siteID,
            int pageID,
            int moduleID,
            string titleImportant,
            DateTime? dateImportant1,
            DateTime? dateImportant2,
            DateTime? dateImportant3,
            DateTime? dateImportant4)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ImportantDates_Update", 9);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@PageID", SqlDbType.Int, ParameterDirection.Input, pageID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@TitleImportant", SqlDbType.NVarChar, 250, ParameterDirection.Input, titleImportant);
            sph.DefineSqlParameter("@DateImportant1", SqlDbType.DateTime, ParameterDirection.Input, dateImportant1);
            sph.DefineSqlParameter("@DateImportant2", SqlDbType.DateTime, ParameterDirection.Input, dateImportant2);
            sph.DefineSqlParameter("@DateImportant3", SqlDbType.DateTime, ParameterDirection.Input, dateImportant3);
            sph.DefineSqlParameter("@DateImportant4", SqlDbType.DateTime, ParameterDirection.Input, dateImportant4);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }
        public static IDataReader GetTopHot(
        int top, int siteId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ImportantDates_SelectTopHot", 2);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, top);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            return sph.ExecuteReader();
        }
        /// <summary>
        /// Deletes a row from the md_ImportantDates table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_ImportantDates_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_ImportantDates table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ImportantDates_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_ImportantDates table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_ImportantDates_GetCount",
                null));

        }
        public static int GetCount( DateTime? dateImportant1, DateTime? dateImportant2, DateTime? dateImportant3, DateTime? dateImportant4)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ImportantDates_GetCount_nb2", 4);
            sph.DefineSqlParameter("@DateImportant1", SqlDbType.DateTime, ParameterDirection.Input, dateImportant1);
            sph.DefineSqlParameter("@DateImportant2", SqlDbType.DateTime, ParameterDirection.Input, dateImportant2);
            sph.DefineSqlParameter("@DateImportant3", SqlDbType.DateTime, ParameterDirection.Input, dateImportant3);
            sph.DefineSqlParameter("@DateImportant4", SqlDbType.DateTime, ParameterDirection.Input, dateImportant4);

            return Convert.ToInt32(sph.ExecuteScalar());
            //ConnectionString.GetReadConnectionString(),
            //CommandType.StoredProcedure,
            //"md_Admissions_GetCount",
            //null));

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_ImportantDates table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_ImportantDates_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the md_ImportantDates table.
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ImportantDates_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

        public static IDataReader GetPage(
            DateTime? dateImportant1, DateTime? dateImportant2, DateTime? dateImportant3, DateTime? dateImportant4,
            int pageNumber,
            int pageSize,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCount(dateImportant1, dateImportant2, dateImportant3, dateImportant4);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_ImportantDates_SelectPage_nb2", 6);

            sph.DefineSqlParameter("@DateImportan1", SqlDbType.DateTime, ParameterDirection.Input, dateImportant1);
            sph.DefineSqlParameter("@DateImportan2", SqlDbType.DateTime, ParameterDirection.Input, dateImportant2);
            sph.DefineSqlParameter("@DateImportan3", SqlDbType.DateTime, ParameterDirection.Input, dateImportant3);
            sph.DefineSqlParameter("@DateImportan4", SqlDbType.DateTime, ParameterDirection.Input, dateImportant4);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



// Author:					Mr Hậu
// Created:					2020-12-28
// Last Modified:			2020-12-28
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

    public static class DBDAOTAO_HocVi
    {


        /// <summary>
        /// Inserts a row in the DAOTAO_HocVi table. Returns new integer id.
        /// </summary>
        /// <param name="hOCVI_ID"> hOCVI_ID </param>
        /// <param name="hOCVI_TEN"> hOCVI_TEN </param>
        /// <param name="hOCVI_MA"> hOCVI_MA </param>
        /// <returns>int</returns>
        public static int Create(
            string hOCVI_ID,
            string hOCVI_TEN,
            string hOCVI_MA)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "DAOTAO_HocVi_Insert", 3);
            sph.DefineSqlParameter("@HOCVI_ID", SqlDbType.NVarChar, 250, ParameterDirection.Input, hOCVI_ID);
            sph.DefineSqlParameter("@HOCVI_TEN", SqlDbType.NVarChar, 350, ParameterDirection.Input, hOCVI_TEN);
            sph.DefineSqlParameter("@HOCVI_MA", SqlDbType.NVarChar, 350, ParameterDirection.Input, hOCVI_MA);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the DAOTAO_HocVi table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="hOCVI_ID"> hOCVI_ID </param>
        /// <param name="hOCVI_TEN"> hOCVI_TEN </param>
        /// <param name="hOCVI_MA"> hOCVI_MA </param>
        /// <returns>bool</returns>
        public static bool Update(
            long itemID,
            string hOCVI_ID,
            string hOCVI_TEN,
            string hOCVI_MA)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "DAOTAO_HocVi_Update", 4);
            sph.DefineSqlParameter("@ItemID", SqlDbType.BigInt, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@HOCVI_ID", SqlDbType.NVarChar, 250, ParameterDirection.Input, hOCVI_ID);
            sph.DefineSqlParameter("@HOCVI_TEN", SqlDbType.NVarChar, 350, ParameterDirection.Input, hOCVI_TEN);
            sph.DefineSqlParameter("@HOCVI_MA", SqlDbType.NVarChar, 350, ParameterDirection.Input, hOCVI_MA);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the DAOTAO_HocVi table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            long itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "DAOTAO_HocVi_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.BigInt, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the DAOTAO_HocVi table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            long itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "DAOTAO_HocVi_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.BigInt, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the DAOTAO_HocVi table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "DAOTAO_HocVi_GetCount",
                null));

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the DAOTAO_HocVi table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "DAOTAO_HocVi_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the DAOTAO_HocVi table.
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "DAOTAO_HocVi_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



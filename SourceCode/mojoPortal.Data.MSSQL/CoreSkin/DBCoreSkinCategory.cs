// Author:					Joe Audette
// Created:					2017-12-23
// Last Modified:			2017-12-23
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

    public static class DBCoreSkinCategory
    {


        /// <summary>
        /// Inserts a row in the core_SkinCategory table. Returns new integer id.
        /// </summary>
        /// <param name="parentID"> parentID </param>
        /// <param name="categoryName"> categoryName </param>
        /// <param name="skinID"> skinID </param>
        /// <param name="urlDescription"> urlDescription </param>
        /// <param name="orderBy"> orderBy </param>
        /// <param name="isCategoryTemplate"> isCategoryTemplate </param>
        /// <param name="dateCreate"> dateCreate </param>
        /// <param name="createBy"> createBy </param>
        /// <returns>int</returns>
        public static int Create(
            int parentID,
            string categoryName,
            int skinID,
            string urlDescription,
            int orderBy,
            bool isCategoryTemplate,
            DateTime dateCreate,
            int createBy)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_SkinCategory_Insert",8);
            sph.DefineSqlParameter("@ParentID", SqlDbType.Int, ParameterDirection.Input, parentID);
            sph.DefineSqlParameter("@CategoryName", SqlDbType.NVarChar, 255, ParameterDirection.Input, categoryName);
            sph.DefineSqlParameter("@SkinID", SqlDbType.Int, ParameterDirection.Input, skinID);
            sph.DefineSqlParameter("@UrlDescription", SqlDbType.NVarChar, 250, ParameterDirection.Input, urlDescription);
            sph.DefineSqlParameter("@OrderBy", SqlDbType.Int, ParameterDirection.Input, orderBy);
            sph.DefineSqlParameter("@IsCategoryTemplate", SqlDbType.Bit, ParameterDirection.Input, isCategoryTemplate);
            sph.DefineSqlParameter("@DateCreate", SqlDbType.DateTime, ParameterDirection.Input, dateCreate);
            sph.DefineSqlParameter("@CreateBy", SqlDbType.Int, ParameterDirection.Input, createBy);
            
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the core_SkinCategory table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="parentID"> parentID </param>
        /// <param name="categoryName"> categoryName </param>
        /// <param name="skinID"> skinID </param>
        /// <param name="urlDescription"> urlDescription </param>
        /// <param name="orderBy"> orderBy </param>
        /// <param name="isCategoryTemplate"> isCategoryTemplate </param>
        /// <param name="dateCreate"> dateCreate </param>
        /// <param name="createBy"> createBy </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int parentID,
            string categoryName,
            int skinID,
            string urlDescription,
            int orderBy,
            bool isCategoryTemplate,
            DateTime dateCreate,
            int createBy)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_SkinCategory_Update", 9);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@ParentID", SqlDbType.Int, ParameterDirection.Input, parentID);
            sph.DefineSqlParameter("@CategoryName", SqlDbType.NVarChar, 255, ParameterDirection.Input, categoryName);
            sph.DefineSqlParameter("@SkinID", SqlDbType.Int, ParameterDirection.Input, skinID);
            sph.DefineSqlParameter("@UrlDescription", SqlDbType.NVarChar, 250, ParameterDirection.Input, urlDescription);
            sph.DefineSqlParameter("@OrderBy", SqlDbType.Int, ParameterDirection.Input, orderBy);
            sph.DefineSqlParameter("@IsCategoryTemplate", SqlDbType.Bit, ParameterDirection.Input, isCategoryTemplate);
            sph.DefineSqlParameter("@DateCreate", SqlDbType.DateTime, ParameterDirection.Input, dateCreate);
            sph.DefineSqlParameter("@CreateBy", SqlDbType.Int, ParameterDirection.Input, createBy);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the core_SkinCategory table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_SkinCategory_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the core_SkinCategory table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_SkinCategory_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }




        /// <summary>
        /// Gets a count of rows in the core_SkinCategory table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "core_SkinCategory_GetCount",
                null));

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the core_SkinCategory table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "core_SkinCategory_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the core_SkinCategory table.
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_SkinCategory_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



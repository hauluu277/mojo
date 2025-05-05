// Author:					HiNet JSC
// Created:					2014-6-16
// Last Modified:			2014-6-16
// 
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)  
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software.
using mojoPortal.Data;
using System;
using System.Data;

namespace SwirlingQuestionFeature.Data
{
    public static class DBSwirlingQuestionCategories
    {
        /// <summary>
        /// Inserts a row in the md_QACategory table. Returns new integer id.
        /// </summary>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="SwirlingQuestionCategory"> SwirlingQuestionCategory </param>
        /// <param name="description"> description </param>
        /// <param name="sortOrder"> sortOrder </param>
        /// <param name="lastModified"> lastModified </param>
        /// <returns>int</returns>
        public static int Create(
            int parentID,
            int moduleID,
            string categoryName,
            string description,
            int sortOrder,
            DateTime lastModified)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_QACategory_Insert", 6);
            sph.DefineSqlParameter("@ParentID", SqlDbType.Int, ParameterDirection.Input, parentID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@CategoryName", SqlDbType.NVarChar, 255, ParameterDirection.Input, categoryName);
            sph.DefineSqlParameter("@Description", SqlDbType.NVarChar, -1, ParameterDirection.Input, description);
            sph.DefineSqlParameter("@SortOrder", SqlDbType.Int, ParameterDirection.Input, sortOrder);
            sph.DefineSqlParameter("@LastModified", SqlDbType.DateTime, ParameterDirection.Input, lastModified);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_QACategory table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="SwirlingQuestionCategory"> SwirlingQuestionCategory </param>
        /// <param name="description"> description </param>
        /// <param name="sortOrder"> sortOrder </param>
        /// <param name="lastModified"> lastModified </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int parentID,
            int moduleID,
            string categoryName,
            string description,
            int sortOrder,
            DateTime lastModified)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_QACategory_Update", 7);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@ParentID", SqlDbType.Int, ParameterDirection.Input, parentID);
            sph.DefineSqlParameter("@CategoryName", SqlDbType.NVarChar, 255, ParameterDirection.Input, categoryName);
            sph.DefineSqlParameter("@Description", SqlDbType.NVarChar, -1, ParameterDirection.Input, description);
            sph.DefineSqlParameter("@SortOrder", SqlDbType.Int, ParameterDirection.Input, sortOrder);
            sph.DefineSqlParameter("@LastModified", SqlDbType.DateTime, ParameterDirection.Input, lastModified);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_QACategory table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_QACategory_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_QACategory table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_QACategory_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_QACategory table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_QACategory_GetCount",
                null));

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_QACategory table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_QACategory_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the md_QACategory table.
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_QACategory_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();
        }


        public static IDataReader GetRoot(int moduleID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_QACategory_SelectRoot", 1);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            return sph.ExecuteReader();
        }


        public static IDataReader GetChildren(int categoryId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_QACategory_SelectChildren", 1);
            sph.DefineSqlParameter("@CategoryId", SqlDbType.Int, ParameterDirection.Input, categoryId);
            return sph.ExecuteReader();
        }

        public static IDataReader GetParent(int parentId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_QACategory_SelectParent", 1);
            sph.DefineSqlParameter("@ParentId", SqlDbType.Int, ParameterDirection.Input, parentId);
            return sph.ExecuteReader();
        }
    }
}

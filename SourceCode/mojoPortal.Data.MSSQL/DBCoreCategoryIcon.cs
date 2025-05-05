using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace mojoPortal.Data
{
    public static class DBCoreCategoryIcon
    {
        /// <summary>
        /// Inserts a row in the md_IconModule table. Returns new integer id.
        /// </summary>
        /// <param name="iconName"> iconName </param>
        /// <param name="iconUrl"> iconUrl </param>
        /// <returns>int</returns>
        public static int Create(
            string iconName,
            string iconUrl)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_IconModule_Insert", 2);
            sph.DefineSqlParameter("@IconName", SqlDbType.NVarChar, 150, ParameterDirection.Input, iconName);
            sph.DefineSqlParameter("@IconUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, iconUrl);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_IconModule table. Returns true if row updated.
        /// </summary>
        /// <param name="iconID"> iconID </param>
        /// <param name="iconName"> iconName </param>
        /// <param name="iconUrl"> iconUrl </param>
        /// <returns>bool</returns>
        public static bool Update(
            int iconID,
            string iconName,
            string iconUrl)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_IconModule_Update", 3);
            sph.DefineSqlParameter("@IconID", SqlDbType.Int, ParameterDirection.Input, iconID);
            sph.DefineSqlParameter("@IconName", SqlDbType.NVarChar, 150, ParameterDirection.Input, iconName);
            sph.DefineSqlParameter("@IconUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, iconUrl);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_IconModule table. Returns true if row deleted.
        /// </summary>
        /// <param name="iconID"> iconID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int iconID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_IconModule_Delete", 1);
            sph.DefineSqlParameter("@IconID", SqlDbType.Int, ParameterDirection.Input, iconID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_IconModule table.
        /// </summary>
        /// <param name="iconID"> iconID </param>
        public static IDataReader GetOne(
            int iconID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_IconModule_SelectOne", 1);
            sph.DefineSqlParameter("@IconID", SqlDbType.Int, ParameterDirection.Input, iconID);
            return sph.ExecuteReader();

        }

        public static IDataReader GetOneByName(
            string iconName)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_IconModule_SelectByName", 1);
            sph.DefineSqlParameter("@IconName", SqlDbType.NVarChar, 150, ParameterDirection.Input, iconName);
            return sph.ExecuteReader();

        }
        public static IDataReader GetByUrl(
            string iconUrl)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_IconModule_SelectByName", 1);
            sph.DefineSqlParameter("@IconName", SqlDbType.NVarChar, 150, ParameterDirection.Input, iconUrl);
            return sph.ExecuteReader();

        }
        /// <summary>
        /// Gets a count of rows in the md_IconModule table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_IconModule_GetCount",
                null));

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_IconModule table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_IconModule_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the md_IconModule table.
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_IconModule_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }
    }
}

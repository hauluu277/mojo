// Author:					Mr Hậu
// Created:					2020-8-3
// Last Modified:			2020-8-3
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

    public static class DBFileAttachments
    {


        /// <summary>
        /// Inserts a row in the md_FileAttachment table. Returns new integer id.
        /// </summary>
        /// <param name="objectID"> objectID </param>
        /// <param name="typeItem"> typeItem </param>
        /// <param name="fileName"> fileName </param>
        /// <param name="filePath"> filePath </param>
        /// <param name="fileExtensions"> fileExtensions </param>
        /// <param name="downloadCount"> downloadCount </param>
        /// <param name="createdDate"> createdDate </param>
        /// <returns>int</returns>
        public static int Create(
            int objectID,
            int typeItem,
            string fileName,
            string filePath,
            string fileExtensions,
            int downloadCount,
            DateTime createdDate)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_FileAttachment_Insert", 7);
            sph.DefineSqlParameter("@ObjectID", SqlDbType.Int, ParameterDirection.Input, objectID);
            sph.DefineSqlParameter("@TypeItem", SqlDbType.Int, ParameterDirection.Input, typeItem);
            sph.DefineSqlParameter("@FileName", SqlDbType.NVarChar, 550, ParameterDirection.Input, fileName);
            sph.DefineSqlParameter("@FilePath", SqlDbType.NVarChar, -1, ParameterDirection.Input, filePath);
            sph.DefineSqlParameter("@FileExtensions", SqlDbType.NVarChar, 50, ParameterDirection.Input, fileExtensions);
            sph.DefineSqlParameter("@DownloadCount", SqlDbType.Int, ParameterDirection.Input, downloadCount);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_FileAttachment table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="objectID"> objectID </param>
        /// <param name="typeItem"> typeItem </param>
        /// <param name="fileName"> fileName </param>
        /// <param name="filePath"> filePath </param>
        /// <param name="fileExtensions"> fileExtensions </param>
        /// <param name="downloadCount"> downloadCount </param>
        /// <param name="createdDate"> createdDate </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int objectID,
            int typeItem,
            string fileName,
            string filePath,
            string fileExtensions,
            int downloadCount,
            DateTime createdDate)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_FileAttachment_Update", 8);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@ObjectID", SqlDbType.Int, ParameterDirection.Input, objectID);
            sph.DefineSqlParameter("@TypeItem", SqlDbType.Int, ParameterDirection.Input, typeItem);
            sph.DefineSqlParameter("@FileName", SqlDbType.NVarChar, 550, ParameterDirection.Input, fileName);
            sph.DefineSqlParameter("@FilePath", SqlDbType.NVarChar, -1, ParameterDirection.Input, filePath);
            sph.DefineSqlParameter("@FileExtensions", SqlDbType.NVarChar, 50, ParameterDirection.Input, fileExtensions);
            sph.DefineSqlParameter("@DownloadCount", SqlDbType.Int, ParameterDirection.Input, downloadCount);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_FileAttachment table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_FileAttachment_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);
        }


        public static bool DeleteByObject(
        int objectid)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_FileAttachment_DeleteByObject", 1);
            sph.DefineSqlParameter("@ObjectID", SqlDbType.Int, ParameterDirection.Input, objectid);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);
        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_FileAttachment table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_FileAttachment_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();
        }


        public static IDataReader GetByObject(int objectId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_FileAttachment_SelectByOject", 1);
            sph.DefineSqlParameter("@ObjectID", SqlDbType.Int, ParameterDirection.Input, objectId);
            return sph.ExecuteReader();
        }

        /// <summary>
        /// Gets a count of rows in the md_FileAttachment table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_FileAttachment_GetCount",
                null));

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_FileAttachment table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_FileAttachment_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the md_FileAttachment table.
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_FileAttachment_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



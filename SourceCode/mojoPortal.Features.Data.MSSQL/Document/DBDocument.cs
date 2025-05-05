// Author:					HiNet
// Created:					2015-3-19
// Last Modified:			2015-3-19
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

    public static class DBDocument
    {


        /// <summary>
        /// Inserts a row in the md_Document table. Returns new integer id.
        /// </summary>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="pageID"> pageID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="summary"> summary </param>
        /// <param name="sign"> sign </param>
        /// <param name="datePromulgate"> datePromulgate </param>
        /// <param name="dateEffect"> dateEffect </param>
        /// <param name="signer"> signer </param>
        /// <param name="filePath"> filePath </param>
        /// <param name="coQuanID"> coQuanID </param>
        /// <param name="loaiVB"> loaiVB </param>
        /// <param name="linhVuc"> linhVuc </param>
        /// <param name="itemUrl"> itemUrl </param>
        /// <param name="createdByUser"> createdByUser </param>
        /// <param name="isApproved"> isApproved </param>
        /// <param name="approvedDate"> approvedDate </param>
        /// <param name="approvedGuid"> approvedGuid </param>
        /// <param name="createdByUserGuid"> createdByUserGuid </param>
        /// <returns>int</returns>
        public static int Create(
            int moduleID,
            int pageID,
            int siteID,
            string summary,
            string sign,
            DateTime? datePromulgate,
            DateTime? dateEffect,
            string signer,
            string filePath,
            int coQuanID,
            int loaiVB,
            int linhVuc,
            string itemUrl,
            string createdByUser,
            bool isApproved,
            DateTime? approvedDate,
            Guid approvedGuid,
            Guid createdByUserGuid,
            int yearPromulgate,
            string nationalPromulgate,
            int langID,
            string fts,
            string contentDoc,
            int type)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Document_Insert", 24);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@PageID", SqlDbType.Int, ParameterDirection.Input, pageID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@Summary", SqlDbType.NText, ParameterDirection.Input, summary);
            sph.DefineSqlParameter("@Sign", SqlDbType.NVarChar, 255, ParameterDirection.Input, sign);
            sph.DefineSqlParameter("@DatePromulgate", SqlDbType.DateTime, ParameterDirection.Input, datePromulgate);
            sph.DefineSqlParameter("@DateEffect", SqlDbType.DateTime, ParameterDirection.Input, dateEffect);
            sph.DefineSqlParameter("@Signer", SqlDbType.NVarChar, 255, ParameterDirection.Input, signer);
            sph.DefineSqlParameter("@FilePath", SqlDbType.NVarChar, 255, ParameterDirection.Input, filePath);
            sph.DefineSqlParameter("@CoQuanID", SqlDbType.Int, ParameterDirection.Input, coQuanID);
            sph.DefineSqlParameter("@LoaiVB", SqlDbType.Int, ParameterDirection.Input, loaiVB);
            sph.DefineSqlParameter("@LinhVuc", SqlDbType.Int, ParameterDirection.Input, linhVuc);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, itemUrl);
            sph.DefineSqlParameter("@CreatedByUser", SqlDbType.NVarChar, 100, ParameterDirection.Input, createdByUser);
            sph.DefineSqlParameter("@IsApproved", SqlDbType.Bit, ParameterDirection.Input, isApproved);
            sph.DefineSqlParameter("@ApprovedDate", SqlDbType.DateTime, ParameterDirection.Input, approvedDate);
            sph.DefineSqlParameter("@ApprovedGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, approvedGuid);
            sph.DefineSqlParameter("@CreatedByUserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, createdByUserGuid);
            sph.DefineSqlParameter("@YearPromulgate", SqlDbType.Int, ParameterDirection.Input, yearPromulgate);
            sph.DefineSqlParameter("@NationalPromulgate", SqlDbType.NVarChar, 255, ParameterDirection.Input, nationalPromulgate);
            sph.DefineSqlParameter("@LangID", SqlDbType.Int, ParameterDirection.Input, langID);
            sph.DefineSqlParameter("@FTS", SqlDbType.NText, ParameterDirection.Input, fts);
            sph.DefineSqlParameter("@ContentDoc", SqlDbType.NText, ParameterDirection.Input, contentDoc);
            sph.DefineSqlParameter("@Type", SqlDbType.Int, ParameterDirection.Input, type);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_Document table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="pageID"> pageID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="summary"> summary </param>
        /// <param name="sign"> sign </param>
        /// <param name="datePromulgate"> datePromulgate </param>
        /// <param name="dateEffect"> dateEffect </param>
        /// <param name="signer"> signer </param>
        /// <param name="filePath"> filePath </param>
        /// <param name="coQuanID"> coQuanID </param>
        /// <param name="loaiVB"> loaiVB </param>
        /// <param name="linhVuc"> linhVuc </param>
        /// <param name="itemUrl"> itemUrl </param>
        /// <param name="createdByUser"> createdByUser </param>
        /// <param name="isApproved"> isApproved </param>
        /// <param name="approvedDate"> approvedDate </param>
        /// <param name="approvedGuid"> approvedGuid </param>
        /// <param name="createdByUserGuid"> createdByUserGuid </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int moduleID,
            int pageID,
            int siteID,
            string summary,
            string sign,
            DateTime? datePromulgate,
            DateTime? dateEffect,
            string signer,
            string filePath,
            int coQuanID,
            int loaiVB,
            int linhVuc,
            string itemUrl,
            string createdByUser,
            bool isApproved,
            DateTime? approvedDate,
            Guid approvedGuid,
            Guid createdByUserGuid,
            int yearPromulgate,
            string nationalPromulgate,
            int langID,
            string fts,
            string contentDoc,
            int type)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Document_Update", 25);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@PageID", SqlDbType.Int, ParameterDirection.Input, pageID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@Summary", SqlDbType.NText, ParameterDirection.Input, summary);
            sph.DefineSqlParameter("@Sign", SqlDbType.NVarChar, 255, ParameterDirection.Input, sign);
            sph.DefineSqlParameter("@DatePromulgate", SqlDbType.DateTime, ParameterDirection.Input, datePromulgate);
            sph.DefineSqlParameter("@DateEffect", SqlDbType.DateTime, ParameterDirection.Input, dateEffect);
            sph.DefineSqlParameter("@Signer", SqlDbType.NVarChar, 255, ParameterDirection.Input, signer);
            sph.DefineSqlParameter("@FilePath", SqlDbType.NVarChar, 255, ParameterDirection.Input, filePath);
            sph.DefineSqlParameter("@CoQuanID", SqlDbType.Int, ParameterDirection.Input, coQuanID);
            sph.DefineSqlParameter("@LoaiVB", SqlDbType.Int, ParameterDirection.Input, loaiVB);
            sph.DefineSqlParameter("@LinhVuc", SqlDbType.Int, ParameterDirection.Input, linhVuc);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, itemUrl);
            sph.DefineSqlParameter("@CreatedByUser", SqlDbType.NVarChar, 100, ParameterDirection.Input, createdByUser);
            sph.DefineSqlParameter("@IsApproved", SqlDbType.Bit, ParameterDirection.Input, isApproved);
            sph.DefineSqlParameter("@ApprovedDate", SqlDbType.DateTime, ParameterDirection.Input, approvedDate);
            sph.DefineSqlParameter("@ApprovedGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, approvedGuid);
            sph.DefineSqlParameter("@CreatedByUserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, createdByUserGuid);
            sph.DefineSqlParameter("@YearPromulgate", SqlDbType.Int, ParameterDirection.Input, yearPromulgate);
            sph.DefineSqlParameter("@NationalPromulgate", SqlDbType.NVarChar, 255, ParameterDirection.Input, nationalPromulgate);
            sph.DefineSqlParameter("@LangID", SqlDbType.Int, ParameterDirection.Input, langID);
            sph.DefineSqlParameter("@FTS", SqlDbType.NText, ParameterDirection.Input, fts);
            sph.DefineSqlParameter("@ContentDoc", SqlDbType.NText, ParameterDirection.Input, contentDoc);
            sph.DefineSqlParameter("@Type", SqlDbType.Int, ParameterDirection.Input, type);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_Document table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Document_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_Document table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Document_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }

        public static IDataReader GetById(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_document_SelectById", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }

        public static IDataReader GetTopSlide(
            int siteId, int number)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Document_SelectTopSlide", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            //sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            sph.DefineSqlParameter("@Number", SqlDbType.Int, ParameterDirection.Input, number);
            return sph.ExecuteReader();

        }
        public static IDataReader GetHotNew(int siteID, int SoLuong)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Document_SelectHotNew", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@SoLuong", SqlDbType.Int, ParameterDirection.Input, SoLuong);
            return sph.ExecuteReader();

        }

        public static IDataReader GetOthers(
            int itemID, int siteId, int moduleId, int linhVuc, int number)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Document_SelectOthers", 5);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            sph.DefineSqlParameter("@LinhVuc", SqlDbType.Int, ParameterDirection.Input, linhVuc);
            sph.DefineSqlParameter("@Number", SqlDbType.Int, ParameterDirection.Input, number);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_Document table.
        /// </summary>
        public static int GetCount(int siteId,
            int moduleId,
             int linhVuc,
            int loaiVb,
            int coQuan,
            int? status,
            int namBanHanh,
            int chuDe,
            string keyword
            )
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Document_GetCount", 9);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            sph.DefineSqlParameter("@LinhVuc", SqlDbType.Int, ParameterDirection.Input, linhVuc);
            sph.DefineSqlParameter("@LoaiVB", SqlDbType.Int, ParameterDirection.Input, loaiVb);
            sph.DefineSqlParameter("@CoQuan", SqlDbType.Int, ParameterDirection.Input, coQuan);
            sph.DefineSqlParameter("@Status", SqlDbType.Int, ParameterDirection.Input, status);
            sph.DefineSqlParameter("@NamBanHanh", SqlDbType.Int, ParameterDirection.Input, namBanHanh);
            sph.DefineSqlParameter("@ChuDe", SqlDbType.Int, ParameterDirection.Input, chuDe);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);
            return Convert.ToInt32(sph.ExecuteScalar());
        }
        public static int GetCountAll(int siteId,
          int moduleId,
           int linhVuc,
          int loaiVb,
          int coQuan,
          int? status,
          int namBanHanh,
          int chuDe,
          string keyword,
          int nhomVB
          )
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Document_GetCountAll", 10);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            sph.DefineSqlParameter("@LinhVuc", SqlDbType.Int, ParameterDirection.Input, linhVuc);
            sph.DefineSqlParameter("@LoaiVB", SqlDbType.Int, ParameterDirection.Input, loaiVb);
            sph.DefineSqlParameter("@CoQuan", SqlDbType.Int, ParameterDirection.Input, coQuan);
            sph.DefineSqlParameter("@Status", SqlDbType.Int, ParameterDirection.Input, status);
            sph.DefineSqlParameter("@NamBanHanh", SqlDbType.Int, ParameterDirection.Input, namBanHanh);
            sph.DefineSqlParameter("@ChuDe", SqlDbType.Int, ParameterDirection.Input, chuDe);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@NhomVB", SqlDbType.Int, ParameterDirection.Input, nhomVB);
            return Convert.ToInt32(sph.ExecuteScalar());
        }
        public static int GetCountByLinhVuc(int siteId,
            int moduleId,
             int linhVuc
            )
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Document_GetCountByLinhVuc", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            sph.DefineSqlParameter("@LinhVuc", SqlDbType.Int, ParameterDirection.Input, linhVuc);
            return Convert.ToInt32(sph.ExecuteScalar());
        }

        public static IDataReader GetDocumentByPage(int siteId, int pageId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Document_SelectByPage", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@PageID", SqlDbType.Int, ParameterDirection.Input, pageId);
            return sph.ExecuteReader();
        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_Document table.
        /// </summary>
        public static IDataReader GetAll(int siteID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Document_SelectAll", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            return sph.ExecuteReader();
        }

        public static IDataReader GetAllByYear(int siteID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Document_GetAllByYear", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            return sph.ExecuteReader();
        }

        public static IDataReader GetSelectYear()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_Document_SelectYear",
                null);

        }

        /// <summary>
        /// Gets a page of data from the md_Document table.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static IDataReader GetPage(
            int siteId,
            int moduleId,
            int linhVuc,
            int loaiVb,
            int coQuan,
            int pageNumber,
            int pageSize,
            int? status,
            int namBanHanh,
            int chuDe,
            string keyword,
            out int totalPages,
            out int totalItems)
        {
            totalPages = 1;
            int totalRows
                = GetCount(siteId, moduleId, linhVuc, loaiVb, coQuan, status, namBanHanh, chuDe, keyword);
            totalItems = totalRows;
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Document_SelectPage", 11);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            sph.DefineSqlParameter("@LinhVuc", SqlDbType.Int, ParameterDirection.Input, linhVuc);
            sph.DefineSqlParameter("@LoaiVB", SqlDbType.Int, ParameterDirection.Input, loaiVb);
            sph.DefineSqlParameter("@CoQuan", SqlDbType.Int, ParameterDirection.Input, coQuan);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@Status", SqlDbType.Int, ParameterDirection.Input, status);
            sph.DefineSqlParameter("@NamBanHanh", SqlDbType.Int, ParameterDirection.Input, namBanHanh);
            sph.DefineSqlParameter("@ChuDe", SqlDbType.Int, ParameterDirection.Input, chuDe);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a page of data from the md_Document table.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static IDataReader GetPageAll(
            int siteId,
            int moduleId,
            int linhVuc,
            int loaiVb,
            int coQuan,
            int pageNumber,
            int pageSize,
            int? status,
            int namBanHanh,
            int chuDe,
            string keyword,
            int nhomVB,
            out int totalPages,
            out int totalItems)
        {
            totalPages = 1;
            int totalRows
                = GetCountAll(siteId, moduleId, linhVuc, loaiVb, coQuan, status, namBanHanh, chuDe, keyword,nhomVB);
            totalItems = totalRows;
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Document_SelectPageAll", 12);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            sph.DefineSqlParameter("@LinhVuc", SqlDbType.Int, ParameterDirection.Input, linhVuc);
            sph.DefineSqlParameter("@LoaiVB", SqlDbType.Int, ParameterDirection.Input, loaiVb);
            sph.DefineSqlParameter("@CoQuan", SqlDbType.Int, ParameterDirection.Input, coQuan);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@Status", SqlDbType.Int, ParameterDirection.Input, status);
            sph.DefineSqlParameter("@NamBanHanh", SqlDbType.Int, ParameterDirection.Input, namBanHanh);
            sph.DefineSqlParameter("@ChuDe", SqlDbType.Int, ParameterDirection.Input, chuDe);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@NhomVB", SqlDbType.Int, ParameterDirection.Input, nhomVB);
            return sph.ExecuteReader();

        }

    }

}



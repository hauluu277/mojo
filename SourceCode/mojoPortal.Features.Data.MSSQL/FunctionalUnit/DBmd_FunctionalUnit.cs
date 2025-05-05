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

    public static class DBmd_FunctionalUnit
    {


        /// <summary>
        /// Inserts a row in the md_FunctionalUnit table. Returns new integer id.
        /// </summary>
        /// <param name="siteID"> siteID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="title"> title </param>
        /// <param name="general"> general </param>
        /// <param name="functionP"> functionP </param>
        /// <param name="mission"> mission </param>
        /// <param name="officerID"> officerID </param>
        /// <param name="achievement"> achievement </param>
        /// <param name="procedureP"> procedureP </param>
        /// <param name="contact"> contact </param>
        /// <param name="createDate"> createDate </param>
        /// <param name="creator"> creator </param>
        /// <param name="editDate"> editDate </param>
        /// <param name="editor"> editor </param>
        /// <param name="isPublished"> isPublished </param>
        /// <param name="urlItem"> isPublished </param>
        /// <returns>int</returns>
        public static int Create(
            int siteID,
            int moduleID,
            string title,
            string general,
            string functionP,
            string mission,
            int officerID,
            string achievement,
            string procedureP,
            string contact,
            DateTime createDate,
            int creator,
            DateTime editDate,
            int editor,
            bool? isPublished,
            string urlItem,
            string lichCongTac,
            int orderByUnit,
            int allowUserModify,
            string maKhoaPhong,
            bool? isShowQuestion)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_FunctionalUnit_Insert", 21);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 250, ParameterDirection.Input, title);
            sph.DefineSqlParameter("@General", SqlDbType.NText, ParameterDirection.Input, general);
            sph.DefineSqlParameter("@FunctionP", SqlDbType.NText, ParameterDirection.Input, functionP);
            sph.DefineSqlParameter("@Mission", SqlDbType.NText, ParameterDirection.Input, mission);
            sph.DefineSqlParameter("@OfficerID", SqlDbType.Int, ParameterDirection.Input, officerID);
            sph.DefineSqlParameter("@Achievement", SqlDbType.NText, ParameterDirection.Input, achievement);
            sph.DefineSqlParameter("@ProcedureP", SqlDbType.NText, ParameterDirection.Input, procedureP);
            sph.DefineSqlParameter("@Contact", SqlDbType.NText, ParameterDirection.Input, contact);
            sph.DefineSqlParameter("@CreateDate", SqlDbType.DateTime, ParameterDirection.Input, createDate);
            sph.DefineSqlParameter("@Creator", SqlDbType.Int, ParameterDirection.Input, creator);
            sph.DefineSqlParameter("@EditDate", SqlDbType.DateTime, ParameterDirection.Input, editDate);
            sph.DefineSqlParameter("@Editor", SqlDbType.Int, ParameterDirection.Input, editor);
            sph.DefineSqlParameter("@IsPublished", SqlDbType.Bit, ParameterDirection.Input, isPublished);
            sph.DefineSqlParameter("@UrlItem", SqlDbType.NVarChar, 500, ParameterDirection.Input, urlItem);
            sph.DefineSqlParameter("@LichCongTac", SqlDbType.NText, ParameterDirection.Input, lichCongTac);
            sph.DefineSqlParameter("@OrderByUnit", SqlDbType.Int, ParameterDirection.Input, orderByUnit);
            sph.DefineSqlParameter("@AllowUserModify", SqlDbType.Int, ParameterDirection.Input, allowUserModify);
            sph.DefineSqlParameter("@MaKhoaPhong", SqlDbType.NVarChar, 250, ParameterDirection.Input, maKhoaPhong);
            sph.DefineSqlParameter("@IsShowQuestion", SqlDbType.Bit, ParameterDirection.Input, isShowQuestion);

            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_FunctionalUnit table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="title"> title </param>
        /// <param name="general"> general </param>
        /// <param name="functionP"> functionP </param>
        /// <param name="mission"> mission </param>
        /// <param name="officerID"> officerID </param>
        /// <param name="achievement"> achievement </param>
        /// <param name="procedureP"> procedureP </param>
        /// <param name="contact"> contact </param>
        /// <param name="createDate"> createDate </param>
        /// <param name="creator"> creator </param>
        /// <param name="editDate"> editDate </param>
        /// <param name="editor"> editor </param>
        /// <param name="isPublished"> isPublished </param>
        /// <param name="urlItem"> isPublished </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int siteID,
            int moduleID,
            string title,
            string general,
            string functionP,
            string mission,
            int officerID,
            string achievement,
            string procedureP,
            string contact,
            DateTime createDate,
            int creator,
            DateTime editDate,
            int editor,
            bool? isPublished,
            string urlItem,
            string lichCongTac,
            int orderByUnit,
            int allowUserModify,
            string maKhoaPhong,
            bool? isShowQuestion)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_FunctionalUnit_Update", 22);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 250, ParameterDirection.Input, title);
            sph.DefineSqlParameter("@General", SqlDbType.NText, ParameterDirection.Input, general);
            sph.DefineSqlParameter("@FunctionP", SqlDbType.NText, ParameterDirection.Input, functionP);
            sph.DefineSqlParameter("@Mission", SqlDbType.NText, ParameterDirection.Input, mission);
            sph.DefineSqlParameter("@OfficerID", SqlDbType.Int, ParameterDirection.Input, officerID);
            sph.DefineSqlParameter("@Achievement", SqlDbType.NText, ParameterDirection.Input, achievement);
            sph.DefineSqlParameter("@ProcedureP", SqlDbType.NText, ParameterDirection.Input, procedureP);
            sph.DefineSqlParameter("@Contact", SqlDbType.NText, ParameterDirection.Input, contact);
            sph.DefineSqlParameter("@CreateDate", SqlDbType.DateTime, ParameterDirection.Input, createDate);
            sph.DefineSqlParameter("@Creator", SqlDbType.Int, ParameterDirection.Input, creator);
            sph.DefineSqlParameter("@EditDate", SqlDbType.DateTime, ParameterDirection.Input, editDate);
            sph.DefineSqlParameter("@Editor", SqlDbType.Int, ParameterDirection.Input, editor);
            sph.DefineSqlParameter("@IsPublished", SqlDbType.Bit, ParameterDirection.Input, isPublished);
            sph.DefineSqlParameter("@UrlItem", SqlDbType.NVarChar, 500, ParameterDirection.Input, urlItem);
            sph.DefineSqlParameter("@LichCongTac", SqlDbType.NText, ParameterDirection.Input, lichCongTac);
            sph.DefineSqlParameter("@OrderByUnit", SqlDbType.Int, ParameterDirection.Input, orderByUnit);
            sph.DefineSqlParameter("@AllowUserModify", SqlDbType.Int, ParameterDirection.Input, allowUserModify);
            sph.DefineSqlParameter("@MaKhoaPhong", SqlDbType.NVarChar, 250, ParameterDirection.Input, maKhoaPhong);
            sph.DefineSqlParameter("@IsShowQuestion", SqlDbType.Bit, ParameterDirection.Input, isShowQuestion);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_FunctionalUnit table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_FunctionalUnit_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_FunctionalUnit table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_FunctionalUnit_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_FunctionalUnit table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_FunctionalUnit_GetCount",
                null));

        }


        public static int GetCountManage(string title, DateTime? createDate)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_FunctionalUnit_GetCountManage", 2);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 255, ParameterDirection.Input, title);
            sph.DefineSqlParameter("@CreateDate", SqlDbType.DateTime, ParameterDirection.Input, createDate);
            return Convert.ToInt32(sph.ExecuteScalar());
        }

        public static int GetCountRecenlist(int siteId, int moduleId, string title, DateTime? createDate)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_FunctionalUnit_GetCountRecentList", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 255, ParameterDirection.Input, title);
            sph.DefineSqlParameter("@CreateDate", SqlDbType.DateTime, ParameterDirection.Input, createDate);
            return Convert.ToInt32(sph.ExecuteScalar());
        }


        public static int GetCountFunctionalUnit(string title, DateTime? createDate, int siteId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_FunctionalUnit_GetCount_ForEndUser", 3);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 250, ParameterDirection.Input, title);
            sph.DefineSqlParameter("@CreateDate", SqlDbType.DateTime, ParameterDirection.Input, createDate);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            return Convert.ToInt32(sph.ExecuteScalar());
        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_FunctionalUnit table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_FunctionalUnit_SelectAll",
                null);

        }

        public static IDataReader GetAllPublished()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_FunctionalUnit_SelectAllPublished",
                null);

        }


        /// <summary>
        /// Gets a page of data from the md_FunctionalUnit table.
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_FunctionalUnit_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }




        public static IDataReader GetFunctionalUnit(
            string title,
            DateTime? createDate,
            int siteId,
            int pageNumber,
            int pageSize,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCountFunctionalUnit(title, createDate, siteId);

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
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_FunctionalUnit_SelectPageOfficer", 5);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 250, ParameterDirection.Input, title);
            sph.DefineSqlParameter("@CreateDate", SqlDbType.DateTime, ParameterDirection.Input, createDate);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();
        }

        public static IDataReader GetList(int officerID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_FunctionalIUnit_SelectByOfficer", 1);
            sph.DefineSqlParameter("@OfficerID", SqlDbType.Int, ParameterDirection.Input, officerID);
            return sph.ExecuteReader();
        }

        public static IDataReader GetListByUser(int userId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_FunctionalIUnit_SelectByUser", 1);
            sph.DefineSqlParameter("@UserID", SqlDbType.Int, ParameterDirection.Input, userId);
            return sph.ExecuteReader();
        }

        public static IDataReader GetOrther(int itemId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_FunctionalIUnit_SelectByOrther", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemId);
            return sph.ExecuteReader();
        }

        public static IDataReader GetPageManage(
           string title,
           DateTime? createDate,
            int pageNumber,
           int pageSize,
           out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCountManage(title, createDate);

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
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_FunctionalUnit_SelectPageManage", 4);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 250, ParameterDirection.Input, title);
            sph.DefineSqlParameter("@CreateDate", SqlDbType.DateTime, ParameterDirection.Input, createDate);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }


        public static IDataReader GetPageRecentList(
            int siteId,
            int moduleId,
           string title,
           DateTime? createDate,
            int pageNumber,
           int pageSize,
           out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCountRecenlist(siteId, moduleId, title, createDate);

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
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_FunctionalUnit_SelectPageRecentList", 6);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleId);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 250, ParameterDirection.Input, title);
            sph.DefineSqlParameter("@CreateDate", SqlDbType.DateTime, ParameterDirection.Input, createDate);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }
    }

}



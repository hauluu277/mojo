using mojoPortal.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace QuestionAnswerFeatures.Data
{
    public static class DBQuestionAnswer
    {

        /// <summary>
        /// Inserts a row in the md_QuestionAnswer table. Returns new integer id.
        /// </summary>
        /// <param name="siteID"> siteID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="question"> question </param>
        /// <param name="linhVucID"> linhVucID </param>
        /// <param name="loaiLinhVucID"> loaiLinhVucID </param>
        /// <param name="cityID"> cityID </param>
        /// <param name="districtID"> districtID </param>
        /// <param name="contentQuestion"> contentQuestion </param>
        /// <param name="name"> name </param>
        /// <param name="email"> email </param>
        /// <param name="phone"> phone </param>
        /// <param name="isApprove"> isApprove </param>
        /// <param name="userApprove"> userApprove </param>
        /// <param name="isDelete"> isDelete </param>
        /// <param name="views"> views </param>
        /// <param name="dateApprove"> dateApprove </param>
        /// <param name="createDate"> createDate </param>
        /// <returns>int</returns>
        public static int Create(
            int siteID,
            int pageID,
            int moduleID,
            string question,
            int linhVucID,
            int loaiLinhVucID,
            string contentQuestion,
            string itemUrl,
            string name,
            string email,
            string phone,
            string fts,
            bool isApprove,
            int userApprove,
            bool isDelete,
            int views,
            DateTime dateApprove,
            DateTime createDate,
            int createByUser,
            int? departmentId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_QuestionAnswer_Insert", 20);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@PageID", SqlDbType.Int, ParameterDirection.Input, pageID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@Question", SqlDbType.NVarChar, 350, ParameterDirection.Input, question);
            sph.DefineSqlParameter("@LinhVucID", SqlDbType.Int, ParameterDirection.Input, linhVucID);
            sph.DefineSqlParameter("@LoaiLinhVucID", SqlDbType.Int, ParameterDirection.Input, loaiLinhVucID);
            sph.DefineSqlParameter("@ContentQuestion", SqlDbType.NText, ParameterDirection.Input, contentQuestion);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NText, ParameterDirection.Input, itemUrl);
            sph.DefineSqlParameter("@Name", SqlDbType.NVarChar, 350, ParameterDirection.Input, name);
            sph.DefineSqlParameter("@Email", SqlDbType.NVarChar, 350, ParameterDirection.Input, email);
            sph.DefineSqlParameter("@Phone", SqlDbType.NVarChar, 50, ParameterDirection.Input, phone);
            sph.DefineSqlParameter("@FTS", SqlDbType.NText, ParameterDirection.Input, fts);
            sph.DefineSqlParameter("@IsApprove", SqlDbType.Bit, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@UserApprove", SqlDbType.Int, ParameterDirection.Input, userApprove);
            sph.DefineSqlParameter("@IsDelete", SqlDbType.Bit, ParameterDirection.Input, isDelete);
            sph.DefineSqlParameter("@Views", SqlDbType.Int, ParameterDirection.Input, views);
            sph.DefineSqlParameter("@DateApprove", SqlDbType.DateTime, ParameterDirection.Input, dateApprove);
            sph.DefineSqlParameter("@CreateDate", SqlDbType.DateTime, ParameterDirection.Input, createDate);
            sph.DefineSqlParameter("@CreateByUser", SqlDbType.Int, ParameterDirection.Input, createByUser);
            sph.DefineSqlParameter("@DepartmentId", SqlDbType.Int, ParameterDirection.Input, departmentId);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_QuestionAnswer table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="question"> question </param>
        /// <param name="linhVucID"> linhVucID </param>
        /// <param name="loaiLinhVucID"> loaiLinhVucID </param>
        /// <param name="cityID"> cityID </param>
        /// <param name="districtID"> districtID </param>
        /// <param name="contentQuestion"> contentQuestion </param>
        /// <param name="name"> name </param>
        /// <param name="email"> email </param>
        /// <param name="phone"> phone </param>
        /// <param name="isApprove"> isApprove </param>
        /// <param name="userApprove"> userApprove </param>
        /// <param name="isDelete"> isDelete </param>
        /// <param name="views"> views </param>
        /// <param name="dateApprove"> dateApprove </param>
        /// <param name="createDate"> createDate </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int siteID,
            int pageID,
            int moduleID,
            string question,
            int linhVucID,
            int loaiLinhVucID,
            string contentQuestion,
            string itemUrl,
            string name,
            string email,
            string phone,
            string fts,
            bool isApprove,
            int userApprove,
            bool isDelete,
            int views,
            DateTime dateApprove,
            DateTime createDate,
            int createByUser,
            int? departmentId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_QuestionAnswer_Update", 21);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@PageID", SqlDbType.Int, ParameterDirection.Input, pageID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@Question", SqlDbType.NVarChar, 350, ParameterDirection.Input, question);
            sph.DefineSqlParameter("@LinhVucID", SqlDbType.Int, ParameterDirection.Input, linhVucID);
            sph.DefineSqlParameter("@LoaiLinhVucID", SqlDbType.Int, ParameterDirection.Input, loaiLinhVucID);
            sph.DefineSqlParameter("@ContentQuestion", SqlDbType.NText, ParameterDirection.Input, contentQuestion);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NText, ParameterDirection.Input, itemUrl);
            sph.DefineSqlParameter("@Name", SqlDbType.NVarChar, 350, ParameterDirection.Input, name);
            sph.DefineSqlParameter("@Email", SqlDbType.NVarChar, 350, ParameterDirection.Input, email);
            sph.DefineSqlParameter("@Phone", SqlDbType.NVarChar, 50, ParameterDirection.Input, phone);
            sph.DefineSqlParameter("@FTS", SqlDbType.NText, ParameterDirection.Input, fts);
            sph.DefineSqlParameter("@IsApprove", SqlDbType.Bit, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@UserApprove", SqlDbType.Int, ParameterDirection.Input, userApprove);
            sph.DefineSqlParameter("@IsDelete", SqlDbType.Bit, ParameterDirection.Input, isDelete);
            sph.DefineSqlParameter("@Views", SqlDbType.Int, ParameterDirection.Input, views);
            sph.DefineSqlParameter("@DateApprove", SqlDbType.DateTime, ParameterDirection.Input, dateApprove);
            sph.DefineSqlParameter("@CreateDate", SqlDbType.DateTime, ParameterDirection.Input, createDate);
            sph.DefineSqlParameter("@CreateByUser", SqlDbType.Int, ParameterDirection.Input, createByUser);
            sph.DefineSqlParameter("@DepartmentId", SqlDbType.Int, ParameterDirection.Input, departmentId);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_QuestionAnswer table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_QuestionAnswer_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_QuestionAnswer table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_QuestionAnswer_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }


        public static bool UpdateView(
    int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_QuestionAnswer_UpdateView", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets a count of rows in the md_QuestionAnswer table.
        /// </summary>
        public static int GetCount(int siteID,
            int categoryID,
            int categoryChildID,
            int isApprove,
            int orderBy,
            string keyword,
            int deptId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_QuestionAnswer_GetCount", 7);
            sph.DefineSqlParameter("@DeptId", SqlDbType.Int, ParameterDirection.Input, deptId);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@CategoryChildID", SqlDbType.Int, ParameterDirection.Input, categoryChildID);
            sph.DefineSqlParameter("@IsApprove", SqlDbType.Int, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@OrderBy", SqlDbType.Int, ParameterDirection.Input, orderBy);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, 350, ParameterDirection.Input, keyword);
            return Convert.ToInt32(sph.ExecuteScalar());

        }


        public static int GetCountForUser(int siteID, int userID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_QuestionAnswer_GetCountForUser", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@UserID", SqlDbType.Int, ParameterDirection.Input, userID);
            return Convert.ToInt32(sph.ExecuteScalar());

        }
        public static IDataReader GetTopQuestion(int siteID, int top)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_QuestionAnswer_GetTop", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, top);
            return sph.ExecuteReader();

        }


        /// <summary>
        /// Gets an IDataReader with all rows in the md_QuestionAnswer table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_QuestionAnswer_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the md_QuestionAnswer table.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static IDataReader GetPage(
            int siteID,
            int categoryID,
            int categoryChildID,
            int isApprove,
            int orderBy,
            string keyword,
            int pageNumber,
            int pageSize,
            int deptId,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCount(siteID, categoryID, categoryChildID, isApprove, orderBy, keyword,deptId);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_QuestionAnswer_SelectPage", 9);
            sph.DefineSqlParameter("@DeptId", SqlDbType.Int, ParameterDirection.Input, deptId);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            sph.DefineSqlParameter("@CategoryChildID", SqlDbType.Int, ParameterDirection.Input, categoryChildID);
            sph.DefineSqlParameter("@IsApprove", SqlDbType.Int, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@OrderBy", SqlDbType.Int, ParameterDirection.Input, orderBy);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, 350, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }


        public static IDataReader GetPageForUser(
            int siteID,
            int userID,
            int pageNumber,
            int pageSize,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCountForUser(siteID, userID);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_QuestionAnswer_SelectPageForUser", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@UserID", SqlDbType.Int, ParameterDirection.Input, userID);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }
    }
}

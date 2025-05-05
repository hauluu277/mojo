using mojoPortal.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace QuestionAnswerFeatures.Data
{
    public static class DBAnswer
    {
        /// <summary>
        /// Inserts a row in the md_Answers table. Returns new integer id.
        /// </summary>
        /// <param name="questionID"> questionID </param>
        /// <param name="answerName"> answerName </param>
        /// <param name="name"> name </param>
        /// <param name="email"> email </param>
        /// <param name="isApprove"> isApprove </param>
        /// <param name="createDate"> createDate </param>
        /// <returns>int</returns>
        public static int Create(
            int questionID,
            string answerName,
            string name,
            string email,
            bool isApprove,
            DateTime createDate,
            int userID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Answers_Insert", 7);
            sph.DefineSqlParameter("@QuestionID", SqlDbType.Int, ParameterDirection.Input, questionID);
            sph.DefineSqlParameter("@AnswerName", SqlDbType.NText, ParameterDirection.Input, answerName);
            sph.DefineSqlParameter("@Name", SqlDbType.NVarChar, 350, ParameterDirection.Input, name);
            sph.DefineSqlParameter("@Email", SqlDbType.NVarChar, 350, ParameterDirection.Input, email);
            sph.DefineSqlParameter("@IsApprove", SqlDbType.Bit, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@CreateDate", SqlDbType.DateTime, ParameterDirection.Input, createDate);
            sph.DefineSqlParameter("@UserID", SqlDbType.Int, ParameterDirection.Input, userID);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_Answers table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="questionID"> questionID </param>
        /// <param name="answerName"> answerName </param>
        /// <param name="name"> name </param>
        /// <param name="email"> email </param>
        /// <param name="isApprove"> isApprove </param>
        /// <param name="createDate"> createDate </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int questionID,
            string answerName,
            string name,
            string email,
            bool isApprove,
            DateTime createDate,
            int userID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Answers_Update", 8);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@QuestionID", SqlDbType.Int, ParameterDirection.Input, questionID);
            sph.DefineSqlParameter("@AnswerName", SqlDbType.NText, ParameterDirection.Input, answerName);
            sph.DefineSqlParameter("@Name", SqlDbType.NVarChar, 350, ParameterDirection.Input, name);
            sph.DefineSqlParameter("@Email", SqlDbType.NVarChar, 350, ParameterDirection.Input, email);
            sph.DefineSqlParameter("@IsApprove", SqlDbType.Bit, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@CreateDate", SqlDbType.DateTime, ParameterDirection.Input, createDate);
            sph.DefineSqlParameter("@UserID", SqlDbType.Int, ParameterDirection.Input, userID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_Answers table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Answers_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        public static bool DeleteByQuestion(
    int questionId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Answers_DeleteByQuestion", 1);
            sph.DefineSqlParameter("@Question", SqlDbType.Int, ParameterDirection.Input, questionId);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_Answers table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Answers_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_Answers table.
        /// </summary>
        public static int GetCount(int questionID, int isApprove)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Answers_GetCount", 2);
            sph.DefineSqlParameter("@QuestionID", SqlDbType.Int, ParameterDirection.Input, questionID);
            sph.DefineSqlParameter("@IsApprove", SqlDbType.Int, ParameterDirection.Input, isApprove);
            return Convert.ToInt32(sph.ExecuteScalar());

        }

        public static int GetCountForUser(int userID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Answers_GetCountForUser", 1);
            sph.DefineSqlParameter("@UserID", SqlDbType.Int, ParameterDirection.Input, userID);
            return Convert.ToInt32(sph.ExecuteScalar());

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_Answers table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_Answers_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the md_Answers table.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static IDataReader GetPage(
            int questionID, int isApprove,
            int pageNumber,
            int pageSize,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCount(questionID, isApprove);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Answers_SelectPage", 4);
            sph.DefineSqlParameter("@QuestionID", SqlDbType.Int, ParameterDirection.Input, questionID);
            sph.DefineSqlParameter("@IsApprove", SqlDbType.Int, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }


        public static IDataReader GetPageForUser(int userID,
            int pageNumber,
           int pageSize,
           out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCountForUser(userID);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Answers_SelectPageForUser", 3);
            sph.DefineSqlParameter("@UserID", SqlDbType.Int, ParameterDirection.Input, userID);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }
    }
}

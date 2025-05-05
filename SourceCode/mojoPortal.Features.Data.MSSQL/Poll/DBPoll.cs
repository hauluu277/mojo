/// Author:				Christian Fredh
/// Created:			2007-07-01
/// Last Modified:		2008-11-08
/// 
/// 
/// The use and distribution terms for this software are covered by the 
/// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
/// which can be found in the file CPL.TXT at the root of this distribution.
/// By using this software in any fashion, you are agreeing to be bound by 
/// the terms of this license.
///
/// You must not remove this notice, or any other, from this software.

using System;
using System.Data;
using System.Configuration;
using mojoPortal.Data;

namespace PollFeature.Data
{

    public static class DBPoll
    {

        public static IDataReader GetPolls(Guid siteGuid)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Polls_Select", 1);
            sph.DefineSqlParameter("@SiteGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, siteGuid);
            return sph.ExecuteReader();

        }
        public static IDataReader GetActivePolls(Guid siteGuid)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Polls_SelectActive", 2);
            sph.DefineSqlParameter("@SiteGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, siteGuid);
            sph.DefineSqlParameter("@CurrentTime", SqlDbType.DateTime, ParameterDirection.Input, DateTime.UtcNow);
            return sph.ExecuteReader();
        }

        public static IDataReader GetPollsByUserGuid(Guid userGuid)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Polls_SelectByUserGuid", 1);
            sph.DefineSqlParameter("@UserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, userGuid);
            return sph.ExecuteReader();
        }

        public static int Add(
            Guid pollGuid,
            Guid siteGuid,
            String question,
            bool anonymousVoting,
            bool allowViewingResultsBeforeVoting,
            bool showOrderNumbers,
            bool showResultsWhenDeactivated,
            bool active,
            DateTime activeFrom,
            DateTime activeTo,
            bool isPublish,
            bool isApprove,
            int siteID,
            string comment
            )
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "mp_Polls_Insert", 14);
            sph.DefineSqlParameter("@PollGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, pollGuid);
            sph.DefineSqlParameter("@SiteGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, siteGuid);
            sph.DefineSqlParameter("@Question", SqlDbType.NVarChar, 255, ParameterDirection.Input, question);
            sph.DefineSqlParameter("@AnonymousVoting", SqlDbType.Bit, ParameterDirection.Input, anonymousVoting);
            sph.DefineSqlParameter("@AllowViewingResultsBeforeVoting", SqlDbType.Bit, ParameterDirection.Input, allowViewingResultsBeforeVoting);
            sph.DefineSqlParameter("@ShowOrderNumbers", SqlDbType.Bit, ParameterDirection.Input, showOrderNumbers);
            sph.DefineSqlParameter("@ShowResultsWhenDeactivated", SqlDbType.Bit, ParameterDirection.Input, showResultsWhenDeactivated);
            sph.DefineSqlParameter("@Active", SqlDbType.Bit, ParameterDirection.Input, active);
            sph.DefineSqlParameter("@ActiveFrom", SqlDbType.DateTime, ParameterDirection.Input, activeFrom);
            sph.DefineSqlParameter("@ActiveTo", SqlDbType.DateTime, ParameterDirection.Input, activeTo);
            sph.DefineSqlParameter("@IsPublish", SqlDbType.Bit, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@IsApprove", SqlDbType.Bit, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@Comment", SqlDbType.NVarChar, 500, ParameterDirection.Input, comment);


            int rowsAffected = sph.ExecuteNonQuery();
            return rowsAffected;

        }

        public static bool Update(
            Guid pollGuid,
            String question,
            bool anonymousVoting,
            bool allowViewingResultsBeforeVoting,
            bool showOrderNumbers,
            bool showResultsWhenDeactivated,
            bool active,
            DateTime activeFrom,
            DateTime activeTo,
            bool isPublish,
            bool isApprove,
            int siteID,
            string comment)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "mp_Polls_Update", 13);
            sph.DefineSqlParameter("@PollGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, pollGuid);
            sph.DefineSqlParameter("@Question", SqlDbType.NVarChar, 255, ParameterDirection.Input, question);
            sph.DefineSqlParameter("@AnonymousVoting", SqlDbType.Bit, ParameterDirection.Input, anonymousVoting);
            sph.DefineSqlParameter("@AllowViewingResultsBeforeVoting", SqlDbType.Bit, ParameterDirection.Input, allowViewingResultsBeforeVoting);
            sph.DefineSqlParameter("@ShowOrderNumbers", SqlDbType.Bit, ParameterDirection.Input, showOrderNumbers);
            sph.DefineSqlParameter("@ShowResultsWhenDeactivated", SqlDbType.Bit, ParameterDirection.Input, showResultsWhenDeactivated);
            sph.DefineSqlParameter("@Active", SqlDbType.Bit, ParameterDirection.Input, active);
            sph.DefineSqlParameter("@ActiveFrom", SqlDbType.DateTime, ParameterDirection.Input, activeFrom);
            sph.DefineSqlParameter("@ActiveTo", SqlDbType.DateTime, ParameterDirection.Input, activeTo);
            sph.DefineSqlParameter("@IsPublish", SqlDbType.Bit, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@IsApprove", SqlDbType.Bit, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@Comment", SqlDbType.NVarChar, 500, ParameterDirection.Input, comment);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);


        }

        public static IDataReader GetPoll(Guid pollGuid)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Polls_SelectOne", 1);
            sph.DefineSqlParameter("@PollGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, pollGuid);
            return sph.ExecuteReader();

        }

        public static IDataReader GetPollByModuleID(int moduleID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Polls_SelectOneByModuleID", 1);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            return sph.ExecuteReader();
        }
        public static IDataReader GetPollByPoolGuid(Guid pollGuid)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Polls_SelectByPollGuid", 1);
            sph.DefineSqlParameter("@PollGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, pollGuid);
            return sph.ExecuteReader();
        }

        public static bool ClearVotes(Guid pollGuid)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "mp_Polls_ClearVotes", 1);
            sph.DefineSqlParameter("@PollGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, pollGuid);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        public static bool Delete(Guid pollGuid)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "mp_Polls_Delete", 1);
            sph.DefineSqlParameter("@PollGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, pollGuid);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        public static bool UpdatePublish(Guid pollGuid)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "mp_Polls_UpdatePublish", 1);
            sph.DefineSqlParameter("@PollGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, pollGuid);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }
        public static bool UpdateApprove(Guid pollGuid)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "mp_Polls_UpdateApprove", 1);
            sph.DefineSqlParameter("@PollGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, pollGuid);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);
        }

        public static bool DeleteBySite(int siteId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "mp_Polls_DeleteBySite", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > -1);

        }

        public static bool UserHasVoted(Guid pollGuid, Guid userGuid)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Polls_UserHasVoted", 2);
            sph.DefineSqlParameter("@PollGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, pollGuid);
            sph.DefineSqlParameter("@UserGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, userGuid);
            int userHasVoted = Convert.ToInt32(sph.ExecuteScalar());
            return (userHasVoted == 1);

        }

        public static bool AddToModule(Guid pollGuid, int moduleID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "mp_Polls_AddToModule", 2);
            sph.DefineSqlParameter("@PollGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, pollGuid);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > -1);

        }

        public static bool RemoveFromModule(int moduleID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "mp_Polls_RemoveFromModule", 1);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > -1);

        }

        /// <summary>
        /// Gets a count of rows in the md_DanhBa table.
        /// </summary>
        public static int GetCount(
             int siteId,
            bool? isPublish,
            bool? isApprove,
            string keyword)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Polls_GetCount", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@IsPublish", SqlDbType.Bit, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@IsApprove", SqlDbType.Bit, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@Question", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);
            return Convert.ToInt32(sph.ExecuteScalar());

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public static int GetCountByAll(
     int siteId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Polls_GetCountByAll", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            return Convert.ToInt32(sph.ExecuteScalar());

        }

        /// <summary>
        /// Gets a page of data from the md_DanhBa table.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static IDataReader GetPage(
            int siteID,
            int pageNumber,
            int pageSize,
            bool? isPublish,
            bool? isApprove,
            string keyword,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCount(siteID, isPublish, isApprove, keyword);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Polls_SelectPage", 6);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@IsPublish", SqlDbType.Bit, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@IsApprove", SqlDbType.Bit, ParameterDirection.Input, isApprove);
            sph.DefineSqlParameter("@Question", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);
            return sph.ExecuteReader();
        }
        public static IDataReader GetPageByAll(
           int siteID,
           int pageNumber,
           int pageSize,
           out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCountByAll(siteID);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "mp_Polls_SelectByAll", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();
        }
    }
}

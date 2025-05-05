using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoPortal.Data
{
    public class DBBannerBuilder
    {

        /// <summary>
        /// Deletes a row from the md_Banner table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Banner_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Inserts a row in the md_Banner table. Returns new integer id.
        /// </summary>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="pageID"> pageID </param>
        /// <param name="name"> name </param>
        /// <param name="description"> description </param>
        /// <param name="link"> link </param>
        /// <param name="path"> path </param>
        /// <param name="hitCount"> hitCount </param>
        /// <param name="width"> width </param>
        /// <param name="isImage"> isImage </param>
        /// <param name="number"> number </param>
        /// <param name="isHorizontal"> isHorizontal </param>
        /// <param name="startDate"> startDate </param>
        /// <param name="endDate"> endDate </param>
        /// <param name="isFollow"> isFollow </param>
        /// <param name="isTarget"> isTarget </param>
        /// <param name="createdByUser"> createdByUser </param>
        /// <param name="createdDate"> createdDate </param>
        /// <param name="isPublic"> isPublic </param>
        /// <returns>int</returns>
        public static int Create(
            int moduleID,
            int siteID,
            int pageID,
            string name,
            string description,
            string link,
            string path,
            int hitCount,
            string width,
            bool isImage,
            int number,
            bool isHorizontal,
            DateTime startDate,
            DateTime? endDate,
            bool isFollow,
            bool isTarget,
            string createdByUser,
            DateTime createdDate,
            bool isPublic,
            int priority,
            bool noClick)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Banner_Insert", 21);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@PageID", SqlDbType.Int, ParameterDirection.Input, pageID);
            sph.DefineSqlParameter("@Name", SqlDbType.NVarChar, 255, ParameterDirection.Input, name);
            sph.DefineSqlParameter("@Description", SqlDbType.NText, ParameterDirection.Input, description);
            sph.DefineSqlParameter("@Link", SqlDbType.NVarChar, 255, ParameterDirection.Input, link);
            sph.DefineSqlParameter("@Path", SqlDbType.NVarChar, 255, ParameterDirection.Input, path);
            sph.DefineSqlParameter("@HitCount", SqlDbType.Int, ParameterDirection.Input, hitCount);
            sph.DefineSqlParameter("@Width", SqlDbType.NVarChar, 10, ParameterDirection.Input, width);
            sph.DefineSqlParameter("@IsImage", SqlDbType.Bit, ParameterDirection.Input, isImage);
            sph.DefineSqlParameter("@Number", SqlDbType.Int, ParameterDirection.Input, number);
            sph.DefineSqlParameter("@IsHorizontal", SqlDbType.Bit, ParameterDirection.Input, isHorizontal);
            sph.DefineSqlParameter("@StartDate", SqlDbType.DateTime, ParameterDirection.Input, startDate);
            sph.DefineSqlParameter("@EndDate", SqlDbType.DateTime, ParameterDirection.Input, endDate);
            sph.DefineSqlParameter("@IsFollow", SqlDbType.Bit, ParameterDirection.Input, isFollow);
            sph.DefineSqlParameter("@IsTarget", SqlDbType.Bit, ParameterDirection.Input, isTarget);
            sph.DefineSqlParameter("@CreatedByUser", SqlDbType.NVarChar, 100, ParameterDirection.Input, createdByUser);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@IsPublic", SqlDbType.Bit, ParameterDirection.Input, isPublic);
            sph.DefineSqlParameter("@Priority", SqlDbType.Int, ParameterDirection.Input, priority);
            sph.DefineSqlParameter("@NoClick", SqlDbType.Bit, ParameterDirection.Input, noClick);
            //sph.DefineSqlParameter("@IsSlideTop", SqlDbType.Bit, ParameterDirection.Input, isSlideTop);
            //sph.DefineSqlParameter("@IsSlideBottom", SqlDbType.Bit, ParameterDirection.Input, isSlideBottom);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_Banner table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="pageID"> pageID </param>
        /// <param name="name"> name </param>
        /// <param name="description"> description </param>
        /// <param name="link"> link </param>
        /// <param name="path"> path </param>
        /// <param name="hitCount"> hitCount </param>
        /// <param name="width"> width </param>
        /// <param name="isImage"> isImage </param>
        /// <param name="number"> number </param>
        /// <param name="isHorizontal"> isHorizontal </param>
        /// <param name="startDate"> startDate </param>
        /// <param name="endDate"> endDate </param>
        /// <param name="isFollow"> isFollow </param>
        /// <param name="isTarget"> isTarget </param>
        /// <param name="createdByUser"> createdByUser </param>
        /// <param name="createdDate"> createdDate </param>
        /// <param name="isPublic"> isPublic </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int moduleID,
            int siteID,
            int pageID,
            string name,
            string description,
            string link,
            string path,
            int hitCount,
            string width,
            bool isImage,
            int number,
            bool isHorizontal,
            DateTime startDate,
            DateTime? endDate,
            bool isFollow,
            bool isTarget,
            string createdByUser,
            DateTime createdDate,
            bool isPublic,
            int priority,
            bool noClick)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_Banner_Update", 22);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@PageID", SqlDbType.Int, ParameterDirection.Input, pageID);
            sph.DefineSqlParameter("@Name", SqlDbType.NVarChar, 255, ParameterDirection.Input, name);
            sph.DefineSqlParameter("@Description", SqlDbType.NText, ParameterDirection.Input, description);
            sph.DefineSqlParameter("@Link", SqlDbType.NVarChar, 255, ParameterDirection.Input, link);
            sph.DefineSqlParameter("@Path", SqlDbType.NVarChar, 255, ParameterDirection.Input, path);
            sph.DefineSqlParameter("@HitCount", SqlDbType.Int, ParameterDirection.Input, hitCount);
            sph.DefineSqlParameter("@Width", SqlDbType.NVarChar, 10, ParameterDirection.Input, width);
            sph.DefineSqlParameter("@IsImage", SqlDbType.Bit, ParameterDirection.Input, isImage);
            sph.DefineSqlParameter("@Number", SqlDbType.Int, ParameterDirection.Input, number);
            sph.DefineSqlParameter("@IsHorizontal", SqlDbType.Bit, ParameterDirection.Input, isHorizontal);
            sph.DefineSqlParameter("@StartDate", SqlDbType.DateTime, ParameterDirection.Input, startDate);
            sph.DefineSqlParameter("@EndDate", SqlDbType.DateTime, ParameterDirection.Input, endDate);
            sph.DefineSqlParameter("@IsFollow", SqlDbType.Bit, ParameterDirection.Input, isFollow);
            sph.DefineSqlParameter("@IsTarget", SqlDbType.Bit, ParameterDirection.Input, isTarget);
            sph.DefineSqlParameter("@CreatedByUser", SqlDbType.NVarChar, 100, ParameterDirection.Input, createdByUser);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@IsPublic", SqlDbType.Bit, ParameterDirection.Input, isPublic);
            sph.DefineSqlParameter("@Priority", SqlDbType.Int, ParameterDirection.Input, priority);
            sph.DefineSqlParameter("@NoClick", SqlDbType.Bit, ParameterDirection.Input, noClick);
            //sph.DefineSqlParameter("@IsSlideTop", SqlDbType.Bit, ParameterDirection.Input, isSlideTop);
            //sph.DefineSqlParameter("@IsSlideBottom", SqlDbType.Bit, ParameterDirection.Input, isSlideBottom);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }




        /// <summary>
        /// Gets an IDataReader with all rows in the md_Banner table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_Banner_SelectAll",
                null);

        }
        /// <summary>
        /// Gets an IDataReader with one row from the md_Banner table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Banner_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }

        public static IDataReader GetByModule(
            int moduleID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Banner_SelectByModule", 1);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            return sph.ExecuteReader();

        }
    }
}

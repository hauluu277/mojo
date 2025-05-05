using mojoPortal.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace VideoIntroduceFeatures.Data
{
    public class DBVideoIntroduce
    {
        /// <summary>
        /// Inserts a row in the md_VideoIntroduce table. Returns new integer id.
        /// </summary>
        /// <param name="siteID"> siteID </param>
        /// <param name="pageID"> pageID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="title"> title </param>
        /// <param name="name"> name </param>
        /// <param name="image"> image </param>
        /// <param name="imageVideo"> imageVideo </param>
        /// <param name="contentDetail"> contentDetail </param>
        /// <param name="typeVideo"> typeVideo </param>
        /// <param name="sizeVideo"> sizeVideo </param>
        /// <param name="isPublic"> isPublic </param>
        /// <param name="createBy"> createBy </param>
        /// <param name="createDate"> createDate </param>
        /// <returns>int</returns>
        public static int Create(
            int siteID,
            int pageID,
            int moduleID,
            string title,
            string itemUrl,
            string name,
            string video,
            string youtubeUrl,
            int views,
            string imageVideo,
            string contentDetail,
            string typeVideo,
            int typePlayer,
            double sizeVideo,
            bool isPublic,
            int createBy,
            DateTime createDate,
            bool isHot)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_VideoIntroduce_Insert", 18);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@PageID", SqlDbType.Int, ParameterDirection.Input, pageID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 350, ParameterDirection.Input, title);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NVarChar, 350, ParameterDirection.Input, itemUrl);
            sph.DefineSqlParameter("@Name", SqlDbType.NVarChar, 350, ParameterDirection.Input, name);
            sph.DefineSqlParameter("@Video", SqlDbType.NVarChar, 250, ParameterDirection.Input, video);
            sph.DefineSqlParameter("@YoutubeUrl", SqlDbType.NVarChar, 650, ParameterDirection.Input, youtubeUrl);
            sph.DefineSqlParameter("@Views", SqlDbType.Int, ParameterDirection.Input, views);
            sph.DefineSqlParameter("@ImageVideo", SqlDbType.NVarChar, 250, ParameterDirection.Input, imageVideo);
            sph.DefineSqlParameter("@ContentDetail", SqlDbType.NText, ParameterDirection.Input, contentDetail);
            sph.DefineSqlParameter("@TypeVideo", SqlDbType.NVarChar, 50, ParameterDirection.Input, typeVideo);
            sph.DefineSqlParameter("@TypePlayer", SqlDbType.Int, ParameterDirection.Input, typePlayer);
            sph.DefineSqlParameter("@SizeVideo", SqlDbType.Float, ParameterDirection.Input, sizeVideo);
            sph.DefineSqlParameter("@IsPublic", SqlDbType.Bit, ParameterDirection.Input, isPublic);
            sph.DefineSqlParameter("@CreateBy", SqlDbType.Int, ParameterDirection.Input, createBy);
            sph.DefineSqlParameter("@CreateDate", SqlDbType.DateTime, ParameterDirection.Input, createDate);
            sph.DefineSqlParameter("@IsHot", SqlDbType.Bit, ParameterDirection.Input, isHot);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_VideoIntroduce table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="pageID"> pageID </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="title"> title </param>
        /// <param name="name"> name </param>
        /// <param name="image"> image </param>
        /// <param name="imageVideo"> imageVideo </param>
        /// <param name="contentDetail"> contentDetail </param>
        /// <param name="typeVideo"> typeVideo </param>
        /// <param name="sizeVideo"> sizeVideo </param>
        /// <param name="isPublic"> isPublic </param>
        /// <param name="createBy"> createBy </param>
        /// <param name="createDate"> createDate </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int siteID,
            int pageID,
            int moduleID,
            string title,
            string itemUrl,
            string name,
            string video,
            string youtubeUrl,
            int views,
            string imageVideo,
            string contentDetail,
            string typeVideo,
            int typePlayer,
            double sizeVideo,
            bool isPublic,
            int createBy,
            DateTime createDate,
            bool isHot)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_VideoIntroduce_Update", 19);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@PageID", SqlDbType.Int, ParameterDirection.Input, pageID);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 350, ParameterDirection.Input, title);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NVarChar, 350, ParameterDirection.Input, itemUrl);
            sph.DefineSqlParameter("@Name", SqlDbType.NVarChar, 350, ParameterDirection.Input, name);
            sph.DefineSqlParameter("@Video", SqlDbType.NVarChar, 250, ParameterDirection.Input, video);
            sph.DefineSqlParameter("@YoutubeUrl", SqlDbType.NVarChar, 650, ParameterDirection.Input, youtubeUrl);
            sph.DefineSqlParameter("@Views", SqlDbType.Int, ParameterDirection.Input, views);
            sph.DefineSqlParameter("@ImageVideo", SqlDbType.NVarChar, 250, ParameterDirection.Input, imageVideo);
            sph.DefineSqlParameter("@ContentDetail", SqlDbType.NText, ParameterDirection.Input, contentDetail);
            sph.DefineSqlParameter("@TypeVideo", SqlDbType.NVarChar, 50, ParameterDirection.Input, typeVideo);
            sph.DefineSqlParameter("@TypePlayer", SqlDbType.Int, ParameterDirection.Input, typePlayer);
            sph.DefineSqlParameter("@SizeVideo", SqlDbType.Float, ParameterDirection.Input, sizeVideo);
            sph.DefineSqlParameter("@IsPublic", SqlDbType.Bit, ParameterDirection.Input, isPublic);
            sph.DefineSqlParameter("@CreateBy", SqlDbType.Int, ParameterDirection.Input, createBy);
            sph.DefineSqlParameter("@CreateDate", SqlDbType.DateTime, ParameterDirection.Input, createDate);
            sph.DefineSqlParameter("@IsHot", SqlDbType.Bit, ParameterDirection.Input, isHot);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_VideoIntroduce table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_VideoIntroduce_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_VideoIntroduce table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_VideoIntroduce_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }


        public static IDataReader GetIsHot(int siteID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_VideoIntroduce_GetIsHot", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            return sph.ExecuteReader();


            //return SqlHelper.ExecuteReader(
            //  ConnectionString.GetReadConnectionString(),
            //  CommandType.StoredProcedure,
            //  "md_VideoIntroduce_GetIsHot",
            //  null);

        }

        public static IDataReader GetAllPublic(int siteID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_VideoIntroduce_GetAllPublic", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            return sph.ExecuteReader();
        }


        public static IDataReader GetBySite(int siteID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_VideoIntroduce_GetBySite", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            return sph.ExecuteReader();
        }


        public static IDataReader GetAllVideoPublic(int siteID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_VideoIntroduce_GetAllVideoPublic", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            return sph.ExecuteReader();
        }
        public static IDataReader GetTopOrtherVideo(int siteID, int top)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_VideoIntroduce_GetTopOrtherVideo", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, top);
            return sph.ExecuteReader();
        }

        public static IDataReader GetTopOrtherVideo(int siteID, int top, int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_VideoIntroduce_GetTopOrtherVideo2", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, top);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();
        }

        /// <summary>
        /// Gets a count of rows in the md_VideoIntroduce table.
        /// </summary>
        public static int GetCount(int siteId,
            int isPublic,
            int typePlayer,
            string keyword)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_VideoIntroduce_GetCount", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@IsPublic", SqlDbType.Int, ParameterDirection.Input, isPublic);
            sph.DefineSqlParameter("@TypePlayer", SqlDbType.Int, ParameterDirection.Input, typePlayer);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, 350, ParameterDirection.Input, keyword);
            return Convert.ToInt32(sph.ExecuteScalar());

        }

        public static bool UpdateIsHot(int siteID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_VideoIntroduce_UpdateIsHot", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);
        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_VideoIntroduce table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_VideoIntroduce_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the md_VideoIntroduce table.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static IDataReader GetPage(
            int siteId,
            int isPublic,
            int typePlayer,
            string keyword,
            int pageNumber,
            int pageSize,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCount(siteId, isPublic, typePlayer, keyword);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_VideoIntroduce_SelectPage", 6);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@IsPublic", SqlDbType.Int, ParameterDirection.Input, isPublic);
            sph.DefineSqlParameter("@TypePlayer", SqlDbType.Int, ParameterDirection.Input, typePlayer);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, 350, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }
}

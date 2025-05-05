using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using mojoPortal.Data;

namespace ArticleFeature.Data
{

    public static class DBArticleBO
    {
        public static IDataReader GetArticleTopHotOrther(int ItemId, int Top, int langId, int siteId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectTop_HotOrther", 3);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, ItemId);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, Top);
            sph.DefineSqlParameter("@SiteId", SqlDbType.Int, ParameterDirection.Input, siteId);
            return sph.ExecuteReader();

        }

        public static IDataReader GetArticleHot(int Top, int siteId, int category)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectTopHot", 3);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, Top);
            sph.DefineSqlParameter("@SiteId", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, category);

            return sph.ExecuteReader();

        }

        public static int GetPageCategoryCount(int categoryID, int langId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_Others_SelectPage_ByCategoryID_Count", 1);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            return Convert.ToInt32(sph.ExecuteScalar());
        }

        public static IDataReader GetPageCategory(
            int categoryID,
            int langId,
            int pageSize,
            int pageNumber,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetPageCategoryCount(categoryID, langId);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_Others_SelectPage_ByCategoryID", 3);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, categoryID);
            return sph.ExecuteReader();

        }
        public static IDataReader GetArticleTopOrther(
           int CategoryID, int langId, int ItemId, int Top, bool Hot)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectTopOrther", 4);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, CategoryID);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, ItemId);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, Top);
            sph.DefineSqlParameter("@Hot", SqlDbType.Bit, ParameterDirection.Input, Hot);
            return sph.ExecuteReader();

        }
        public static IDataReader GetArticleTop(
           int CategoryID, int Top, int langId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_Articles_SelectTop", 2);
            sph.DefineSqlParameter("@CategoryID", SqlDbType.Int, ParameterDirection.Input, CategoryID);
            sph.DefineSqlParameter("@Top", SqlDbType.Int, ParameterDirection.Input, Top);
            return sph.ExecuteReader();
        }
    }
}

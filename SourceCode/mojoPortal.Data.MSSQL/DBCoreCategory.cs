// Author:					HiNet JSC
// Created:					2014-6-27
// Last Modified:			2014-6-27
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

    public static class DBCoreCategory
    {


        /// <summary>
        /// Inserts a row in the core_Category table. Returns new integer id.
        /// </summary>
        /// <param name="parentID"> parentID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="name"> name </param>
        /// <param name="description"> description </param>
        /// <param name="itemCount"> itemCount </param>
        /// <param name="createdUtc"> createdUtc </param>
        /// <param name="createdBy"> createdBy </param>
        /// <param name="modifiedUtc"> modifiedUtc </param>
        /// <param name="modifiedBy"> modifiedBy </param>
        /// <returns>int</returns>

        public static int Create(
            int parentID,
            int siteID,
            string name,
            string nameen,
            string description,
            int itemCount,
            DateTime createdUtc,
            Guid createdBy,
            DateTime modifiedUtc,
            Guid modifiedBy,
            int priority,
            int iconId,
            bool automatic,
            int coreSkinID,
            bool coreSkinDefault,
            bool isPhongBan,
            bool showMenuLeft,
            string pathIMG,
            string pathFile,
            string subName,
            bool isLinhVucDieuTra,
            bool isTinTuc,
            string code,
            string Sumary,
            bool targetBlank,
            string color,
            bool showCategoryChild
            )
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_Category_Insert", 27);
            sph.DefineSqlParameter("@ParentID", SqlDbType.Int, ParameterDirection.Input, parentID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@Name", SqlDbType.NVarChar, -1, ParameterDirection.Input, name);
            sph.DefineSqlParameter("@NameEN", SqlDbType.NVarChar, 255, ParameterDirection.Input, nameen);
            sph.DefineSqlParameter("@Description", SqlDbType.NVarChar, -1, ParameterDirection.Input, description);
            sph.DefineSqlParameter("@ItemCount", SqlDbType.Int, ParameterDirection.Input, itemCount);
            sph.DefineSqlParameter("@CreatedUtc", SqlDbType.DateTime, ParameterDirection.Input, createdUtc);
            sph.DefineSqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier, ParameterDirection.Input, createdBy);
            sph.DefineSqlParameter("@ModifiedUtc", SqlDbType.DateTime, ParameterDirection.Input, modifiedUtc);
            sph.DefineSqlParameter("@ModifiedBy", SqlDbType.UniqueIdentifier, ParameterDirection.Input, modifiedBy);
            sph.DefineSqlParameter("@Priority", SqlDbType.Int, ParameterDirection.Input, priority);
            sph.DefineSqlParameter("@IconID", SqlDbType.Int, ParameterDirection.Input, iconId);
            sph.DefineSqlParameter("@Automatic", SqlDbType.Bit, ParameterDirection.Input, automatic);
            sph.DefineSqlParameter("@CoreSkinID", SqlDbType.Int, ParameterDirection.Input, coreSkinID);
            sph.DefineSqlParameter("@CoreSkinDefault", SqlDbType.Bit, ParameterDirection.Input, coreSkinDefault);
            sph.DefineSqlParameter("@IsPhongBan", SqlDbType.Bit, ParameterDirection.Input, isPhongBan);
            sph.DefineSqlParameter("@ShowMenuLeft", SqlDbType.Bit, ParameterDirection.Input, showMenuLeft);
            sph.DefineSqlParameter("@PathIMG", SqlDbType.NVarChar, -1, ParameterDirection.Input, pathIMG);
            sph.DefineSqlParameter("@PathFile", SqlDbType.NVarChar, -1, ParameterDirection.Input, pathFile);
            sph.DefineSqlParameter("@SubName", SqlDbType.NVarChar, -1, ParameterDirection.Input, subName);
            sph.DefineSqlParameter("@IsLinhVucDieuTra", SqlDbType.Bit, ParameterDirection.Input, isLinhVucDieuTra);
            sph.DefineSqlParameter("@IsTinTuc", SqlDbType.Bit, ParameterDirection.Input, isTinTuc);
            sph.DefineSqlParameter("@Code", SqlDbType.NVarChar, 250, ParameterDirection.Input, code);
            sph.DefineSqlParameter("@Sumary", SqlDbType.NVarChar, 550, ParameterDirection.Input, Sumary);
            sph.DefineSqlParameter("@TargetBlank", SqlDbType.Bit, ParameterDirection.Input, targetBlank);
            sph.DefineSqlParameter("@Color", SqlDbType.NVarChar, 250, ParameterDirection.Input, color);
            sph.DefineSqlParameter("@ShowCategoryChild", SqlDbType.Bit, ParameterDirection.Input, showCategoryChild);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        public static IDataReader GetCoreSkinDefault(
                            int siteID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_SelectCoreSkinDefault", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            return sph.ExecuteReader();


        }

        public static IDataReader GetBySkin(
                          int skinID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_SelectBySkin", 1);
            sph.DefineSqlParameter("@CoreSkinID", SqlDbType.Int, ParameterDirection.Input, skinID);
            return sph.ExecuteReader();

        }
        public static IDataReader GetBySite(
                        int siteID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_SelectBySite", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            return sph.ExecuteReader();

        }


        public static IDataReader GetListParent(int itemId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_CategorySelectListParent", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemId);
            return sph.ExecuteReader();
        }

        /// <summary>
        /// Updates a row in the core_Category table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="parentID"> parentID </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="name"> name </param>
        /// <param name="description"> description </param>
        /// <param name="itemCount"> itemCount </param>
        /// <param name="createdUtc"> createdUtc </param>
        /// <param name="createdBy"> createdBy </param>
        /// <param name="modifiedUtc"> modifiedUtc </param>
        /// <param name="modifiedBy"> modifiedBy </param>
        /// <returns>bool</returns>
        public static bool Update(
            int itemID,
            int parentID,
            int siteID,
            string name,
            string nameen,
            string description,
            int itemCount,
            DateTime createdUtc,
            Guid createdBy,
            DateTime modifiedUtc,
            Guid modifiedBy,
            int priority,
            int iconId,
            bool automatic,
            int coreSkinID,
            bool coreSkinDefault,
            bool isPhongBan,
            bool showMenuLeft,
            string pathIMG,
            string pathFile,
            string subName,
            bool isLinhVucDieuTra,
            bool isTinTuc,
            string code,
            string Sumary,
            bool targetBlank,
            string color,
            bool showCategoryChild)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_Category_Update", 28);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@ParentID", SqlDbType.Int, ParameterDirection.Input, parentID);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@Name", SqlDbType.NVarChar, 255, ParameterDirection.Input, name);
            sph.DefineSqlParameter("@NameEN", SqlDbType.NVarChar, 255, ParameterDirection.Input, nameen);
            sph.DefineSqlParameter("@Description", SqlDbType.NVarChar, -1, ParameterDirection.Input, description);
            sph.DefineSqlParameter("@ItemCount", SqlDbType.Int, ParameterDirection.Input, itemCount);
            sph.DefineSqlParameter("@CreatedUtc", SqlDbType.DateTime, ParameterDirection.Input, createdUtc);
            sph.DefineSqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier, ParameterDirection.Input, createdBy);
            sph.DefineSqlParameter("@ModifiedUtc", SqlDbType.DateTime, ParameterDirection.Input, modifiedUtc);
            sph.DefineSqlParameter("@ModifiedBy", SqlDbType.UniqueIdentifier, ParameterDirection.Input, modifiedBy);
            sph.DefineSqlParameter("@Priority", SqlDbType.Int, ParameterDirection.Input, priority);
            sph.DefineSqlParameter("@IconID", SqlDbType.Int, ParameterDirection.Input, iconId);
            sph.DefineSqlParameter("@Automatic", SqlDbType.Bit, ParameterDirection.Input, automatic);
            sph.DefineSqlParameter("@CoreSkinID", SqlDbType.Int, ParameterDirection.Input, coreSkinID);
            sph.DefineSqlParameter("@CoreSkinDefault", SqlDbType.Bit, ParameterDirection.Input, coreSkinDefault);
            sph.DefineSqlParameter("@IsPhongBan", SqlDbType.Bit, ParameterDirection.Input, isPhongBan);
            sph.DefineSqlParameter("@ShowMenuLeft", SqlDbType.Bit, ParameterDirection.Input, showMenuLeft);
            sph.DefineSqlParameter("@PathIMG", SqlDbType.NVarChar, -1, ParameterDirection.Input, pathIMG);
            sph.DefineSqlParameter("@PathFile", SqlDbType.NVarChar, -1, ParameterDirection.Input, pathFile);
            sph.DefineSqlParameter("@SubName", SqlDbType.NVarChar, -1, ParameterDirection.Input, subName);
            sph.DefineSqlParameter("@IsLinhVucDieuTra", SqlDbType.Bit, ParameterDirection.Input, isLinhVucDieuTra);
            sph.DefineSqlParameter("@IsTinTuc", SqlDbType.Bit, ParameterDirection.Input, isTinTuc);
            sph.DefineSqlParameter("@Code", SqlDbType.NVarChar, 250, ParameterDirection.Input, code);
            sph.DefineSqlParameter("@Sumary", SqlDbType.NVarChar, 550, ParameterDirection.Input, Sumary);
            sph.DefineSqlParameter("@TargetBlank", SqlDbType.Bit, ParameterDirection.Input, targetBlank);
            sph.DefineSqlParameter("@Color", SqlDbType.NVarChar, 250, ParameterDirection.Input, color);
            sph.DefineSqlParameter("@ShowCategoryChild", SqlDbType.Bit, ParameterDirection.Input, showCategoryChild);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }


        public static bool UpdateMultiple(int siteId, string arrItemId, bool isTinTuc, bool isLinhVucDieuTra)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_Category_UpdateMultiple", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ArrItemId", SqlDbType.NVarChar, -1, ParameterDirection.Input, arrItemId);
            sph.DefineSqlParameter("@IsTinTuc", SqlDbType.Bit, ParameterDirection.Input, isTinTuc);
            sph.DefineSqlParameter("@IsLinhVucDieuTra", SqlDbType.Bit, ParameterDirection.Input, isLinhVucDieuTra);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }


        /// <summary>
        /// Deletes a row from the core_Category table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_Category_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);
        }

        public static IDataReader GetOneParent(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_SelectOneParent", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }


        public static IDataReader GetCategoryArticleBySite(int siteId, string categories)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_SelectCategoryBySite", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@Categories", SqlDbType.NVarChar, 500, ParameterDirection.Input, categories);
            return sph.ExecuteReader();

        }



        /// <summary>
        /// Gets an IDataReader with one row from the core_Category table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_SelectOne2", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();
        }



        public static IDataReader GetByCode(int siteId, string code)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_SelectByCode", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@Code", SqlDbType.NVarChar, 250, ParameterDirection.Input, code);
            return sph.ExecuteReader();
        }


        public static IDataReader GetByUrl(string url, int siteId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_SelectByUrl", 2);
            sph.DefineSqlParameter("@Url", SqlDbType.NVarChar, 255, ParameterDirection.Input, url);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            return sph.ExecuteReader();
        }

        public static IDataReader GetByUrlForArticle(string url, int siteId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_SelectByUrlForArticle", 2);
            sph.DefineSqlParameter("@Url", SqlDbType.NVarChar, 255, ParameterDirection.Input, url);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            return sph.ExecuteReader();
        }
        public static IDataReader GetByUrlForLinhVuc(string url, int siteId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_SelectByUrlForLinhVuc", 2);
            sph.DefineSqlParameter("@Url", SqlDbType.NVarChar, 255, ParameterDirection.Input, url);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            return sph.ExecuteReader();
        }


        public static IDataReader GetAllChild(int parentId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_GetAllChild", 1);
            sph.DefineSqlParameter("@ParentID", SqlDbType.Int, ParameterDirection.Input, parentId);
            return sph.ExecuteReader();
        }



        public static bool DeleteBySkin(
       int coreSkinID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_Category_DeleteBySkin", 1);
            sph.DefineSqlParameter("@CoreSkinID", SqlDbType.Int, ParameterDirection.Input, coreSkinID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);
        }

        /// <summary>
        /// Gets a count of rows in the core_Category table.
        /// </summary>
        public static int GetCount(int siteId, string keyword, string childList)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_GetCount", 3);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@ChildList", SqlDbType.NVarChar, 250, ParameterDirection.Input, childList);
            return int.Parse(sph.ExecuteScalar().ToString());
        }

        /// <summary>
        /// Gets an IDataReader with all rows in the core_Category table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "core_Category_SelectAll",
                null);

        }

        public static IDataReader GetAll(int moduleID, int number, int type)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_SelectAll_ByModuleID_Type", 3);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@Number", SqlDbType.Int, ParameterDirection.Input, number);
            sph.DefineSqlParameter("@Type", SqlDbType.Int, ParameterDirection.Input, type);
            return sph.ExecuteReader();
        }

        /// <summary>
        /// Gets a page of data from the core_Category table.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static IDataReader GetPage(
            int siteId,
            int pageNumber,
            int pageSize,
            string keyword,
            string childList,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCount(siteId, keyword, childList);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_SelectPage", 5);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@ChildList", SqlDbType.NVarChar, 500, ParameterDirection.Input, childList);
            return sph.ExecuteReader();

        }
        public static int GetCountArticle(int siteId, string keyword, int parentid, string parents)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_ArticleGetCount", 4);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@Parentid", SqlDbType.Int, ParameterDirection.Input, parentid);
            sph.DefineSqlParameter("@Parents", SqlDbType.VarChar, 250, ParameterDirection.Input, parents);
            return int.Parse(sph.ExecuteScalar().ToString());
        }
        public static IDataReader GetPageArticle(
        int siteId,
        int pageNumber,
        int pageSize,
        string keyword,
        int parentid,
        string parents,
        out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCountArticle(siteId, keyword, parentid, parents);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_ArticleSelectPage", 6);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);
            sph.DefineSqlParameter("@ParentID", SqlDbType.Int, ParameterDirection.Input, parentid);
            sph.DefineSqlParameter("@Parents", SqlDbType.VarChar, 250, ParameterDirection.Input, parents);
            return sph.ExecuteReader();

        }


        public static IDataReader GetCoreSkinDefaultRoot(int siteID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_SelectCoreSkinDefault", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            return sph.ExecuteReader();
        }
        public static IDataReader GetParents(int itemId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_SelectParent", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemId);
            return sph.ExecuteReader();
        }
        public static IDataReader GetRoot(int siteID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_SelectRoot", 1);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            return sph.ExecuteReader();
        }

        public static IDataReader GetAllRoot()
        {
            return SqlHelper.ExecuteReader(
    ConnectionString.GetReadConnectionString(),
    CommandType.StoredProcedure,
    "core_Category_SelectAllRoot",
null);
        }

        public static IDataReader GetRootByCat(int siteID, int catID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_SelectRoot_ByCat", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@CatID", SqlDbType.Int, ParameterDirection.Input, catID);
            return sph.ExecuteReader();
        }

        public static IDataReader GetRootByCatShort(int siteID, int catID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_SelectRoot_ByCatShort", 2);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@CatID", SqlDbType.Int, ParameterDirection.Input, catID);
            return sph.ExecuteReader();
        }

        public static IDataReader GetChildren(int categoryId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_SelectChildren", 1);
            sph.DefineSqlParameter("@CategoryId", SqlDbType.Int, ParameterDirection.Input, categoryId);
            return sph.ExecuteReader();
        }

        public static IDataReader GetChildrenTotalArticle(int categoryId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_SelectChildrenTotalArticle", 1);
            sph.DefineSqlParameter("@CategoryId", SqlDbType.Int, ParameterDirection.Input, categoryId);
            return sph.ExecuteReader();
        }


        public static IDataReader GetListChildren(int parentID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_SelectListChildren", 1);
            sph.DefineSqlParameter("@ParentID", SqlDbType.Int, ParameterDirection.Input, parentID);
            return sph.ExecuteReader();
        }



        public static IDataReader GetListChildrenAll(string parent)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_SelectListChildrenAll", 1);
            sph.DefineSqlParameter("@Parent", SqlDbType.NVarChar, 250, ParameterDirection.Input, parent);
            return sph.ExecuteReader();
        }

        public static IDataReader GetChildren(int siteId, string code)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_SelectChildrenByCode", 2);
            sph.DefineSqlParameter("@SiteId", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@Code", SqlDbType.NVarChar, 250, ParameterDirection.Input, code);
            return sph.ExecuteReader();
        }

        public static IDataReader GetChildren(int siteId, int categoryId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_SelectChildrenBySite", 2);
            sph.DefineSqlParameter("@SiteId", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@CategoryId", SqlDbType.Int, ParameterDirection.Input, categoryId);
            return sph.ExecuteReader();
        }
        public static IDataReader GetChildrenByParent(int itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_SelectOneParent", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.Int, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();
        }


        public static IDataReader GetByIcon(int iconId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_Category_SelectByIcon", 1);
            sph.DefineSqlParameter("@IconID", SqlDbType.Int, ParameterDirection.Input, iconId);
            return sph.ExecuteReader();
        }
    }

}



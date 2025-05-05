
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
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using mojoportal.CoreHelpers;
using mojoPortal.Data;

namespace mojoPortal.Business
{

    public class CoreCategory
    {

        #region Constructors

        public CoreCategory()
        { }


        public CoreCategory(
            int itemID)
        {
            GetCoreCategory(
                itemID);
        }

        public CoreCategory(string url, int siteId)
        {
            GetByUrl(url, siteId);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int parentID = -1;
        private int siteID = -1;
        private string name = string.Empty;
        private string nameen = string.Empty;
        private string parentName = string.Empty;
        private string description = string.Empty;
        private int itemCount = -1;
        private DateTime createdUtc = DateTime.UtcNow;
        private Guid createdBy = Guid.Empty;
        private DateTime modifiedUtc = DateTime.UtcNow;
        private Guid modifiedBy = Guid.Empty;
        private int priority = 0;
        private int iconId = -1;
        private string iconUrl = string.Empty;
        private bool automatic = false;
        private int coreSkinID = -1;
        private bool coreSkinDefault = false;
        private bool isPhongBan = false;
        private bool showMenuLeft = false;
        private string pathIMG = string.Empty;
        private string pathFile = string.Empty;
        private string subName = string.Empty;
        private bool isLinhVucDieuTra = false;
        private bool isTinTuc = false;
        private string code = string.Empty;
        private string sumary = string.Empty;
        private bool targetBlank = false;
        private string color = string.Empty;
        private int totalArticle = 0;
        private bool showCategoryChild = false;
        #endregion

        #region Public Properties
        public bool ShowCategoryChild
        {
            get { return showCategoryChild; }
            set { showCategoryChild = value; }
        }
        public int TotalArticle
        {
            get { return totalArticle; }
            set { totalArticle = value; }
        }
        public string Color
        {
            get { return color; }
            set { color = value; }
        }
        public bool TargetBlank
        {
            get { return targetBlank; }
            set { targetBlank = value; }
        }
        public string Sumary
        {
            get { return sumary; }
            set { sumary = value; }
        }
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        public bool IsTinTuc
        {
            get { return isTinTuc; }
            set { isTinTuc = value; }
        }
        public bool IsLinhVucDieuTra
        {
            get { return isLinhVucDieuTra; }
            set { isLinhVucDieuTra = value; }
        }
        public string SubName
        {
            get { return subName; }
            set { subName = value; }
        }
        // hihi
        //public string PhanQuyen
        //{
        //    get { return PhanQuyen; }
        //    set { PhanQuyen = value; }
        //}

        public string PathFile
        {
            get { return pathFile; }
            set { pathFile = value; }
        }
        public string PathIMG
        {
            get { return pathIMG; }
            set { pathIMG = value; }
        }
        public bool ShowMenuLeft { get { return showMenuLeft; } set { showMenuLeft = value; } }
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public int ParentID
        {
            get { return parentID; }
            set { parentID = value; }
        }
        public int SiteID
        {
            get { return siteID; }
            set { siteID = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string NameEN
        {
            get { return nameen; }
            set { nameen = value; }
        }
        public string ParentName
        {
            get { return parentName; }
            set { parentName = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public int ItemCount
        {
            get { return itemCount; }
            set { itemCount = value; }
        }
        public DateTime CreatedUtc
        {
            get { return createdUtc; }
            set { createdUtc = value; }
        }
        public Guid CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }
        public DateTime ModifiedUtc
        {
            get { return modifiedUtc; }
            set { modifiedUtc = value; }
        }
        public Guid ModifiedBy
        {
            get { return modifiedBy; }
            set { modifiedBy = value; }
        }
        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }
        public int IconID
        {
            get { return iconId; }
            set { iconId = value; }
        }
        public string IconUrl
        {
            get { return iconUrl; }
            set { iconUrl = value; }
        }
        public bool Automatic
        {
            get { return automatic; }
            set { automatic = value; }
        }

        public int CoreSkinID
        {
            get { return coreSkinID; }
            set { coreSkinID = value; }
        }

        public bool CoreSkinDefault
        {
            get { return coreSkinDefault; }
            set { coreSkinDefault = value; }
        }
        public bool IsPhongBan
        {
            get { return isPhongBan; }
            set { isPhongBan = value; }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of CoreCategory.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetCoreCategory(
            int itemID)
        {
            using (IDataReader reader = DBCoreCategory.GetOne(
                itemID))
            {
                PopulateFromReader(reader, true);
            }

        }
        private void GetByUrl(string url, int siteID)
        {
            using (IDataReader reader = DBCoreCategory.GetByUrl(url, siteID))
            {
                PopulateFromReader(reader);
            }
        }


        private void PopulateFromReader(IDataReader reader, bool hasParent = false)
        {
            if (reader.Read())
            {
                this.itemID = Convert.ToInt32(reader["ItemID"]);
                this.parentID = Convert.ToInt32(reader["ParentID"]);
                this.siteID = Convert.ToInt32(reader["SiteID"]);
                this.name = reader["Name"].ToString();
                this.nameen = reader["NameEN"].ToString();
                if (hasParent)
                {
                    this.parentName = reader["ParentName"].ToString();
                }




                this.description = reader["Description"].ToString();
                this.itemCount = Convert.ToInt32(reader["ItemCount"]);
                this.createdUtc = Convert.ToDateTime(reader["CreatedUtc"]);
                this.createdBy = new Guid(reader["CreatedBy"].ToString());
                this.modifiedUtc = Convert.ToDateTime(reader["ModifiedUtc"]);
                this.modifiedBy = new Guid(reader["ModifiedBy"].ToString());
                this.priority = Convert.ToInt32(reader["Priority"]);
                if (reader["IconID"] != DBNull.Value)
                {
                    this.iconId = Convert.ToInt32(reader["IconID"]);

                }
                if (reader["ShowCategoryChild"] != DBNull.Value)
                {
                    this.showCategoryChild = Convert.ToBoolean(reader["ShowCategoryChild"]);

                }


                if (!String.IsNullOrEmpty(reader["Automatic"].ToString()))
                {
                    this.automatic = Convert.ToBoolean(reader["Automatic"].ToString());
                }
                if (!string.IsNullOrEmpty(reader["CoreSkinID"].ToString()))
                {
                    this.coreSkinID = Convert.ToInt32(reader["CoreSkinID"].ToString());
                }

                if (!string.IsNullOrEmpty(reader["CoreSkinDefault"].ToString()))
                {
                    this.coreSkinDefault = Convert.ToBoolean(reader["CoreSkinDefault"]);
                }
                if (!string.IsNullOrEmpty(reader["IsPhongBan"].ToString()))
                {
                    this.isPhongBan = Convert.ToBoolean(reader["IsPhongBan"]);
                }
                if (!string.IsNullOrEmpty(reader["ShowMenuLeft"].ToString()))
                {
                    this.showMenuLeft = Convert.ToBoolean(reader["ShowMenuLeft"]);
                }
                if (!string.IsNullOrEmpty(reader["PathIMG"].ToString()))
                {
                    this.pathIMG = reader["PathIMG"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["SubName"].ToString()))
                {
                    this.subName = reader["SubName"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["PathFile"].ToString()))
                {
                    this.pathFile = reader["PathFile"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["IsTinTuc"].ToString()))
                {
                    this.isTinTuc = Convert.ToBoolean(reader["IsTinTuc"].ToString());
                }
                if (!string.IsNullOrEmpty(reader["IsLinhVucDieuTra"].ToString()))
                {
                    this.isLinhVucDieuTra = Convert.ToBoolean(reader["IsLinhVucDieuTra"].ToString());
                }
                this.code = GenericData<string>.GetDataOrDefault(reader["Code"], this.code);
                this.sumary = GenericData<string>.GetDataOrDefault(reader["Sumary"], this.sumary);
                this.targetBlank = GenericData<bool>.GetDataOrDefault(reader["TargetBlank"], this.targetBlank);
                this.color = GenericData<string>.GetDataOrDefault(reader["Color"], this.color);
            }

        }

        /// <summary>
        /// Persists a new instance of CoreCategory. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBCoreCategory.Create(
                this.parentID,
                this.siteID,
                this.name,
                this.nameen,
                this.description,
                this.itemCount,
                this.createdUtc,
                this.createdBy,
                this.modifiedUtc,
                this.modifiedBy,
                this.priority,
                this.iconId,
                this.automatic,
                this.coreSkinID,
                this.coreSkinDefault,
                this.isPhongBan,
                this.showMenuLeft,
                this.pathIMG,
                this.pathFile,
                this.subName,
                this.isLinhVucDieuTra,
                this.isTinTuc,
                this.code,
                this.sumary,
                this.targetBlank,
                this.color,
                this.showCategoryChild);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of CoreCategory. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBCoreCategory.Update(
                this.itemID,
                this.parentID,
                this.siteID,
                this.name,
                this.nameen,
                this.description,
                this.itemCount,
                this.createdUtc,
                this.createdBy,
                this.modifiedUtc,
                this.modifiedBy,
                this.priority,
                this.iconId,
                this.automatic,
                this.coreSkinID,
                this.coreSkinDefault,
                this.isPhongBan,
                this.showMenuLeft,
                this.pathIMG,
                this.pathFile,
                this.subName,
                this.isLinhVucDieuTra,
                this.isTinTuc,
                this.code,
                this.sumary,
                this.targetBlank,
                this.color,
                this.showCategoryChild);

        }





        #endregion

        #region Public Methods
        private static CoreCategory LoadOneReader(IDataReader reader, bool hasParent = false)
        {
            CoreCategory result = new CoreCategory();
            if (reader.Read())
            {
                result.itemID = Convert.ToInt32(reader["ItemID"]);
                result.parentID = Convert.ToInt32(reader["ParentID"]);
                result.siteID = Convert.ToInt32(reader["SiteID"]);
                result.name = reader["Name"].ToString();
                result.nameen = reader["NameEN"].ToString();
                if (hasParent)
                {
                    result.parentName = reader["ParentName"].ToString();
                }

                result.description = reader["Description"].ToString();
                result.itemCount = Convert.ToInt32(reader["ItemCount"]);
                result.createdUtc = Convert.ToDateTime(reader["CreatedUtc"]);
                result.createdBy = new Guid(reader["CreatedBy"].ToString());
                result.modifiedUtc = Convert.ToDateTime(reader["ModifiedUtc"]);
                result.modifiedBy = new Guid(reader["ModifiedBy"].ToString());
                result.priority = Convert.ToInt32(reader["Priority"]);
                if (reader["IconID"] != DBNull.Value)
                {
                    result.iconId = Convert.ToInt32(reader["IconID"]);

                }
                if (reader["ShowCategoryChild"] != DBNull.Value)
                {
                    result.showCategoryChild = Convert.ToBoolean(reader["ShowCategoryChild"]);

                }
                if (!String.IsNullOrEmpty(reader["Automatic"].ToString()))
                {
                    result.automatic = Convert.ToBoolean(reader["Automatic"].ToString());
                }
                if (!string.IsNullOrEmpty(reader["CoreSkinID"].ToString()))
                {
                    result.coreSkinID = Convert.ToInt32(reader["CoreSkinID"].ToString());
                }

                if (!string.IsNullOrEmpty(reader["CoreSkinDefault"].ToString()))
                {
                    result.coreSkinDefault = Convert.ToBoolean(reader["CoreSkinDefault"]);
                }
                if (!string.IsNullOrEmpty(reader["IsPhongBan"].ToString()))
                {
                    result.isPhongBan = Convert.ToBoolean(reader["IsPhongBan"]);
                }
                if (!string.IsNullOrEmpty(reader["ShowMenuLeft"].ToString()))
                {
                    result.showMenuLeft = Convert.ToBoolean(reader["ShowMenuLeft"]);
                }
                if (!string.IsNullOrEmpty(reader["PathIMG"].ToString()))
                {
                    result.pathIMG = reader["PathIMG"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["SubName"].ToString()))
                {
                    result.subName = reader["SubName"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["PathFile"].ToString()))
                {
                    result.pathFile = reader["PathFile"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["IsTinTuc"].ToString()))
                {
                    result.isTinTuc = Convert.ToBoolean(reader["IsTinTuc"].ToString());
                }
                if (!string.IsNullOrEmpty(reader["IsLinhVucDieuTra"].ToString()))
                {
                    result.isLinhVucDieuTra = Convert.ToBoolean(reader["IsLinhVucDieuTra"].ToString());
                }
                result.code = GenericData<string>.GetDataOrDefault(reader["Code"], result.code);
                result.sumary = GenericData<string>.GetDataOrDefault(reader["Sumary"], result.sumary);
                result.targetBlank = GenericData<bool>.GetDataOrDefault(reader["TargetBlank"], result.targetBlank);
                result.color = GenericData<string>.GetDataOrDefault(reader["Color"], result.color);
            }
            return result;
        }
        /// <summary>
        /// Saves this instance of CoreCategory. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        public bool Save()
        {
            if (this.itemID > 0)
            {
                return Update();
            }
            else
            {
                return Create();
            }
        }




        #endregion

        #region Static Methods

        /// <summary>
        /// Deletes an instance of CoreCategory. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBCoreCategory.Delete(
                itemID);
        }
        public static bool DeleteCoreSkin(
          int coreSkinID)
        {
            return DBCoreCategory.DeleteBySkin(
                coreSkinID);
        }

        public static CoreCategory GetUrlForArticle(int siteId, string url)
        {
            return LoadOneReader(DBCoreCategory.GetByUrlForArticle(url, siteId));
        }

        public static CoreCategory GetUrlForLinhVuc(int siteId, string url)
        {
            return LoadOneReader(DBCoreCategory.GetByUrlForLinhVuc(url, siteId));
        }



        /// <summary>
        /// Gets a count of CoreCategory. 
        /// </summary>
        public static int GetCount(int siteId, string keyword, string listChild)
        {
            return DBCoreCategory.GetCount(siteId, keyword, listChild);
        }

        public static IDataReader GetCategoryArticleBySite(int siteId, string categories)
        {
            return DBCoreCategory.GetCategoryArticleBySite(siteId, categories);
        }

        private static List<CoreCategory> LoadListFromReader(IDataReader reader, bool getTotalArticle = false)
        {
            List<CoreCategory> categoryList = new List<CoreCategory>();
            try
            {
                while (reader.Read())
                {
                    CoreCategory category = new CoreCategory();
                    category.itemID = Convert.ToInt32(reader["ItemID"]);
                    if (reader["IconID"] != DBNull.Value)
                    {
                        category.iconId = Convert.ToInt32(reader["IconID"]);
                    }
                    category.parentID = Convert.ToInt32(reader["ParentID"]);
                    category.siteID = Convert.ToInt32(reader["SiteID"]);
                    category.name = reader["Name"].ToString();
                    category.nameen = reader["NameEN"].ToString();
                    category.parentName = reader["ParentName"].ToString();
                    category.description = reader["Description"].ToString();
                    category.itemCount = Convert.ToInt32(reader["ItemCount"]);
                    category.createdUtc = Convert.ToDateTime(reader["CreatedUtc"]);
                    category.createdBy = new Guid(reader["CreatedBy"].ToString());
                    category.modifiedUtc = Convert.ToDateTime(reader["ModifiedUtc"]);
                    category.modifiedBy = new Guid(reader["ModifiedBy"].ToString());
                    category.priority = Convert.ToInt32(reader["Priority"]);

                    if (getTotalArticle)
                    {
                        if (reader["TotalArticle"] != DBNull.Value)
                        {
                            category.totalArticle = Convert.ToInt32(reader["TotalArticle"].ToString());
                        }
                    }
                    if (reader["ShowCategoryChild"] != DBNull.Value)
                    {
                        category.showCategoryChild = Convert.ToBoolean(reader["ShowCategoryChild"]);

                    }

                    if (!String.IsNullOrEmpty(reader["Automatic"].ToString()))
                    {
                        category.automatic = Convert.ToBoolean(reader["Automatic"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["CoreSkinID"].ToString()))
                    {
                        category.coreSkinID = Convert.ToInt32(reader["CoreSkinID"].ToString());
                    }

                    if (!string.IsNullOrEmpty(reader["CoreSkinDefault"].ToString()))
                    {
                        category.coreSkinDefault = Convert.ToBoolean(reader["CoreSkinDefault"]);
                    }
                    if (!string.IsNullOrEmpty(reader["IsPhongBan"].ToString()))
                    {
                        category.isPhongBan = Convert.ToBoolean(reader["IsPhongBan"]);
                    }
                    if (!string.IsNullOrEmpty(reader["ShowMenuLeft"].ToString()))
                    {
                        category.showMenuLeft = Convert.ToBoolean(reader["ShowMenuLeft"]);
                    }
                    if (!string.IsNullOrEmpty(reader["PathFile"].ToString()))
                    {
                        category.pathFile = reader["PathFile"].ToString();
                    }

                    if (!string.IsNullOrEmpty(reader["PathIMG"].ToString()))
                    {
                        category.pathIMG = reader["PathIMG"].ToString();
                    }

                    if (!string.IsNullOrEmpty(reader["SubName"].ToString()))
                    {
                        category.subName = reader["SubName"].ToString();
                    }
                    category.code = GenericData<string>.GetDataOrDefault(reader["Code"], category.code);
                    category.sumary = GenericData<string>.GetDataOrDefault(reader["Sumary"], category.sumary);
                    category.targetBlank = GenericData<bool>.GetDataOrDefault(reader["TargetBlank"], category.targetBlank);
                    category.color = GenericData<string>.GetDataOrDefault(reader["Color"], category.color);
                    categoryList.Add(category);

                }
            }
            finally
            {
                reader.Close();
            }

            return categoryList;

        }
        private static List<CoreCategory> LoadListFromReader2(IDataReader reader, bool getTotalArticle = false)
        {
            List<CoreCategory> categoryList = new List<CoreCategory>();
            try
            {
                while (reader.Read())
                {
                    CoreCategory category = new CoreCategory();
                    category.itemID = Convert.ToInt32(reader["ItemID"]);
                    category.parentID = Convert.ToInt32(reader["ParentID"]);
                    category.siteID = Convert.ToInt32(reader["SiteID"]);
                    category.name = reader["Name"].ToString();
                    category.nameen = reader["NameEN"].ToString();
                    category.parentName = reader["ParentName"].ToString();
                    category.description = reader["Description"].ToString();
                    category.itemCount = Convert.ToInt32(reader["ItemCount"]);
                    category.createdUtc = Convert.ToDateTime(reader["CreatedUtc"]);
                    category.createdBy = new Guid(reader["CreatedBy"].ToString());
                    category.modifiedUtc = Convert.ToDateTime(reader["ModifiedUtc"]);
                    category.modifiedBy = new Guid(reader["ModifiedBy"].ToString());
                    category.priority = Convert.ToInt32(reader["Priority"]);
                    if (getTotalArticle)
                    {
                        if (reader["TotalArticle"] != DBNull.Value)
                        {
                            category.totalArticle = Convert.ToInt32(reader["TotalArticle"].ToString());
                        }
                    }
                    if (reader["ShowCategoryChild"] != DBNull.Value)
                    {
                        category.showCategoryChild = Convert.ToBoolean(reader["ShowCategoryChild"]);

                    }
                    if (!String.IsNullOrEmpty(reader["Automatic"].ToString()))
                    {
                        category.automatic = Convert.ToBoolean(reader["Automatic"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["CoreSkinID"].ToString()))
                    {
                        category.coreSkinID = Convert.ToInt32(reader["CoreSkinID"].ToString());
                    }

                    if (!string.IsNullOrEmpty(reader["CoreSkinDefault"].ToString()))
                    {
                        category.coreSkinDefault = Convert.ToBoolean(reader["CoreSkinDefault"]);
                    }
                    if (!string.IsNullOrEmpty(reader["IsPhongBan"].ToString()))
                    {
                        category.isPhongBan = Convert.ToBoolean(reader["IsPhongBan"]);
                    }
                    if (!string.IsNullOrEmpty(reader["ShowMenuLeft"].ToString()))
                    {
                        category.showMenuLeft = Convert.ToBoolean(reader["ShowMenuLeft"]);
                    }
                    if (!string.IsNullOrEmpty(reader["PathIMG"].ToString()))
                    {
                        category.pathIMG = reader["PathIMG"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["PathFile"].ToString()))
                    {
                        category.pathFile = reader["PathFile"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["SubName"].ToString()))
                    {
                        category.subName = reader["SubName"].ToString();
                    }
                    category.code = GenericData<string>.GetDataOrDefault(reader["Code"], category.code);
                    category.sumary = GenericData<string>.GetDataOrDefault(reader["Sumary"], category.sumary);
                    category.targetBlank = GenericData<bool>.GetDataOrDefault(reader["TargetBlank"], category.targetBlank);
                    category.color = GenericData<string>.GetDataOrDefault(reader["Color"], category.color);
                    categoryList.Add(category);

                }
            }
            finally
            {
                reader.Close();
            }

            return categoryList;

        }
        private static List<CoreCategory> LoadListFromReader1(IDataReader reader)
        {
            List<CoreCategory> categoryList = new List<CoreCategory>();
            try
            {
                while (reader.Read())
                {
                    CoreCategory category = new CoreCategory();
                    if (reader["IconID"] != DBNull.Value)
                    {
                        category.iconId = Convert.ToInt32(reader["IconID"]);
                    }
                    if (reader["IconUrl"] != DBNull.Value)
                    {
                        category.iconUrl = reader["IconUrl"].ToString();
                    }
                    category.itemID = Convert.ToInt32(reader["ItemID"]);
                    category.parentID = Convert.ToInt32(reader["ParentID"]);
                    category.siteID = Convert.ToInt32(reader["SiteID"]);
                    category.name = reader["Name"].ToString();
                    category.nameen = reader["NameEN"].ToString();
                    category.parentName = reader["ParentName"].ToString();
                    category.description = reader["Description"].ToString();
                    category.itemCount = Convert.ToInt32(reader["ItemCount"]);
                    category.createdUtc = Convert.ToDateTime(reader["CreatedUtc"]);
                    category.createdBy = new Guid(reader["CreatedBy"].ToString());
                    category.modifiedUtc = Convert.ToDateTime(reader["ModifiedUtc"]);
                    category.modifiedBy = new Guid(reader["ModifiedBy"].ToString());
                    category.priority = Convert.ToInt32(reader["Priority"]);
                    if (!String.IsNullOrEmpty(reader["Automatic"].ToString()))
                    {
                        category.automatic = Convert.ToBoolean(reader["Automatic"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["CoreSkinID"].ToString()))
                    {
                        category.coreSkinID = Convert.ToInt32(reader["CoreSkinID"].ToString());
                    }

                    if (!string.IsNullOrEmpty(reader["CoreSkinDefault"].ToString()))
                    {
                        category.coreSkinDefault = Convert.ToBoolean(reader["CoreSkinDefault"]);
                    }
                    if (!string.IsNullOrEmpty(reader["ShowMenuLeft"].ToString()))
                    {
                        category.showMenuLeft = Convert.ToBoolean(reader["ShowMenuLeft"]);
                    }
                    if (!string.IsNullOrEmpty(reader["PathFile"].ToString()))
                    {
                        category.pathFile = reader["PathFile"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["SubName"].ToString()))
                    {
                        category.subName = reader["SubName"].ToString();
                    }
                    if (reader["ShowCategoryChild"] != DBNull.Value)
                    {
                        category.showCategoryChild = Convert.ToBoolean(reader["ShowCategoryChild"]);

                    }
                    category.code = GenericData<string>.GetDataOrDefault(reader["Code"], category.code);
                    category.sumary = GenericData<string>.GetDataOrDefault(reader["Sumary"], category.sumary);
                    category.targetBlank = GenericData<bool>.GetDataOrDefault(reader["TargetBlank"], category.targetBlank);
                    category.color = GenericData<string>.GetDataOrDefault(reader["Color"], category.color);
                    categoryList.Add(category);

                }
            }
            finally
            {
                reader.Close();
            }

            return categoryList;

        }
        public static List<CoreCategory> GetPageArticle(int siteId, int pageNumber, int pageSize, string keyword, int parentid, string parents, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBCoreCategory.GetPageArticle(siteId, pageNumber, pageSize, keyword, parentid, parents, out totalPages);
            return LoadListFromReader1(reader);
        }
        public static List<int> GetListParent(int itemID)
        {
            IDataReader reader = DBCoreCategory.GetListParent(itemID);
            List<int> result = new List<int>();
            while (reader.Read())
            {
                if (!string.IsNullOrEmpty(reader["ParentID"].ToString()))
                {
                    result.Add(Convert.ToInt32(reader["ParentID"].ToString()));
                }
            }
            return result;
        }

        public static List<int> GetListChild(int parentId)
        {
            IDataReader reader = DBCoreCategory.GetAllChild(parentId);
            var result = new List<int>();
            while (reader.Read())
            {
                if (!string.IsNullOrEmpty(reader["ItemID"].ToString()))
                {
                    result.Add(Convert.ToInt32(reader["ItemID"].ToString()));
                }
            }
            return result;
        }

        public static string GetParent(int itemID)
        {
            List<int> listItem = new List<int>();
            IDataReader reader = DBCoreCategory.GetParents(itemID);
            try
            {
                while (reader.Read())
                {
                    if (!string.IsNullOrEmpty(reader["ParentID"].ToString()))
                    {
                        listItem.Add(Convert.ToInt32(reader["ParentID"].ToString()));
                    }
                }
            }
            finally
            {
                reader.Close();
            }
            var result = listItem.Distinct().ToArray();
            return string.Join(",", result);
        }

        public static bool UpdateMultiple(int siteId, string arrItemId, bool isTinTuc, bool isLinhVucDieuTra)
        {
            return DBCoreCategory.UpdateMultiple(siteId, arrItemId, isTinTuc, isLinhVucDieuTra);
        }
        public static CoreCategory GetByCode(int siteId, string code)
        {
            return LoadOneReader(DBCoreCategory.GetByCode(siteId, code));
        }
        public static List<CoreCategory> GetListChildAll(string parent)
        {
            List<CoreCategory> result = new List<CoreCategory>();
            IDataReader reader = DBCoreCategory.GetListChildrenAll(parent);
            try
            {
                while (reader.Read())
                {
                    var category = new CoreCategory();
                    category.itemID = Convert.ToInt32(reader["ItemID"]);
                    category.name = reader["Name"].ToString();
                    category.parentID = Convert.ToInt32(reader["ParentID"]);
                    category.description = reader["Description"].ToString();
                    result.Add(category);
                }
            }
            finally
            {
                reader.Close();
            }
            return result;
        }


        public static List<int> GetListChildrenID(int parentID)
        {
            List<int> listItem = new List<int>();
            IDataReader reader = DBCoreCategory.GetListChildren(parentID);
            try
            {
                while (reader.Read())
                {
                    if (!string.IsNullOrEmpty(reader["ItemID"].ToString()))
                    {
                        listItem.Add(Convert.ToInt32(reader["ItemID"].ToString()));
                    }
                }
            }
            finally
            {

            }
            return listItem;
        }

        public static string GetListChildren(int parentID)
        {
            List<int> listItem = new List<int>();
            IDataReader reader = DBCoreCategory.GetListChildren(parentID);
            try
            {
                while (reader.Read())
                {
                    if (!string.IsNullOrEmpty(reader["ItemID"].ToString()))
                    {
                        listItem.Add(Convert.ToInt32(reader["ItemID"].ToString()));
                    }
                }
            }
            finally
            {

            }
            var result = listItem.Distinct().ToArray();
            return string.Join(",", result);
        }


        /// <summary>
        /// Gets an IList with all instances of CoreCategory.
        /// </summary>
        public static List<CoreCategory> GetAll()
        {
            IDataReader reader = DBCoreCategory.GetAll();
            return LoadListFromReader(reader);

        }
        public static List<CoreCategory> GetCoreSkinDefault(int siteID)
        {
            IDataReader reader = DBCoreCategory.GetCoreSkinDefault(siteID);
            return LoadListFromReader(reader);
        }

        public static List<CoreCategory> GetBySkin(int coreSkinID)
        {
            IDataReader reader = DBCoreCategory.GetBySkin(coreSkinID);
            return LoadListFromReader(reader);
        }
        public static List<CoreCategory> GetBySite(int siteID)
        {
            IDataReader reader = DBCoreCategory.GetBySite(siteID);
            return LoadListFromReader(reader);
        }

        public static List<CoreCategory> GetAll(int moduleID, int number, int type)
        {
            IDataReader reader = DBCoreCategory.GetAll();
            return LoadListFromReader(reader);

        }


        /// <summary>
        /// Gets an IList with page of instances of CoreCategory.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<CoreCategory> GetPage(int siteId, int pageNumber, int pageSize, string keyword, string childList, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBCoreCategory.GetPage(siteId, pageNumber, pageSize, keyword, childList, out totalPages);
            return LoadListFromReader1(reader);
        }
        public static List<CoreCategory> GetAllRoot()
        {
            IDataReader reader = DBCoreCategory.GetAllRoot();
            return LoadListFromReader2(reader);
        }

        public static List<CoreCategory> GetRoot(int siteID)
        {
            IDataReader reader = DBCoreCategory.GetRoot(siteID);
            return LoadListFromReader2(reader);
        }

        public static List<CoreCategory> GetCoreSkinDefaultRoot(int siteID)
        {
            IDataReader reader = DBCoreCategory.GetCoreSkinDefaultRoot(siteID);
            return LoadListFromReader2(reader);
        }

        public static List<CoreCategory> GetRootByCat(int siteID, int catID)
        {
            IDataReader reader = DBCoreCategory.GetRootByCat(siteID, catID);
            return LoadListFromReader(reader);
        }

        public static List<CoreCategory> GetChildrenTotalArticle(int categoryId)
        {
            IDataReader reader = DBCoreCategory.GetChildrenTotalArticle(categoryId);
            return LoadListFromReader2(reader, true);
        }

        public static List<CoreCategory> GetChildren(int parentId)
        {
            IDataReader reader = DBCoreCategory.GetChildren(parentId);
            return LoadListFromReader2(reader);
        }

        public static List<CoreCategory> GetChildren(int siteId, string code)
        {
            IDataReader reader = DBCoreCategory.GetChildren(siteId, code);
            return LoadListFromReader2(reader);
        }
        public static List<CoreCategory> GetChildren(int siteId, int categoryId)
        {
            IDataReader reader = DBCoreCategory.GetChildren(siteId, categoryId);
            return LoadListFromReader2(reader);
        }
        public static List<CoreCategory> GetChildrenByParent(int parentID)
        {
            IDataReader reader = DBCoreCategory.GetChildrenByParent(parentID);
            return LoadListFromReader2(reader);
        }
        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of CoreCategory.
        /// </summary>
        public static int CompareByItemID(CoreCategory category1, CoreCategory category2)
        {
            return category1.ItemID.CompareTo(category2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of CoreCategory.
        /// </summary>
        public static int CompareByParentID(CoreCategory category1, CoreCategory category2)
        {
            return category1.ParentID.CompareTo(category2.ParentID);
        }

        public static int CompareByPriority(CoreCategory category1, CoreCategory category2)
        {
            return category1.Priority.CompareTo(category2.Priority);
        }
        /// <summary>
        /// Compares 2 instances of CoreCategory.
        /// </summary>
        public static int CompareBySiteID(CoreCategory category1, CoreCategory category2)
        {
            return category1.SiteID.CompareTo(category2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of CoreCategory.
        /// </summary>
        public static int CompareByName(CoreCategory category1, CoreCategory category2)
        {
            return category1.Name.CompareTo(category2.Name);
        }
        /// <summary>
        /// Compares 2 instances of CoreCategory.
        /// </summary>
        public static int CompareByDescription(CoreCategory category1, CoreCategory category2)
        {
            return category1.Description.CompareTo(category2.Description);
        }
        /// <summary>
        /// Compares 2 instances of CoreCategory.
        /// </summary>
        public static int CompareByItemCount(CoreCategory category1, CoreCategory category2)
        {
            return category1.ItemCount.CompareTo(category2.ItemCount);
        }
        /// <summary>
        /// Compares 2 instances of CoreCategory.
        /// </summary>
        public static int CompareByCreatedUtc(CoreCategory category1, CoreCategory category2)
        {
            return category1.CreatedUtc.CompareTo(category2.CreatedUtc);
        }
        /// <summary>
        /// Compares 2 instances of CoreCategory.
        /// </summary>
        public static int CompareByModifiedUtc(CoreCategory category1, CoreCategory category2)
        {
            return category1.ModifiedUtc.CompareTo(category2.ModifiedUtc);
        }

        #endregion


    }

}







// Author:					Mr Hậu
// Created:					2020-3-13
// Last Modified:			2020-3-13
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
using mojoportal.CoreHelpers;
using mojoPortal.Data;

namespace mojoPortal.Business
{

    public class coreMenu
    {

        #region Constructors

        public coreMenu()
        { }


        public coreMenu(
            int itemID)
        {
            Getcore_Menu(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = 0;
        private int siteID = 0;
        private int parentID = 0;
        private string name = string.Empty;
        private string linkMenu = string.Empty;
        private string imageUrl = string.Empty;
        private int orderBy = 0;
        private DateTime createdDate = DateTime.UtcNow;
        private int createdBy = -1;
        private DateTime updatedDate = DateTime.UtcNow;
        private int updateBy = -1;
        private string styleCss = string.Empty;
        private int typeMenu = 1;
        private bool? show = false;
        private bool isDisplayLink = false;
        private bool isPhongBan = false;
        private bool isEnglish = false;
        private int? typeLink = 0;
        private long? itemLink = 0;
        private int oldID = 0;
        private bool isLogin = false;
        private bool noClick = false;
        private bool targetBlank = false;
        #endregion

        #region Public Properties
        public bool TargetBlank
        {
            get { return targetBlank; }
            set { targetBlank = value; }
        }
        public bool NoClick
        {
            get { return noClick; }
            set { noClick = value; }
        }
        public bool IsLogin
        {
            get { return isLogin; }
            set { isLogin = value; }
        }
        private int OldID
        {
            get { return oldID; }
            set { oldID = value; }
        }
        public int? TypeLink { get { return typeLink; } set { typeLink = value; } }
        public long? ItemLink { get { return itemLink; } set { itemLink = value; } }
        public bool IsEnglish { get { return isEnglish; } set { isEnglish = value; } }
        public bool IsPhongBan { get { return isPhongBan; } set { isPhongBan = value; } }
        public bool IsDisplayLink { get { return isDisplayLink; } set { isDisplayLink = value; } }
        public string StyleCss { get { return styleCss; } set { styleCss = value; } }
        public int TypeMenu { get { return typeMenu; } set { typeMenu = value; } }

        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public int SiteID
        {
            get { return siteID; }
            set { siteID = value; }
        }
        public int ParentID
        {
            get { return parentID; }
            set { parentID = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string LinkMenu
        {
            get { return linkMenu; }
            set { linkMenu = value; }
        }
        public string ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; }
        }
        public int OrderBy
        {
            get { return orderBy; }
            set { orderBy = value; }
        }
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }
        public int CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }
        public DateTime UpdatedDate
        {
            get { return updatedDate; }
            set { updatedDate = value; }
        }
        public int UpdateBy
        {
            get { return updateBy; }
            set { updateBy = value; }
        }
        public bool? Show
        {
            get { return show; }
            set { show = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of core_Menu.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void Getcore_Menu(
            int itemID)
        {
            using (IDataReader reader = DBcore_Menu.GetOne(
                itemID))
            {
                PopulateFromReader(reader);
            }

        }


        private void PopulateFromReader(IDataReader reader)
        {
            if (reader.Read())
            {
                this.itemID = Convert.ToInt32(reader["ItemID"]);
                this.siteID = Convert.ToInt32(reader["SiteID"]);
                this.parentID = Convert.ToInt32(reader["ParentID"]);
                this.name = reader["Name"].ToString();
                this.linkMenu = reader["LinkMenu"].ToString();
                this.imageUrl = reader["ImageUrl"].ToString();
                this.orderBy = Convert.ToInt32(reader["OrderBy"]);
                this.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                this.createdBy = Convert.ToInt32(reader["CreatedBy"]);
                this.updatedDate = Convert.ToDateTime(reader["UpdatedDate"]);
                this.updateBy = Convert.ToInt32(reader["UpdateBy"]);
                if (!string.IsNullOrEmpty(reader["StyleCss"].ToString()))
                {
                    this.styleCss = reader["StyleCss"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["TypeMenu"].ToString()))
                {
                    this.typeMenu = Convert.ToInt32(reader["TypeMenu"]);
                }
                if (!string.IsNullOrEmpty(reader["Show"].ToString()))
                {
                    this.show = Convert.ToBoolean(reader["Show"]);
                }
                if (!string.IsNullOrEmpty(reader["IsDisplayLink"].ToString()))
                {
                    this.isDisplayLink = Convert.ToBoolean(reader["IsDisplayLink"]);
                }
                if (!string.IsNullOrEmpty(reader["IsPhongBan"].ToString()))
                {
                    this.isPhongBan = Convert.ToBoolean(reader["IsPhongBan"]);
                }
                if (!string.IsNullOrEmpty(reader["IsEnglish"].ToString()))
                {
                    this.isEnglish = Convert.ToBoolean(reader["IsEnglish"]);
                }
                if (!string.IsNullOrEmpty(reader["TypeLink"].ToString()))
                {
                    this.typeLink = Convert.ToInt32(reader["TypeLink"]);
                }
                if (!string.IsNullOrEmpty(reader["ItemLink"].ToString()))
                {
                    this.itemLink = Convert.ToInt64(reader["ItemLink"]);
                }
                this.isLogin = GenericData<bool>.GetDataOrDefault(reader["IsLogin"], isLogin);
                this.noClick = GenericData<bool>.GetDataOrDefault(reader["NoClick"], noClick);
                this.targetBlank = GenericData<bool>.GetDataOrDefault(reader["TargetBlank"], targetBlank);
            }

        }

        /// <summary>
        /// Persists a new instance of core_Menu. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBcore_Menu.Create(
                this.siteID,
                this.parentID,
                this.name,
                this.linkMenu,
                this.imageUrl,
                this.orderBy,
                this.createdDate,
                this.createdBy,
                this.updatedDate,
                this.updateBy,
                this.styleCss,
                this.typeMenu,
                this.show,
                this.isDisplayLink,
                this.isPhongBan,
                this.isEnglish,
                this.typeLink,
                this.itemLink,
                this.isLogin,
                this.noClick,
                this.targetBlank);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of core_Menu. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBcore_Menu.Update(
                this.itemID,
                this.siteID,
                this.parentID,
                this.name,
                this.linkMenu,
                this.imageUrl,
                this.orderBy,
                this.createdDate,
                this.createdBy,
                this.updatedDate,
                this.updateBy,
                this.styleCss,
                this.typeMenu,
                this.show,
                this.isDisplayLink,
                this.isPhongBan,
                this.isEnglish,
                this.typeLink,
                this.itemLink,
                this.isLogin,
                noClick,
                this.targetBlank);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of core_Menu. Returns true on success.
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
        /// Deletes an instance of core_Menu. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBcore_Menu.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of core_Menu. 
        /// </summary>
        public static int GetCount()
        {
            return DBcore_Menu.GetCount();
        }

        private static List<coreMenu> LoadListFromReader(IDataReader reader)
        {
            List<coreMenu> core_MenuList = new List<coreMenu>();
            try
            {
                while (reader.Read())
                {
                    coreMenu core_Menu = new coreMenu();
                    core_Menu.itemID = Convert.ToInt32(reader["ItemID"]);
                    core_Menu.siteID = Convert.ToInt32(reader["SiteID"]);
                    core_Menu.parentID = Convert.ToInt32(reader["ParentID"]);
                    core_Menu.name = reader["Name"].ToString();
                    core_Menu.linkMenu = reader["LinkMenu"].ToString();
                    core_Menu.imageUrl = reader["ImageUrl"].ToString();
                    core_Menu.orderBy = Convert.ToInt32(reader["OrderBy"]);
                    core_Menu.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    core_Menu.createdBy = Convert.ToInt32(reader["CreatedBy"]);
                    core_Menu.updatedDate = Convert.ToDateTime(reader["UpdatedDate"]);
                    core_Menu.updateBy = Convert.ToInt32(reader["UpdateBy"]);
                    if (!string.IsNullOrEmpty(reader["StyleCss"].ToString()))
                    {
                        core_Menu.styleCss = reader["StyleCss"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["TypeMenu"].ToString()))
                    {
                        core_Menu.typeMenu = Convert.ToInt32(reader["TypeMenu"]);
                    }
                    if (!string.IsNullOrEmpty(reader["Show"].ToString()))
                    {
                        core_Menu.show = Convert.ToBoolean(reader["Show"]);
                    }
                    if (!string.IsNullOrEmpty(reader["IsDisplayLink"].ToString()))
                    {
                        core_Menu.isDisplayLink = Convert.ToBoolean(reader["IsDisplayLink"]);
                    }
                    if (!string.IsNullOrEmpty(reader["IsPhongBan"].ToString()))
                    {
                        core_Menu.isPhongBan = Convert.ToBoolean(reader["IsPhongBan"]);
                    }
                    if (!string.IsNullOrEmpty(reader["IsEnglish"].ToString()))
                    {
                        core_Menu.isEnglish = Convert.ToBoolean(reader["IsEnglish"]);
                    }
                    if (!string.IsNullOrEmpty(reader["TypeLink"].ToString()))
                    {
                        core_Menu.typeLink = Convert.ToInt32(reader["TypeLink"]);
                    }
                    if (!string.IsNullOrEmpty(reader["ItemLink"].ToString()))
                    {
                        core_Menu.itemLink = Convert.ToInt64(reader["ItemLink"]);
                    }
                    core_Menu.isLogin = GenericData<bool>.GetDataOrDefault(reader["IsLogin"], core_Menu.isLogin);
                    core_Menu.noClick = GenericData<bool>.GetDataOrDefault(reader["NoClick"], core_Menu.noClick);
                    core_Menu.targetBlank = GenericData<bool>.GetDataOrDefault(reader["TargetBlank"], core_Menu.targetBlank);
                    core_MenuList.Add(core_Menu);

                }
            }
            finally
            {
                reader.Close();
            }

            return core_MenuList;

        }

        /// <summary>
        /// Gets an IList with all instances of core_Menu.
        /// </summary>
        public static List<coreMenu> GetAll()
        {
            IDataReader reader = DBcore_Menu.GetAll();
            return LoadListFromReader(reader);
        }

        public static List<coreMenu> GetBySite(int siteID)
        {
            IDataReader reader = DBcore_Menu.GetBySite(siteID);
            return LoadListFromReader(reader);
        }

        public static List<coreMenu> GetRoot(int siteId, int typeMenu, bool? isEnglish)
        {
            IDataReader reader = DBcore_Menu.GetRoot(siteId, typeMenu, isEnglish);
            return LoadListFromReader(reader);
        }

        public static List<coreMenu> GetByParent(int parentId, bool isEnglish, bool? isShow)
        {
            IDataReader reader = DBcore_Menu.GetByParent(parentId, isEnglish, isShow);
            return LoadListFromReader(reader);
        }


        public static List<coreMenu> GetParentRoot(int siteId, int typeMenu, bool? isEnglish)
        {
            IDataReader reader = DBcore_Menu.GetParentRoot(siteId, typeMenu, isEnglish);
            return LoadListFromReader(reader);
        }


        /// <summary>
        /// Gets an IList with page of instances of core_Menu.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<coreMenu> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBcore_Menu.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of core_Menu.
        /// </summary>
        public static int CompareByItemID(coreMenu core_Menu1, coreMenu core_Menu2)
        {
            return core_Menu1.ItemID.CompareTo(core_Menu2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of core_Menu.
        /// </summary>
        public static int CompareBySiteID(coreMenu core_Menu1, coreMenu core_Menu2)
        {
            return core_Menu1.SiteID.CompareTo(core_Menu2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of core_Menu.
        /// </summary>
        public static int CompareByParentID(coreMenu core_Menu1, coreMenu core_Menu2)
        {
            return core_Menu1.ParentID.CompareTo(core_Menu2.ParentID);
        }
        /// <summary>
        /// Compares 2 instances of core_Menu.
        /// </summary>
        public static int CompareByName(coreMenu core_Menu1, coreMenu core_Menu2)
        {
            return core_Menu1.Name.CompareTo(core_Menu2.Name);
        }
        /// <summary>
        /// Compares 2 instances of core_Menu.
        /// </summary>
        public static int CompareByLinkMenu(coreMenu core_Menu1, coreMenu core_Menu2)
        {
            return core_Menu1.LinkMenu.CompareTo(core_Menu2.LinkMenu);
        }
        /// <summary>
        /// Compares 2 instances of core_Menu.
        /// </summary>
        public static int CompareByImageUrl(coreMenu core_Menu1, coreMenu core_Menu2)
        {
            return core_Menu1.ImageUrl.CompareTo(core_Menu2.ImageUrl);
        }
        /// <summary>
        /// Compares 2 instances of core_Menu.
        /// </summary>
        public static int CompareByOrderBy(coreMenu core_Menu1, coreMenu core_Menu2)
        {
            return core_Menu1.OrderBy.CompareTo(core_Menu2.OrderBy);
        }
        /// <summary>
        /// Compares 2 instances of core_Menu.
        /// </summary>
        public static int CompareByCreatedDate(coreMenu core_Menu1, coreMenu core_Menu2)
        {
            return core_Menu1.CreatedDate.CompareTo(core_Menu2.CreatedDate);
        }
        /// <summary>
        /// Compares 2 instances of core_Menu.
        /// </summary>
        public static int CompareByCreatedBy(coreMenu core_Menu1, coreMenu core_Menu2)
        {
            return core_Menu1.CreatedBy.CompareTo(core_Menu2.CreatedBy);
        }
        /// <summary>
        /// Compares 2 instances of core_Menu.
        /// </summary>
        public static int CompareByUpdatedDate(coreMenu core_Menu1, coreMenu core_Menu2)
        {
            return core_Menu1.UpdatedDate.CompareTo(core_Menu2.UpdatedDate);
        }
        /// <summary>
        /// Compares 2 instances of core_Menu.
        /// </summary>
        public static int CompareByUpdateBy(coreMenu core_Menu1, coreMenu core_Menu2)
        {
            return core_Menu1.UpdateBy.CompareTo(core_Menu2.UpdateBy);
        }

        #endregion
    }
    public class coreMenuConvert
    {
        public coreMenuConvert()
        {

        }
        public int IdNew { get; set; }
        public int IdOld { get; set; }
        public int IdParentId { get; set; }
        public int typeMenu { get; set; }
    }

}







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
using System.Collections;
using System.Collections.Generic;
using System.Data;
using mojoPortal.Data;

namespace mojoPortal.Business
{

    public class md_FunctionalUnit
    {

        #region Constructors

        public md_FunctionalUnit()
        { }


        public md_FunctionalUnit(
            int itemID)
        {
            Getmd_FunctionalUnit(
                itemID);
        }

        #endregion

        #region Private Properties
        private int itemID = -1;
        private int siteID = -1;
        private int moduleID = -1;
        private string title = string.Empty;
        private string general = string.Empty;
        private string functionP = string.Empty;
        private string mission = string.Empty;
        private int officerID = -1;
        private string achievement = string.Empty;
        private string procedureP = string.Empty;
        private string contact = string.Empty;
        private DateTime createDate = DateTime.UtcNow;
        private int creator = -1;
        private string createByName = string.Empty;
        private DateTime editDate = DateTime.UtcNow;
        private int editor = -1;
        private bool? isPublished = null;
        private string urlItem = string.Empty;
        private string lichCongTac = string.Empty;
        private int orderByUnit = 1;
        private int allowUserModify = 0;
        private string maKhoaPhong = string.Empty;
        private bool? isShowQuestion = null;
        #endregion

        #region Public Properties
        public bool? IsShowQuestion { get { return isShowQuestion; } set { isShowQuestion = value; } }
        public string MaKhoaPhong { get { return maKhoaPhong; } set { maKhoaPhong = value; } }
        public int AllowUserModify
        {
            get { return allowUserModify; }
            set { allowUserModify = value; }
        }
        public int OrderByUnit
        {
            get { return orderByUnit; }
            set { orderByUnit = value; }
        }
        public string LichCongTac
        {
            get { return lichCongTac; }
            set { lichCongTac = value; }
        }
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
        public int ModuleID
        {
            get { return moduleID; }
            set { moduleID = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string General
        {
            get { return general; }
            set { general = value; }
        }
        public string FunctionP
        {
            get { return functionP; }
            set { functionP = value; }
        }
        public string Mission
        {
            get { return mission; }
            set { mission = value; }
        }
        public int OfficerID
        {
            get { return officerID; }
            set { officerID = value; }
        }
        public string Achievement
        {
            get { return achievement; }
            set { achievement = value; }
        }
        public string ProcedureP
        {
            get { return procedureP; }
            set { procedureP = value; }
        }
        public string Contact
        {
            get { return contact; }
            set { contact = value; }
        }
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
        public int Creator
        {
            get { return creator; }
            set { creator = value; }
        }
        public string CreateByName
        {
            get { return createByName; }
            set { createByName = value; }
        }
        public DateTime EditDate
        {
            get { return editDate; }
            set { editDate = value; }
        }
        public int Editor
        {
            get { return editor; }
            set { editor = value; }
        }
        public bool? IsPublished
        {
            get { return isPublished; }
            set { isPublished = value; }
        }
        public string UrlItem
        {
            get { return urlItem; }
            set { urlItem = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of md_FunctionalUnit.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void Getmd_FunctionalUnit(
            int itemID)
        {
            using (IDataReader reader = DBmd_FunctionalUnit.GetOne(
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
                this.moduleID = Convert.ToInt32(reader["ModuleID"]);
                this.title = reader["Title"].ToString();
                this.general = reader["General"].ToString();
                this.functionP = reader["FunctionP"].ToString();
                this.mission = reader["Mission"].ToString();
                this.officerID = Convert.ToInt32(reader["OfficerID"]);
                this.achievement = reader["Achievement"].ToString();
                this.procedureP = reader["ProcedureP"].ToString();
                this.contact = reader["Contact"].ToString();
                this.createDate = Convert.ToDateTime(reader["CreateDate"]);
                this.creator = Convert.ToInt32(reader["Creator"]);
                this.editDate = Convert.ToDateTime(reader["EditDate"]);
                this.editor = Convert.ToInt32(reader["Editor"]);
                if (!string.IsNullOrEmpty(reader["IsPublished"].ToString()))
                {
                    this.isPublished = Convert.ToBoolean(reader["IsPublished"].ToString());
                }
                this.urlItem = reader["UrlItem"].ToString();
                if (!string.IsNullOrEmpty(reader["LichCongTac"].ToString()))
                {
                    this.lichCongTac = reader["LichCongTac"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["OrderByUnit"].ToString()))
                {
                    this.orderByUnit = Convert.ToInt32(reader["OrderByUnit"].ToString());
                }
                if (!string.IsNullOrEmpty(reader["AllowUserModify"].ToString()))
                {
                    this.allowUserModify = Convert.ToInt32(reader["AllowUserModify"]);
                }
                if (!string.IsNullOrEmpty(reader["MaKhoaPhong"].ToString()))
                {
                    this.maKhoaPhong = reader["MaKhoaPhong"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["IsShowQuestion"].ToString()))
                {
                    this.isShowQuestion = Convert.ToBoolean(reader["IsShowQuestion"]);
                }
            }

        }

        /// <summary>
        /// Persists a new instance of md_FunctionalUnit. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBmd_FunctionalUnit.Create(
                this.siteID,
                this.moduleID,
                this.title,
                this.general,
                this.functionP,
                this.mission,
                this.officerID,
                this.achievement,
                this.procedureP,
                this.contact,
                this.createDate,
                this.creator,
                this.editDate,
                this.editor,
                this.isPublished,
                this.urlItem,
                this.lichCongTac,
                this.orderByUnit,
                this.allowUserModify,
                this.maKhoaPhong,
                this.isShowQuestion);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of md_FunctionalUnit. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBmd_FunctionalUnit.Update(
                this.itemID,
                this.siteID,
                this.moduleID,
                this.title,
                this.general,
                this.functionP,
                this.mission,
                this.officerID,
                this.achievement,
                this.procedureP,
                this.contact,
                this.createDate,
                this.creator,
                this.editDate,
                this.editor,
                this.isPublished,
                this.urlItem,
                this.lichCongTac,
                this.orderByUnit,
                this.allowUserModify,
                this.maKhoaPhong,
                this.isShowQuestion);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of md_FunctionalUnit. Returns true on success.
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
        /// Deletes an instance of md_FunctionalUnit. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBmd_FunctionalUnit.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of md_FunctionalUnit. 
        /// </summary>
        public static int GetCount()
        {
            return DBmd_FunctionalUnit.GetCount();
        }

        private static List<md_FunctionalUnit> LoadListFromReader(IDataReader reader, bool getByName = false)
        {
            List<md_FunctionalUnit> md_FunctionalUnitList = new List<md_FunctionalUnit>();
            try
            {
                while (reader.Read())
                {
                    md_FunctionalUnit md_FunctionalUnit = new md_FunctionalUnit();
                    md_FunctionalUnit.itemID = Convert.ToInt32(reader["ItemID"]);
                    md_FunctionalUnit.siteID = Convert.ToInt32(reader["SiteID"]);
                    md_FunctionalUnit.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    md_FunctionalUnit.title = reader["Title"].ToString();
                    md_FunctionalUnit.general = reader["General"].ToString();
                    md_FunctionalUnit.functionP = reader["FunctionP"].ToString();
                    md_FunctionalUnit.mission = reader["Mission"].ToString();
                    md_FunctionalUnit.officerID = Convert.ToInt32(reader["OfficerID"]);
                    md_FunctionalUnit.achievement = reader["Achievement"].ToString();
                    md_FunctionalUnit.procedureP = reader["ProcedureP"].ToString();
                    md_FunctionalUnit.contact = reader["Contact"].ToString();
                    md_FunctionalUnit.createDate = Convert.ToDateTime(reader["CreateDate"]);
                    md_FunctionalUnit.creator = Convert.ToInt32(reader["Creator"]);
                    if (getByName)
                    {
                        md_FunctionalUnit.createByName = reader["CreateByName"].ToString();
                    }
                    md_FunctionalUnit.editDate = Convert.ToDateTime(reader["EditDate"]);
                    md_FunctionalUnit.editor = Convert.ToInt32(reader["Editor"]);
                    if (!string.IsNullOrEmpty(reader["IsPublished"].ToString()))
                    {
                        md_FunctionalUnit.isPublished = Convert.ToBoolean(reader["IsPublished"].ToString());
                    }
                    md_FunctionalUnit.urlItem = reader["UrlItem"].ToString();
                    if (!string.IsNullOrEmpty(reader["OrderByUnit"].ToString()))
                    {
                        md_FunctionalUnit.orderByUnit = Convert.ToInt32(reader["OrderByUnit"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["AllowUserModify"].ToString()))
                    {
                        md_FunctionalUnit.allowUserModify = Convert.ToInt32(reader["AllowUserModify"]);
                    }
                    if (!string.IsNullOrEmpty(reader["MaKhoaPhong"].ToString()))
                    {
                        md_FunctionalUnit.maKhoaPhong = reader["MaKhoaPhong"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["IsShowQuestion"].ToString()))
                    {
                        md_FunctionalUnit.isShowQuestion = Convert.ToBoolean(reader["IsShowQuestion"]);
                    }
                    md_FunctionalUnitList.Add(md_FunctionalUnit);

                }
            }
            finally
            {
                reader.Close();
            }

            return md_FunctionalUnitList;

        }

        /// <summary>
        /// Gets an IList with all instances of md_FunctionalUnit.
        /// </summary>
        public static List<md_FunctionalUnit> GetAll()
        {
            IDataReader reader = DBmd_FunctionalUnit.GetAll();
            return LoadListFromReader(reader);

        }
        public static List<md_FunctionalUnit> GetAllPublished()
        {
            IDataReader reader = DBmd_FunctionalUnit.GetAllPublished();
            return LoadListFromReader(reader);
        }

        /// <summary>
        /// Gets an IList with page of instances of md_FunctionalUnit.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<md_FunctionalUnit> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBmd_FunctionalUnit.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }
        public static List<md_FunctionalUnit> GetPageManage(string title, DateTime? createDate, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBmd_FunctionalUnit.GetPageManage(title, createDate, pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }
        public static List<md_FunctionalUnit> GetPageRecentList(int siteId, int moduleId, string title, DateTime? createDate, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBmd_FunctionalUnit.GetPageRecentList(siteId, moduleId, title, createDate, pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }


        public static List<md_FunctionalUnit> GetFunctionalUnit(string title, DateTime? createDate, int siteId, int pageNumber, int pageSize, out int totalPages)
        {
            IDataReader reader = DBmd_FunctionalUnit.GetFunctionalUnit(title, createDate, siteId, pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }

        public static List<md_FunctionalUnit> GetList(int officerID)
        {
            IDataReader reader = DBmd_FunctionalUnit.GetList(officerID);
            return LoadListFromReader(reader);
        }

        public static List<md_FunctionalUnit> GetListByUser(int userID)
        {
            IDataReader reader = DBmd_FunctionalUnit.GetListByUser(userID);
            return LoadListFromReader(reader);
        }


        public static List<md_FunctionalUnit> GetOrther(int itemId)
        {
            IDataReader reader = DBmd_FunctionalUnit.GetOrther(itemId);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of md_FunctionalUnit.
        /// </summary>
        public static int CompareByItemID(md_FunctionalUnit md_FunctionalUnit1, md_FunctionalUnit md_FunctionalUnit2)
        {
            return md_FunctionalUnit1.ItemID.CompareTo(md_FunctionalUnit2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of md_FunctionalUnit.
        /// </summary>
        public static int CompareBySiteID(md_FunctionalUnit md_FunctionalUnit1, md_FunctionalUnit md_FunctionalUnit2)
        {
            return md_FunctionalUnit1.SiteID.CompareTo(md_FunctionalUnit2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of md_FunctionalUnit.
        /// </summary>
        public static int CompareByModuleID(md_FunctionalUnit md_FunctionalUnit1, md_FunctionalUnit md_FunctionalUnit2)
        {
            return md_FunctionalUnit1.ModuleID.CompareTo(md_FunctionalUnit2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of md_FunctionalUnit.
        /// </summary>
        public static int CompareByTitle(md_FunctionalUnit md_FunctionalUnit1, md_FunctionalUnit md_FunctionalUnit2)
        {
            return md_FunctionalUnit1.Title.CompareTo(md_FunctionalUnit2.Title);
        }
        /// <summary>
        /// Compares 2 instances of md_FunctionalUnit.
        /// </summary>
        public static int CompareByGeneral(md_FunctionalUnit md_FunctionalUnit1, md_FunctionalUnit md_FunctionalUnit2)
        {
            return md_FunctionalUnit1.General.CompareTo(md_FunctionalUnit2.General);
        }
        /// <summary>
        /// Compares 2 instances of md_FunctionalUnit.
        /// </summary>
        public static int CompareByFunctionP(md_FunctionalUnit md_FunctionalUnit1, md_FunctionalUnit md_FunctionalUnit2)
        {
            return md_FunctionalUnit1.FunctionP.CompareTo(md_FunctionalUnit2.FunctionP);
        }
        /// <summary>
        /// Compares 2 instances of md_FunctionalUnit.
        /// </summary>
        public static int CompareByMission(md_FunctionalUnit md_FunctionalUnit1, md_FunctionalUnit md_FunctionalUnit2)
        {
            return md_FunctionalUnit1.Mission.CompareTo(md_FunctionalUnit2.Mission);
        }
        /// <summary>
        /// Compares 2 instances of md_FunctionalUnit.
        /// </summary>
        public static int CompareByOfficerID(md_FunctionalUnit md_FunctionalUnit1, md_FunctionalUnit md_FunctionalUnit2)
        {
            return md_FunctionalUnit1.OfficerID.CompareTo(md_FunctionalUnit2.OfficerID);
        }
        /// <summary>
        /// Compares 2 instances of md_FunctionalUnit.
        /// </summary>
        public static int CompareByAchievement(md_FunctionalUnit md_FunctionalUnit1, md_FunctionalUnit md_FunctionalUnit2)
        {
            return md_FunctionalUnit1.Achievement.CompareTo(md_FunctionalUnit2.Achievement);
        }
        /// <summary>
        /// Compares 2 instances of md_FunctionalUnit.
        /// </summary>
        public static int CompareByProcedureP(md_FunctionalUnit md_FunctionalUnit1, md_FunctionalUnit md_FunctionalUnit2)
        {
            return md_FunctionalUnit1.ProcedureP.CompareTo(md_FunctionalUnit2.ProcedureP);
        }
        /// <summary>
        /// Compares 2 instances of md_FunctionalUnit.
        /// </summary>
        public static int CompareByContact(md_FunctionalUnit md_FunctionalUnit1, md_FunctionalUnit md_FunctionalUnit2)
        {
            return md_FunctionalUnit1.Contact.CompareTo(md_FunctionalUnit2.Contact);
        }
        /// <summary>
        /// Compares 2 instances of md_FunctionalUnit.
        /// </summary>
        public static int CompareByCreateDate(md_FunctionalUnit md_FunctionalUnit1, md_FunctionalUnit md_FunctionalUnit2)
        {
            return md_FunctionalUnit1.CreateDate.CompareTo(md_FunctionalUnit2.CreateDate);
        }
        /// <summary>
        /// Compares 2 instances of md_FunctionalUnit.
        /// </summary>
        public static int CompareByCreator(md_FunctionalUnit md_FunctionalUnit1, md_FunctionalUnit md_FunctionalUnit2)
        {
            return md_FunctionalUnit1.Creator.CompareTo(md_FunctionalUnit2.Creator);
        }
        /// <summary>
        /// Compares 2 instances of md_FunctionalUnit.
        /// </summary>
        public static int CompareByEditDate(md_FunctionalUnit md_FunctionalUnit1, md_FunctionalUnit md_FunctionalUnit2)
        {
            return md_FunctionalUnit1.EditDate.CompareTo(md_FunctionalUnit2.EditDate);
        }
        /// <summary>
        /// Compares 2 instances of md_FunctionalUnit.
        /// </summary>
        public static int CompareByEditor(md_FunctionalUnit md_FunctionalUnit1, md_FunctionalUnit md_FunctionalUnit2)
        {
            return md_FunctionalUnit1.Editor.CompareTo(md_FunctionalUnit2.Editor);
        }

        #endregion


    }

}






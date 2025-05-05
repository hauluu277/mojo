
// Author:					Trieubv
// Created:					2015-10-27
// Last Modified:			2015-10-27
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

    public class DuThaoVanBan
    {

        #region Constructors

        public DuThaoVanBan()
        { }


        public DuThaoVanBan(
            int itemID)
        {
            GetDuThaoVanBan(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int moduleID = -1;
        private int pageID = -1;
        private int siteID = -1;
        private int linhVucID = -1;
        private int loaiVanBanID = -1;
        private string title = string.Empty;
        private string summary = string.Empty;
        private int fileID = -1;
        private string createdByUser = string.Empty;
        private DateTime createdDate = DateTime.UtcNow;
        private Guid lastModUserGuid = Guid.Empty;
        private DateTime lastModUtc = DateTime.UtcNow;
        private string itemUrl = string.Empty;
        private string fTS = string.Empty;
        private bool isPublic = false;
        private int publicByUser = -1;
        private DateTime publicDate = DateTime.UtcNow;
        private DateTime startDate = DateTime.UtcNow;
        private DateTime? endDate = null;
        private string linhVucName = string.Empty;
        private string loaiVBName = string.Empty;
        private int coQuanBanHanhID = -1;
        private string coQuanBanHanhName = string.Empty;
        private int totalYKien = 0;
        public int TotalYKien
        {
            get { return totalYKien; }
            set { totalYKien = value; }
        }
        #endregion

        #region Public Properties
        public string CoQuanBanHanhName
        {
            get { return coQuanBanHanhName; }
            set
            {
                coQuanBanHanhName = value;
            }
        }
        public int CoQuanBanHanhID
        {
            get { return coQuanBanHanhID; }
            set { coQuanBanHanhID = value; }
        }

        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public int ModuleID
        {
            get { return moduleID; }
            set { moduleID = value; }
        }
        public int PageID
        {
            get { return pageID; }
            set { pageID = value; }
        }
        public int SiteID
        {
            get { return siteID; }
            set { siteID = value; }
        }
        public int LinhVucID
        {
            get { return linhVucID; }
            set { linhVucID = value; }
        }
        public int LoaiVanBanID
        {
            get { return loaiVanBanID; }
            set { loaiVanBanID = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Summary
        {
            get { return summary; }
            set { summary = value; }
        }
        public int FileID
        {
            get { return fileID; }
            set { fileID = value; }
        }
        public string CreatedByUser
        {
            get { return createdByUser; }
            set { createdByUser = value; }
        }
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }
        public Guid LastModUserGuid
        {
            get { return lastModUserGuid; }
            set { lastModUserGuid = value; }
        }
        public DateTime LastModUtc
        {
            get { return lastModUtc; }
            set { lastModUtc = value; }
        }
        public string ItemUrl
        {
            get { return itemUrl; }
            set { itemUrl = value; }
        }
        public string FTS
        {
            get { return fTS; }
            set { fTS = value; }
        }
        public bool IsPublic
        {
            get { return isPublic; }
            set { isPublic = value; }
        }
        public int PublicByUser
        {
            get { return publicByUser; }
            set { publicByUser = value; }
        }
        public DateTime PublicDate
        {
            get { return publicDate; }
            set { publicDate = value; }
        }
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        public DateTime? EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public string LinhVucName
        {
            get { return linhVucName; }
            set { linhVucName = value; }
        }
        public string LoaiVBName
        {
            get { return loaiVBName; }
            set { loaiVBName = value; }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of DuThaoVanBan.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetDuThaoVanBan(
            int itemID)
        {
            using (IDataReader reader = DBDuThaoVanBan.GetOne(
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
                this.moduleID = Convert.ToInt32(reader["ModuleID"]);
                this.pageID = Convert.ToInt32(reader["PageID"]);
                this.siteID = Convert.ToInt32(reader["SiteID"]);
                this.linhVucID = Convert.ToInt32(reader["LinhVucID"]);
                this.loaiVanBanID = Convert.ToInt32(reader["LoaiVanBanID"]);


                this.title = reader["Title"].ToString();
                this.summary = reader["Summary"].ToString();
                this.fileID = Convert.ToInt32(reader["FileID"]);
                this.createdByUser = reader["CreatedByUser"].ToString();
                this.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                this.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                this.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                this.itemUrl = reader["ItemUrl"].ToString();
                this.fTS = reader["FTS"].ToString();
                this.isPublic = Convert.ToBoolean(reader["IsPublic"]);
                this.publicByUser = Convert.ToInt32(reader["PublicByUser"]);
                if (!string.IsNullOrEmpty(reader["PublicDate"].ToString()))
                {
                    this.publicDate = Convert.ToDateTime(reader["PublicDate"]);
                }

                if (!string.IsNullOrEmpty(reader["CoQuanBanHanhID"].ToString()))
                {
                    this.coQuanBanHanhID = Convert.ToInt32(reader["CoQuanBanHanhID"]);

                    if (!string.IsNullOrEmpty(reader["CoQuanBanHanhName"].ToString()))
                    {
                        this.coQuanBanHanhName = reader["CoQuanBanHanhName"].ToString();
                    }
                }
                if (!string.IsNullOrEmpty(reader["LinhVucName"].ToString()))
                {
                    this.linhVucName = reader["LinhVucName"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["LoaiVBName"].ToString()))
                {
                    this.loaiVBName = reader["LoaiVBName"].ToString();
                }

                if (!string.IsNullOrEmpty(reader["StartDate"].ToString()))
                {
                    this.startDate = Convert.ToDateTime(reader["StartDate"]);
                }
                if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                {
                    this.endDate = Convert.ToDateTime(reader["EndDate"]);
                }

            }

        }

        /// <summary>
        /// Persists a new instance of DuThaoVanBan. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBDuThaoVanBan.Create(
                this.moduleID,
                this.pageID,
                this.siteID,
                this.linhVucID,
                this.loaiVanBanID,
                this.title,
                this.summary,
                this.fileID,
                this.createdByUser,
                this.createdDate,
                this.lastModUserGuid,
                this.lastModUtc,
                this.itemUrl,
                this.fTS,
                this.isPublic,
                this.publicByUser,
                this.publicDate,
                this.startDate,
                this.endDate,
                coQuanBanHanhID);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of DuThaoVanBan. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBDuThaoVanBan.Update(
                this.itemID,
                this.moduleID,
                this.pageID,
                this.siteID,
                this.linhVucID,
                this.loaiVanBanID,
                this.title,
                this.summary,
                this.fileID,
                this.createdByUser,
                this.createdDate,
                this.lastModUserGuid,
                this.lastModUtc,
                this.itemUrl,
                this.fTS,
                this.isPublic,
                this.publicByUser,
                this.publicDate,
                this.startDate,
            this.endDate,
            coQuanBanHanhID);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of DuThaoVanBan. Returns true on success.
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
        /// Deletes an instance of DuThaoVanBan. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBDuThaoVanBan.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of DuThaoVanBan. 
        /// </summary>
        public static int GetCount(int siteID, int moduleID, int linhvucID, int loaiID, string keyword, bool? status, int? dateExpires, DateTime? date)
        {
            return DBDuThaoVanBan.GetCount(siteID, moduleID, linhvucID, loaiID, keyword, status, dateExpires, date);
        }

        private static List<DuThaoVanBan> LoadListFromReader(IDataReader reader, bool getpage = false)
        {
            List<DuThaoVanBan> duThaoVanBanList = new List<DuThaoVanBan>();
            try
            {
                while (reader.Read())
                {
                    DuThaoVanBan duThaoVanBan = new DuThaoVanBan();
                    duThaoVanBan.itemID = Convert.ToInt32(reader["ItemID"]);
                    duThaoVanBan.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    duThaoVanBan.pageID = Convert.ToInt32(reader["PageID"]);
                    duThaoVanBan.siteID = Convert.ToInt32(reader["SiteID"]);
                    duThaoVanBan.linhVucID = Convert.ToInt32(reader["LinhVucID"]);
                    duThaoVanBan.loaiVanBanID = Convert.ToInt32(reader["LoaiVanBanID"]);
                    duThaoVanBan.title = reader["Title"].ToString();
                    duThaoVanBan.summary = reader["Summary"].ToString();
                    duThaoVanBan.fileID = Convert.ToInt32(reader["FileID"]);
                    duThaoVanBan.createdByUser = reader["CreatedByUser"].ToString();
                    duThaoVanBan.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    duThaoVanBan.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                    duThaoVanBan.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                    duThaoVanBan.itemUrl = reader["ItemUrl"].ToString();
                    duThaoVanBan.fTS = reader["FTS"].ToString();
                    duThaoVanBan.isPublic = Convert.ToBoolean(reader["IsPublic"]);
                    duThaoVanBan.publicByUser = Convert.ToInt32(reader["PublicByUser"]);
                    if (!string.IsNullOrEmpty(reader["PublicDate"].ToString()))
                    {
                        duThaoVanBan.publicDate = Convert.ToDateTime(reader["PublicDate"]);
                    }
                    if (!string.IsNullOrEmpty(reader["StartDate"].ToString()))
                    {
                        duThaoVanBan.startDate = Convert.ToDateTime(reader["StartDate"]);
                    }
                    if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                    {
                        duThaoVanBan.endDate = Convert.ToDateTime(reader["EndDate"]);
                    }
                    if (getpage)
                    {
                        duThaoVanBan.linhVucName = reader["LinhVucName"].ToString();
                        duThaoVanBan.loaiVBName = reader["LoaiVBName"].ToString();
                        if (!string.IsNullOrEmpty(reader["TotalYKien"].ToString()))
                        {
                            duThaoVanBan.totalYKien = Convert.ToInt32(reader["TotalYKien"]);
                        }
                    }
                    duThaoVanBanList.Add(duThaoVanBan);

                }
            }
            finally
            {
                reader.Close();
            }

            return duThaoVanBanList;

        }

        /// <summary>
        /// Gets an IList with all instances of DuThaoVanBan.
        /// </summary>
        public static List<DuThaoVanBan> GetAll()
        {
            IDataReader reader = DBDuThaoVanBan.GetAll();
            return LoadListFromReader(reader);

        }
        public static List<DuThaoVanBan> GetOrther(int siteID, int moduleID, int top, int itemID)
        {
            IDataReader reader = DBDuThaoVanBan.GetOrther(siteID, moduleID, top, itemID);
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of DuThaoVanBan.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<DuThaoVanBan> GetPage(int siteID, int moduleID, int linhvucID, int loaiID, string keyword, bool? status, int? dateExpires, DateTime? date, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBDuThaoVanBan.GetPage(siteID, moduleID, linhvucID, loaiID, keyword, status, dateExpires, date, pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader, true);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of DuThaoVanBan.
        /// </summary>
        public static int CompareByItemID(DuThaoVanBan duThaoVanBan1, DuThaoVanBan duThaoVanBan2)
        {
            return duThaoVanBan1.ItemID.CompareTo(duThaoVanBan2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of DuThaoVanBan.
        /// </summary>
        public static int CompareByModuleID(DuThaoVanBan duThaoVanBan1, DuThaoVanBan duThaoVanBan2)
        {
            return duThaoVanBan1.ModuleID.CompareTo(duThaoVanBan2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of DuThaoVanBan.
        /// </summary>
        public static int CompareByPageID(DuThaoVanBan duThaoVanBan1, DuThaoVanBan duThaoVanBan2)
        {
            return duThaoVanBan1.PageID.CompareTo(duThaoVanBan2.PageID);
        }
        /// <summary>
        /// Compares 2 instances of DuThaoVanBan.
        /// </summary>
        public static int CompareBySiteID(DuThaoVanBan duThaoVanBan1, DuThaoVanBan duThaoVanBan2)
        {
            return duThaoVanBan1.SiteID.CompareTo(duThaoVanBan2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of DuThaoVanBan.
        /// </summary>
        public static int CompareByLinhVucID(DuThaoVanBan duThaoVanBan1, DuThaoVanBan duThaoVanBan2)
        {
            return duThaoVanBan1.LinhVucID.CompareTo(duThaoVanBan2.LinhVucID);
        }
        /// <summary>
        /// Compares 2 instances of DuThaoVanBan.
        /// </summary>
        public static int CompareByLoaiVanBanID(DuThaoVanBan duThaoVanBan1, DuThaoVanBan duThaoVanBan2)
        {
            return duThaoVanBan1.LoaiVanBanID.CompareTo(duThaoVanBan2.LoaiVanBanID);
        }
        /// <summary>
        /// Compares 2 instances of DuThaoVanBan.
        /// </summary>
        public static int CompareByTitle(DuThaoVanBan duThaoVanBan1, DuThaoVanBan duThaoVanBan2)
        {
            return duThaoVanBan1.Title.CompareTo(duThaoVanBan2.Title);
        }
        /// <summary>
        /// Compares 2 instances of DuThaoVanBan.
        /// </summary>
        public static int CompareBySummary(DuThaoVanBan duThaoVanBan1, DuThaoVanBan duThaoVanBan2)
        {
            return duThaoVanBan1.Summary.CompareTo(duThaoVanBan2.Summary);
        }
        /// <summary>
        /// Compares 2 instances of DuThaoVanBan.
        /// </summary>
        public static int CompareByFileID(DuThaoVanBan duThaoVanBan1, DuThaoVanBan duThaoVanBan2)
        {
            return duThaoVanBan1.FileID.CompareTo(duThaoVanBan2.FileID);
        }
        /// <summary>
        /// Compares 2 instances of DuThaoVanBan.
        /// </summary>
        public static int CompareByCreatedByUser(DuThaoVanBan duThaoVanBan1, DuThaoVanBan duThaoVanBan2)
        {
            return duThaoVanBan1.CreatedByUser.CompareTo(duThaoVanBan2.CreatedByUser);
        }
        /// <summary>
        /// Compares 2 instances of DuThaoVanBan.
        /// </summary>
        public static int CompareByCreatedDate(DuThaoVanBan duThaoVanBan1, DuThaoVanBan duThaoVanBan2)
        {
            return duThaoVanBan1.CreatedDate.CompareTo(duThaoVanBan2.CreatedDate);
        }
        /// <summary>
        /// Compares 2 instances of DuThaoVanBan.
        /// </summary>
        public static int CompareByLastModUtc(DuThaoVanBan duThaoVanBan1, DuThaoVanBan duThaoVanBan2)
        {
            return duThaoVanBan1.LastModUtc.CompareTo(duThaoVanBan2.LastModUtc);
        }
        /// <summary>
        /// Compares 2 instances of DuThaoVanBan.
        /// </summary>
        public static int CompareByItemUrl(DuThaoVanBan duThaoVanBan1, DuThaoVanBan duThaoVanBan2)
        {
            return duThaoVanBan1.ItemUrl.CompareTo(duThaoVanBan2.ItemUrl);
        }
        /// <summary>
        /// Compares 2 instances of DuThaoVanBan.
        /// </summary>
        public static int CompareByFTS(DuThaoVanBan duThaoVanBan1, DuThaoVanBan duThaoVanBan2)
        {
            return duThaoVanBan1.FTS.CompareTo(duThaoVanBan2.FTS);
        }

        #endregion


    }

}






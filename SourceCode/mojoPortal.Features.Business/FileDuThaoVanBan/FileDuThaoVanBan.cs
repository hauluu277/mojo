
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

    public class FileDuThao
    {

        #region Constructors

        public FileDuThao()
        { }


        public FileDuThao(
            int itemID)
        {
            GetFileDuThao(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int siteID = -1;
        private int moduleID = -1;
        private int pageID = -1;
        private int duThaoID = -1;
        private string name = string.Empty;
        private string filePath = string.Empty;
        private int createdByUserID = -1;
        private DateTime createdDate = DateTime.UtcNow;
        private string lastModUserID = string.Empty;
        private DateTime lastModUtc = DateTime.UtcNow;

        #endregion

        #region Public Properties

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
        public int PageID
        {
            get { return pageID; }
            set { pageID = value; }
        }
        public int DuThaoID
        {
            get { return duThaoID; }
            set { duThaoID = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }
        public int CreatedByUserID
        {
            get { return createdByUserID; }
            set { createdByUserID = value; }
        }
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }
        public string LastModUserID
        {
            get { return lastModUserID; }
            set { lastModUserID = value; }
        }
        public DateTime LastModUtc
        {
            get { return lastModUtc; }
            set { lastModUtc = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of FileDuThao.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetFileDuThao(
            int itemID)
        {
            using (IDataReader reader = DBFileDuThao.GetOne(
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
                this.pageID = Convert.ToInt32(reader["PageID"]);
                this.duThaoID = Convert.ToInt32(reader["DuThaoID"]);
                this.name = reader["Name"].ToString();
                this.filePath = reader["FilePath"].ToString();
                this.createdByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                this.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                this.lastModUserID = reader["LastModUserID"].ToString();
                this.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);

            }

        }

        /// <summary>
        /// Persists a new instance of FileDuThao. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBFileDuThao.Create(
                this.siteID,
                this.moduleID,
                this.pageID,
                this.duThaoID,
                this.name,
                this.filePath,
                this.createdByUserID,
                this.createdDate,
                this.lastModUserID,
                this.lastModUtc);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of FileDuThao. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBFileDuThao.Update(
                this.itemID,
                this.siteID,
                this.moduleID,
                this.pageID,
                this.duThaoID,
                this.name,
                this.filePath,
                this.createdByUserID,
                this.createdDate,
                this.lastModUserID,
                this.lastModUtc);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of FileDuThao. Returns true on success.
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
        /// Deletes an instance of FileDuThao. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBFileDuThao.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of FileDuThao. 
        /// </summary>
        public static int GetCount()
        {
            return DBFileDuThao.GetCount();
        }

        private static List<FileDuThao> LoadListFromReader(IDataReader reader)
        {
            List<FileDuThao> fileDuThaoList = new List<FileDuThao>();
            try
            {
                while (reader.Read())
                {
                    FileDuThao fileDuThao = new FileDuThao();
                    fileDuThao.itemID = Convert.ToInt32(reader["ItemID"]);
                    fileDuThao.siteID = Convert.ToInt32(reader["SiteID"]);
                    fileDuThao.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    fileDuThao.pageID = Convert.ToInt32(reader["PageID"]);
                    fileDuThao.duThaoID = Convert.ToInt32(reader["DuThaoID"]);
                    fileDuThao.name = reader["Name"].ToString();
                    fileDuThao.filePath = reader["FilePath"].ToString();
                    fileDuThao.createdByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    fileDuThao.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    fileDuThao.lastModUserID = reader["LastModUserID"].ToString();
                    fileDuThao.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                    fileDuThaoList.Add(fileDuThao);

                }
            }
            finally
            {
                reader.Close();
            }

            return fileDuThaoList;

        }

        /// <summary>
        /// Gets an IList with all instances of FileDuThao.
        /// </summary>
        public static List<FileDuThao> GetAll()
        {
            IDataReader reader = DBFileDuThao.GetAll();
            return LoadListFromReader(reader);

        }
        public static List<FileDuThao> GetAllByDuThaoId(int duThaoID)
        {
            IDataReader reader = DBFileDuThao.GetAllByDuThaoId(duThaoID);
            return LoadListFromReader(reader);

        }


        /// <summary>
        /// Gets an IList with page of instances of FileDuThao.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<FileDuThao> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBFileDuThao.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of FileDuThao.
        /// </summary>
        public static int CompareByItemID(FileDuThao fileDuThao1, FileDuThao fileDuThao2)
        {
            return fileDuThao1.ItemID.CompareTo(fileDuThao2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of FileDuThao.
        /// </summary>
        public static int CompareBySiteID(FileDuThao fileDuThao1, FileDuThao fileDuThao2)
        {
            return fileDuThao1.SiteID.CompareTo(fileDuThao2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of FileDuThao.
        /// </summary>
        public static int CompareByModuleID(FileDuThao fileDuThao1, FileDuThao fileDuThao2)
        {
            return fileDuThao1.ModuleID.CompareTo(fileDuThao2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of FileDuThao.
        /// </summary>
        public static int CompareByPageID(FileDuThao fileDuThao1, FileDuThao fileDuThao2)
        {
            return fileDuThao1.PageID.CompareTo(fileDuThao2.PageID);
        }
        /// <summary>
        /// Compares 2 instances of FileDuThao.
        /// </summary>
        public static int CompareByDuThaoID(FileDuThao fileDuThao1, FileDuThao fileDuThao2)
        {
            return fileDuThao1.DuThaoID.CompareTo(fileDuThao2.DuThaoID);
        }
        /// <summary>
        /// Compares 2 instances of FileDuThao.
        /// </summary>
        public static int CompareByName(FileDuThao fileDuThao1, FileDuThao fileDuThao2)
        {
            return fileDuThao1.Name.CompareTo(fileDuThao2.Name);
        }
        /// <summary>
        /// Compares 2 instances of FileDuThao.
        /// </summary>
        public static int CompareByCreatedByUserID(FileDuThao fileDuThao1, FileDuThao fileDuThao2)
        {
            return fileDuThao1.CreatedByUserID.CompareTo(fileDuThao2.CreatedByUserID);
        }
        /// <summary>
        /// Compares 2 instances of FileDuThao.
        /// </summary>
        public static int CompareByCreatedDate(FileDuThao fileDuThao1, FileDuThao fileDuThao2)
        {
            return fileDuThao1.CreatedDate.CompareTo(fileDuThao2.CreatedDate);
        }
        /// <summary>
        /// Compares 2 instances of FileDuThao.
        /// </summary>
        public static int CompareByLastModUserID(FileDuThao fileDuThao1, FileDuThao fileDuThao2)
        {
            return fileDuThao1.LastModUserID.CompareTo(fileDuThao2.LastModUserID);
        }
        /// <summary>
        /// Compares 2 instances of FileDuThao.
        /// </summary>
        public static int CompareByLastModUtc(FileDuThao fileDuThao1, FileDuThao fileDuThao2)
        {
            return fileDuThao1.LastModUtc.CompareTo(fileDuThao2.LastModUtc);
        }

        #endregion


    }

}






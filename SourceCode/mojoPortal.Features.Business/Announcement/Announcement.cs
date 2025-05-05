
// Author:					Manh Dtr
// Created:					2020-1-3
// Last Modified:			2020-1-3
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

    public class md_Announcement
    {

        #region Constructors

        public md_Announcement()
        { }


        public md_Announcement(
            int itemID)
        {
            Getmd_Announcement(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int siteID = -1;
        private int pageID = -1;
        private int moduleID = -1;
        private DateTime dateAnno = DateTime.UtcNow;
        private string contentAnno = string.Empty;
        private int annoHot = -1;

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
        public int PageID
        {
            get { return pageID; }
            set { pageID = value; }
        }
        public int ModuleID
        {
            get { return moduleID; }
            set { moduleID = value; }
        }
        public DateTime DateAnno
        {
            get { return dateAnno; }
            set { dateAnno = value; }
        }
        public string ContentAnno
        {
            get { return contentAnno; }
            set { contentAnno = value; }
        }
        public int AnnoHot
        {
            get { return annoHot; }
            set { annoHot = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of md_Announcement.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void Getmd_Announcement(
            int itemID)
        {
            using (IDataReader reader = DBmd_Announcement.GetOne(
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
                this.pageID = Convert.ToInt32(reader["PageID"]);
                this.moduleID = Convert.ToInt32(reader["ModuleID"]);
                this.dateAnno = Convert.ToDateTime(reader["DateAnno"]);
                this.contentAnno = reader["ContentAnno"].ToString();
                this.annoHot = Convert.ToInt32(reader["AnnoHot"]);

            }

        }

        /// <summary>
        /// Persists a new instance of md_Announcement. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBmd_Announcement.Create(
                this.siteID,
                this.pageID,
                this.moduleID,
                this.dateAnno,
                this.contentAnno,
                this.annoHot);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of md_Announcement. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBmd_Announcement.Update(
                this.itemID,
                this.siteID,
                this.pageID,
                this.moduleID,
                this.dateAnno,
                this.contentAnno,
                this.annoHot);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of md_Announcement. Returns true on success.
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
        /// Deletes an instance of md_Announcement. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBmd_Announcement.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of md_Announcement. 
        /// </summary>
        public static int GetCount()
        {
            return DBmd_Announcement.GetCount();
        }

        private static List<md_Announcement> LoadListFromReader(IDataReader reader)
        {
            List<md_Announcement> md_AnnouncementList = new List<md_Announcement>();
            try
            {
                while (reader.Read())
                {
                    md_Announcement md_Announcement = new md_Announcement();
                    md_Announcement.itemID = Convert.ToInt32(reader["ItemID"]);
                    md_Announcement.siteID = Convert.ToInt32(reader["SiteID"]);
                    md_Announcement.pageID = Convert.ToInt32(reader["PageID"]);
                    md_Announcement.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    md_Announcement.dateAnno = Convert.ToDateTime(reader["DateAnno"]);
                    md_Announcement.contentAnno = reader["ContentAnno"].ToString();
                    md_Announcement.annoHot = Convert.ToInt32(reader["AnnoHot"]);
                    md_AnnouncementList.Add(md_Announcement);

                }
            }
            finally
            {
                reader.Close();
            }

            return md_AnnouncementList;

        }

        /// <summary>
        /// Gets an IList with all instances of md_Announcement.
        /// </summary>
        public static List<md_Announcement> GetAll()
        {
            IDataReader reader = DBmd_Announcement.GetAll();
            return LoadListFromReader(reader);

        }
        public static List<md_Announcement> GetTopHot(int top, int siteId)
        {
            IDataReader reader = DBmd_Announcement.GetTopHot(top, siteId);
            return LoadListFromReader(reader);
        }
        /// <summary>
        /// Gets an IList with page of instances of md_Announcement.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<md_Announcement> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBmd_Announcement.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
    }
    public static List<md_Announcement> GetPage(int siteId,int moduleId,int pageNumber,int pageSize, int categoryID, string keyword,out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBmd_Announcement.GetPage(siteId, moduleId, pageNumber, pageSize, categoryID, keyword, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of md_Announcement.
        /// </summary>
        public static int CompareByItemID(md_Announcement md_Announcement1, md_Announcement md_Announcement2)
        {
            return md_Announcement1.ItemID.CompareTo(md_Announcement2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of md_Announcement.
        /// </summary>
        public static int CompareBySiteID(md_Announcement md_Announcement1, md_Announcement md_Announcement2)
        {
            return md_Announcement1.SiteID.CompareTo(md_Announcement2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of md_Announcement.
        /// </summary>
        public static int CompareByPageID(md_Announcement md_Announcement1, md_Announcement md_Announcement2)
        {
            return md_Announcement1.PageID.CompareTo(md_Announcement2.PageID);
        }
        /// <summary>
        /// Compares 2 instances of md_Announcement.
        /// </summary>
        public static int CompareByModuleID(md_Announcement md_Announcement1, md_Announcement md_Announcement2)
        {
            return md_Announcement1.ModuleID.CompareTo(md_Announcement2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of md_Announcement.
        /// </summary>
        public static int CompareByDateAnno(md_Announcement md_Announcement1, md_Announcement md_Announcement2)
        {
            return md_Announcement1.DateAnno.CompareTo(md_Announcement2.DateAnno);
        }
        /// <summary>
        /// Compares 2 instances of md_Announcement.
        /// </summary>
        public static int CompareByContentAnno(md_Announcement md_Announcement1, md_Announcement md_Announcement2)
        {
            return md_Announcement1.ContentAnno.CompareTo(md_Announcement2.ContentAnno);
        }
        /// <summary>
        /// Compares 2 instances of md_Announcement.
        /// </summary>
        public static int CompareByAnnoHot(md_Announcement md_Announcement1, md_Announcement md_Announcement2)
        {
            return md_Announcement1.AnnoHot.CompareTo(md_Announcement2.AnnoHot);
        }

        #endregion


    }

}






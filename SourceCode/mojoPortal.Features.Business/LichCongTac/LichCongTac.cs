
// Author:					Trieubv
// Created:					2016-1-4
// Last Modified:			2016-1-4
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

    public class LichCongTac
    {

        #region Constructors

        public LichCongTac()
        { }


        public LichCongTac(
            int itemID)
        {
            GetLichCongTac(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int siteID = -1;
        private int moduleID = -1;
        private int pageID = -1;
        private string startTime = string.Empty;
        private string endTime = string.Empty;
        private string buoiSang = string.Empty;
        private string buoiChieu = string.Empty;
        private string buoiToi = string.Empty;
        private DateTime startDate = DateTime.UtcNow;
        private DateTime? endDate = null;
        private int week = -1;
        private DateTime dateCreate = DateTime.UtcNow;
        private int createBy = -1;
        private string fTS = string.Empty;
        private DateTime startWeek = DateTime.UtcNow;
        private DateTime endWeek = DateTime.UtcNow;
        private int nam = -1;
        private int dayId = -1;
        private string thu = string.Empty;
        private string diaDiem = string.Empty;
        private string categoryAuthor { get; set; }

        #endregion

        #region Public Properties
        public string CategoryAuthor
        {
            get { return categoryAuthor; }
            set { categoryAuthor = value; }
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
        public int PageID
        {
            get { return pageID; }
            set { pageID = value; }
        }
        public string StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        public string EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
        public string BuoiSang
        {
            get { return buoiSang; }
            set { buoiSang = value; }
        }
        public string BuoiChieu
        {
            get { return buoiChieu; }
            set { buoiChieu = value; }
        }
        public string BuoiToi
        {
            get { return buoiToi; }
            set { buoiToi = value; }
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
        public int Week
        {
            get { return week; }
            set { week = value; }
        }
        public DateTime DateCreate
        {
            get { return dateCreate; }
            set { dateCreate = value; }
        }
        public int CreateBy
        {
            get { return createBy; }
            set { createBy = value; }
        }
        public string FTS
        {
            get { return fTS; }
            set { fTS = value; }
        }
        public DateTime StartWeek
        {
            get { return startWeek; }
            set { startWeek = value; }
        }
        public DateTime EndWeek
        {
            get { return endWeek; }
            set { endWeek = value; }
        }
        public int Nam
        {
            get { return nam; }
            set { nam = value; }
        }
        public int DayID
        {
            get { return dayId; }
            set { dayId = value; }
        }
        public string Thu
        {
            get { return thu; }
            set { thu = value; }
        }

        public string DiaDiem
        {
            get { return diaDiem; }
            set { diaDiem = value; }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of LichCongTac.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetLichCongTac(
            int itemID)
        {
            using (IDataReader reader = DBLichCongTac.GetOne(
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
                this.startTime = reader["StartTime"].ToString();
                this.endTime = reader["EndTime"].ToString();
                this.buoiSang = reader["BuoiSang"].ToString();
                this.buoiChieu = reader["BuoiChieu"].ToString();
                this.buoiToi = reader["BuoiToi"].ToString();
                this.startDate = Convert.ToDateTime(reader["StartDate"]);
                if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                {
                    this.endDate = Convert.ToDateTime(reader["EndDate"]);
                }
                this.week = Convert.ToInt32(reader["Week"]);
                this.dateCreate = Convert.ToDateTime(reader["DateCreate"]);
                this.createBy = Convert.ToInt32(reader["CreateBy"]);
                this.fTS = reader["FTS"].ToString();
                this.startWeek = Convert.ToDateTime(reader["StartWeek"]);
                this.endWeek = Convert.ToDateTime(reader["EndWeek"]);
                this.nam = Convert.ToInt32(reader["Nam"]);
                if (!string.IsNullOrEmpty(reader["DayID"].ToString()))
                {
                    this.dayId = Convert.ToInt32(reader["DayID"]);
                }

                if (!string.IsNullOrEmpty(reader["DiaDiem"].ToString()))
                {
                    this.diaDiem = reader["DiaDiem"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["CategoryAuthor"].ToString()))
                {
                    this.categoryAuthor = reader["CategoryAuthor"].ToString();
                }
            }

        }

        /// <summary>
        /// Persists a new instance of LichCongTac. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBLichCongTac.Create(
                this.siteID,
                this.moduleID,
                this.pageID,
                this.startTime,
                this.endTime,
                this.buoiSang,
                this.buoiChieu,
                this.buoiToi,
                this.startDate,
                this.endDate,
                this.week,
                this.dateCreate,
                this.createBy,
                this.fTS,
                this.startWeek,
                this.endWeek,
                this.nam,
                this.dayId,
                this.diaDiem,
                this.categoryAuthor);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of LichCongTac. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBLichCongTac.Update(
                this.itemID,
                this.siteID,
                this.moduleID,
                this.pageID,
                this.startTime,
                this.endTime,
                           this.buoiSang,
                this.buoiChieu,
                this.buoiToi,
                this.startDate,
                this.endDate,
                this.week,
                this.dateCreate,
                this.createBy,
                this.fTS,
                this.startWeek,
                this.endWeek,
                this.nam,
                this.dayId,
                this.diaDiem,
                this.categoryAuthor);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of LichCongTac. Returns true on success.
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
        /// Deletes an instance of LichCongTac. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBLichCongTac.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of LichCongTac. 
        /// </summary>
        public static int GetCount(int siteID, int moduleID, int week, int year, string author, int dayId, string keyword)
        {
            return DBLichCongTac.GetCount(siteID, moduleID, week, year, dayId, author, keyword);
        }

        private static List<LichCongTac> LoadListFromReader(IDataReader reader)
        {
            List<LichCongTac> lichCongTacList = new List<LichCongTac>();
            try
            {
                while (reader.Read())
                {
                    LichCongTac lichCongTac = new LichCongTac();
                    lichCongTac.itemID = Convert.ToInt32(reader["ItemID"]);
                    lichCongTac.siteID = Convert.ToInt32(reader["SiteID"]);
                    lichCongTac.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    lichCongTac.pageID = Convert.ToInt32(reader["PageID"]);
                    lichCongTac.startTime = reader["StartTime"].ToString();
                    lichCongTac.endTime = reader["EndTime"].ToString();
                    lichCongTac.buoiSang = reader["BuoiSang"].ToString();
                    lichCongTac.buoiChieu = reader["BuoiChieu"].ToString();
                    lichCongTac.buoiToi = reader["BuoiToi"].ToString();
                    lichCongTac.startDate = Convert.ToDateTime(reader["StartDate"]);
                    if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                    {
                        lichCongTac.endDate = Convert.ToDateTime(reader["EndDate"]);
                    }
                    lichCongTac.week = Convert.ToInt32(reader["Week"]);
                    lichCongTac.dateCreate = Convert.ToDateTime(reader["DateCreate"]);
                    lichCongTac.createBy = Convert.ToInt32(reader["CreateBy"]);
                    lichCongTac.fTS = reader["FTS"].ToString();
                    lichCongTac.startWeek = Convert.ToDateTime(reader["StartWeek"]);
                    lichCongTac.endWeek = Convert.ToDateTime(reader["EndWeek"]);
                    lichCongTac.nam = Convert.ToInt32(reader["Nam"]);
                    if (!string.IsNullOrEmpty(reader["DayID"].ToString()))
                    {
                        lichCongTac.dayId = Convert.ToInt32(reader["DayID"]);
                    }
                    if (!string.IsNullOrEmpty(reader["DiaDiem"].ToString()))
                    {
                        lichCongTac.diaDiem = reader["DiaDiem"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["CategoryAuthor"].ToString()))
                    {
                        lichCongTac.categoryAuthor = reader["CategoryAuthor"].ToString();
                    }
                    lichCongTacList.Add(lichCongTac);

                }
            }
            finally
            {
                reader.Close();
            }

            return lichCongTacList;

        }

        /// <summary>
        /// Gets an IList with all instances of LichCongTac.
        /// </summary>
        public static List<LichCongTac> GetAll()
        {
            IDataReader reader = DBLichCongTac.GetAll();
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of LichCongTac.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<LichCongTac> GetPage(int siteID, int moduleID, int week, int year, int dayId, string author, string keyword, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBLichCongTac.GetPage(siteID, moduleID, week, year, dayId, author, keyword, pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }
        public static List<LichCongTac> GetPageFix(int siteID, int moduleID, int week, int year, int dayId, string author, string keyword, int pageNumber, int pageSize, out int totalPages, out int totalCount)
        {
            totalPages = 1;
            IDataReader reader = DBLichCongTac.GetPageFix(siteID, moduleID, week, year, dayId, author, keyword, pageNumber, pageSize, out totalPages, out totalCount);
            return LoadListFromReader(reader);
        }

        public static DataTable Export(int siteID, int moduleID, int week, int year, int dayId, string author, string keyword, int pageNumber, int pageSize, out int totalPages)
        {
            IDataReader reader = DBLichCongTac.GetPage(siteID, moduleID, week, year, dayId, author, keyword, pageNumber, pageSize, out totalPages);
            DataTable table = new DataTable();
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("Nam", typeof(string));
            table.Columns.Add("Tuan", typeof(string));
            table.Columns.Add("Ngay Bat Dau", typeof(string));
            table.Columns.Add("Ngay Ket Thuc", typeof(string));
            table.Columns.Add("Ngay", typeof(string));
            var stt = 0;
            while (reader.Read())
            {
                stt++;
                DataRow row = table.NewRow();
                table.Rows.Add(stt, reader["Nam"], reader["Week"], string.Format("{0:dd/MM/yyyy}", reader["StartWeek"]), string.Format("{0:dd/MM/yyyy}", reader["EndWeek"]), string.Format("{0:dd/MM/yyyy}", reader["StartDate"]));
                table.Rows.Add(row);
            }
            return table;
        }
        public static DataTable ExportAll(int siteID, int moduleID, int week, int year, int dayId, string author, string keyword, int pageNumber, int pageSize, out int totalPages)
        {
            var total = DBLichCongTac.GetCount(siteID, moduleID, week, year, dayId, author, keyword);

            IDataReader reader = DBLichCongTac.GetPage(siteID, moduleID, week, year, dayId, author, keyword, pageNumber, total, out totalPages);
            DataTable table = new DataTable();
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("Nam", typeof(string));
            table.Columns.Add("Tuan", typeof(string));
            table.Columns.Add("Ngay Bat Dau", typeof(string));
            table.Columns.Add("Ngay Ket Thuc", typeof(string));
            table.Columns.Add("Ngay", typeof(string));
            var stt = 0;
            while (reader.Read())
            {
                stt++;
                table.Rows.Add(stt, reader["Nam"], reader["Week"], string.Format("{0:dd/MM/yyyy}", reader["StartWeek"]), string.Format("{0:dd/MM/yyyy}", reader["EndWeek"]), string.Format("{0:dd/MM/yyyy}", reader["StartDate"]));
            }
            return table;
        }


        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of LichCongTac.
        /// </summary>
        public static int CompareByItemID(LichCongTac lichCongTac1, LichCongTac lichCongTac2)
        {
            return lichCongTac1.ItemID.CompareTo(lichCongTac2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of LichCongTac.
        /// </summary>
        public static int CompareBySiteID(LichCongTac lichCongTac1, LichCongTac lichCongTac2)
        {
            return lichCongTac1.SiteID.CompareTo(lichCongTac2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of LichCongTac.
        /// </summary>
        public static int CompareByModuleID(LichCongTac lichCongTac1, LichCongTac lichCongTac2)
        {
            return lichCongTac1.ModuleID.CompareTo(lichCongTac2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of LichCongTac.
        /// </summary>
        public static int CompareByPageID(LichCongTac lichCongTac1, LichCongTac lichCongTac2)
        {
            return lichCongTac1.PageID.CompareTo(lichCongTac2.PageID);
        }
        /// <summary>
        /// Compares 2 instances of LichCongTac.
        /// </summary>
        public static int CompareByStartTime(LichCongTac lichCongTac1, LichCongTac lichCongTac2)
        {
            return lichCongTac1.StartTime.CompareTo(lichCongTac2.StartTime);
        }
        /// <summary>
        /// Compares 2 instances of LichCongTac.
        /// </summary>
        public static int CompareByEndTime(LichCongTac lichCongTac1, LichCongTac lichCongTac2)
        {
            return lichCongTac1.EndTime.CompareTo(lichCongTac2.EndTime);
        }
        /// <summary>
        /// Compares 2 instances of LichCongTac.
        /// </summary>
        //public static int CompareBySummary(LichCongTac lichCongTac1, LichCongTac lichCongTac2)
        //{
        //    return lichCongTac1.Summary.CompareTo(lichCongTac2.Summary);
        //}
        /// <summary>
        /// Compares 2 instances of LichCongTac.
        /// </summary>
        public static int CompareByStartDate(LichCongTac lichCongTac1, LichCongTac lichCongTac2)
        {
            return lichCongTac1.StartDate.CompareTo(lichCongTac2.StartDate);
        }
        /// <summary>
        /// Compares 2 instances of LichCongTac.
        /// </summary>
        /// <summary>
        /// Compares 2 instances of LichCongTac.
        /// </summary>
        public static int CompareByWeek(LichCongTac lichCongTac1, LichCongTac lichCongTac2)
        {
            return lichCongTac1.Week.CompareTo(lichCongTac2.Week);
        }
        /// <summary>
        /// Compares 2 instances of LichCongTac.
        /// </summary>
        public static int CompareByDateCreate(LichCongTac lichCongTac1, LichCongTac lichCongTac2)
        {
            return lichCongTac1.DateCreate.CompareTo(lichCongTac2.DateCreate);
        }
        /// <summary>
        /// Compares 2 instances of LichCongTac.
        /// </summary>
        public static int CompareByCreateBy(LichCongTac lichCongTac1, LichCongTac lichCongTac2)
        {
            return lichCongTac1.CreateBy.CompareTo(lichCongTac2.CreateBy);
        }
        /// <summary>
        /// Compares 2 instances of LichCongTac.
        /// </summary>
        public static int CompareByFTS(LichCongTac lichCongTac1, LichCongTac lichCongTac2)
        {
            return lichCongTac1.FTS.CompareTo(lichCongTac2.FTS);
        }

        #endregion


    }

}






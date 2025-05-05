
// Author:					HiNet
// Created:					2015-3-19
// Last Modified:			2015-3-19
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
using System.Web;
using mojoportal.CoreHelpers;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Data;
using mojoPortal.Features.Business.QLLog;

namespace mojoPortal.Business
{

    public class Documentation
    {
        private const string featureGuid = "a040f25b-a4a0-4bb4-b072-31b30a8ae814";

        public static Guid FeatureGuid
        {
            get { return new Guid(featureGuid); }
        }
        #region Constructors

        public Documentation()
        { }


        public Documentation(
            int itemID)
        {
            Getmd_Document(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int moduleID = -1;
        private int pageID = -1;
        private int siteID = -1;
        private string summary = string.Empty;
        private string sign = string.Empty;
        private DateTime? datePromulgate;
        private DateTime? dateEffect;
        private string signer = string.Empty;
        private string filePath = string.Empty;
        private int coQuanID = -1;
        private int loaiVB = -1;
        private int linhVuc = -1;
        private string itemUrl = string.Empty;
        private string createdByUser = string.Empty;
        private bool isApproved = false;
        private DateTime? approvedDate;
        private Guid approvedGuid = Guid.Empty;
        private Guid createdByUserGuid = Guid.Empty;
        private string linhVucName = string.Empty;
        private string loaiVbName = string.Empty;
        private string coQuanName = string.Empty;
        private int yearPromulgate = -1;
        private string nationalPromulgate = string.Empty;
        private int langID = -1;
        private string fTS = string.Empty;
        private string contentDoc = string.Empty;
        private string searchIndexPath = string.Empty;
        private int type = -1;
        private bool isNew = false;

        #endregion

        #region Public Properties
        public bool IsNew
        {
            get { return isNew; }
            set { isNew = value; }
        }
        public int Type { get { return type; } set { type = value; } }
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
        public string Summary
        {
            get { return summary; }
            set { summary = value; }
        }
        public string Sign
        {
            get { return sign; }
            set { sign = value; }
        }
        public DateTime? DatePromulgate
        {
            get { return datePromulgate; }
            set { datePromulgate = value; }
        }
        public DateTime? DateEffect
        {
            get { return dateEffect; }
            set { dateEffect = value; }
        }
        public string Signer
        {
            get { return signer; }
            set { signer = value; }
        }
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }
        public int CoQuanID
        {
            get { return coQuanID; }
            set { coQuanID = value; }
        }
        public int LoaiVB
        {
            get { return loaiVB; }
            set { loaiVB = value; }
        }
        public int LinhVuc
        {
            get { return linhVuc; }
            set { linhVuc = value; }
        }
        public string ItemUrl
        {
            get { return itemUrl; }
            set { itemUrl = value; }
        }
        public string CreatedByUser
        {
            get { return createdByUser; }
            set { createdByUser = value; }
        }
        public bool IsApproved
        {
            get { return isApproved; }
            set { isApproved = value; }
        }
        public DateTime? ApprovedDate
        {
            get { return approvedDate; }
            set { approvedDate = value; }
        }
        public Guid ApprovedGuid
        {
            get { return approvedGuid; }
            set { approvedGuid = value; }
        }
        public Guid CreatedByUserGuid
        {
            get { return createdByUserGuid; }
            set { createdByUserGuid = value; }
        }
        public string LinhVucName
        {
            get { return linhVucName; }
            set { linhVucName = value; }
        }
        public string LoaiVBName
        {
            get { return loaiVbName; }
            set { loaiVbName = value; }
        }
        public string CoQuanName
        {
            get { return coQuanName; }
            set { coQuanName = value; }
        }
        public int YearPromulgate
        {
            get { return yearPromulgate; }
            set { yearPromulgate = value; }
        }
        public string NationalPromulgate
        {
            get { return nationalPromulgate; }
            set { nationalPromulgate = value; }
        }
        public int LangID
        {
            get { return langID; }
            set { langID = value; }
        }
        public string FTS
        {
            get { return fTS; }
            set { fTS = value; }
        }
        public string ContentDoc
        {
            get { return contentDoc; }
            set { contentDoc = value; }
        }
        public string SearchIndexPath
        {
            get { return searchIndexPath; }
            set { searchIndexPath = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of md_Document.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void Getmd_Document(
            int itemID)
        {
            using (IDataReader reader = DBDocument.GetOne(
                itemID))
            {
                PopulateFromReader(reader, true);
            }

        }


        private void PopulateFromReader(IDataReader reader, bool getone = false)
        {
            if (reader.Read())
            {
                this.itemID = Convert.ToInt32(reader["ItemID"]);
                this.moduleID = Convert.ToInt32(reader["ModuleID"]);
                this.pageID = Convert.ToInt32(reader["PageID"]);
                this.siteID = Convert.ToInt32(reader["SiteID"]);
                this.summary = reader["Summary"].ToString();
                this.sign = reader["Sign"].ToString();
                if (!string.IsNullOrEmpty(reader["DatePromulgate"].ToString()))
                {
                    this.datePromulgate = Convert.ToDateTime(reader["DatePromulgate"]);
                }
                if (!string.IsNullOrEmpty(reader["DateEffect"].ToString()))
                {
                    this.dateEffect = Convert.ToDateTime(reader["DateEffect"]);
                }
                this.signer = reader["Signer"].ToString();
                this.filePath = reader["FilePath"].ToString();
                this.coQuanID = Convert.ToInt32(reader["CoQuanID"]);
                this.loaiVB = Convert.ToInt32(reader["LoaiVB"]);
                this.linhVuc = Convert.ToInt32(reader["LinhVuc"]);
                this.itemUrl = reader["ItemUrl"].ToString();
                this.createdByUser = reader["CreatedByUser"].ToString();
                this.isApproved = Convert.ToBoolean(reader["IsApproved"]);
                this.approvedDate = Convert.ToDateTime(reader["ApprovedDate"]);
                if (!string.IsNullOrEmpty(reader["ApprovedGuid"].ToString()))
                {
                    this.approvedGuid = new Guid(reader["ApprovedGuid"].ToString());
                }
                this.createdByUserGuid = new Guid(reader["CreatedByUserGuid"].ToString());
                if (!string.IsNullOrEmpty(reader["YearPromulgate"].ToString()))
                {
                    this.yearPromulgate = Convert.ToInt32(reader["YearPromulgate"]);
                }
                this.nationalPromulgate = reader["NationalPromulgate"].ToString();
                this.type = GenericData<int>.GetDataOrDefault(reader["Type"], this.type);
                if (!string.IsNullOrEmpty(reader["LangID"].ToString()))
                {
                    this.langID = Convert.ToInt32(reader["LangID"]);
                }
                this.fTS = reader["FTS"].ToString();
                this.contentDoc = reader["ContentDoc"].ToString();
                if (getone)
                {
                    this.coQuanName = reader["CoQuanName"].ToString();
                    this.linhVucName = reader["LinhVucName"].ToString();
                    this.loaiVbName = reader["LoaiVbName"].ToString();
                }

            }

        }

        /// <summary>
        /// Persists a new instance of md_Document. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBDocument.Create(
                this.moduleID,
                this.pageID,
                this.siteID,
                this.summary,
                this.sign,
                this.datePromulgate,
                this.dateEffect,
                this.signer,
                this.filePath,
                this.coQuanID,
                this.loaiVB,
                this.linhVuc,
                this.itemUrl,
                this.createdByUser,
                this.isApproved,
                this.approvedDate,
                this.approvedGuid,
                this.createdByUserGuid,
                this.yearPromulgate,
                this.nationalPromulgate,
                this.langID,
                this.fTS,
                this.contentDoc,
                this.type);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of md_Document. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBDocument.Update(
                this.itemID,
                this.moduleID,
                this.pageID,
                this.siteID,
                this.summary,
                this.sign,
                this.datePromulgate,
                this.dateEffect,
                this.signer,
                this.filePath,
                this.coQuanID,
                this.loaiVB,
                this.linhVuc,
                this.itemUrl,
                this.createdByUser,
                this.isApproved,
                this.approvedDate,
                this.approvedGuid,
                this.createdByUserGuid,
                this.yearPromulgate,
                this.nationalPromulgate,
                this.langID,
                this.fTS,
                this.contentDoc,
                this.type);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of md_Document. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        public bool Save()
        {
            QLLog qlLog = new QLLog()
            {
                DiaChiIP = Common.findMyIP().ToString(),
                Type = KieuLogConstant.LogQuanTriVanBan,
                DuongDanThaoTac = HttpContext.Current.Request.Url.PathAndQuery,
                CreatedBy = this.createdByUser,
                CreatedByUser = -1,
                CreatedDate = DateTime.Now
            };

            if (this.itemID > 0)
            {
                qlLog.NoiDung = "Cập nhập văn bản mã số: " + this.ItemID;
                qlLog.HanhDongThaoTac = KieuLogConstant.CapNhapDuLieu;
                qlLog.Save();
                return Update();
            }
            else
            {
                qlLog.NoiDung = "Thêm mới văn bản";
                qlLog.HanhDongThaoTac = KieuLogConstant.ThemMoi;
                qlLog.Save();
                return Create();
            }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Deletes an instance of md_Document. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBDocument.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of md_Document. 
        /// </summary>
        public static int GetCount(int siteId, int moduleId, int linhVuc, int loaiVb, int coQuan, int? status, int namBanHanh, int chuDe, string keyword)
        {
            return DBDocument.GetCount(siteId, moduleId, linhVuc, loaiVb, coQuan, status, namBanHanh, chuDe, keyword);
        }

        public static int GetCountByLinhVuc(int siteId, int moduleId, int linhVuc)
        {
            return DBDocument.GetCountByLinhVuc(siteId, moduleId, linhVuc);
        }

        private static List<Documentation> LoadListFromReader(IDataReader reader, bool getpage = false, bool getbyid = false)
        {
            List<Documentation> md_DocumentList = new List<Documentation>();
            try
            {
                while (reader.Read())
                {
                    Documentation md_Document = new Documentation();
                    md_Document.itemID = Convert.ToInt32(reader["ItemID"]);
                    md_Document.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    md_Document.pageID = Convert.ToInt32(reader["PageID"]);
                    md_Document.siteID = Convert.ToInt32(reader["SiteID"]);
                    md_Document.summary = reader["Summary"].ToString();
                    md_Document.sign = reader["Sign"].ToString();
                    if (!string.IsNullOrEmpty(reader["DatePromulgate"].ToString()))
                    {
                        md_Document.datePromulgate = Convert.ToDateTime(reader["DatePromulgate"]);
                    }

                    if (!string.IsNullOrEmpty(reader["DateEffect"].ToString()))
                    {
                        md_Document.dateEffect = Convert.ToDateTime(reader["DateEffect"]);
                    }

                    md_Document.signer = reader["Signer"].ToString();
                    md_Document.filePath = reader["FilePath"].ToString();
                    md_Document.coQuanID = Convert.ToInt32(reader["CoQuanID"]);
                    md_Document.loaiVB = Convert.ToInt32(reader["LoaiVB"]);
                    md_Document.linhVuc = Convert.ToInt32(reader["LinhVuc"]);
                    md_Document.itemUrl = reader["ItemUrl"].ToString();
                    md_Document.createdByUser = reader["CreatedByUser"].ToString();
                    md_Document.isApproved = Convert.ToBoolean(reader["IsApproved"]);
                    md_Document.approvedDate = Convert.ToDateTime(reader["ApprovedDate"]);
                    if (!string.IsNullOrEmpty(reader["ApprovedGuid"].ToString()))
                    {
                        md_Document.approvedGuid = new Guid(reader["ApprovedGuid"].ToString());
                    }
                    md_Document.createdByUserGuid = new Guid(reader["CreatedByUserGuid"].ToString());
                    if (!string.IsNullOrEmpty(reader["YearPromulgate"].ToString()))
                    {
                        md_Document.yearPromulgate = Convert.ToInt32(reader["YearPromulgate"]);
                    }
                    md_Document.nationalPromulgate = reader["NationalPromulgate"].ToString();
                    if (!string.IsNullOrEmpty(reader["LangID"].ToString()))
                    {
                        md_Document.langID = Convert.ToInt32(reader["LangID"]);
                    }
                    md_Document.fTS = reader["FTS"].ToString();
                    md_Document.contentDoc = reader["ContentDoc"].ToString();
                    if (getpage || getbyid)
                    {
                        md_Document.linhVucName = reader["LinhVucName"].ToString();
                        md_Document.loaiVbName = reader["LoaiVBName"].ToString();
                        md_Document.coQuanName = reader["CoQuanName"].ToString();
                    }
                    md_Document.type = GenericData<int>.GetDataOrDefault(reader["Type"], md_Document.type);

                    if (md_Document.approvedDate.HasValue && md_Document.approvedDate.Value.AddDays(5) >= DateTime.Now.ToString("dd/MM/yyyy").ToDateTimeNotNull())
                    {
                        md_Document.IsNew = true;
                    }


                    md_DocumentList.Add(md_Document);

                }
            }
            finally
            {
                reader.Close();
            }

            return md_DocumentList;

        }
        public static DataTable GetDocumentByPage(int siteId, int pageId)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ItemID", typeof(int));
            dataTable.Columns.Add("ModuleID", typeof(int));
            dataTable.Columns.Add("ModuleTitle", typeof(string));
            dataTable.Columns.Add("Sign", typeof(string));
            dataTable.Columns.Add("ItemUrl", typeof(string));
            dataTable.Columns.Add("Summary", typeof(string));
            dataTable.Columns.Add("DatePromulgate", typeof(DateTime));
            dataTable.Columns.Add("ViewRoles", typeof(string));

            using (IDataReader reader = DBDocument.GetDocumentByPage(siteId, pageId))
            {
                while (reader.Read())
                {
                    DataRow row = dataTable.NewRow();

                    row["ItemID"] = reader["ItemID"];
                    row["ModuleID"] = reader["ModuleID"];
                    row["ModuleTitle"] = reader["ModuleTitle"];
                    row["Sign"] = reader["Sign"];
                    row["ItemUrl"] = reader["ItemUrl"];
                    row["Summary"] = reader["Summary"];
                    row["DatePromulgate"] = Convert.ToDateTime(reader["DatePromulgate"]);
                    row["ViewRoles"] = reader["ViewRoles"];

                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
        }
        /// <summary>
        /// Gets an IList with all instances of md_Document.
        /// </summary>
        public static List<Documentation> GetAll(int siteID)
        {
            IDataReader reader = DBDocument.GetAll(siteID);
            return LoadListFromReader(reader);

        }

        public static List<Documentation> GetHotNew(int siteID, int SoLuong)
        {
            IDataReader reader = DBDocument.GetHotNew(siteID, SoLuong);
            return LoadListFromReader(reader);

        }

        public static List<Documentation> GetAllByYear(int siteID)
        {
            IDataReader reader = DBDocument.GetAllByYear(siteID);
            return LoadListFromReader(reader);

        }
        public static List<Documentation> GetSelectYear()
        {
            IDataReader reader = DBDocument.GetSelectYear();
            return LoadListFromReader(reader, true);

        }
        public static List<Documentation> GetById(int itemId)
        {
            IDataReader reader = DBDocument.GetById(itemId);
            return LoadListFromReader(reader, true);
        }
        public static List<Documentation> GetTopSlide(int siteId, int number)
        {
            IDataReader reader = DBDocument.GetTopSlide(siteId, number);
            return LoadListFromReader(reader);
        }
        public static List<Documentation> GetOthers(int itemId, int siteId, int moduleId, int linhVuc, int number)
        {
            IDataReader reader = DBDocument.GetOthers(itemId, siteId, moduleId, linhVuc, number);
            return LoadListFromReader(reader);
        }

        /// <summary>
        /// Gets an IList with page of instances of md_Document.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<Documentation> GetPage(int siteId, int moduleId, int linhVuc, int loaiVb, int coQuan, int pageNumber, int pageSize, int? status, int namBanHanh, int chuDe, string keyword, out int totalPages, out int totalItems)
        {
            totalPages = 1;
            IDataReader reader = DBDocument.GetPage(siteId, moduleId, linhVuc, loaiVb, coQuan, pageNumber, pageSize, status, namBanHanh, chuDe, keyword, out totalPages, out totalItems);
            return LoadListFromReader(reader, true);
        }

        public static List<Documentation> GetPageAll(int siteId, int moduleId, int linhVuc, int loaiVb, int coQuan, int pageNumber, int pageSize, int? status, int namBanHanh, int chuDe, string keyword, int nhomVB, out int totalPages, out int totalItems)
        {
            totalPages = 1;
            IDataReader reader = DBDocument.GetPageAll(siteId, moduleId, linhVuc, loaiVb, coQuan, pageNumber, pageSize, status, namBanHanh, chuDe, keyword, nhomVB, out totalPages, out totalItems);
            return LoadListFromReader(reader, true);
        }
        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of md_Document.
        /// </summary>
        public static int CompareByItemID(Documentation md_Document1, Documentation md_Document2)
        {
            return md_Document1.ItemID.CompareTo(md_Document2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of md_Document.
        /// </summary>
        public static int CompareByModuleID(Documentation md_Document1, Documentation md_Document2)
        {
            return md_Document1.ModuleID.CompareTo(md_Document2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of md_Document.
        /// </summary>
        public static int CompareByPageID(Documentation md_Document1, Documentation md_Document2)
        {
            return md_Document1.PageID.CompareTo(md_Document2.PageID);
        }
        /// <summary>
        /// Compares 2 instances of md_Document.
        /// </summary>
        public static int CompareBySiteID(Documentation md_Document1, Documentation md_Document2)
        {
            return md_Document1.SiteID.CompareTo(md_Document2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of md_Document.
        /// </summary>
        public static int CompareBySummary(Documentation md_Document1, Documentation md_Document2)
        {
            return md_Document1.Summary.CompareTo(md_Document2.Summary);
        }
        /// <summary>
        /// Compares 2 instances of md_Document.
        /// </summary>
        public static int CompareBySign(Documentation md_Document1, Documentation md_Document2)
        {
            return md_Document1.Sign.CompareTo(md_Document2.Sign);
        }
        /// <summary>
        /// Compares 2 instances of md_Document.
        /// </summary>
        //public static int CompareByDatePromulgate(Documentation md_Document1, Documentation md_Document2)
        //{
        //    return md_Document1.DatePromulgate.CompareTo(md_Document2.DatePromulgate);
        //}
        /// <summary>
        /// Compares 2 instances of md_Document.
        /// </summary>
        //public static int CompareByDateEffect(Documentation md_Document1, Documentation md_Document2)
        //{
        //    return md_Document1.DateEffect.CompareTo(md_Document2.DateEffect);
        //}
        /// <summary>
        /// Compares 2 instances of md_Document.
        /// </summary>
        public static int CompareBySigner(Documentation md_Document1, Documentation md_Document2)
        {
            return md_Document1.Signer.CompareTo(md_Document2.Signer);
        }
        /// <summary>
        /// Compares 2 instances of md_Document.
        /// </summary>
        public static int CompareByFilePath(Documentation md_Document1, Documentation md_Document2)
        {
            return md_Document1.FilePath.CompareTo(md_Document2.FilePath);
        }
        /// <summary>
        /// Compares 2 instances of md_Document.
        /// </summary>
        public static int CompareByCoQuanID(Documentation md_Document1, Documentation md_Document2)
        {
            return md_Document1.CoQuanID.CompareTo(md_Document2.CoQuanID);
        }
        /// <summary>
        /// Compares 2 instances of md_Document.
        /// </summary>
        public static int CompareByLoaiVB(Documentation md_Document1, Documentation md_Document2)
        {
            return md_Document1.LoaiVB.CompareTo(md_Document2.LoaiVB);
        }
        /// <summary>
        /// Compares 2 instances of md_Document.
        /// </summary>
        public static int CompareByLinhVuc(Documentation md_Document1, Documentation md_Document2)
        {
            return md_Document1.LinhVuc.CompareTo(md_Document2.LinhVuc);
        }
        /// <summary>
        /// Compares 2 instances of md_Document.
        /// </summary>
        public static int CompareByItemUrl(Documentation md_Document1, Documentation md_Document2)
        {
            return md_Document1.ItemUrl.CompareTo(md_Document2.ItemUrl);
        }
        /// <summary>
        /// Compares 2 instances of md_Document.
        /// </summary>
        public static int CompareByCreatedByUser(Documentation md_Document1, Documentation md_Document2)
        {
            return md_Document1.CreatedByUser.CompareTo(md_Document2.CreatedByUser);
        }
        /// <summary>
        /// Compares 2 instances of md_Document.
        /// </summary>
        //public static int CompareByApprovedDate(Documentation md_Document1, Documentation md_Document2)
        //{
        //    return md_Document1.ApprovedDate.CompareTo(md_Document2.ApprovedDate);
        //}

        #endregion


    }

}







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
using mojoPortal.Data;

namespace mojoPortal.Business
{

    public class DocumentReference
    {
        private const string featureGuid = "a040f25b-a4a0-4bb4-b072-31b30a8ae814";

        public static Guid FeatureGuid
        {
            get { return new Guid(featureGuid); }
        }
        #region Constructors

        public DocumentReference()
        { }


        public DocumentReference(
            int itemID)
        {
            Getmd_DocumentReference(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int moduleID = -1;
        private int pageID = -1;
        private int siteID = -1;
        private int rootItemID = -1;
        private string summary = string.Empty;
        private string sign = string.Empty;
        private DateTime? datePromulgate = DateTime.UtcNow;
        private DateTime? dateEffect = DateTime.UtcNow;
        private string signer = string.Empty;
        private string filePath = string.Empty;
        private int coQuanID = -1;
        private int loaiVB = -1;
        private int linhVuc = -1;
        private string itemUrl = string.Empty;
        private string createdByUser = string.Empty;
        private bool isApproved = false;
        private DateTime approvedDate = DateTime.UtcNow;
        private Guid approvedGuid = Guid.Empty;
        private Guid createdByUserGuid = Guid.Empty;
        private string linhVucName = string.Empty;
        private string loaiVbName = string.Empty;
        private string coQuanName = string.Empty;
        private int yearPromulgate = -1;
        private int langId = -1;
        private string nationalPromulgate = string.Empty;

        #endregion

        #region Public Properties

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
        public int RootItemID
        {
            get { return rootItemID; }
            set { rootItemID = value; }
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
        public DateTime ApprovedDate
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
        public int LangID
        {
            get { return langId; }
            set { langId = value; }
        }
        public string NationalPromulgate
        {
            get { return nationalPromulgate; }
            set { nationalPromulgate = value; }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of md_Document.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void Getmd_DocumentReference(
            int itemID)
        {
            using (IDataReader reader = DBDocumentReference.GetOne(
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
                this.rootItemID = Convert.ToInt32(reader["RootItemID"]);
                this.summary = reader["Summary"].ToString();
                this.sign = reader["Sign"].ToString();
                this.datePromulgate = Convert.ToDateTime(reader["DatePromulgate"]);
                this.dateEffect = Convert.ToDateTime(reader["DateEffect"]);
                this.signer = reader["Signer"].ToString();
                this.filePath = reader["FilePath"].ToString();
                this.coQuanID = Convert.ToInt32(reader["CoQuanID"]);
                this.loaiVB = Convert.ToInt32(reader["LoaiVB"]);
                this.linhVuc = Convert.ToInt32(reader["LinhVuc"]);
                this.itemUrl = reader["ItemUrl"].ToString();
                this.createdByUser = reader["CreatedByUser"].ToString();
                this.isApproved = Convert.ToBoolean(reader["IsApproved"]);
                this.approvedDate = Convert.ToDateTime(reader["ApprovedDate"]);
                this.approvedGuid = new Guid(reader["ApprovedGuid"].ToString());
                this.createdByUserGuid = new Guid(reader["CreatedByUserGuid"].ToString());
                this.yearPromulgate = Convert.ToInt32(reader["YearPromulgate"]);
                this.langId = Convert.ToInt32(reader["LangID"]);
                this.nationalPromulgate = reader["NationalPromulgate"].ToString();
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

            newID = DBDocumentReference.Create(
                this.moduleID,
                this.pageID,
                this.siteID,
                this.rootItemID,
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
                this.langId,
                this.nationalPromulgate);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of md_Document. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBDocumentReference.Update(
                this.itemID,
                this.moduleID,
                this.pageID,
                this.siteID,
                this.rootItemID,
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
                this.langId,
                this.nationalPromulgate);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of md_Document. Returns true on success.
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
        /// Deletes an instance of md_Document. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBDocumentReference.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of md_Document. 
        /// </summary>
        public static int GetCount(int siteId, int moduleId, int linhVuc, int loaiVb, int coQuan, bool? status, int namBanHanh, string keyword)
        {
            return DBDocumentReference.GetCount(siteId, moduleId, linhVuc, loaiVb, coQuan, status, namBanHanh, keyword);
        }

        public static int GetCountByLinhVuc(int siteId, int moduleId, int linhVuc)
        {
            return DBDocumentReference.GetCountByLinhVuc(siteId, moduleId, linhVuc);
        }

        private static List<DocumentReference> LoadListFromReader(IDataReader reader, bool getpage = false, bool getbyid = false, bool getdocumentbyrootid = false)
        {
            List<DocumentReference> md_DocumentList = new List<DocumentReference>();
            try
            {
                while (reader.Read())
                {

                    DocumentReference md_Document = new DocumentReference();
                    md_Document.itemID = Convert.ToInt32(reader["ItemID"]);
                    md_Document.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    md_Document.pageID = Convert.ToInt32(reader["PageID"]);
                    md_Document.siteID = Convert.ToInt32(reader["SiteID"]);
                    md_Document.rootItemID = Convert.ToInt32(reader["RootItemID"]);
                    md_Document.summary = reader["Summary"].ToString();
                    md_Document.sign = reader["Sign"].ToString();
                    md_Document.datePromulgate = Convert.ToDateTime(reader["DatePromulgate"]);
                    md_Document.dateEffect = Convert.ToDateTime(reader["DateEffect"]);
                    md_Document.signer = reader["Signer"].ToString();
                    md_Document.filePath = reader["FilePath"].ToString();
                    md_Document.coQuanID = Convert.ToInt32(reader["CoQuanID"]);
                    md_Document.loaiVB = Convert.ToInt32(reader["LoaiVB"]);
                    md_Document.linhVuc = Convert.ToInt32(reader["LinhVuc"]);
                    md_Document.itemUrl = reader["ItemUrl"].ToString();
                    md_Document.createdByUser = reader["CreatedByUser"].ToString();
                    md_Document.isApproved = Convert.ToBoolean(reader["IsApproved"]);
                    md_Document.approvedDate = Convert.ToDateTime(reader["ApprovedDate"]);
                    md_Document.approvedGuid = new Guid(reader["ApprovedGuid"].ToString());
                    md_Document.createdByUserGuid = new Guid(reader["CreatedByUserGuid"].ToString());
                    md_Document.yearPromulgate = Convert.ToInt32(reader["YearPromulgate"]);
                    md_Document.langId = Convert.ToInt32(reader["LangID"]);
                    md_Document.nationalPromulgate = reader["NationalPromulgate"].ToString();
                    if (getpage || getbyid || getdocumentbyrootid)
                    {
                        md_Document.linhVucName = reader["LinhVucName"].ToString();
                        md_Document.loaiVbName = reader["LoaiVBName"].ToString();
                        md_Document.coQuanName = reader["CoQuanName"].ToString();
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

        /// <summary>
        /// Gets an IList with all instances of md_Document.
        /// </summary>
        public static List<DocumentReference> GetAll()
        {
            IDataReader reader = DBDocumentReference.GetAll();
            return LoadListFromReader(reader);

        }

        public static List<DocumentReference> GetAllByYear()
        {
            IDataReader reader = DBDocumentReference.GetAllByYear();
            return LoadListFromReader(reader);

        }
        public static List<DocumentReference> GetSelectYear()
        {
            IDataReader reader = DBDocumentReference.GetSelectYear();
            return LoadListFromReader(reader, true);

        }
        public static List<DocumentReference> GetById(int itemId)
        {
            IDataReader reader = DBDocumentReference.GetById(itemId);
            return LoadListFromReader(reader, true);
        }
        public static List<DocumentReference> GetTopSlide(int siteId, int number)
        {
            IDataReader reader = DBDocumentReference.GetTopSlide(siteId, number);
            return LoadListFromReader(reader);
        }
        public static List<DocumentReference> GetDocumentByRootId(int RootItemId)
        {
            IDataReader reader = DBDocumentReference.GetDocumentByRootId(RootItemId);
            return LoadListFromReader(reader, true);
        }
        public static List<DocumentReference> GetOthers(int itemId, int siteId, int moduleId, int linhVuc, int number)
        {
            IDataReader reader = DBDocumentReference.GetOthers(itemId, siteId, moduleId, linhVuc, number);
            return LoadListFromReader(reader);
        }

        /// <summary>
        /// Gets an IList with page of instances of md_Document.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<DocumentReference> GetPage(int siteId, int moduleId, int linhVuc, int loaiVb, int coQuan, int pageNumber, int pageSize, bool? status, int namBanHanh, string keyword, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBDocumentReference.GetPage(siteId, moduleId, linhVuc, loaiVb, coQuan, pageNumber, pageSize, status, namBanHanh, keyword, out totalPages);
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






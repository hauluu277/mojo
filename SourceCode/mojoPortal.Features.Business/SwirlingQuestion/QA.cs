
// Author:					TrieuBV
// Created:					2015-9-21
// Last Modified:			2015-9-21

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using SwirlingQuestionFeature.Data;

namespace SwirlingQuestionFeature.Business
{

    public class QA
    {

        #region Constructors

        public QA()
        { }


        public QA(
            Guid guid)
        {
            GetQA(
                guid);
        }

        #endregion

        #region Private Properties

        private Guid guid = Guid.Empty;
        private int siteID = -1;
        private int moduleID = -1;
        private string title = string.Empty;
        private string question = string.Empty;
        private string answer = string.Empty;
        private bool isPublished = false;
        private DateTime lastModified = DateTime.UtcNow;
        private string createdByName = string.Empty;
        private string createdByEmail = string.Empty;
        private string createdByPhone = string.Empty;
        private int answerUser = -1;
        private int hitCount = -1;
        private string itemUrl = string.Empty;
        private bool isHot = false;
        private int commentCount = -1;
        private int qACategoryID = -1;
        private int coQuanID = -1;
        private bool is_Active = false;
        private string lyDoXoa = string.Empty;
        private bool viPhamQuyChe = false;
        private string ghiChu = string.Empty;
        private bool duyetCauHoi = false;
        private bool duThaoTraLoi = false;
        private string fileDinhKem = string.Empty;
        private bool duyetDuThaoTraLoi = false;
        private string lyDoKhongPheDuyetTraLoi = string.Empty;
        private bool xuatBanDuThaoTraLoi = false;
        private Guid nguoiXoaCauHoi = Guid.Empty;
        private Guid nguoiKiemDuyet = Guid.Empty;
        private DateTime ngayXoaCauHoi = DateTime.UtcNow;
        private DateTime ngayKiemDuyet = DateTime.UtcNow;
        private Guid nguoiXuatBanCauHoi = Guid.Empty;
        private DateTime ngayXuatBanCauHoi = DateTime.UtcNow;
        private Guid nguoiTaoDuThaoTraLoi = Guid.Empty;
        private DateTime ngayTaoDuThao = DateTime.UtcNow;
        private Guid nguoiPheDuyetDuThao = Guid.Empty;
        private DateTime ngayPheDuyet = DateTime.UtcNow;
        private Guid nguoiXuatBanCauTraLoi = Guid.Empty;
        private DateTime ngayXuatBanCauTraLoi = DateTime.UtcNow;
        private bool tiepNhanCauHoi = false;
        private string fts = string.Empty;
        private bool khongDuyetDuThao = false;
        private int phongBanID = -1;
        private bool duyetThongBao=false;
        private Guid nguoiDuyetThongBao=Guid.Empty;
        private DateTime ngayDuyetThongBao=DateTime.UtcNow;
        private bool xuatBanThongBao =false;
        private Guid nguoiXuatBanThongBao=Guid.Empty;
        private DateTime ngayXuatBanThongBao = DateTime.UtcNow;
        private int creatQuestionByUser;
		private	DateTime ngayTiepNhan=DateTime.UtcNow;
        private int nguoiTiepNhan;
        private bool statusSend;
		private bool chuyenDuThao;
		private int nguoiChuyenDuThao;
        private DateTime ngayChuyenDuThao = DateTime.UtcNow;
        #endregion

        #region Public Properties

        public Guid Guid
        {
            get { return guid; }
            set { guid = value; }
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
        public string Question
        {
            get { return question; }
            set { question = value; }
        }
        public string Answer
        {
            get { return answer; }
            set { answer = value; }
        }
        public bool IsPublished
        {
            get { return isPublished; }
            set { isPublished = value; }
        }
        public DateTime LastModified
        {
            get { return lastModified; }
            set { lastModified = value; }
        }
        public string CreatedByName
        {
            get { return createdByName; }
            set { createdByName = value; }
        }
        public string CreatedByEmail
        {
            get { return createdByEmail; }
            set { createdByEmail = value; }
        }
        public string CreatedByPhone
        {
            get { return createdByPhone; }
            set { createdByPhone = value; }
        }
        public int AnswerUser
        {
            get { return answerUser; }
            set { answerUser = value; }
        }
        public int HitCount
        {
            get { return hitCount; }
            set { hitCount = value; }
        }
        public string ItemUrl
        {
            get { return itemUrl; }
            set { itemUrl = value; }
        }
        public bool IsHot
        {
            get { return isHot; }
            set { isHot = value; }
        }
        public int CommentCount
        {
            get { return commentCount; }
            set { commentCount = value; }
        }
        public int QACategoryID
        {
            get { return qACategoryID; }
            set { qACategoryID = value; }
        }
        public int CoQuanID
        {
            get { return coQuanID; }
            set { coQuanID = value; }
        }
        public bool Is_Active
        {
            get { return is_Active; }
            set { is_Active = value; }
        }
        public string LyDoXoa
        {
            get { return lyDoXoa; }
            set { lyDoXoa = value; }
        }
        public bool ViPhamQuyChe
        {
            get { return viPhamQuyChe; }
            set { viPhamQuyChe = value; }
        }
        public string GhiChu
        {
            get { return ghiChu; }
            set { ghiChu = value; }
        }
        public bool DuyetCauHoi
        {
            get { return duyetCauHoi; }
            set { duyetCauHoi = value; }
        }
        public bool DuThaoTraLoi
        {
            get { return duThaoTraLoi; }
            set { duThaoTraLoi = value; }
        }
        public string FileDinhKem
        {
            get { return fileDinhKem; }
            set { fileDinhKem = value; }
        }
        public bool DuyetDuThaoTraLoi
        {
            get { return duyetDuThaoTraLoi; }
            set { duyetDuThaoTraLoi = value; }
        }
        public string LyDoKhongPheDuyetTraLoi
        {
            get { return lyDoKhongPheDuyetTraLoi; }
            set { lyDoKhongPheDuyetTraLoi = value; }
        }
        public bool XuatBanDuThaoTraLoi
        {
            get { return xuatBanDuThaoTraLoi; }
            set { xuatBanDuThaoTraLoi = value; }
        }
        public Guid NguoiXoaCauHoi
        {
            get { return nguoiXoaCauHoi; }
            set { nguoiXoaCauHoi = value; }
        }
        public Guid NguoiKiemDuyet
        {
            get { return nguoiKiemDuyet; }
            set { nguoiKiemDuyet = value; }
        }
        public DateTime NgayXoaCauHoi
        {
            get { return ngayXoaCauHoi; }
            set { ngayXoaCauHoi = value; }
        }
        public DateTime NgayKiemDuyet
        {
            get { return ngayKiemDuyet; }
            set { ngayKiemDuyet = value; }
        }
        public Guid NguoiXuatBanCauHoi
        {
            get { return nguoiXuatBanCauHoi; }
            set { nguoiXuatBanCauHoi = value; }
        }
        public DateTime NgayXuatBanCauHoi
        {
            get { return ngayXuatBanCauHoi; }
            set { ngayXuatBanCauHoi = value; }
        }
        public Guid NguoiTaoDuThaoTraLoi
        {
            get { return nguoiTaoDuThaoTraLoi; }
            set { nguoiTaoDuThaoTraLoi = value; }
        }
        public DateTime NgayTaoDuThao
        {
            get { return ngayTaoDuThao; }
            set { ngayTaoDuThao = value; }
        }
        public Guid NguoiPheDuyetDuThao
        {
            get { return nguoiPheDuyetDuThao; }
            set { nguoiPheDuyetDuThao = value; }
        }
        public DateTime NgayPheDuyet
        {
            get { return ngayPheDuyet; }
            set { ngayPheDuyet = value; }
        }
        public Guid NguoiXuatBanCauTraLoi
        {
            get { return nguoiXuatBanCauTraLoi; }
            set { nguoiXuatBanCauTraLoi = value; }
        }
        public DateTime NgayXuatBanCauTraLoi
        {
            get { return ngayXuatBanCauTraLoi; }
            set { ngayXuatBanCauTraLoi = value; }
        }
        public bool TiepNhanCauHoi
        {
            get { return tiepNhanCauHoi; }
            set { tiepNhanCauHoi = value; }
        }
        public string FTS
        {
            get { return fts; }
            set { fts = value; }
        }
        public bool KhongDuyetDuThao
        {
            get { return khongDuyetDuThao; }
            set { khongDuyetDuThao = value; }
        }
        public int PhongBanID
        {
            get { return phongBanID; }
            set { phongBanID = value; }
        }
        public bool DuyetThongBao
        {
            get { return duyetThongBao; }
            set { duyetThongBao = value; }
        }
        public Guid NguoiDuyetThongBao
        {
            get { return nguoiDuyetThongBao; }
            set { nguoiDuyetThongBao = value; }
        }
        public DateTime NgayDuyetThongBao
        {
            get { return ngayDuyetThongBao; }
            set { ngayDuyetThongBao = value; }
        }
        public bool XuatBanThongBao
        {
            get { return xuatBanThongBao; }
            set { xuatBanThongBao = value; }
        }
        public Guid NguoiXuatBanThongBao
        {
            get { return nguoiXuatBanThongBao; }
            set { nguoiXuatBanThongBao = value; }
        }
        public DateTime NgayXuatBanThongBao
        {
            get { return ngayXuatBanThongBao; }
            set { ngayXuatBanThongBao = value; }
        }
        public int CreatQuestionByUser
        {
            get { return creatQuestionByUser; }
            set { creatQuestionByUser = value; }
        }
        public DateTime NgayTiepNhan
        {
            get { return ngayTiepNhan; }
            set { ngayTiepNhan = value; }
        }
        public int NguoiTiepNhan
        {
            get { return nguoiTiepNhan; }
            set { nguoiTiepNhan = value; }
        }
        public bool StatusSend
        {
            get { return statusSend; }
            set { statusSend = value; }
        }
        public bool ChuyenDuThao
        {
            get { return chuyenDuThao; }
            set { chuyenDuThao = value; }
        }
        public int NguoiChuyenDuThao
        {
            get { return nguoiChuyenDuThao; }
            set { nguoiChuyenDuThao = value; }
        }
        public DateTime NgayChuyenDuThao
        {
            get { return ngayChuyenDuThao; }
            set { ngayChuyenDuThao = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of QA.
        /// </summary>
        /// <param name="guid"> guid </param>
        private void GetQA(
            Guid guid)
        {
            using (IDataReader reader = DBQA.GetOne(
                guid))
            {
                PopulateFromReader(reader);
            }

        }


        private void PopulateFromReader(IDataReader reader)
        {
            if (reader.Read())
            {
                this.guid = new Guid(reader["Guid"].ToString());
                this.moduleID = Convert.ToInt32(reader["ModuleID"]);
                this.title = reader["Title"].ToString();
                this.question = reader["Question"].ToString();
                this.answer = reader["Answer"].ToString();
                this.isPublished = Convert.ToBoolean(reader["IsPublished"]);
                this.lastModified = Convert.ToDateTime(reader["LastModified"]);
                this.createdByName = reader["CreatedByName"].ToString();
                this.createdByEmail = reader["CreatedByEmail"].ToString();
                this.createdByPhone = reader["CreatedByPhone"].ToString();
                this.answerUser = Convert.ToInt32(reader["AnswerUser"]);
                this.hitCount = Convert.ToInt32(reader["HitCount"]);
                this.itemUrl = reader["ItemUrl"].ToString();
                this.isHot = Convert.ToBoolean(reader["IsHot"]);
                this.commentCount = Convert.ToInt32(reader["CommentCount"]);
                this.qACategoryID = Convert.ToInt32(reader["QACategoryID"]);
                if (!string.IsNullOrEmpty(reader["CoQuanID"].ToString()))
                {
                    this.coQuanID = Convert.ToInt32(reader["CoQuanID"]);
                }
                if (!string.IsNullOrEmpty(reader["Is_Active"].ToString()))
                {
                    this.is_Active = Convert.ToBoolean(reader["Is_Active"]);
                }
                this.lyDoXoa = reader["LyDoXoa"].ToString();
                if (!string.IsNullOrEmpty(reader["ViPhamQuyChe"].ToString()))
                {
                    this.viPhamQuyChe = Convert.ToBoolean(reader["ViPhamQuyChe"]);
                }
                this.ghiChu = reader["GhiChu"].ToString();
                if (!string.IsNullOrEmpty(reader["DuyetCauHoi"].ToString()))
                {
                    this.duyetCauHoi = Convert.ToBoolean(reader["DuyetCauHoi"]);
                }
                if (!string.IsNullOrEmpty(reader["DuThaoTraLoi"].ToString()))
                {
                    this.duThaoTraLoi = Convert.ToBoolean(reader["DuThaoTraLoi"]);
                }
                this.fileDinhKem = reader["FileDinhKem"].ToString();
                if (!string.IsNullOrEmpty(reader["DuyetDuThaoTraLoi"].ToString()))
                {
                    this.duyetDuThaoTraLoi = Convert.ToBoolean(reader["DuyetDuThaoTraLoi"]);
                }
                this.lyDoKhongPheDuyetTraLoi = reader["LyDoKhongPheDuyetTraLoi"].ToString();
                if (!string.IsNullOrEmpty(reader["XuatBanDuThaoTraLoi"].ToString()))
                {
                    this.xuatBanDuThaoTraLoi = Convert.ToBoolean(reader["XuatBanDuThaoTraLoi"]);
                }
                if (!string.IsNullOrEmpty(reader["NguoiXoaCauHoi"].ToString()))
                {
                    this.nguoiXoaCauHoi = new Guid(reader["NguoiXoaCauHoi"].ToString());
                }
                if (!string.IsNullOrEmpty(reader["NguoiKiemDuyet"].ToString()))
                {
                    this.nguoiKiemDuyet = new Guid(reader["NguoiKiemDuyet"].ToString());
                }
                if (!string.IsNullOrEmpty(reader["NgayXoaCauHoi"].ToString()))
                {
                    this.ngayXoaCauHoi = Convert.ToDateTime(reader["NgayXoaCauHoi"]);
                }
                if (!string.IsNullOrEmpty(reader["NgayKiemDuyet"].ToString()))
                {
                    this.ngayKiemDuyet = Convert.ToDateTime(reader["NgayKiemDuyet"]);
                }
                if (!string.IsNullOrEmpty(reader["NguoiXuatBanCauHoi"].ToString()))
                {
                    this.nguoiXuatBanCauHoi = new Guid(reader["NguoiXuatBanCauHoi"].ToString());
                }
                if (!string.IsNullOrEmpty(reader["NgayXuatBanCauHoi"].ToString()))
                {
                    this.ngayXuatBanCauHoi = Convert.ToDateTime(reader["NgayXuatBanCauHoi"]);
                }
                if (!string.IsNullOrEmpty(reader["NguoiTaoDuThaoTraLoi"].ToString()))
                {
                    this.nguoiTaoDuThaoTraLoi = new Guid(reader["NguoiTaoDuThaoTraLoi"].ToString());
                }
                if (!string.IsNullOrEmpty(reader["NgayTaoDuThao"].ToString()))
                {
                    this.ngayTaoDuThao = Convert.ToDateTime(reader["NgayTaoDuThao"]);
                }
                if (!string.IsNullOrEmpty(reader["NguoiPheDuyetDuThao"].ToString()))
                {
                    this.nguoiPheDuyetDuThao = new Guid(reader["NguoiPheDuyetDuThao"].ToString());
                }
                if (!string.IsNullOrEmpty(reader["NgayPheDuyet"].ToString()))
                {
                    this.ngayPheDuyet = Convert.ToDateTime(reader["NgayPheDuyet"]);
                }
                if (!string.IsNullOrEmpty(reader["NguoiXuatBanCauTraLoi"].ToString()))
                {
                    this.nguoiXuatBanCauTraLoi = new Guid(reader["NguoiXuatBanCauTraLoi"].ToString());
                }
                if (!string.IsNullOrEmpty(reader["NgayXuatBanCauTraLoi"].ToString()))
                {
                    this.ngayXuatBanCauTraLoi = Convert.ToDateTime(reader["NgayXuatBanCauTraLoi"]);
                }
                if (!string.IsNullOrEmpty(reader["TiepNhanCauHoi"].ToString()))
                {
                    this.tiepNhanCauHoi = Convert.ToBoolean(reader["TiepNhanCauHoi"]);
                }
                this.fts = reader["FTS"].ToString();
                if (!string.IsNullOrEmpty(reader["KhongPheDuyetTraLoi"].ToString()))
                {
                    this.duyetDuThaoTraLoi = Convert.ToBoolean(reader["KhongPheDuyetTraLoi"]);
                }
                if (!string.IsNullOrEmpty(reader["PhongBanID"].ToString()))
                {
                    this.phongBanID = Convert.ToInt32(reader["PhongBanID"]);
                }
                if (!string.IsNullOrEmpty(reader["DuyetThongBao"].ToString()))
                {
                    this.duyetThongBao = Convert.ToBoolean(reader["DuyetThongBao"]);
                }
                if (!string.IsNullOrEmpty(reader["NguoiDuyetThongBao"].ToString()))
                {
                    this.nguoiDuyetThongBao = new Guid(reader["NguoiDuyetThongBao"].ToString());
                }
                if (!string.IsNullOrEmpty(reader["NgayDuyetThongBao"].ToString()))
                {
                    this.ngayDuyetThongBao = Convert.ToDateTime(reader["NgayDuyetThongBao"]);
                }
                if (!string.IsNullOrEmpty(reader["XuatBanThongBao"].ToString()))
                {
                    this.xuatBanThongBao = Convert.ToBoolean(reader["XuatBanThongBao"]);
                }
                if (!string.IsNullOrEmpty(reader["NguoiXuatBanThongBao"].ToString()))
                {
                    this.nguoiXuatBanThongBao =new Guid(reader["NguoiXuatBanThongBao"].ToString());
                }
                if (!string.IsNullOrEmpty(reader["NgayXuatBanThongBao"].ToString()))
                {
                    this.ngayXuatBanThongBao = Convert.ToDateTime(reader["NgayXuatBanThongBao"].ToString());
                }
                if (!string.IsNullOrEmpty(reader["CreatQuestionByUser"].ToString()))
                {
                    this.creatQuestionByUser = Convert.ToInt32(reader["CreatQuestionByUser"].ToString());
                }
                if (!string.IsNullOrEmpty(reader["NgayTiepNhan"].ToString()))
                {
                    this.ngayTiepNhan = Convert.ToDateTime(reader["NgayTiepNhan"].ToString());
                }
                if (!string.IsNullOrEmpty(reader["NguoiTiepNhan"].ToString()))
                {
                    this.nguoiTiepNhan = Convert.ToInt32(reader["NguoiTiepNhan"].ToString());
                }
                if (!string.IsNullOrEmpty(reader["StatusSend"].ToString()))
                {
                    this.statusSend = Convert.ToBoolean(reader["StatusSend"].ToString());
                }
                if (!string.IsNullOrEmpty(reader["ChuyenDuThao"].ToString()))
                {
                    this.chuyenDuThao = Convert.ToBoolean(reader["ChuyenDuThao"].ToString());
                }
                if (!string.IsNullOrEmpty(reader["NguoiChuyenDuThao"].ToString()))
                {
                    this.nguoiChuyenDuThao = Convert.ToInt32(reader["NguoiChuyenDuThao"].ToString());
                }
                if (!string.IsNullOrEmpty(reader["NgayChuyenDuThao"].ToString()))
                {
                    this.ngayChuyenDuThao = Convert.ToDateTime(reader["NgayChuyenDuThao"].ToString());
                }
            }

        }

        /// <summary>
        /// Persists a new instance of QA. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            this.guid = Guid.NewGuid();

            int rowsAffected = DBQA.Create(
                this.guid,
                this.moduleID,
                this.title,
                this.question,
                this.answer,
                this.isPublished,
                this.lastModified,
                this.createdByName,
                this.createdByEmail,
                this.createdByPhone,
                this.answerUser,
                this.hitCount,
                this.itemUrl,
                this.isHot,
                this.commentCount,
                this.qACategoryID,
                this.coQuanID,
                this.is_Active,
                this.lyDoXoa,
                this.viPhamQuyChe,
                this.ghiChu,
                this.duyetCauHoi,
                this.duThaoTraLoi,
                this.fileDinhKem,
                this.duyetDuThaoTraLoi,
                this.lyDoKhongPheDuyetTraLoi,
                this.xuatBanDuThaoTraLoi,
                this.nguoiXoaCauHoi,
                this.nguoiKiemDuyet,
                this.ngayXoaCauHoi,
                this.ngayKiemDuyet,
                this.nguoiXuatBanCauHoi,
                this.ngayXuatBanCauHoi,
                this.nguoiTaoDuThaoTraLoi,
                this.ngayTaoDuThao,
                this.nguoiPheDuyetDuThao,
                this.ngayPheDuyet,
                this.nguoiXuatBanCauTraLoi,
                this.ngayXuatBanCauTraLoi,
                this.tiepNhanCauHoi,
                this.fts,
                this.khongDuyetDuThao,
                this.phongBanID,
                this.duyetThongBao,
                this.nguoiDuyetThongBao,
                this.ngayDuyetThongBao,
                this.xuatBanThongBao,
                this.nguoiXuatBanThongBao,
                this.ngayXuatBanThongBao,
                 this.creatQuestionByUser,
                 this.ngayTiepNhan,
                 this.nguoiTiepNhan,
                 this.statusSend,
                 this.chuyenDuThao,
                 this.nguoiChuyenDuThao,
                 this.ngayChuyenDuThao);

            return (rowsAffected > 0);

        }

        /// <summary>
        /// Updates this instance of QA. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBQA.Update(
                this.guid,
                this.moduleID,
                this.title,
                this.question,
                this.answer,
                this.isPublished,
                this.lastModified,
                this.createdByName,
                this.createdByEmail,
                this.createdByPhone,
                this.answerUser,
                this.hitCount,
                this.itemUrl,
                this.isHot,
                this.commentCount,
                this.qACategoryID,
                this.coQuanID,
                this.is_Active,
                this.lyDoXoa,
                this.viPhamQuyChe,
                this.ghiChu,
                this.duyetCauHoi,
                this.duThaoTraLoi,
                this.fileDinhKem,
                this.duyetDuThaoTraLoi,
                this.lyDoKhongPheDuyetTraLoi,
                this.xuatBanDuThaoTraLoi,
                this.nguoiXoaCauHoi,
                this.nguoiKiemDuyet,
                this.ngayXoaCauHoi,
                this.ngayKiemDuyet,
                this.nguoiXuatBanCauHoi,
                this.ngayXuatBanCauHoi,
                this.nguoiTaoDuThaoTraLoi,
                this.ngayTaoDuThao,
                this.nguoiPheDuyetDuThao,
                this.ngayPheDuyet,
                this.nguoiXuatBanCauTraLoi,
                this.ngayXuatBanCauTraLoi,
                this.tiepNhanCauHoi,
                this.fts,
                this.khongDuyetDuThao,
                this.phongBanID,
                this.duyetThongBao,
                this.nguoiDuyetThongBao,
                this.ngayDuyetThongBao,
                this.xuatBanThongBao,
                this.nguoiXuatBanThongBao,
                this.ngayXuatBanThongBao,
                this.creatQuestionByUser,
                 this.ngayTiepNhan,
                 this.nguoiTiepNhan,
                 this.statusSend,
                 this.chuyenDuThao,
                 this.nguoiChuyenDuThao,
                 this.ngayChuyenDuThao);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of QA. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        public bool Save()
        {
            if (this.guid != Guid.Empty)
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
        /// Deletes an instance of QA. Returns true on success.
        /// </summary>
        /// <param name="guid"> guid </param>
        /// <returns>bool</returns>
        public static bool Delete(
            Guid guid)
        {
            return DBQA.Delete(
                guid);
        }


        /// <summary>
        /// Gets a count of QA. 
        /// </summary>
        public static int GetCount()
        {
            return DBQA.GetCount();
        }

        private static List<QA> LoadListFromReader(IDataReader reader)
        {
            List<QA> qAList = new List<QA>();
            try
            {
                while (reader.Read())
                {
                    QA qA = new QA();
                    qA.guid = new Guid(reader["Guid"].ToString());
                    qA.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    qA.title = reader["Title"].ToString();
                    qA.question = reader["Question"].ToString();
                    qA.answer = reader["Answer"].ToString();
                    qA.isPublished = Convert.ToBoolean(reader["IsPublished"]);
                    qA.lastModified = Convert.ToDateTime(reader["LastModified"]);
                    qA.createdByName = reader["CreatedByName"].ToString();
                    qA.createdByEmail = reader["CreatedByEmail"].ToString();
                    qA.createdByPhone = reader["CreatedByPhone"].ToString();
                    qA.answerUser = Convert.ToInt32(reader["AnswerUser"]);
                    qA.hitCount = Convert.ToInt32(reader["HitCount"]);
                    qA.itemUrl = reader["ItemUrl"].ToString();
                    qA.isHot = Convert.ToBoolean(reader["IsHot"]);
                    qA.commentCount = Convert.ToInt32(reader["CommentCount"]);
                    qA.qACategoryID = Convert.ToInt32(reader["QACategoryID"]);
                    qA.coQuanID = Convert.ToInt32(reader["CoQuanID"]);
                    qA.is_Active = Convert.ToBoolean(reader["Is_Active"]);
                    qA.lyDoXoa = reader["LyDoXoa"].ToString();
                    qA.viPhamQuyChe = Convert.ToBoolean(reader["ViPhamQuyChe"]);
                    qA.ghiChu = reader["GhiChu"].ToString();
                    qA.duyetCauHoi = Convert.ToBoolean(reader["DuyetCauHoi"]);
                    qA.duThaoTraLoi = Convert.ToBoolean(reader["DuThaoTraLoi"]);
                    qA.fileDinhKem = reader["FileDinhKem"].ToString();
                    qA.duyetDuThaoTraLoi = Convert.ToBoolean(reader["DuyetDuThaoTraLoi"]);
                    qA.lyDoKhongPheDuyetTraLoi = reader["LyDoKhongPheDuyetTraLoi"].ToString();
                    qA.xuatBanDuThaoTraLoi = Convert.ToBoolean(reader["XuatBanDuThaoTraLoi"]);
                    qA.nguoiXoaCauHoi = new Guid(reader["NguoiXoaCauHoi"].ToString());
                    qA.nguoiKiemDuyet = new Guid(reader["NguoiKiemDuyet"].ToString());
                    qA.ngayXoaCauHoi = Convert.ToDateTime(reader["NgayXoaCauHoi"]);
                    qA.ngayKiemDuyet = Convert.ToDateTime(reader["NgayKiemDuyet"]);
                    qA.nguoiXuatBanCauHoi = new Guid(reader["NguoiXuatBanCauHoi"].ToString());
                    qA.ngayXuatBanCauHoi = Convert.ToDateTime(reader["NgayXuatBanCauHoi"]);
                    qA.nguoiTaoDuThaoTraLoi = new Guid(reader["NguoiTaoDuThaoTraLoi"].ToString());
                    qA.ngayTaoDuThao = Convert.ToDateTime(reader["NgayTaoDuThao"]);
                    qA.nguoiPheDuyetDuThao = new Guid(reader["NguoiPheDuyetDuThao"].ToString());
                    qA.ngayPheDuyet = Convert.ToDateTime(reader["NgayPheDuyet"]);
                    qA.nguoiXuatBanCauTraLoi = new Guid(reader["NguoiXuatBanCauTraLoi"].ToString());
                    qA.ngayXuatBanCauTraLoi = Convert.ToDateTime(reader["NgayXuatBanCauTraLoi"]);
                    qA.fts = reader["FTS"].ToString();
                    if (!string.IsNullOrEmpty(reader["CreatQuestionByUser"].ToString()))
                    {
                        qA.creatQuestionByUser = Convert.ToInt32(reader["CreatQuestionByUser"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["NgayTiepNhan"].ToString()))
                    {
                        qA.ngayTiepNhan = Convert.ToDateTime(reader["NgayTiepNhan"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["NguoiTiepNhan"].ToString()))
                    {
                        qA.nguoiTiepNhan = Convert.ToInt32(reader["NguoiTiepNhan"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["StatusSend"].ToString()))
                    {
                        qA.statusSend = Convert.ToBoolean(reader["StatusSend"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["ChuyenDuThao"].ToString()))
                    {
                        qA.chuyenDuThao = Convert.ToBoolean(reader["ChuyenDuThao"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["NguoiChuyenDuThao"].ToString()))
                    {
                        qA.nguoiChuyenDuThao = Convert.ToInt32(reader["NguoiChuyenDuThao"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["NgayChuyenDuThao"].ToString()))
                    {
                        qA.ngayChuyenDuThao = Convert.ToDateTime(reader["NgayChuyenDuThao"].ToString());
                    }

                    qAList.Add(qA);

                }
            }
            finally
            {
                reader.Close();
            }

            return qAList;

        }

        /// <summary>
        /// Gets an IList with all instances of QA.
        /// </summary>
        public static List<QA> GetAll()
        {
            IDataReader reader = DBQA.GetAll();
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of QA.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<QA> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBQA.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of QA.
        /// </summary>
        public static int CompareByModuleID(QA qA1, QA qA2)
        {
            return qA1.ModuleID.CompareTo(qA2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of QA.
        /// </summary>
        public static int CompareByTitle(QA qA1, QA qA2)
        {
            return qA1.Title.CompareTo(qA2.Title);
        }
        /// <summary>
        /// Compares 2 instances of QA.
        /// </summary>
        public static int CompareByQuestion(QA qA1, QA qA2)
        {
            return qA1.Question.CompareTo(qA2.Question);
        }
        /// <summary>
        /// Compares 2 instances of QA.
        /// </summary>
        public static int CompareByAnswer(QA qA1, QA qA2)
        {
            return qA1.Answer.CompareTo(qA2.Answer);
        }
        /// <summary>
        /// Compares 2 instances of QA.
        /// </summary>
        public static int CompareByLastModified(QA qA1, QA qA2)
        {
            return qA1.LastModified.CompareTo(qA2.LastModified);
        }
        /// <summary>
        /// Compares 2 instances of QA.
        /// </summary>
        public static int CompareByCreatedByName(QA qA1, QA qA2)
        {
            return qA1.CreatedByName.CompareTo(qA2.CreatedByName);
        }
        /// <summary>
        /// Compares 2 instances of QA.
        /// </summary>
        public static int CompareByCreatedByEmail(QA qA1, QA qA2)
        {
            return qA1.CreatedByEmail.CompareTo(qA2.CreatedByEmail);
        }
        /// <summary>
        /// Compares 2 instances of QA.
        /// </summary>
        public static int CompareByCreatedByPhone(QA qA1, QA qA2)
        {
            return qA1.CreatedByPhone.CompareTo(qA2.CreatedByPhone);
        }
        /// <summary>
        /// Compares 2 instances of QA.
        /// </summary>
        public static int CompareByAnswerUser(QA qA1, QA qA2)
        {
            return qA1.AnswerUser.CompareTo(qA2.AnswerUser);
        }
        /// <summary>
        /// Compares 2 instances of QA.
        /// </summary>
        public static int CompareByHitCount(QA qA1, QA qA2)
        {
            return qA1.HitCount.CompareTo(qA2.HitCount);
        }
        /// <summary>
        /// Compares 2 instances of QA.
        /// </summary>
        public static int CompareByItemUrl(QA qA1, QA qA2)
        {
            return qA1.ItemUrl.CompareTo(qA2.ItemUrl);
        }
        /// <summary>
        /// Compares 2 instances of QA.
        /// </summary>
        public static int CompareByCommentCount(QA qA1, QA qA2)
        {
            return qA1.CommentCount.CompareTo(qA2.CommentCount);
        }
        /// <summary>
        /// Compares 2 instances of QA.
        /// </summary>
        public static int CompareByQACategoryID(QA qA1, QA qA2)
        {
            return qA1.QACategoryID.CompareTo(qA2.QACategoryID);
        }
        /// <summary>
        /// Compares 2 instances of QA.
        /// </summary>
        public static int CompareByCoQuanID(QA qA1, QA qA2)
        {
            return qA1.CoQuanID.CompareTo(qA2.CoQuanID);
        }
        /// <summary>
        /// Compares 2 instances of QA.
        /// </summary>
        public static int CompareByLyDoXoa(QA qA1, QA qA2)
        {
            return qA1.LyDoXoa.CompareTo(qA2.LyDoXoa);
        }
        /// <summary>
        /// Compares 2 instances of QA.
        /// </summary>
        public static int CompareByGhiChu(QA qA1, QA qA2)
        {
            return qA1.GhiChu.CompareTo(qA2.GhiChu);
        }
        /// <summary>
        /// Compares 2 instances of QA.
        /// </summary>
        public static int CompareByFileDinhKem(QA qA1, QA qA2)
        {
            return qA1.FileDinhKem.CompareTo(qA2.FileDinhKem);
        }
        /// <summary>
        /// Compares 2 instances of QA.
        /// </summary>
        public static int CompareByLyDoKhongPheDuyetTraLoi(QA qA1, QA qA2)
        {
            return qA1.LyDoKhongPheDuyetTraLoi.CompareTo(qA2.LyDoKhongPheDuyetTraLoi);
        }
        /// <summary>
        /// Compares 2 instances of QA.
        /// </summary>
        public static int CompareByNgayXoaCauHoi(QA qA1, QA qA2)
        {
            return qA1.NgayXoaCauHoi.CompareTo(qA2.NgayXoaCauHoi);
        }
        /// <summary>
        /// Compares 2 instances of QA.
        /// </summary>
        public static int CompareByNgayKiemDuyet(QA qA1, QA qA2)
        {
            return qA1.NgayKiemDuyet.CompareTo(qA2.NgayKiemDuyet);
        }
        /// <summary>
        /// Compares 2 instances of QA.
        /// </summary>
        public static int CompareByNgayXuatBanCauHoi(QA qA1, QA qA2)
        {
            return qA1.NgayXuatBanCauHoi.CompareTo(qA2.NgayXuatBanCauHoi);
        }
        /// <summary>
        /// Compares 2 instances of QA.
        /// </summary>
        public static int CompareByNgayTaoDuThao(QA qA1, QA qA2)
        {
            return qA1.NgayTaoDuThao.CompareTo(qA2.NgayTaoDuThao);
        }
        /// <summary>
        /// Compares 2 instances of QA.
        /// </summary>
        public static int CompareByNgayPheDuyet(QA qA1, QA qA2)
        {
            return qA1.NgayPheDuyet.CompareTo(qA2.NgayPheDuyet);
        }
        /// <summary>
        /// Compares 2 instances of QA.
        /// </summary>
        public static int CompareByNgayXuatBanCauTraLoi(QA qA1, QA qA2)
        {
            return qA1.NgayXuatBanCauTraLoi.CompareTo(qA2.NgayXuatBanCauTraLoi);
        }

        #endregion


    }

}






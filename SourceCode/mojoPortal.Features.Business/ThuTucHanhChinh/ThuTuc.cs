
// Author:					hauld
// Created:					2023-6-22
// Last Modified:			2023-6-22
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

    public class ThuTuc
    {

        #region Constructors

        public ThuTuc()
        { }


        public ThuTuc(
            long itemID)
        {
            Getcore_ThuTuc(
                itemID);
        }

        #endregion

        #region Private Properties

        private long itemID;
        private int idCoQuan = -1;
        private int idMucDo = -1;
        private int idCapDoThuTuc = -1;
        private string maThuTuc = string.Empty;
        private string tenThuTuc = string.Empty;
        private int idLinhVuc = -1;
        private string cachThucThucHien = string.Empty;
        private int idDoiTuongThucHien = -1;
        private string trinhTuThucHien = string.Empty;
        private string thoiHanGianQuyet = string.Empty;
        private string phi = string.Empty;
        private string lePhi = string.Empty;
        private string thanhPhanHoSo = string.Empty;
        private int soLuongHoSo = -1;
        private string yeuCauDieuKien = string.Empty;
        private string canCuPhapLy = string.Empty;
        private string ketQuaThucHien = string.Empty;
        private string linkDVC = string.Empty;
        private bool isPublish = false;
        private string createdBy = string.Empty;
        private int createdByUser = -1;
        private DateTime createdDate = DateTime.UtcNow;
        private int siteID = -1;
        private DateTime editDate = DateTime.UtcNow;
        private int editByUser = -1;
        private string mucDoName = string.Empty;
        private string linhVucName = string.Empty;
        #endregion

        #region Public Properties
        public string MucDoName
        {
            get
            {
                return mucDoName;
            }

            set { mucDoName = value; }
        }
        public string LinhVucName
        {
            get { return linhVucName; }
            set { linhVucName = value; }
        }


        public long ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public int IdCoQuan
        {
            get { return idCoQuan; }
            set { idCoQuan = value; }
        }
        public int IdMucDo
        {
            get { return idMucDo; }
            set { idMucDo = value; }
        }
        public int IdCapDoThuTuc
        {
            get { return idCapDoThuTuc; }
            set { idCapDoThuTuc = value; }
        }
        public string MaThuTuc
        {
            get { return maThuTuc; }
            set { maThuTuc = value; }
        }
        public string TenThuTuc
        {
            get { return tenThuTuc; }
            set { tenThuTuc = value; }
        }
        public int IdLinhVuc
        {
            get { return idLinhVuc; }
            set { idLinhVuc = value; }
        }
        public string CachThucThucHien
        {
            get { return cachThucThucHien; }
            set { cachThucThucHien = value; }
        }
        public int IdDoiTuongThucHien
        {
            get { return idDoiTuongThucHien; }
            set { idDoiTuongThucHien = value; }
        }
        public string TrinhTuThucHien
        {
            get { return trinhTuThucHien; }
            set { trinhTuThucHien = value; }
        }
        public string ThoiHanGianQuyet
        {
            get { return thoiHanGianQuyet; }
            set { thoiHanGianQuyet = value; }
        }
        public string Phi
        {
            get { return phi; }
            set { phi = value; }
        }
        public string LePhi
        {
            get { return lePhi; }
            set { lePhi = value; }
        }
        public string ThanhPhanHoSo
        {
            get { return thanhPhanHoSo; }
            set { thanhPhanHoSo = value; }
        }
        public int SoLuongHoSo
        {
            get { return soLuongHoSo; }
            set { soLuongHoSo = value; }
        }
        public string YeuCauDieuKien
        {
            get { return yeuCauDieuKien; }
            set { yeuCauDieuKien = value; }
        }
        public string CanCuPhapLy
        {
            get { return canCuPhapLy; }
            set { canCuPhapLy = value; }
        }
        public string KetQuaThucHien
        {
            get { return ketQuaThucHien; }
            set { ketQuaThucHien = value; }
        }
        public string LinkDVC
        {
            get { return linkDVC; }
            set { linkDVC = value; }
        }
        public bool IsPublish
        {
            get { return isPublish; }
            set { isPublish = value; }
        }
        public string CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }
        public int CreatedByUser
        {
            get { return createdByUser; }
            set { createdByUser = value; }
        }
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }
        public int SiteID
        {
            get { return siteID; }
            set { siteID = value; }
        }
        public DateTime EditDate
        {
            get { return editDate; }
            set { editDate = value; }
        }
        public int EditByUser
        {
            get { return editByUser; }
            set { editByUser = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of core_ThuTuc.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void Getcore_ThuTuc(
            long itemID)
        {
            using (IDataReader reader = DBcore_ThuTuc.GetOne(
                itemID))
            {
                PopulateFromReader(reader);
            }

        }


        private void PopulateFromReader(IDataReader reader)
        {
            if (reader.Read())
            {
                this.itemID = Convert.ToInt64(reader["ItemID"]);
                if (!string.IsNullOrEmpty(reader["IdCoQuan"].ToString()))
                {
                    this.idCoQuan = Convert.ToInt32(reader["IdCoQuan"]);
                }


                if (!string.IsNullOrEmpty(reader["IdMucDo"].ToString()))
                {
                    this.idMucDo = Convert.ToInt32(reader["IdMucDo"]);
                }



                if (!string.IsNullOrEmpty(reader["IdCapDoThuTuc"].ToString()))
                {
                    this.idCapDoThuTuc = Convert.ToInt32(reader["IdCapDoThuTuc"]);
                }

                this.maThuTuc = reader["MaThuTuc"].ToString();
                this.tenThuTuc = reader["TenThuTuc"].ToString();
                this.idLinhVuc = Convert.ToInt32(reader["IdLinhVuc"]);
                this.cachThucThucHien = reader["CachThucThucHien"].ToString();

                if (!string.IsNullOrEmpty(reader["IdDoiTuongThucHien"].ToString()))
                {
                    this.idDoiTuongThucHien = Convert.ToInt32(reader["IdDoiTuongThucHien"]);
                }

                if (!string.IsNullOrEmpty(reader["MucDoName"].ToString()))
                {
                    this.mucDoName = reader["MucDoName"].ToString();
                }

                if (!string.IsNullOrEmpty(reader["LinhVucName"].ToString()))
                {
                    this.linhVucName = reader["LinhVucName"].ToString();
                }



           

                if (!string.IsNullOrEmpty(reader["TrinhTuThucHien"].ToString()))
                {
                    this.trinhTuThucHien = reader["TrinhTuThucHien"].ToString();
                }

    
                if (!string.IsNullOrEmpty(reader["ThoiHanGianQuyet"].ToString()))
                {
                    this.thoiHanGianQuyet = reader["ThoiHanGianQuyet"].ToString();
                }

          
                if (!string.IsNullOrEmpty(reader["Phi"].ToString()))
                {
                    this.phi = reader["Phi"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["LePhi"].ToString()))
                {
                    this.lePhi = reader["LePhi"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["ThanhPhanHoSo"].ToString()))
                {
                    this.thanhPhanHoSo = reader["ThanhPhanHoSo"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["SoLuongHoSo"].ToString()))
                {
                    this.soLuongHoSo = Convert.ToInt32(reader["SoLuongHoSo"]);
                }
                if (!string.IsNullOrEmpty(reader["YeuCauDieuKien"].ToString()))
                {
                    this.yeuCauDieuKien = reader["YeuCauDieuKien"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["CanCuPhapLy"].ToString()))
                {
                    this.canCuPhapLy = reader["CanCuPhapLy"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["KetQuaThucHien"].ToString()))
                {
                    this.ketQuaThucHien = reader["KetQuaThucHien"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["LinkDVC"].ToString()))
                {
                    this.linkDVC = reader["LinkDVC"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["IsPublish"].ToString()))
                {
                    this.isPublish = Convert.ToBoolean(reader["IsPublish"]);
                }
                if (!string.IsNullOrEmpty(reader["CreatedBy"].ToString()))
                {
                    this.createdBy = reader["CreatedBy"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["CreatedByUser"].ToString()))
                {
                    this.createdByUser = Convert.ToInt32(reader["CreatedByUser"]);
                }
                if (!string.IsNullOrEmpty(reader["CreatedDate"].ToString()))
                {
                    this.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                }
                if (!string.IsNullOrEmpty(reader["SiteID"].ToString()))
                {
                    this.siteID = Convert.ToInt32(reader["SiteID"]);
                }
             


                if (!string.IsNullOrEmpty(reader["EditDate"].ToString()))
                {
                    this.editDate = Convert.ToDateTime(reader["EditDate"]);
                }

                if (!string.IsNullOrEmpty(reader["EditByUser"].ToString()))
                {
                    this.editByUser = Convert.ToInt32(reader["EditByUser"]);
                }


            }

        }

        /// <summary>
        /// Persists a new instance of core_ThuTuc. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBcore_ThuTuc.Create(
                this.idCoQuan,
                this.idMucDo,
                this.idCapDoThuTuc,
                this.maThuTuc,
                this.tenThuTuc,
                this.idLinhVuc,
                this.cachThucThucHien,
                this.idDoiTuongThucHien,
                this.trinhTuThucHien,
                this.thoiHanGianQuyet,
                this.phi,
                this.lePhi,
                this.thanhPhanHoSo,
                this.soLuongHoSo,
                this.yeuCauDieuKien,
                this.canCuPhapLy,
                this.ketQuaThucHien,
                this.linkDVC,
                this.isPublish,
                this.createdBy,
                this.createdByUser,
                this.createdDate,
                this.siteID,
                this.editDate,
                this.editByUser);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of core_ThuTuc. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBcore_ThuTuc.Update(
                this.itemID,
                this.idCoQuan,
                this.idMucDo,
                this.idCapDoThuTuc,
                this.maThuTuc,
                this.tenThuTuc,
                this.idLinhVuc,
                this.cachThucThucHien,
                this.idDoiTuongThucHien,
                this.trinhTuThucHien,
                this.thoiHanGianQuyet,
                this.phi,
                this.lePhi,
                this.thanhPhanHoSo,
                this.soLuongHoSo,
                this.yeuCauDieuKien,
                this.canCuPhapLy,
                this.ketQuaThucHien,
                this.linkDVC,
                this.isPublish,
                this.createdBy,
                this.createdByUser,
                this.createdDate,
                this.siteID,
                this.editDate,
                this.editByUser);

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of core_ThuTuc. Returns true on success.
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
        /// Deletes an instance of core_ThuTuc. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            long itemID)
        {
            return DBcore_ThuTuc.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of core_ThuTuc. 
        /// </summary>
        public static int GetCount(int siteId,
            bool? IsPublic,
            int linhVucId,
            int mucDoId,
            int capThuTucId,
            string keyword)
        {
            return DBcore_ThuTuc.GetCount(siteId, IsPublic, linhVucId,
                mucDoId,
                capThuTucId,
                keyword);
        }

        private static List<ThuTuc> LoadListFromReader(IDataReader reader)
        {
            List<ThuTuc> core_ThuTucList = new List<ThuTuc>();
            try
            {
                while (reader.Read())
                {
                    ThuTuc core_ThuTuc = new ThuTuc();
                    core_ThuTuc.itemID = Convert.ToInt64(reader["ItemID"]);
                    if (!string.IsNullOrEmpty(reader["IdCoQuan"].ToString()))
                    {
                        core_ThuTuc.idCoQuan = Convert.ToInt32(reader["IdCoQuan"]);
                    }

                    if (!string.IsNullOrEmpty(reader["IdMucDo"].ToString()))
                    {
                        core_ThuTuc.idMucDo = Convert.ToInt32(reader["IdMucDo"]);
                    }


                    if (!string.IsNullOrEmpty(reader["IdCapDoThuTuc"].ToString()))
                    {
                        core_ThuTuc.idCapDoThuTuc = Convert.ToInt32(reader["IdCapDoThuTuc"]);
                    }


                    if (!string.IsNullOrEmpty(reader["MaThuTuc"].ToString()))
                    {
                        core_ThuTuc.maThuTuc = reader["MaThuTuc"].ToString();
                    }

                    if (!string.IsNullOrEmpty(reader["TenThuTuc"].ToString()))
                    {
                        core_ThuTuc.tenThuTuc = reader["TenThuTuc"].ToString();
                    }

                    if (!string.IsNullOrEmpty(reader["IdLinhVuc"].ToString()))
                    {
                        core_ThuTuc.idLinhVuc = Convert.ToInt32(reader["IdLinhVuc"]);
                    }

                    if (!string.IsNullOrEmpty(reader["CachThucThucHien"].ToString()))
                    {
                        core_ThuTuc.cachThucThucHien = reader["CachThucThucHien"].ToString();
                    }

                    

                    if (!string.IsNullOrEmpty(reader["IdDoiTuongThucHien"].ToString()))
                    {
                        core_ThuTuc.idDoiTuongThucHien = Convert.ToInt32(reader["IdDoiTuongThucHien"]);
                    }

                    if (!string.IsNullOrEmpty(reader["MucDoName"].ToString()))
                    {
                        core_ThuTuc.mucDoName = reader["MucDoName"].ToString();
                    }

                    if (!string.IsNullOrEmpty(reader["LinhVucName"].ToString()))
                    {
                        core_ThuTuc.linhVucName = reader["LinhVucName"].ToString();
                    }

                    if (!string.IsNullOrEmpty(reader["TrinhTuThucHien"].ToString()))
                    {
                        core_ThuTuc.trinhTuThucHien = reader["TrinhTuThucHien"].ToString();
                    }

                    if (!string.IsNullOrEmpty(reader["ThoiHanGianQuyet"].ToString()))
                    {
                        core_ThuTuc.thoiHanGianQuyet = reader["ThoiHanGianQuyet"].ToString();
                    }

                    if (!string.IsNullOrEmpty(reader["Phi"].ToString()))
                    {
                        core_ThuTuc.phi = reader["Phi"].ToString();
                    }

                    if (!string.IsNullOrEmpty(reader["LePhi"].ToString()))
                    {
                        core_ThuTuc.lePhi = reader["LePhi"].ToString();
                    }

                    if (!string.IsNullOrEmpty(reader["ThanhPhanHoSo"].ToString()))
                    {
                        core_ThuTuc.thanhPhanHoSo = reader["ThanhPhanHoSo"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["SoLuongHoSo"].ToString()))
                    {
                        core_ThuTuc.soLuongHoSo = Convert.ToInt32(reader["SoLuongHoSo"]);
                    }

                    if (!string.IsNullOrEmpty(reader["YeuCauDieuKien"].ToString()))
                    {
                        core_ThuTuc.yeuCauDieuKien = reader["YeuCauDieuKien"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["CanCuPhapLy"].ToString()))
                    {
                        core_ThuTuc.canCuPhapLy = reader["CanCuPhapLy"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["KetQuaThucHien"].ToString()))
                    {
                        core_ThuTuc.ketQuaThucHien = reader["KetQuaThucHien"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["LinkDVC"].ToString()))
                    {
                        core_ThuTuc.linkDVC = reader["LinkDVC"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["IsPublish"].ToString()))
                    {
                        core_ThuTuc.isPublish = Convert.ToBoolean(reader["IsPublish"]);
                    }
                    if (!string.IsNullOrEmpty(reader["CreatedBy"].ToString()))
                    {
                        core_ThuTuc.createdBy = reader["CreatedBy"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["CreatedByUser"].ToString()))
                    {
                        core_ThuTuc.createdByUser = Convert.ToInt32(reader["CreatedByUser"]);
                    }
                    if (!string.IsNullOrEmpty(reader["CreatedDate"].ToString()))
                    {
                        core_ThuTuc.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    }
                    if (!string.IsNullOrEmpty(reader["SiteID"].ToString()))
                    {
                        core_ThuTuc.siteID = Convert.ToInt32(reader["SiteID"]);

                    }


                    if (!string.IsNullOrEmpty(reader["EditDate"].ToString()))
                    {
                        core_ThuTuc.editDate = Convert.ToDateTime(reader["EditDate"]);

                    }

                    if (!string.IsNullOrEmpty(reader["EditByUser"].ToString()))
                    {
                        core_ThuTuc.editByUser = Convert.ToInt32(reader["EditByUser"]);
                    }


                    core_ThuTucList.Add(core_ThuTuc);

                }
            }
            finally
            {
                reader.Close();
            }

            return core_ThuTucList;

        }

        /// <summary>
        /// Gets an IList with all instances of core_ThuTuc.
        /// </summary>
        public static List<ThuTuc> GetAll()
        {
            IDataReader reader = DBcore_ThuTuc.GetAll();
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of core_ThuTuc.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<ThuTuc> GetPage(int siteId,
            bool? IsPublic,
            int linhVucId,
            int mucDoId,
            int capThuTucId,
            string keyword, int pageNumber, int pageSize, out int totalPages,out int totalCount)
        {
            totalPages = 1;
            IDataReader reader = DBcore_ThuTuc.GetPage(siteId, IsPublic, linhVucId,
                mucDoId,
                capThuTucId,
                keyword,
                pageNumber, pageSize, out totalPages,out totalCount);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of core_ThuTuc.
        /// </summary>
        public static int CompareByIdCoQuan(ThuTuc core_ThuTuc1,ThuTuc core_ThuTuc2)
        {
            return core_ThuTuc1.IdCoQuan.CompareTo(core_ThuTuc2.IdCoQuan);
        }
        /// <summary>
        /// Compares 2 instances of core_ThuTuc.
        /// </summary>
        public static int CompareByIdMucDo(ThuTuc core_ThuTuc1,ThuTuc core_ThuTuc2)
        {
            return core_ThuTuc1.IdMucDo.CompareTo(core_ThuTuc2.IdMucDo);
        }
        /// <summary>
        /// Compares 2 instances of core_ThuTuc.
        /// </summary>
        public static int CompareByIdCapDoThuTuc(ThuTuc core_ThuTuc1,ThuTuc core_ThuTuc2)
        {
            return core_ThuTuc1.IdCapDoThuTuc.CompareTo(core_ThuTuc2.IdCapDoThuTuc);
        }
        /// <summary>
        /// Compares 2 instances of core_ThuTuc.
        /// </summary>
        public static int CompareByMaThuTuc(ThuTuc core_ThuTuc1,ThuTuc core_ThuTuc2)
        {
            return core_ThuTuc1.MaThuTuc.CompareTo(core_ThuTuc2.MaThuTuc);
        }
        /// <summary>
        /// Compares 2 instances of core_ThuTuc.
        /// </summary>
        public static int CompareByTenThuTuc(ThuTuc core_ThuTuc1,ThuTuc core_ThuTuc2)
        {
            return core_ThuTuc1.TenThuTuc.CompareTo(core_ThuTuc2.TenThuTuc);
        }
        /// <summary>
        /// Compares 2 instances of core_ThuTuc.
        /// </summary>
        public static int CompareByIdLinhVuc(ThuTuc core_ThuTuc1,ThuTuc core_ThuTuc2)
        {
            return core_ThuTuc1.IdLinhVuc.CompareTo(core_ThuTuc2.IdLinhVuc);
        }
        /// <summary>
        /// Compares 2 instances of core_ThuTuc.
        /// </summary>
        public static int CompareByCachThucThucHien(ThuTuc core_ThuTuc1,ThuTuc core_ThuTuc2)
        {
            return core_ThuTuc1.CachThucThucHien.CompareTo(core_ThuTuc2.CachThucThucHien);
        }
        /// <summary>
        /// Compares 2 instances of core_ThuTuc.
        /// </summary>
        public static int CompareByIdDoiTuongThucHien(ThuTuc core_ThuTuc1,ThuTuc core_ThuTuc2)
        {
            return core_ThuTuc1.IdDoiTuongThucHien.CompareTo(core_ThuTuc2.IdDoiTuongThucHien);
        }
        /// <summary>
        /// Compares 2 instances of core_ThuTuc.
        /// </summary>
        public static int CompareByTrinhTuThucHien(ThuTuc core_ThuTuc1,ThuTuc core_ThuTuc2)
        {
            return core_ThuTuc1.TrinhTuThucHien.CompareTo(core_ThuTuc2.TrinhTuThucHien);
        }
        /// <summary>
        /// Compares 2 instances of core_ThuTuc.
        /// </summary>
        public static int CompareByThoiHanGianQuyet(ThuTuc core_ThuTuc1,ThuTuc core_ThuTuc2)
        {
            return core_ThuTuc1.ThoiHanGianQuyet.CompareTo(core_ThuTuc2.ThoiHanGianQuyet);
        }
        /// <summary>
        /// Compares 2 instances of core_ThuTuc.
        /// </summary>
        public static int CompareByPhi(ThuTuc core_ThuTuc1,ThuTuc core_ThuTuc2)
        {
            return core_ThuTuc1.Phi.CompareTo(core_ThuTuc2.Phi);
        }
        /// <summary>
        /// Compares 2 instances of core_ThuTuc.
        /// </summary>
        public static int CompareByLePhi(ThuTuc core_ThuTuc1,ThuTuc core_ThuTuc2)
        {
            return core_ThuTuc1.LePhi.CompareTo(core_ThuTuc2.LePhi);
        }
        /// <summary>
        /// Compares 2 instances of core_ThuTuc.
        /// </summary>
        public static int CompareByThanhPhanHoSo(ThuTuc core_ThuTuc1,ThuTuc core_ThuTuc2)
        {
            return core_ThuTuc1.ThanhPhanHoSo.CompareTo(core_ThuTuc2.ThanhPhanHoSo);
        }
        /// <summary>
        /// Compares 2 instances of core_ThuTuc.
        /// </summary>
        public static int CompareBySoLuongHoSo(ThuTuc core_ThuTuc1,ThuTuc core_ThuTuc2)
        {
            return core_ThuTuc1.SoLuongHoSo.CompareTo(core_ThuTuc2.SoLuongHoSo);
        }
        /// <summary>
        /// Compares 2 instances of core_ThuTuc.
        /// </summary>
        public static int CompareByYeuCauDieuKien(ThuTuc core_ThuTuc1,ThuTuc core_ThuTuc2)
        {
            return core_ThuTuc1.YeuCauDieuKien.CompareTo(core_ThuTuc2.YeuCauDieuKien);
        }
        /// <summary>
        /// Compares 2 instances of core_ThuTuc.
        /// </summary>
        public static int CompareByCanCuPhapLy(ThuTuc core_ThuTuc1,ThuTuc core_ThuTuc2)
        {
            return core_ThuTuc1.CanCuPhapLy.CompareTo(core_ThuTuc2.CanCuPhapLy);
        }
        /// <summary>
        /// Compares 2 instances of core_ThuTuc.
        /// </summary>
        public static int CompareByKetQuaThucHien(ThuTuc core_ThuTuc1,ThuTuc core_ThuTuc2)
        {
            return core_ThuTuc1.KetQuaThucHien.CompareTo(core_ThuTuc2.KetQuaThucHien);
        }
        /// <summary>
        /// Compares 2 instances of core_ThuTuc.
        /// </summary>
        public static int CompareByLinkDVC(ThuTuc core_ThuTuc1,ThuTuc core_ThuTuc2)
        {
            return core_ThuTuc1.LinkDVC.CompareTo(core_ThuTuc2.LinkDVC);
        }
        /// <summary>
        /// Compares 2 instances of core_ThuTuc.
        /// </summary>
        public static int CompareByCreatedBy(ThuTuc core_ThuTuc1,ThuTuc core_ThuTuc2)
        {
            return core_ThuTuc1.CreatedBy.CompareTo(core_ThuTuc2.CreatedBy);
        }
        /// <summary>
        /// Compares 2 instances of core_ThuTuc.
        /// </summary>
        public static int CompareByCreatedByUser(ThuTuc core_ThuTuc1,ThuTuc core_ThuTuc2)
        {
            return core_ThuTuc1.CreatedByUser.CompareTo(core_ThuTuc2.CreatedByUser);
        }
        /// <summary>
        /// Compares 2 instances of core_ThuTuc.
        /// </summary>
        public static int CompareByCreatedDate(ThuTuc core_ThuTuc1,ThuTuc core_ThuTuc2)
        {
            return core_ThuTuc1.CreatedDate.CompareTo(core_ThuTuc2.CreatedDate);
        }
        /// <summary>
        /// Compares 2 instances of core_ThuTuc.
        /// </summary>
        public static int CompareBySiteID(ThuTuc core_ThuTuc1,ThuTuc core_ThuTuc2)
        {
            return core_ThuTuc1.SiteID.CompareTo(core_ThuTuc2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of core_ThuTuc.
        /// </summary>
        public static int CompareByEditDate(ThuTuc core_ThuTuc1,ThuTuc core_ThuTuc2)
        {
            return core_ThuTuc1.EditDate.CompareTo(core_ThuTuc2.EditDate);
        }
        /// <summary>
        /// Compares 2 instances of core_ThuTuc.
        /// </summary>
        public static int CompareByEditByUser(ThuTuc core_ThuTuc1,ThuTuc core_ThuTuc2)
        {
            return core_ThuTuc1.EditByUser.CompareTo(core_ThuTuc2.EditByUser);
        }

        #endregion


    }

}






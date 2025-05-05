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
using System.IO;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using mojoPortal.Data;

namespace mojoPortal.Data
{

    public static class DBcore_ThuTuc
    {


        /// <summary>
        /// Inserts a row in the core_ThuTuc table. Returns new integer id.
        /// </summary>
        /// <param name="idCoQuan"> idCoQuan </param>
        /// <param name="idMucDo"> idMucDo </param>
        /// <param name="idCapDoThuTuc"> idCapDoThuTuc </param>
        /// <param name="maThuTuc"> maThuTuc </param>
        /// <param name="tenThuTuc"> tenThuTuc </param>
        /// <param name="idLinhVuc"> idLinhVuc </param>
        /// <param name="cachThucThucHien"> cachThucThucHien </param>
        /// <param name="idDoiTuongThucHien"> idDoiTuongThucHien </param>
        /// <param name="trinhTuThucHien"> trinhTuThucHien </param>
        /// <param name="thoiHanGianQuyet"> thoiHanGianQuyet </param>
        /// <param name="phi"> phi </param>
        /// <param name="lePhi"> lePhi </param>
        /// <param name="thanhPhanHoSo"> thanhPhanHoSo </param>
        /// <param name="soLuongHoSo"> soLuongHoSo </param>
        /// <param name="yeuCauDieuKien"> yeuCauDieuKien </param>
        /// <param name="canCuPhapLy"> canCuPhapLy </param>
        /// <param name="ketQuaThucHien"> ketQuaThucHien </param>
        /// <param name="linkDVC"> linkDVC </param>
        /// <param name="isPublish"> isPublish </param>
        /// <param name="createdBy"> createdBy </param>
        /// <param name="createdByUser"> createdByUser </param>
        /// <param name="createdDate"> createdDate </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="editDate"> editDate </param>
        /// <param name="editByUser"> editByUser </param>
        /// <returns>int</returns>
        public static int Create(
            int idCoQuan,
            int idMucDo,
            int idCapDoThuTuc,
            string maThuTuc,
            string tenThuTuc,
            int idLinhVuc,
            string cachThucThucHien,
            int idDoiTuongThucHien,
            string trinhTuThucHien,
            string thoiHanGianQuyet,
            string phi,
            string lePhi,
            string thanhPhanHoSo,
            int soLuongHoSo,
            string yeuCauDieuKien,
            string canCuPhapLy,
            string ketQuaThucHien,
            string linkDVC,
            bool isPublish,
            string createdBy,
            int createdByUser,
            DateTime createdDate,
            int siteID,
            DateTime editDate,
            int editByUser)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_ThuTuc_Insert", 25);
            sph.DefineSqlParameter("@IdCoQuan", SqlDbType.Int, ParameterDirection.Input, idCoQuan);
            sph.DefineSqlParameter("@IdMucDo", SqlDbType.Int, ParameterDirection.Input, idMucDo);
            sph.DefineSqlParameter("@IdCapDoThuTuc", SqlDbType.Int, ParameterDirection.Input, idCapDoThuTuc);
            sph.DefineSqlParameter("@MaThuTuc", SqlDbType.NVarChar, 250, ParameterDirection.Input, maThuTuc);
            sph.DefineSqlParameter("@TenThuTuc", SqlDbType.NVarChar, 550, ParameterDirection.Input, tenThuTuc);
            sph.DefineSqlParameter("@IdLinhVuc", SqlDbType.Int, ParameterDirection.Input, idLinhVuc);
            sph.DefineSqlParameter("@CachThucThucHien", SqlDbType.NVarChar, 250, ParameterDirection.Input, cachThucThucHien);
            sph.DefineSqlParameter("@IdDoiTuongThucHien", SqlDbType.Int, ParameterDirection.Input, idDoiTuongThucHien);
            sph.DefineSqlParameter("@TrinhTuThucHien", SqlDbType.NText, ParameterDirection.Input, trinhTuThucHien);
            sph.DefineSqlParameter("@ThoiHanGianQuyet", SqlDbType.NText, ParameterDirection.Input, thoiHanGianQuyet);
            sph.DefineSqlParameter("@Phi", SqlDbType.NText, ParameterDirection.Input, phi);
            sph.DefineSqlParameter("@LePhi", SqlDbType.NText, ParameterDirection.Input, lePhi);
            sph.DefineSqlParameter("@ThanhPhanHoSo", SqlDbType.NText, ParameterDirection.Input, thanhPhanHoSo);
            sph.DefineSqlParameter("@SoLuongHoSo", SqlDbType.Int, ParameterDirection.Input, soLuongHoSo);
            sph.DefineSqlParameter("@YeuCauDieuKien", SqlDbType.NText, ParameterDirection.Input, yeuCauDieuKien);
            sph.DefineSqlParameter("@CanCuPhapLy", SqlDbType.NText, ParameterDirection.Input, canCuPhapLy);
            sph.DefineSqlParameter("@KetQuaThucHien", SqlDbType.NText, ParameterDirection.Input, ketQuaThucHien);
            sph.DefineSqlParameter("@LinkDVC", SqlDbType.NVarChar, 550, ParameterDirection.Input, linkDVC);
            sph.DefineSqlParameter("@IsPublish", SqlDbType.Bit, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@CreatedBy", SqlDbType.NVarChar, 250, ParameterDirection.Input, createdBy);
            sph.DefineSqlParameter("@CreatedByUser", SqlDbType.Int, ParameterDirection.Input, createdByUser);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@EditDate", SqlDbType.DateTime, ParameterDirection.Input, editDate);
            sph.DefineSqlParameter("@EditByUser", SqlDbType.Int, ParameterDirection.Input, editByUser);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the core_ThuTuc table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="idCoQuan"> idCoQuan </param>
        /// <param name="idMucDo"> idMucDo </param>
        /// <param name="idCapDoThuTuc"> idCapDoThuTuc </param>
        /// <param name="maThuTuc"> maThuTuc </param>
        /// <param name="tenThuTuc"> tenThuTuc </param>
        /// <param name="idLinhVuc"> idLinhVuc </param>
        /// <param name="cachThucThucHien"> cachThucThucHien </param>
        /// <param name="idDoiTuongThucHien"> idDoiTuongThucHien </param>
        /// <param name="trinhTuThucHien"> trinhTuThucHien </param>
        /// <param name="thoiHanGianQuyet"> thoiHanGianQuyet </param>
        /// <param name="phi"> phi </param>
        /// <param name="lePhi"> lePhi </param>
        /// <param name="thanhPhanHoSo"> thanhPhanHoSo </param>
        /// <param name="soLuongHoSo"> soLuongHoSo </param>
        /// <param name="yeuCauDieuKien"> yeuCauDieuKien </param>
        /// <param name="canCuPhapLy"> canCuPhapLy </param>
        /// <param name="ketQuaThucHien"> ketQuaThucHien </param>
        /// <param name="linkDVC"> linkDVC </param>
        /// <param name="isPublish"> isPublish </param>
        /// <param name="createdBy"> createdBy </param>
        /// <param name="createdByUser"> createdByUser </param>
        /// <param name="createdDate"> createdDate </param>
        /// <param name="siteID"> siteID </param>
        /// <param name="editDate"> editDate </param>
        /// <param name="editByUser"> editByUser </param>
        /// <returns>bool</returns>
        public static bool Update(
            long itemID,
            int idCoQuan,
            int idMucDo,
            int idCapDoThuTuc,
            string maThuTuc,
            string tenThuTuc,
            int idLinhVuc,
            string cachThucThucHien,
            int idDoiTuongThucHien,
            string trinhTuThucHien,
            string thoiHanGianQuyet,
            string phi,
            string lePhi,
            string thanhPhanHoSo,
            int soLuongHoSo,
            string yeuCauDieuKien,
            string canCuPhapLy,
            string ketQuaThucHien,
            string linkDVC,
            bool isPublish,
            string createdBy,
            int createdByUser,
            DateTime createdDate,
            int siteID,
            DateTime editDate,
            int editByUser)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_ThuTuc_Update", 26);
            sph.DefineSqlParameter("@ItemID", SqlDbType.BigInt, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@IdCoQuan", SqlDbType.Int, ParameterDirection.Input, idCoQuan);
            sph.DefineSqlParameter("@IdMucDo", SqlDbType.Int, ParameterDirection.Input, idMucDo);
            sph.DefineSqlParameter("@IdCapDoThuTuc", SqlDbType.Int, ParameterDirection.Input, idCapDoThuTuc);
            sph.DefineSqlParameter("@MaThuTuc", SqlDbType.NVarChar, 250, ParameterDirection.Input, maThuTuc);
            sph.DefineSqlParameter("@TenThuTuc", SqlDbType.NVarChar, 550, ParameterDirection.Input, tenThuTuc);
            sph.DefineSqlParameter("@IdLinhVuc", SqlDbType.Int, ParameterDirection.Input, idLinhVuc);
            sph.DefineSqlParameter("@CachThucThucHien", SqlDbType.NVarChar, 250, ParameterDirection.Input, cachThucThucHien);
            sph.DefineSqlParameter("@IdDoiTuongThucHien", SqlDbType.Int, ParameterDirection.Input, idDoiTuongThucHien);
            sph.DefineSqlParameter("@TrinhTuThucHien", SqlDbType.NText, ParameterDirection.Input, trinhTuThucHien);
            sph.DefineSqlParameter("@ThoiHanGianQuyet", SqlDbType.NText, ParameterDirection.Input, thoiHanGianQuyet);
            sph.DefineSqlParameter("@Phi", SqlDbType.NText, ParameterDirection.Input, phi);
            sph.DefineSqlParameter("@LePhi", SqlDbType.NText, ParameterDirection.Input, lePhi);
            sph.DefineSqlParameter("@ThanhPhanHoSo", SqlDbType.NText, ParameterDirection.Input, thanhPhanHoSo);
            sph.DefineSqlParameter("@SoLuongHoSo", SqlDbType.Int, ParameterDirection.Input, soLuongHoSo);
            sph.DefineSqlParameter("@YeuCauDieuKien", SqlDbType.NText, ParameterDirection.Input, yeuCauDieuKien);
            sph.DefineSqlParameter("@CanCuPhapLy", SqlDbType.NText, ParameterDirection.Input, canCuPhapLy);
            sph.DefineSqlParameter("@KetQuaThucHien", SqlDbType.NText, ParameterDirection.Input, ketQuaThucHien);
            sph.DefineSqlParameter("@LinkDVC", SqlDbType.NVarChar, 550, ParameterDirection.Input, linkDVC);
            sph.DefineSqlParameter("@IsPublish", SqlDbType.Bit, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@CreatedBy", SqlDbType.NVarChar, 250, ParameterDirection.Input, createdBy);
            sph.DefineSqlParameter("@CreatedByUser", SqlDbType.Int, ParameterDirection.Input, createdByUser);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteID);
            sph.DefineSqlParameter("@EditDate", SqlDbType.DateTime, ParameterDirection.Input, editDate);
            sph.DefineSqlParameter("@EditByUser", SqlDbType.Int, ParameterDirection.Input, editByUser);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the core_ThuTuc table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            long itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "core_ThuTuc_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.BigInt, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the core_ThuTuc table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            long itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_ThuTuc_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.BigInt, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the core_ThuTuc table.
        /// </summary>
        public static int GetCount(int siteId,
            bool? IsPublic,
            int linhVucId,
            int mucDoId,
            int capThuTucId,
            string keyword)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_ThuTuc_GetCount", 6);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@IsPublic", SqlDbType.Bit, ParameterDirection.Input, IsPublic);
            sph.DefineSqlParameter("@LinhVucID", SqlDbType.Int, ParameterDirection.Input, linhVucId);
            sph.DefineSqlParameter("@MucDoID", SqlDbType.Int, ParameterDirection.Input, mucDoId);
            sph.DefineSqlParameter("@CapThuTucID", SqlDbType.Int, ParameterDirection.Input, capThuTucId);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);

            return Convert.ToInt32(sph.ExecuteScalar());

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the core_ThuTuc table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "core_ThuTuc_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the core_ThuTuc table.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static IDataReader GetPage(
            int siteId,
            bool? IsPublic,
            int linhVucId,
            int mucDoId,
            int capThuTucId,
            string keyword,
            int pageNumber,
            int pageSize,
            out int totalPages,out int totalCount)
        {
            totalPages = 1;
            int totalRows
                = GetCount(siteId, IsPublic, linhVucId, mucDoId, capThuTucId, keyword);
            totalCount = totalRows;
            if (pageSize > 0) totalPages = totalRows / pageSize;

            if (totalRows <= pageSize)
            {
                totalPages = 1;
            }
            else
            {
                int remainder;
                Math.DivRem(totalRows, pageSize, out remainder);
                if (remainder > 0)
                {
                    totalPages += 1;
                }
            }

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "core_ThuTuc_SelectPage", 8);
            sph.DefineSqlParameter("@SiteID", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@IsPublic", SqlDbType.Bit, ParameterDirection.Input, IsPublic);
            sph.DefineSqlParameter("@LinhVucID", SqlDbType.Int, ParameterDirection.Input, linhVucId);
            sph.DefineSqlParameter("@MucDoID", SqlDbType.Int, ParameterDirection.Input, mucDoId);
            sph.DefineSqlParameter("@CapThuTucID", SqlDbType.Int, ParameterDirection.Input, capThuTucId);
            sph.DefineSqlParameter("@Keyword", SqlDbType.NVarChar, 255, ParameterDirection.Input, keyword);

            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



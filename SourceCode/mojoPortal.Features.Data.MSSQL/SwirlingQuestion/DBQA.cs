// Author:					TrieuBV
// Created:					2015-9-21
// Last Modified:			2015-9-21

using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using mojoPortal.Data;

namespace SwirlingQuestionFeature.Data
{

    public static class DBQA
    {


        /// <summary>
        /// Inserts a row in the md_QA table. Returns rows affected count.
        /// </summary>
        /// <param name="guid"> guid </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="title"> title </param>
        /// <param name="question"> question </param>
        /// <param name="answer"> answer </param>
        /// <param name="isPublished"> isPublished </param>
        /// <param name="lastModified"> lastModified </param>
        /// <param name="createdByName"> createdByName </param>
        /// <param name="createdByEmail"> createdByEmail </param>
        /// <param name="createdByPhone"> createdByPhone </param>
        /// <param name="answerUser"> answerUser </param>
        /// <param name="hitCount"> hitCount </param>
        /// <param name="itemUrl"> itemUrl </param>
        /// <param name="isHot"> isHot </param>
        /// <param name="commentCount"> commentCount </param>
        /// <param name="qACategoryID"> qACategoryID </param>
        /// <param name="coQuanID"> coQuanID </param>
        /// <param name="is_Active"> is_Active </param>
        /// <param name="lyDoXoa"> lyDoXoa </param>
        /// <param name="viPhamQuyChe"> viPhamQuyChe </param>
        /// <param name="ghiChu"> ghiChu </param>
        /// <param name="duyetCauHoi"> duyetCauHoi </param>
        /// <param name="duThaoTraLoi"> duThaoTraLoi </param>
        /// <param name="fileDinhKem"> fileDinhKem </param>
        /// <param name="duyetDuThaoTraLoi"> duyetDuThaoTraLoi </param>
        /// <param name="lyDoKhongPheDuyetTraLoi"> lyDoKhongPheDuyetTraLoi </param>
        /// <param name="xuatBanDuThaoTraLoi"> xuatBanDuThaoTraLoi </param>
        /// <param name="nguoiXoaCauHoi"> nguoiXoaCauHoi </param>
        /// <param name="nguoiKiemDuyet"> nguoiKiemDuyet </param>
        /// <param name="ngayXoaCauHoi"> ngayXoaCauHoi </param>
        /// <param name="ngayKiemDuyet"> ngayKiemDuyet </param>
        /// <param name="nguoiXuatBanCauHoi"> nguoiXuatBanCauHoi </param>
        /// <param name="ngayXuatBanCauHoi"> ngayXuatBanCauHoi </param>
        /// <param name="nguoiTaoDuThaoTraLoi"> nguoiTaoDuThaoTraLoi </param>
        /// <param name="ngayTaoDuThao"> ngayTaoDuThao </param>
        /// <param name="nguoiPheDuyetDuThao"> nguoiPheDuyetDuThao </param>
        /// <param name="ngayPheDuyet"> ngayPheDuyet </param>
        /// <param name="nguoiXuatBanCauTraLoi"> nguoiXuatBanCauTraLoi </param>
        /// <param name="ngayXuatBanCauTraLoi"> ngayXuatBanCauTraLoi </param>
        /// <returns>int</returns>
        public static int Create(
            Guid guid,
            int moduleID,
            string title,
            string question,
            string answer,
            bool isPublished,
            DateTime lastModified,
            string createdByName,
            string createdByEmail,
            string createdByPhone,
            int answerUser,
            int hitCount,
            string itemUrl,
            bool isHot,
            int commentCount,
            int qACategoryID,
            int coQuanID,
            bool is_Active,
            string lyDoXoa,
            bool viPhamQuyChe,
            string ghiChu,
            bool duyetCauHoi,
            bool duThaoTraLoi,
            string fileDinhKem,
            bool duyetDuThaoTraLoi,
            string lyDoKhongPheDuyetTraLoi,
            bool xuatBanDuThaoTraLoi,
            Guid nguoiXoaCauHoi,
            Guid nguoiKiemDuyet,
            DateTime ngayXoaCauHoi,
            DateTime ngayKiemDuyet,
            Guid nguoiXuatBanCauHoi,
            DateTime ngayXuatBanCauHoi,
            Guid nguoiTaoDuThaoTraLoi,
            DateTime ngayTaoDuThao,
            Guid nguoiPheDuyetDuThao,
            DateTime ngayPheDuyet,
            Guid nguoiXuatBanCauTraLoi,
            DateTime ngayXuatBanCauTraLoi,
            bool tiepNhanCauHoi,
            string fts,
            bool khongDuyetDuThao,
            int phongBanID,
            bool duyetThongBao,
            Guid nguoiDuyetThongBao,
            DateTime ngayDuyetThongBao,
            bool xuatBanThongBao,
            Guid nguoiXuatBanThongBao,
            DateTime ngayXuatBanThongBao,
            int creatQuestionByUser,
			DateTime ngayTiepNhan,
			int	nguoiTiepNhan,
            bool statusSend,
            bool chuyenDuThao,
            int nguoiChuyenDuThao,
            DateTime ngayChuyenDuThao)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_QA_Insert", 56);
            sph.DefineSqlParameter("@Guid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, guid);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 255, ParameterDirection.Input, title);
            sph.DefineSqlParameter("@Question", SqlDbType.NText, ParameterDirection.Input, question);
            sph.DefineSqlParameter("@Answer", SqlDbType.NText, ParameterDirection.Input, answer);
            sph.DefineSqlParameter("@IsPublished", SqlDbType.Bit, ParameterDirection.Input, isPublished);
            sph.DefineSqlParameter("@LastModified", SqlDbType.DateTime, ParameterDirection.Input, lastModified);
            sph.DefineSqlParameter("@CreatedByName", SqlDbType.NVarChar, 255, ParameterDirection.Input, createdByName);
            sph.DefineSqlParameter("@CreatedByEmail", SqlDbType.NVarChar, 255, ParameterDirection.Input, createdByEmail);
            sph.DefineSqlParameter("@CreatedByPhone", SqlDbType.NVarChar, 50, ParameterDirection.Input, createdByPhone);
            sph.DefineSqlParameter("@AnswerUser", SqlDbType.Int, ParameterDirection.Input, answerUser);
            sph.DefineSqlParameter("@HitCount", SqlDbType.Int, ParameterDirection.Input, hitCount);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, itemUrl);
            sph.DefineSqlParameter("@IsHot", SqlDbType.Bit, ParameterDirection.Input, isHot);
            sph.DefineSqlParameter("@CommentCount", SqlDbType.Int, ParameterDirection.Input, commentCount);
            sph.DefineSqlParameter("@QACategoryID", SqlDbType.Int, ParameterDirection.Input, qACategoryID);
            sph.DefineSqlParameter("@CoQuanID", SqlDbType.Int, ParameterDirection.Input, coQuanID);
            sph.DefineSqlParameter("@Is_Active", SqlDbType.Bit, ParameterDirection.Input, is_Active);
            sph.DefineSqlParameter("@LyDoXoa", SqlDbType.NText, ParameterDirection.Input, lyDoXoa);
            sph.DefineSqlParameter("@ViPhamQuyChe", SqlDbType.Bit, ParameterDirection.Input, viPhamQuyChe);
            sph.DefineSqlParameter("@GhiChu", SqlDbType.NText, ParameterDirection.Input, ghiChu);
            sph.DefineSqlParameter("@DuyetCauHoi", SqlDbType.Bit, ParameterDirection.Input, duyetCauHoi);
            sph.DefineSqlParameter("@DuThaoTraLoi", SqlDbType.Bit, ParameterDirection.Input, duThaoTraLoi);
            sph.DefineSqlParameter("@FileDinhKem", SqlDbType.NVarChar, 255, ParameterDirection.Input, fileDinhKem);
            sph.DefineSqlParameter("@DuyetDuThaoTraLoi", SqlDbType.Bit, ParameterDirection.Input, duyetDuThaoTraLoi);
            sph.DefineSqlParameter("@LyDoKhongPheDuyetTraLoi", SqlDbType.NText, ParameterDirection.Input, lyDoKhongPheDuyetTraLoi);
            sph.DefineSqlParameter("@XuatBanDuThaoTraLoi", SqlDbType.Bit, ParameterDirection.Input, xuatBanDuThaoTraLoi);
            sph.DefineSqlParameter("@NguoiXoaCauHoi", SqlDbType.UniqueIdentifier, ParameterDirection.Input, nguoiXoaCauHoi);
            sph.DefineSqlParameter("@NguoiKiemDuyet", SqlDbType.UniqueIdentifier, ParameterDirection.Input, nguoiKiemDuyet);
            sph.DefineSqlParameter("@NgayXoaCauHoi", SqlDbType.DateTime, ParameterDirection.Input, ngayXoaCauHoi);
            sph.DefineSqlParameter("@NgayKiemDuyet", SqlDbType.DateTime, ParameterDirection.Input, ngayKiemDuyet);
            sph.DefineSqlParameter("@NguoiXuatBanCauHoi", SqlDbType.UniqueIdentifier, ParameterDirection.Input, nguoiXuatBanCauHoi);
            sph.DefineSqlParameter("@NgayXuatBanCauHoi", SqlDbType.DateTime, ParameterDirection.Input, ngayXuatBanCauHoi);
            sph.DefineSqlParameter("@NguoiTaoDuThaoTraLoi", SqlDbType.UniqueIdentifier, ParameterDirection.Input, nguoiTaoDuThaoTraLoi);
            sph.DefineSqlParameter("@NgayTaoDuThao", SqlDbType.DateTime, ParameterDirection.Input, ngayTaoDuThao);
            sph.DefineSqlParameter("@NguoiPheDuyetDuThao", SqlDbType.UniqueIdentifier, ParameterDirection.Input, nguoiPheDuyetDuThao);
            sph.DefineSqlParameter("@NgayPheDuyet", SqlDbType.DateTime, ParameterDirection.Input, ngayPheDuyet);
            sph.DefineSqlParameter("@NguoiXuatBanCauTraLoi", SqlDbType.UniqueIdentifier, ParameterDirection.Input, nguoiXuatBanCauTraLoi);
            sph.DefineSqlParameter("@NgayXuatBanCauTraLoi", SqlDbType.DateTime, ParameterDirection.Input, ngayXuatBanCauTraLoi);
            sph.DefineSqlParameter("@TiepNhanCauHoi", SqlDbType.Bit, ParameterDirection.Input, tiepNhanCauHoi);
            sph.DefineSqlParameter("@FTS", SqlDbType.NText, ParameterDirection.Input, fts);
            sph.DefineSqlParameter("@KhongPheDuyetTraLoi", SqlDbType.Bit, ParameterDirection.Input, khongDuyetDuThao);
            sph.DefineSqlParameter("@PhongBanID", SqlDbType.Int, ParameterDirection.Input, phongBanID);
            sph.DefineSqlParameter("@DuyetThongBao", SqlDbType.Bit, ParameterDirection.Input, duyetThongBao);
            sph.DefineSqlParameter("@NguoiDuyetThongBao", SqlDbType.UniqueIdentifier, ParameterDirection.Input, nguoiDuyetThongBao);
            sph.DefineSqlParameter("@NgayDuyetThongBao", SqlDbType.DateTime, ParameterDirection.Input, ngayDuyetThongBao);
            sph.DefineSqlParameter("@XuatBanThongBao", SqlDbType.Bit, ParameterDirection.Input, xuatBanThongBao);
            sph.DefineSqlParameter("@NguoiXuatBanThongBao", SqlDbType.UniqueIdentifier, ParameterDirection.Input, nguoiXuatBanThongBao);
            sph.DefineSqlParameter("@NgayXuatBanThongBao", SqlDbType.DateTime, ParameterDirection.Input, ngayXuatBanThongBao);
            sph.DefineSqlParameter("@CreatQuestionByUser", SqlDbType.Int, ParameterDirection.Input, creatQuestionByUser);
            sph.DefineSqlParameter("@NgayTiepNhan", SqlDbType.DateTime, ParameterDirection.Input, ngayTiepNhan);
            sph.DefineSqlParameter("@NguoiTiepNhan", SqlDbType.Int, ParameterDirection.Input, nguoiTiepNhan);
            sph.DefineSqlParameter("@StatusSend", SqlDbType.Bit, ParameterDirection.Input, statusSend);
            sph.DefineSqlParameter("@ChuyenDuThao", SqlDbType.Bit, ParameterDirection.Input, chuyenDuThao);
            sph.DefineSqlParameter("@NguoiChuyenDuThao", SqlDbType.Int, ParameterDirection.Input, nguoiChuyenDuThao);
            sph.DefineSqlParameter("@NgayChuyenDuThao", SqlDbType.DateTime, ParameterDirection.Input, ngayChuyenDuThao);
            int rowsAffected = sph.ExecuteNonQuery();
            return rowsAffected;

        }


        /// <summary>
        /// Updates a row in the md_QA table. Returns true if row updated.
        /// </summary>
        /// <param name="guid"> guid </param>
        /// <param name="moduleID"> moduleID </param>
        /// <param name="title"> title </param>
        /// <param name="question"> question </param>
        /// <param name="answer"> answer </param>
        /// <param name="isPublished"> isPublished </param>
        /// <param name="lastModified"> lastModified </param>
        /// <param name="createdByName"> createdByName </param>
        /// <param name="createdByEmail"> createdByEmail </param>
        /// <param name="createdByPhone"> createdByPhone </param>
        /// <param name="answerUser"> answerUser </param>
        /// <param name="hitCount"> hitCount </param>
        /// <param name="itemUrl"> itemUrl </param>
        /// <param name="isHot"> isHot </param>
        /// <param name="commentCount"> commentCount </param>
        /// <param name="qACategoryID"> qACategoryID </param>
        /// <param name="coQuanID"> coQuanID </param>
        /// <param name="is_Active"> is_Active </param>
        /// <param name="lyDoXoa"> lyDoXoa </param>
        /// <param name="viPhamQuyChe"> viPhamQuyChe </param>
        /// <param name="ghiChu"> ghiChu </param>
        /// <param name="duyetCauHoi"> duyetCauHoi </param>
        /// <param name="duThaoTraLoi"> duThaoTraLoi </param>
        /// <param name="fileDinhKem"> fileDinhKem </param>
        /// <param name="duyetDuThaoTraLoi"> duyetDuThaoTraLoi </param>
        /// <param name="lyDoKhongPheDuyetTraLoi"> lyDoKhongPheDuyetTraLoi </param>
        /// <param name="xuatBanDuThaoTraLoi"> xuatBanDuThaoTraLoi </param>
        /// <param name="nguoiXoaCauHoi"> nguoiXoaCauHoi </param>
        /// <param name="nguoiKiemDuyet"> nguoiKiemDuyet </param>
        /// <param name="ngayXoaCauHoi"> ngayXoaCauHoi </param>
        /// <param name="ngayKiemDuyet"> ngayKiemDuyet </param>
        /// <param name="nguoiXuatBanCauHoi"> nguoiXuatBanCauHoi </param>
        /// <param name="ngayXuatBanCauHoi"> ngayXuatBanCauHoi </param>
        /// <param name="nguoiTaoDuThaoTraLoi"> nguoiTaoDuThaoTraLoi </param>
        /// <param name="ngayTaoDuThao"> ngayTaoDuThao </param>
        /// <param name="nguoiPheDuyetDuThao"> nguoiPheDuyetDuThao </param>
        /// <param name="ngayPheDuyet"> ngayPheDuyet </param>
        /// <param name="nguoiXuatBanCauTraLoi"> nguoiXuatBanCauTraLoi </param>
        /// <param name="ngayXuatBanCauTraLoi"> ngayXuatBanCauTraLoi </param>
        /// <returns>bool</returns>
        public static bool Update(
            Guid guid,
            int moduleID,
            string title,
            string question,
            string answer,
            bool isPublished,
            DateTime lastModified,
            string createdByName,
            string createdByEmail,
            string createdByPhone,
            int answerUser,
            int hitCount,
            string itemUrl,
            bool isHot,
            int commentCount,
            int qACategoryID,
            int coQuanID,
            bool is_Active,
            string lyDoXoa,
            bool viPhamQuyChe,
            string ghiChu,
            bool duyetCauHoi,
            bool duThaoTraLoi,
            string fileDinhKem,
            bool duyetDuThaoTraLoi,
            string lyDoKhongPheDuyetTraLoi,
            bool xuatBanDuThaoTraLoi,
            Guid nguoiXoaCauHoi,
            Guid nguoiKiemDuyet,
            DateTime ngayXoaCauHoi,
            DateTime ngayKiemDuyet,
            Guid nguoiXuatBanCauHoi,
            DateTime ngayXuatBanCauHoi,
            Guid nguoiTaoDuThaoTraLoi,
            DateTime ngayTaoDuThao,
            Guid nguoiPheDuyetDuThao,
            DateTime ngayPheDuyet,
            Guid nguoiXuatBanCauTraLoi,
            DateTime ngayXuatBanCauTraLoi,
            bool tiepNhanCauHoi,
            string fts,
            bool khongDuyetDuThao,
            int phongBanID,
            bool duyetThongBao,
            Guid nguoiDuyetThongBao,
            DateTime ngayDuyetThongBao,
            bool xuatBanThongBao,
            Guid nguoiXuatBanThongBao,
            DateTime ngayXuatBanThongBao,
            int creatQuestionByUser,
            DateTime ngayTiepNhan,
            int nguoiTiepNhan,
            bool statusSend,
            bool chuyenDuThao,
            int nguoiChuyenDuThao,
            DateTime ngayChuyenDuThao)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_QA_Update", 56);
            sph.DefineSqlParameter("@Guid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, guid);
            sph.DefineSqlParameter("@ModuleID", SqlDbType.Int, ParameterDirection.Input, moduleID);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 255, ParameterDirection.Input, title);
            sph.DefineSqlParameter("@Question", SqlDbType.NText, ParameterDirection.Input, question);
            sph.DefineSqlParameter("@Answer", SqlDbType.NText, ParameterDirection.Input, answer);
            sph.DefineSqlParameter("@IsPublished", SqlDbType.Bit, ParameterDirection.Input, isPublished);
            sph.DefineSqlParameter("@LastModified", SqlDbType.DateTime, ParameterDirection.Input, lastModified);
            sph.DefineSqlParameter("@CreatedByName", SqlDbType.NVarChar, 255, ParameterDirection.Input, createdByName);
            sph.DefineSqlParameter("@CreatedByEmail", SqlDbType.NVarChar, 255, ParameterDirection.Input, createdByEmail);
            sph.DefineSqlParameter("@CreatedByPhone", SqlDbType.NVarChar, 50, ParameterDirection.Input, createdByPhone);
            sph.DefineSqlParameter("@AnswerUser", SqlDbType.Int, ParameterDirection.Input, answerUser);
            sph.DefineSqlParameter("@HitCount", SqlDbType.Int, ParameterDirection.Input, hitCount);
            sph.DefineSqlParameter("@ItemUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, itemUrl);
            sph.DefineSqlParameter("@IsHot", SqlDbType.Bit, ParameterDirection.Input, isHot);
            sph.DefineSqlParameter("@CommentCount", SqlDbType.Int, ParameterDirection.Input, commentCount);
            sph.DefineSqlParameter("@QACategoryID", SqlDbType.Int, ParameterDirection.Input, qACategoryID);
            sph.DefineSqlParameter("@CoQuanID", SqlDbType.Int, ParameterDirection.Input, coQuanID);
            sph.DefineSqlParameter("@Is_Active", SqlDbType.Bit, ParameterDirection.Input, is_Active);
            sph.DefineSqlParameter("@LyDoXoa", SqlDbType.NText, ParameterDirection.Input, lyDoXoa);
            sph.DefineSqlParameter("@ViPhamQuyChe", SqlDbType.Bit, ParameterDirection.Input, viPhamQuyChe);
            sph.DefineSqlParameter("@GhiChu", SqlDbType.NText, ParameterDirection.Input, ghiChu);
            sph.DefineSqlParameter("@DuyetCauHoi", SqlDbType.Bit, ParameterDirection.Input, duyetCauHoi);
            sph.DefineSqlParameter("@DuThaoTraLoi", SqlDbType.Bit, ParameterDirection.Input, duThaoTraLoi);
            sph.DefineSqlParameter("@FileDinhKem", SqlDbType.NVarChar, 255, ParameterDirection.Input, fileDinhKem);
            sph.DefineSqlParameter("@DuyetDuThaoTraLoi", SqlDbType.Bit, ParameterDirection.Input, duyetDuThaoTraLoi);
            sph.DefineSqlParameter("@LyDoKhongPheDuyetTraLoi", SqlDbType.NText, ParameterDirection.Input, lyDoKhongPheDuyetTraLoi);
            sph.DefineSqlParameter("@XuatBanDuThaoTraLoi", SqlDbType.Bit, ParameterDirection.Input, xuatBanDuThaoTraLoi);
            sph.DefineSqlParameter("@NguoiXoaCauHoi", SqlDbType.UniqueIdentifier, ParameterDirection.Input, nguoiXoaCauHoi);
            sph.DefineSqlParameter("@NguoiKiemDuyet", SqlDbType.UniqueIdentifier, ParameterDirection.Input, nguoiKiemDuyet);
            sph.DefineSqlParameter("@NgayXoaCauHoi", SqlDbType.DateTime, ParameterDirection.Input, ngayXoaCauHoi);
            sph.DefineSqlParameter("@NgayKiemDuyet", SqlDbType.DateTime, ParameterDirection.Input, ngayKiemDuyet);
            sph.DefineSqlParameter("@NguoiXuatBanCauHoi", SqlDbType.UniqueIdentifier, ParameterDirection.Input, nguoiXuatBanCauHoi);
            sph.DefineSqlParameter("@NgayXuatBanCauHoi", SqlDbType.DateTime, ParameterDirection.Input, ngayXuatBanCauHoi);
            sph.DefineSqlParameter("@NguoiTaoDuThaoTraLoi", SqlDbType.UniqueIdentifier, ParameterDirection.Input, nguoiTaoDuThaoTraLoi);
            sph.DefineSqlParameter("@NgayTaoDuThao", SqlDbType.DateTime, ParameterDirection.Input, ngayTaoDuThao);
            sph.DefineSqlParameter("@NguoiPheDuyetDuThao", SqlDbType.UniqueIdentifier, ParameterDirection.Input, nguoiPheDuyetDuThao);
            sph.DefineSqlParameter("@NgayPheDuyet", SqlDbType.DateTime, ParameterDirection.Input, ngayPheDuyet);
            sph.DefineSqlParameter("@NguoiXuatBanCauTraLoi", SqlDbType.UniqueIdentifier, ParameterDirection.Input, nguoiXuatBanCauTraLoi);
            sph.DefineSqlParameter("@NgayXuatBanCauTraLoi", SqlDbType.DateTime, ParameterDirection.Input, ngayXuatBanCauTraLoi);
            sph.DefineSqlParameter("@TiepNhanCauHoi", SqlDbType.Bit, ParameterDirection.Input, tiepNhanCauHoi);
            sph.DefineSqlParameter("@FTS", SqlDbType.NText, ParameterDirection.Input, fts);
            sph.DefineSqlParameter("@KhongPheDuyetTraLoi", SqlDbType.Bit, ParameterDirection.Input, khongDuyetDuThao);
            sph.DefineSqlParameter("@PhongBanID", SqlDbType.Int, ParameterDirection.Input, phongBanID);
            sph.DefineSqlParameter("@DuyetThongBao", SqlDbType.Bit, ParameterDirection.Input, duyetThongBao);
            sph.DefineSqlParameter("@NguoiDuyetThongBao", SqlDbType.UniqueIdentifier, ParameterDirection.Input, nguoiDuyetThongBao);
            sph.DefineSqlParameter("@NgayDuyetThongBao", SqlDbType.DateTime, ParameterDirection.Input, ngayDuyetThongBao);
            sph.DefineSqlParameter("@XuatBanThongBao", SqlDbType.Bit, ParameterDirection.Input, xuatBanThongBao);
            sph.DefineSqlParameter("@NguoiXuatBanThongBao", SqlDbType.UniqueIdentifier, ParameterDirection.Input, nguoiXuatBanThongBao);
            sph.DefineSqlParameter("@NgayXuatBanThongBao", SqlDbType.DateTime, ParameterDirection.Input, ngayXuatBanThongBao);
            sph.DefineSqlParameter("@CreatQuestionByUser", SqlDbType.Int, ParameterDirection.Input, creatQuestionByUser);
            sph.DefineSqlParameter("@NgayTiepNhan", SqlDbType.DateTime, ParameterDirection.Input, ngayTiepNhan);
            sph.DefineSqlParameter("@NguoiTiepNhan", SqlDbType.Int, ParameterDirection.Input, nguoiTiepNhan);
            sph.DefineSqlParameter("@StatusSend", SqlDbType.Bit, ParameterDirection.Input, statusSend);
            sph.DefineSqlParameter("@ChuyenDuThao", SqlDbType.Bit, ParameterDirection.Input, chuyenDuThao);
            sph.DefineSqlParameter("@NguoiChuyenDuThao", SqlDbType.Int, ParameterDirection.Input, nguoiChuyenDuThao);
            sph.DefineSqlParameter("@NgayChuyenDuThao", SqlDbType.DateTime, ParameterDirection.Input, ngayChuyenDuThao);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_QA table. Returns true if row deleted.
        /// </summary>
        /// <param name="guid"> guid </param>
        /// <returns>bool</returns>
        public static bool Delete(
            Guid guid)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_QA_Delete", 1);
            sph.DefineSqlParameter("@Guid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, guid);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_QA table.
        /// </summary>
        /// <param name="guid"> guid </param>
        public static IDataReader GetOne(
            Guid guid)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_QA_SelectOne", 1);
            sph.DefineSqlParameter("@Guid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, guid);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_QA table.
        /// </summary>
        public static int GetCount()
        {

            return Convert.ToInt32(SqlHelper.ExecuteScalar(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_QA_GetCount",
                null));

        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_QA table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_QA_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the md_QA table.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static IDataReader GetPage(
            int pageNumber,
            int pageSize,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCount();

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_QA_SelectPage", 2);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();

        }

    }

}



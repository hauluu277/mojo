// Author:					Mr Hậu
// Created:					2021-3-31
// Last Modified:			2021-3-31
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

    public static class DBQuestionDepartment
    {


        /// <summary>
        /// Inserts a row in the md_QuestionDepartment table. Returns new integer id.
        /// </summary>
        /// <param name="departmentID"> departmentID </param>
        /// <param name="hoTen"> hoTen </param>
        /// <param name="phone"> phone </param>
        /// <param name="noiDung"> noiDung </param>
        /// <param name="diaChiDonVi"> diaChiDonVi </param>
        /// <param name="noiDungTraLoi"> noiDungTraLoi </param>
        /// <param name="nguoiTraLoi"> nguoiTraLoi </param>
        /// <param name="ngayTraLoi"> ngayTraLoi </param>
        /// <param name="nguoiTraLoiId"> nguoiTraLoiId </param>
        /// <param name="isTraLoi"> isTraLoi </param>
        /// <param name="isPublish"> isPublish </param>
        /// <param name="isSendMail"> isSendMail </param>
        /// <param name="createdDate"> createdDate </param>
        /// <returns>int</returns>
        public static int Create(
            int? departmentID,
            string hoTen,
            string phone,
            string noiDung,
            string diaChiDonVi,
            string noiDungTraLoi,
            string nguoiTraLoi,
            DateTime? ngayTraLoi,
            int? nguoiTraLoiId,
            bool? isTraLoi,
            bool? isPublish,
            bool? isSendMail,
            DateTime createdDate,
            string email)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_QuestionDepartment_Insert", 14);
            sph.DefineSqlParameter("@DepartmentID", SqlDbType.Int, ParameterDirection.Input, departmentID);
            sph.DefineSqlParameter("@HoTen", SqlDbType.NVarChar, 550, ParameterDirection.Input, hoTen);
            sph.DefineSqlParameter("@Phone", SqlDbType.NVarChar, 50, ParameterDirection.Input, phone);
            sph.DefineSqlParameter("@NoiDung", SqlDbType.NVarChar, -1, ParameterDirection.Input, noiDung);
            sph.DefineSqlParameter("@DiaChiDonVi", SqlDbType.NVarChar, 550, ParameterDirection.Input, diaChiDonVi);
            sph.DefineSqlParameter("@NoiDungTraLoi", SqlDbType.NVarChar, -1, ParameterDirection.Input, noiDungTraLoi);
            sph.DefineSqlParameter("@NguoiTraLoi", SqlDbType.NVarChar, 550, ParameterDirection.Input, nguoiTraLoi);
            sph.DefineSqlParameter("@NgayTraLoi", SqlDbType.DateTime, ParameterDirection.Input, ngayTraLoi);
            sph.DefineSqlParameter("@NguoiTraLoiId", SqlDbType.Int, ParameterDirection.Input, nguoiTraLoiId);
            sph.DefineSqlParameter("@IsTraLoi", SqlDbType.Bit, ParameterDirection.Input, isTraLoi);
            sph.DefineSqlParameter("@IsPublish", SqlDbType.Bit, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@IsSendMail", SqlDbType.Bit, ParameterDirection.Input, isSendMail);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@Email", SqlDbType.NVarChar, 550, ParameterDirection.Input, email);
            int newID = Convert.ToInt32(sph.ExecuteScalar());
            return newID;
        }


        /// <summary>
        /// Updates a row in the md_QuestionDepartment table. Returns true if row updated.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <param name="departmentID"> departmentID </param>
        /// <param name="hoTen"> hoTen </param>
        /// <param name="phone"> phone </param>
        /// <param name="noiDung"> noiDung </param>
        /// <param name="diaChiDonVi"> diaChiDonVi </param>
        /// <param name="noiDungTraLoi"> noiDungTraLoi </param>
        /// <param name="nguoiTraLoi"> nguoiTraLoi </param>
        /// <param name="ngayTraLoi"> ngayTraLoi </param>
        /// <param name="nguoiTraLoiId"> nguoiTraLoiId </param>
        /// <param name="isTraLoi"> isTraLoi </param>
        /// <param name="isPublish"> isPublish </param>
        /// <param name="isSendMail"> isSendMail </param>
        /// <param name="createdDate"> createdDate </param>
        /// <returns>bool</returns>
        public static bool Update(
            long itemID,
            int? departmentID,
            string hoTen,
            string phone,
            string noiDung,
            string diaChiDonVi,
            string noiDungTraLoi,
            string nguoiTraLoi,
            DateTime? ngayTraLoi,
            int? nguoiTraLoiId,
            bool? isTraLoi,
            bool? isPublish,
            bool? isSendMail,
            DateTime createdDate,
            string email)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_QuestionDepartment_Update", 15);
            sph.DefineSqlParameter("@ItemID", SqlDbType.BigInt, ParameterDirection.Input, itemID);
            sph.DefineSqlParameter("@DepartmentID", SqlDbType.Int, ParameterDirection.Input, departmentID);
            sph.DefineSqlParameter("@HoTen", SqlDbType.NVarChar, 550, ParameterDirection.Input, hoTen);
            sph.DefineSqlParameter("@Phone", SqlDbType.NVarChar, 50, ParameterDirection.Input, phone);
            sph.DefineSqlParameter("@NoiDung", SqlDbType.NVarChar, -1, ParameterDirection.Input, noiDung);
            sph.DefineSqlParameter("@DiaChiDonVi", SqlDbType.NVarChar, 550, ParameterDirection.Input, diaChiDonVi);
            sph.DefineSqlParameter("@NoiDungTraLoi", SqlDbType.NVarChar, -1, ParameterDirection.Input, noiDungTraLoi);
            sph.DefineSqlParameter("@NguoiTraLoi", SqlDbType.NVarChar, 550, ParameterDirection.Input, nguoiTraLoi);
            sph.DefineSqlParameter("@NgayTraLoi", SqlDbType.DateTime, ParameterDirection.Input, ngayTraLoi);
            sph.DefineSqlParameter("@NguoiTraLoiId", SqlDbType.Int, ParameterDirection.Input, nguoiTraLoiId);
            sph.DefineSqlParameter("@IsTraLoi", SqlDbType.Bit, ParameterDirection.Input, isTraLoi);
            sph.DefineSqlParameter("@IsPublish", SqlDbType.Bit, ParameterDirection.Input, isPublish);
            sph.DefineSqlParameter("@IsSendMail", SqlDbType.Bit, ParameterDirection.Input, isSendMail);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@Email", SqlDbType.NVarChar, 550, ParameterDirection.Input, email);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Deletes a row from the md_QuestionDepartment table. Returns true if row deleted.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            long itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetWriteConnectionString(), "md_QuestionDepartment_Delete", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.BigInt, ParameterDirection.Input, itemID);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

        }

        /// <summary>
        /// Gets an IDataReader with one row from the md_QuestionDepartment table.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        public static IDataReader GetOne(
            long itemID)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_QuestionDepartment_SelectOne", 1);
            sph.DefineSqlParameter("@ItemID", SqlDbType.BigInt, ParameterDirection.Input, itemID);
            return sph.ExecuteReader();

        }

        /// <summary>
        /// Gets a count of rows in the md_QuestionDepartment table.
        /// </summary>
        public static int GetCount(int department, DateTime? createdDate, bool? status, string name, string phone)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_QuestionDepartment_GetCount", 5);
            sph.DefineSqlParameter("@DepartmentID", SqlDbType.Int, ParameterDirection.Input, department);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@Status", SqlDbType.Bit, ParameterDirection.Input, status);
            sph.DefineSqlParameter("@Name", SqlDbType.NVarChar, 550, ParameterDirection.Input, name);
            sph.DefineSqlParameter("@Phone", SqlDbType.NVarChar, 250, ParameterDirection.Input, phone);
            return Convert.ToInt32(sph.ExecuteScalar());
        }

        public static int GetPublishCount(int department)
        {
            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_QuestionDepartment_GetPublishCount", 1);
            sph.DefineSqlParameter("@DepartmentID", SqlDbType.Int, ParameterDirection.Input, department);
            return Convert.ToInt32(sph.ExecuteScalar());
        }

        /// <summary>
        /// Gets an IDataReader with all rows in the md_QuestionDepartment table.
        /// </summary>
        public static IDataReader GetAll()
        {

            return SqlHelper.ExecuteReader(
                ConnectionString.GetReadConnectionString(),
                CommandType.StoredProcedure,
                "md_QuestionDepartment_SelectAll",
                null);

        }

        /// <summary>
        /// Gets a page of data from the md_QuestionDepartment table.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static IDataReader GetPage(
            int department,
            DateTime? createdDate,
            bool? status,
            string name,
            string phone,
            int pageNumber,
            int pageSize,
            out int totalPages)
        {
            totalPages = 1;
            int totalRows
                = GetCount(department, createdDate, status, name, phone);

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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_QuestionDepartment_SelectPage", 7);
            sph.DefineSqlParameter("@DepartmentID", SqlDbType.Int, ParameterDirection.Input, department);
            sph.DefineSqlParameter("@CreatedDate", SqlDbType.DateTime, ParameterDirection.Input, createdDate);
            sph.DefineSqlParameter("@Status", SqlDbType.Bit, ParameterDirection.Input, status);
            sph.DefineSqlParameter("@Name", SqlDbType.NVarChar, 550, ParameterDirection.Input, name);
            sph.DefineSqlParameter("@Phone", SqlDbType.NVarChar, 250, ParameterDirection.Input, phone);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();
        }


        public static IDataReader GetPublishPage(
         int department,
         int pageNumber,
         int pageSize,
         out int totalPages,
         out int totalCount)
        {
            totalPages = 1;
            int totalRows
                = GetPublishCount(department);
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

            SqlParameterHelper sph = new SqlParameterHelper(ConnectionString.GetReadConnectionString(), "md_QuestionDepartment_SelectPublishPage", 3);
            sph.DefineSqlParameter("@DepartmentID", SqlDbType.Int, ParameterDirection.Input, department);
            sph.DefineSqlParameter("@PageNumber", SqlDbType.Int, ParameterDirection.Input, pageNumber);
            sph.DefineSqlParameter("@PageSize", SqlDbType.Int, ParameterDirection.Input, pageSize);
            return sph.ExecuteReader();
        }
    }

}



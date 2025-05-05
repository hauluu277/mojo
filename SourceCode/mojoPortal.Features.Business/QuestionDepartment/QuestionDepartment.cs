
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
using System.Collections;
using System.Collections.Generic;
using System.Data;
using mojoPortal.Data;

namespace mojoPortal.Business
{

    public class QuestionDepartment
    {

        #region Constructors

        public QuestionDepartment()
        { }


        public QuestionDepartment(
            long itemID)
        {
            GetQuestionDepartment(
                itemID);
        }

        #endregion

        #region Private Properties

        private long itemID;
        private int? departmentID = null;
        private string hoTen = string.Empty;
        private string phone = string.Empty;
        private string noiDung = string.Empty;
        private string diaChiDonVi = string.Empty;
        private string noiDungTraLoi = string.Empty;
        private string nguoiTraLoi = string.Empty;
        private DateTime? ngayTraLoi = null;
        private int? nguoiTraLoiId = null;
        private bool? isTraLoi = null;
        private bool? isPublish = null;
        private bool? isSendMail = null;
        private DateTime createdDate = DateTime.Now;
        private string statusName = string.Empty;
        private string email = string.Empty;
        private string createdDateFormat = string.Empty;
        private string dateAnswerFormat = string.Empty;

        #endregion

        #region Public Properties
        public string CreatedDateFormat
        {
            get { return createdDateFormat; }
            set { createdDateFormat = value; }
        }
        public string DateAnswerFormat
        {
            get { return dateAnswerFormat; }
            set { dateAnswerFormat = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string StatusName
        {
            get { return statusName; }
            set { statusName = value; }
        }

        public long ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public int? DepartmentID
        {
            get { return departmentID; }
            set { departmentID = value; }
        }
        public string HoTen
        {
            get { return hoTen; }
            set { hoTen = value; }
        }
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        public string NoiDung
        {
            get { return noiDung; }
            set { noiDung = value; }
        }
        public string DiaChiDonVi
        {
            get { return diaChiDonVi; }
            set { diaChiDonVi = value; }
        }
        public string NoiDungTraLoi
        {
            get { return noiDungTraLoi; }
            set { noiDungTraLoi = value; }
        }
        public string NguoiTraLoi
        {
            get { return nguoiTraLoi; }
            set { nguoiTraLoi = value; }
        }
        public DateTime? NgayTraLoi
        {
            get { return ngayTraLoi; }
            set { ngayTraLoi = value; }
        }
        public int? NguoiTraLoiId
        {
            get { return nguoiTraLoiId; }
            set { nguoiTraLoiId = value; }
        }
        public bool? IsTraLoi
        {
            get { return isTraLoi; }
            set { isTraLoi = value; }
        }
        public bool? IsPublish
        {
            get { return isPublish; }
            set { isPublish = value; }
        }
        public bool? IsSendMail
        {
            get { return isSendMail; }
            set { isSendMail = value; }
        }
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of QuestionDepartment.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetQuestionDepartment(
            long itemID)
        {
            using (IDataReader reader = DBQuestionDepartment.GetOne(
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

                if (!string.IsNullOrEmpty(reader["DepartmentID"].ToString()))
                {
                    this.departmentID = Convert.ToInt32(reader["DepartmentID"]);
                }
                if (!string.IsNullOrEmpty(reader["HoTen"].ToString()))
                {
                    this.hoTen = reader["HoTen"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["Phone"].ToString()))
                {
                    this.phone = reader["Phone"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["NoiDung"].ToString()))
                {
                    this.noiDung = reader["NoiDung"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["DiaChiDonVi"].ToString()))
                {
                    this.diaChiDonVi = reader["DiaChiDonVi"].ToString();
                }
                if (!string.IsNullOrEmpty(reader["NoiDungTraLoi"].ToString()))
                {
                    this.noiDungTraLoi = reader["NoiDungTraLoi"].ToString();

                }
                if (!string.IsNullOrEmpty(reader["NguoiTraLoi"].ToString()))
                {
                    this.nguoiTraLoi = reader["NguoiTraLoi"].ToString();

                }
                if (!string.IsNullOrEmpty(reader["NgayTraLoi"].ToString()))
                {
                    this.ngayTraLoi = Convert.ToDateTime(reader["NgayTraLoi"]);
                }
                if (!string.IsNullOrEmpty(reader["NguoiTraLoiId"].ToString()))
                {
                    this.nguoiTraLoiId = Convert.ToInt32(reader["NguoiTraLoiId"]);
                }
                if (!string.IsNullOrEmpty(reader["IsTraLoi"].ToString()))
                {
                    this.isTraLoi = Convert.ToBoolean(reader["IsTraLoi"]);
                }
                if (!string.IsNullOrEmpty(reader["IsPublish"].ToString()))
                {
                    this.isPublish = Convert.ToBoolean(reader["IsPublish"]);
                }
                if (!string.IsNullOrEmpty(reader["IsSendMail"].ToString()))
                {
                    this.isSendMail = Convert.ToBoolean(reader["IsSendMail"]);
                }
                if (!string.IsNullOrEmpty(reader["CreatedDate"].ToString()))
                {
                    this.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                }
                if (!string.IsNullOrEmpty(reader["Email"].ToString()))
                {
                    this.email = reader["Email"].ToString();
                }

            }

        }

        /// <summary>
        /// Persists a new instance of QuestionDepartment. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBQuestionDepartment.Create(
                this.departmentID,
                this.hoTen,
                this.phone,
                this.noiDung,
                this.diaChiDonVi,
                this.noiDungTraLoi,
                this.nguoiTraLoi,
                this.ngayTraLoi,
                this.nguoiTraLoiId,
                this.isTraLoi,
                this.isPublish,
                this.isSendMail,
                this.createdDate,
                this.email);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of QuestionDepartment. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBQuestionDepartment.Update(
                this.itemID,
                this.departmentID,
                this.hoTen,
                this.phone,
                this.noiDung,
                this.diaChiDonVi,
                this.noiDungTraLoi,
                this.nguoiTraLoi,
                this.ngayTraLoi,
                this.nguoiTraLoiId,
                this.isTraLoi,
                this.isPublish,
                this.isSendMail,
                this.createdDate,
                this.email);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of QuestionDepartment. Returns true on success.
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
        /// Deletes an instance of QuestionDepartment. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            long itemID)
        {
            return DBQuestionDepartment.Delete(
                itemID);
        }


        private static List<QuestionDepartment> LoadListFromReader(IDataReader reader)
        {
            List<QuestionDepartment> questionDepartmentList = new List<QuestionDepartment>();
            try
            {
                while (reader.Read())
                {
                    QuestionDepartment questionDepartment = new QuestionDepartment();
                    questionDepartment.itemID = Convert.ToInt64(reader["ItemID"]);
                    if (!string.IsNullOrEmpty(reader["DepartmentID"].ToString()))
                    {
                        questionDepartment.departmentID = Convert.ToInt32(reader["DepartmentID"]);
                    }
                    if (!string.IsNullOrEmpty(reader["HoTen"].ToString()))
                    {
                        questionDepartment.hoTen = reader["HoTen"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["Phone"].ToString()))
                    {
                        questionDepartment.phone = reader["Phone"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["NoiDung"].ToString()))
                    {
                        questionDepartment.noiDung = reader["NoiDung"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["DiaChiDonVi"].ToString()))
                    {
                        questionDepartment.diaChiDonVi = reader["DiaChiDonVi"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["NoiDungTraLoi"].ToString()))
                    {
                        questionDepartment.noiDungTraLoi = reader["NoiDungTraLoi"].ToString();

                    }
                    if (!string.IsNullOrEmpty(reader["NguoiTraLoi"].ToString()))
                    {
                        questionDepartment.nguoiTraLoi = reader["NguoiTraLoi"].ToString();

                    }
                    if (!string.IsNullOrEmpty(reader["NgayTraLoi"].ToString()))
                    {
                        questionDepartment.ngayTraLoi = Convert.ToDateTime(reader["NgayTraLoi"]);
                        questionDepartment.dateAnswerFormat = string.Format("{0:dd/MM/yyyy HH:mm}",questionDepartment.ngayTraLoi);
                    }
                    if (!string.IsNullOrEmpty(reader["NguoiTraLoiId"].ToString()))
                    {
                        questionDepartment.nguoiTraLoiId = Convert.ToInt32(reader["NguoiTraLoiId"]);
                    }
                    if (!string.IsNullOrEmpty(reader["IsTraLoi"].ToString()))
                    {
                        questionDepartment.isTraLoi = Convert.ToBoolean(reader["IsTraLoi"]);
                    }
                    if (!string.IsNullOrEmpty(reader["IsPublish"].ToString()))
                    {
                        questionDepartment.isPublish = Convert.ToBoolean(reader["IsPublish"]);
                    }
                    if (!string.IsNullOrEmpty(reader["IsSendMail"].ToString()))
                    {
                        questionDepartment.isSendMail = Convert.ToBoolean(reader["IsSendMail"]);
                    }
                    if (!string.IsNullOrEmpty(reader["CreatedDate"].ToString()))
                    {
                        questionDepartment.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                        questionDepartment.createdDateFormat = string.Format("{0:dd/MM/yyyy HH:mm}", questionDepartment.createdDate);
                    }
                    if (questionDepartment.isTraLoi.HasValue && questionDepartment.isTraLoi.Value)
                    {
                        questionDepartment.statusName = "Đã trả lời";
                    }
                    else
                    {
                        questionDepartment.statusName = "Chưa trả lời";
                    }
                    if (!string.IsNullOrEmpty(reader["Email"].ToString()))
                    {
                        questionDepartment.email = reader["Email"].ToString();
                    }
                    questionDepartmentList.Add(questionDepartment);

                }
            }
            finally
            {
                reader.Close();
            }

            return questionDepartmentList;

        }

        /// <summary>
        /// Gets an IList with all instances of QuestionDepartment.
        /// </summary>
        public static List<QuestionDepartment> GetAll()
        {
            IDataReader reader = DBQuestionDepartment.GetAll();
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of QuestionDepartment.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<QuestionDepartment> GetPage(int department, DateTime? createdDate, bool? status, string name, string phone, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBQuestionDepartment.GetPage(department, createdDate, status, name, phone, pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }
        public static List<QuestionDepartment> GetPublishPage(int department, int pageNumber, int pageSize, out int totalPages, out int totalCount)
        {
            totalPages = 1;
            IDataReader reader = DBQuestionDepartment.GetPublishPage(department, pageNumber, pageSize, out totalPages, out totalCount);
            return LoadListFromReader(reader);
        }

        #endregion

        #region Comparison Methods


        /// <summary>
        /// Compares 2 instances of QuestionDepartment.
        /// </summary>
        public static int CompareByHoTen(QuestionDepartment questionDepartment1, QuestionDepartment questionDepartment2)
        {
            return questionDepartment1.HoTen.CompareTo(questionDepartment2.HoTen);
        }
        /// <summary>
        /// Compares 2 instances of QuestionDepartment.
        /// </summary>
        public static int CompareByPhone(QuestionDepartment questionDepartment1, QuestionDepartment questionDepartment2)
        {
            return questionDepartment1.Phone.CompareTo(questionDepartment2.Phone);
        }
        /// <summary>
        /// Compares 2 instances of QuestionDepartment.
        /// </summary>
        public static int CompareByNoiDung(QuestionDepartment questionDepartment1, QuestionDepartment questionDepartment2)
        {
            return questionDepartment1.NoiDung.CompareTo(questionDepartment2.NoiDung);
        }
        /// <summary>
        /// Compares 2 instances of QuestionDepartment.
        /// </summary>
        public static int CompareByDiaChiDonVi(QuestionDepartment questionDepartment1, QuestionDepartment questionDepartment2)
        {
            return questionDepartment1.DiaChiDonVi.CompareTo(questionDepartment2.DiaChiDonVi);
        }
        /// <summary>
        /// Compares 2 instances of QuestionDepartment.
        /// </summary>
        public static int CompareByNoiDungTraLoi(QuestionDepartment questionDepartment1, QuestionDepartment questionDepartment2)
        {
            return questionDepartment1.NoiDungTraLoi.CompareTo(questionDepartment2.NoiDungTraLoi);
        }
        /// <summary>
        /// Compares 2 instances of QuestionDepartment.
        /// </summary>
        public static int CompareByNguoiTraLoi(QuestionDepartment questionDepartment1, QuestionDepartment questionDepartment2)
        {
            return questionDepartment1.NguoiTraLoi.CompareTo(questionDepartment2.NguoiTraLoi);
        }

        /// <summary>
        /// Compares 2 instances of QuestionDepartment.
        /// </summary>
        public static int CompareByCreatedDate(QuestionDepartment questionDepartment1, QuestionDepartment questionDepartment2)
        {
            return questionDepartment1.CreatedDate.CompareTo(questionDepartment2.CreatedDate);
        }

        #endregion


    }

}






using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using QuestionAnswerFeatures.Data;

namespace QuestionAnswerFeatures.Business
{
    public class QuestionAnswer
    {
        #region Constructors
        public QuestionAnswer()
        { }


        public QuestionAnswer(
            int itemID)
        {
            GetQuestionAnswer(
                itemID);
        }
        #endregion

        #region Private Properties

        private int itemID = -1;
        private int siteID = -1;
        private int pageID = -1;
        private int moduleID = -1;
        private string question = string.Empty;
        private string itemUrl = string.Empty;
        private int linhVucID = -1;
        private int loaiLinhVucID = -1;
        private string contentQuestion = string.Empty;
        private string name = string.Empty;
        private string email = string.Empty;
        private string phone = string.Empty;
        private bool isApprove = false;
        private int userApprove = -1;
        private bool isDelete = false;
        private int views = 0;
        private DateTime dateApprove = DateTime.Now;
        private DateTime createDate = DateTime.Now;
        private string categoryName = string.Empty;
        private string categoryChildName = string.Empty;
        private string fts = string.Empty;
        private int totalAnswer = 0;
        private int totalAnswerApproved = 0;
        private int createByUser = -1;
        private int? departmentId;
        private string departmentName = string.Empty;

        #endregion

        #region Public Properties
        public string DepartmentName
        {
            get { return departmentName; }
            set { departmentName = value; }
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
        public string Question
        {
            get { return question; }
            set { question = value; }
        }
        public int LinhVucID
        {
            get { return linhVucID; }
            set { linhVucID = value; }
        }
        public int LoaiLinhVucID
        {
            get { return loaiLinhVucID; }
            set { loaiLinhVucID = value; }
        }
        public string ItemUrl
        {
            get { return itemUrl; }
            set { itemUrl = value; }
        }

        public string ContentQuestion
        {
            get { return contentQuestion; }
            set { contentQuestion = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        public bool IsApprove
        {
            get { return isApprove; }
            set { isApprove = value; }
        }
        public int UserApprove
        {
            get { return userApprove; }
            set { userApprove = value; }
        }
        public bool IsDelete
        {
            get { return isDelete; }
            set { isDelete = value; }
        }
        public int Views
        {
            get { return views; }
            set { views = value; }
        }
        public DateTime DateApprove
        {
            get { return dateApprove; }
            set { dateApprove = value; }
        }
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        public string CategoryChildName
        {
            get { return categoryChildName; }
            set { categoryChildName = value; }
        }
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        public string FTS
        {
            get { return fts; }
            set { fts = value; }
        }

        public int TotalAnswer
        {
            get { return totalAnswer; }
            set { totalAnswer = value; }
        }

        public int TotalAnswerApproved
        {
            get { return totalAnswerApproved; }
            set { totalAnswerApproved = value; }
        }

        public int CreateByUser
        {
            get { return createByUser; }
            set { createByUser = value; }
        }

        public int? DepartmentId
        {
            get
            {
                return departmentId;
            }
            set { departmentId = value; }
        }


        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of QuestionAnswer.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetQuestionAnswer(
            int itemID)
        {
            using (IDataReader reader = DBQuestionAnswer.GetOne(
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
                this.itemUrl = reader["ItemUrl"].ToString();
                this.moduleID = Convert.ToInt32(reader["ModuleID"]);
                this.question = reader["Question"].ToString();
                this.linhVucID = Convert.ToInt32(reader["LinhVucID"]);
                this.loaiLinhVucID = Convert.ToInt32(reader["LoaiLinhVucID"]);
                if (reader["CreateByUser"] != DBNull.Value)
                {
                    this.CreateByUser = int.Parse(reader["CreateByUser"].ToString());
                }
                if (reader["DepartmentId"] != DBNull.Value)
                {
                    this.departmentId = int.Parse(reader["DepartmentId"].ToString());
                }

                this.contentQuestion = reader["ContentQuestion"].ToString();
                this.name = reader["Name"].ToString();
                this.email = reader["Email"].ToString();
                this.phone = reader["Phone"].ToString();
                this.isApprove = Convert.ToBoolean(reader["IsApprove"]);
                this.userApprove = Convert.ToInt32(reader["UserApprove"]);
                this.isDelete = Convert.ToBoolean(reader["IsDelete"]);
                this.views = Convert.ToInt32(reader["Views"]);
                this.dateApprove = Convert.ToDateTime(reader["DateApprove"]);
                this.createDate = Convert.ToDateTime(reader["CreateDate"]);
                this.fts = reader["FTS"].ToString();

            }

        }

        /// <summary>
        /// Persists a new instance of QuestionAnswer. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBQuestionAnswer.Create(
                this.siteID,
                this.pageID,
                this.moduleID,
                this.question,
                this.linhVucID,
                this.loaiLinhVucID,
                this.contentQuestion,
                this.itemUrl,
                this.name,
                this.email,
                this.phone,
                this.fts,
                this.isApprove,
                this.userApprove,
                this.isDelete,
                this.views,
                this.dateApprove,
                this.createDate,
                this.createByUser,
                departmentId);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of QuestionAnswer. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBQuestionAnswer.Update(
                this.itemID,
                this.siteID,
                this.pageID,
                this.moduleID,
                this.question,
                this.linhVucID,
                this.loaiLinhVucID,
                this.contentQuestion,
                this.itemUrl,
                this.name,
                this.email,
                this.phone,
                this.fts,
                this.isApprove,
                this.userApprove,
                this.isDelete,
                this.views,
                this.dateApprove,
                this.createDate,
                this.createByUser,
                departmentId);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of QuestionAnswer. Returns true on success.
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
        /// Deletes an instance of QuestionAnswer. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBQuestionAnswer.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of QuestionAnswer. 
        /// </summary>

        private static List<QuestionAnswer> LoadListFromReader(IDataReader reader)
        {
            List<QuestionAnswer> QuestionAnswerList = new List<QuestionAnswer>();
            try
            {
                while (reader.Read())
                {
                    QuestionAnswer QuestionAnswer = new QuestionAnswer();
                    QuestionAnswer.itemID = Convert.ToInt32(reader["ItemID"]);
                    QuestionAnswer.siteID = Convert.ToInt32(reader["SiteID"]);
                    QuestionAnswer.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    QuestionAnswer.pageID = Convert.ToInt32(reader["PageID"]);
                    QuestionAnswer.itemUrl = reader["ItemUrl"].ToString();
                    QuestionAnswer.question = reader["Question"].ToString();
                    QuestionAnswer.linhVucID = Convert.ToInt32(reader["LinhVucID"]);
                    QuestionAnswer.loaiLinhVucID = Convert.ToInt32(reader["LoaiLinhVucID"]);
                    QuestionAnswer.contentQuestion = reader["ContentQuestion"].ToString();
                    QuestionAnswer.name = reader["Name"].ToString();
                    QuestionAnswer.email = reader["Email"].ToString();
                    QuestionAnswer.phone = reader["Phone"].ToString();
                    QuestionAnswer.isApprove = Convert.ToBoolean(reader["IsApprove"]);
                    QuestionAnswer.userApprove = Convert.ToInt32(reader["UserApprove"]);
                    QuestionAnswer.isDelete = Convert.ToBoolean(reader["IsDelete"]);
                    QuestionAnswer.views = Convert.ToInt32(reader["Views"]);
                    QuestionAnswer.dateApprove = Convert.ToDateTime(reader["DateApprove"]);
                    QuestionAnswer.createDate = Convert.ToDateTime(reader["CreateDate"]);
                    QuestionAnswer.fts = reader["FTS"].ToString();
                    if (!string.IsNullOrEmpty(reader["DepartmentId"].ToString()))
                    {
                        QuestionAnswer.departmentId = Convert.ToInt32(reader["DepartmentId"]);
                    }
                    QuestionAnswerList.Add(QuestionAnswer);

                }
            }
            finally
            {
                reader.Close();
            }

            return QuestionAnswerList;

        }

        private static List<QuestionAnswer> LoadListFromReaderPageForUser(IDataReader reader)
        {
            List<QuestionAnswer> QuestionAnswerList = new List<QuestionAnswer>();
            try
            {
                while (reader.Read())
                {
                    QuestionAnswer QuestionAnswer = new QuestionAnswer();
                    QuestionAnswer.itemID = Convert.ToInt32(reader["ItemID"]);
                    QuestionAnswer.siteID = Convert.ToInt32(reader["SiteID"]);
                    QuestionAnswer.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    QuestionAnswer.pageID = Convert.ToInt32(reader["PageID"]);
                    QuestionAnswer.itemUrl = reader["ItemUrl"].ToString();
                    QuestionAnswer.question = reader["Question"].ToString();
                    QuestionAnswer.linhVucID = Convert.ToInt32(reader["LinhVucID"]);
                    QuestionAnswer.loaiLinhVucID = Convert.ToInt32(reader["LoaiLinhVucID"]);
                    QuestionAnswer.contentQuestion = reader["ContentQuestion"].ToString();
                    QuestionAnswer.name = reader["Name"].ToString();
                    QuestionAnswer.email = reader["Email"].ToString();
                    QuestionAnswer.phone = reader["Phone"].ToString();
                    QuestionAnswer.isApprove = Convert.ToBoolean(reader["IsApprove"]);
                    QuestionAnswer.userApprove = Convert.ToInt32(reader["UserApprove"]);
                    QuestionAnswer.isDelete = Convert.ToBoolean(reader["IsDelete"]);
                    QuestionAnswer.views = Convert.ToInt32(reader["Views"]);
                    QuestionAnswer.dateApprove = Convert.ToDateTime(reader["DateApprove"]);
                    QuestionAnswer.createDate = Convert.ToDateTime(reader["CreateDate"]);
                    if (reader["TotalAnswerApproved"] != DBNull.Value)
                    {
                        QuestionAnswer.totalAnswerApproved = int.Parse(reader["TotalAnswerApproved"].ToString());
                    }
                    //if (reader["TotalAnswerApproved"] != DBNull.Value)
                    //{
                    //    QuestionAnswer.totalAnswerApproved = int.Parse(reader["TotalAnswerApproved"].ToString());
                    //}
                    if (!string.IsNullOrEmpty(reader["DepartmentId"].ToString()))
                    {
                        QuestionAnswer.departmentId = Convert.ToInt32(reader["DepartmentId"]);
                    }
                    QuestionAnswerList.Add(QuestionAnswer);

                }
            }
            finally
            {
                reader.Close();
            }

            return QuestionAnswerList;

        }

        private static List<QuestionAnswer> LoadListFromReaderPage(IDataReader reader)
        {
            List<QuestionAnswer> QuestionAnswerList = new List<QuestionAnswer>();
            try
            {
                while (reader.Read())
                {
                    QuestionAnswer QuestionAnswer = new QuestionAnswer();
                    QuestionAnswer.itemID = Convert.ToInt32(reader["ItemID"]);
                    QuestionAnswer.siteID = Convert.ToInt32(reader["SiteID"]);
                    QuestionAnswer.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    QuestionAnswer.pageID = Convert.ToInt32(reader["PageID"]);
                    QuestionAnswer.itemUrl = reader["ItemUrl"].ToString();
                    QuestionAnswer.question = reader["Question"].ToString();
                    QuestionAnswer.linhVucID = Convert.ToInt32(reader["LinhVucID"]);
                    QuestionAnswer.loaiLinhVucID = Convert.ToInt32(reader["LoaiLinhVucID"]);
                    QuestionAnswer.contentQuestion = reader["ContentQuestion"].ToString();
                    QuestionAnswer.name = reader["Name"].ToString();
                    QuestionAnswer.email = reader["Email"].ToString();
                    QuestionAnswer.phone = reader["Phone"].ToString();
                    QuestionAnswer.isApprove = Convert.ToBoolean(reader["IsApprove"]);
                    QuestionAnswer.userApprove = Convert.ToInt32(reader["UserApprove"]);
                    QuestionAnswer.isDelete = Convert.ToBoolean(reader["IsDelete"]);
                    QuestionAnswer.views = Convert.ToInt32(reader["Views"]);
                    QuestionAnswer.dateApprove = Convert.ToDateTime(reader["DateApprove"]);
                    QuestionAnswer.createDate = Convert.ToDateTime(reader["CreateDate"]);
                    if (reader["CategoryName"] != DBNull.Value)
                    {
                        QuestionAnswer.categoryName = reader["CategoryName"].ToString();
                    }
                    if (reader["CategoryChildName"] != DBNull.Value)
                    {
                        QuestionAnswer.categoryChildName = reader["CategoryChildName"].ToString();
                    }
                    if (reader["TotalAnswer"] != DBNull.Value)
                    {
                        QuestionAnswer.totalAnswer = int.Parse(reader["TotalAnswer"].ToString());
                    }
                    if (reader["TotalAnswerApproved"] != DBNull.Value)
                    {
                        QuestionAnswer.totalAnswerApproved = int.Parse(reader["TotalAnswerApproved"].ToString());
                    }
                    if (!string.IsNullOrEmpty(reader["DepartmentId"].ToString()))
                    {
                        QuestionAnswer.departmentId = Convert.ToInt32(reader["DepartmentId"]);
                    }
                    if (reader["DepartmentName"] != DBNull.Value)
                    {
                        QuestionAnswer.departmentName = reader["DepartmentName"].ToString();
                    }
                    QuestionAnswerList.Add(QuestionAnswer);

                }
            }
            finally
            {
                reader.Close();
            }

            return QuestionAnswerList;

        }

        /// <summary>
        /// Gets an IList with all instances of QuestionAnswer.
        /// </summary>
        public static List<QuestionAnswer> GetAll()
        {
            IDataReader reader = DBQuestionAnswer.GetAll();
            return LoadListFromReader(reader);

        }

        public static List<QuestionAnswer> GetTopQuestion(int siteID, int top)
        {
            IDataReader reader = DBQuestionAnswer.GetTopQuestion(siteID, top);
            return LoadListFromReader(reader);

        }

        public static bool UpdateView(int itemId)
        {
            return DBQuestionAnswer.UpdateView(itemId);
        }

        /// <summary>
        /// Gets an IList with page of instances of QuestionAnswer.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<QuestionAnswer> GetPage(int siteID,
            int categoryID,
            int categoryChildID,
            int isPublic,
            int orderBy,
            string keyword,
            int pageNumber,
            int pageSize,
            int deptId,
            out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBQuestionAnswer.GetPage(siteID, categoryID, categoryChildID, isPublic, orderBy, keyword, pageNumber, pageSize, deptId, out totalPages);
            return LoadListFromReaderPage(reader);
        }



        public static List<QuestionAnswer> GetPageForUser(int siteID,
    int userID,
     int pageNumber,
    int pageSize,
    out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBQuestionAnswer.GetPageForUser(siteID, userID, pageNumber, pageSize, out totalPages);
            return LoadListFromReaderPageForUser(reader);
        }


        #endregion

    }
}

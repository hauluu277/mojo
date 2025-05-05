using QuestionAnswerFeatures.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace QuestionAnswerFeatures.Business
{
    public class Answer
    {
        #region Constructors

        public Answer()
        { }


        public Answer(
            int itemID)
        {
            GetAnswer(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int questionID = -1;
        private string answerName = string.Empty;
        private string name = string.Empty;
        private string email = string.Empty;
        private bool isApprove = false;
        private DateTime createDate = DateTime.UtcNow;
        private int userID = -1;
        private string questionName = string.Empty;
        private string questionUrl = string.Empty;
        private int totalAnswerApproved = 0;


        #endregion

        #region Public Properties
        public string QuestionUrl
        {
            get { return questionUrl; }
            set { questionUrl = value; }
        }
        public int TotalAnswerApproved
        {
            get { return totalAnswerApproved; }
            set { totalAnswerApproved = value; }
        }
        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public int QuestionID
        {
            get { return questionID; }
            set { questionID = value; }
        }
        public string AnswerName
        {
            get { return answerName; }
            set { answerName = value; }
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
        public bool IsApprove
        {
            get { return isApprove; }
            set { isApprove = value; }
        }
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public string QuestionName
        {
            get { return questionName; }
            set { questionName = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of Answer.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetAnswer(
            int itemID)
        {
            using (IDataReader reader = DBAnswer.GetOne(
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
                this.questionID = Convert.ToInt32(reader["QuestionID"]);
                this.answerName = reader["AnswerName"].ToString();
                this.name = reader["Name"].ToString();
                this.email = reader["Email"].ToString();
                this.isApprove = Convert.ToBoolean(reader["IsApprove"]);
                this.createDate = Convert.ToDateTime(reader["CreateDate"]);
                if (reader["UserID"] != null)
                {
                    this.userID = int.Parse(reader["UserID"].ToString());
                }

            }

        }

        /// <summary>
        /// Persists a new instance of Answer. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBAnswer.Create(
                this.questionID,
                this.answerName,
                this.name,
                this.email,
                this.isApprove,
                this.createDate,
                this.userID);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of Answer. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBAnswer.Update(
                this.itemID,
                this.questionID,
                this.answerName,
                this.name,
                this.email,
                this.isApprove,
                this.createDate,
                this.userID);

        }




        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of Answer. Returns true on success.
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
        /// Deletes an instance of Answer. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBAnswer.Delete(
                itemID);
        }

        public static bool DeleteQuestionId(
    int questionId)
        {
            return DBAnswer.DeleteByQuestion(
                questionId);
        }


        /// <summary>
        /// Gets a count of Answer. 
        /// </summary>
        public static int GetCount(int questionID, int isApprove)
        {
            return DBAnswer.GetCount(questionID, isApprove);
        }

        private static List<Answer> LoadListFromReader(IDataReader reader)
        {
            List<Answer> AnswerList = new List<Answer>();
            try
            {
                while (reader.Read())
                {
                    Answer Answer = new Answer();
                    Answer.itemID = Convert.ToInt32(reader["ItemID"]);
                    Answer.questionID = Convert.ToInt32(reader["QuestionID"]);
                    Answer.answerName = reader["AnswerName"].ToString();
                    Answer.name = reader["Name"].ToString();
                    Answer.email = reader["Email"].ToString();
                    Answer.isApprove = Convert.ToBoolean(reader["IsApprove"]);
                    Answer.createDate = Convert.ToDateTime(reader["CreateDate"]);
                    if (reader["UserID"] != DBNull.Value)
                    {
                        Answer.userID = int.Parse(reader["UserID"].ToString());
                    }
                    AnswerList.Add(Answer);

                }
            }
            finally
            {
                reader.Close();
            }

            return AnswerList;

        }



        private static List<Answer> LoadListFromReaderForUser(IDataReader reader)
        {
            List<Answer> AnswerList = new List<Answer>();
            try
            {
                while (reader.Read())
                {
                    Answer Answer = new Answer();
                    Answer.itemID = Convert.ToInt32(reader["ItemID"]);
                    Answer.questionID = Convert.ToInt32(reader["QuestionID"]);
                    Answer.answerName = reader["AnswerName"].ToString();
                    Answer.name = reader["Name"].ToString();
                    Answer.email = reader["Email"].ToString();
                    Answer.isApprove = Convert.ToBoolean(reader["IsApprove"]);
                    Answer.createDate = Convert.ToDateTime(reader["CreateDate"]);
                    if (reader["UserID"] != DBNull.Value)
                    {
                        Answer.userID = int.Parse(reader["UserID"].ToString());
                    }
                    if (reader["QuestionName"] != DBNull.Value)
                    {
                        Answer.questionName = reader["QuestionName"].ToString();
                    }
                    if (reader["QuestionUrl"] != DBNull.Value)
                    {
                        Answer.questionUrl = reader["QuestionUrl"].ToString();
                    }

                    if (reader["TotalAnswerApproved"] != DBNull.Value)
                    {
                        Answer.totalAnswerApproved = int.Parse(reader["TotalAnswerApproved"].ToString());
                    }
                    AnswerList.Add(Answer);

                }
            }
            finally
            {
                reader.Close();
            }

            return AnswerList;

        }

        /// <summary>
        /// Gets an IList with all instances of Answer.
        /// </summary>
        public static List<Answer> GetAll()
        {
            IDataReader reader = DBAnswer.GetAll();
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of Answer.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<Answer> GetPage(int questionID, int isApprove, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBAnswer.GetPage(questionID, isApprove, pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }


        public static List<Answer> GetPageForUser(int userID, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBAnswer.GetPageForUser(userID, pageNumber, pageSize, out totalPages);
            return LoadListFromReaderForUser(reader);
        }
        #endregion

    }
}

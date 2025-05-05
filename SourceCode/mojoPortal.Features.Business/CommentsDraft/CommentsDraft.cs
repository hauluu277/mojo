
// Author:					Trieubv
// Created:					2015-12-2
// Last Modified:			2015-12-2
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

    public class CommentsDraft
    {

        #region Constructors

        public CommentsDraft()
        { }


        public CommentsDraft(
            int itemID)
        {
            GetCommentsDraft(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int siteID = -1;
        private int pageID = -1;
        private int moduleID = -1;
        private string name = string.Empty;
        private string email = string.Empty;
        private string address = string.Empty;
        private string mobile = string.Empty;
        private string comment = string.Empty;
        private string filePath = string.Empty;
        private DateTime dateCreated = DateTime.UtcNow;
        private string fTS = string.Empty;
        private bool isApproved = false;
        private bool isPublished = false;
        private DateTime dateApproved = DateTime.UtcNow;
        private bool datePublished = false;
        private int userApproved = -1;
        private int userPublished = -1;
        private int duThaoID = -1;
        private string itemUrl = string.Empty;

        #endregion

        #region Public Properties

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
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }
        public DateTime DateCreated
        {
            get { return dateCreated; }
            set { dateCreated = value; }
        }
        public string FTS
        {
            get { return fTS; }
            set { fTS = value; }
        }
        public bool IsApproved
        {
            get { return isApproved; }
            set { isApproved = value; }
        }
        public bool IsPublished
        {
            get { return isPublished; }
            set { isPublished = value; }
        }
        public DateTime DateApproved
        {
            get { return dateApproved; }
            set { dateApproved = value; }
        }
        public bool DatePublished
        {
            get { return datePublished; }
            set { datePublished = value; }
        }
        public int UserApproved
        {
            get { return userApproved; }
            set { userApproved = value; }
        }
        public int UserPublished
        {
            get { return userPublished; }
            set { userPublished = value; }
        }
        public int DuThaoID
        {
            get { return duThaoID; }
            set { duThaoID = value; }
        }
        public string ItemUrl
        {
            get { return itemUrl; }
            set { itemUrl = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of CommentsDraft.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetCommentsDraft(
            int itemID)
        {
            using (IDataReader reader = DBCommentsDraft.GetOne(
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
                this.moduleID = Convert.ToInt32(reader["ModuleID"]);
                this.name = reader["Name"].ToString();
                this.email = reader["Email"].ToString();
                this.address = reader["Address"].ToString();
                this.mobile = reader["Mobile"].ToString();
                this.comment = reader["Comment"].ToString();
                this.filePath = reader["FilePath"].ToString();
                this.dateCreated = Convert.ToDateTime(reader["DateCreated"]);
                this.fTS = reader["FTS"].ToString();
                this.isApproved = Convert.ToBoolean(reader["IsApproved"]);
                this.isPublished = Convert.ToBoolean(reader["IsPublished"]);
                this.dateApproved = Convert.ToDateTime(reader["DateApproved"]);
                this.datePublished = Convert.ToBoolean(reader["DatePublished"]);
                this.userApproved = Convert.ToInt32(reader["UserApproved"]);
                this.userPublished = Convert.ToInt32(reader["UserPublished"]);
                this.duThaoID = Convert.ToInt32(reader["DuThaoID"]);
                this.itemUrl = reader["DuThaoID"].ToString();
            }

        }

        /// <summary>
        /// Persists a new instance of CommentsDraft. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBCommentsDraft.Create(
                this.siteID,
                this.pageID,
                this.moduleID,
                this.name,
                this.email,
                this.address,
                this.mobile,
                this.comment,
                this.filePath,
                this.dateCreated,
                this.fTS,
                this.isApproved,
                this.isPublished,
                this.dateApproved,
                this.datePublished,
                this.userApproved,
                this.userPublished,
                this.duThaoID,
                this.itemUrl);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of CommentsDraft. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBCommentsDraft.Update(
                this.itemID,
                this.siteID,
                this.pageID,
                this.moduleID,
                this.name,
                this.email,
                this.address,
                this.mobile,
                this.comment,
                this.filePath,
                this.dateCreated,
                this.fTS,
                this.isApproved,
                this.isPublished,
                this.dateApproved,
                this.datePublished,
                this.userApproved,
                this.userPublished,
                this.duThaoID,
                this.itemUrl);
            

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of CommentsDraft. Returns true on success.
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
        /// Deletes an instance of CommentsDraft. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBCommentsDraft.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of CommentsDraft. 
        /// </summary>
        public static int GetCount(int duThaoID, int isApprove, int isPublished)
        {
            return DBCommentsDraft.GetCount(duThaoID, isApprove, isPublished);
        }

        private static List<CommentsDraft> LoadListFromReader(IDataReader reader)
        {
            List<CommentsDraft> commentsDraftList = new List<CommentsDraft>();
            try
            {
                while (reader.Read())
                {
                    CommentsDraft commentsDraft = new CommentsDraft();
                    commentsDraft.itemID = Convert.ToInt32(reader["ItemID"]);
                    commentsDraft.siteID = Convert.ToInt32(reader["SiteID"]);
                    commentsDraft.pageID = Convert.ToInt32(reader["PageID"]);
                    commentsDraft.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    commentsDraft.name = reader["Name"].ToString();
                    commentsDraft.email = reader["Email"].ToString();
                    commentsDraft.address = reader["Address"].ToString();
                    commentsDraft.mobile = reader["Mobile"].ToString();
                    commentsDraft.comment = reader["Comment"].ToString();
                    commentsDraft.filePath = reader["FilePath"].ToString();
                    commentsDraft.dateCreated = Convert.ToDateTime(reader["DateCreated"]);
                    commentsDraft.fTS = reader["FTS"].ToString();
                    commentsDraft.isApproved = Convert.ToBoolean(reader["IsApproved"]);
                    commentsDraft.isPublished = Convert.ToBoolean(reader["IsPublished"]);
                    commentsDraft.dateApproved = Convert.ToDateTime(reader["DateApproved"]);
                    commentsDraft.datePublished = Convert.ToBoolean(reader["DatePublished"]);
                    commentsDraft.userApproved = Convert.ToInt32(reader["UserApproved"]);
                    commentsDraft.userPublished = Convert.ToInt32(reader["UserPublished"]);
                    commentsDraft.duThaoID = Convert.ToInt32(reader["DuThaoID"]);
                    commentsDraft.itemUrl = reader["ItemUrl"].ToString();
                    commentsDraftList.Add(commentsDraft);

                }
            }
            finally
            {
                reader.Close();
            }

            return commentsDraftList;

        }

        /// <summary>
        /// Gets an IList with all instances of CommentsDraft.
        /// </summary>
        public static List<CommentsDraft> GetAll()
        {
            IDataReader reader = DBCommentsDraft.GetAll();
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of CommentsDraft.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<CommentsDraft> GetPage(int duThaoID, int isApprove, int isPublished, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBCommentsDraft.GetPage(duThaoID, isApprove, isPublished, pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of CommentsDraft.
        /// </summary>
        public static int CompareByItemID(CommentsDraft commentsDraft1, CommentsDraft commentsDraft2)
        {
            return commentsDraft1.ItemID.CompareTo(commentsDraft2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of CommentsDraft.
        /// </summary>
        public static int CompareBySiteID(CommentsDraft commentsDraft1, CommentsDraft commentsDraft2)
        {
            return commentsDraft1.SiteID.CompareTo(commentsDraft2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of CommentsDraft.
        /// </summary>
        public static int CompareByPageID(CommentsDraft commentsDraft1, CommentsDraft commentsDraft2)
        {
            return commentsDraft1.PageID.CompareTo(commentsDraft2.PageID);
        }
        /// <summary>
        /// Compares 2 instances of CommentsDraft.
        /// </summary>
        public static int CompareByModuleID(CommentsDraft commentsDraft1, CommentsDraft commentsDraft2)
        {
            return commentsDraft1.ModuleID.CompareTo(commentsDraft2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of CommentsDraft.
        /// </summary>
        public static int CompareByName(CommentsDraft commentsDraft1, CommentsDraft commentsDraft2)
        {
            return commentsDraft1.Name.CompareTo(commentsDraft2.Name);
        }
        /// <summary>
        /// Compares 2 instances of CommentsDraft.
        /// </summary>
        public static int CompareByEmail(CommentsDraft commentsDraft1, CommentsDraft commentsDraft2)
        {
            return commentsDraft1.Email.CompareTo(commentsDraft2.Email);
        }
        /// <summary>
        /// Compares 2 instances of CommentsDraft.
        /// </summary>
        public static int CompareByAddress(CommentsDraft commentsDraft1, CommentsDraft commentsDraft2)
        {
            return commentsDraft1.Address.CompareTo(commentsDraft2.Address);
        }
        /// <summary>
        /// Compares 2 instances of CommentsDraft.
        /// </summary>
        public static int CompareByMobile(CommentsDraft commentsDraft1, CommentsDraft commentsDraft2)
        {
            return commentsDraft1.Mobile.CompareTo(commentsDraft2.Mobile);
        }
        /// <summary>
        /// Compares 2 instances of CommentsDraft.
        /// </summary>
        public static int CompareByComment(CommentsDraft commentsDraft1, CommentsDraft commentsDraft2)
        {
            return commentsDraft1.Comment.CompareTo(commentsDraft2.Comment);
        }
        /// <summary>
        /// Compares 2 instances of CommentsDraft.
        /// </summary>
        public static int CompareByFilePath(CommentsDraft commentsDraft1, CommentsDraft commentsDraft2)
        {
            return commentsDraft1.FilePath.CompareTo(commentsDraft2.FilePath);
        }
        /// <summary>
        /// Compares 2 instances of CommentsDraft.
        /// </summary>
        public static int CompareByDateCreated(CommentsDraft commentsDraft1, CommentsDraft commentsDraft2)
        {
            return commentsDraft1.DateCreated.CompareTo(commentsDraft2.DateCreated);
        }
        /// <summary>
        /// Compares 2 instances of CommentsDraft.
        /// </summary>
        public static int CompareByFTS(CommentsDraft commentsDraft1, CommentsDraft commentsDraft2)
        {
            return commentsDraft1.FTS.CompareTo(commentsDraft2.FTS);
        }
        /// <summary>
        /// Compares 2 instances of CommentsDraft.
        /// </summary>
        public static int CompareByDateApproved(CommentsDraft commentsDraft1, CommentsDraft commentsDraft2)
        {
            return commentsDraft1.DateApproved.CompareTo(commentsDraft2.DateApproved);
        }
        /// <summary>
        /// Compares 2 instances of CommentsDraft.
        /// </summary>
        public static int CompareByUserApproved(CommentsDraft commentsDraft1, CommentsDraft commentsDraft2)
        {
            return commentsDraft1.UserApproved.CompareTo(commentsDraft2.UserApproved);
        }
        /// <summary>
        /// Compares 2 instances of CommentsDraft.
        /// </summary>
        public static int CompareByUserPublished(CommentsDraft commentsDraft1, CommentsDraft commentsDraft2)
        {
            return commentsDraft1.UserPublished.CompareTo(commentsDraft2.UserPublished);
        }

        #endregion


    }

}






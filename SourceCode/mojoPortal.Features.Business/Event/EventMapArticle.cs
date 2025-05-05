
// Author:					NamDV
// Created:					2015-9-16
// Last Modified:			2015-9-16
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
using EventFeature.Data;

namespace EventFeature.Business
{

    public class EventMapArticle
    {

        #region Constructors

        public EventMapArticle()
        { }


        public EventMapArticle(
            int itemID)
        {
            GetEventMapArticle(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int articleID = -1;
        private int eventID = -1;
        private string name = string.Empty;
        private string url = string.Empty;
        #endregion

        #region Public Properties

        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public int ArticleID
        {
            get { return articleID; }
            set { articleID = value; }
        }
        public int EventID
        {
            get { return eventID; }
            set { eventID = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of EventMapArticle.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetEventMapArticle(
            int itemID)
        {
            using (IDataReader reader = DBEventMapArticle.GetOne(
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
                this.articleID = Convert.ToInt32(reader["ArticleID"]);
                this.eventID = Convert.ToInt32(reader["EventID"]);

            }

        }

        /// <summary>
        /// Persists a new instance of EventMapArticle. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBEventMapArticle.Create(
                this.articleID,
                this.eventID);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of EventMapArticle. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBEventMapArticle.Update(
                this.itemID,
                this.articleID,
                this.eventID);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of EventMapArticle. Returns true on success.
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
        /// Deletes an instance of EventMapArticle. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBEventMapArticle.Delete(
                itemID);
        }

        public static bool DeleteAllByArticle(
           int articleID)
        {
            return DBEventMapArticle.DeleteAllByArticle(
                articleID);
        }

        /// <summary>
        /// Gets a count of EventMapArticle. 
        /// </summary>
        public static int GetCount()
        {
            return DBEventMapArticle.GetCount();
        }

        private static List<EventMapArticle> LoadListFromReader(IDataReader reader)
        {
            List<EventMapArticle> eventMapArticleList = new List<EventMapArticle>();
            try
            {
                while (reader.Read())
                {
                    EventMapArticle eventMapArticle = new EventMapArticle();
                    eventMapArticle.itemID = Convert.ToInt32(reader["ItemID"]);
                    eventMapArticle.articleID = Convert.ToInt32(reader["ArticleID"]);
                    eventMapArticle.eventID = Convert.ToInt32(reader["EventID"]);
                    if (!string.IsNullOrEmpty(reader["Name"].ToString()))
                    {
                        eventMapArticle.name = reader["Name"].ToString();
                    }
                    if (!string.IsNullOrEmpty(reader["Url"].ToString()))
                    {
                        eventMapArticle.url = reader["Url"].ToString();
                    }
                    eventMapArticleList.Add(eventMapArticle);

                }
            }
            finally
            {
                reader.Close();
            }

            return eventMapArticleList;

        }

        /// <summary>
        /// Gets an IList with all instances of EventMapArticle.
        /// </summary>
        public static List<EventMapArticle> GetAll()
        {
            IDataReader reader = DBEventMapArticle.GetAll();
            return LoadListFromReader(reader);

        }
        public static List<int> GetByArticleID(int article)
        {
            List<int> lstEventID = new List<int>();
            IDataReader reader = DBEventMapArticle.GetByArticleID(article);
            try
            {
                while (reader.Read())
                {
                    if (!string.IsNullOrEmpty(reader["EventID"].ToString()))
                    {
                        lstEventID.Add(int.Parse(reader["EventID"].ToString()));
                    }
                }
            }
            finally
            {
                reader.Close();
            }
            return lstEventID;
        }

        public static List<EventMapArticle> GetAllByArticle(int articleID)
        {
            IDataReader reader = DBEventMapArticle.GetAllByArticle(articleID);
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of EventMapArticle.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<EventMapArticle> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBEventMapArticle.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of EventMapArticle.
        /// </summary>
        public static int CompareByItemID(EventMapArticle eventMapArticle1, EventMapArticle eventMapArticle2)
        {
            return eventMapArticle1.ItemID.CompareTo(eventMapArticle2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of EventMapArticle.
        /// </summary>
        public static int CompareByArticleID(EventMapArticle eventMapArticle1, EventMapArticle eventMapArticle2)
        {
            return eventMapArticle1.ArticleID.CompareTo(eventMapArticle2.ArticleID);
        }
        /// <summary>
        /// Compares 2 instances of EventMapArticle.
        /// </summary>
        public static int CompareByEventID(EventMapArticle eventMapArticle1, EventMapArticle eventMapArticle2)
        {
            return eventMapArticle1.EventID.CompareTo(eventMapArticle2.EventID);
        }

        #endregion


    }

}






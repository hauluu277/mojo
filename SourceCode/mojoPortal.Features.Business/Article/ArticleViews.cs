
// Author:					HAU XOAC
// Created:					2017-11-2
// Last Modified:			2017-11-2
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

    public class ArticleViews
    {

        #region Constructors

        public ArticleViews()
        { }


        public ArticleViews(
            decimal itemID)
        {
            GetArticleViews(
                itemID);
        }

        #endregion

        #region Private Properties

        private decimal itemID;
        private int articleID = -1;
        private int totalView = -1;
        private DateTime dayView = DateTime.UtcNow;

        #endregion

        #region Public Properties

        public decimal ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public int ArticleID
        {
            get { return articleID; }
            set { articleID = value; }
        }
        public int TotalView
        {
            get { return totalView; }
            set { totalView = value; }
        }
        public DateTime DayView
        {
            get { return dayView; }
            set { dayView = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of ArticleViews.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetArticleViews(
            decimal itemID)
        {
            using (IDataReader reader = DBArticleViews.GetOne(
                itemID))
            {
                PopulateFromReader(reader);
            }

        }


        private void PopulateFromReader(IDataReader reader)
        {
            if (reader.Read())
            {
                this.itemID = Convert.ToDecimal(reader["ItemID"]);
                this.articleID = Convert.ToInt32(reader["ArticleID"]);
                this.totalView = Convert.ToInt32(reader["TotalView"]);
                this.dayView = Convert.ToDateTime(reader["DayView"]);

            }

        }

        /// <summary>
        /// Persists a new instance of ArticleViews. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBArticleViews.Create(
                this.articleID,
                this.totalView,
                this.dayView);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of ArticleViews. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBArticleViews.Update(
                this.itemID,
                this.articleID,
                this.totalView,
                this.dayView);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of ArticleViews. Returns true on success.
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
        /// Deletes an instance of ArticleViews. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            decimal itemID)
        {
            return DBArticleViews.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of ArticleViews. 
        /// </summary>
        public static int GetCount()
        {
            return DBArticleViews.GetCount();
        }

        public static bool IsExit(int articleID, string currentDate)
        {
            return DBArticleViews.IsExit(articleID, currentDate);
        }
        public static bool UpdateView(int articleID, string currentDay)
        {
            return DBArticleViews.UpdateView(articleID, currentDay);
        }

        private static List<ArticleViews> LoadListFromReader(IDataReader reader)
        {
            List<ArticleViews> ArticleViewsList = new List<ArticleViews>();
            try
            {
                while (reader.Read())
                {
                    ArticleViews ArticleViews = new ArticleViews();
                    ArticleViews.itemID = Convert.ToDecimal(reader["ItemID"]);
                    ArticleViews.articleID = Convert.ToInt32(reader["ArticleID"]);
                    ArticleViews.totalView = Convert.ToInt32(reader["TotalView"]);
                    ArticleViews.dayView = Convert.ToDateTime(reader["DayView"]);
                    ArticleViewsList.Add(ArticleViews);

                }
            }
            finally
            {
                reader.Close();
            }

            return ArticleViewsList;

        }

        /// <summary>
        /// Gets an IList with all instances of ArticleViews.
        /// </summary>
        public static List<ArticleViews> GetAll()
        {
            IDataReader reader = DBArticleViews.GetAll();
            return LoadListFromReader(reader);

        }

        /// <summary>
        /// Gets an IList with page of instances of ArticleViews.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<ArticleViews> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBArticleViews.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of ArticleViews.
        /// </summary>
        public static int CompareByArticleID(ArticleViews ArticleViews1, ArticleViews ArticleViews2)
        {
            return ArticleViews1.ArticleID.CompareTo(ArticleViews2.ArticleID);
        }
        /// <summary>
        /// Compares 2 instances of ArticleViews.
        /// </summary>
        public static int CompareByTotalView(ArticleViews ArticleViews1, ArticleViews ArticleViews2)
        {
            return ArticleViews1.TotalView.CompareTo(ArticleViews2.TotalView);
        }
        /// <summary>
        /// Compares 2 instances of ArticleViews.
        /// </summary>
        public static int CompareByDayView(ArticleViews ArticleViews1, ArticleViews ArticleViews2)
        {
            return ArticleViews1.DayView.CompareTo(ArticleViews2.DayView);
        }

        #endregion


    }

}






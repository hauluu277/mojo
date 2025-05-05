
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

    public class Event
    {

        #region Constructors

        public Event()
        { }


        public Event(
            int itemID)
        {
            GetEvent(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int moduleID = -1;
        private int siteID = -1;
        private int categoryID = -1;
        private string title = string.Empty;
        private string summary = string.Empty;
        private string description = string.Empty;
        private string imageUrl = string.Empty;
        private DateTime startDate = DateTime.UtcNow;
        private DateTime? endDate = null;
        private int commentCount = -1;
        private int hitCount = -1;
        private Guid eventGuid = Guid.Empty;
        private Guid moduleGuid = Guid.Empty;
        private string location = string.Empty;
        private Guid userGuid = Guid.Empty;
        private string createdByUser = string.Empty;
        private DateTime createdDate = DateTime.UtcNow;
        private Guid lastModUserGuid = Guid.Empty;
        private DateTime lastModUtc = DateTime.UtcNow;
        private string itemUrl = string.Empty;
        private string metaTitle = string.Empty;
        private string metaKeywords = string.Empty;
        private string metaDescription = string.Empty;
        private bool? isApproved = false;
        private Guid approvedGuid = Guid.Empty;
        private DateTime approvedDate = DateTime.UtcNow;
        private bool? isPublished = false;
        private Guid publishedGuid = Guid.Empty;
        private DateTime publishedDate = DateTime.UtcNow;
        private bool allowComment = false;
        private bool isHot = false;
        private bool isHome = false;
        private string tag = string.Empty;
        private string fTS = string.Empty;
        private int langID = -1;
        private string commentByBoss = string.Empty;
        private string categoryName = string.Empty;
        private DateTime? startTime = null;
        private DateTime? endTime = null;
        #endregion

        #region Public Properties
        public DateTime? StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        public DateTime? EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        public int ItemID
        {
            get { return itemID; }
            set { itemID = value; }
        }
        public int ModuleID
        {
            get { return moduleID; }
            set { moduleID = value; }
        }
        public int SiteID
        {
            get { return siteID; }
            set { siteID = value; }
        }
        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Summary
        {
            get { return summary; }
            set { summary = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; }
        }
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        public DateTime? EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        public int CommentCount
        {
            get { return commentCount; }
            set { commentCount = value; }
        }
        public int HitCount
        {
            get { return hitCount; }
            set { hitCount = value; }
        }
        public Guid EventGuid
        {
            get { return eventGuid; }
            set { eventGuid = value; }
        }
        public Guid ModuleGuid
        {
            get { return moduleGuid; }
            set { moduleGuid = value; }
        }
        public string Location
        {
            get { return location; }
            set { location = value; }
        }
        public Guid UserGuid
        {
            get { return userGuid; }
            set { userGuid = value; }
        }
        public string CreatedByUser
        {
            get { return createdByUser; }
            set { createdByUser = value; }
        }
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }
        public Guid LastModUserGuid
        {
            get { return lastModUserGuid; }
            set { lastModUserGuid = value; }
        }
        public DateTime LastModUtc
        {
            get { return lastModUtc; }
            set { lastModUtc = value; }
        }
        public string ItemUrl
        {
            get { return itemUrl; }
            set { itemUrl = value; }
        }
        public string MetaTitle
        {
            get { return metaTitle; }
            set { metaTitle = value; }
        }
        public string MetaKeywords
        {
            get { return metaKeywords; }
            set { metaKeywords = value; }
        }
        public string MetaDescription
        {
            get { return metaDescription; }
            set { metaDescription = value; }
        }
        public bool? IsApproved
        {
            get { return isApproved; }
            set { isApproved = value; }
        }
        public Guid ApprovedGuid
        {
            get { return approvedGuid; }
            set { approvedGuid = value; }
        }
        public DateTime ApprovedDate
        {
            get { return approvedDate; }
            set { approvedDate = value; }
        }
        public bool? IsPublished
        {
            get { return isPublished; }
            set { isPublished = value; }
        }
        public Guid PublishedGuid
        {
            get { return publishedGuid; }
            set { publishedGuid = value; }
        }
        public DateTime PublishedDate
        {
            get { return publishedDate; }
            set { publishedDate = value; }
        }
        public bool AllowComment
        {
            get { return allowComment; }
            set { allowComment = value; }
        }
        public bool IsHot
        {
            get { return isHot; }
            set { isHot = value; }
        }
        public bool IsHome
        {
            get { return isHome; }
            set { isHome = value; }
        }
        public string Tag
        {
            get { return tag; }
            set { tag = value; }
        }
        public string FTS
        {
            get { return fTS; }
            set { fTS = value; }
        }
        public int LangID
        {
            get { return langID; }
            set { langID = value; }
        }

        public string CommentByBoss
        {
            get { return commentByBoss; }
            set { commentByBoss = value; }
        }
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }
        //
        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of _event.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetEvent(
            int itemID)
        {
            using (IDataReader reader = DBEvents.GetOne(
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
                this.moduleID = Convert.ToInt32(reader["ModuleID"]);
                this.siteID = Convert.ToInt32(reader["SiteID"]);
                this.categoryID = Convert.ToInt32(reader["CategoryID"]);
                this.title = reader["Title"].ToString();
                this.summary = reader["Summary"].ToString();
                this.description = reader["Description"].ToString();
                this.imageUrl = reader["ImageUrl"].ToString();
                if (!string.IsNullOrEmpty(reader["StartDate"].ToString()))
                {
                    this.startDate = Convert.ToDateTime(reader["StartDate"]);
                }
                if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                {
                    this.endDate = Convert.ToDateTime(reader["EndDate"]);
                }
                this.commentCount = Convert.ToInt32(reader["CommentCount"]);
                this.hitCount = Convert.ToInt32(reader["HitCount"]);
                this.eventGuid = new Guid(reader["EventGuid"].ToString());
                this.moduleGuid = new Guid(reader["ModuleGuid"].ToString());
                this.location = reader["Location"].ToString();
                this.userGuid = new Guid(reader["UserGuid"].ToString());
                this.createdByUser = reader["CreatedByUser"].ToString();
                this.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                this.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                this.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                this.itemUrl = reader["ItemUrl"].ToString();
                this.metaTitle = reader["MetaTitle"].ToString();
                this.metaKeywords = reader["MetaKeywords"].ToString();
                this.metaDescription = reader["MetaDescription"].ToString();
                this.isApproved = Convert.ToBoolean(reader["IsApproved"]);
                this.approvedGuid = new Guid(reader["ApprovedGuid"].ToString());
                this.approvedDate = Convert.ToDateTime(reader["ApprovedDate"]);
                this.isPublished = Convert.ToBoolean(reader["IsPublished"]);
                this.publishedGuid = new Guid(reader["PublishedGuid"].ToString());
                this.publishedDate = Convert.ToDateTime(reader["PublishedDate"]);
                this.allowComment = Convert.ToBoolean(reader["AllowComment"]);
                this.isHot = Convert.ToBoolean(reader["IsHot"]);
                this.isHome = Convert.ToBoolean(reader["IsHome"]);
                this.tag = reader["Tag"].ToString();
                this.fTS = reader["FTS"].ToString();
                this.langID = Convert.ToInt32(reader["LangID"]);
                if (!string.IsNullOrEmpty(reader["StartTime"].ToString()))
                {
                    this.startTime = Convert.ToDateTime(reader["StartTime"]);
                }
                if (!string.IsNullOrEmpty(reader["EndTime"].ToString()))
                {
                    this.endTime = Convert.ToDateTime(reader["EndTime"]);
                }

            }

        }

        /// <summary>
        /// Persists a new instance of _event. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;
            eventGuid = Guid.NewGuid();
            newID = DBEvents.Create(
                this.moduleID,
                this.siteID,
                this.categoryID,
                this.title,
                this.summary,
                this.description,
                this.imageUrl,
                this.startDate,
                this.endDate,
                this.commentCount,
                this.hitCount,
                this.eventGuid,
                this.moduleGuid,
                this.location,
                this.userGuid,
                this.createdByUser,
                this.createdDate,
                this.lastModUserGuid,
                this.lastModUtc,
                this.itemUrl,
                this.metaTitle,
                this.metaKeywords,
                this.metaDescription,
                this.isApproved,
                this.approvedGuid,
                this.approvedDate,
                this.isPublished,
                this.publishedGuid,
                this.publishedDate,
                this.allowComment,
                this.isHot,
                this.isHome,
                this.tag,
                this.fTS,
                this.langID,
                this.commentByBoss,
                this.startTime,
                this.endTime);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of _event. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {
            if (this.eventGuid == Guid.Empty)
            {
                this.eventGuid = Guid.NewGuid();
            }
            return DBEvents.Update(
                this.itemID,
                this.moduleID,
                this.siteID,
                this.categoryID,
                this.title,
                this.summary,
                this.description,
                this.imageUrl,
                this.startDate,
                this.endDate,
                this.commentCount,
                this.hitCount,
                this.eventGuid,
                this.moduleGuid,
                this.location,
                this.userGuid,
                this.createdByUser,
                this.createdDate,
                this.lastModUserGuid,
                this.lastModUtc,
                this.itemUrl,
                this.metaTitle,
                this.metaKeywords,
                this.metaDescription,
                this.isApproved,
                this.approvedGuid,
                this.approvedDate,
                this.isPublished,
                this.publishedGuid,
                this.publishedDate,
                this.allowComment,
                this.isHot,
                this.isHome,
                this.tag,
                this.fTS,
                this.langID,
                this.commentByBoss,
                this.startTime,
                this.endTime);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of _event. Returns true on success.
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
        /// Deletes an instance of _event. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBEvents.Delete(
                itemID);
        }


        /// <summary>
        /// Gets a count of _event. 
        /// </summary>
        public static int GetCount()
        {
            return DBEvents.GetCount();
        }

        private static List<Event> LoadListFromReader(IDataReader reader, bool isLoadCategory = true)
        {
            List<Event> eventList = new List<Event>();
            try
            {
                while (reader.Read())
                {
                    Event _event = new Event();
                    _event.itemID = Convert.ToInt32(reader["ItemID"]);
                    _event.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    _event.siteID = Convert.ToInt32(reader["SiteID"]);
                    _event.categoryID = Convert.ToInt32(reader["CategoryID"]);
                    _event.title = reader["Title"].ToString();
                    _event.summary = reader["Summary"].ToString();
                    _event.description = reader["Description"].ToString();
                    _event.imageUrl = reader["ImageUrl"].ToString();
                    if (!string.IsNullOrEmpty(reader["StartDate"].ToString()))
                    {
                        _event.startDate = Convert.ToDateTime(reader["StartDate"]);
                    }
                    if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                    {
                        _event.endDate = Convert.ToDateTime(reader["EndDate"]);
                    }
                    _event.commentCount = Convert.ToInt32(reader["CommentCount"]);
                    _event.hitCount = Convert.ToInt32(reader["HitCount"]);
                    if (reader["EventGuid"] != DBNull.Value)
                    {
                        _event.eventGuid = new Guid(reader["EventGuid"].ToString());
                    }
                    _event.moduleGuid = new Guid(reader["ModuleGuid"].ToString());
                    _event.location = reader["Location"].ToString();
                    _event.userGuid = new Guid(reader["UserGuid"].ToString());
                    _event.createdByUser = reader["CreatedByUser"].ToString();
                    _event.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    _event.lastModUserGuid = new Guid(reader["LastModUserGuid"].ToString());
                    _event.lastModUtc = Convert.ToDateTime(reader["LastModUtc"]);
                    _event.itemUrl = reader["ItemUrl"].ToString();
                    _event.metaTitle = reader["MetaTitle"].ToString();
                    _event.metaKeywords = reader["MetaKeywords"].ToString();
                    _event.metaDescription = reader["MetaDescription"].ToString();
                    _event.isApproved = Convert.ToBoolean(reader["IsApproved"]);
                    _event.approvedGuid = new Guid(reader["ApprovedGuid"].ToString());
                    _event.approvedDate = Convert.ToDateTime(reader["ApprovedDate"]);
                    _event.isPublished = Convert.ToBoolean(reader["IsPublished"]);
                    _event.publishedGuid = new Guid(reader["PublishedGuid"].ToString());
                    _event.publishedDate = Convert.ToDateTime(reader["PublishedDate"]);
                    _event.allowComment = Convert.ToBoolean(reader["AllowComment"]);
                    _event.isHot = Convert.ToBoolean(reader["IsHot"]);
                    _event.isHome = Convert.ToBoolean(reader["IsHome"]);
                    _event.tag = reader["Tag"].ToString();
                    _event.fTS = reader["FTS"].ToString();
                    _event.langID = Convert.ToInt32(reader["LangID"]);
                    if (reader["CommentByBoss"] != DBNull.Value)
                    {
                        if (!String.IsNullOrEmpty(reader["CommentByBoss"].ToString()))
                        {
                            _event.commentByBoss = reader["CommentByBoss"].ToString();
                        }
                    }
                    if (isLoadCategory)
                    {
                        if (reader["CategoryName"] != DBNull.Value)
                        {
                            _event.categoryName = reader["CategoryName"].ToString();
                        }
                    }
                    if (!string.IsNullOrEmpty(reader["StartTime"].ToString()))
                    {
                        _event.startTime = Convert.ToDateTime(reader["StartTime"]);
                    }
                    if (!string.IsNullOrEmpty(reader["EndTime"].ToString()))
                    {
                        _event.endTime = Convert.ToDateTime(reader["EndTime"]);
                    }
                    eventList.Add(_event);

                }
            }
            finally
            {
                reader.Close();
            }

            return eventList;

        }

        public static List<Event> GetEventHot(int siteID, int number)
        {
            IDataReader reader = DBEvents.GetEventHot(siteID, number);
            return LoadListFromReader(reader);
        }
        public static List<Event> GetOneEvent(int itemID)
        {
            IDataReader reader = DBEvents.GetOne(itemID);
            return LoadListFromReader(reader);
        }


        /// <summary>
        /// Gets an IList with all instances of _event.
        /// </summary>
        public static List<Event> GetAll()
        {
            IDataReader reader = DBEvents.GetAll();
            return LoadListFromReader(reader);

        }

        public static List<Event> GetAllForArticle(int siteID)
        {
            IDataReader reader = DBEvents.GetAllForArticle(siteID);
            return LoadListFromReader(reader);

        }

        public static List<Event> GetTopHot(int top, int siteId)
        {
            IDataReader reader = DBEvents.GetTopHot(top, siteId);
            return LoadListFromReader(reader, false);
        }

        public static List<Event> GetTopOrther(int top, int siteId, string skips)
        {
            IDataReader reader = DBEvents.GetTopOrther(top, siteId, skips);
            return LoadListFromReader(reader, false);
        }

        public static List<Event> GetTopSkipId(int top, int siteId, int itemId)
        {
            IDataReader reader = DBEvents.GetTopSkipId(top, siteId, itemId);
            return LoadListFromReader(reader, false);
        }


        /// <summary>
        /// Gets an IList with page of instances of _event.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<Event> GetPage(int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBEvents.GetPage(pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }


        public static List<Event> GetPage(int siteId, int moduleId, int pageNumber, int pageSize, int categoryID, int isApprove, int isPublish, string keyword, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBEvents.GetPage(siteId, moduleId, pageNumber, pageSize, categoryID, isApprove, isPublish, keyword, out totalPages);
            return LoadListFromReader(reader);
        }

        public static List<Event> GetPageForEndUser(int siteId, int moduleId, int pageNumber, int pageSize, int categoryID, string keyword, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBEvents.GetPageForEndUser(siteId, moduleId, pageNumber, pageSize, categoryID, keyword, out totalPages);
            return LoadListFromReader(reader);
        }

        public static List<Event> GetOthersPageByModuleId(int moduleID, int pageSize, int currentPage)
        {
            IDataReader reader = DBEvents.GetOthersPageByModuleId(moduleID, pageSize, currentPage);
            return LoadListFromReader(reader);
        }

        public static List<Event> GetArticleTopOrther(int categoryID, int ItemId, int Top, bool Hot)
        {
            IDataReader reader = DBEvents.GetEventTopOrther(categoryID, ItemId, Top, Hot);
            return LoadListFromReader(reader);
        }

        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of _event.
        /// </summary>
        public static int CompareByItemID(Event event1, Event event2)
        {
            return event1.ItemID.CompareTo(event2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of _event.
        /// </summary>
        public static int CompareByModuleID(Event event1, Event event2)
        {
            return event1.ModuleID.CompareTo(event2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of _event.
        /// </summary>
        public static int CompareBySiteID(Event event1, Event event2)
        {
            return event1.SiteID.CompareTo(event2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of _event.
        /// </summary>
        public static int CompareByCategoryID(Event event1, Event event2)
        {
            return event1.CategoryID.CompareTo(event2.CategoryID);
        }
        /// <summary>
        /// Compares 2 instances of _event.
        /// </summary>
        public static int CompareByTitle(Event event1, Event event2)
        {
            return event1.Title.CompareTo(event2.Title);
        }
        /// <summary>
        /// Compares 2 instances of _event.
        /// </summary>
        public static int CompareBySummary(Event event1, Event event2)
        {
            return event1.Summary.CompareTo(event2.Summary);
        }
        /// <summary>
        /// Compares 2 instances of _event.
        /// </summary>
        public static int CompareByDescription(Event event1, Event event2)
        {
            return event1.Description.CompareTo(event2.Description);
        }
        /// <summary>
        /// Compares 2 instances of _event.
        /// </summary>
        public static int CompareByImageUrl(Event event1, Event event2)
        {
            return event1.ImageUrl.CompareTo(event2.ImageUrl);
        }
        /// <summary>
        /// Compares 2 instances of _event.
        /// </summary>
        public static int CompareByStartDate(Event event1, Event event2)
        {
            return event1.StartDate.CompareTo(event2.StartDate);
        }
        /// <summary>
        /// Compares 2 instances of _event.
        /// </summary>
        public static int CompareByEndDate(Event event1, Event event2)
        {
            return event1.EndDate.Value.CompareTo(event2.EndDate.Value);
        }
        /// <summary>
        /// Compares 2 instances of _event.
        /// </summary>
        public static int CompareByCommentCount(Event event1, Event event2)
        {
            return event1.CommentCount.CompareTo(event2.CommentCount);
        }
        /// <summary>
        /// Compares 2 instances of _event.
        /// </summary>
        public static int CompareByHitCount(Event event1, Event event2)
        {
            return event1.HitCount.CompareTo(event2.HitCount);
        }
        /// <summary>
        /// Compares 2 instances of _event.
        /// </summary>
        public static int CompareByLocation(Event event1, Event event2)
        {
            return event1.Location.CompareTo(event2.Location);
        }
        /// <summary>
        /// Compares 2 instances of _event.
        /// </summary>
        public static int CompareByCreatedByUser(Event event1, Event event2)
        {
            return event1.CreatedByUser.CompareTo(event2.CreatedByUser);
        }
        /// <summary>
        /// Compares 2 instances of _event.
        /// </summary>
        public static int CompareByCreatedDate(Event event1, Event event2)
        {
            return event1.CreatedDate.CompareTo(event2.CreatedDate);
        }
        /// <summary>
        /// Compares 2 instances of _event.
        /// </summary>
        public static int CompareByLastModUtc(Event event1, Event event2)
        {
            return event1.LastModUtc.CompareTo(event2.LastModUtc);
        }
        /// <summary>
        /// Compares 2 instances of _event.
        /// </summary>
        public static int CompareByItemUrl(Event event1, Event event2)
        {
            return event1.ItemUrl.CompareTo(event2.ItemUrl);
        }
        /// <summary>
        /// Compares 2 instances of _event.
        /// </summary>
        public static int CompareByMetaTitle(Event event1, Event event2)
        {
            return event1.MetaTitle.CompareTo(event2.MetaTitle);
        }
        /// <summary>
        /// Compares 2 instances of _event.
        /// </summary>
        public static int CompareByMetaKeywords(Event event1, Event event2)
        {
            return event1.MetaKeywords.CompareTo(event2.MetaKeywords);
        }
        /// <summary>
        /// Compares 2 instances of _event.
        /// </summary>
        public static int CompareByMetaDescription(Event event1, Event event2)
        {
            return event1.MetaDescription.CompareTo(event2.MetaDescription);
        }
        /// <summary>
        /// Compares 2 instances of _event.
        /// </summary>
        public static int CompareByApprovedDate(Event event1, Event event2)
        {
            return event1.ApprovedDate.CompareTo(event2.ApprovedDate);
        }
        /// <summary>
        /// Compares 2 instances of _event.
        /// </summary>
        public static int CompareByPublishedDate(Event event1, Event event2)
        {
            return event1.PublishedDate.CompareTo(event2.PublishedDate);
        }
        /// <summary>
        /// Compares 2 instances of _event.
        /// </summary>
        public static int CompareByTag(Event event1, Event event2)
        {
            return event1.Tag.CompareTo(event2.Tag);
        }
        /// <summary>
        /// Compares 2 instances of _event.
        /// </summary>
        public static int CompareByFTS(Event event1, Event event2)
        {
            return event1.FTS.CompareTo(event2.FTS);
        }
        /// <summary>
        /// Compares 2 instances of _event.
        /// </summary>
        public static int CompareByLangID(Event event1, Event event2)
        {
            return event1.LangID.CompareTo(event2.LangID);
        }

        #endregion


    }

}






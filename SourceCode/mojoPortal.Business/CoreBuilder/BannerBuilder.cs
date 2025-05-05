using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mojoPortal.Data;
using System.Data;

namespace mojoPortal.Business
{

    public class BannerBuilder
    {

        #region Constructors

        public BannerBuilder()
        { }


        public BannerBuilder(
            int itemID)
        {
            GetBanner(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int moduleID = -1;
        private int siteID = -1;
        private int pageID = -1;
        private string name = string.Empty;
        private string description = string.Empty;
        private string link = string.Empty;
        private string path = string.Empty;
        private int hitCount = -1;
        private string width = string.Empty;
        private bool isImage = false;
        private int number = -1;
        private bool isHorizontal = false;
        private DateTime startDate = DateTime.UtcNow;
        private DateTime? endDate = null;
        private bool isFollow = false;
        private bool isTarget = false;
        private string createdByUser = string.Empty;
        private DateTime createdDate = DateTime.UtcNow;
        private bool isPublic = false;
        private int priority = 0;
        private bool isSlideTop = false;
        private bool isSlideBottom = false;
        private bool noClick = false;

        #endregion

        #region Public Properties

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
        public int PageID
        {
            get { return pageID; }
            set { pageID = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string Link
        {
            get { return link; }
            set { link = value; }
        }
        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        public int HitCount
        {
            get { return hitCount; }
            set { hitCount = value; }
        }
        public string Width
        {
            get { return width; }
            set { width = value; }
        }
        public bool IsImage
        {
            get { return isImage; }
            set { isImage = value; }
        }
        public int Number
        {
            get { return number; }
            set { number = value; }
        }
        public bool IsHorizontal
        {
            get { return isHorizontal; }
            set { isHorizontal = value; }
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
        public bool IsFollow
        {
            get { return isFollow; }
            set { isFollow = value; }
        }
        public bool IsTarget
        {
            get { return isTarget; }
            set { isTarget = value; }
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
        public bool IsPublic
        {
            get { return isPublic; }
            set { isPublic = value; }
        }
        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }
        public bool IsSlideTop
        {
            get { return isSlideTop; }
            set { isSlideTop = value; }
        }
        public bool IsSlideBottom
        {
            get { return isSlideBottom; }
            set { isSlideBottom = value; }
        }

        public bool NoClick
        {
            get { return noClick; }
            set { noClick = value; }
        }


        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of Banner.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetBanner(
                int itemID)
        {
            using (IDataReader reader = DBBannerBuilder.GetOne(
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
                this.pageID = Convert.ToInt32(reader["PageID"]);
                this.name = reader["Name"].ToString();
                this.description = reader["Description"].ToString();
                this.link = reader["Link"].ToString();
                this.path = reader["Path"].ToString();
                this.hitCount = Convert.ToInt32(reader["HitCount"]);
                this.width = reader["Width"].ToString();
                this.isImage = Convert.ToBoolean(reader["IsImage"]);
                this.number = Convert.ToInt32(reader["Number"]);
                this.isHorizontal = Convert.ToBoolean(reader["IsHorizontal"]);
                this.startDate = Convert.ToDateTime(reader["StartDate"]);
                if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                {
                    this.endDate = Convert.ToDateTime(reader["EndDate"]);
                }
                this.isFollow = Convert.ToBoolean(reader["IsFollow"]);
                this.isTarget = Convert.ToBoolean(reader["IsTarget"]);
                this.createdByUser = reader["CreatedByUser"].ToString();
                this.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                this.isPublic = Convert.ToBoolean(reader["IsPublic"]);
                if (!string.IsNullOrEmpty(reader["Priority"].ToString()))
                {
                    this.priority = Convert.ToInt32(reader["Priority"]);
                }
                if (!string.IsNullOrEmpty(reader["NoClick"].ToString()))
                {
                    this.noClick = bool.Parse(reader["NoClick"].ToString());
                }
                //if (!string.IsNullOrEmpty(reader["IsSlideTop"].ToString()))
                //{
                //    this.isSlideTop = Convert.ToBoolean(reader["IsSlideTop"]);
                //}
                //if (!string.IsNullOrEmpty(reader["IsSlideBottom"].ToString()))
                //{
                //    this.isSlideBottom = Convert.ToBoolean(reader["IsSlideBottom"]);
                //}
            }

        }

        /// <summary>
        /// Persists a new instance of Banner. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBBannerBuilder.Create(
                this.moduleID,
                this.siteID,
                this.pageID,
                this.name,
                this.description,
                this.link,
                this.path,
                this.hitCount,
                this.width,
                this.isImage,
                this.number,
                this.isHorizontal,
                this.startDate,
                this.endDate,
                this.isFollow,
                this.isTarget,
                this.createdByUser,
                this.createdDate,
                this.isPublic,
                this.priority,
                this.noClick
                );

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of Banner. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBBannerBuilder.Update(
                this.itemID,
                this.moduleID,
                this.siteID,
                this.pageID,
                this.name,
                this.description,
                this.link,
                this.path,
                this.hitCount,
                this.width,
                this.isImage,
                this.number,
                this.isHorizontal,
                this.startDate,
                this.endDate,
                this.isFollow,
                this.isTarget,
                this.createdByUser,
                this.createdDate,
                this.isPublic,
                this.priority,
                this.noClick);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of Banner. Returns true on success.
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
        /// Deletes an instance of Banner. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBBannerBuilder.Delete(
                itemID);
        }


        private static List<BannerBuilder> LoadListFromReader(IDataReader reader)
        {
            List<BannerBuilder> BannerList = new List<BannerBuilder>();
            try
            {
                while (reader.Read())
                {
                    BannerBuilder Banner = new BannerBuilder();
                    Banner.itemID = Convert.ToInt32(reader["ItemID"]);
                    Banner.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    Banner.siteID = Convert.ToInt32(reader["SiteID"]);
                    Banner.pageID = Convert.ToInt32(reader["PageID"]);
                    Banner.name = reader["Name"].ToString();
                    Banner.description = reader["Description"].ToString();
                    Banner.link = reader["Link"].ToString();
                    Banner.path = reader["Path"].ToString();
                    Banner.hitCount = Convert.ToInt32(reader["HitCount"]);
                    Banner.width = reader["Width"].ToString();
                    Banner.isImage = Convert.ToBoolean(reader["IsImage"]);
                    Banner.number = Convert.ToInt32(reader["Number"]);
                    Banner.isHorizontal = Convert.ToBoolean(reader["IsHorizontal"]);
                    Banner.startDate = Convert.ToDateTime(reader["StartDate"]);
                    if (!string.IsNullOrEmpty(reader["EndDate"].ToString()))
                    {
                        Banner.endDate = Convert.ToDateTime(reader["EndDate"]);
                    }
                    Banner.isFollow = Convert.ToBoolean(reader["IsFollow"]);
                    Banner.isTarget = Convert.ToBoolean(reader["IsTarget"]);
                    Banner.createdByUser = reader["CreatedByUser"].ToString();
                    Banner.createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    Banner.isPublic = Convert.ToBoolean(reader["IsPublic"]);
                    if (!string.IsNullOrEmpty(reader["NoClick"].ToString()))
                    {
                        Banner.noClick = Convert.ToBoolean(reader["NoClick"].ToString());
                    }
                    //if (!string.IsNullOrEmpty(reader["IsSlideTop"].ToString()))
                    //{
                    //    Banner.isSlideTop = Convert.ToBoolean(reader["IsSlideTop"]);
                    //}
                    //if (!string.IsNullOrEmpty(reader["IsSlideBottom"].ToString()))
                    //{
                    //    Banner.isSlideBottom = Convert.ToBoolean(reader["IsSlideBottom"]);
                    //}
                    BannerList.Add(Banner);

                }
            }
            finally
            {
                reader.Close();
            }

            return BannerList;

        }

        /// <summary>
        /// Gets an IList with all instances of Banner.
        /// </summary>
        public static List<BannerBuilder> GetAll()
        {
            IDataReader reader = DBBannerBuilder.GetAll();
            return LoadListFromReader(reader);

        }


        public static List<BannerBuilder> GetByModule(int moduleId)
        {
            IDataReader reader = DBBannerBuilder.GetByModule(moduleId);
            return LoadListFromReader(reader);
        }




        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of Banner.
        /// </summary>
        public static int CompareByItemID(BannerBuilder Banner1, BannerBuilder Banner2)
        {
            return Banner1.ItemID.CompareTo(Banner2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of Banner.
        /// </summary>
        public static int CompareByModuleID(BannerBuilder Banner1, BannerBuilder Banner2)
        {
            return Banner1.ModuleID.CompareTo(Banner2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of Banner.
        /// </summary>
        public static int CompareBySiteID(BannerBuilder Banner1, BannerBuilder Banner2)
        {
            return Banner1.SiteID.CompareTo(Banner2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of Banner.
        /// </summary>
        public static int CompareByPageID(BannerBuilder Banner1, BannerBuilder Banner2)
        {
            return Banner1.PageID.CompareTo(Banner2.PageID);
        }
        /// <summary>
        /// Compares 2 instances of Banner.
        /// </summary>
        public static int CompareByName(BannerBuilder Banner1, BannerBuilder Banner2)
        {
            return Banner1.Name.CompareTo(Banner2.Name);
        }
        /// <summary>
        /// Compares 2 instances of Banner.
        /// </summary>
        public static int CompareByDescription(BannerBuilder Banner1, BannerBuilder Banner2)
        {
            return Banner1.Description.CompareTo(Banner2.Description);
        }
        /// <summary>
        /// Compares 2 instances of Banner.
        /// </summary>
        public static int CompareByLink(BannerBuilder Banner1, BannerBuilder Banner2)
        {
            return Banner1.Link.CompareTo(Banner2.Link);
        }
        /// <summary>
        /// Compares 2 instances of Banner.
        /// </summary>
        public static int CompareByPath(BannerBuilder Banner1, BannerBuilder Banner2)
        {
            return Banner1.Path.CompareTo(Banner2.Path);
        }
        /// <summary>
        /// Compares 2 instances of Banner.
        /// </summary>
        public static int CompareByHitCount(BannerBuilder Banner1, BannerBuilder Banner2)
        {
            return Banner1.HitCount.CompareTo(Banner2.HitCount);
        }
        /// <summary>
        /// Compares 2 instances of Banner.
        /// </summary>
        public static int CompareByWidth(BannerBuilder Banner1, BannerBuilder Banner2)
        {
            return Banner1.Width.CompareTo(Banner2.Width);
        }
        /// <summary>
        /// Compares 2 instances of Banner.
        /// </summary>
        public static int CompareByNumber(BannerBuilder Banner1, BannerBuilder Banner2)
        {
            return Banner1.Number.CompareTo(Banner2.Number);
        }
        /// <summary>
        /// Compares 2 instances of Banner.
        /// </summary>
        public static int CompareByStartDate(BannerBuilder Banner1, BannerBuilder Banner2)
        {
            return Banner1.StartDate.CompareTo(Banner2.StartDate);
        }
        /// <summary>
        /// Compares 2 instances of Banner.
        /// </summary>
        //public static int CompareByEndDate(Banner Banner1, Banner Banner2)
        //{
        //    return Banner1.EndDate.CompareTo(Banner2.EndDate);
        //}
        /// <summary>
        /// Compares 2 instances of Banner.
        /// </summary>
        public static int CompareByCreatedByUser(BannerBuilder Banner1, BannerBuilder Banner2)
        {
            return Banner1.CreatedByUser.CompareTo(Banner2.CreatedByUser);
        }
        /// <summary>
        /// Compares 2 instances of Banner.
        /// </summary>
        public static int CompareByCreatedDate(BannerBuilder Banner1, BannerBuilder Banner2)
        {
            return Banner1.CreatedDate.CompareTo(Banner2.CreatedDate);
        }

        #endregion


    }

}


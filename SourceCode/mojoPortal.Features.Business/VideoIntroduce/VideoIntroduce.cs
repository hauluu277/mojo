using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using VideoIntroduceFeatures.Data;

namespace VideoIntroduceFeatures.Business
{
    public class VideoIntroduce
    {
        #region Constructors

        public VideoIntroduce()
        { }


        public VideoIntroduce(
            int itemID)
        {
            GetVideoIntroduce(
                itemID);
        }

        #endregion

        #region Private Properties

        private int itemID = -1;
        private int siteID = -1;
        private int pageID = -1;
        private int moduleID = -1;
        private string title = string.Empty;
        private string itemUrl = string.Empty;
        private string name = string.Empty;
        private string video = string.Empty;
        private string youtubeUrl = string.Empty;
        private int views = 0;
        private string imageVideo = string.Empty;
        private string contentDetail = string.Empty;
        private string typeVideo = string.Empty;
        private int typePlayer = 0;
        private double sizeVideo;
        private bool isPublic = false;
        private int createBy = -1;
        private DateTime createDate = DateTime.Now;
        private bool isHot = false;

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
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string ItemUrl
        {
            get { return itemUrl; }
            set { itemUrl = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Video
        {
            get { return video; }
            set { video = value; }
        }
        public string YoutubeUrl
        {
            get { return youtubeUrl; }
            set { youtubeUrl = value; }
        }
        public int Views
        {
            get { return views; }
            set { views = value; }
        }
        public string ImageVideo
        {
            get { return imageVideo; }
            set { imageVideo = value; }
        }
        public string ContentDetail
        {
            get { return contentDetail; }
            set { contentDetail = value; }
        }
        public string TypeVideo
        {
            get { return typeVideo; }
            set { typeVideo = value; }
        }
        public int TypePlayer
        {
            get { return typePlayer; }
            set { typePlayer = value; }
        }
        public double SizeVideo
        {
            get { return sizeVideo; }
            set { sizeVideo = value; }
        }
        public bool IsPublic
        {
            get { return isPublic; }
            set { isPublic = value; }
        }
        public int CreateBy
        {
            get { return createBy; }
            set { createBy = value; }
        }
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
        public bool IsHot
        {
            get { return isHot; }
            set { isHot = value; }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Gets an instance of VideoIntroduce.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        private void GetVideoIntroduce(
            int itemID)
        {
            using (IDataReader reader = DBVideoIntroduce.GetOne(
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
                this.title = reader["Title"].ToString();
                this.itemUrl = reader["ItemUrl"].ToString();
                this.name = reader["Name"].ToString();
                this.video = reader["Video"].ToString();
                if (reader["YoutubeUrl"] != DBNull.Value)
                {
                    this.youtubeUrl = reader["YoutubeUrl"].ToString();
                }
                this.views = int.Parse(reader["Views"].ToString());
                if (reader["ImageVideo"] != DBNull.Value)
                {
                    this.imageVideo = reader["ImageVideo"].ToString();
                }
                else
                {
                    this.imageVideo = ConfigurationManager.AppSettings["ImageVideoIntroduceDefault"];
                }
                this.contentDetail = reader["ContentDetail"].ToString();
                this.typeVideo = reader["TypeVideo"].ToString();
                if (reader["TypePlayer"] != DBNull.Value)
                {
                    this.typePlayer = int.Parse(reader["TypePlayer"].ToString());
                }
                this.sizeVideo = Convert.ToDouble(reader["SizeVideo"]);
                this.isPublic = Convert.ToBoolean(reader["IsPublic"]);
                this.createBy = Convert.ToInt32(reader["CreateBy"]);
                this.createDate = Convert.ToDateTime(reader["CreateDate"]);
                if (reader["IsHot"] != DBNull.Value)
                {
                    this.isHot = bool.Parse(reader["IsHot"].ToString());
                }
            }

        }

        /// <summary>
        /// Persists a new instance of VideoIntroduce. Returns true on success.
        /// </summary>
        /// <returns></returns>
        private bool Create()
        {
            int newID = 0;

            newID = DBVideoIntroduce.Create(
                this.siteID,
                this.pageID,
                this.moduleID,
                this.title,
                this.itemUrl,
                this.name,
                this.video,
                this.youtubeUrl,
                this.views,
                this.imageVideo,
                this.contentDetail,
                this.typeVideo,
                this.typePlayer,
                this.sizeVideo,
                this.isPublic,
                this.createBy,
                this.createDate,
                this.isHot);

            this.itemID = newID;

            return (newID > 0);

        }


        /// <summary>
        /// Updates this instance of VideoIntroduce. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBVideoIntroduce.Update(
                this.itemID,
                this.siteID,
                this.pageID,
                this.moduleID,
                this.title,
                this.itemUrl,
                this.name,
                this.video,
                this.youtubeUrl,
                this.views,
                this.imageVideo,
                this.contentDetail,
                this.typeVideo,
                this.typePlayer,
                this.sizeVideo,
                this.isPublic,
                this.createBy,
                this.createDate,
                this.isHot);

        }





        #endregion

        #region Public Methods

        /// <summary>
        /// Saves this instance of VideoIntroduce. Returns true on success.
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
        /// Deletes an instance of VideoIntroduce. Returns true on success.
        /// </summary>
        /// <param name="itemID"> itemID </param>
        /// <returns>bool</returns>
        public static bool Delete(
            int itemID)
        {
            return DBVideoIntroduce.Delete(
                itemID);
        }


        public static bool UpdateIsHot(int siteID)
        {
            return DBVideoIntroduce.UpdateIsHot(siteID);
        }

        /// <summary>
        /// Gets a count of VideoIntroduce. 
        /// </summary>
        public static int GetCount(int siteID, int isPublic, int typePlayer, string keyword)
        {
            return DBVideoIntroduce.GetCount(siteID, isPublic, typePlayer, keyword);
        }

        private static List<VideoIntroduce> LoadListFromReader(IDataReader reader)
        {
            List<VideoIntroduce> VideoIntroduceList = new List<VideoIntroduce>();
            try
            {
                while (reader.Read())
                {
                    VideoIntroduce VideoIntroduce = new VideoIntroduce();
                    VideoIntroduce.itemID = Convert.ToInt32(reader["ItemID"]);
                    VideoIntroduce.siteID = Convert.ToInt32(reader["SiteID"]);
                    VideoIntroduce.pageID = Convert.ToInt32(reader["PageID"]);
                    VideoIntroduce.moduleID = Convert.ToInt32(reader["ModuleID"]);
                    VideoIntroduce.title = reader["Title"].ToString();
                    VideoIntroduce.itemUrl = reader["ItemUrl"].ToString();
                    VideoIntroduce.name = reader["Name"].ToString();
                    VideoIntroduce.video = reader["Video"].ToString();
                    if (reader["YoutubeUrl"] != DBNull.Value)
                    {
                        VideoIntroduce.youtubeUrl = reader["YoutubeUrl"].ToString();
                    }
                    VideoIntroduce.views = Convert.ToInt32(reader["Views"]);
                    if (!string.IsNullOrEmpty(reader["ImageVideo"].ToString()))
                    {
                        VideoIntroduce.imageVideo = reader["ImageVideo"].ToString();
                    }
                    else
                    {
                        VideoIntroduce.imageVideo = ConfigurationManager.AppSettings["ImageVideoIntroduceDefault"];
                    }
                    VideoIntroduce.contentDetail = reader["ContentDetail"].ToString();
                    VideoIntroduce.typeVideo = reader["TypeVideo"].ToString();
                    if (reader["TypePlayer"] != DBNull.Value)
                    {
                        VideoIntroduce.typePlayer = int.Parse(reader["TypePlayer"].ToString());
                    }
                    VideoIntroduce.sizeVideo = Convert.ToDouble(reader["SizeVideo"]);
                    VideoIntroduce.isPublic = Convert.ToBoolean(reader["IsPublic"]);
                    VideoIntroduce.createBy = Convert.ToInt32(reader["CreateBy"]);
                    VideoIntroduce.createDate = Convert.ToDateTime(reader["CreateDate"]);
                    if (reader["IsHot"] != DBNull.Value)
                    {
                        VideoIntroduce.isHot = bool.Parse(reader["IsHot"].ToString());
                    }
                    VideoIntroduceList.Add(VideoIntroduce);

                }
            }
            finally
            {
                reader.Close();
            }

            return VideoIntroduceList;

        }

        /// <summary>
        /// Gets an IList with all instances of VideoIntroduce.
        /// </summary>
        public static List<VideoIntroduce> GetAll()
        {
            IDataReader reader = DBVideoIntroduce.GetAll();
            return LoadListFromReader(reader);

        }

        public static List<VideoIntroduce> GetAllPublic(int siteID)
        {
            IDataReader reader = DBVideoIntroduce.GetAllPublic(siteID);
            return LoadListFromReader(reader);
        }

        public static List<VideoIntroduce> GetBySite(int siteID)
        {
            IDataReader reader = DBVideoIntroduce.GetBySite(siteID);
            return LoadListFromReader(reader);
        }


        public static List<VideoIntroduce> GetTopOrtherVideo(int siteID, int top)
        {
            IDataReader reader = DBVideoIntroduce.GetTopOrtherVideo(siteID, top);
            return LoadListFromReader(reader);

        }
        public static List<VideoIntroduce> GetTopOrtherVideo(int siteID, int top, int itemID)
        {
            IDataReader reader = DBVideoIntroduce.GetTopOrtherVideo(siteID, top, itemID);
            return LoadListFromReader(reader);

        }
        public static List<VideoIntroduce> GetAllVideoPublic(int siteID)
        {
            IDataReader reader = DBVideoIntroduce.GetAllVideoPublic(siteID);
            return LoadListFromReader(reader);

        }



        public static VideoIntroduce GetVideoIsHot(int siteID)
        {
            IDataReader reader = DBVideoIntroduce.GetIsHot(siteID);
            return PopulateFromReaderIsHot(reader);
        }

        public static VideoIntroduce PopulateFromReaderIsHot(IDataReader reader)
        {
            VideoIntroduce video = new VideoIntroduce();
            if (reader.Read())
            {
                video.itemID = Convert.ToInt32(reader["ItemID"]);
                video.siteID = Convert.ToInt32(reader["SiteID"]);
                video.pageID = Convert.ToInt32(reader["PageID"]);
                video.moduleID = Convert.ToInt32(reader["ModuleID"]);
                video.title = reader["Title"].ToString();
                video.itemUrl = reader["ItemUrl"].ToString();
                video.name = reader["Name"].ToString();
                video.video = reader["Video"].ToString();
                if (reader["YoutubeUrl"] != DBNull.Value)
                {
                    video.youtubeUrl = reader["YoutubeUrl"].ToString();
                }
                video.views = int.Parse(reader["Views"].ToString());
                if (reader["ImageVideo"] != DBNull.Value)
                {
                    video.imageVideo = reader["ImageVideo"].ToString();
                }
                else
                {
                    video.imageVideo = ConfigurationManager.AppSettings["ImageVideoIntroduceDefault"];
                }
                video.contentDetail = reader["ContentDetail"].ToString();
                video.typeVideo = reader["TypeVideo"].ToString();
                if (reader["TypePlayer"] != DBNull.Value)
                {
                    video.typePlayer = int.Parse(reader["TypePlayer"].ToString());
                }
                video.sizeVideo = Convert.ToDouble(reader["SizeVideo"]);
                video.isPublic = Convert.ToBoolean(reader["IsPublic"]);
                video.createBy = Convert.ToInt32(reader["CreateBy"]);
                video.createDate = Convert.ToDateTime(reader["CreateDate"]);
                if (reader["IsHot"] != DBNull.Value)
                {
                    video.isHot = bool.Parse(reader["IsHot"].ToString());
                }
            }
            return video;
        }

        /// <summary>
        /// Gets an IList with page of instances of VideoIntroduce.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalPages">total pages</param>
        public static List<VideoIntroduce> GetPage(int siteID, int isPublic, int typePlayer, string keyword, int pageNumber, int pageSize, out int totalPages)
        {
            totalPages = 1;
            IDataReader reader = DBVideoIntroduce.GetPage(siteID, isPublic, typePlayer, keyword, pageNumber, pageSize, out totalPages);
            return LoadListFromReader(reader);
        }



        #endregion

        #region Comparison Methods

        /// <summary>
        /// Compares 2 instances of VideoIntroduce.
        /// </summary>
        public static int CompareByItemID(VideoIntroduce VideoIntroduce1, VideoIntroduce VideoIntroduce2)
        {
            return VideoIntroduce1.ItemID.CompareTo(VideoIntroduce2.ItemID);
        }
        /// <summary>
        /// Compares 2 instances of VideoIntroduce.
        /// </summary>
        public static int CompareBySiteID(VideoIntroduce VideoIntroduce1, VideoIntroduce VideoIntroduce2)
        {
            return VideoIntroduce1.SiteID.CompareTo(VideoIntroduce2.SiteID);
        }
        /// <summary>
        /// Compares 2 instances of VideoIntroduce.
        /// </summary>
        public static int CompareByPageID(VideoIntroduce VideoIntroduce1, VideoIntroduce VideoIntroduce2)
        {
            return VideoIntroduce1.PageID.CompareTo(VideoIntroduce2.PageID);
        }
        /// <summary>
        /// Compares 2 instances of VideoIntroduce.
        /// </summary>
        public static int CompareByModuleID(VideoIntroduce VideoIntroduce1, VideoIntroduce VideoIntroduce2)
        {
            return VideoIntroduce1.ModuleID.CompareTo(VideoIntroduce2.ModuleID);
        }
        /// <summary>
        /// Compares 2 instances of VideoIntroduce.
        /// </summary>
        public static int CompareByTitle(VideoIntroduce VideoIntroduce1, VideoIntroduce VideoIntroduce2)
        {
            return VideoIntroduce1.Title.CompareTo(VideoIntroduce2.Title);
        }
        /// <summary>
        /// Compares 2 instances of VideoIntroduce.
        /// </summary>
        public static int CompareByName(VideoIntroduce VideoIntroduce1, VideoIntroduce VideoIntroduce2)
        {
            return VideoIntroduce1.Name.CompareTo(VideoIntroduce2.Name);
        }
        /// <summary>
        /// Compares 2 instances of VideoIntroduce.
        /// </summary>
        /// <summary>
        /// Compares 2 instances of VideoIntroduce.
        /// </summary>
        public static int CompareByImageVideo(VideoIntroduce VideoIntroduce1, VideoIntroduce VideoIntroduce2)
        {
            return VideoIntroduce1.ImageVideo.CompareTo(VideoIntroduce2.ImageVideo);
        }
        /// <summary>
        /// Compares 2 instances of VideoIntroduce.
        /// </summary>
        public static int CompareByContentDetail(VideoIntroduce VideoIntroduce1, VideoIntroduce VideoIntroduce2)
        {
            return VideoIntroduce1.ContentDetail.CompareTo(VideoIntroduce2.ContentDetail);
        }
        /// <summary>
        /// Compares 2 instances of VideoIntroduce.
        /// </summary>
        public static int CompareByTypeVideo(VideoIntroduce VideoIntroduce1, VideoIntroduce VideoIntroduce2)
        {
            return VideoIntroduce1.TypeVideo.CompareTo(VideoIntroduce2.TypeVideo);
        }
        /// <summary>
        /// Compares 2 instances of VideoIntroduce.
        /// </summary>
        public static int CompareByCreateBy(VideoIntroduce VideoIntroduce1, VideoIntroduce VideoIntroduce2)
        {
            return VideoIntroduce1.CreateBy.CompareTo(VideoIntroduce2.CreateBy);
        }
        /// <summary>
        /// Compares 2 instances of VideoIntroduce.
        /// </summary>
        public static int CompareByCreateDate(VideoIntroduce VideoIntroduce1, VideoIntroduce VideoIntroduce2)
        {
            return VideoIntroduce1.CreateDate.CompareTo(VideoIntroduce2.CreateDate);
        }

        #endregion
    }
}

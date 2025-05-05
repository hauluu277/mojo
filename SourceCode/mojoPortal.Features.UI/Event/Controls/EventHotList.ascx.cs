using EventFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Linq;

namespace EventFeature.UI
{
    public partial class EventHotList : UserControl
    {
        protected EventConfiguration config = new EventConfiguration();

        private int pageId = -1;
        private int moduleId = -1;
        private int number = -1;
        private TimeZoneInfo timeZone;
        protected Double timeOffset;
        private string siteRoot = string.Empty;
        public SiteSettings siteSettings;
        protected bool visibleImg = false;
        public int PageId
        {
            get { return pageId; }
            set { pageId = value; }
        }

        public int ModuleId
        {
            get { return moduleId; }
            set { moduleId = value; }
        }
        public string SiteRoot
        {
            get { return siteRoot; }
            set { siteRoot = value; }
        }
        #region OnInit

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(Page_Load);

        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSettings();
            PopulateLabels();
            if (!IsPostBack)
            {
                PopulateControls();
            }
        }
        private void PopulateControls()
        {
            PopulateEvent();
        }
        private void PopulateLabels()
        {
            //if (string.IsNullOrEmpty(config.Title))
            //{
            //    lblTit.Text = Resources.EventResources.EventHighlightsTitle;
            //}
            //else
            //{FormatDate
            //    lblTit.Text = config.Title;
            //}
            hplEvent.Text = Resources.EventResources.Event;
            hplEvent.ToolTip = Resources.EventResources.Event;
            hplEvent.NavigateUrl = SiteRoot + config.EventUrl;
        }
        private void PopulateEvent()
        {
            List<Event> eventHots = Event.GetTopHot(2, siteSettings.SiteId);
            rptEventHot.DataSource = eventHots;
            rptEventHot.DataBind();

            var skips = string.Empty;
            if (eventHots != null && eventHots.Count > 0)
            {
                skips = string.Join(",", eventHots.Select(x => x.ItemID).ToArray());
            }

            List<Event> events = Event.GetTopOrther(3, siteSettings.SiteId, skips);
            rptEvent.DataSource = events;
            rptEvent.DataBind();

        }
        public bool CheckSummary(string data = "")
        {
            bool result = false;
            if (!string.IsNullOrEmpty(data))
            {
                result = true;
            }
            return result;
        }
        protected bool ShowImage(string imageUrl)
        {
            if (String.IsNullOrEmpty(imageUrl))
            {
                return false;
            }
            else
            {
                string filePath = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["EventImagesFolder"] + imageUrl;
                filePath = filePath.Replace("/", "\\");
                if (File.Exists(filePath))
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
        }
        public bool ShowImg(string img)
        {
            if (!string.IsNullOrEmpty(img))
            {
                try
                {
                    string thumbnailImageURL = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["EventImagesFolder"] + img;
                    //thumbnailImageURL = thumbnailImageURL.Replace("/", "\\");
                    if (File.Exists(thumbnailImageURL))
                    {
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
        protected string formatContent(string Content)
        {
            if (!string.IsNullOrEmpty(Content))
            {
                return Regex.Replace(Content, "<(.|\n)*?>", string.Empty);
            }
            else
            {
                return string.Empty;
            }
        }
        public string FormatDate(object date)
        {
            if (date != null && !string.IsNullOrEmpty(date.ToString()))
            {
                try
                {
                    var dateModified = DateTime.Parse(date.ToString());
                    var timeFormat = "";
                    var result = timeZone != null ? TimeZoneInfo.ConvertTimeFromUtc(dateModified, timeZone) : dateModified.AddHours(timeOffset);
                    if (result != null)
                    {
                        timeFormat = string.Format("{0:dd/MM/yyyy}", result);
                    }
                    return timeFormat;
                }
                catch { }
            }
            return "";
        }
        protected string FormatBlogTitleUrl(string siteRoot, string itemUrl, int itemId, int pageId, int moduleId)
        {
            return FormatBlogTitleUrl(siteRoot, itemUrl, itemId, pageId, moduleId, false, string.Empty);
        }
        protected string FormatBlogTitleUrl(string siteRoot, string itemUrl, int itemId, int pageId, int moduleId, bool useOverrideUrl, string overrideUrl)
        {
            string url = "";
            if (useOverrideUrl && overrideUrl.Length > 0)
            {
                url = overrideUrl;
                return url;
            }
            //if (itemId > 0)
            //{
            //    int count = Article.GetCountByEvent(itemId);
            //    if (count == 1)
            //    {
            //        Article article = new Article(itemId, true);
            //        if (article != null)
            //        {
            //            if (article.ItemUrl.Length > 0)
            //            {
            //                return siteRoot + article.ItemUrl.Replace("~", string.Empty);
            //            }
            //            else
            //            {

            //                url = siteSettings.SiteRoot + "/Article/ViewPost.aspx?pageid=" + PageId.ToInvariantString()
            //                                  + "&ItemID=" + article.ItemID.ToInvariantString()
            //                                  + "&mid=" + ModuleId.ToInvariantString();
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (itemUrl.Length > 0)
            //        {
            //            url = siteRoot + itemUrl.Replace("~", string.Empty);
            //        }
            //        else
            //        {

            //            url = siteRoot + "/Event/ViewPost.aspx?pageid=" + pageId.ToInvariantString()
            //                + "&ItemID=" + itemId.ToInvariantString()
            //                + "&mid=" + moduleId.ToInvariantString();
            //        }
            //    }
            //}
            if (itemUrl.Length > 0)
                return siteRoot + itemUrl.Replace("~", string.Empty);
            return siteRoot + "/Event/ViewPost.aspx?pageid=" + pageId.ToInvariantString()
                + "&ItemID=" + itemId.ToInvariantString()
                + "&mid=" + moduleId.ToInvariantString();


        }
        private void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new EventConfiguration(getModuleSettings);
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            timeZone = SiteUtils.GetUserTimeZone();
            timeOffset = SiteUtils.GetUserTimeOffset();
            //visibleImg = config.ShowImageRight;
        }

        public string FormatDateTime(object startDate, object endDate)
        {
            var result = string.Empty;
            if (endDate != null)
            {
                return string.Format("{0} - {1} | {2}",
                    string.Format("{0:hh:mm}", startDate),
                    string.Format("{0:hh:mm}", endDate),
                    string.Format("{0:dd/MM/yyyy}", startDate));
            }
            return string.Format("{0:hh:mm dd/MM/yyyy}", startDate);
        }
    }
}
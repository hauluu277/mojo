using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
//using DocumentFeature.UI;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace DocumentFeature.UI
{
    public partial class DocumentHotList : UserControl
    {
        protected DocumentConfiguration config = new DocumentConfiguration();

        private int pageId = -1;
        private int moduleId = -1;
        private TimeZoneInfo timeZone;
        protected Double timeOffset;
        private string siteRoot = string.Empty;
        public SiteSettings siteSettings;
        protected bool visibleImg = false;
        private int number = 2;
        public int Number
        {
            get { return number; }
            set { number = value; }
        }
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
            PopulateTopSilde();
        }
        private void PopulateLabels()
        {
            if (string.IsNullOrEmpty(config.TitleHotNew))
            {
                lblTit.Text = "Văn bản mới";
            }
            else
            {
                lblTit.Text = config.TitleHotNew;
            }
        }
        private void PopulateTopSilde()
        {
            List<Documentation> slide = Documentation.GetHotNew(siteSettings.SiteId, number);
            rptArticle.DataSource = slide;
            rptArticle.DataBind();
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
        public string FormatDate(string date)
        {
            if (!String.IsNullOrEmpty(date))
            {
                try
                {
                    var dateModified = DateTime.Parse(date);
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
            if (itemUrl.Length > 0)
                return siteRoot + itemUrl.Replace("~", string.Empty);
            return siteRoot + "/Event/ViewPost.aspx?pageid=" + pageId.ToInvariantString()
                + "&ItemID=" + itemId.ToInvariantString()
                + "&mid=" + moduleId.ToInvariantString();


        }
        private void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new DocumentConfiguration(getModuleSettings);
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            try
            {
                number = int.Parse(config.SoLuongHienThi);
            }
            catch
            {

                number = 2;
            }
            timeZone = SiteUtils.GetUserTimeZone();
            timeOffset = SiteUtils.GetUserTimeOffset();
            //visibleImg = config.ShowImageRight;
        }
    }
}
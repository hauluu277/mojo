// Author:				        NamDV
// Created:			            2015-09-22
// 

using System;
using System.Globalization;
using System.Web.UI;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.Framework;
using mojoPortal.Web;
using Resources;
using ArticleFeature.Business;
using mojoPortal.Features;


namespace ArticleFeature.UI
{
    public partial class FeedLinks : UserControl
    {
        private int pageId = -1;
        private int moduleId = -1;
        private string siteRoot = string.Empty;
        private ArticleConfiguration config = new ArticleConfiguration();
        private string imageSiteRoot = string.Empty;
        private SiteSettings siteSettings = null;
        protected string RssImageFile = WebConfigSettings.RSSImageFileName;


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

        public string ImageSiteRoot
        {
            get { return imageSiteRoot; }
            set { imageSiteRoot = value; }
        }

        public ArticleConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }



        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected override void OnPreRender(EventArgs e)
        {
            if (this.Visible)
            {
                if (pageId == -1) { return; }
                if (moduleId == -1) { return; }

                LoadSettings();
                PopulateLabels();
                SetupLinks();
            }


            base.OnPreRender(e);

        }

        private void SetupLinks()
        {
            if (siteSettings == null) { return; }

            lnkRSS.HRef = GetRssUrl();
            imgRSS.Src = ImageSiteRoot + "/Data/SiteImages/" + RssImageFile;

            lnkAddMSN.HRef = "http://my.msn.com/addtomymsn.armx?id=rss&amp;ut=" + GetRssUrl();

            imgMSNRSS.Src = ImageSiteRoot + "/Data/SiteImages/rss_mymsn.gif";

            lnkAddToLive.HRef = "http://www.live.com/?add=" + Server.UrlEncode(GetRssUrl());

            imgAddToLive.Src = ImageSiteRoot + "/Data/SiteImages/addtolive.gif";

            lnkAddYahoo.HRef = "http://e.my.yahoo.com/config/promo_content?.module=ycontent&amp;.url="
                + GetRssUrl();

            imgYahooRSS.Src = ImageSiteRoot + "/Data/SiteImages/addtomyyahoo2.gif";

            lnkAddGoogle.HRef = "http://fusion.google.com/add?feedurl="
                + GetRssUrl();

            imgGoogleRSS.Src = ImageSiteRoot + "/Data/SiteImages/googleaddrss.gif";

            liOdiogoPodcast.Visible = (config.OdiogoPodcastUrl.Length > 0);
            lnkOdiogoPodcast.HRef = config.OdiogoPodcastUrl;
            lnkOdiogoPodcastTextLink.NavigateUrl = config.OdiogoPodcastUrl;
            imgOdiogoPodcast.Src = ImageSiteRoot + "/Data/SiteImages/podcast.png";




        }

        private string GetRssUrl()
        {
            if (config.FeedburnerFeedUrl.Length > 0) return config.FeedburnerFeedUrl;

            return SiteRoot + "/article" + ModuleId.ToString(CultureInfo.InvariantCulture) + "rss.aspx";

        }

        private void PopulateLabels()
        {
            lnkRSS.Title = ArticleResources.BlogRSSLinkTitle;
            lnkAddMSN.Title = ArticleResources.BlogModuleAddToMyMSNLink;
            lnkAddYahoo.Title = ArticleResources.BlogModuleAddToMyYahooLink;
            lnkAddGoogle.Title = ArticleResources.BlogModuleAddToGoogleLink;
            lnkAddToLive.Title = ArticleResources.BlogModuleAddToWindowsLiveLink;
            lnkOdiogoPodcast.Title = ArticleResources.PodcastLink;
            lnkOdiogoPodcastTextLink.Text = ArticleResources.PodcastLink;

        }

        private void LoadSettings()
        {
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            if (siteSettings == null) { return; }

            siteRoot = siteSettings.SiteRoot;

            liAddGoogle.Visible = config.ShowAddFeedLinks;
            liAddMSN.Visible = config.ShowAddFeedLinks;
            liAddYahoo.Visible = config.ShowAddFeedLinks;
            liAddToLive.Visible = config.ShowAddFeedLinks;

            if (imageSiteRoot.Length == 0) { imageSiteRoot = WebUtils.GetSiteRoot(); }
        }


    }
}
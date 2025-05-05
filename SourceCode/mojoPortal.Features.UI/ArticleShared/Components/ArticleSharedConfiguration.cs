using mojoPortal.Web.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace mojoPortal.Features
{
    /// <summary>
    /// encapsulates the feature instance configuration loaded from module settings into a more friendly object
    /// </summary>
    public class ArticleSharedConfiguration
    {
        public ArticleSharedConfiguration()
        { }

        public ArticleSharedConfiguration(Hashtable settings)
        {
            LoadSettings(settings);

        }
        private void LoadSettings(Hashtable settings)
        {
            if (settings == null) { throw new ArgumentException("must pass in a hashtable of settings"); }
            if (settings.Contains("SiteCategorySetting"))
            {
                siteCategorySetting = settings["SiteCategorySetting"].ToString();
            }
            pageSize = WebUtils.ParseInt32FromHashtable(settings, "BlogEntriesToShowSetting", pageSize);
            showAuthorSignature = WebUtils.ParseBoolFromHashtable(settings, "ShowAuthorSignatureSetting", showAuthorSignature);
            maxNumberOfCharactersInTitleSetting = WebUtils.ParseInt32FromHashtable(settings, "MaxNumberOfCharactersInTitleSetting", maxNumberOfCharactersInTitleSetting);
            showImage = WebUtils.ParseBoolFromHashtable(settings, "ImageShowSetting", showImage);
            showSiteNameSetting = WebUtils.ParseBoolFromHashtable(settings, "ShowSiteNameSetting", showSiteNameSetting);
            topSelectSetting = WebUtils.ParseInt32FromHashtable(settings, "TopSelectSetting", topSelectSetting);
            if (settings.Contains("CustomCssClassSetting"))
            {
                customCssClassSetting = settings["CustomCssClassSetting"].ToString();
            }
            if (settings.Contains("TitleOrtherSetting"))
            {
                titleOrtherSetting = settings["TitleOrtherSetting"].ToString();
            }
            if (settings.Contains("UrlTitleSetting"))
            {
                urlTitleSetting = settings["UrlTitleSetting"].ToString();
            }
            if (settings.Contains("PageViewSetting"))
            {
                pageViewSetting = settings["PageViewSetting"].ToString();
            }
        }

        private string pageViewSetting = string.Empty;
        public string PageViewSetting
        {
            get { return pageViewSetting; }
        }



        private string customCssClassSetting = string.Empty;
        public string CustomCssClassSetting
        {
            get { return customCssClassSetting; }
        }

        private int topSelectSetting = 5;
        public int TopSelectSetting
        {
            get { return topSelectSetting; }
        }

        private string titleOrtherSetting;
        public string TitleOrtherSetting
        {
            get { return titleOrtherSetting; }
        }
        private string urlTitleSetting;
        public string UrlTitleSetting
        {
            get { return urlTitleSetting; }
        }
        private bool showSiteNameSetting = false;
        public bool ShowSiteNameSetting
        {
            get { return showSiteNameSetting; }
        }

        private bool showImage = true;

        public bool ShowImage
        {
            get { return showImage; }
        }


        private int maxNumberOfCharactersInTitleSetting = 100;

        public int MaxNumberOfCharactersInTitleSetting
        {
            get { return maxNumberOfCharactersInTitleSetting; }
        }

        private bool showAuthorSignature = false;
        public bool ShowAuthorSignature
        {
            get { return showAuthorSignature; }
        }
        private string siteCategorySetting = string.Empty;

        public string SiteCategorySetting
        {
            get { return siteCategorySetting; }
        }

        private int pageSize = 15;

        public int PageSize
        {
            get { return pageSize; }
        }
    }
}
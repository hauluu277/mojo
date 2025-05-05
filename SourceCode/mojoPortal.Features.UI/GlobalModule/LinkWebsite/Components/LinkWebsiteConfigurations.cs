using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;
using mojoPortal.Web.Controls.google;
using mojoPortal.Web.Framework;

namespace mojoPortal.Features
{
    /// <summary>
    /// encapsulates the feature instance configuration loaded from module settings into a more friendly object
    /// </summary>
    public class LinkWebsiteConfigurations
    {
        public LinkWebsiteConfigurations()
        { }

        public LinkWebsiteConfigurations(Hashtable settings)
        {
            LoadSettings(settings);

        }
        private void LoadSettings(Hashtable settings)
        {
            if (settings == null) { throw new ArgumentException("must pass in a hashtable of settings"); }
            if (settings.Contains("CategoryLinkWebsiteSetting"))
                categoryLinkWebsiteSetting = settings["CategoryLinkWebsiteSetting"].ToString();

            if (settings.Contains("CategoryLinkSetting"))
                categoryLinkSetting = settings["CategoryLinkSetting"].ToString();

            if (settings.Contains("CategoryLinhVucSetting"))
                categoryLinhVucSetting = settings["CategoryLinhVucSetting"].ToString();

            if (settings.Contains("CategoryTongDieuTraSetting"))
                categoryTongDieuTraSetting = settings["CategoryTongDieuTraSetting"].ToString();
            //categorySettings = WebUtils.ParseBoolFromHashtable(settings, "CategorySettings", categorySettings);

        }
        private string categoryLinhVucSetting = string.Empty;
        public string CategoryLinhVucSetting
        {
            get { return categoryLinhVucSetting; }
        }

        private string categoryTongDieuTraSetting = string.Empty;
        public string CategoryTongDieuTraSetting
        {
            get { return categoryTongDieuTraSetting; }
        }


        private string categoryLinkWebsiteSetting = string.Empty;
        public string CategoryLinkWebsiteSetting
        {
            get { return categoryLinkWebsiteSetting; }
        }

        private string categoryLinkSetting = string.Empty;
        public string CategoryLinkSetting
        {
            get { return categoryLinkSetting; }
        }


    }
}
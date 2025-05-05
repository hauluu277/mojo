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
    public class ReportConfigurations
    {
        public ReportConfigurations()
        { }

        public ReportConfigurations(Hashtable settings)
        {
            LoadSettings(settings);

        }
        private void LoadSettings(Hashtable settings)
        {
            if (settings == null) { throw new ArgumentException("must pass in a hashtable of settings"); }
            if (settings.Contains("CategoryReportSetting"))
                categoryReportSetting = settings["CategoryReportSetting"].ToString();
            //categorySettings = WebUtils.ParseBoolFromHashtable(settings, "CategorySettings", categorySettings);

        }
        private string categoryReportSetting = string.Empty;
        public string CategoryReportSetting
        {
            get { return categoryReportSetting; }
        }



    }
}
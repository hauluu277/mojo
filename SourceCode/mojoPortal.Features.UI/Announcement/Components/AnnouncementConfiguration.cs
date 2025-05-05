using mojoPortal.Web.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Features
{
    public class AnnouncementConfiguration
    {
        public AnnouncementConfiguration()
        { }

        public AnnouncementConfiguration(Hashtable settings)
        {
            LoadSettings(settings);

        }
        private void LoadSettings(Hashtable settings)
        {
            if (settings == null) { throw new ArgumentException("must pass in a hashtable of settings"); }
            pageSize = WebUtils.ParseInt32FromHashtable(settings, "PageSizeSetting", pageSize);
            displayOrtherVideo = WebUtils.ParseInt32FromHashtable(settings, "DisplayOrtherVideoSetting", displayOrtherVideo);


            if (settings.Contains("RoleSetting"))
            {
                roleSetting = settings["RoleSetting"].ToString();
            }
        }
        private int pageSize = 20;
        public int PageSize
        {
            get { return pageSize; }
        }
        private int displayOrtherVideo = 20;
        public int DisplayOrtherVideo
        {
            get { return displayOrtherVideo; }
        }
        private string roleSetting = string.Empty;
        public string RoleSetting
        {
            get { return roleSetting; }
        }
    }
}
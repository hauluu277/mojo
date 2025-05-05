using mojoPortal.Web.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Features
{
    public class VideoIntroduceConfiguration
    {
        public VideoIntroduceConfiguration()
        { }

        public VideoIntroduceConfiguration(Hashtable settings)
        {
            LoadSettings(settings);

        }

        private void LoadSettings(Hashtable settings)
        {
            if (settings == null) { throw new ArgumentException("must pass in a hashtable of settings"); }
            pageSize = WebUtils.ParseInt32FromHashtable(settings, "PageSizeSetting", pageSize);
            displayOrtherVideo = WebUtils.ParseInt32FromHashtable(settings, "DisplayOrtherVideoSetting", displayOrtherVideo);

            if (settings.Contains("KieuHienThiSetting"))
                kieuHienThiSetting = settings["KieuHienThiSetting"].ToString();
            numberShowSetting = WebUtils.ParseInt32FromHashtable(settings, "NumberShowSetting", numberShowSetting);
            videoHienThiSetting = WebUtils.ParseInt32FromHashtable(settings, "VideoHienThiSetting", videoHienThiSetting);
        }

        private int numberShowSetting = 5;
        public int NumberShowSetting
        {
            get { return numberShowSetting; }
        }
        private int videoHienThiSetting = 0;
        public int VideoHienThiSetting
        {
            get { return videoHienThiSetting; }
        }


        private string kieuHienThiSetting = "";
        public string KieuHienThiSetting
        {
            get { return kieuHienThiSetting; }
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
    }
}
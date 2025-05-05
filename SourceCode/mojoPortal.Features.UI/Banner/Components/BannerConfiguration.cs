using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;
using mojoPortal.Web.Controls.google;
using mojoPortal.Web.Framework;

namespace mojoPortal.Features
{
    public class BannerConfiguration
    {
        public BannerConfiguration()
        {

        }
        public BannerConfiguration(Hashtable settings)
        {
            if (settings == null) { throw new ArgumentException("you must pass in a Hashtable with settings"); }
            if (settings.Contains("BannerModuleConfigSetting"))
                bannerModuleConfig = settings["BannerModuleConfigSetting"].ToString();
            showPager = WebUtils.ParseBoolFromHashtable(settings, "LookupShowPagerInListSetting", showPager);
            pageSize = WebUtils.ParseInt32FromHashtable(settings, "PageSizeSetting", pageSize);
            bannerWidth = WebUtils.ParseInt32FromHashtable(settings, "bannerWidthSetting", bannerWidth);
            bannerHeight = WebUtils.ParseInt32FromHashtable(settings, "bannerHeightSetting", bannerHeight);
            bannerNumber = WebUtils.ParseInt32FromHashtable(settings, "bannerNumberSetting", bannerNumber);
            bannerNumberRow = WebUtils.ParseInt32FromHashtable(settings, "bannerNumberRowSetting", bannerNumberRow);
            lookupNumberShow = WebUtils.ParseInt32FromHashtable(settings, "LookupNumberShow", lookupNumberShow);
            heightSetting = WebUtils.ParseBoolFromHashtable(settings, "HeightSetting", heightSetting);
            horizontal = WebUtils.ParseBoolFromHashtable(settings, "horizontalSetting", horizontal);


            lengthwise = WebUtils.ParseBoolFromHashtable(settings, "lengthwiseSetting", lengthwise);
            bannerMargin = WebUtils.ParseInt32FromHashtable(settings, "BannerMarginSetting", bannerMargin);
            bannerWidthSlideTop = WebUtils.ParseInt32FromHashtable(settings, "BannerWidthSlideTopSetting", bannerWidthSlideTop);
            bannerHeightSlideTop = WebUtils.ParseInt32FromHashtable(settings, "BannerHeightSlideTopSetting", bannerHeightSlideTop);
            speedSlide = WebUtils.ParseInt32FromHashtable(settings, "SpeedSlide", speedSlide);
            timeChangeImage = WebUtils.ParseInt32FromHashtable(settings, "TimeChangeImage", timeChangeImage);
            if (settings.Contains("NameSetting"))
            {
                nameSetting = settings["NameSetting"].ToString();
            }

            if (settings.Contains("SliderSetting"))
            {
                sliderSetting = settings["SliderSetting"].ToString();
            }
            if(settings.Contains("TitleBannerSetting"))
            {
                headBanner = settings["TitleBannerSetting"].ToString();
            }

            if (settings.Contains("ModuleCssCustomeSetting"))
                moduleCssCustome = settings["ModuleCssCustomeSetting"].ToString();
            if (settings.Contains("ArticleDateTimeFormat"))
            {
                string format = settings["ArticleDateTimeFormat"].ToString().Trim();
                if (format.Length > 0)
                {
                    try
                    {
#pragma warning disable 168
                        string d = DateTime.Now.ToString(format, CultureInfo.CurrentCulture);
#pragma warning restore 168
                        dateTimeFormat = format;
                    }
                    catch (FormatException) { }
                }

            }
            if (settings.Contains("RolesThatCanDoFoo"))
            {
                allowedRoles = settings["RolesThatCanDoFoo"].ToString();
            }
        }

        private string nameSetting = "Đối tác";
        public string NameSetting
        {
            get { return nameSetting; }
        }

        private string dateTimeFormat = string.Empty;
        public string DateTimeFormat
        {
            get { return dateTimeFormat; }
        }
        private string headBanner = "KHOA PHÒNG";
        public string HeadBanner
        {
            get { return headBanner; }
        }
        private int speedSlide = 3500;
        public int SpeedSlide
        {
            get { return speedSlide; }
        }
        //Thiết lập chung banner
        #region Banner Setting

        //Tùy chỉnh class css
        string moduleCssCustome = "";
        public string ModuleCssCustome
        {
            get { return moduleCssCustome; }
        }
        //Cài đặt chiều cao cho banner
        private bool heightSetting = false;
        public bool HeightSetting
        {
            get { return heightSetting; }
        }

        private bool horizontal = false;
        public bool Horizontal
        {
            get { return horizontal; }
        }
        //Độ rộng banner
        private int bannerWidth = 100;
        public int BannerWidth
        {
            get { return bannerWidth; }
        }

        private int bannerNumberRow = 3;
        public int BannerNumberRow
        {
            get { return bannerNumberRow; }
        }

        private int bannerNumber = 3;
        public int BannerNumber
        {
            get { return bannerNumber; }
        }
        #endregion


        #region Setup Slider Banner

        private int timeChangeImage = 2500;
        public int TimeChangeImage
        {
            get { return timeChangeImage; }
        }

        private string sliderSetting = "0";
        public string SliderSetting
        {
            get { return sliderSetting; }
        }

        private int bannerHeightSlideTop = 100;
        public int BannerHeightSlideTop
        {
            get { return bannerHeightSlideTop; }
        }

        private int bannerWidthSlideTop = 200;
        public int BannerWidthSlideTop
        {
            get { return bannerWidthSlideTop; }
        }

        private int bannerHeight = 100;
        public int BannerHeight
        {
            get { return bannerHeight; }
        }
        #endregion

        private int bannerMargin = 10;
        public int BannerMargin
        {
            get { return bannerMargin; }
        }

        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
        }

        private int lookupNumberShow = 5;
        public int LookupNumberShow
        {
            get { return lookupNumberShow; }
        }

        private bool lengthwise = false;
        public bool Lengthwise
        {
            get { return lengthwise; }
        }
        private string allowedRoles = string.Empty;
        public string AllowedRoles
        {
            get { return allowedRoles; }
        }
        private bool showPager = true;

        public bool ShowPager
        {
            get { return showPager; }
        }
        private string approverRoles = "Admins;Content Administrators;";

        public string ApproverRoles
        {
            get { return approverRoles; }
        }
        private string bannerModuleConfig;
        public string BannerModuleConfig
        {
            get { return bannerModuleConfig; }
        }

      
    }
}
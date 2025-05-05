using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;
using mojoPortal.Web.Controls.google;
using mojoPortal.Web.Framework;

namespace mojoPortal.Features
{
    public class DocumentConfiguration
    {
        public DocumentConfiguration()
        {

        }
        public DocumentConfiguration(Hashtable settings)
        {
            if (settings == null) { throw new ArgumentException("you must pass in a Hashtable with settings"); }
            showPager = WebUtils.ParseBoolFromHashtable(settings, "LookupShowPagerInListSetting", showPager);
            pageSize = WebUtils.ParseInt32FromHashtable(settings, "PageSizeSetting", pageSize);
            showLeftPanelSetting = WebUtils.ParseBoolFromHashtable(settings, "ShowLeftPanelSetting", showLeftPanelSetting);
            showRightPanelSetting = WebUtils.ParseBoolFromHashtable(settings, "ShowRightPanelSetting", showRightPanelSetting);
            documentOtherNumber = WebUtils.ParseInt32FromHashtable(settings, "DocumentOtherNumberSetting", documentOtherNumber);
            documentSildeNumber = WebUtils.ParseInt32FromHashtable(settings, "documentSildeNumberSetting", documentSildeNumber);

            nhomVanBanSetting = WebUtils.ParseInt32FromHashtable(settings, "NhomVanBanSetting", nhomVanBanSetting);

            if (settings.Contains("SoLuongHienThiSetting"))
            {
                soLuongHienThi = settings["SoLuongHienThiSetting"].ToString();
            }

            if (settings.Contains("CssCustomSetting"))
            {
                cssCustom = settings["CssCustomSetting"].ToString();
            }
            if (settings.Contains("TitleHotNewSetting"))
            {
                titleHotNew = settings["TitleHotNewSetting"].ToString();
            }

            showBlogSearchBox = WebUtils.ParseBoolFromHashtable(settings, "ShowBlogSearchBox", showBlogSearchBox);
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
        private int nhomVanBanSetting = 0;
        public int NhomVanBanSetting
        {
            get
            {
                return nhomVanBanSetting;
            }
        }
        private string cssCustom = string.Empty;
        public string CssCustom
        {
            get
            {
                return cssCustom;
            }
        }
        private string dateTimeFormat = string.Empty;
        public string DateTimeFormat
        {
            get { return dateTimeFormat; }
        }
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
        }
        private int documentOtherNumber = 10;
        public int DocumentOtherNumber
        {
            get { return documentOtherNumber; }
        }

        private int documentSildeNumber = 5;
        public int DocumentSildeNumber
        {
            get { return documentSildeNumber; }
        }
        private string allowedRoles = string.Empty;
        public string AllowedRoles
        {
            get { return allowedRoles; }
        }

        private bool showLeftPanelSetting = true;

        public bool ShowLeftPanelSetting
        {
            get { return showLeftPanelSetting; }
        }
        private bool showBlogSearchBox = false;

        public bool ShowBlogSearchBox
        {
            get { return showBlogSearchBox; }
        }
        private bool showRightPanelSetting = true;

        public bool ShowRightPanelSetting
        {
            get { return showRightPanelSetting; }
        }

        private Unit editorHeight = Unit.Parse("350");
        public Unit EditorHeight { get { return editorHeight; } }

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
        private string soLuongHienThi = string.Empty;
        public string SoLuongHienThi
        {
            get { return soLuongHienThi; }
            set { soLuongHienThi = value; }
        }
        private string titleHotNew = string.Empty;
        public string TitleHotNew
        {
            get { return titleHotNew; }
            set { titleHotNew = value; }
        }
    }
}
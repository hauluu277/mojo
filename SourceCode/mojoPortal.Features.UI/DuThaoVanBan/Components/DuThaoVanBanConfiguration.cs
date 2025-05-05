using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;
using mojoPortal.Web.Controls.google;
using mojoPortal.Web.Framework;

namespace DuThaoVanBanFeature.UI
{
    public class DuThaoVanBanConfiguration
    {
        public DuThaoVanBanConfiguration()
        {

        }
        public DuThaoVanBanConfiguration(Hashtable settings)
        {
            if (settings == null) { throw new ArgumentException("you must pass in a Hashtable with settings"); }
            showPager = WebUtils.ParseBoolFromHashtable(settings, "ShowPagerSetting", showPager);
            pageSize = WebUtils.ParseInt32FromHashtable(settings, "PageSizeSetting", pageSize);
            showLeftPanelSetting = WebUtils.ParseBoolFromHashtable(settings, "ShowLeftPanelSetting", showLeftPanelSetting);
            showRightPanelSetting = WebUtils.ParseBoolFromHashtable(settings, "ShowRightPanelSetting", showRightPanelSetting);
            duThaoOrtherNumber = WebUtils.ParseInt32FromHashtable(settings, "DuThaoOrtherNumberSetting", duThaoOrtherNumber);
            pageSizeComment = WebUtils.ParseInt32FromHashtable(settings, "PageSizeCommentSetting", pageSizeComment);
            showPagerComment = WebUtils.ParseBoolFromHashtable(settings, "ShowPagerCommentSetting", showPagerComment);
            numberCharacter = WebUtils.ParseInt32FromHashtable(settings, "NumberCharacterSetting", numberCharacter);
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
            if (settings.Contains("RoleApproveSetting"))
            {
                roles = settings["RoleApproveSetting"].ToString();
                roleApprove = roles.Split(';');
            }
            if (settings.Contains("RolesThatCanDoFoo"))
            {
                allowedRoles = settings["RolesThatCanDoFoo"].ToString();
            }
        }
        private string roles = string.Empty;
        private IList roleApprove;
        public IList RoleApprove { get { return roleApprove; } }
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
        private int pageSizeComment = 10;
        public int PageSizeComment
        {
            get { return pageSizeComment; }
        }
        private int numberCharacter = 10;
        public int NumberCharacter
        {
            get { return numberCharacter; }
        }
        private int duThaoOrtherNumber = 10;
        public int DuThaoOrtherNumber
        {
            get { return duThaoOrtherNumber; }
        }

        private string allowedRoles = string.Empty;
        public string AllowedRoles
        {
            get { return allowedRoles; }
        }

        private bool showPagerComment = true;

        public bool ShowPagerComment
        {
            get { return showPagerComment; }
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
    }
}
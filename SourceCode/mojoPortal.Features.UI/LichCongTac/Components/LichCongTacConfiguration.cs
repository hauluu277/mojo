using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;
using mojoPortal.Web.Controls.google;
using mojoPortal.Web.Framework;

namespace mojoPortal.Features
{
    public class LichCongTacConfiguration
    {
        public LichCongTacConfiguration()
        {

        }
        public LichCongTacConfiguration(Hashtable settings)
        {
            if (settings == null) { throw new ArgumentException("you must pass in a Hashtable with settings"); }
            showPager = WebUtils.ParseBoolFromHashtable(settings, "ShowPagerInListSetting", showPager);
            pageSize = WebUtils.ParseInt32FromHashtable(settings, "PageSizeSeeting", pageSize);
            if (settings.Contains("RolesThatCanDoFoo"))
            {
                allowedRoles = settings["RolesThatCanDoFoo"].ToString();
            }
            if (settings.Contains("RoleAdminSetting"))
            {
                roles = settings["RoleAdminSetting"].ToString();
                roleAdmin = settings["RoleAdminSetting"].ToString();
                //roleAdmin = roles.Split(';');
            }

        }
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
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

        private string roles = string.Empty;
        private string roleAdmin;
        public string RoleAdmin { get { return roleAdmin; } }

    }
}
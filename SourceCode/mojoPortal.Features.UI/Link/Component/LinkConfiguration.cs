using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;
using mojoPortal.Web.Controls.google;
using mojoPortal.Web.Framework;

namespace mojoPortal.Features
{
    public class LinkConfiguration
    {
        public LinkConfiguration()
        {

        }
        public LinkConfiguration(Hashtable settings)
        {
            if (settings == null) { throw new ArgumentException("you must pass in a Hashtable with settings"); }
            if (settings.Contains("LinkCategoryConfigSetting"))
                //linkCategoryConfig = WebUtils.ParseStringFromQueryString(settings, "LinkCategoryConfigSetting", linkCategoryConfig);
                linkCategoryConfig = settings["LinkCategoryConfigSetting"].ToString();
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

            orderBySetting = WebUtils.ParseInt32FromHashtable(settings, "OrderSetting", orderBySetting);

            if (settings.Contains("ModuleCssCustomeSetting"))
                moduleCssCustome = settings["ModuleCssCustomeSetting"].ToString();


            displayList = WebUtils.ParseInt32FromHashtable(settings, "DisplayListSetting", displayList);
            //if (settings.Contains("DisplayListSetting"))
            //{
            //    displayList = settings["DisplayListSetting"].ToString();
            //}


        }

        private int displayList = 1;
        public int DisplayList
        {
            get { return displayList; }
        }

        private string dateTimeFormat = string.Empty;
        public string DateTimeFormat
        {
            get { return dateTimeFormat; }
        }

        private string allowedRoles = string.Empty;
        public string AllowedRoles
        {
            get { return allowedRoles; }
        }

        private string approverRoles = "Admins;Content Administrators;";

        public string ApproverRoles
        {
            get { return approverRoles; }
        }

        private string linkCategoryConfig;
        public string LinkCategoryConfig
        {
            get { return linkCategoryConfig; }
        }
        string moduleCssCustome = "";
        public string ModuleCssCustome
        {
            get { return moduleCssCustome; }
        }

        private int orderBySetting = 1;
        public int OrderBySetting
        {
            get { return orderBySetting; }
        }
    }
}
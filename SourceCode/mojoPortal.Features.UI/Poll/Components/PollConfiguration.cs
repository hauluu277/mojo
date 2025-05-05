using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Features
{
    public class PollConfiguration
    {
        public PollConfiguration() { }
        public PollConfiguration(Hashtable settings)
        {
            if (settings == null) { throw new ArgumentException("must pass in a hashtable of settings"); }

            pageSize = WebUtils.ParseInt32FromHashtable(settings, "PollSettingPageSize", pageSize);

            showPager = WebUtils.ParseBoolFromHashtable(settings, "PollShowPagerInListSetting", showPager);
            if (settings.Contains("RolePublishSetting"))
            {
                roles = settings["RolePublishSetting"].ToString();
                rolePublish = roles.Split(';');
            }
            if (settings.Contains("RoleApproveSetting"))
            {
                roles = settings["RoleApproveSetting"].ToString();
                roleApprove = roles.Split(';');
            }

            if (settings.Contains("TitleSetting"))
            {
                titleSetting = settings["TitleSetting"].ToString();
            }
        }
        private string titleSetting = PollResources.PollTitle;
        private string roles = string.Empty;
        private IList rolePublish;
        public string TitleSetting
        {
            get { return titleSetting; }
        }
        public IList RolePublish { get { return rolePublish; } }

        private IList roleApprove;
        public IList RoleApprove { get { return roleApprove; } }
        private int pageSize = 1;
        public int PageSize
        {
            get { return pageSize; }
        }
        private bool showPager;

        public bool ShowPager
        {
            get { return showPager; }
        }
    }
}
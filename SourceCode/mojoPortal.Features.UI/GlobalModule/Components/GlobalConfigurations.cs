using mojoPortal.Web.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Features
{
    public class GlobalConfigurations
    {
        public GlobalConfigurations()
        { }

        public GlobalConfigurations(Hashtable settings)
        {
            LoadSettings(settings);

        }
        private void LoadSettings(Hashtable settings)
        {
            if (settings == null) { throw new ArgumentException("must pass in a hashtable of settings"); }
            if (settings.Contains("CategorySetting"))
            {
                categorySetting = settings["CategorySetting"].ToString();
                if (!string.IsNullOrEmpty(categorySetting))
                {
                    var split = categorySetting.Split('-');
                    GetCategoryFist = int.Parse(split[0]);
                }
            }

            if (settings.Contains("GroupHienThiSetting"))
            {
                groupHienThiSetting = settings["GroupHienThiSetting"].ToString();
                if (!string.IsNullOrEmpty(groupHienThiSetting))
                {
                    var split = groupHienThiSetting.Split('-');
                    GetGroupFirst = int.Parse(split[0]);
                }
            }
            colunmHienThiSetting = WebUtils.ParseInt32FromHashtable(settings, "ColunmHienThiSetting", colunmHienThiSetting);
        }

        public int colunmHienThiSetting = 0;
        public int ColunmHienThiSetting
        {
            get { return colunmHienThiSetting; }
        }

        private string groupHienThiSetting = "";
        public string GroupHienThiSetting
        {
            get { return groupHienThiSetting; }
        }
        public int GetGroupFirst = 0;

        public int GetCategoryFist = 0;
        private string categorySetting = "";
        public string CategorySetting
        {
            get
            {
                return categorySetting;
            }
        }
    }
}
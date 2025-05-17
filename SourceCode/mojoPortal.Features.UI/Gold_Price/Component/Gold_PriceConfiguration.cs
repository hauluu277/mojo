using mojoPortal.Web.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Resources;

namespace Gold_PriceFeatures.UI
{
    public class Gold_PriceConfiguration
    {

        string moduleGold_PriceCssCustome = "";
        public string ModuleGold_PriceCssCustome
        {
            get { return moduleGold_PriceCssCustome; }
        }
        public Gold_PriceConfiguration()
        { }

        public Gold_PriceConfiguration(Hashtable settings)
        {
            LoadSettings(settings);

        }

        private void LoadSettings(Hashtable settings)
        {
            if (settings == null) { throw new ArgumentException("must pass in a hashtable of settings"); }
            if (settings.Contains("ClassCustomSetting"))
            {
                classCustom = settings["ClassCustomSetting"].ToString();
            }
            recenListPageSize = WebUtils.ParseInt32FromHashtable(settings, "RecentListPageSizeSetting", recenListPageSize);
            showNumberQuestion = WebUtils.ParseInt32FromHashtable(settings, "ShowNumberQuestionSetting", showNumberQuestion);
            if (settings.Contains("TitleSetting"))
            {
                titleSetting = settings["TitleSetting"].ToString();
            }
            if (settings.Contains("ModuleGold_PriceCssCustome"))
                moduleGold_PriceCssCustome = settings["ModuleGold_PriceCssCustome"].ToString();
            if (settings.Contains("GiaoDienHienThiQASetting"))
            {
                giaoDienHienThi = settings["GiaoDienHienThiQASetting"].ToString();
            }
        }

        private string titleSetting = SwirlingQuestionResource.QuestionAnswer;
        public string TitleSetting
        {
            get { return titleSetting; }
        }
        private string classCustom = string.Empty;
        public string ClassCustom
        {
            get { return classCustom; }
        }
        private int showNumberQuestion = 10;
        public int ShowNumberQuestion
        {
            get { return showNumberQuestion; }
        }
        private int recenListPageSize = 10;
        public int RecentListPageSize
        {
            get { return recenListPageSize; }
        }

        private string giaoDienHienThi = "HienThiDangList";
        public string GiaoDienHienThi
        {
            get { return giaoDienHienThi; }
        }
    }
}
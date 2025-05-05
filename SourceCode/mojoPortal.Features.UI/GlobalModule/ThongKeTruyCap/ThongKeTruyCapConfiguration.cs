using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Features.UI.GlobalModule.ThongKeTruyCap
{
    public class ThongKeTruyCapConfiguration
    {
        public ThongKeTruyCapConfiguration()
        { }

        public ThongKeTruyCapConfiguration(Hashtable settings)
        {
            LoadSettings(settings);

        }
        private void LoadSettings(Hashtable settings)
        {
            if (settings == null) { throw new ArgumentException("must pass in a hashtable of settings"); }
            if (settings.Contains("ArticleCategoryConfigSetting"))
                thongKeTruyCapCategoryConfig = settings["ThongKeTruyCapCategorySetting"].ToString();

            if (settings.Contains("SetGiaoDienSetting") && settings["SetGiaoDienSetting"].ToString() != "")
            {
                setGiaoDien = Convert.ToInt32(settings["SetGiaoDienSetting"]);
            }

            if (settings.Contains("ModuleDisplayCssCustomeSetting"))
                moduleDisplayCssCustome = settings["ModuleDisplayCssCustomeSetting"].ToString();
        }
        string moduleDisplayCssCustome = "";
        public string ModuleDisplayCssCustome
        {
            get { return moduleDisplayCssCustome; }
        }
        private int setGiaoDien = 1;
        public int SetGiaoDien
        {
            get { return setGiaoDien; }
        }

        private string thongKeTruyCapCategoryConfig;
        public string ThongKeTruyCapCategoryConfig
        {
            get { return thongKeTruyCapCategoryConfig; }
        }
    }
}
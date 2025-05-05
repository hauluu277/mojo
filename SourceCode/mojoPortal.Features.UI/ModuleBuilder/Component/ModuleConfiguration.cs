using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mojoPortal.Web.Framework;

namespace mojoPortal.Features
{
    public class ModuleConfiguration
    {
        public ModuleConfiguration()
        { }

        public ModuleConfiguration(Hashtable settings)
        {
            LoadSettings(settings);

        }
        private void LoadSettings(Hashtable settings)
        {
            if (settings == null) { throw new ArgumentException("must pass in a hashtable of settings"); }

            if (settings.Contains("MaKhoaPhongSetting"))
            {
                maKhoaPhongSetting = settings["MaKhoaPhongSetting"].ToString();
            }
            if (settings.Contains("DepartmentSetting"))
            {
                departmentSetting = settings["DepartmentSetting"].ToString();
            }
            if (settings.Contains("HocViSetting"))
            {
                hocViSetting = settings["HocViSetting"].ToString();
            }

        }
        private string hocViSetting = string.Empty;
        public string HocViSetting { get { return hocViSetting; } }

        private string departmentSetting = string.Empty;
        public string DepartmentSetting { get { return departmentSetting; } }

        private string maKhoaPhongSetting = string.Empty;
        public string MaKhoaPhongSetting { get { return maKhoaPhongSetting; } }
    }
}
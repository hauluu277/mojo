using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Features.UI.GlobalModule.FileHome.Components
{
    public class FileHomeConfigurations
    {
        public FileHomeConfigurations()
        { }

        public FileHomeConfigurations(Hashtable settings)
        {
            LoadSettings(settings);

        }
        private void LoadSettings(Hashtable settings)
        {
            if (settings == null) { throw new ArgumentException("must pass in a hashtable of settings"); }
            if (settings.Contains("FileHomeSetting"))
                fileHomeSetting = settings["FileHomeSetting"].ToString();

            //categorySettings = WebUtils.ParseBoolFromHashtable(settings, "CategorySettings", categorySettings);

        }
        private string fileHomeSetting = string.Empty;
        public string FileHomeSetting
        {
            get { return fileHomeSetting; }
        }
    }
}
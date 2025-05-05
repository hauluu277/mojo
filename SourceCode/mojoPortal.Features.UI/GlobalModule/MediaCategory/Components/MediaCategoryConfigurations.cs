using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Features.UI.GlobalModule.MediaCategory.Components
{
    public class MediaCategoryConfigurations
    {
        public MediaCategoryConfigurations()
        { }

        public MediaCategoryConfigurations(Hashtable settings)
        {
            LoadSettings(settings);

        }
        private void LoadSettings(Hashtable settings)
        {
            if (settings == null) { throw new ArgumentException("must pass in a hashtable of settings"); }
            if (settings.Contains("MediaCategorySetting"))
                mediaCategorySetting = settings["MediaCategorySetting"].ToString();

            //categorySettings = WebUtils.ParseBoolFromHashtable(settings, "CategorySettings", categorySettings);

        }
        private string mediaCategorySetting = string.Empty;
        public string MediaCategorySetting
        {
            get { return mediaCategorySetting; }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Features
{
    public static class BannerUtils
    {
        public static string FormatImageDialog(string path, string imageUrl)
        {
            return imageUrl != string.Empty && imageUrl.Contains(".")
                                    ? "~/" + path + imageUrl
                                //? "~/" + path + imageUrl.Insert(imageUrl.LastIndexOf("."), "_t")
                                : "~/" + path + "logo.png";
        }
        public static string ImageApprove(bool isApprove)
        {
            string imageName = "minus-circle.gif";
            if (isApprove)
            {
                imageName = "tick-circle.gif";
            }
            return string.Format("/Data/SiteImages/article-icon/{0}", imageName);
        }
    }
}
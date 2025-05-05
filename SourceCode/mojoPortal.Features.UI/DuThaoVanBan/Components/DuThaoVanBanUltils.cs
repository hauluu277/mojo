using System;
using System.Globalization;
using System.Text;
using mojoPortal.Web.Framework;
using Resources;
using Utilities;
using ArticleFeature.Business;
using mojoPortal.Features.Business.Utilities;

namespace DuThaoVanBanFeature.UI
{
    public static class DuThaoVanBanConstant
    {
        /// <summary>
        /// Feature Guid of Document module
        /// </summary>
        public static string FEATUREGUID = "a040f25b-a4a0-4bb4-b072-31b30a8ae814";

    }
    public static class DuThaoVanBanUltils
    {
        public static string FormatBlogTitleUrl(string siteRoot, string itemUrl, int itemId, int pageId, int moduleId)
        {
            return FormatBlogTitleUrl(siteRoot, itemUrl, itemId, pageId, moduleId, false, string.Empty);
        }

        public static string FormatBlogTitleUrl(string siteRoot, string itemUrl, int itemId, int pageId, int moduleId, bool useOverrideUrl, string overrideUrl)
        {
            if (useOverrideUrl && overrideUrl.Length > 0)
            {
                return overrideUrl;
            }
            if (itemUrl.Length > 0)
                return siteRoot + itemUrl.Replace("~", string.Empty);
            return siteRoot + "/Document/Detail.aspx?pageid=" + pageId.ToInvariantString()
                + "&mid=" + moduleId.ToInvariantString()
                + "&item=" + itemId.ToInvariantString();
                
        }
        public static DateTime ParseDateTime(string s)
        {
            try
            {
                return DateTime.Parse(s, CultureInfo.CurrentCulture);
            }
            catch
            {
                return DateTime.Now;
            }
        }
        public static string Truncate(string Description, int length)
        {
            if (Description.Length > length)
            {
                Description = Description.Substring(0, length)+"...";
            }

            return Description;
        }
        public class LanguageConstant
        {
            public static int VN = 1;
            public static int EN = 2;
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
        public static DateTime? ToDataTime(this string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                var date = obj.Split('/');
                if (date != null)
                {
                    if (!string.IsNullOrEmpty(date[0]) && !string.IsNullOrEmpty(date[1]) && !string.IsNullOrEmpty(date[2]))
                    {
                        var day = int.Parse(date[0]).ToString("00");
                        var month = int.Parse(date[1]).ToString("00");
                        var year = int.Parse(date[2]).ToString("0000");
                        return DateTime.ParseExact(string.Format("{0}/{1}/{2}", day, month, year), "dd/MM/yyyy", null);
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }
    }
}
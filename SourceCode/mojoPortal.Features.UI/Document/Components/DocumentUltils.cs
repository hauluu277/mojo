using System;
using System.Globalization;
using System.Text;
using mojoPortal.Web.Framework;
using Resources;
using Utilities;
using ArticleFeature.Business;
using mojoPortal.Features.Business.Utilities;

namespace mojoPortal.Features
{
    public static class DocumentConstant
    {
        /// <summary>
        /// Feature Guid of Document module
        /// </summary>
        public static string FEATUREGUID = "a040f25b-a4a0-4bb4-b072-31b30a8ae814";

    }
    public static class DocumentUltils
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
        public class PageSize
        {
            public static int DefaultID = 0;
            public static string Default = "--Chọn định dạng--";
            public static int ImageID = 1;
            public static string Image = "Đăng ảnh";
            public static int FlashID = 2;
            public static string Flash = "Đăng flash";
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
       
    }
}
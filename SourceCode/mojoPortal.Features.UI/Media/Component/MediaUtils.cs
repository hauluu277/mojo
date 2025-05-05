using System;
using System.Globalization;
using System.Text;
using mojoPortal.Web.Framework;
using Resources;
using Utilities;
using mojoPortal.Features.Business.Utilities;
using System.IO;
using System.Drawing;
namespace mojoPortal.Features
{
    public static class MediaUtils
    {

        public static string ImageApprove(string status)
        {
            bool? isApprove = null;
            if (!string.IsNullOrEmpty(status))
            {
                isApprove = bool.Parse(status);
            }
            string imageName = "pending.gif";
            if (isApprove.HasValue && isApprove.Value)
            {
                imageName = "tick-circle.gif";
            }
            else if (isApprove.HasValue && !isApprove.Value)
            {
                imageName = "minus-circle.gif";
            }
            return string.Format("/Data/SiteImages/article-icon/{0}", imageName);
        }
        public static string FormatImageDialog(string path, string imageUrl)
        {
            return imageUrl != string.Empty && imageUrl.Contains(".")
                                ? "~/" + path + imageUrl.Insert(imageUrl.LastIndexOf("."), "_slide")
                                : "~/" + path + "logo.png";
        }
        public static string FormatEditGroupMediaTitleUrl(string siteRoot, string itemUrl, int itemId, int pageId, int moduleId)
        {
            return FormatEditGroupMediaTitleUrl(siteRoot, itemUrl, itemId, pageId, moduleId, false, string.Empty);
        }

        public static string FormatAudioTitleUrl(string siteRoot, string itemUrl, int itemId, int pageId, int moduleId)
        {
            if (itemUrl.Length > 0)
                return siteRoot + itemUrl.Replace("~", string.Empty);
            return siteRoot + "/Audio/DetailAudio.aspx?pageid=" + pageId.ToInvariantString()
                + "&groupId=" + itemId.ToInvariantString()
                + "&mid=" + moduleId.ToInvariantString();
        }

        public static string FormatEditGroupMediaTitleUrl(string siteRoot, string itemUrl, int itemId, int pageId, int moduleId, bool useOverrideUrl, string overrideUrl)
        {
            if (useOverrideUrl && overrideUrl.Length > 0)
            {
                return overrideUrl;
            }
            if (itemUrl.Length > 0)
                return siteRoot + itemUrl.Replace("~", string.Empty);
            return siteRoot + "/Media/ViewListMediaAlbum.aspx?pageid=" + pageId.ToInvariantString()
                + "&groupMediaId=" + itemId.ToInvariantString()
                + "&mid=" + moduleId.ToInvariantString();
        }
        public static string FormatMediaAlbumTitleUrl(string siteRoot, string itemUrl, int itemId, int pageId, int moduleId)
        {
            return FormatMediaAlbumTitleUrl(siteRoot, itemUrl, itemId, pageId, moduleId, false, string.Empty);
        }

        public static string FormatMediaAlbumTitleUrl(string siteRoot, string itemUrl, int itemId, int pageId, int moduleId, bool useOverrideUrl, string overrideUrl)
        {
            if (useOverrideUrl && overrideUrl.Length > 0)
            {
                return overrideUrl;
            }
            if (itemUrl.Length > 0)
                return siteRoot + itemUrl.Replace("~", string.Empty);
            return siteRoot + "/Media/ViewMediaAlbum.aspx?pageid=" + pageId.ToInvariantString()
                + "&ItemID=" + itemId.ToInvariantString()
                + "&mid=" + moduleId.ToInvariantString();
        }

        public static string FormatEditMediaAlbumTitleUrl(string siteRoot, string itemUrl, int itemId, int pageId, int moduleId)
        {
            return FormatEditMediaAlbumTitleUrl(siteRoot, itemUrl, itemId, pageId, moduleId, false, string.Empty);
        }

        public static string FormatEditMediaAlbumTitleUrl(string siteRoot, string itemUrl, int itemId, int pageId, int moduleId, bool useOverrideUrl, string overrideUrl)
        {
            if (useOverrideUrl && overrideUrl.Length > 0)
            {
                return overrideUrl;
            }
            if (itemUrl.Length > 0)
                return siteRoot + itemUrl.Replace("~", string.Empty);
            return siteRoot + "/Media/EditMediaAlbum.aspx?pageid=" + pageId.ToInvariantString()
                + "&ItemID=" + itemId.ToInvariantString()
                + "&mid=" + moduleId.ToInvariantString();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mojoPortal.Features.Business.Utilities;
using mojoPortal.Web.Framework;

namespace Gold_PriceFeatures.UI
{
    public class Gold_PriceUtils
    {
        public static string FormatQuestionListUrl(string siteRoot, string itemUrl, int pageId, int cateId, int cateChildId, int orderby, bool useOverrideUrl, string overrideUrl)
        {
            if (useOverrideUrl && overrideUrl.Length > 0)
            {
                return overrideUrl;
            }
            if (itemUrl.Length > 0)
                return siteRoot + itemUrl.Replace("~", string.Empty);
            return siteRoot + "/Gold_Price/Gold_PriceDisplayManager.aspx?pageid=" + pageId.ToInvariantString()
            + "&cateId=" + cateId.ToInvariantString()
            + "&cateChildId=" + cateChildId.ToInvariantString()
            + "&orderby=" + orderby.ToInvariantString();
        }

        public static string FormatDetailQuestionUrl(string siteRoot, int pageId, string itemUrl, int itemId, bool useOverrideUrl, string overrideUrl)
        {
            if (useOverrideUrl && overrideUrl.Length > 0)
            {
                return overrideUrl;
            }
            if (itemUrl.Length > 0)
                return siteRoot + itemUrl.Replace("~", string.Empty);
            return siteRoot + "/Gold_Price/Gold_PriceDisplayManager.aspx?pageid=" + pageId + "&itemId=" + itemId.ToInvariantString();
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Features
{
    public class VideoIntroduceUtils
    {
        public static string VideoItemUrl(int pageid, int mid, int itemId, int pageNumber)
        {
            return "/VideoIntroduce/ListVideoIntroduce.aspx?pageid=" + pageid + "&mid=" + mid + "&item=" + itemId + "&pagenumber=" + pageNumber;
        }
        public static string VideoDetailItemUrl(string siteRoot, int pageid, int mid, int itemId, string itemUrl)
        {
            //if (!string.IsNullOrEmpty(itemUrl))
            //    return itemUrl;
            //else
            return siteRoot + "/VideoIntroduce/DetailVideo.aspx?mid=" + mid + "&pageid=" + pageid + "&item=" + itemId;
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

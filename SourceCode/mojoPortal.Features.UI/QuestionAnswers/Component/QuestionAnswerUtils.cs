using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mojoPortal.Features.Business.Utilities;
using mojoPortal.Web.Framework;

namespace mojoPortal.Features
{
    public class QuestionAnswerUtils
    {
        public static string FormatQuestionListUrl(string siteRoot, string itemUrl, int pageId, int cateId, int cateChildId, int orderby, bool useOverrideUrl, string overrideUrl)
        {
            if (useOverrideUrl && overrideUrl.Length > 0)
            {
                return overrideUrl;
            }
            if (itemUrl.Length > 0)
                return siteRoot + itemUrl.Replace("~", string.Empty);
            return siteRoot + "/QuestionAnswers/ListQuestionAnswer.aspx?pageid=" + pageId.ToInvariantString()
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
            return siteRoot + "/QuestionAnswers/QuestionDetail.aspx?pageid=" + pageId + "&itemId=" + itemId.ToInvariantString();
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
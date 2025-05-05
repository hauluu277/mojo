using System;
using System.Globalization;
using System.Text;
using mojoPortal.Web.Framework;
using Resources;
using Utilities;
using mojoPortal.Features.Business.Utilities;

namespace mojoPortal.Features
{
    public static class PollUtils
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
    }
}
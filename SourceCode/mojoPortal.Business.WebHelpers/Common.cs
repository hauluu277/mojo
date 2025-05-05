using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace mojoPortal.Business.WebHelpers
{

   

    public static class Common
    {
        public static string FormatUrl(string siteRoot, string itemUrl)
        {
            if (!string.IsNullOrEmpty(itemUrl))
            {
                return siteRoot + itemUrl.Replace("~", string.Empty);
            }
            return string.Empty;
        }
        public static List<ListItem> ListMorderation(int? selected = 0)
        {
            var result = new List<ListItem>()
            {
                //new ListItem{Text="Tất cả",Value="-1"},
                new ListItem{Text="Chờ xuất bản",Value=Comment.ModerationPending.ToString()},
                new ListItem{Text="Đã xuất bản",Value=Comment.ModerationApproved.ToString()},
                new ListItem{Text="Không xuất bản",Value=Comment.ModerationRejected.ToString()}
            };
            return result;
        }


        public static string StatusComment(this object status)
        {
            if (status != null)
            {
                int state = Convert.ToInt32(status);
                if (state == Comment.ModerationApproved)
                {
                    return "<span class=\"text-success\"><i class=\"fa fa-check fa-lg\" aria-hidden=\"true\"></i></span>";
                }
                else if (state == Comment.ModerationRejected)
                {
                    return "<span class='text-danger'><i class=\"fa fa-ban fa-lg\" aria-hidden=\"true\"></i></span>";
                }
            }
            return "...";
        }

        public static string StatusIconComment(this object status)
        {
            if (status != null)
            {
                int state = Convert.ToInt32(status);
                if (state == Comment.ModerationApproved)
                {
                    return "/Data/SiteImages/article-icon/tick-circle.gif";
                }
                else if (state == Comment.ModerationRejected)
                {
                    return "/Data/SiteImages/article-icon/minus-circle.gif";
                }
            }
            return "/Data/SiteImages/article-icon/minus-circle.gif";
        }


        public static string GetStatusText(this object status)
        {
            if (status != null)
            {
                if (Convert.ToBoolean(status))
                {
                    return "<span style='font-weight:bold'>Hiển thị</span>";
                }
                return "<span style='font-weight:bold; color:red'>Không hiển thị</span>";
            }
            return string.Empty;
        }

        public static string md5EndCode(this string input)
        {
            MD5 md5Hash = new MD5CryptoServiceProvider();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        private static string url = "https://ipinfo.io/ip";
        public static IPAddress findMyIP()
        {
            return IPAddress.Parse(new WebClient().DownloadString(url));
        }

        /// <summary>
        /// Quyền quản lý bình luận tin bài
        /// </summary>
        /// <returns></returns>
        public static bool AccessManageComment()
        {
            return WebUser.IsInRoles(ConfigurationManager.AppSettings["RoleArticleComment"]);
        }

    }
}

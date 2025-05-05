using System;
using System.Globalization;
using System.Text;
using mojoPortal.Web.Framework;
using Resources;
using Utilities;
using ArticleFeature.Business;
using mojoPortal.Features.Business.Utilities;
using System.Web.UI.WebControls;
using System.IO;
using System.Web;
using System.Configuration;

namespace mojoPortal.Features
{
    public static class ArticleUtils
    {
        public static void AddConfirmButton(WebControl control, string strConfirm)
        {
            control.Attributes.Add("onclick", "javascript:return " +
                                              "confirm('" + strConfirm + "')");
        }
        public static string FormatDialogScript(string moduleID, string itemID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type='text/javascript'>");
            sb.Append("$(document).ready(function(){");
            sb.Append("$('.img" + moduleID + itemID + "').click(function(){");

            sb.Append("$('." + moduleID + itemID + "').dialog('option', 'width', 'auto');");
            sb.Append("$('." + moduleID + itemID + "').dialog('open'); return false;");

            sb.Append("});");
            sb.Append("});");
            sb.Append("</script>");
            return sb.ToString();
        }

        public static string CheckTargetTitleUrl(bool useOverrideUrl)
        {
            return useOverrideUrl ? "_blank" : "_self";
        }
        public static string FormatEventTitleUrl(string siteRoot, string itemUrl, int itemId, int pageId, int moduleId)
        {
            return FormatBlogTitleUrl(siteRoot, itemUrl, itemId, pageId, moduleId, false, string.Empty);
        }

        public static string FormatEventTitleUrl(string siteRoot, string itemUrl, int itemId, int pageId, int moduleId, bool useOverrideUrl, string overrideUrl)
        {
            if (useOverrideUrl && overrideUrl.Length > 0)
            {
                return overrideUrl;
            }
            if (itemUrl.Length > 0)
                return siteRoot + itemUrl.Replace("~", string.Empty);
            return siteRoot + "/Event/ViewPost.aspx?pageid=" + pageId.ToInvariantString()
                + "&ItemID=" + itemId.ToInvariantString()
                + "&mid=" + moduleId.ToInvariantString();
        }
        public static string FormatPostAuthor(string userGuid, ArticleConfiguration config)
        {
            if (!config.ShowPostAuthor) return string.Empty;
            DBUtilities repository = new DBUtilities();
            mp_User user = repository.GetUser(new Guid(userGuid));
            string result = "<div class='authorname'><span class='authorpre'>" + ArticleResources.PostAuthorFormat + "</span><span class='authorbg'></span>" + user.Name + "</div>";
            if (config.ShowAuthorOccupation)
            {
                result += "<div class='authorocc'><span class='authoroccpre'>" + ArticleResources.PostAuthorOccFormat + "</span><span class='authoroccbg'></span>" + user.Occupation + "</div>";
            }
            if (config.ShowAuthorWebsiteUrl)
            {
                result += "<div class='authorweb'><span class='authorwebpre'>" + ArticleResources.PostAuthorWebFormat + "</span><span class='authorwebbg'></span>" + user.WebSiteURL + "</div>";
            }
            if (config.ShowAuthorYahoo)
            {
                result += "<div class='authoryahoo'><span class='authoryahoopre'>" + ArticleResources.PostAuthorYahooFormat + "</span><span class='authoryahoobg'></span>" + user.Yahoo + "</div>";
            }
            if (config.ShowAuthorSignature)
            {
                result += "<div class='authorsign'><span class='authorsignpre'>" + ArticleResources.PostAuthorSignFormat + "</span><span class='authorsignbg'></span>" + user.Signature + "</div>";
            }
            return result;
        }

        public static string FormatBlogDate(DateTime startDate, ArticleConfiguration config, TimeZoneInfo timeZone, double TimeOffset)
        {
            if (config.DateTimeFormat == string.Empty) return string.Empty;
            return timeZone != null ? TimeZoneInfo.ConvertTimeFromUtc(startDate, timeZone).ToString(config.DateTimeFormat) : startDate.AddHours(TimeOffset).ToString(config.DateTimeFormat);
        }

        public static string FormatImageDialog(string path, string imageUrl)
        {
            if (!string.IsNullOrEmpty(imageUrl) && imageUrl.Contains("."))
            {
                if (imageUrl.Contains("http") || imageUrl.Contains("https")) return imageUrl;
                if (imageUrl.ToLower().Contains("/data/sites"))
                {
                    return imageUrl;
                }
                return "/" + path + imageUrl;
            }
            //return "/data/images/logo.png";
            return string.Empty;
        }

        public static string FormatImageDialogNew(string path, string imageUrl)
        {
            if (!string.IsNullOrEmpty(imageUrl) && imageUrl.Contains("."))
            {
                if (imageUrl.Contains("http") || imageUrl.Contains("https")) return imageUrl;
                if (imageUrl.ToLower().Contains("/Data/Sites"))
                {
                    return imageUrl;
                }
                return "/" + path + imageUrl;
            }
            //return "/data/images/logo.png";
            return string.Empty;
        }
        public static string FormatImageTooltip(string path, string imageUrl)
        {
            return imageUrl != string.Empty && imageUrl.Contains(".")
                                ? "/" + path + imageUrl
                                //? "/" + path + imageUrl.Insert(imageUrl.LastIndexOf("."), "_t")
                                : "/" + path + "logo.png";
        }
        public static string ApproveText(string status)
        {
            bool? isApprove = null;
            if (!string.IsNullOrEmpty(status))
            {
                isApprove = bool.Parse(status);
            }
            string status_text = "<span class='text-primary font-bold'>Chờ duyệt</span>";
            if (isApprove.HasValue && isApprove.Value)
            {
                status_text = "<span class='text-success font-bold'>Đã được duyệt</span>";
            }
            else if (isApprove.HasValue && !isApprove.Value)
            {
                status_text = "<span class='red font-bold'>Không duyệt</span>";
            }
            return status_text;
        }

        public static string PublishedText(string status)
        {
            bool? isApprove = null;
            if (!string.IsNullOrEmpty(status))
            {
                isApprove = bool.Parse(status);
            }
            string status_text = "<span class='text-primary font-bold'>Chờ xuất bản</span>";
            if (isApprove.HasValue && isApprove.Value)
            {
                status_text = "<span class='text-success font-bold'>Đã xuất bản</span>";
            }
            else if (isApprove.HasValue && !isApprove.Value)
            {
                status_text = "<span class='red font-bold'>Không xuất bản</span>";
            }
            return status_text;
        }

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

        //public static string FormatPostAuthor(string userGuid, ArticleSearchConfiguration config)
        //{
        //    if (!config.ShowPostAuthor) return string.Empty;
        //    DBUtilities repository = new DBUtilities();
        //    mp_User user = repository.GetUser(new Guid(userGuid));
        //    return string.Format(CultureInfo.InvariantCulture, ArticleResources.PostAuthorFormat, config.BlogAuthor.Length > 0 ? config.BlogAuthor : user.Name);
        //}

        //public static string FormatBlogDate(DateTime startDate, ArticleSearchConfiguration config, TimeZoneInfo timeZone, double TimeOffset)
        //{
        //    if (config.DateTimeFormat == string.Empty) return string.Empty;
        //    return timeZone != null ? TimeZoneInfo.ConvertTimeFromUtc(startDate, timeZone).ToString(config.DateTimeFormat) : startDate.AddHours(TimeOffset).ToString(config.DateTimeFormat);
        //}


        public static string BuildEditUrl(int itemID, string[] listModuleId, string SiteRoot, int PageId, int ModuleId)
        {
            string result = string.Empty;
            if (listModuleId != null && listModuleId.Length > 0)
            {
                Article item = new Article(itemID);
                if (item != null)
                {
                    DBUtilities repositoryUtilities = new DBUtilities();
                    mp_PageModule pageModule = repositoryUtilities.GetFirstPageByModuleID(item.ModuleID);
                    if (pageModule != null)
                    {
                        result = SiteRoot + "/Article/EditPost.aspx?pageid=" + pageModule.PageID + "&ItemID=" +
                                 itemID + "&mid=" + item.ModuleID;
                    }
                }
            }
            else
            {
                result = SiteRoot + "/Article/EditPost.aspx?pageid=" + PageId + "&ItemID=" +
                       itemID + "&mid=" + ModuleId;
            }
            return result;
        }

        public static string GetRssUrl(ArticleConfiguration config, string SiteRoot, int ModuleId)
        {
            if (config.FeedburnerFeedUrl.Length > 0) return config.FeedburnerFeedUrl;
            return SiteRoot + "/blog" + ModuleId.ToInvariantString() + "rss.aspx";
        }

        public static string FormatImageArticle(string path, string imageUrl)
        {
            return imageUrl != string.Empty ? "~/" + path + imageUrl : "/data/images/logo.png";
        }

        public static string FormatTooltip(string title, string content, ArticleConfiguration config)
        {
            string result;
            if (config.UseTooltipSettings)
            {
                if (content.Length > config.TooltipMaxCharSettings)
                {
                    content = UIHelper.CreateExcerpt(content, config.TooltipMaxCharSettings, "...");
                }
                result = "<div class='tooltip'><div class='title-l'></div><div class='title-m'>" + title + "</div><div class='title-r'></div><div class='body'>" + content + "</div></div>";
            }
            else
            {
                result = title;
            }
            return result;
        }

        public static bool CheckImageUrl(string imageUrl)
        {
            return imageUrl != string.Empty;
        }

        public static string FormatBlogTitle(string title, int maxChars)
        {
            if (title.Length > maxChars)
            {
                title = title.Remove(maxChars);
                title += "...";
            }
            return title;
        }

        public static string FormatBlogTitleUrl(string siteRoot, string itemUrl, int itemId, int pageId, int moduleId)
        {
            return FormatBlogTitleUrl(siteRoot, itemUrl, itemId, pageId, moduleId, false, string.Empty);
        }


        public static string FormatBlogTitleUrl(string siteRoot, string itemUrl, int itemId, int pageId, int moduleId, bool useOverrideUrl, string overrideUrl)
        {
            if (itemUrl.Contains(itemUrl))
            {
                return siteRoot + itemUrl.Replace("~/", "/");
            }
            if (useOverrideUrl && overrideUrl.Length > 0)
            {
                return overrideUrl;
            }
            if (itemUrl.Length > 0)
                return siteRoot + itemUrl.Replace("~", string.Empty);
            return siteRoot + "/Article/ViewPost.aspx?pageid=" + pageId.ToInvariantString()
                + "&ItemID=" + itemId.ToInvariantString()
                + "&mid=" + moduleId.ToInvariantString();
        }

        public static string FormatBlogEntry(string blogHtml, string excerpt, ArticleConfiguration config)
        {
            if (config.UseExcerpt)
            {
                if ((excerpt.Length > 0) && (excerpt != "<p>&#160;</p>"))
                {
                    return excerpt + config.ExcerptSuffix;
                }
                blogHtml = SecurityHelper.RemoveMarkup(blogHtml);
                if (blogHtml.Length > config.ExcerptLength)
                {
                    return UIHelper.CreateExcerpt(blogHtml, config.ExcerptLength, config.ExcerptSuffix);
                }
            }
            return blogHtml;
        }

        public static string FormatReadMoreLink(ArticleConfiguration config, string url, int itemId, string siteRoot, int pageId, int moduleId)
        {
            if (config.ShowMoreLinkText)
            {
                string itemUrl = FormatBlogTitleUrl(siteRoot, url, itemId, pageId, moduleId);
                return "<div class='morelink-wrapper'><div class='l'></div><div class='m'><a href='" + itemUrl + "' class='morelink'>" + ArticleResources.DetailLabel + "</a></div><div class='r'></div></div><div class='cleared'></div>";
            }
            else return string.Empty;
        }

        public static string FormatSlideArticle(string blogHtml, string excerpt, string url, int itemId, ArticleConfiguration config, string siteRoot, int pageId, int moduleId, int langId)
        {
            if (config.UseExcerpt)
            {
                string itemUrl = FormatBlogTitleUrl(siteRoot, url, itemId, pageId, moduleId);
                if ((excerpt.Length > 0) && (excerpt != "<p>&#160;</p>"))
                {
                    return excerpt + config.ExcerptSuffix + " <a href='" + itemUrl + "' class='morelink'>" + config.MoreLinkText + "</a><div>&nbsp;</div>";
                }
                blogHtml = SecurityHelper.RemoveMarkup(blogHtml);
                if (blogHtml.Length > config.MainSlideExcerpt)
                {

                    string result = UIHelper.CreateExcerpt(blogHtml, config.MainSlideExcerpt, config.ExcerptSuffix);
                    result += " <div class='morelink-wrapper'><div class='l'></div><div class='m'><a href='" + itemUrl + "' class='morelink'>" + config.MoreLinkText + "</a></div><div class='r'></div></div>";
                    return result;
                }
            }
            return blogHtml;
        }
        

        public static bool ShowImage(string imageUrl)
        {
            if (String.IsNullOrEmpty(imageUrl))
            {
                return false;
            }
            else
            {
                if (imageUrl.Contains("http") || imageUrl.Contains("https")) return true;
                if (imageUrl.ToLower().Contains("/data/sites"))
                {
                    string decodedUrl = HttpUtility.UrlDecode(imageUrl);
                    var file = HttpContext.Current.Server.MapPath(decodedUrl).Replace("\\", "/");
                    return File.Exists(file);
                }
                string filePath = HttpContext.Current.Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["ArticleImagesFolder"] + imageUrl;
                filePath = filePath.Replace("/", "\\");
                if (File.Exists(filePath))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }



        public static StringBuilder SetJQueryScript(ArticleConfiguration config, int moduleId)
        {
            StringBuilder sb = new StringBuilder();
            if (config.AutoScrollSetting)
            {
                sb.Append("<script type='text/javascript'>$(document).ready(function(){");
                sb.Append("$('.scrollable" + moduleId + "').scrollable({");
                sb.Append("circular: " + config.AutoScrollCircularSetting.ToString().ToLower());
                if (config.AutoScrollEasingSetting != string.Empty)
                {
                    sb.Append(", easing: '" + config.AutoScrollEasingSetting + "'");
                }
                sb.Append(", steps: " + config.AutoScrollItemsSetting);
                sb.Append(", speed: " + config.AutoScrollSpeedSetting);
                sb.Append(", vertical: " + config.AutoScrollVerticalSetting.ToString().ToLower());
                sb.Append("}).autoscroll(" + config.AutoScrollTimeSetting + ");});</script>");
            }
            if (config.UseTooltipSettings)
            {
                sb.Append("<script type='text/javascript'>");
                sb.Append("var prm = Sys.WebForms.PageRequestManager.getInstance();");
                sb.Append("prm.add_endRequest(function() {");
                sb.Append("tooltipajax" + moduleId + "();");
                sb.Append("}}");
                sb.Append("function tooltipajax" + moduleId + "(){");
                sb.Append("$(document).ready(function(){");
                sb.Append("$('.tooltipable" + moduleId + " a[title]').tooltip({");
                sb.Append("opacity: 1");
                sb.Append(", effect: '" + config.TooltipEffectSettings + "'");
                sb.Append(", delay: '" + config.TooltipDelaySettings + "'");
                sb.Append(", tipClass: '" + config.UseTooltipCssSettings + "'");
                sb.Append("}).dynamic({ bottom: { direction: 'down', bounce: true } });;});");
                sb.Append("}");
                sb.Append("tooltipajax" + moduleId + "();");
                sb.Append("</script>");
            }
            if (config.UseSlideArticle)
            {
                sb.Append("<script type='text/javascript'>$(document).ready(function(){");
                sb.Append("$('.mojo-tabs-slide" + moduleId +
                          "').tabs({fx:{opacity: 'toggle'}, select: function(event, ui) {");
                sb.Append("$(this).css('height', $(this).height());");
                sb.Append("$(this).css('overflow', 'hidden');");
                sb.Append("},");
                sb.Append("show: function(event, ui) {");
                sb.Append("$(this).css('height', 'auto');");
                sb.Append("$(this).css('overflow', 'visible');");
                sb.Append("}}).tabs('rotate' , " + config.SlideTimeTransition + " , true);");
                sb.Append("});</script>");
            }
            return sb;
        }
    }
}
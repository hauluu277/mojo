using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Xml;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.Framework;
using Argotic.Syndication;
using ArticleFeature.Business;
using mojoPortal.Web;


namespace ArticleFeature.UI
{

    public partial class RssPage : Page
    {
        private int moduleId = -1;
        private int pageID = -1;
        private PageSettings pageSettings;
        private Module module;
        private bool addSignature;
        Hashtable moduleSettings;
        private bool addCommentsLink;
        private string navigationSiteRoot = string.Empty;
        private string blogBaseUrl = string.Empty;
        private string imageSiteRoot = string.Empty;
        private string cssBaseUrl = string.Empty;
        private bool feedIsDisabled;
        private bool useExcerptInFeed;
        private string ExcerptSuffix = "...";
        private string MoreLinkText = "read more";
        private int excerptLength = 250;
        private bool ShowPostAuthorSetting;
        private bool showImageOnRSSSetting = false;
        private bool addTweetThisToFeed;
        private bool addFacebookLikeToFeed;
        private string channelDescription = string.Empty;
        private string copyright = string.Empty;
        private string authorEmail = string.Empty;
        private Guid securityBypassGuid = Guid.Empty;
        private bool canView;
        private int timeToLive = -1;


        protected void Page_Load(object sender, EventArgs e)
        {
            // nothing should post here
            if (Page.IsPostBack) return;

            LoadSettings();


            if (canView)
            {
                RenderRss();

            }
            else
            {
                RenderError("Invalid Request");
            }

        }



        private void RenderRss()
        {
            Argotic.Syndication.RssFeed feed = new Argotic.Syndication.RssFeed();
            RssChannel channel = new RssChannel { Generator = "http://wwww.aita.gov.vn Portal RSS" };
            feed.Channel = channel;
            channel.Title = module.ModuleTitle;
            channel.Copyright = "http://wwww.aita.gov.vn";
            channel.LastBuildDate = DateTime.Now;
            string pu = WebUtils.ResolveServerUrl(SiteUtils.GetCurrentPageUrl());

            channel.Link = new Uri(pu);
            if (channelDescription.Length > 0) { channel.Description = channelDescription; }
            if (copyright.Length > 0) { channel.Copyright = copyright; }
            if (authorEmail.Length > 0) { channel.ManagingEditor = authorEmail; }
            if (timeToLive > -1) { channel.TimeToLive = timeToLive; }

            using (IDataReader dr = Article.GetArticlesForRSS(moduleId, DateTime.UtcNow))
            {
                while (dr.Read())
                {
                    bool inFeed = Convert.ToBoolean(dr["IncludeInFeed"]);
                    if (!inFeed) { continue; }
                    RssItem item = new RssItem();
                    string blogItemUrl = FormatBlogUrl(dr["ItemUrl"].ToString(), Convert.ToInt32(dr["ItemID"]));
                    item.Link = new Uri(Request.Url, blogItemUrl);
                    item.Guid = new RssGuid(blogItemUrl);
                    item.Title = dr["Title"].ToString();
                    item.PublicationDate = Convert.ToDateTime(dr["StartDate"]);

                    if (ShowPostAuthorSetting)
                    {
                        // techically this is supposed to be an email address
                        // but wouldn't that lead to a lot of spam?
                        item.Author = dr["Name"].ToString();
                    }

                    item.Comments = new Uri(blogItemUrl);

                    string signature = string.Empty;

                    if (addSignature)
                    {
                        signature = "<br /><a href='" + blogItemUrl + "'>" + dr["Name"] + "</a>";
                    }

                    if (addCommentsLink)
                    {
                        signature += "&nbsp;&nbsp;" + "<a href='" + blogItemUrl + "'>...</a>";
                    }

                    if (addTweetThisToFeed)
                    {
                        signature += GenerateTweetThisLink(item.Title, blogItemUrl);

                    }

                    if (addFacebookLikeToFeed)
                    {
                        signature += GenerateFacebookLikeButton(blogItemUrl);

                    }


                    string blogPost = SiteUtils.ChangeRelativeUrlsToFullyQuailifiedUrls(navigationSiteRoot, imageSiteRoot, dr["Summary"].ToString());
                    if (showImageOnRSSSetting)
                    {
                        string image = "<div style='float:left;width:10%'><img width='100%' src='" + ConfigurationManager.AppSettings["DomainName"] + "/" + ConfigurationManager.AppSettings["ArticleImagesFolder"] + dr["ImageUrl"] + "' align=left alt='" + dr["Title"].ToString() + "'/></div>";
                        string content = "<div style='float:left;width:90%'>" + blogPost + signature + "</div><div style='clear:both;'></div>";
                        item.Description = image + content;
                    }
                    else
                    {
                        item.Description = "<div style='float:left;width:100%'>" + blogPost + signature + "</div>";
                    }
                    //if ((!useExcerptInFeed) || (blogPost.Length <= excerptLength))
                    //{
                    //    item.Description = blogPost + signature;
                    //}
                    //else
                    //{
                    //    string excerpt = SiteUtils.ChangeRelativeUrlsToFullyQuailifiedUrls(navigationSiteRoot, imageSiteRoot, dr["Description"].ToString());

                    //    if ((excerpt.Length > 0) && (excerpt != "<p>&#160;</p>"))
                    //    {
                    //        excerpt = excerpt
                    //            + ExcerptSuffix
                    //            + " <a href='"
                    //            + blogItemUrl + "'>" + MoreLinkText + "</a><div>&nbsp;</div>";
                    //    }
                    //    else
                    //    {
                    //        excerpt = UIHelper.CreateExcerpt(dr["Description"].ToString(), excerptLength, ExcerptSuffix)
                    //            + " <a href='"
                    //            + blogItemUrl + "'>" + MoreLinkText + "</a><div>&nbsp;</div>";
                    //    }

                    //    item.Description = excerpt;

                    //}

                    channel.AddItem(item);

                }
            }


            Response.Cache.SetExpires(DateTime.Now.AddMinutes(30));
            Response.Cache.SetCacheability(HttpCacheability.Public);
            //Response.Cache.VaryByParams["g"] = true;

            Response.ContentType = "application/xml";

            Encoding encoding = new UTF8Encoding();
            Response.ContentEncoding = encoding;

            using (XmlTextWriter xmlTextWriter = new XmlTextWriter(Response.OutputStream, encoding))
            {
                xmlTextWriter.Formatting = Formatting.Indented;

                //////////////////
                // style for RSS Feed viewed in browsers
                if (ConfigurationManager.AppSettings["RSSCSS"] != null)
                {
                    string rssCss = ConfigurationManager.AppSettings["RSSCSS"];
                    xmlTextWriter.WriteWhitespace(" ");
                    xmlTextWriter.WriteRaw("<?xml-stylesheet type=\"text/css\" href=\"" + cssBaseUrl + rssCss + "\" ?>");

                }

                if (ConfigurationManager.AppSettings["RSSXsl"] != null)
                {
                    string rssXsl = ConfigurationManager.AppSettings["RSSXsl"];
                    xmlTextWriter.WriteWhitespace(" ");
                    xmlTextWriter.WriteRaw("<?xml-stylesheet type=\"text/xsl\" href=\"" + cssBaseUrl + rssXsl + "\" ?>");

                }
                ///////////////////////////

                feed.Save(xmlTextWriter);


            }



        }

        private const int maxTweetLength = 140;

        private string GenerateTweetThisLink(string titleToTweet, string urlToTweet)
        {
            string format = "<a class='tweetthislink' title='Tweet This' href='{0}'><img src='" + imageSiteRoot + "/Data/SiteImages/tweetthis3.png' alt='Tweet This' /></a>";

            string twitterUrl;

            int maxTitleLength = maxTweetLength - (urlToTweet.Length + 1);
            if (maxTitleLength > 0)
            {
                if ((titleToTweet.Length > maxTitleLength) && (titleToTweet.Length > 3))
                {
                    titleToTweet = titleToTweet.Substring(0, (maxTitleLength - 3)) + "...";
                }

                twitterUrl = "http://twitter.com/home?status=" + Page.Server.UrlEncode(titleToTweet + " " + urlToTweet);

            }
            else
            {
                twitterUrl = "http://twitter.com/home?status=" + Page.Server.UrlEncode(urlToTweet);
            }

            return string.Format(CultureInfo.InvariantCulture, format, twitterUrl);
        }

        private string GenerateFacebookLikeButton(string urlToLike)
        {
            const string format = "<div class='fblikebutton'><iframe src='http://www.facebook.com/plugins/like.php?href={0}&amp;layout=standard&amp;show_faces=false&amp;width=450&amp;height=35&amp;action=like&amp;colorscheme=light' scrolling='no' frameborder='0' allowTransparency='true' style='border:none; overflow:hidden;width:450px; height:35px;'></iframe></div>";

            return string.Format(CultureInfo.InvariantCulture, format, Page.Server.UrlEncode(urlToLike));
        }

        private void LoadSettings()
        {
            pageID = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            securityBypassGuid = WebUtils.ParseGuidFromQueryString("g", securityBypassGuid);
            pageSettings = CacheHelper.GetCurrentPage();
            module = GetModule();

            if ((moduleId == -1) || (module == null)) { return; }

            bool bypassPageSecurity = false;

            if ((securityBypassGuid != Guid.Empty) && (securityBypassGuid == WebConfigSettings.InternalFeedSecurityBypassKey))
            {
                bypassPageSecurity = true;
            }

            if (
                (bypassPageSecurity)
                || (WebUser.IsInRoles(pageSettings.AuthorizedRoles))
                || (WebUser.IsInRoles(module.ViewRoles))
                )
            {
                canView = true;
            }

            if (!canView) { return; }

            if (WebConfigSettings.UseFoldersInsteadOfHostnamesForMultipleSites)
            {
                navigationSiteRoot = SiteUtils.GetNavigationSiteRoot();
                blogBaseUrl = navigationSiteRoot;
                imageSiteRoot = WebUtils.GetSiteRoot();
                cssBaseUrl = imageSiteRoot;
            }
            else
            {
                navigationSiteRoot = WebUtils.GetHostRoot();
                blogBaseUrl = SiteUtils.GetNavigationSiteRoot();
                imageSiteRoot = navigationSiteRoot;
                cssBaseUrl = WebUtils.GetSiteRoot();

            }

            moduleSettings = ModuleSettings.GetModuleSettings(moduleId);

            feedIsDisabled = WebUtils.ParseBoolFromHashtable(moduleSettings, "BlogDisableFeedSetting", feedIsDisabled);

            if (feedIsDisabled) { canView = false; return; }

            addSignature = WebUtils.ParseBoolFromHashtable(moduleSettings, "RSSAddSignature", false);

            addTweetThisToFeed = WebUtils.ParseBoolFromHashtable(moduleSettings, "AddTweetThisToFeed", addTweetThisToFeed);

            addFacebookLikeToFeed = WebUtils.ParseBoolFromHashtable(moduleSettings, "AddFacebookLikeToFeed", addFacebookLikeToFeed);

            ShowPostAuthorSetting = WebUtils.ParseBoolFromHashtable(moduleSettings, "ShowPostAuthorSetting", ShowPostAuthorSetting);


            addCommentsLink = WebUtils.ParseBoolFromHashtable(moduleSettings, "RSSAddCommentsLink", false);

            bool BlogAllowComments = WebUtils.ParseBoolFromHashtable(moduleSettings, "BlogAllowComments", false);
            if (!BlogAllowComments) { addCommentsLink = false; }

            useExcerptInFeed = WebUtils.ParseBoolFromHashtable(moduleSettings, "UseExcerptInFeedSetting", useExcerptInFeed);

            excerptLength = WebUtils.ParseInt32FromHashtable(moduleSettings, "BlogExcerptLengthSetting", excerptLength);

            if (moduleSettings.Contains("BlogExcerptSuffixSetting"))
            {
                ExcerptSuffix = moduleSettings["BlogExcerptSuffixSetting"].ToString();
            }

            if (moduleSettings.Contains("BlogMoreLinkText"))
            {
                MoreLinkText = moduleSettings["BlogMoreLinkText"].ToString();
            }

            if (moduleSettings.Contains("BlogDescriptionSetting"))
            {
                channelDescription = moduleSettings["BlogDescriptionSetting"].ToString();
            }

            if (moduleSettings.Contains("BlogCopyrightSetting"))
            {
                copyright = moduleSettings["BlogCopyrightSetting"].ToString();
            }

            if (moduleSettings.Contains("BlogAuthorEmailSetting"))
            {
                authorEmail = moduleSettings["BlogAuthorEmailSetting"].ToString().Replace("@", "@nospam");
            }


            timeToLive = WebUtils.ParseInt32FromHashtable(moduleSettings, "BlogRSSCacheTimeSetting", timeToLive);

            if (moduleSettings.Contains("ShowImageOnRSSSetting"))
            {
                showImageOnRSSSetting = WebUtils.ParseBoolFromHashtable(moduleSettings, "ShowImageOnRSSSetting", showImageOnRSSSetting);
            }

        }




        private string FormatBlogUrl(string itemUrl, int itemId)
        {
            //if (itemUrl.Length > 0)
            //    return blogBaseUrl + itemUrl.Replace("~", string.Empty);
            if (itemUrl.Length > 0)
                return WebUtils.ResolveServerUrl(blogBaseUrl + itemUrl.Replace("~", string.Empty));

            return blogBaseUrl + "/Article/ViewPost.aspx?pageid=" + pageID.ToInvariantString()
                + "&ItemID=" + itemId.ToInvariantString()
                + "&mid=" + moduleId.ToInvariantString();

        }

        private Module GetModule()
        {

            Module m = null;
            if (pageSettings != null)
            {
                foreach (Module mod in pageSettings.Modules)
                {
                    if (mod.ModuleId == moduleId)
                        m = mod;
                }
            }

            if (m == null) return null;
            if (m.ModuleId == -1) return null;

            if (m.ControlSource.ToLower().Contains("articlemodule.ascx"))
            {
                return m;
            }

            return null;
        }



        private void RenderError(string message)
        {

            Response.Write(message);
            Response.End();
        }

        //legacy
        //private void RenderRss(int moduleId)
        //{
        //    /*

        //    For more info on RSS 2.0
        //    http://www.feedvalidator.org/docs/rss2.html

        //    Fields not implemented yet:
        //    <blogChannel:blogRoll>http://radio.weblogs.com/0001015/userland/scriptingNewsLeftLinks.opml</blogChannel:blogRoll>
        //    <blogChannel:mySubscriptions>http://radio.weblogs.com/0001015/gems/mySubscriptions.opml</blogChannel:mySubscriptions>
        //    <blogChannel:blink>http://diveintomark.org/</blogChannel:blink>
        //    <lastBuildDate>Mon, 30 Sep 2002 11:00:00 GMT</lastBuildDate>
        //    <docs>http://backend.userland.com/rss</docs>

        //    */



        //    Response.Cache.SetExpires(DateTime.Now.AddMinutes(30));
        //    Response.Cache.SetCacheability(HttpCacheability.Public);
        //    //Response.Cache.VaryByParams["g"] = true;

        //    Response.ContentType = "application/xml";

        //    moduleSettings = ModuleSettings.GetModuleSettings(moduleId);

        //    addSignature = WebUtils.ParseBoolFromHashtable(
        //        moduleSettings, "RSSAddSignature", false);

        //    ShowPostAuthorSetting = WebUtils.ParseBoolFromHashtable(
        //        moduleSettings, "ShowPostAuthorSetting", ShowPostAuthorSetting);

        //    addCommentsLink = WebUtils.ParseBoolFromHashtable(
        //        moduleSettings, "RSSAddCommentsLink", false);

        //    feedIsDisabled = WebUtils.ParseBoolFromHashtable(
        //        moduleSettings, "BlogDisableFeedSetting", feedIsDisabled);

        //    useExcerptInFeed = WebUtils.ParseBoolFromHashtable(
        //        moduleSettings, "UseExcerptInFeedSetting", useExcerptInFeed);

        //    excerptLength = WebUtils.ParseInt32FromHashtable(
        //        moduleSettings, "BlogExcerptLengthSetting", excerptLength);

        //    if (moduleSettings.Contains("BlogExcerptSuffixSetting"))
        //    {
        //        ExcerptSuffix = moduleSettings["BlogExcerptSuffixSetting"].ToString();
        //    }

        //    if (moduleSettings.Contains("BlogMoreLinkText"))
        //    {
        //        MoreLinkText = moduleSettings["BlogMoreLinkText"].ToString();
        //    }


        //    //


        //    //siteRoot = WebUtils.GetSiteRoot();

        //    Encoding encoding = new UTF8Encoding();
        //    Response.ContentEncoding = encoding;

        //    using (XmlTextWriter xmlTextWriter = new XmlTextWriter(Response.OutputStream, encoding))
        //    {
        //        xmlTextWriter.Formatting = Formatting.Indented;

        //        xmlTextWriter.WriteStartDocument();


        //        //////////////////
        //        // style for RSS Feed viewed in browsers
        //        if (ConfigurationManager.AppSettings["RSSCSS"] != null)
        //        {
        //            string rssCss = ConfigurationManager.AppSettings["RSSCSS"].ToString();
        //            xmlTextWriter.WriteWhitespace(" ");
        //            xmlTextWriter.WriteRaw("<?xml-stylesheet type=\"text/css\" href=\"" + cssBaseUrl + rssCss + "\" ?>");

        //        }

        //        if (ConfigurationManager.AppSettings["RSSXsl"] != null)
        //        {
        //            string rssXsl = ConfigurationManager.AppSettings["RSSXsl"].ToString();
        //            xmlTextWriter.WriteWhitespace(" ");
        //            xmlTextWriter.WriteRaw("<?xml-stylesheet type=\"text/xsl\" href=\"" + cssBaseUrl + rssXsl + "\" ?>");

        //        }
        //        ///////////////////////////


        //        xmlTextWriter.WriteComment("RSS generated by mojoPortal Blog Module V 1.0 on "
        //            + DateTime.Now.ToLongDateString());

        //        xmlTextWriter.WriteStartElement("rss");

        //        xmlTextWriter.WriteStartAttribute("version", "");
        //        xmlTextWriter.WriteString("2.0");
        //        xmlTextWriter.WriteEndAttribute();

        //        xmlTextWriter.WriteStartElement("channel");
        //        /*  
        //            RSS 2.0
        //            Required elements for channel are title link and description
        //        */

        //        xmlTextWriter.WriteStartElement("title");
        //        xmlTextWriter.WriteString(module.ModuleTitle);
        //        xmlTextWriter.WriteEndElement();

        //        // this assumes a valid pageid passed in url
        //        string blogUrl = SiteUtils.GetCurrentPageUrl();

        //        xmlTextWriter.WriteStartElement("link");
        //        xmlTextWriter.WriteString(blogUrl);
        //        xmlTextWriter.WriteEndElement();

        //        xmlTextWriter.WriteStartElement("description");
        //        xmlTextWriter.WriteString(moduleSettings["BlogDescriptionSetting"].ToString());
        //        xmlTextWriter.WriteEndElement();

        //        xmlTextWriter.WriteStartElement("copyright");
        //        xmlTextWriter.WriteString(moduleSettings["BlogCopyrightSetting"].ToString());
        //        xmlTextWriter.WriteEndElement();

        //        // begin optional RSS 2.0 fields

        //        //ttl = time to live in minutes, how long a channel can be cached before refreshing from the source
        //        xmlTextWriter.WriteStartElement("ttl");
        //        xmlTextWriter.WriteString(moduleSettings["BlogRSSCacheTimeSetting"].ToString());
        //        xmlTextWriter.WriteEndElement();

        //        //protection from scrapers wnating to add you to the spam list
        //        string authorEmail = moduleSettings["BlogAuthorEmailSetting"].ToString().Replace("@", "@nospam");

        //        xmlTextWriter.WriteStartElement("managingEditor");
        //        xmlTextWriter.WriteString(authorEmail);
        //        xmlTextWriter.WriteEndElement();

        //        xmlTextWriter.WriteStartElement("generator");
        //        xmlTextWriter.WriteString("mojoPortal Blog Module V 1.0");
        //        xmlTextWriter.WriteEndElement();

        //        // check if the user has page view permission

        //        if (
        //            (!feedIsDisabled)
        //            && (pageSettings.ContainsModule(moduleId))
        //            && ((bypassPageSecurity) || (WebUser.IsInRoles(pageSettings.AuthorizedRoles)))
        //            )
        //        {
        //            RenderItems(xmlTextWriter);
        //        }
        //        else
        //        {
        //            //beginning of blog entry
        //            xmlTextWriter.WriteStartElement("item");
        //            xmlTextWriter.WriteStartElement("title");
        //            xmlTextWriter.WriteString("this feed is not available");
        //            xmlTextWriter.WriteEndElement();

        //            xmlTextWriter.WriteStartElement("link");
        //            xmlTextWriter.WriteString(navigationSiteRoot);
        //            xmlTextWriter.WriteEndElement();

        //            xmlTextWriter.WriteStartElement("pubDate");
        //            xmlTextWriter.WriteString(DateTime.UtcNow.ToString("r"));
        //            xmlTextWriter.WriteEndElement();

        //            xmlTextWriter.WriteStartElement("guid");
        //            xmlTextWriter.WriteString(navigationSiteRoot);
        //            xmlTextWriter.WriteEndElement();

        //            //end blog entry
        //            xmlTextWriter.WriteEndElement();

        //        }


        //        //end of document
        //        xmlTextWriter.WriteEndElement();

        //    }



        //}

        //private void RenderItems(XmlTextWriter xmlTextWriter)
        //{
        //    string blogCommentLabel = ConfigurationManager.AppSettings["BlogCommentCountLabel"];

        //    using (IDataReader dr = Blog.GetBlogs(moduleId, DateTime.UtcNow))
        //    {
        //        //write channel items
        //        while (dr.Read())
        //        {
        //            string inFeed = dr["IncludeInFeed"].ToString();
        //            if (inFeed == "True" || inFeed == "1")
        //            {
        //                //beginning of blog entry
        //                xmlTextWriter.WriteStartElement("item");

        //                string blogItemUrl = FormatBlogUrl(dr["ItemUrl"].ToString(), Convert.ToInt32(dr["ItemID"]));

        //                /*  
        //                RSS 2.0
        //                All elements of an item are optional, however at least one of title or description 
        //                must be present.
        //                */

        //                xmlTextWriter.WriteStartElement("title");
        //                xmlTextWriter.WriteString(dr["Heading"].ToString());
        //                xmlTextWriter.WriteEndElement();

        //                xmlTextWriter.WriteStartElement("link");
        //                xmlTextWriter.WriteString(blogItemUrl);
        //                xmlTextWriter.WriteEndElement();

        //                xmlTextWriter.WriteStartElement("pubDate");
        //                xmlTextWriter.WriteString(Convert.ToDateTime(dr["StartDate"]).ToString("r"));
        //                xmlTextWriter.WriteEndElement();

        //                xmlTextWriter.WriteStartElement("guid");
        //                xmlTextWriter.WriteString(blogItemUrl);
        //                xmlTextWriter.WriteEndElement();

        //                if (ShowPostAuthorSetting)
        //                {
        //                    xmlTextWriter.WriteStartElement("author");
        //                    // techically this is supposed to be an email address
        //                    // but wouldn't that lead to a lot of spam?
        //                    xmlTextWriter.WriteString(dr["Name"].ToString());
        //                    xmlTextWriter.WriteEndElement();


        //                }

        //                xmlTextWriter.WriteStartElement("comments");
        //                xmlTextWriter.WriteString(blogItemUrl);
        //                xmlTextWriter.WriteEndElement();

        //                string signature = string.Empty;

        //                if (addSignature)
        //                {
        //                    signature = "<br /><a href='" + imageSiteRoot + "'>"
        //                        + dr["Name"].ToString() + "</a>";

        //                }
        //                //else
        //                //{
        //                //    signature = "<br /><br />";

        //                //}

        //                if (addCommentsLink)
        //                {
        //                    signature += "&nbsp;&nbsp;"
        //                        + "<a href='" + blogItemUrl + "'>" + blogCommentLabel + "...</a>";
        //                }


        //                string blogPost = SiteUtils.ChangeRelativeUrlsToFullyQuailifiedUrls(navigationSiteRoot, imageSiteRoot, dr["Description"].ToString());

        //                if ((!useExcerptInFeed) || (blogPost.Length <= excerptLength))
        //                {
        //                    xmlTextWriter.WriteStartElement("description");
        //                    xmlTextWriter.WriteCData(blogPost + signature);
        //                    xmlTextWriter.WriteEndElement();
        //                }
        //                else
        //                {
        //                    string excerpt = SiteUtils.ChangeRelativeUrlsToFullyQuailifiedUrls(navigationSiteRoot, imageSiteRoot,dr["Abstract"].ToString());

        //                    if ((excerpt.Length > 0) && (excerpt != "<p>&#160;</p>"))
        //                    {
        //                        excerpt = excerpt
        //                            + ExcerptSuffix
        //                            + " <a href='"
        //                            + blogItemUrl + "'>" + MoreLinkText + "</a><div>&nbsp;</div>";
        //                    }
        //                    else
        //                    {
        //                        excerpt = UIHelper.CreateExcerpt(dr["Description"].ToString(), excerptLength, ExcerptSuffix)
        //                            + " <a href='"
        //                            + blogItemUrl + "'>" + MoreLinkText + "</a><div>&nbsp;</div>"; ;
        //                    }

        //                    xmlTextWriter.WriteStartElement("description");
        //                    xmlTextWriter.WriteCData(excerpt);
        //                    xmlTextWriter.WriteEndElement();

        //                }


        //                //end blog entry
        //                xmlTextWriter.WriteEndElement();

        //            }
        //        }
        //    }

        //}

    }
}

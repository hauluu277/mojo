using System;
using System.Web;
using System.Web.Services;
using System.Text;
using System.Xml;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.Framework;
using Resources;
using mojoPortal.Web;

namespace MojoFeature.UI
{
    /// <summary>
    /// A service to suggest a friendly url for a blog post
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class BlogUrlSuggestService : IHttpHandler
    {
        private Double timeOffset;
        protected TimeZoneInfo timeZone;

        public void ProcessRequest(HttpContext context)
        {
            timeOffset = SiteUtils.GetUserTimeOffset();
            timeZone = SiteUtils.GetUserTimeZone();
            SendResponse(context);
        }
        public string GetParam(string parameter, string key)
        {
            string desiredValue = string.Empty;
            foreach (string item in parameter.Split('&'))
            {
                string[] parts = item.Replace("?", "").Split('=');
                if (parts[0] == key)
                {
                    desiredValue = parts[1];
                    break;
                }
            }
            return desiredValue;
        }

        public string GetParamFist(string paramter)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(paramter))
            {
                result = paramter.Split('&')[0];
            }
            return result;
        }

        private void SendResponse(HttpContext context)
        {
            if (context == null) return;

            context.Response.ContentType = "application/xml";
            Encoding encoding = new UTF8Encoding();

            XmlTextWriter xmlTextWriter = new XmlTextWriter(context.Response.OutputStream, encoding) { Formatting = Formatting.Indented };

            xmlTextWriter.WriteStartDocument();
            xmlTextWriter.WriteStartElement("DATA");
            string warning = string.Empty;

            if (context.Request.Params.Get("pn") != null)
            {
                String pageName = context.Request.Params.Get("pn");

                string category = GetParam(pageName, "cat");
                string tit = GetParamFist(pageName);

                if (WebConfigSettings.AppendDateToBlogUrls)
                {
                    if (timeZone != null)
                    {
                        pageName += "-" + DateTime.UtcNow.ToLocalTime(timeZone).ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        pageName += "-" + DateTime.UtcNow.AddHours(timeOffset).ToString("yyyy-MM-dd");
                    }

                }

                SiteSettings siteSettings = CacheHelper.GetCurrentSiteSettings();

                if (siteSettings != null)
                {
                    String friendlyUrl = SiteUtils.SuggestFriendlyUrl(tit, siteSettings);

                    if (WebPageInfo.IsPhysicalWebPage("~/" + friendlyUrl))
                    {
                        warning = ArticleResources.BlogUrlConflictWithPhysicalPageError;
                    }

                    xmlTextWriter.WriteStartElement("fn");

                    xmlTextWriter.WriteString("~/" + friendlyUrl);

                    xmlTextWriter.WriteEndElement();

                    xmlTextWriter.WriteStartElement("wn");
                    xmlTextWriter.WriteString(warning);
                    xmlTextWriter.WriteEndElement();

                }

            }
            else
            {
                if (context.Request.Params.Get("cu") != null)
                {
                    String enteredUrl = context.Server.UrlDecode(context.Request.Params.Get("cu"));
                    if (WebPageInfo.IsPhysicalWebPage(enteredUrl))
                    {
                        warning = ArticleResources.BlogUrlConflictWithPhysicalPageError;
                    }

                    xmlTextWriter.WriteStartElement("wn");
                    xmlTextWriter.WriteString(warning);
                    xmlTextWriter.WriteEndElement();
                }


            }

            //end of document
            xmlTextWriter.WriteEndElement();
            xmlTextWriter.WriteEndDocument();

            xmlTextWriter.Close();
            //Response.End();


        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

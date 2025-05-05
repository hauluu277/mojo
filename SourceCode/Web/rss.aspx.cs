using mojoPortal.Business;
using mojoPortal.Service.Business;
using mojoPortal.Web.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.ServiceModel.Syndication;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace mojoPortal.Web
{
    public partial class rss : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            var data = createXML();
            string xml = System.Text.UTF8Encoding.UTF8.GetString(data);

            //Clear page
            Response.ContentType = "text/xml";
            Response.Write(xml);
            Response.End();
        }
        public byte[] createXML()
        {
            var id = WebUtils.ParseInt32FromQueryString("id", -1);
            md_ArticlesBusiness articlesBusiness = new md_ArticlesBusiness(new mojoportal.Service.UoW.UnitOfWork());

            var lstArticle = articlesBusiness.GetByCategory(id, true);
            var catData = new CoreCategory(id);

            var url = new Uri(ConfigurationManager.AppSettings["domain"] + catData.Description);
            var feed = new SyndicationFeed(catData.Name, catData.Sumary, url, catData.ItemID.ToString(), DateTime.Now);

            var items = new List<SyndicationItem>();
            foreach (var item in lstArticle)
            {
                var itemUrl = new Uri(ConfigurationManager.AppSettings["domain"] + item.ItemUrl.Replace("~/", string.Empty));
                var title = item.Title;
                var description = item.Summary;
                items.Add(new SyndicationItem(title, description, itemUrl, item.ItemID.ToString(), item.CreatedDate.GetValueOrDefault(DateTime.Now)));
            }
            feed.Items = items;
            var settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                NewLineHandling = NewLineHandling.Entitize,
                NewLineOnAttributes = true,
                Indent = true
            };
            using (var stream = new MemoryStream())
            {
                using (var xmlWriter = XmlWriter.Create(stream, settings))
                {
                    var rssFormatter = new Rss20FeedFormatter(feed, false);
                    rssFormatter.WriteTo(xmlWriter);
                    xmlWriter.Flush();
                }
                return stream.ToArray();
            };
        }
    }
}
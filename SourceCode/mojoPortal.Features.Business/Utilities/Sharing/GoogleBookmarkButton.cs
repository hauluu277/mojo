using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.UI
{
    /// <summary>
    /// a simple control to implement a Facebook share button
    /// </summary>
    public class GoogleBookmarkButton : WebControl
    {

        protected override void Render(HtmlTextWriter writer)
        {
            //base.Render(writer);
            if (HttpContext.Current == null) { return; }
            StringBuilder bookmarkUrl = new StringBuilder(string.Format("http://google.pt/bookmarks/mark?op=edit&bkmk={0}&title={1}",
                GetShortUrl(url), title));
            writer.Write("<a title=\"" + altImg + "\" onclick=\"window.open(this.href, '', 'resizable=no,status=no,location=no,toolbar=no,menubar=no,fullscreen=no,scrollbars=no,dependent=no,width=700,height=500');return false;\" href=\"" + bookmarkUrl + "\" ><img src=\"" + imgUrl + "\" alt=" + altImg + " /></a>");
        }

        private string imgUrl = string.Empty;
        public string ImgUrl
        {
            get { return imgUrl; }
            set { imgUrl = value; }
        }

        private string altImg = "Google Bookmark";
        public string AltImg
        {
            get { return altImg; }
            set { altImg = value; }
        }

        private string url = string.Empty;
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        private string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }


        public static string GetShortUrl(string urlToShorten)
        {
            try
            {
                if (!urlToShorten.ToLower().StartsWith("http") && !urlToShorten.ToLower().StartsWith("ftp"))
                {
                    urlToShorten = "http://" + urlToShorten;
                }
                var request = WebRequest.Create("http://tinyurl.com/api-create.php?url=" + urlToShorten);
                var res = request.GetResponse();
                string shortUrl;
                // ReSharper disable PossibleNullReferenceException
                using (var reader = new StreamReader(res.GetResponseStream()))
                // ReSharper restore PossibleNullReferenceException
                {
                    shortUrl = reader.ReadToEnd();
                }
                return shortUrl;
            }
            catch (Exception)
            {
                return urlToShorten;
            }

        }
    }
}
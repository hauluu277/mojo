using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.UI
{
    /// <summary>
    /// a simple control to implement a Facebook share button
    /// </summary>
    public class FacebookShareButton : WebControl
    {

        protected override void Render(HtmlTextWriter writer)
        {
            //base.Render(writer);
            if (HttpContext.Current == null) { return; }
            writer.Write("<a title=\"" + altImg + "\" onclick=\"window.open(this.href, '', 'resizable=no,status=no,location=no,toolbar=no,menubar=no,fullscreen=no,scrollbars=no,dependent=no,width=500,height=400');return false;\" href=\"http://www.facebook.com/sharer.php?u=" + url + "&t=" + title + "\" ><img src=\"" + imgUrl + "\" alt=" + altImg + " /></a>");
        }

        private string imgUrl = string.Empty;
        public string ImgUrl
        {
            get { return imgUrl; }
            set { imgUrl = value; }
        }

        private string altImg = "Share on Facebook";
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
    }
}
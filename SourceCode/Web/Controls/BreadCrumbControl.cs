using mojoPortal.Business.WebHelpers.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.UI
{
    public class BreadCrumbControl : WebControl
    {

        private bool showHomePage = true;
        public bool ShowHomePage
        {
            get { return showHomePage; }
            set { showHomePage = value; }
        }
        public InfoLink InfoLink_1 { get; set; }
        public InfoLink InfoLink_2 { get; set; }
        public InfoLink InfoLink_3 { get; set; }
        public InfoLink InfoLink_4 { get; set; }

        protected override void Render(HtmlTextWriter writer)
        {
            if (HttpContext.Current == null)
            {
                writer.Write("[" + this.ID + "]");
                return;
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<ul class='breadcrumb'>");
            if (showHomePage)
            {
                stringBuilder.Append("<li>");
                stringBuilder.Append("<a href='/' title='Trang chủ'>Trang chủ</a>");
                stringBuilder.Append("</li>");
            }
            if (InfoLink_1 != null)
            {
                stringBuilder.Append("<li>");
                stringBuilder.Append("<a href='" + InfoLink_1.UrlLink + "'  class='" + (InfoLink_1.ActiveLink ? "active" : string.Empty) + "' title='" + InfoLink_1.NameLink + "'>" + InfoLink_1.NameLink + "</a>");
                stringBuilder.Append("</li>");
            }
            if (InfoLink_2 != null)
            {
                stringBuilder.Append("<li>");
                stringBuilder.Append("<a href='" + InfoLink_2.UrlLink + "'  class='" + (InfoLink_2.ActiveLink ? "active" : string.Empty) + "' title='" + InfoLink_2.NameLink + "'>" + InfoLink_2.NameLink + "</a>");
                stringBuilder.Append("</li>");
            }
            if (InfoLink_3 != null)
            {
                stringBuilder.Append("<li>");
                stringBuilder.Append("<a href='" + InfoLink_3.UrlLink + "'  class='active' title='" + (InfoLink_3.ActiveLink ? "active" : string.Empty) + "'>" + InfoLink_3.NameLink + "</a>");
                stringBuilder.Append("</li>");
            }
            if (InfoLink_4 != null)
            {
                stringBuilder.Append("<li>");
                stringBuilder.Append("<a href='" + InfoLink_4.UrlLink + "'  class='active' title='" + (InfoLink_4.ActiveLink ? "active" : string.Empty) + "'>" + InfoLink_4.NameLink + "</a>");
                stringBuilder.Append("</li>");
            }

            stringBuilder.Append("</ul>");
            writer.Write(stringBuilder.ToString());
        }

    }
   
}
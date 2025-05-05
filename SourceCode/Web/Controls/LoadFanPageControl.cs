using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.UI
{
    public class LoadFanPageControl : WebControl
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EnableViewState = false;
        }
        protected override void Render(HtmlTextWriter writer)
        {
            if (HttpContext.Current == null)
            {
                return;
            }

            DoRender(writer);

        }

        private void DoRender(HtmlTextWriter writer)
        {
            SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(siteSetting.FanPageIframe);
            writer.Write(stringBuilder.ToString());

        }
    }
}
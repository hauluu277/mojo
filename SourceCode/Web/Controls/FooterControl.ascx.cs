using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.Controls
{
    public partial class FooterControl : System.Web.UI.UserControl
    {
        private readonly SiteSettings siteSettings = CacheHelper.GetCurrentSiteSettings();
        private string siteRoot = SiteUtils.GetNavigationSiteRoot();
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += new EventHandler(Page_Load);
        }
     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var cc = new SiteSettings(siteSettings.SiteId);
                literFooter.Text = cc.Footer;
            }
        }
    }
}
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.Controls.Common
{
    public partial class ConfigSystem : System.Web.UI.UserControl
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
                var sites = new SiteSettings(siteSettings.SiteId);
                StringBuilder text = new StringBuilder();
                text.Append("<style type='text/css'>");

                var backgroundColorMenu = CoreCategory.GetByCode(sites.SiteId, ConfigurationManager.AppSettings["BACKGROUND-COLOR-MENU"]);
                if (backgroundColorMenu.ItemID > 0)
                {
                    text.Append("header{background:" + backgroundColorMenu.Color + " !important;}");
                    text.Append(".ButtonSearch #ctl00_search_button{background-color:" + backgroundColorMenu.Code + " !important;}");
                }

                var backgroundHeader = CoreCategory.GetByCode(sites.SiteId, ConfigurationManager.AppSettings["BACKGROUND-IMAGE-HEADER"]);
                if (backgroundHeader.ItemID > 0)
                {
                    text.Append(".Banner-School{background-image:url('" + backgroundHeader.PathIMG + "') !important;}");
                }

                text.Append("</style>");
                literConfig.Text = text.ToString();
            }

        }
    }
}
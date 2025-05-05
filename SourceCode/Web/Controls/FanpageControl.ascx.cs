using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.Controls
{
    public partial class FanpageControl : System.Web.UI.UserControl
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
                if (!string.IsNullOrEmpty(sites.FanPageIframe))
                {
                    //1. Id App facebook
                    //2. Link fanpage facebook

                    var lstSetting = sites.FanPageIframe.Split(',');
                    if (lstSetting != null && lstSetting.Count() == 2)
                    {
                        //string idApp = lstSetting[0];
                        string linkFanPage = lstSetting[0];
                        bool isShow = lstSetting[1].ToBooleanOrFalse();
                        if (isShow)
                        {
                            string pathToHTMLFile = Server.MapPath("/Controls/fanpage.html");
                            string htmlString = System.IO.File.ReadAllText(pathToHTMLFile);
                            using (FileStream fs = File.Open(pathToHTMLFile, FileMode.Open, FileAccess.ReadWrite))
                            {
                                using (StreamReader sr = new StreamReader(fs))
                                {
                                    htmlString = sr.ReadToEnd();
                                }
                                fs.Close();
                            }
                            htmlString = htmlString.Replace("{{appId}}", System.Configuration.ConfigurationManager.AppSettings["AppId"])
                                .Replace("{{link_fanpage}}", linkFanPage);
                            literFanpage.Text = htmlString;
                        }
                    }
                }
            }

        }
    }
}
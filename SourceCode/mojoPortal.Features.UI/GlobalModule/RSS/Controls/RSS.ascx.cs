using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Features.UI.GlobalModule.ThongKeTruyCap;
using mojoPortal.Model.Data;
using mojoPortal.Service.Business;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Newtonsoft.Json;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace RSSFeature.UI
{
    public partial class RSS : UserControl
    {
        #region Properties
        private int moduleId = -1;
        public SiteSettings siteSettings;
        protected List<string> lstRss;

        protected int langId = 1;

        public int ModuleId
        {
            get { return moduleId; }
            set { moduleId = value; }
        }
        #endregion

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var lang = CultureInfo.CurrentCulture.Name;
            langId = lang == "vi-VN" ? LanguageConstant.VN : LanguageConstant.EN;
            LoadSettings();
            LoadLinkRSS();
            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }

        private void PopulateControls()
        {

        }

        protected virtual void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            //config = new ThongKeTruyCapConfiguration(getModuleSettings);
            siteSettings = CacheHelper.GetCurrentSiteSettings();
        }

        protected virtual void LoadLinkRSS()
        {
            //var feed = new SyndicationFeed("Tiêu đề", "Mô tả", new Uri(ConfigurationManager.AppSettings["domain"] + "/rss"), "RSS", DateTime.Now);
            //feed.Copyright = new TextSyndicationContent($"{DateTime.Now.Year} Bến Tre");

            //var category = Category.GetAllCatagory(1);
            //var lstUrlRSSCategory = new List<SyndicationItem>();
            var sites = new SiteSettings(1);
            var root = CoreCategory.GetChildren(sites.ArticleCategory);
            var lstLinkRSS = new List<ListItem>();

            foreach (var item in root)
            {
                var dataLink = new ListItem()
                {
                    Value = ConfigurationManager.AppSettings["domain"] + "rss.aspx?id=" + item.ItemID,
                    Text = item.Name
                };
                lstLinkRSS.Add(dataLink);
            }
            rptLinkRss.DataSource = lstLinkRSS;
            rptLinkRss.DataBind();
        }
    }
}
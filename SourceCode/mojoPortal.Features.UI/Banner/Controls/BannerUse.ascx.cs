// Author:					HiNet
// Created:					2015-3-13
// Last Modified:			2015-3-13
// 
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)  
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using mojoPortal.Web.UI;
using mojoPortal.Features;
using log4net;
using mojoPortal.Business;
using Resources;
using mojoPortal.Business.WebHelpers;

namespace BannerFeature.UI
{

    public partial class BannerUse : UserControl
    {
        // FeatureGuid 2530febc-09bf-4d47-a0ca-26d22bf95612
        private int pageId = -1;
        private int moduleId = -1;
        private int siteId = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        private bool isSlideTop = false;
        private bool isSlideBottom = false;
        readonly SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
        private BannerConfiguration config = new BannerConfiguration();
        public string SiteRoot
        {
            get { return siteRoot; }
            set { siteRoot = value; }
        }
        public int SiteId
        {
            get { return siteId; }
            set { siteId = value; }
        }
        public string ImageSiteRoot
        {
            get { return imageSiteRoot; }
            set { imageSiteRoot = value; }
        }
        public int PageId
        {
            get { return pageId; }
            set { pageId = value; }
        }

        public int ModuleId
        {
            get { return moduleId; }
            set { moduleId = value; }
        }
        public bool IsEditable { get; set; }
        #region OnInit

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(Page_Load);

        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            LoadSettings();
            PopulateLabels();
            if (!IsPostBack)
            {
                PopulateControls();
            }

        }

        private void PopulateControls()
        {
            PopulateBanner();
        }

        private void PopulateBanner()
        {
            List<Banner> banner = Banner.GetBannerByConfig(siteSetting.SiteId, moduleId, pageId, config.BannerNumber);
            rptbanner.DataSource = banner;
            rptbanner.DataBind();
        }
        private void PopulateLabels()
        {

        }
        public string BuildFlashObject(bool type, string filepath)
        {
            if (type == false)
            {
                //string obj_format = "<object classid='clsid:d27cdb6e-ae6d-11cf-96b8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0' width='200' height='200'><param name='movie' value='~/Data/Images/Banner/{0}' /><embed width='200' height='200' src='~/Data/Images/Banner/{0}' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer'></embed></object>";
                string obj_format = "<embed width='100' height='100' align='middle' quality='high' wmode='opaque' allowscriptaccess='always' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer' src='{0}/Data/Images/Banner/{1}'></embed>";
                //<object data='<%#"~/Data/Images/Banner/"+Eval("FilePath") %>'></object>
                return string.Format(obj_format, SiteRoot, filepath);
            }
            return string.Empty;
        }
        public string Target(bool target)
        {
            string blank = string.Empty;
            if (target == true)
            {
                return blank = "_blank";
            }
            return string.Empty;
        }
        private void LoadSettings()
        {
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new BannerConfiguration(moduleSettings);
        }


    }
}
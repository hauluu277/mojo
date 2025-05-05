// Author:					HiNet
// Created:					2015-3-30
// Last Modified:			2015-3-30
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
using log4net;
using mojoPortal.Business;
using mojoPortal.Features;
using Resources;
using mojoPortal.Business.WebHelpers;
using System.Text.RegularExpressions;

namespace DocumentFeature.UI
{

    public partial class SlideListModule : UserControl
    {
        // FeatureGuid d4b1ad4b-6e07-4e9d-8970-c2ce6ef022cb
        protected DocumentConfiguration config = new DocumentConfiguration();

        private int pageId = -1;
        private int moduleId = -1;
        private int number = -1;
        private string siteRoot = string.Empty;
        private SiteSettings siteSettings;
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
        public string SiteRoot
        {
            get { return siteRoot; }
            set { siteRoot = value; }
        }
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
            PopulateTopSilde();
        }

        private void PopulateTopSilde()
        {
            List<Documentation> slide = Documentation.GetTopSlide(siteSettings.SiteId, config.DocumentSildeNumber);
            rptDocSlide.DataSource = slide;
            rptDocSlide.DataBind();
        }
        private void PopulateLabels()
        {

        }

        protected string formartUrl(int pageId, int moduleId, int itemId)
        {
            string Url = siteSettings.SiteRoot + "/Document/Detail.aspx?pageid=" + pageId + "&mid=" + moduleId + "&item=" + itemId;
            return Url;
        }
        protected string formatContent(string Content)
        {
            if (!string.IsNullOrEmpty(Content))
            {
                return Regex.Replace(Content, "<(.|\n)*?>", string.Empty);
            }
            else
            {
                return string.Empty;
            }
        }
        private void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new DocumentConfiguration(getModuleSettings);
            siteSettings = CacheHelper.GetCurrentSiteSettings();
        }


    }
}
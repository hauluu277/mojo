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
using log4net;
using mojoPortal.Business;
using Resources;

namespace BannerFeature.UI
{

    public partial class BannerUseModule : SiteModuleControl
    {
        // FeatureGuid 4206e603-6e8f-44a8-8bd0-a5f121ef53b3
        
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
            PopulateControls();

        }

        private void PopulateControls()
        {
            BannerUse.ModuleId = ModuleId;
            BannerUse.PageId = PageId;
            BannerUse.SiteId = SiteId;
            BannerUse.SiteRoot = SiteRoot;
            BannerUse.IsEditable = IsEditable;
            BannerUse.ImageSiteRoot = ImageSiteRoot;
        }


        private void PopulateLabels()
        {

        }

        private void LoadSettings()
        {


        }


    }
}
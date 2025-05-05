// Author:					HiNet
// Created:					2014-9-24
// Last Modified:			2014-9-24
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
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;

namespace ArticleFeature.UI
{

    public partial class ArticleHotModule : SiteModuleControl
    {
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        readonly SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
        ArticleConfiguration config = new ArticleConfiguration();
        // FeatureGuid 8169280b-5720-4301-965a-20a09701b9f7

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
            pnlOuterWrap.CssClass = config.ModuleHotCssCustome;
        }

        private void PopulateControls()
        {
            TitleControl.Visible = false;
            if (siteUser != null)
            {
                if (siteUser.IsInRoles("Admins"))
                {
                    TitleControl.Visible = true;
                }
            }
            TitleControl.ShowEditLinkOverride = false;
            HotList.PageId = PageId;
            HotList.ModuleId = ModuleId;
            HotList.SiteRoot = SiteRoot;
            HotList.ImageSiteRoot = ImageSiteRoot;
            HotList.IsEditable = IsEditable;
            HotList.SiteId = siteSetting.SiteId;
        }


        private void PopulateLabels()
        {
            //TitleControl.EditText = "Edit";
        }

        private void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new ArticleConfiguration(getModuleSettings);

        }


    }
}
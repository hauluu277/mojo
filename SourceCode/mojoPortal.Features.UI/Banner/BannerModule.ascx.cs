// Author:					HiNet
// Created:					2015-3-12
// Last Modified:			2015-3-12
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

namespace BannerFeature.UI
{

    public partial class BannerModule : SiteModuleControl
    {
        // FeatureGuid 19695e58-f755-4579-8c71-0b31a2022850
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        readonly SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
        private BannerConfiguration config = new BannerConfiguration();

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
            TitleControl.EditUrl =SiteRoot + "/Banner/EditPost.aspx";
            BannerView.ModuleId = ModuleId;
            BannerView.PageId = PageId;
            BannerView.SiteRoot = SiteRoot;
            BannerView.IsEditable = IsEditable;
            BannerView.ImageSiteRoot = ImageSiteRoot;
        }
        private void PopulateLabels()
        {
            TitleControl.Visible = false;
            if (siteUser != null)
            {
                if (siteUser.IsInRoles("Admins") || IsEditable)
                {
                    TitleControl.Visible = true;
                }
            }
            TitleControl.EditUrl = SiteRoot + "/Banner/editpost.aspx";
            TitleControl.EditText = BannerResources.BannerAddPostLabel;
            if (IsEditable)
            {
                TitleControl.LiteralExtraMarkup =
                    "&nbsp;<a href='"
                    + SiteRoot
                    + "/banner/managepost.aspx?pageid=" + PageId.ToInvariantString()
                    + "&amp;mid=" + ModuleId.ToInvariantString()
                    + "' class='ModuleEditLink' title='"
                    + BlogResources.Administration + "'>"
                    + BlogResources.Administration + "</a>"
                    ;
                if (!siteUser.IsInRoles("Admins"))
                {
                    TitleControl.DisabledModuleSettingsLink = true;
                }
            }
            pnlOuterWrap.CssClass = config.ModuleCssCustome;
        }
        private void LoadSettings()
        {
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new BannerConfiguration(moduleSettings);

        }


    }
}
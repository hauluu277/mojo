// Author:					HiNet
// Created:					2015-3-19
// Last Modified:			2015-3-19
// 
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)  
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software.

using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Features.UI.GlobalModule.ThongKeTruyCap;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;

namespace ThongKeTruyCapFeature.UI
{

    public partial class ThongKeTruyCapModule : SiteModuleControl
    {
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        ThongKeTruyCapConfiguration config = new ThongKeTruyCapConfiguration();
        // FeatureGuid c6e16a14-2aa1-43ff-9626-e55bae7d5826

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
            pnlOuterWrap.CssClass = config.ModuleDisplayCssCustome;
            TitleControl.Visible = true;
            if (siteUser != null)
            {
                if (siteUser.IsInRoles("Admins"))
                {
                    TitleControl.Visible = true;
                }
            }
            thongKeTruyCapControl.config = config;
            thongKeTruyCapControl.ModuleId = ModuleId;
            TitleControl.ShowEditLinkOverride = false;
        }

        private void PopulateLabels()
        {
            TitleControl.Visible = true;
            if (siteUser != null)
            {
                if (siteUser.IsInRoles("Admins"))
                {
                    TitleControl.DisabledModuleSettingsLink = false;
                }
                else
                {
                    TitleControl.DisabledModuleSettingsLink = true;
                }
            }
        }

        private void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new ThongKeTruyCapConfiguration(getModuleSettings);
            siteSettings = CacheHelper.GetCurrentSiteSettings();
        }
    }
}

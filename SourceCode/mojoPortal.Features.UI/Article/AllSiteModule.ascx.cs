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
using mojoPortal.Features;
using mojoPortal.Web;
using System;
using System.Collections;

namespace ArticleFeature.UI
{

    public partial class AllSiteModule : SiteModuleControl
    {
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        ArticleConfiguration config = new ArticleConfiguration();
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
            pnlOuterWrap.CssClass = config.ModuleRightCssCustome;

        }

        private void PopulateControls()
        {

        }

        private void PopulateLabels()
        {
            TitleControl.Visible = false;
            if (siteUser != null)
            {
                if (siteUser.IsInRoles("Admins"))
                {
                    TitleControl.Visible = true;
                }
            }
            //if (IsEditable)
            //{
            //    TitleControl.LiteralExtraMarkup =
            //        "&nbsp;<a href='"
            //        + SiteRoot
            //        + "/document/managepost.aspx?pageid=" + PageId.ToInvariantString()
            //        + "&amp;mid=" + ModuleId.ToInvariantString()
            //        + "' class='ModuleEditLink' title='"
            //        + BlogResources.Administration + "'>"
            //        + BlogResources.Administration + "</a>"
            //        ;
            //}
        }

        private void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new ArticleConfiguration(getModuleSettings);
        }


    }
}

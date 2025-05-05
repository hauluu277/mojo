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
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;

namespace ThuTucHanhChinhFeature.UI
{

    public partial class ThuTucHanhChinhModule : SiteModuleControl
    {
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        DocumentConfiguration config = new DocumentConfiguration();
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
            TitleControl.EditUrl = SiteRoot + "/Document/EditPost.aspx";
            ThuTucHanhChinhList.ModuleId = ModuleId;
            ThuTucHanhChinhList.PageId = PageId;
            ThuTucHanhChinhList.SiteRoot = SiteRoot;
            ThuTucHanhChinhList.IsEditable = IsEditable;
            ThuTucHanhChinhList.ImageSiteRoot = ImageSiteRoot;
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
            //TitleControl.EditUrl = SiteRoot + "/document/editpost.aspx";
            //TitleControl.EditText = DocumentResources.AddPostLabel;
            
            //if (IsEditable)
            //{
            //    TitleControl.Visible = true;
            //    TitleControl.LiteralExtraMarkup =
            //        "&nbsp;<a href='"
            //        + SiteRoot
            //        + "/document/managepost.aspx?pageid=" + PageId.ToInvariantString()
            //        + "&amp;mid=" + ModuleId.ToInvariantString()
            //        + "' class='ModuleEditLink' title='"
            //        + BlogResources.Administration + "'>"
            //        + BlogResources.Administration + "</a>"
            //        ;
            //    TitleControl.ForceShowExtraMarkup = true;
            //    TitleControl.CanEdit = false;
            //    TitleControl.DisabledModuleSettingsLink = false;
            //}
        }

        private void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new DocumentConfiguration(getModuleSettings);
            siteSettings = CacheHelper.GetCurrentSiteSettings();
        }


    }
}


// Author:					Trieubv
// Created:					2015-10-27
// Last Modified:			2015-10-27
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

namespace DuThaoVanBanFeature.UI
{

    public partial class DuThaoVanBanModule : SiteModuleControl
    {
        // FeatureGuid 7f19b7d5-b8a2-492b-8421-1e51bcbbea8f
        protected DuThaoVanBanConfiguration config = new DuThaoVanBanConfiguration();
        private bool isUserApprove = false;
        #region OnInit
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
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
            RecentList.ModuleId = ModuleId;
            RecentList.PageId = PageId;
            RecentList.SiteRoot = SiteRoot;
            RecentList.ImageSiteRoot = ImageSiteRoot;
            RecentList.IsEditable = IsEditable;
        }


        private void PopulateLabels()
        {
            Title1.Visible = false;
            if (siteUser != null)
            {
                if (siteUser.IsInRoles("Admins") || isUserApprove==true)
                {
                    Title1.Visible = true;
                    Title1.EditUrl = SiteRoot + "/duthaovanban/editpost.aspx";
                    Title1.EditText = DocumentResources.AddPostLabel;
                }
            }       
            if (IsEditable || isUserApprove)
            {
                Title1.LiteralExtraMarkup =
                    "&nbsp;<a href='"
                    + SiteRoot
                    + "/duthaovanban/managepost.aspx?pageid=" + PageId.ToInvariantString()
                    + "&amp;mid=" + ModuleId.ToInvariantString()
                    + "' class='ModuleEditLink' title='"
                    + BlogResources.Administration + "'>"
                    + BlogResources.Administration + "</a>"
                    ;
            }
        }

        private void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new DuThaoVanBanConfiguration(getModuleSettings);
            if (WebUser.IsInRoles(config.RoleApprove))
            {
                isUserApprove = true;
            }
        }


    }
}
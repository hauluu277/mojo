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
using mojoPortal.Features;

namespace LinkFeature.UI
{

    public partial class LinkModule : SiteModuleControl
    {
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        LinkConfiguration config = new LinkConfiguration();
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
            pnlOuterWrap.CssClass = config.ModuleCssCustome;
        }

        private void PopulateControls()
        {
            TitleControl.EditUrl = SiteRoot + "/Admin/AdminLink.aspx";
            RecentList.ModuleId = ModuleId;
            RecentList.PageId = PageId;
            RecentList.SiteRoot = SiteRoot;
            RecentList.IsEditable = IsEditable;
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
            TitleControl.EditUrl = SiteRoot + "/Admin/AdminLink.aspx";
            TitleControl.EditText = ArticleResources.AddNewLink;
            if (IsEditable)
            {
                TitleControl.LiteralExtraMarkup =
                    "&nbsp;<a href='"
                    + SiteRoot
                    + "/Admin/AdminLink.aspx"
                    + "' class='ModuleEditLink' title='"
                    + BlogResources.Administration + "'>"
                    + BlogResources.Administration + "</a>"
                    ;
            }
        }

        private void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new LinkConfiguration(getModuleSettings);
        }


    }
}

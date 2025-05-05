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
using mojoPortal.Business.WebHelpers;

namespace LichCongTacFeature.UI
{

    public partial class LichCongTacModule : SiteModuleControl
    {
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
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
            //TitleControl.EditUrl = SiteRoot + "/lichcongtac/EditPost.aspx";
            //RecentList.PageId = PageId;
            //RecentList.ModuleId = ModuleId;
            //RecentList.SiteRoot = SiteRoot;
            //RecentList.IsEditable = IsEditable;
        }

        private void PopulateLabels()
        {
            TitleControl.Visible = true;
            //if (siteUser != null)
            //{
            //    if (siteUser.IsInRoles("Admins"))
            //    {
            //        TitleControl.EditUrl = SiteRoot + "/lichcongtac/editpost.aspx";
            //        TitleControl.EditText = LichCongTacResources.AddLabel;

            //        TitleControl.LiteralExtraMarkup =
            //        "&nbsp;<a href='"
            //        + SiteRoot
            //        + "/lichcongtac/managepost.aspx?pageid=" + PageId.ToInvariantString()
            //        + "&amp;mid=" + ModuleId.ToInvariantString()
            //        + "' class='ModuleEditLink' title='Quản trị'>Quản trị lịch làm việc</a>"
            //        ;
            //    }
            //    else
            //    {

            //        if (IsEditable)
            //        {
            //            TitleControl.DisabledModuleSettingsLink = false;
            //            TitleControl.CanEdit = false;
            //            TitleControl.LiteralExtraMarkup =
            //                "&nbsp;<a href='"
            //                + SiteRoot
            //                + "/lichcongtac/editpost.aspx?pageid=" + PageId.ToInvariantString()
            //                + "&amp;mid=" + ModuleId.ToInvariantString()
            //                + "' class='ModuleEditLink' title='Thêm mới lịch công tác'>Thêm mới lịch làm việc</a>"
            //                + "&nbsp;<a href='"
            //                + SiteRoot
            //                + "/lichcongtac/managepost.aspx?pageid=" + PageId.ToInvariantString()
            //                + "&amp;mid=" + ModuleId.ToInvariantString()
            //                + "' class='ModuleEditLink' title='Quản trị'>Quản trị lịch làm việc</a>"
            //                ;
            //                TitleControl.ForceShowExtraMarkup = true;
            //        }
            //    }
            //}

        }

        private void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            siteSettings = CacheHelper.GetCurrentSiteSettings();
        }
    }
}

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

namespace MediaFeature.UI
{

    public partial class SlideMediaModule : SiteModuleControl
    {
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        MediaConfiguration config = new MediaConfiguration();
        // FeatureGuid c6e16a14-2aa1-43ff-9626-e55bae7d5826

        #region OnInit

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(Page_Load);
            //btnSearch.Click +=btnSearch_Click;

        }

        //private void btnSearch_Click(object sender, EventArgs e)
        //{
        //    string pageUrl = SiteRoot + "/Media/ViewListMediaGroup.aspx"
        //          + "?pageid=" + PageId.ToInvariantString()
        //          + "&mid=" + ModuleId.ToInvariantString()
        //          + "&keyword=" + txtSearch.Text
        //          + "&pagenumber=1";
        //    WebUtils.SetupRedirect(this, pageUrl);
        //}

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
            TitleControl.EditUrl = SiteRoot + "/Media/EditMediaAlbum.aspx";
            SlideMediaControl.PageId = PageId;
            SlideMediaControl.ModuleId = ModuleId;
            SlideMediaControl.PageSize = config.PageSizeModule;
            SlideMediaControl.Is_PhanTrang = false;
            SlideMediaControl.SiteRoot = SiteRoot;
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
            TitleControl.EditUrl = SiteRoot + "/Media/EditMediaAlbum.aspx";
            TitleControl.EditText = MediaResources.MediaAlbumAddNewTitle;
            if (IsEditable)
            {
                TitleControl.LiteralExtraMarkup =
                    "&nbsp;<a href='"
                    + SiteRoot
                    + "/Media/CreateMultipleImage.aspx?pageid=" + PageId.ToInvariantString()
                    + "&amp;mid=" + ModuleId.ToInvariantString()
                    + "' class='ModuleEditLink' title='Thêm nhiều ảnh'>Thêm nhiều ảnh</a>"
                    + "&nbsp;<a href='"
                    + SiteRoot
                    + "/Media/ManagePost.aspx?pageid=" + PageId.ToInvariantString()
                    + "&amp;mid=" + ModuleId.ToInvariantString()
                    + "' class='ModuleEditLink' title='"
                    + BlogResources.Administration + "'>"
                    + BlogResources.Administration + "</a>"

                    ;
            }
            //lblDictionaryTitle.Text = MediaResources.CategoryMultiMediaTitle;
            //btnSearch.Text = MediaResources.MediaButtonSearch;
            //hplDataGroup.Text = MediaResources.CategoryMultiMediaTitle;
            //hplMutilData.Text = MediaResources.MultiMediaDataTitle;
            //hplDataGroup.NavigateUrl = SiteRoot + "/Media/ViewListMediaGroup.aspx?pageid=" + PageId + "&mid=" + ModuleId;
            //hplMutilData.NavigateUrl = SiteRoot + "/Media/ViewListMediaAlbum.aspx?pageid=" + PageId + "&mid=" + ModuleId;
        }

        private void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new MediaConfiguration(getModuleSettings);
        }


    }
}

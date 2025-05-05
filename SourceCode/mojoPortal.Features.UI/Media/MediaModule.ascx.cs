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

namespace MediaFeature.UI
{

    public partial class MediaModule : SiteModuleControl
    {
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        public MediaConfiguration config = new MediaConfiguration();
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
            MediaGroupList.PageId = PageId;
            MediaGroupList.ModuleId = ModuleId;
            MediaGroupList.PageSize = config.PageSizeModule;
            MediaGroupList.Is_PhanTrang = false;
            MediaGroupList.SiteRoot = SiteRoot;
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
        }

        private void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new MediaConfiguration(getModuleSettings);
        }


    }
}

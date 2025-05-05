using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Features;
using mojoPortal.Business;
using mojoPortal.Web;
using System.Collections;
using mojoPortal.Web.Framework;


namespace VideoIntroduceFeatures.UI
{
    public partial class VideoHotModule : SiteModuleControl
    {
        VideoIntroduceConfiguration config = new VideoIntroduceConfiguration();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            EnableViewState = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadParams();
            LoadSettings();
            PopulateLabels();
            PopulateControls();
        }

        private void PopulateLabels()
        {
            TitleControl.Visible = false;
            TitleControl.EditUrl = SiteRoot + "/VideoIntroduce/editpost.aspx";
            TitleControl.EditText = "Thêm mới video";
            if (IsEditable)
            {
                TitleControl.Visible = true;
                TitleControl.LiteralExtraMarkup = "&nbsp;&nbsp;&nbsp;<a href='"
                    + SiteRoot
                    + "/VideoIntroduce/ManagePost.aspx?pageid=" + PageId.ToInvariantString()
                    + "' class='ModuleEditLink' title='Quản trị video'>Quản trị video</a>"
                    ;
            }
        }

        private void PopulateControls()
        {
            VideoHot.ModuleId = ModuleId;
            VideoHot.PageId = PageId;
            VideoHot.SiteId = SiteId;
            VideoHot.Config = config;
            VideoHot.SiteRoot = SiteRoot;
            VideoHot.IsEditable = IsEditable;
            VideoHot.siteSettings = siteSettings;

        }
        private void LoadSettings()
        {
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new VideoIntroduceConfiguration(moduleSettings);
        }
        private void LoadParams()
        {

        }
    }
}
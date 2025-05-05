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

namespace AnnouncementFeatures.UI
{
    public partial class AnnouncementModule : SiteModuleControl
    {
        AnnouncementConfiguration config = new AnnouncementConfiguration();
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

            TitleControl.EditUrl = SiteRoot + "/Announcement/editpost.aspx";
            TitleControl.EditText = "Thêm mới thông báo";
            if (IsEditable)
            {
                TitleControl.Visible = true;
                TitleControl.LiteralExtraMarkup = "&nbsp;&nbsp;&nbsp;<a href='"
                    + SiteRoot
                    + "/Announcement/Manage.aspx?pageid=" + PageId.ToInvariantString()
                    + "' class='ModuleEditLink' title='Quản thông báo'>Quản trị thông báo</a>"
                    ;
            }
        }

        private void PopulateControls()
        {
            AnnouncementRecenlist.ModuleId = ModuleId;
            AnnouncementRecenlist.PageId = PageId;
            AnnouncementRecenlist.SiteId = SiteId;
            AnnouncementRecenlist.Config = config;
            AnnouncementRecenlist.SiteRoot = SiteRoot;
            AnnouncementRecenlist.IsEditable = IsEditable;
            //AnnouncementRecenlist.siteSettings = siteSettings;

        }
        private void LoadSettings()
        {
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new AnnouncementConfiguration(moduleSettings);
        }
        private void LoadParams()
        {

        }
    }
}
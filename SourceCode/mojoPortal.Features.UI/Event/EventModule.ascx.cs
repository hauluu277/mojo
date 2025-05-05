using mojoPortal.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Web.Framework;
using mojoPortal.Features;
using mojoPortal.Business;
using System.Collections;
using Resources;
using mojoPortal.Business.WebHelpers;

namespace EventFeature.UI
{
    public partial class EventModule : SiteModuleControl
    {
        protected EventConfiguration config = new EventConfiguration();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            EnableViewState = false;
        }

        protected void btnViewAll_Click(object sender, EventArgs e)
        {
            WebUtils.SetupRedirect(this, SiteRoot + "/event/managepost.aspx?pageid=" + PageId.ToInvariantString() + "&mid=" + ModuleId.ToInvariantString());
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            WebUtils.SetupRedirect(this, SiteRoot + "/event/editpost.aspx?pageid=" + PageId.ToInvariantString() + "&mid=" + ModuleId.ToInvariantString());
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadParams();
            LoadSettings();
            PopulateLabels();
            if (!IsPostBack)
            {
                PopulateControls();
            }
        }

        private void PopulateLabels()
        {
            TitleControl.Visible = false;
            if (siteUser != null)
            {
                if (siteUser.IsInRoles("Admins") || WebUser.IsInRoles(config.RolePost) || WebUser.IsInRoles(config.RoleApproved))
                {
                    if (siteUser.IsInRoles("Admins"))
                    {
                        TitleControl.DisabledModuleSettingsLink = false;
                    }
                    else
                    {
                        TitleControl.DisabledModuleSettingsLink = true;
                    }
                    TitleControl.Visible = true;
                }
            }
            TitleControl.EditUrl = SiteRoot + "/event/editpost.aspx";
            TitleControl.EditText = EventResources.BlogAddPostLabel;
            if (IsEditable)
            {
                TitleControl.LiteralExtraMarkup =
                    "&nbsp;<a href='"
                    + SiteRoot
                    + "/event/managepost.aspx?pageid=" + PageId.ToInvariantString()
                    + "&amp;mid=" + ModuleId.ToInvariantString()
                    + "' class='ModuleEditLink' title='"
                    + EventResources.Administration + "'>"
                    + EventResources.Administration + "</a>"
                    ;
            }
        }
        protected virtual void LoadSettings()
        {
            pnlContainer.ModuleId = ModuleId;
        }

        private void PopulateControls()
        {
            recentList.ModuleId = ModuleId;
            recentList.PageId = PageId;
            recentList.SiteId = SiteId;
            recentList.Config = config;
            recentList.SiteRoot = SiteRoot;
            recentList.ImageSiteRoot = ImageSiteRoot;
            recentList.IsEditable = IsEditable;
        }
        private void LoadParams()
        {
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new EventConfiguration(moduleSettings);
        }

    }
}
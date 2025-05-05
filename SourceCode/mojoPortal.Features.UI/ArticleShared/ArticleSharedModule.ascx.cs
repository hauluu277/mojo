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

namespace ArticleFeature.UI
{
    public partial class ArticleSharedModule : SiteModuleControl
    {
        protected ArticleSharedConfiguration config = new ArticleSharedConfiguration();
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
                if (siteUser.IsInRoles("Admins"))
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

        }
        protected virtual void LoadSettings()
        {
            pnlContainer.ModuleId = ModuleId;
        }

        private void PopulateControls()
        {
            ArticleSharedControl.ModuleId = ModuleId;
            ArticleSharedControl.PageId = PageId;
            ArticleSharedControl.SiteId = SiteId;
            ArticleSharedControl.Config = config;
            ArticleSharedControl.SiteRoot = SiteRoot;
            ArticleSharedControl.ImageSiteRoot = ImageSiteRoot;
            ArticleSharedControl.IsEditable = IsEditable;
        }
        private void LoadParams()
        {
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new ArticleSharedConfiguration(moduleSettings);
        }

    }
}
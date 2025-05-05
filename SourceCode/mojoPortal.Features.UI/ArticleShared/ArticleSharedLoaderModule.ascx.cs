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
    public partial class ArticleSharedLoaderModule : SiteModuleControl
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
            PopulateControls();
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
            pnlContainer.CssClass = config.CustomCssClassSetting;
        }

        private void PopulateControls()
        {
            var pageId = 0;
            if (!string.IsNullOrEmpty(config.PageViewSetting))
            {
                try
                {
                    pageId = Convert.ToInt32(config.PageViewSetting.Split('-')[0].ToString());

                }
                catch
                {
                    pageId = 0;
                }
            }


            ArticleSharedLoaderControl.ModuleId = ModuleId;
            ArticleSharedLoaderControl.PageId = pageId;
            ArticleSharedLoaderControl.SiteId = SiteId;
            ArticleSharedLoaderControl.Config = config;
            ArticleSharedLoaderControl.SiteRoot = SiteRoot;
            ArticleSharedLoaderControl.ImageSiteRoot = ImageSiteRoot;
            ArticleSharedLoaderControl.IsEditable = IsEditable;
        }
        private void LoadParams()
        {
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new ArticleSharedConfiguration(moduleSettings);
        }

    }
}
using mojoPortal.Business;
using mojoPortal.Features;
using mojoPortal.Web;
using System;
using System.Collections;

namespace ArticleFeature.UI
{
    public partial class ArticleCongThanhVienModule : SiteModuleControl
    {
        protected ArticleConfiguration config = new ArticleConfiguration();
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
            config = new ArticleConfiguration(moduleSettings);
        }

    }
}
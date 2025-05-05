using ArticleFeature.Business;
using ArticleFeature.UI;
using mojoPortal.Business;
using mojoPortal.Features;
using mojoPortal.Web;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkWebsiteFeature.UI
{
    public partial class LinkWebsiteModule : SiteModuleControl
    {
        private int loadedModuleId = -1;
        private int loadedPageId = -1;
        private int categoryId = -1;
        string[] listModuleId;
        private int countOfDrafts;
        protected LinkWebsiteConfigurations config = new LinkWebsiteConfigurations();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            EnableViewState = false;
        }

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            LoadSettings();
            PopulateLabels();
            PopulateControls();
            //pnlOuterWrap.CssClass = config.ModuleDisplayCssCustome;

        }

        private void PopulateControls()
        {
            if (!IsPostBack)
            {
                BindLink();
                BindDropdownLink();
            }
        }
        private void BindLink()
        {
            lblLienKetWebsite.Text = GlobalResource.LienKetWebsiteLabel;
            if (!string.IsNullOrEmpty(config.CategoryLinkSetting))
            {
                rptDanhMuc.DataSource = CoreCategory.GetChildren(config.CategoryLinkSetting.ToIntOrZero());
                rptDanhMuc.DataBind();
            }
        }
        private void BindDropdownLink()
        {
            if (!string.IsNullOrEmpty(config.CategoryLinkWebsiteSetting))
            {
                ddlLink.DataTextField = "Name";
                ddlLink.DataValueField = "Description";
                ddlLink.DataSource = CoreCategory.GetChildren(config.CategoryLinkWebsiteSetting.ToIntOrZero());
                ddlLink.DataBind();
            }
        }
        protected virtual void PopulateLabels()
        {
            Title1.Visible = false;
            if (siteUser != null)
            {
                if (siteUser.IsInRoles("Admins"))
                {
                    Title1.Visible = true;
                }
            }
            Title1.ShowEditLinkOverride = false;

        }


        protected virtual void LoadSettings()
        {
            pnlContainer.ModuleId = ModuleId;
            config = new LinkWebsiteConfigurations(Settings);

        }
    }
}
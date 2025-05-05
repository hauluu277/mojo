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
    public partial class DieuTraModule : SiteModuleControl
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
                BindLinhVuc();
                BindTongDieuTra();
            }
        }
        private void BindLinhVuc()
        {
            lblLinhVucDieuTra.Text = GlobalResource.LinhVucDieuTraLabel;
            if (!string.IsNullOrEmpty(config.CategoryLinhVucSetting))
            {
                rptLinhVuc.DataSource = CoreCategory.GetChildren(config.CategoryLinhVucSetting.ToIntOrZero());
                rptLinhVuc.DataBind();
            }
        }
        private void BindTongDieuTra()
        {
            lblTongDieuTra.Text = GlobalResource.TongDieuTraLabel;
            lblDieuTraThongKe.Text = GlobalResource.DieuTraThongKeLabel;
            if (!string.IsNullOrEmpty(config.CategoryTongDieuTraSetting))
            {
                rptTongDieuTra.DataSource = CoreCategory.GetChildren(config.CategoryTongDieuTraSetting.ToIntOrZero());
                rptTongDieuTra.DataBind();
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
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

namespace CommonFeature.UI
{
    public partial class FanpageModel : SiteModuleControl
    {
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

        }
        protected virtual void LoadSettings()
        {
            pnlContainer.ModuleId = ModuleId;
        }

        private void PopulateControls()
        {
            iframe.Src= siteSettings.FanPageIframe;
        }
        private void LoadParams()
        {
        }

    }
}
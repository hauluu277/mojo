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

namespace ArticleFeature.UI
{
    public partial class ArticleTrainingModule : SiteModuleControl
    {
        private int loadedModuleId = -1;
        private int loadedPageId = -1;
        private int categoryId = -1;
        string[] listModuleId;
        private int countOfDrafts;
        protected ArticleConfiguration config = new ArticleConfiguration();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            EnableViewState = false;
        }

        protected virtual void Page_Load(object sender, EventArgs e)
        {

            PopulateLabels();
            PopulateControls();
            pnlOuterWrap.CssClass = config.ModuleDisplayCssCustome;

        }

        private void PopulateControls()
        {
            ArticleTrainingControl.SiteId = SiteId;
            ArticleTrainingControl.SiteRoot = SiteRoot;
            ArticleTrainingControl.ModuleId = ModuleId;
            ArticleTrainingControl.PageId = PageId;
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


    }
}
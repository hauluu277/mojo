using ArticleFeature.Business;
using ArticleFeature.UI;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
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
    public partial class CategoryMenuModule : SiteModuleControl
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
            LoadSettings();
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
            var category = config.ArticleCategoryConfig;
            if (!string.IsNullOrEmpty(category))
            {
                var listCategory = CoreCategory.GetChildren(Convert.ToInt32(category.Split('-')[0]));
                rptMenuCategory.DataSource = listCategory;
                rptMenuCategory.DataBind();
            }
        }
        protected List<CoreCategory> LoadCategoryChild(int parentId)
        {
            return CoreCategory.GetChildrenByParent(parentId);
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
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            var module_settings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new ArticleConfiguration(module_settings);
        }

    }
}
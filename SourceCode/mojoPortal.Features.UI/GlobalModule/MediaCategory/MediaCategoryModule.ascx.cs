using ArticleFeature.Business;
using ArticleFeature.UI;
using mojoPortal.Business;
using mojoPortal.Features;
using mojoPortal.Features.UI.GlobalModule.MediaCategory.Components;
using mojoPortal.Web;
using Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MediaCategoryFeature.UI
{
    public partial class MediaCategoryModule : SiteModuleControl
    {
        private int loadedModuleId = -1;
        private int loadedPageId = -1;
        private int categoryId = -1;
        string[] listModuleId;
        private int countOfDrafts;
        protected MediaCategoryConfigurations config = new MediaCategoryConfigurations();
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
                BindMediaControl();
            }
        }
        private void BindMediaControl()
        {
            if (!string.IsNullOrEmpty(config.MediaCategorySetting))
            {
                var category = new CoreCategory(config.MediaCategorySetting.ToIntOrZero());
                if (category != null)
                {
                    lblTitleCategory.Text = category.Name;
                    string result = string.Empty;
                    if (!string.IsNullOrEmpty(category.PathFile))
                    {
                        var extension = Path.GetExtension(category.PathFile);
                        if (!string.IsNullOrEmpty(extension))
                        {
                            extension = extension.Replace(".", string.Empty);
                            result = $"<audio controls/>";
                            result += $"<source src='{category.PathFile}' type='audio/{extension}'>";
                            result += "Your browser does not support the audio element.";
                            result += "</audio>";
                        }
                        else
                        {
                            result = "Chưa có tập tin";
                        }
                        literMedia.Text = result;
                    }
                }
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
            config = new MediaCategoryConfigurations(Settings);

        }
    }
}
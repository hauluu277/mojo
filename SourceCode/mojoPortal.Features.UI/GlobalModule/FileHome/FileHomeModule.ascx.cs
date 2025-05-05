using ArticleFeature.Business;
using ArticleFeature.UI;
using mojoPortal.Business;
using mojoPortal.Features;
using mojoPortal.Features.UI.GlobalModule.FileHome.Components;
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
using VideoIntroduceFeatures.Business;

namespace FileHomeFeature.UI
{
    public partial class FileHomeModule : SiteModuleControl
    {
        private int loadedModuleId = -1;
        private int loadedPageId = -1;
        private int categoryId = -1;
        string[] listModuleId;
        private int countOfDrafts;
        protected FileHomeConfigurations config = new FileHomeConfigurations();
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
                BindVideo();
            }
        }
        private void BindMediaControl()
        {
            if (!string.IsNullOrEmpty(config.FileHomeSetting))
            {
                var category = new CoreCategory(config.FileHomeSetting.ToIntOrZero());
                lblCategory.Text = category.Name;
                var listData = CoreCategory.GetChildren(config.FileHomeSetting.ToIntOrZero());
                rptItem.DataSource = listData;
                rptItem.DataBind();
            }

        }

        private void BindVideo()
        {
            var videoHot = VideoIntroduce.GetVideoIsHot(siteSettings.SiteId);
            if (videoHot != null && videoHot.ItemID > 0)
            {
                hplVideo.Text = videoHot.Title;
                hplVideo.NavigateUrl="javascript:void(0)";
                //hplVideo.NavigateUrl = string.Format("{0}{1}", SiteRoot, videoHot.ItemUrl);
                string iframe = $"<iframe src='{videoHot.YoutubeUrl}' title='{videoHot.Name}'></iframe>";
                literVideo.Text = iframe;
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
            config = new FileHomeConfigurations(Settings);

        }
    }
}
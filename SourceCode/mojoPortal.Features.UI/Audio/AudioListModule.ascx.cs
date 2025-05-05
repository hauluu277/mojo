using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Features.UI.Media.Component;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AudioFeature.UI
{
    public partial class AudioListModule : SiteModuleControl
    {
        private mojoBasePage basePage;
        private Module module;
        private int pageNumber = 1;
        private int totalPages = 1;
        private string createByUser = string.Empty;
        private string dateParam = string.Empty;
        private DateTime? createDate = null;
        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
        private int groupMediaId = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        private SiteSettings siteSettings;
        private int categoryID = -1;
        private bool? isPublish = null;
        private int status = -1;
        private string keyword = string.Empty;
        private int statusFeatureed = -1;
        private bool? isFeatured = null;
        private int step = 0;
        private string isCaNhan = "false";
        public int PageId
        {
            get { return pageId; }
            set { pageId = value; }
        }
        public int ModuleId
        {
            get { return moduleId; }
            set { moduleId = value; }
        }

        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        MediaConfiguration config = new MediaConfiguration();
        // FeatureGuid c6e16a14-2aa1-43ff-9626-e55bae7d5826

        #region OnInit

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(Page_Load);
            //btnSearch.Click +=btnSearch_Click;

        }

        //private void btnSearch_Click(object sender, EventArgs e)
        //{
        //    string pageUrl = SiteRoot + "/Media/ViewListMediaGroup.aspx"
        //          + "?pageid=" + PageId.ToInvariantString()
        //          + "&mid=" + ModuleId.ToInvariantString()
        //          + "&keyword=" + txtSearch.Text
        //          + "&pagenumber=1";
        //    WebUtils.SetupRedirect(this, pageUrl);
        //}

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            LoadSettings();
            PopulateLabels();
            PopulateControls();
            //pnlOuterWrap.CssClass = config.ModuleCssCustome;
        }

        private void PopulateControls()
        {
            LoadAudioGroup();
            listAudioControl.ModuleId = ModuleId;
            listAudioControl.config = config;
        }

        private void LoadAudioGroup()
        {
        }

        private void PopulateLabels()
        {
            
        }

        private void LoadSettings()
        {
            //Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            //config = new MediaConfiguration(getModuleSettings);
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            categoryID = WebUtils.ParseInt32FromQueryString("categoryID", categoryID);
            isCaNhan = WebUtils.ParseStringFromQueryString("iscanhan", isCaNhan);
            string _status = WebUtils.ParseStringFromQueryString("status", string.Empty);
            if (!string.IsNullOrEmpty(_status))
            {
                status = int.Parse(_status);
                if (_status == "1")
                {
                    isPublish = true;
                }
                else if (_status == "0")
                {
                    isPublish = false;
                }
            }
            keyword = WebUtils.ParseStringFromQueryString("keyword", keyword);
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
        }

    }
}
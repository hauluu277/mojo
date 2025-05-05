using MediaAlbumFeature.Business;
using MediaGroupFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using VideoIntroduceFeatures.Business;

namespace AudioFeature.UI
{
    public partial class ListAudioControl : System.Web.UI.UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;

        private mojoBasePage basePage;
        private Module module;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        public MediaConfiguration config { get; set; }
        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
        private int groupMediaId = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        private SiteSettings siteSettings;
        private int categoryID = -1;
        private bool? isFeatured = null;
        private int status = -1;
        private string keyword = string.Empty;
        private int orderBy = 0;

        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkImageUrl = string.Empty;
        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
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
        public int CategoryId
        {
            get { return categoryID; }
            set { categoryID = value; }
        }

        public int GroupMediaID
        {
            get { return groupMediaId; }
            set { groupMediaId = value; }
        }
        public string Keyword
        {
            get { return keyword; }
            set { keyword = value; }
        }
        public string SiteRoot
        {
            get { return siteRoot; }
            set { siteRoot = value; }
        }

        public string ImageSiteRoot
        {
            get { return imageSiteRoot; }
            set { imageSiteRoot = value; }
        }
        #endregion
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            LoadParams();
            LoadSettings();
            PopulateLabels();
            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }
        private void PopulateControls()
        {
            List<md_AudioGroup> listAudio = new List<md_AudioGroup>();
            listAudio = md_AudioGroup.GetPageManage("", null, siteSettings.SiteId, moduleId, pageNumber, config.NumberShowSetting, keyword, categoryID, true, siteUser.UserId, 4, 0, false, out totalPages);

            rptAudio.DataSource = listAudio;
            rptAudio.DataBind();
            string pageUrl = SiteRoot + "/Audio/ViewList.aspx"
           + "?pageid=" + PageId.ToInvariantString()
           + "&pagenumber={0}";

            pgrAudio.PageURLFormat = pageUrl;
            pgrAudio.ShowFirstLast = true;
            pgrAudio.PageSize = config.NumberShowSetting;
            pgrAudio.PageCount = totalPages;
            pgrAudio.CurrentIndex = pageNumber;
            pnlAudioPager.Visible = (totalPages > 1);
        }

        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            groupMediaId = WebUtils.ParseInt32FromQueryString("groupMediaId", GroupMediaID);
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);

        }
        public void PopulateLabels()
        {

        }
        protected virtual void LoadSettings()
        {

            siteSettings = CacheHelper.GetCurrentSiteSettings();
            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }

        }

    }
}
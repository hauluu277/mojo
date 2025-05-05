using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;

namespace DanhSachTruongFeature.UI
{
    public partial class DanhSachTruongList : UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;

        private mojoBasePage basePage;
        private Module module;

        private int pageId = -1;
        private int moduleId = -1;
        private int itemId = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        public SiteSettings siteSettings;
        protected int langId = 1;

        readonly PageSettings pageSettings = CacheHelper.GetCurrentPage();
        readonly SiteSettings siteSetting = CacheHelper.GetCurrentSiteSettings();
        private GlobalConfigurations config = new GlobalConfigurations();
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
        public string SiteRoot
        {
            get { return siteRoot; }
            set { siteRoot = value; }
        }

        public bool IsEditable { get; set; }
        #endregion

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
            //EnableViewState = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var lang = CultureInfo.CurrentCulture.Name;
            langId = lang == "vi-VN" ? LanguageConstant.VN : LanguageConstant.EN;
            LoadSettings();
            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }

        private void PopulateControls()
        {
            if (config.GetCategoryFist > 0)
            {
                rptArticles.DataSource = CoreCategory.GetChildren(config.GetCategoryFist);
                rptArticles.DataBind();
            }
            else
            {
                var parent = CoreCategory.GetByCode(1, WebConfigSettings.DM_TRUONGHOC);
                if (parent != null)
                {
                    var data = new List<CoreCategory>();
                    var listChild = CoreCategory.GetChildren(parent.ItemID);
                    foreach (var item in listChild)
                    {
                        var getCategory = CoreCategory.GetChildren(item.ItemID);
                        if (getCategory != null && getCategory.Count > 0)
                        {
                            data.AddRange(getCategory);
                        }
                    }
                    rptArticles.DataSource = data;
                    rptArticles.DataBind();
                }
            }
        }

        protected virtual void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new GlobalConfigurations(getModuleSettings);
            //BIND CONFIG HERE

            siteSettings = CacheHelper.GetCurrentSiteSettings();
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);

            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
        }

    }
}
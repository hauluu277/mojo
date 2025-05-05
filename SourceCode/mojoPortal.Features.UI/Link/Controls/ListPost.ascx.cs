using ArticleFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkFeature.UI
{
    public partial class ListPost : UserControl
    {
        #region Properties
        private int pageNumber = 1;
        private int totalPages = 1;

        private mojoBasePage basePage;
        private Module module;
        protected LinkConfiguration config = new LinkConfiguration();

        private int pageId = -1;
        private int siteId = -1;
        private int moduleId = -1;
        private int categoryId = -1;
        private string siteRoot = string.Empty;
        private SiteSettings siteSettings;
        protected int langId = 1;
        protected string EditContentImage = WebConfigSettings.EditContentImage;
        protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;
        protected string DeleteLinkText = SwirlingQuestionResource.ButtonDelete;
        protected string DeleteLinkImageUrl = string.Empty;
        protected string EditLinkText = SwirlingQuestionResource.ButtonEdit;
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
        public int SiteId
        {
            get { return siteId; }
            set { siteId = value; }
        }

        public string SiteRoot
        {
            get { return siteRoot; }
            set { siteRoot = value; }
        }

        public LinkConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }

        public bool IsEditable { get; set; }
        #endregion

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += Page_Load;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var lang = CultureInfo.CurrentCulture.Name;
            langId = lang == "vi-VN" ? LanguageConstant.VN : LanguageConstant.EN;
            LoadSettings();
            PopulateLabels();
            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
        }

        private void PopulateLabels()
        {
            CategoryLink cat = new CategoryLink(categoryId);
            if (cat != null)
            {
                lblTitle.Text = cat.Name;
            }
        }

        private void PopulateControls()
        {
            BindLink();
        }

        private void BindLink()
        {
            List<CoreLink> lstLink = CoreLink.GetByCatId(categoryId);
            rptLink.DataSource = lstLink;
            rptLink.DataBind();
        }
        //public List<CoreLink> GetListChild(int parentId)
        //{
        //    List<CoreLink> lst = CoreLink.GetChildByCatId(categoryId, parentId, SiteId);
        //    return lst;
        //}
        //protected bool pnChildVisibe(int parentId)
        //{
        //    bool visible = false;
        //    List<CoreLink> lst = CoreLink.GetChildByCatId(categoryId, parentId, SiteId);
        //    if (lst != null && lst.Count > 0)
        //    {
        //        visible = true;
        //    }
        //    return visible;
        //}
        protected virtual void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new LinkConfiguration(getModuleSettings);
            string _categoryId = config.LinkCategoryConfig.ToString().Replace("-", string.Empty);
            categoryId = Convert.ToInt32(_categoryId);

            if (Page is mojoBasePage)
            {
                basePage = Page as mojoBasePage;
                module = basePage.GetModule(moduleId);
            }
        }

        protected string formatContent(string Content)
        {
            if (!string.IsNullOrEmpty(Content))
            {
                return Regex.Replace(Content, "<(.|\n)*?>", string.Empty);
            }
            else
            {
                return string.Empty;
            }
        }
        protected string FormatArticleDate(DateTime datePromulgate)
        {
            if (config.DateTimeFormat == string.Empty) return string.Empty;
            return datePromulgate.ToString(config.DateTimeFormat);
        }

    }
}
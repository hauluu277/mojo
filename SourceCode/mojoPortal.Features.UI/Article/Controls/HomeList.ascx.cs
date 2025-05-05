// Author:					HiNet
// Created:					2014-9-24
// Last Modified:			2014-9-24
// 
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)  
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using mojoPortal.Web.UI;
using log4net;
using mojoPortal.Business;
using Resources;
using mojoPortal.Features;
using ArticleFeature.Business;
using System.IO;
using System.Linq;
using mojoPortal.Web;

namespace ArticleFeature.UI
{

    public partial class HomeList : UserControl
    {
        // FeatureGuid 65db0c7d-86a1-4e77-a171-d6e5d413c1b6
        private int pageId = -1;
        private int moduleId = -1;
        private int siteId = -1;
        protected int langId = -1;
        protected int type = -1;
        private string siteRoot = string.Empty;
        private string imageSiteRoot = string.Empty;
        private bool showImgConfig = false;
        private ArticleConfiguration config = new ArticleConfiguration();
        public bool ShowImgConfig
        {
            get { return showImgConfig; }
            set { showImgConfig = value; }
        }
        public int PageId
        {
            get { return pageId; }
            set { pageId = value; }
        }
        public int SiteId
        {
            get { return siteId; }
            set { siteId = value; }
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

        public string ImageSiteRoot
        {
            get { return imageSiteRoot; }
            set { imageSiteRoot = value; }
        }
        public bool IsEditable { get; set; }
        #region OnInit

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(Page_Load);

        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadSettings();
            PopulateLabels();
            if (!Page.IsPostBack)
            {
                PopulateControls();
            }

        }

        private void PopulateControls()
        {
            LoadArticleCategory();
            LoadArticle();
        }



        private void PopulateLabels()
        {

        }
        private void LoadArticle()
        {
            var listArticle = new List<Article>();
            var listCategory = config.ArticleCategoryConfig.ToListIntV2();
            foreach (var item in listCategory)
            {
                var category = Article.GetArticleTop(item, 6);
                listArticle.AddRange(category);
            }
            rptArticle.DataSource = listArticle;
            rptArticle.DataBind();
            //rptArticle.DataSource = Article.GetByCategorySetting(siteId, moduleId, config.HomeListSelectorMutipleSetting);
            //rptArticle.DataBind();
        }
        private void LoadArticleCategory()
        {
            var listCategory = new List<CoreCategory>();
            var listCategorySetting = config.ArticleCategoryConfig.ToListIntV2();
            foreach (var item in listCategorySetting)
            {
                var category = new CoreCategory(item);
                if (category != null)
                {
                    listCategory.Add(category);
                }
            }
            rptArticleCategory.DataSource = listCategory;
            rptArticleCategory.DataBind();
        }

        private void LoadSettings()
        {
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new ArticleConfiguration(moduleSettings);
            showImgConfig = config.ShowImgArticleHot;
        }

        protected bool ShowImage(string imageUrl)
        {
            if (String.IsNullOrEmpty(imageUrl))
            {
                return false;
            }
            else
            {
                string filePath = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["ArticleImagesFolder"] + imageUrl;
                filePath = filePath.Replace("/", "\\");
                if (File.Exists(filePath))
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
        }
    }
}
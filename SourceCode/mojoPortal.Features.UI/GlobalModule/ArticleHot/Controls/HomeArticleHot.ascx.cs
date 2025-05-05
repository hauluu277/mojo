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

using ArticleFeature.Business;
using mojoPortal.Business;
using mojoPortal.Features;
using mojoPortal.Features.UI.Article.Components;
using mojoPortal.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.UI;

namespace ArticleFeature.UI
{

    public partial class HomeArticleHot : UserControl
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
        protected bool isShowSapo = false;
        protected string HieuUngTin = "KhongHieuUng";
        protected int ThoiGianChuyenDong = 0;
        private ArticleConfiguration config = new ArticleConfiguration();
        protected ArticleNewConfiguration configHot = new ArticleNewConfiguration();

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
            //LoadArticleCategory();
            LoadArticle();
        }

        private void PopulateLabels()
        {
        }

        private void LoadArticle()
      {
            pnlType1.Visible = false;
            if (config.HomeDisplayTypeSetting == ArticleHomeConstant.Type_1)
            {
                pnlType1.Visible = true;
                var categories = config.ArticleCategoryConfig == "0-" ? "" : config.ArticleCategoryConfig;
                categories = categories.Replace("-", " ");
                if (!string.IsNullOrEmpty(categories))
                {
                    categories = categories.Trim();
                }
                var listArticle = Article.GetArticleIsHomeHotByCategory(siteId, categories, configHot.NumberArticleLimit + 4, 0, configHot.IsLayTinTuCongThanhVien);
                if (listArticle != null && listArticle.Any())
                {
                    rptArticleSlider.DataSource = listArticle.Take(configHot.NumberArticleLimit).ToList();
                    rptArticleSlider.DataBind();
                    if (listArticle.Count > configHot.NumberArticleLimit)
                    {
                        rptArticle.DataSource = listArticle.Skip(configHot.NumberArticleLimit);
                        rptArticle.DataBind();
                    }
                }
            }
            else if (config.HomeDisplayTypeSetting == ArticleHomeConstant.Type_2)
            {

            }
            else if (config.HomeDisplayTypeSetting == ArticleHomeConstant.Type_3)
            {

            }
        }
        private void LoadArticleCategory()
        {
            var listCategory = new List<CoreCategory>();
            var listCategorySetting = config.HomeListSelectorMutipleSetting.ToListIntV2();
            foreach (var item in listCategorySetting)
            {
                var category = new CoreCategory(item);
                if (category != null)
                {
                    listCategory.Add(category);
                }
            }
            //rptArticleCategory.DataSource = listCategory;
            //rptArticleCategory.DataBind();
        }

        private void LoadSettings()
        {
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new ArticleConfiguration(moduleSettings);
            configHot = new ArticleNewConfiguration(moduleSettings);
            showImgConfig = config.ShowImgArticleHot;
            isShowSapo = configHot.IsShowSapo;
            HieuUngTin = configHot.SetKieuHieuUng;
            ThoiGianChuyenDong = configHot.SetThoiGianChuyenDong;
        }

        protected string FormatArticleDate(DateTime startDate)
        {
            return string.Format("{0:dd/MM/yyyy}", startDate);
        }

        protected bool ShowImage(string imageUrl)
        {
            if (String.IsNullOrEmpty(imageUrl))
            {
                return false;
            }
            else
            {
                if (imageUrl.Contains("http") || imageUrl.Contains("https")) return true;
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
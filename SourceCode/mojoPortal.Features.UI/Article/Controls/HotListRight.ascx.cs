// Author:					HiNet
// Created:					2015-3-30
// Last Modified:			2015-3-30
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
using mojoPortal.Features;
using Resources;
using mojoPortal.Business.WebHelpers;
using System.Text.RegularExpressions;
using ArticleFeature.Business;
using System.Linq;

namespace ArticleFeature.UI
{

    public partial class HotListRight : UserControl
    {
        // FeatureGuid d4b1ad4b-6e07-4e9d-8970-c2ce6ef022cb
        protected ArticleConfiguration config = new ArticleConfiguration();

        private int pageId = -1;
        private int moduleId = -1;
        private int number = -1;
        private int readMost = 0;
        private string siteRoot = string.Empty;
        private SiteSettings siteSettings;
        protected bool visibleImg = false;
        protected bool displayHorizontal = false;
        protected bool showHotNew = false;
        protected bool displayTitle = true;
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
            if (!IsPostBack)
            {
                PopulateControls();
            }

        }

        private void PopulateControls()
        {
            PopulateTopSilde();
        }

        private void PopulateTopSilde()
        {

            if (showHotNew)
            {
                List<Article> slide = Article.GetArticleHotNew(siteSettings.SiteId, config.ShowArticleHotRight, readMost);

                if(slide != null && slide.Count > 0)
                {
                    var slideFirst = slide[0];
                    imgRightFirst.ImageUrl = ArticleUtils.FormatImageDialogNew(ConfigurationManager.AppSettings["ArticleImagesFolder"], slideFirst.ImageUrl);
                    hplRightFirst.Text = slideFirst.Title;
                    hplRightFirst.NavigateUrl = ArticleUtils.FormatBlogTitleUrl(SiteRoot, slideFirst.ItemUrl, slideFirst.ItemID, PageId, ModuleId);
                    if (slide.Count > 1)
                    {
                        Repeater1.DataSource = slide.Skip(1);
                        Repeater1.DataBind();
                    }

                }
                
            }
            else
            {
                List<Article> slide = Article.GetArticleHotRight(siteSettings.SiteId, config.ShowArticleHotRight, readMost);
                rptArticle.DataSource = slide;
                rptArticle.DataBind();
            }
        }
        private void PopulateLabels()
        {
            lblTit.Text = config.TitleSetting.ToString();
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
        private void LoadSettings()
        {
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(ModuleId);
            config = new ArticleConfiguration(getModuleSettings);
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            visibleImg = config.ShowImageRight;
            displayHorizontal = config.ShowHorizontal;
            displayTitle = config.ShowTitle;
            showHotNew = config.ShotHotNew;
            if (config.ShowArticleMostRead)
            {
                readMost = 1;
            }
            else
            {
                readMost = 0;
            }
        }


    }
}
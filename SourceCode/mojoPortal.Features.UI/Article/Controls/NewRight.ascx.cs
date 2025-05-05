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

using ArticleFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features.UI.Article.Components;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace ArticleFeature.UI
{

    public partial class NewRight : UserControl
    {
        // FeatureGuid d4b1ad4b-6e07-4e9d-8970-c2ce6ef022cb
        protected ArticleNewConfiguration config = new ArticleNewConfiguration();

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
        protected bool isShowSapo = false;
        protected string HieuUngTin = "KhongHieuUng";
        protected int ThoiGianChuyenDong = 0;
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
            if (!IsPostBack)
            {
                PopulateControls();
            }

        }

        private void PopulateControls()
        {
            BindTinMoiDocNhieu();
        }

        private void BindTinMoiDocNhieu()
        {

            var categorySetting = config.ArticleCategoryConfig.Replace("-", " ");

            var data = Article.GetArticleTopNew(categorySetting, config.NumberArticleLimit, siteSettings.SiteId, config.IsLayTinTuCongThanhVien);

            var listArticle = data;
            rptTinMoi.DataSource = listArticle;
            rptTinMoi.DataBind();

            //var dataHitCount = Article.GetArticleTopHitCount(categorySetting, config.NumberArticleLimit, siteSettings.SiteId, config.IsLayTinTuCongThanhVien);

            //pnlTinMoiTinDocNhieu.Visible = true;
            //rptTinDocNhieu.DataSource = dataHitCount;
            //rptTinDocNhieu.DataBind();

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
            config = new ArticleNewConfiguration(getModuleSettings);
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            isShowSapo = config.IsShowSapo;
            HieuUngTin = config.SetKieuHieuUng;
            ThoiGianChuyenDong = config.SetThoiGianChuyenDong;
        }


    }
}
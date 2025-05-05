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

using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Web.Framework;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace DocumentFeature.UI
{

    public partial class DocumentLinkControl : UserControl
    {
        // FeatureGuid d4b1ad4b-6e07-4e9d-8970-c2ce6ef022cb
        protected DocumentConfiguration config = new DocumentConfiguration();

        private int pageId = -1;
        private int moduleId = -1;
        private string siteRoot = string.Empty;
        private int linhVucId = 0;
        private int loaiVB = 0;
        private int coQuan = 0;
        private int nam = 0;

        private SiteSettings siteSettings;
        private string keyword = string.Empty;
        private int pageNumber = 1;
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
            //bind category lĩnh vực
            if (siteSettings.CoreLinhVucVanBanQuyPham > 0)
            {
                var source = CoreCategory.GetChildren(siteSettings.CoreLinhVucVanBanQuyPham);
                rptLinhVuc.DataSource = source;
                rptLinhVuc.DataBind();
            }
            //bind category cơ quan ban hành
            if (siteSettings.CoreCoQuanBanHanhVanBanQuyPham > 0)
            {
                var source = CoreCategory.GetChildren(siteSettings.CoreCoQuanBanHanhVanBanQuyPham);
                rptCoQuanBanHanh.DataSource = source;
                rptCoQuanBanHanh.DataBind();
            }
            //bind category hình thức văn bản
            if (siteSettings.CoreLoaiVanBanQuyPham > 0)
            {
                var source = CoreCategory.GetChildren(siteSettings.CoreLoaiVanBanQuyPham);
                rptLoaiVanBan.DataSource = source;
                rptLoaiVanBan.DataBind();

            }
        }

        private void PopulateLabels()
        {

        }

        protected string GetActiveLinhVuc(int itemId)
        {
            if (itemId == linhVucId)
            {
                return " class='vanban-active'";
            }
            return string.Empty;
        }
        protected string GetActiveCoQuan(int itemId)
        {
            if (itemId == coQuan)
            {
                return " class='vanban-active'";
            }
            return string.Empty;
        }
        protected string GetActiveLoaiVanBan(int itemId)
        {
            if (itemId == loaiVB)
            {
                return " class='vanban-active'";
            }
            return string.Empty;
        }

        protected string formartLinhVucUrl(int coreLinhVuc)
        {
            string Url = SiteRoot + "/document/viewpost.aspx?pageid=" + pageId + "&mid=1530&linhvucid=" + coreLinhVuc + "&loaivb=" + loaiVB + "&coquan=" + coQuan + "&nam=" + nam + "&keyword=" + keyword;
            return Url;
        }
        protected string formartCoQuanUrl(int coreCoQuan)
        {
            string Url = SiteRoot + "/document/viewpost.aspx?pageid=" + pageId + "&mid=1530&linhvucid=" + linhVucId + "&loaivb=" + loaiVB + "&coquan=" + coreCoQuan + "&nam=" + nam + "&keyword=" + keyword;
            return Url;
        }
        protected string formartLoaiVBUrl(int coreLoaiVB)
        {
            string Url = SiteRoot + "/document/viewpost.aspx?pageid=" + pageId + "&mid=1530&linhvucid=" + linhVucId + "&loaivb=" + coreLoaiVB + "&coquan=" + coQuan + "&nam=" + nam + "&keyword=" + keyword;
            return Url;
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
            config = new DocumentConfiguration(getModuleSettings);
            siteSettings = CacheHelper.GetCurrentSiteSettings();
            linhVucId = WebUtils.ParseInt32FromQueryString("linhvucid", linhVucId);
            loaiVB = WebUtils.ParseInt32FromQueryString("loaivb", loaiVB);
            coQuan = WebUtils.ParseInt32FromQueryString("coquan", coQuan);
            nam = WebUtils.ParseInt32FromQueryString("nam", nam);
            keyword = WebUtils.ParseStringFromQueryString("keyword", keyword);

        }


    }
}
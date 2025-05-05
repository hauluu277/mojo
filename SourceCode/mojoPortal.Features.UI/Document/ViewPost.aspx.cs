using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
using mojoPortal.Business.WebHelpers;
using Resources;
using mojoPortal.Features;



namespace DocumentFeature.UI
{

    public partial class ViewPostPage : mojoBasePage
    {
        private int pageId = -1;
        private int moduleId = -1;
        private int linhVucId = -1;
        private int loaiVb = -1;
        private int coQuanId = -1;
        private bool userCanEdit;
        private int namBanHanh = -1;
        private string keyword = string.Empty;
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private DocumentConfiguration config = new DocumentConfiguration();
        // replace this with your own feature guid or make a static property on one of your business objects
        // like MyFeature.FeatureGuid, then you can use that instead of this variable
        private Guid featureGuid = Guid.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadParams();

            // one of these may be usefull
            //if (!UserCanViewPage(moduleId, featureGuid))
            //{
            //    SiteUtils.RedirectToAccessDeniedPage(this);
            //    return;
            //}
            //if (!UserCanEditModule(moduleId, featureGuid))
            //{
            //    SiteUtils.RedirectToAccessDeniedPage(this);
            //    return;
            //}

            LoadSettings();
            PopulateLabels();
            PopulateControls();

        }

        private void PopulateControls()
        {
            DocumentList.ModuleId = moduleId;
            DocumentList.PageId = pageId;
            DocumentList.SiteRoot = SiteRoot;
            DocumentList.IsEditable = userCanEdit;
            DocumentList.ImageSiteRoot = ImageSiteRoot;
            DocumentList.LinhVucId = linhVucId;
            DocumentList.LoaiVb = loaiVb;
            DocumentList.CoQuanId = coQuanId;
            DocumentList.NamBanHanh = namBanHanh;
            DocumentList.Keyword = keyword;
        }


        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, DocumentResources.TitDocLegalLabel);
            TitleControl.Visible = false;
            if (siteUser != null)
            {
                if (siteUser.IsInRoles("Admins"))
                {
                    TitleControl.Visible = true;
                }
            }
            TitleControl.EditUrl = SiteRoot + "/document/editpost.aspx";
            TitleControl.EditText = BlogResources.BlogAddPostLabel;
            if (userCanEdit)
            {
                TitleControl.LiteralExtraMarkup =
                    "&nbsp;<a href='"
                    + SiteRoot
                    + "/document/managepost.aspx?pageid=" + pageId.ToInvariantString()
                    + "&amp;mid=" + moduleId.ToInvariantString()
                    + "' class='ModuleEditLink' title='"
                    + BlogResources.Administration + "'>"
                    + BlogResources.Administration + "</a>"
                    ;
            }
        }

        private void LoadSettings()
        {
            userCanEdit = UserCanEditModule(moduleId);
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new DocumentConfiguration(getModuleSettings);
            LoadSideContent(config.ShowLeftPanelSetting, config.ShowRightPanelSetting);
        }

        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            linhVucId = WebUtils.ParseInt32FromQueryString("linhvucid", linhVucId);
            loaiVb = WebUtils.ParseInt32FromQueryString("loaivb", loaiVb);
            coQuanId = WebUtils.ParseInt32FromQueryString("coquan", coQuanId);
            namBanHanh = WebUtils.ParseInt32FromQueryString("nam", namBanHanh);
            keyword = WebUtils.ParseStringFromQueryString("keyword", keyword);
        }


        #region OnInit

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);

        }

        #endregion
    }
}

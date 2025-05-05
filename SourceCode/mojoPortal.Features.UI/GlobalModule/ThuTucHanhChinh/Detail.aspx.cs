using mojoPortal.Business;
using mojoPortal.Features;
using mojoPortal.Service.Business;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;



namespace ThuTucHanhChinhFeature.UI
{

    public partial class Detail : mojoBasePage
    {
        private int pageId = -1;
        private int moduleId = -1;
        private int linhVucId = -1;
        private int loaiVb = -1;
        private int coQuanId = -1;
        private int itemId = -1;
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
            DetailThuTuchanhChinh.ItemId = itemId;
        }


        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, "Chi tiết dịch vụ công trực tuyến");
            TitleControl.Visible = false;
            if (siteUser != null)
            {
                if (siteUser.IsInRoles("Admins"))
                {
                    TitleControl.Visible = true;
                }
            }
            //TitleControl.EditUrl = SiteRoot + "/document/editpost.aspx";
            //TitleControl.EditText = BlogResources.BlogAddPostLabel;
            //if (userCanEdit)
            //{
            //    TitleControl.LiteralExtraMarkup =
            //        "&nbsp;<a href='"
            //        + SiteRoot
            //        + "/GlobalModule/ThuTucHanhChinh/ManageThuTuc.aspx?pageid=" + pageId.ToInvariantString()
            //        + "&amp;mid=" + moduleId.ToInvariantString()
            //        + "' class='ModuleEditLink' title='"
            //        + BlogResources.Administration + "'>"
            //        + BlogResources.Administration + "</a>"
            //        ;
            //}
        }

        private void LoadSettings()
        {
            userCanEdit = UserCanEditModule(moduleId);
            Hashtable getModuleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new DocumentConfiguration(getModuleSettings);
            LoadSideContent(false, false);
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
            itemId = WebUtils.ParseInt32FromQueryString("itemId", itemId);
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

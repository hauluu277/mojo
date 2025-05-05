using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Features;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MediaFeature.UI
{
    public partial class ManagePost : mojoBasePage
    {
        protected MediaConfiguration config = new MediaConfiguration();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private int pageId = -1;
        private int moduleId = -1;
        private bool userCanEdit;
        private int galleryId = -1;
        override protected void OnInit(EventArgs e)
        {
            LoadParams();
            Load += Page_Load;
            base.OnInit(e);
            LoadSettings();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                SiteUtils.RedirectToLoginPage(this);
                return;
            }
            if (!WebUser.IsInRoles(WebConfigSettings.RoleGalleryManage))
            {
                SiteUtils.RedirectToAccessDeniedPage(this);
                return;
            }

            SecurityHelper.DisableBrowserCache();
  

            //if (!userCanEdit)
            //{
            //    SiteUtils.RedirectToEditAccessDeniedPage();
            //}

            PopulateLabels();

            PopulateControls();
        }
        private void PopulateControls()
        {
            AlbumList.PageId = pageId;
            AlbumList.ModuleId = moduleId;
            AlbumList.SiteRoot = SiteRoot;
        }
        private void PopulateLabels()
        {
            Title = SiteUtils.FormatPageTitle(siteSettings, MediaResources.ManageDataMultiMedia);
            TitleControl.Visible = false;
            heading.Text = "Quản lý hình ảnh thuộc phóng sự ảnh";
            //TitleControl.ModuleInstance = GetModule(moduleId);
            //if (siteUser.IsInRoles("Admins"))
            //{
            //    TitleControl.Visible = true;
            //}

        }

        private void LoadSettings()
        {
            userCanEdit = UserCanEditModule(moduleId);
            pnlContainer.ModuleId = moduleId;
        }
        private void LoadParams()
        {
            galleryId= WebUtils.ParseInt32FromQueryString("gallery", -1);
            pageId = WebUtils.ParseInt32FromQueryString("pageid", -1);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", -1);
            Hashtable moduleSettings = ModuleSettings.GetModuleSettings(moduleId);
            config = new MediaConfiguration(moduleSettings);
        }
    }
}
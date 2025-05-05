//  Author:                     
//  Created:                    2014-02-15
//	Last Modified:              2017-02-27



using log4net;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.AdminUI
{
    public partial class MenuManager : NonCmsBasePage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PageManager));

        private bool isAdmin = false;
        private bool isContentAdmin = false;
        private bool isSiteEditor = false;

        private readonly SiteSettings siteSettings = CacheHelper.GetCurrentSiteSettings();
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        private string siteRoot = SiteUtils.GetNavigationSiteRoot();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                SiteUtils.RedirectToLoginPage(this);
                return;
            }
            if (siteUser.IsInRoles("Admins") || siteUser.IsInRoles(WebConfigSettings.RoleManageMenu))
            {
                //continude
            }
            else
            {
                SiteUtils.RedirectToAccessDeniedPage();
                return;
            }
            LoadSettings();
            //PopulatePageArray();
            PopulateLabels();
            PopulateControls();
        }


        private void PopulateControls()
        {
            //BindListBox();

        }


        private void LoadSettings()
        {
            isAdmin = WebUser.IsAdmin;
            isContentAdmin = WebUser.IsContentAdmin;
            isSiteEditor = SiteUtils.UserIsSiteEditor();
        }



        private void PopulateLabels()
        {

            Title = SiteUtils.FormatPageTitle(siteSettings, PageManagerResources.PageManager);

            headingMenuMain.Text = $"Quản trị danh sách menu chính cổng {siteSettings.SiteName}";
            headingMenutop.Text = "Quản trị danh sách menu phụ cổng " + siteSettings.SiteName;
            //heading.InnerText = $"Quản trị danh sách menu cổng {siteSettings.SiteName}";
            if ((!isAdmin) && (!isSiteEditor) && (!isContentAdmin))
            {
                divAdminLinks.Visible = false;
            }



            lnkAdminMenu.Text = "Quản trị hệ thống";
            lnkAdminMenu.NavigateUrl = SiteRoot + "/Admin/AdminMenu.aspx";

            lnkPageTree.Text = "Quản lý menu";
            lnkPageTree.NavigateUrl = SiteRoot + "/Admin/MenuManager.aspx";

            lnkAltPageManager.Text = PageManagerResources.StandardPageManager;
            lnkAltPageManager.NavigateUrl = SiteRoot + "/Admin/PageTree.aspx";

            pnlMenuLeft.Visible = false;
            pnlTiengVietMenu.Visible = false;


            pnlMenuLeft.Visible = true;
            pnlTiengVietMenu.Visible = true;

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
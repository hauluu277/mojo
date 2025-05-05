//  Author:                     
//  Created:                    2014-02-15
//	Last Modified:              2017-02-27



using log4net;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.Admin.Article;
using mojoPortal.Web.Framework;
using Resources;
using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.AdminUI
{
    public partial class CategoryArticleManage : NonCmsBasePage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PageManager));

        private bool isAdmin = false;
        private bool isContentAdmin = false;
        private bool isSiteEditor = false;
        private bool canEditAnything = false;
        private int selectedPage = -1;
        //private ArrayList sitePages = new ArrayList();
        //private SiteMapDataSource siteMapDataSource;
        //private bool userCanAddPages = false;
        //protected string EditContentImage = WebConfigSettings.EditContentImage;
        //protected string EditPropertiesImage = WebConfigSettings.EditPropertiesImage;
        //protected string DeleteLinkImage = WebConfigSettings.DeleteLinkImage;

        private bool promptOnDelete = true;
        private bool promptOnMove = true;
        private bool promptOnSort = true;
        private bool showAltPageManagerLink = false;
        //private bool showDemoInfo = false;
        private string productUrl = string.Empty;
        private bool linkToViewPermissions = true;
        public int SiteId = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                SiteUtils.RedirectToLoginPage(this);
                return;
            }
            SecurityHelper.DisableBrowserCache();

            if (ArticleHelper.HasRoleArticle() == false)
            {
                SiteUtils.RedirectToAccessDeniedPage(this);
            }

            LoadSettings();

            //PopulatePageArray();
            PopulateLabels();
            PopulateControls();
        }


        private void PopulateControls()
        {
            //BindListBox();
            SiteId = siteSettings.SiteId;
        }


        private void LoadSettings()
        {
            isAdmin = WebUser.IsAdmin;
            isContentAdmin = WebUser.IsContentAdmin;
            isSiteEditor = SiteUtils.UserIsSiteEditor();
        }



        private void PopulateLabels()
        {


            Title = SiteUtils.FormatPageTitle(siteSettings, "Quản lý chuyên mục tin bài");

            heading.Text = "Quản lý chuyên mục tin bài";
            if ((!isAdmin) && (!isSiteEditor) && (!isContentAdmin))
            {
                divAdminLinks.Visible = false;


            }



            lnkAdminMenu.Text = "Quản trị hệ thống";
            lnkAdminMenu.NavigateUrl = SiteRoot + "/Admin/AdminMenu.aspx";

            lnkPageTree.Text = "Quản lý chuyên mục tin bài";
            lnkPageTree.NavigateUrl = SiteRoot + "/Admin/CategoryArticleManage.aspx";

            lnkAltPageManager.Text = PageManagerResources.StandardPageManager;
            lnkAltPageManager.NavigateUrl = SiteRoot + "/Admin/PageTree.aspx";
            if (showAltPageManagerLink)
            {
                lnkAltPageManager.Visible = true;
                altPmSeparator.Visible = true;
            }



            //if(showDemoInfo)
            //{
            //    litDemoInfo.Visible = true;
            //    if(productUrl.Length > 0)
            //    {
            //        litDemoInfo.Text = "This is a demo of <a href='" + productUrl + "'>Page Manager Pro</a>, an add on product available in the mojoPortal Store.";
            //    }
            //    else
            //    {
            //        litDemoInfo.Text = "This is a demo of Page Manager Pro, an add on product available in the mojoPortal Store.";
            //    }
            //}
        }

        #region OnInit

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);

            bool suppressMainMenu = ConfigHelper.GetBoolProperty("PageManager:SuppressMainMenu", false);
            bool suppressPageMenu = ConfigHelper.GetBoolProperty("PageManager:SuppressPageMenu", true);

            SuppressMenuSelection();

            if (suppressMainMenu)
            {
                SuppressAllMenus();
            }
            if (suppressPageMenu)
            {
                SuppressPageMenu();
            }

        }
        #endregion
    }
}
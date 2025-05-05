/// Author:             
/// Created:            2006-01-20
/// Last Modified:      2017-09-27

using System;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.UI;
using mojoPortal.Web.Framework;
using System.IO;
using System.Configuration;
using System.Linq;

namespace mojoPortal.Web
{

    public class layout : MasterPage
    {
        #region declarations moved here from designer.cs 2012-09-16


        protected global::mojoPortal.Web.UI.SiteMenu SiteMenu1;
        protected global::System.Web.UI.WebControls.Panel divLeft;
        protected global::mojoPortal.Web.UI.PageMenuControl PageMenu1;
        protected global::System.Web.UI.WebControls.ContentPlaceHolder leftContent;
        protected global::System.Web.UI.WebControls.Panel divCenter;
        protected global::System.Web.UI.WebControls.ContentPlaceHolder mainContent;
        protected global::System.Web.UI.WebControls.Panel divRight;
        protected global::System.Web.UI.WebControls.ContentPlaceHolder rightContent;
        protected global::System.Web.UI.WebControls.ContentPlaceHolder pageEditContent;


        protected global::System.Web.UI.WebControls.Button search_button;
        protected global::System.Web.UI.WebControls.TextBox search_text;
        protected global::System.Web.UI.WebControls.ImageButton btnEn;
        protected global::System.Web.UI.WebControls.ImageButton btnEnMobile;
        protected global::System.Web.UI.WebControls.ImageButton btnVi;
        protected global::System.Web.UI.WebControls.ImageButton btnViMobile;
        protected global::System.Web.UI.WebControls.TextBox txtSearchMobile;
        protected global::System.Web.UI.WebControls.Button btnMobileSearch;
        protected global::System.Web.UI.WebControls.HyperLink hplEnglish;
        protected global::System.Web.UI.WebControls.HyperLink hplVietNam;

        #endregion

        private int leftModuleCount = 0;
        private int centerModuleCount = 0;
        private int rightModuleCount = 0;
        private int alt1ModuleCount = 0;
        private int alt2ModuleCount = 0;
        protected SiteSettings siteSettings;
        protected PageSettings currentPage = null;
        private SiteMapDataSource siteMapDataSource = null;
        private SiteMapNode rootNode = null;
        protected string SkinBaseUrl = string.Empty;
        private bool useArtisteer3 = false;



        private bool hideEmptyAlt1 = true;

        private bool hideEmptyAlt2 = true;

        private string leftSideNoRightSideCss = "art-layout-cell art-sidebar1 leftside left2column";
        private string rightSideNoLeftSideCss = "art-layout-cell art-sidebar2 rightside right2column";
        private string leftAndRightNoCenterCss = string.Empty;
        private string leftOnlyCss = string.Empty;
        private string rightOnlyCss = string.Empty;
        private string centerNoLeftSideCss = "art-layout-cell art-content center-rightmargin cmszone";
        private string centerNoRightSideCss = "art-layout-cell art-content center-leftmargin cmszone";
        private string centerNoLeftOrRightSideCss = "art-layout-cell art-content-wide center-nomargins cmszone";
        private string centerWithLeftAndRightSideCss = "art-layout-cell  art-content-narrow center-rightandleftmargins cmszone";
        private string emptyCenterCss = string.Empty;
        private bool hideEmptyCenterIfOnlySidesHaveContent = false;

        protected bool isCmsPage = false;
        protected bool isNonCmsBasePage = false;
        protected bool isMobileDevice = false;
        private int mobileOnly = (int)ContentPublishMode.MobileOnly;
        private int webOnly = (int)ContentPublishMode.WebOnly;

        private int skinPageID = -1;


        private void LoadParameter()
        {
            skinPageID = WebUtils.ParseInt32FromQueryString("skinpage", skinPageID);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current == null) { return; }
            LoadParameter();
            siteSettings = CacheHelper.GetCurrentSiteSettings();

            SkinBaseUrl = SiteUtils.GetSkinBaseUrl(Page);

            isMobileDevice = SiteUtils.IsMobileDevice();

            isNonCmsBasePage = Page is NonCmsBasePage;

            if (Page is CmsPage)
            {
                isCmsPage = true;
                currentPage = CacheHelper.GetCurrentPage();
            }
            else if (!isNonCmsBasePage)
            {
                currentPage = CacheHelper.GetPage(WebUtils.ParseInt32FromQueryString("pageid", -1));
            }

            if (siteSettings == null) { return; }

            SiteSettings.UpdateView(siteSettings.SiteId, siteSettings.IsView++);

            if (Request.IsAuthenticated)

            {
                var currentUser = SiteUtils.GetCurrentSiteUser();
                if (currentUser != null)
                {
                    string pathCkfinderUser = string.Format("/Data/Sites/{0}/media/{1}/", siteSettings.SiteId, currentUser.LoginName);
                    string pathCkfinder = Server.MapPath(pathCkfinderUser);
                    if (!Directory.Exists(pathCkfinder))
                    {
                        Directory.CreateDirectory(pathCkfinder);
                    }

                    HttpCookie userInfo = new HttpCookie("user_CkfinderPath");
                    userInfo[ConfigurationManager.AppSettings["CkfinderPath"]] = pathCkfinderUser;
                    userInfo.Expires.AddDays(14);
                    Response.Cookies.Add(userInfo);
                }
                else
                {
                    HttpCookie CkfinderPath = new HttpCookie("user_CkfinderPath", string.Empty);
                    CkfinderPath.Expires = DateTime.Now.AddMinutes(1);
                    CkfinderPath.Path = "/";
                    Response.Cookies.Add(CkfinderPath);
                }
            }


            siteMapDataSource = (SiteMapDataSource)this.FindControl("SiteMapData");
            if (siteMapDataSource == null) { return; }

            siteMapDataSource.SiteMapProvider
                    = "mojosite" + siteSettings.SiteId.ToInvariantString();

            try
            {
                rootNode = siteMapDataSource.Provider.RootNode;
            }
            catch (HttpException)
            {
                return;
            }

            Control c = this.FindControl("StyleSheetCombiner");
            if ((c != null) && (c is StyleSheetCombiner))
            {
                StyleSheetCombiner style = c as StyleSheetCombiner;
                useArtisteer3 = style.UseArtisteer3;
                hideEmptyAlt1 = style.HideEmptyAlt1;
                hideEmptyAlt2 = style.HideEmptyAlt2;
            }

            if (!useArtisteer3)
            {
                Control l = this.FindControl("LayoutDisplaySettings1");
                if ((l != null) && (l is LayoutDisplaySettings))
                {
                    LayoutDisplaySettings layoutSettings = l as LayoutDisplaySettings;
                    leftSideNoRightSideCss = layoutSettings.LeftSideNoRightSideCss;
                    //rightSideNoLeftSideCss = "col-md-4";
                    rightSideNoLeftSideCss = layoutSettings.RightSideNoLeftSideCss;
                    leftAndRightNoCenterCss = layoutSettings.LeftAndRightNoCenterCss;
                    leftOnlyCss = layoutSettings.LeftOnlyCss;
                    rightOnlyCss = layoutSettings.RightOnlyCss;
                    //centerNoLeftSideCss = "col-md-8";
                    centerNoLeftSideCss = layoutSettings.CenterNoLeftSideCss;
                    centerNoRightSideCss = layoutSettings.CenterNoRightSideCss;
                    centerNoLeftOrRightSideCss = layoutSettings.CenterNoLeftOrRightSideCss;
                    centerWithLeftAndRightSideCss = layoutSettings.CenterWithLeftAndRightSideCss;
                    emptyCenterCss = layoutSettings.EmptyCenterCss;
                    hideEmptyCenterIfOnlySidesHaveContent = layoutSettings.HideEmptyCenterIfOnlySidesHaveContent;

                }
            }
            if (hplEnglish != null)
            {
                hplEnglish.NavigateUrl = siteSettings.SiteRoot + "/quangninhportal/home";
            }
            if (hplVietNam != null)
            {
                hplVietNam.NavigateUrl = "/";
            }
            SetupLayout();
        }
        protected void btnVi_Click(object sender, EventArgs e)
        {
            WebUtils.SetupRedirect(this, "/");
        }
        protected void btnViMobile_Click(object sender, EventArgs e)
        {
            WebUtils.SetupRedirect(this, "/");
        }

        protected void btnEn_Click(object sender, EventArgs e)
        {
            WebUtils.SetupRedirect(this, siteSettings.SiteRoot + "/quangninhportal/home");
        }
        protected void btnEnMobile_Click(object sender, EventArgs e)
        {
            WebUtils.SetupRedirect(this, siteSettings.SiteRoot + "/quangninhportal/home");
        }
        protected void search_button_Click(object sender, EventArgs e)
        {
            string redirectUrl = Request.RawUrl;
            if (search_text.Text.Length > 0)
            {
                redirectUrl = SiteUtils.GetNavigationSiteRoot()
                    + "/SearchArticle.aspx?&q=" + Server.UrlEncode(search_text.Text);
            }
            WebUtils.SetupRedirect(this, redirectUrl);
        }

        protected void btnMobileSearch_Click(object sender, EventArgs e)
        {
            string redirectUrl = Request.RawUrl;
            if (txtSearchMobile.Text.Length > 0)
            {
                redirectUrl = SiteUtils.GetNavigationSiteRoot()
                    + "/SearchResults.aspx?&q=" + Server.UrlEncode(txtSearchMobile.Text);
            }

            WebUtils.SetupRedirect(this, redirectUrl);
        }

        /// <summary>
        /// Count items in each of the 3 columns to determine what css class to assign to center and whether to hide side columns.
        /// This gives us automatic adjustment of column layout from 1 to 3 columns for the main layout.
        /// </summary>
        private void SetupLayout()
        {
            // Count menus if they exist within a content pane and are visible
            CountVisibleMenus();
            CountContentInstances();

            if ((hideEmptyAlt1) && (alt1ModuleCount == 0))
            {
                Control a1 = FindControl("divTop");
                if (a1 != null)
                {
                    a1.Visible = false;
                }
                else
                {
                    a1 = FindControl("divAltContent1");
                    if (a1 != null) { a1.Visible = false; }
                }
            }

            if ((hideEmptyAlt2) && (alt2ModuleCount == 0))
            {
                Control a2 = FindControl("divBottom");
                if (a2 != null) { a2.Visible = false; }
            }

            // Set css classes based on count of items in each column panel
            divLeft.Visible = (leftModuleCount > 0);
            divRight.Visible = (rightModuleCount > 0);

            if ((divLeft.Visible) && (!divRight.Visible))
            {
                divLeft.CssClass = leftSideNoRightSideCss;
            }

            if ((divRight.Visible) && (!divLeft.Visible))
            {
                divRight.CssClass = rightSideNoLeftSideCss;
            }

            if (useArtisteer3)
            {
                divCenter.CssClass =
                    divLeft.Visible
                        ? (divRight.Visible ? "art-layout-cell art-content art-content-narrow center-rightandleftmargins cmszone" : "art-layout-cell art-content center-leftmargin cmszone")
                        : (divRight.Visible ? "art-layout-cell art-content center-rightmargin cmszone" : "art-layout-cell art-content-wide center-nomargins cmszone");
            }
            else
            {
                divCenter.CssClass =
                    divLeft.Visible
                        ? (divRight.Visible ? centerWithLeftAndRightSideCss : centerNoRightSideCss)
                        : (divRight.Visible ? centerNoLeftSideCss : centerNoLeftOrRightSideCss);

            }

            //https://www.mojoportal.com/Forums/Thread.aspx?pageid=5&t=11210~1#post46748
            if ((isCmsPage) && (centerModuleCount == 0))
            {
                if ((leftModuleCount > 0) && (rightModuleCount > 0))
                {
                    if (emptyCenterCss.Length > 0) { divCenter.CssClass = emptyCenterCss; }

                    divCenter.Visible = !hideEmptyCenterIfOnlySidesHaveContent;

                    if (leftAndRightNoCenterCss.Length > 0)
                    {
                        divLeft.CssClass = leftAndRightNoCenterCss;
                        divRight.CssClass = leftAndRightNoCenterCss;
                    }

                }
                else if (leftModuleCount > 0)
                {
                    if (emptyCenterCss.Length > 0) { divCenter.CssClass = emptyCenterCss; }

                    if (leftOnlyCss.Length > 0)
                    {
                        divLeft.CssClass = leftOnlyCss;
                    }
                }
                else if (rightModuleCount > 0)
                {
                    if (emptyCenterCss.Length > 0) { divCenter.CssClass = emptyCenterCss; }

                    if (rightOnlyCss.Length > 0)
                    {
                        divRight.CssClass = rightOnlyCss;
                    }
                }

            }


            if (!IsPostBack)
            {

                divLeft.CssClass += " cmszone";
                divRight.CssClass += " cmszone";

                // these are optional panels that may exist in some skins
                // but are not part of the automatic column layout scheme
                Control alt = this.FindControl("divTop");
                if ((alt != null) && (alt is Panel))
                {
                    ((Panel)alt).CssClass += " cmszone";
                }

                alt = this.FindControl("divAlt2");
                if ((alt != null) && (alt is Panel))
                {
                    ((Panel)alt).CssClass += " cmszone";
                }

                alt = this.FindControl("divBottom");
                if ((alt != null) && (alt is Panel))
                {
                    ((Panel)alt).CssClass += " cmszone";
                }
            }


        }

        private void CountContentInstances()
        {
            //Hiển thị các module của skin page
            if (skinPageID > 0)
            {
                var moduleForPage = CoreSkinPageDefault.GetAll().Where(x => x.SkinPageID == skinPageID).ToList();
                if (moduleForPage.Any())
                {
                    foreach (var item in moduleForPage)
                    {


                        if (StringHelper.IsCaseInsensitiveMatch(item.PaneName, "leftpane"))
                        {
                            leftModuleCount++;
                        }

                        if (StringHelper.IsCaseInsensitiveMatch(item.PaneName, "rightpane"))
                        {
                            rightModuleCount++;
                        }

                        if (StringHelper.IsCaseInsensitiveMatch(item.PaneName, "contentpane"))
                        {
                            centerModuleCount++;
                        }

                        if (StringHelper.IsCaseInsensitiveMatch(item.PaneName, "toppane"))
                        {
                            alt1ModuleCount++;
                        }

                        if (StringHelper.IsCaseInsensitiveMatch(item.PaneName, "bottompane"))
                        {
                            alt2ModuleCount++;
                        }
                    }
                }

                // this is to make room for ModuleWrapper or custom usercontrols if they exsits anywhere in left or right
                foreach (Control c in divRight.Controls)
                {
                    if (c is mojoUserControl) { rightModuleCount++; }
                }

                foreach (Control c in divLeft.Controls)
                {
                    if (c is mojoUserControl) { leftModuleCount++; }
                }
            }
            else
            {
                if ((Page is CmsPage) && (currentPage != null))
                {
                    foreach (Module module in currentPage.Modules)
                    {
                        //if ((module.ControlSource == "Modules/LoginModule.ascx") && (Request.IsAuthenticated)) { continue; }
                        if (module.ControlSource == "Modules/LoginModule.ascx")
                        {
                            LoginModuleDisplaySettings loginSettings = new LoginModuleDisplaySettings();
                            this.Controls.Add(loginSettings);
                            if ((Request.IsAuthenticated) && (loginSettings.HideWhenAuthenticated)) { continue; }
                        }

                        if (ModuleIsVisible(module))
                        {
                            if (StringHelper.IsCaseInsensitiveMatch(module.PaneName, "leftpane"))
                            {
                                leftModuleCount++;
                            }

                            if (StringHelper.IsCaseInsensitiveMatch(module.PaneName, "rightpane"))
                            {
                                rightModuleCount++;
                            }

                            if (StringHelper.IsCaseInsensitiveMatch(module.PaneName, "contentpane"))
                            {
                                centerModuleCount++;
                            }

                            if (StringHelper.IsCaseInsensitiveMatch(module.PaneName, "toppane"))
                            {
                                alt1ModuleCount++;
                            }

                            if (StringHelper.IsCaseInsensitiveMatch(module.PaneName, "bottompane"))
                            {
                                alt2ModuleCount++;
                            }
                        }
                    }

                    // this is to make room for ModuleWrapper or custom usercontrols if they exsits anywhere in left or right
                    foreach (Control c in divRight.Controls)
                    {
                        if (c is mojoUserControl) { rightModuleCount++; }
                    }

                    foreach (Control c in divLeft.Controls)
                    {
                        if (c is mojoUserControl) { leftModuleCount++; }
                    }

                }
            }


        }

        private void CountVisibleMenus()
        {
            // Count menus if they exist within a content pane and are visible
            if ((SiteMenu1 != null) && SiteMenu1.Visible)
            {
                // printable view skin doesn't have a menu so it is null there
                if (SiteMenu1.Parent.ID == "divLeft") leftModuleCount++;
                if (SiteMenu1.Parent.ID == "divRight") rightModuleCount++;
            }

            Control c = this.FindControl("PageMenu1");
            if (
                (c != null)
                && (c.Visible)
                )
            {
                PageMenuControl p = (PageMenuControl)c;
                if ((!p.IsSubMenu) || (SiteUtils.TopPageHasChildren(rootNode, p.StartingNodeOffset)))
                {
                    if (c.Parent.ID == "divLeft") leftModuleCount++;
                    if (c.Parent.ID == "divRight") rightModuleCount++;
                }

            }

            c = this.FindControl("PageMenu2");
            if (
                (c != null)
                && (c.Visible)
                )
            {
                PageMenuControl p = (PageMenuControl)c;
                if (SiteUtils.TopPageHasChildren(rootNode, p.StartingNodeOffset))
                {
                    if (c.Parent.ID == "divLeft") leftModuleCount++;
                    if (c.Parent.ID == "divRight") rightModuleCount++;
                }
            }

            c = this.FindControl("PageMenu3");
            if (
                (c != null)
                && (c.Visible)
                )
            {
                PageMenuControl p = (PageMenuControl)c;
                if (SiteUtils.TopPageHasChildren(rootNode, p.StartingNodeOffset))
                {
                    if (c.Parent.ID == "divLeft") leftModuleCount++;
                    if (c.Parent.ID == "divRight") rightModuleCount++;
                }
            }

            c = this.FindControl("pnlMenu");
            if ((c != null) && (c.Parent.ID == "divLeft")) leftModuleCount++;

            c = this.FindControl("StyleSheetCombiner");
            if ((c != null) && (c is StyleSheetCombiner))
            {
                StyleSheetCombiner style = c as StyleSheetCombiner;
                if (style.AlwaysShowLeftColumn) { leftModuleCount++; }
                if (style.AlwaysShowRightColumn) { rightModuleCount++; }
            }



        }

        private bool ModuleIsVisible(Module module)
        {
            if ((module.HideFromAuthenticated) && (Request.IsAuthenticated)) { return false; }
            if ((module.HideFromUnauthenticated) && (!Request.IsAuthenticated)) { return false; }
            if (isMobileDevice && module.PublishMode == webOnly) { return false; }
            if (!isMobileDevice && module.PublishMode == mobileOnly)
            {
                if (WebConfigSettings.RolesThatAlwaysViewMobileContent.Length > 0)
                {
                    if (WebUser.IsInRoles(WebConfigSettings.RolesThatAlwaysViewMobileContent)) { return true; }
                }
                return false;
            }

            return true;
        }


    }
}

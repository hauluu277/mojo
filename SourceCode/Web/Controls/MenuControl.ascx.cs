using GCheckout.AutoGen;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web.UI;
using Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.Controls
{
    public partial class MenuControl : System.Web.UI.UserControl
    {
        private readonly SiteSettings siteSettings = CacheHelper.GetCurrentSiteSettings();
        private string siteRoot = SiteUtils.GetNavigationSiteRoot();

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Load += new EventHandler(Page_Load);
        }
        private string GenderLink(coreMenu menu)
        {
            if (menu.IsLogin)
            {
                //if (Request.IsAuthenticated)
                //{
                //    if (menu.LinkMenu.Contains(ConfigurationManager.AppSettings["URL_VANBAN"]))
                //    {
                //        SiteUser siteUser = SiteUtils.GetCurrentSiteUser();

                //        var token = siteUser.LoginName.md5EndCode();
                //        return string.Format(menu.LinkMenu, token);
                //    }
                //}
                //else
                //{
                if (Request.IsAuthenticated == false)
                {
                    if (menu.LinkMenu.Contains("https") || menu.LinkMenu.Contains("http"))
                    {
                        return menu.LinkMenu;
                    }
                    var urlLogin = SiteUtils.RedirectToLoginPage(menu.LinkMenu);
                    return urlLogin;
                }
                //}
            }
            if (menu.NoClick)
            {
                return "javascript:void(0)";
            }
            if (menu.TypeLink == MenuTypeLinkConstant.Page)
            {
                var getPage = new PageSettings(menu.SiteID, (int)menu.ItemLink.GetValueOrDefault(0));
                if (getPage.PageId > 0)
                {
                    if (getPage.Url.Contains("https") || getPage.Url.Contains("http"))
                    {
                        return getPage.Url;
                    }
                    return siteRoot + getPage.Url.Replace("~", string.Empty);
                }
            }
            else if (menu.TypeLink == MenuTypeLinkConstant.Category)
            {
                var getCategory = new CoreCategory((int)menu.ItemLink.GetValueOrDefault(0));
                if (!string.IsNullOrEmpty(getCategory.Description) && (getCategory.Description.Contains("https") || getCategory.Description.Contains("http")))
                {
                    return getCategory.Description;
                }
                else
                {
                    return siteRoot + getCategory.Description.Replace("~", string.Empty);
                }
            }
            if (menu.LinkMenu.Contains("https") || menu.LinkMenu.Contains("http"))
            {

                if (string.IsNullOrEmpty(menu.LinkMenu))
                {

                    return "javascript:void(0)";
                }


                return menu.LinkMenu;
            }

            return siteRoot + menu.LinkMenu;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                var isMobileDevice = SiteUtils.IsMobileDevice();
                if (isMobileDevice == true)
                {
                    LoadMenuMobile();
                }
                else
                {
                    LoadMenuDesktop();
                }
            }
        }

        private string GetLogo()
        {

            SiteSettings siteSettings = CacheHelper.GetCurrentSiteSettings();
            if (siteSettings == null || String.IsNullOrEmpty(siteSettings.Logo)) return string.Empty;

            string urlToUse = "~/";
            string titleToUse = siteSettings.SiteName;
            string imageUrlToUse;

            if (WebConfigSettings.SiteLogoUseMediaFolder)
            {
                imageUrlToUse = Page.ResolveUrl("~/Data/Sites/")
                + siteSettings.SiteId.ToString()
                + "/media/logos/" + siteSettings.Logo;
            }
            else
            {
                imageUrlToUse = Page.ResolveUrl("~/Data/Sites/")
                + siteSettings.SiteId.ToString()
                + "/logos/" + siteSettings.Logo;
            }

            return imageUrlToUse;

        }

        private void LoadMenuDesktop()
        {
            StringBuilder menuAppend = new StringBuilder();
            var isEnglish = false;
            if (!string.IsNullOrEmpty(WebConfigSettings.SiteEnglish))
            {
                isEnglish = WebConfigSettings.SiteEnglish.ToListInt(',').IndexOf(siteSettings.SiteId) >= 0;
            }
            var root = coreMenu.GetRoot(siteSettings.SiteId, MenuConstant.MenuMain, isEnglish).Where(x => x.Show == true).ToList();

            menuAppend.Append("<ul class='main-menu'>");
            //menu cấp 1
            //menuAppend.Append("<li>");
            //menuAppend.Append("<img src='" + GetLogo() + "'/>");
            //menuAppend.Append("</li>");
            foreach (var item in root)
            {
                if (item.OrderBy == 1 && item.LinkMenu.Contains("/trang-chu"))
                {
                    menuAppend.Append("<li><a href='/' title='" + item.Name + "'>" + item.Name + "</a></li>");

                    continue;
                }

                menuAppend.Append("<li>");
                var children = coreMenu.GetByParent(item.ItemID, isEnglish, true);
                if (children != null && children.Count > 0)
                {
                    menuAppend.Append($"<a href='{GenderLink(item)}' class='has-child' {GenderTarget(item)}>{item.Name}</a>");
                }
                else
                {
                    menuAppend.Append($"<a href='{GenderLink(item)}' {GenderTarget(item)}>{item.Name}</a>");
                }

                if (children != null && children.Count > 0)
                {
                    menuAppend.Append("<div class='menu-gt-truong'>");
                    menuAppend.Append("<div class='menu-content'>");

                    //menu cấp 2
                    foreach (var child in children)
                    {
                        menuAppend.Append($"<div class='cot pd0 wf100 {child.StyleCss}'>");
                        if (!string.IsNullOrEmpty(child.ImageUrl))
                        {
                            menuAppend.Append("<div class='wf100 imgMenu'>");
                            menuAppend.Append($"<a href='{GenderLink(child)}' {GenderTarget(child)}>");
                            menuAppend.Append($"<img src='{child.ImageUrl}'/>");
                            menuAppend.Append("</a>");
                            menuAppend.Append("</div>");
                        }
                        menuAppend.Append("<ul class='ul-content wf100'>");
                        menuAppend.Append("<li>");
                        menuAppend.Append($"<a href='{GenderLink(child)}' {GenderTarget(child)}>{child.Name}</a>");
                        var children_lever2 = coreMenu.GetByParent(child.ItemID, isEnglish, true);
                        if (children_lever2 != null && children_lever2.Count > 0)
                        {
                            menuAppend.Append("<ul class='menu-con'>");
                            //menu cấp 3
                            foreach (var child_lever2 in children_lever2)
                            {
                                menuAppend.Append("<li>");
                                menuAppend.Append("<div class='icon-menu-con'>");
                                menuAppend.Append("<i class='fa fa-angle-double-right' aria-hidden='true'></i>");
                                menuAppend.Append("</div>");
                                menuAppend.Append("<div class='content-icon-menu-con'>");
                                menuAppend.Append($"<a href='{GenderLink(child_lever2)}' {GenderTarget(child_lever2)}>{child_lever2.Name}</a>");
                                menuAppend.Append("</div>");
                                var children_lever3 = coreMenu.GetByParent(child_lever2.ItemID, isEnglish, true);
                                if (children_lever3 != null && children_lever3.Count > 0)
                                {
                                    menuAppend.Append("<ul class='menu-c3'>");
                                    //menu cấp 4
                                    foreach (var child_lever3 in children_lever3)
                                    {
                                        menuAppend.Append("<li>");
                                        menuAppend.Append("<div class='icon-menu-con'><i class='fa fa-caret-right' style='color: #5e5e5e'></i></div>");
                                        menuAppend.Append($"<div class='content-icon-menu-con'><a href='{GenderLink(child_lever3)}' {GenderTarget(child_lever3)}>{child_lever3.Name}</a></div>");

                                        var children_lever4 = coreMenu.GetByParent(child_lever3.ItemID, isEnglish, true);
                                        if (children_lever4 != null && children_lever3.Count > 0)
                                        {
                                            menuAppend.Append("<ul class='menu-c3'>");
                                            //menu cấp 5
                                            foreach (var child_lever4 in children_lever4)
                                            {
                                                menuAppend.Append("<li>");
                                                menuAppend.Append("<div class='icon-menu-con'><span class='icon-c5'>-</span></div>");
                                                menuAppend.Append($"<div class='content-icon-menu-con'><a href='{GenderLink(child_lever4)}' {GenderTarget(child_lever4)}>{child_lever4.Name}</a></div>");
                                                menuAppend.Append("</li>");
                                            }
                                            menuAppend.Append("</ul>");
                                        }
                                        menuAppend.Append("</li>");
                                    }
                                    menuAppend.Append("</ul>");
                                }
                                menuAppend.Append("</li>");
                            }
                            menuAppend.Append("</ul>");
                        }

                        menuAppend.Append("</li>");
                        menuAppend.Append("</ul>");
                        menuAppend.Append("</div>");
                    }

                    menuAppend.Append("</div>");
                    menuAppend.Append("</div>");
                }
                menuAppend.Append("</li>");
            }
            menuAppend.Append("</ul>");

            literMenu.Text = menuAppend.ToString();
        }

        private string GenderTarget(coreMenu menu)
        {
            if (menu.TargetBlank)
            {
                return "target = '_blank'";
            }

            return string.Empty;
        }

        private void LoadMenuChild(coreMenu item, bool isEnglish, ref StringBuilder menuAppend)
        {
            var children = coreMenu.GetByParent(item.ItemID, isEnglish, true);
            if (children != null && children.Count > 0)
            {
                menuAppend.Append($"<li><a href='{GenderLink(item)}' {GenderTarget(item)}>{item.Name} <span class='caret'></span></a>");
                menuAppend.Append("<ul class='dropdown-menu'>");
                foreach (var child in children)
                {
                    LoadMenuChild(child, isEnglish, ref menuAppend);
                }
                menuAppend.Append("</ul>");
                menuAppend.Append("</li>");
            }
            else
            {
                menuAppend.Append($"<li><a href='{GenderLink(item)}' {GenderTarget(item)}>{item.Name}</a></li>");
            }
        }

        private void LoadMenuMobile()
        {
            StringBuilder menuAppend = new StringBuilder();
            menuAppend.Append("<div class='navbar navbar-default' role='navigation'>");
            menuAppend.Append("<div class='navbar-header'>");
            menuAppend.Append("<button type='button' class='navbar-toggle' data-toggle='collapse' data-target='.navbar-collapse'>");
            menuAppend.Append("<span class='sr-only'>Toggle navigation</span>");
            menuAppend.Append("<span class='icon-bar'></span>");
            menuAppend.Append("<span class='icon-bar'></span>");
            menuAppend.Append("<span class='icon-bar'></span>");
            menuAppend.Append("</button>");
            menuAppend.Append("</div>");
            menuAppend.Append(" <div class='navbar-collapse collapse'>");
            menuAppend.Append("<ul class='nav navbar-nav'>");
            var isEnglish = false;
            if (!string.IsNullOrEmpty(WebConfigSettings.SiteEnglish))
            {
                isEnglish = WebConfigSettings.SiteEnglish.ToListInt(',').IndexOf(siteSettings.SiteId) >= 0;
            }
            var root = coreMenu.GetRoot(siteSettings.SiteId, MenuConstant.MenuMain, isEnglish).Where(x => x.Show == true).ToList();
            //menu cấp 1
            foreach (var item in root)
            {
                LoadMenuChild(item, isEnglish, ref menuAppend);
            }

            //trường hợp đã login
            if (Request.IsAuthenticated)
            {
                string buttonLogout = $"<a href='{SiteUtils.GetRelativeNavigationSiteRoot()}/Logoff.aspx' title='{Resource.LogoutLink}'>{Resource.LogoutLink}</a>";
                string infoUser = $"<a href='{SiteUtils.GetRelativeNavigationSiteRoot()}/Secure/UserProfile.aspx' title='{Resource.ProfileLink}'>{Resource.ProfileLink}</a>";
                menuAppend.Append("<li class='li-dangnhap-dx'>");
                menuAppend.Append($"{infoUser}");
                menuAppend.Append("<span class='class-span-pd'>|</span>");
                menuAppend.Append($"{buttonLogout}");
                menuAppend.Append("</li>");
            }
            else
            {

                //chưa login
                string buttonLogin = $"<a href='{SiteUtils.GetRelativeNavigationSiteRoot() + SiteUtils.GetLoginRelativeUrl()}?returnurl={Page.Server.UrlEncode(Page.Request.RawUrl)}' title='{Resource.LoginLink}'>{Resource.LoginLink}</a>";
                string buttonRegister = $"<a href='{SiteUtils.GetRelativeNavigationSiteRoot()}/Secure/Register.aspx' title='{Resource.RegisterLink}'>{Resource.RegisterLink}</a>";
                menuAppend.Append("<li class='li-dangnhap-dx'>");
                menuAppend.Append($"{buttonLogin}");
                menuAppend.Append("<span class='class-span-pd'>|</span>");
                menuAppend.Append($"{buttonRegister}");
                menuAppend.Append("</li>");
            }

            menuAppend.Append("</ul>");
            menuAppend.Append("</div>");
            menuAppend.Append("</div>");
            literMenu.Text = menuAppend.ToString();
        }
    }
}
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Web.Controls
{
    public partial class MenuTopControl : System.Web.UI.UserControl
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
                if (getCategory.ItemID > 0)
                {
                    if (!string.IsNullOrEmpty(getCategory.Description) && (getCategory.Description.Contains("https") || getCategory.Description.Contains("http")))
                    {
                        return getCategory.Description;
                    }
                    else
                    {
                        return siteRoot + getCategory.Description.Replace("~", string.Empty);
                    }
                }
            }
            if (string.IsNullOrEmpty(menu.LinkMenu))
            {
                return "javascript:void(0)";
            }
            if (menu.LinkMenu.Contains("https") || menu.LinkMenu.Contains("http"))
            {
                return menu.LinkMenu;
            }
            return siteRoot + menu.LinkMenu;
        }
        public string GenderLinkUnit(string link)
        {
            return System.Configuration.ConfigurationManager.AppSettings["Domain"] + link;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StringBuilder menuAppend = new StringBuilder();
                var isEnglish = false;
                if (!string.IsNullOrEmpty(WebConfigSettings.SiteEnglish))
                {
                    isEnglish = WebConfigSettings.SiteEnglish.ToListInt(',').IndexOf(siteSettings.SiteId) >= 0;
                }
                var root = coreMenu.GetRoot(1, MenuConstant.MenuTop, isEnglish).Where(x => x.Show == true).ToList();
                menuAppend.Append("<ul class='topaddres header__area__container__row__text--left__ul'>");
                //menu cấp 1
                foreach (var item in root)
                {
                    menuAppend.Append("<li class='menu-bf'>");
                    //if (root.IndexOf(item) > 0)
                    //{
                    //    menuAppend.Append($"<a class='rc lh han_disable_a' href='javascript:void(0)'>{item.Name}</a>");
                    //}
                    //else
                    //{
                    menuAppend.Append($"<a class='rc lh han_disable_a' href='{GenderLink(item)}'>{item.Name}</a>");
                    //}

                    var children = coreMenu.GetByParent(item.ItemID, isEnglish, true);
                    if ((children != null && children.Count > 0) || item.IsPhongBan)
                    {
                        menuAppend.Append("<ul class='menu-item'>");
                        //menu cấp 2
                        foreach (var child in children)
                        {
                            menuAppend.Append("<li>");
                            menuAppend.Append($"<a class='rc lh' href='{GenderLink(child)}'>{child.Name}</a>");
                            menuAppend.Append("</li>");
                        }
                        menuAppend.Append("</ul>");
                    }
                    menuAppend.Append("</li>");
                }
                menuAppend.Append("</ul>");
                literMenuLeft.Text = menuAppend.ToString();
            }
        }
    }
}
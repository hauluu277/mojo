using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

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
                    if (!string.IsNullOrEmpty(getCategory.Description) &&
                        (getCategory.Description.Contains("https") || getCategory.Description.Contains("http")))
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
                    isEnglish = WebConfigSettings.SiteEnglish.ToListInt(',').Contains(siteSettings.SiteId);
                }

                var root = coreMenu.GetRoot(1, MenuConstant.MenuTop, isEnglish)
                                   .Where(x => x.Show == true)
                                   .ToList();

                menuAppend.Append("<div class='mega-wrapper'>");

                foreach (var item in root)
                {
                    var children = coreMenu.GetByParent(item.ItemID, isEnglish, true);
                    menuAppend.Append("<div class='mega-col'>");
                    menuAppend.Append($"<h3>{HttpUtility.HtmlEncode(item.Name)}</h3>");

                    if ((children != null && children.Count > 0) || item.IsPhongBan)
                    {
                        menuAppend.Append("<ul class='submenu-vertical'>");

                        int maxChildren = 7; // Hiển thị tối đa 7 menu con
                        int count = 0;

                        foreach (var child in children)
                        {
                            if (count >= maxChildren) break;
                            menuAppend.Append("<li><a href='" + GenderLink(child) + "'>" + HttpUtility.HtmlEncode(child.Name) + "</a></li>");
                            count++;
                        }

                        // Nếu còn mục con, thêm nút Xem thêm
                        if (children.Count > maxChildren)
                        {
                            menuAppend.Append("<li><a class='see-more-menu' href='" + GenderLink(item) + "'>Xem thêm</a></li>");
                        }

                        menuAppend.Append("</ul>");
                    }

                    menuAppend.Append("</div>");
                }

                menuAppend.Append("</div>");
                literMenuLeft.Text = menuAppend.ToString();
            }
        }
    }
}

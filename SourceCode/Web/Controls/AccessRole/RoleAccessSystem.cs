///	Created:			    2005-03-24
///	Last Modified:		    2013-10-10
/// 
/// The use and distribution terms for this software are covered by the 
/// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
/// which can be found in the file CPL.TXT at the root of this distribution.
/// By using this software in any fashion, you are agreeing to be bound by 
/// the terms of this license.
///
/// You must not remove this notice, or any other, from this software.	

using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Web.Framework;
using Resources;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using System.Text;
using System.Security.Cryptography;

namespace mojoPortal.Web.UI
{

    public class RoleAccessSystem : WebControl
    {
        private SiteSettings siteSettings = null;
        // these separator properties are deprecated
        // it is recommended not to use these properties
        // but instead to use mojoPortal.Web.Controls.SeparatorControl
        private bool useLeftSeparator = false;

        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        public bool UseLeftSeparator
        {
            get { return useLeftSeparator; }
            set { useLeftSeparator = value; }
        }

        private bool renderAsListItem = false;
        public bool RenderAsListItem
        {
            get { return renderAsListItem; }
            set { renderAsListItem = value; }
        }

        private string listItemCSS = "topnavitem";
        public string ListItemCss
        {
            get { return listItemCSS; }
            set { listItemCSS = value; }
        }

        private string overrideText = string.Empty;
        public string OverrideText
        {
            get { return overrideText; }
            set { overrideText = value; }
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
            if (HttpContext.Current == null) { return; }
            siteSettings = CacheHelper.GetCurrentSiteSettings();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (HttpContext.Current == null)
            {
                writer.Write("[" + this.ID + "]");
                return;
            }

            DoRender(writer);

        }





        private void DoRender(HtmlTextWriter writer)
        {
            if (siteSettings == null) { return; }

            var hasRoleAccess = false;
            //if (!WebUser.IsInRoles(siteSettings.RolesThatCanViewMemberList)) { return; }

            StringBuilder append = new StringBuilder();
            append.Append("<ul class='admin-drawer__list nav nav-pills nav-stacked'>");

            if (HttpContext.Current.Request.IsAuthenticated)
            {
                if (WebUser.IsInRoles(WebConfigSettings.RolePublishedArticle) || WebUser.IsInRoles(WebConfigSettings.RoleApprovedArticle) || WebUser.IsInRoles(WebConfigSettings.RoleManageArticle))
                {
                    hasRoleAccess = true;
                    append.Append("<li>");
                    //append.Append("<span class='admin-drawer__list-icon admin-drawer__list-icon--page fa-stack'><i class='fa fa-plus-square fa-25x' aria-hidden='true'></i></span>");
                    append.Append("<a href='" + SiteUtils.GetRelativeNavigationSiteRoot() + WebConfigSettings.PostArticleUrl + "'>Đăng tin bài</a>");
                    append.Append("</li>");
                }

                if (WebUser.IsInRoles(WebConfigSettings.RolePublishedArticle) || WebUser.IsInRoles(WebConfigSettings.RoleApprovedArticle) || WebUser.IsInRoles(WebConfigSettings.RoleManageArticle))
                {
                    hasRoleAccess = true;
                    append.Append("<li>");
                    //append.Append("<span class='admin-drawer__list-icon admin-drawer__list-icon--page fa-stack'><i class='fa fa-windows fa-25x' aria-hidden='true'></i></span>");
                    append.Append("<a href='" + SiteUtils.GetRelativeNavigationSiteRoot() + WebConfigSettings.ManageArticleUrl + "'>Danh sách tin bài</a>");
                    append.Append("</li>");
                }


                if (WebUser.IsInRoles("Admins") || WebUser.IsInRoles(WebConfigSettings.RoleManageCategory))
                {
                    hasRoleAccess = true;
                    append.Append("<li>");
                    //append.Append("<span class='admin-drawer__list-icon admin-drawer__list-icon--page fa-stack'><i class='fa fa-windows fa-25x' aria-hidden='true'></i></span>");
                    append.Append("<a href='" + SiteUtils.GetRelativeNavigationSiteRoot() + WebConfigSettings.ManageCategoryUrl + "'>Quản lý danh mục</a>");
                    append.Append("</li>");
                }
                if (WebUser.IsInRoles(WebConfigSettings.RoleManageArticle))
                {
                    hasRoleAccess = true;
                    append.Append("<li>");
                    //append.Append("<span class='admin-drawer__list-icon admin-drawer__list-icon--page fa-stack'><i class='fa fa-windows fa-25x' aria-hidden='true'></i></span>");
                    append.Append("<a href='" + SiteUtils.GetRelativeNavigationSiteRoot() + WebConfigSettings.ManageArticleCategoryUrl + "'>Chuyên mục tin bài</a>");
                    append.Append("</li>");
                }


                if ((WebUser.IsInRoles("Admins") || WebUser.IsInRoles(WebConfigSettings.RoleManageMenu)))
                {
                    hasRoleAccess = true;
                    append.Append("<li>");
                    //append.Append("<span class='admin-drawer__list-icon admin-drawer__list-icon--page fa-stack'><i class='fa fa-windows fa-25x' aria-hidden='true'></i></span>");
                    append.Append("<a href='" + SiteUtils.GetRelativeNavigationSiteRoot() + WebConfigSettings.ManageMenuUrl + "'>Quản lý menu</a>");
                    append.Append("</li>");
                    append.Append("<li>");
                    //append.Append("<span class='admin-drawer__list-icon admin-drawer__list-icon--page fa-stack'><i class='fa fa-windows fa-25x' aria-hidden='true'></i></span>");
                    //append.Append("<a href='" + SiteUtils.GetRelativeNavigationSiteRoot() + WebConfigSettings.TuyenSinhUrl + "'>Quản lý tuyển sinh</a>");
                    //append.Append("</li>");
                }


                if (WebUser.IsInRole("Admins") && siteSettings.SiteId == 1)
                {
                    hasRoleAccess = true;
                    append.Append("<li>");
                    append.Append("<a href='/logsystem/manage.aspx' class='adminlink'>Hoạt động thành viên</a>");
                    append.Append("</li>");
                }

                hasRoleAccess = true;
                append.Append("<li>");
                append.Append("<a href='/Admin/AdminMenu.aspx' class='adminlink'>Danh sách chức năng</a>");
                append.Append("</li>");
                if (WebUser.IsInRole("Admins"))
                {
                    hasRoleAccess = true;
                    append.Append("<li>");
                    append.Append("<span style='color:red;font-weight:bold'> Site: " + siteSettings.SiteId + "</span>");
                    append.Append("</li>");
                }
                append.Append("</ul>");
            }
            if (hasRoleAccess)
            {
                writer.Write(append);
            }
        }
    }
}

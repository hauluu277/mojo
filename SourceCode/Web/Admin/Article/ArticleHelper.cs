using mojoPortal.Business.WebHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Web.Admin.Article
{
    public class ArticleHelper
    {
        public static bool HasRoleArticle()
        {
            if (WebUser.IsInRole(WebConfigSettings.RoleManageArticle)
                || WebUser.IsInRole(WebConfigSettings.RolePublishedArticle)
                || WebUser.IsInRole(WebConfigSettings.RoleApprovedArticle))
            {
                return true;
            }
            return false;
        }
    }
}
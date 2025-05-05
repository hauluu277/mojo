using mojoPortal.Business.WebHelpers;
using mojoPortal.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Features
{
    public static class CommonArticle
    {
        public static bool AccessPublisheedArticle
        {
            get
            {
                return WebUser.IsInRoles(WebConfigSettings.RolePublishedArticle);
            }
        }

        public static bool AccessApprovedArticle
        {
            get
            {
                return WebUser.IsInRoles(WebConfigSettings.RoleApprovedArticle);
            }

        }

        public static bool AccessManageArticle
        {
            get
            {
                return WebUser.IsInRoles(WebConfigSettings.RoleManageArticle);
            }
        }
    }
}
using System;
using System.Collections;
using System.Configuration;
using System.Web;

namespace mojoPortal.Business.WebHelpers
{
    public static class WebUser
    {

        public static bool AccessRole()
        {
            if (HttpContext.Current == null || HttpContext.Current.User == null) { return false; }
            if (!HttpContext.Current.Request.IsAuthenticated) return false;
            var getUser = UtilCacheHelper.GetCurrentSiteUser();
            if (getUser == null) return false;
            var getSiteSetting = UtilCacheHelper.GetCurrentSiteSettings();
            var result = false;
            if (getUser!= null && !string.IsNullOrEmpty(getUser.SiteManager))
            {
                var lstSiteManage = getUser.SiteManager.ToListInt();
                result = lstSiteManage.Contains(getSiteSetting.SiteId);
            }
            if (result == false)
            {
                HttpContext.Current.Response.Redirect("/logoff.aspx");
            }
            return result;
        }
        public static bool IsInRole(string role)
        {
            if (HttpContext.Current == null || HttpContext.Current.User == null) { return false; }
            if (role == null) { return false; }
            if (AccessRole())
            {
                if (String.IsNullOrEmpty(role)) { return false; }
                if (role.Contains("All Users")) { return true; }
                if (!HttpContext.Current.Request.IsAuthenticated) { return false; }
                if (HttpContext.Current.User.IsInRole("Admins")) { return true; }
                return HttpContext.Current.User.IsInRole(role);
            }
            return false;
        }

        public static bool IsInRoles(string roles)
        {

            if (IsInRole("Admins")) return true;
            if (String.IsNullOrEmpty(roles)) return false;
            if (roles.Contains("All Users;")) return true;
            if (!HttpContext.Current.Request.IsAuthenticated) return false;

            foreach (string role in roles.Split(new char[] { ';' }))
            {
                if (role.IndexOf("All Users") > -1) return true;
                if (IsInRole(role)) return true;
            }
            return false;
        }


        public static bool IsInRoles(IList roles)
        {
            if (AccessRole())
            {
                if (IsInRole("Admins")) return true;

                if (roles == null) return false;

                if (roles.Contains("All Users")) return true;

                if (!HttpContext.Current.Request.IsAuthenticated) return false;


                foreach (string role in roles)
                {
                    if (role.Contains("All Users")) return true;
                    if (IsInRole(role)) return true;
                }
            }
            return false;
        }


        public static bool IsAdmin
        {
            get
            {

                if (!HttpContext.Current.Request.IsAuthenticated) return false;
                if (AccessRole())
                {
                    return IsInRole("Admins");
                }
                return false;
            }
        }


        public static bool IsContentAdmin
        {
            get
            {

                if (!HttpContext.Current.Request.IsAuthenticated) return false;
                if (AccessRole())
                {
                    return IsInRole("Content Administrators");
                }
                return false;
            }
        }

        public static bool IsContentPublisher
        {
            get
            {

                if (!HttpContext.Current.Request.IsAuthenticated) return false;
                if (AccessRole())
                {
                    return IsInRole("Content Publishers");
                }
                return false;
            }
        }

        public static bool IsContentAuthor
        {
            get
            {
                if (!HttpContext.Current.Request.IsAuthenticated) return false;
                if (AccessRole())
                {
                    return IsInRole("Content Authors");
                }
                return false;
            }
        }

        public static bool IsRoleAdmin
        {
            get
            {

                if (!HttpContext.Current.Request.IsAuthenticated) return false;
                if (AccessRole())
                {
                    return IsInRole("Role Admins");
                }
                return false;
            }
        }

        public static bool IsNewsletterAdmin
        {
            get
            {
                if (!HttpContext.Current.Request.IsAuthenticated) return false;
                if (AccessRole())
                {
                    return IsInRole("Newsletter Administrators");
                }
                return false;
            }
        }


        public static bool IsAdminOrContentAdmin
        {
            get
            {
                if (AccessRole())
                { return IsAdmin || IsContentAdmin; }
                return false;
            }
        }

        public static bool IsAdminOrContentAdminOrContentAuthor
        {
            get
            {
                if (AccessRole())
                {
                    return IsAdmin || IsContentAdmin || IsContentAuthor;
                }
                return false;
            }
        }

        public static bool IsAdminOrContentAdminOrContentPublisher
        {
            get
            {
                if (AccessRole())
                {
                    return IsAdmin || IsContentAdmin || IsContentPublisher;
                }
                return false;
            }
        }

        public static bool IsAdminOrContentAdminOrContentPublisherOrContentAuthor
        {
            get
            {
                if (AccessRole())
                {
                    return IsAdmin || IsContentAdmin || IsContentPublisher || IsContentAuthor;
                }
                return false;
            }
        }


        public static bool IsAdminOrContentAdminOrRoleAdmin
        {
            get
            {
                if (AccessRole())
                {
                    return IsAdmin || IsContentAdmin || IsRoleAdmin;
                }
                return false;
            }
        }

        public static bool IsAdminOrRoleAdmin
        {
            get
            {
                if (AccessRole())
                {
                    return IsAdmin || IsRoleAdmin;
                }
                return false;
            }
        }

        public static bool IsAdminOrContentAdminOrRoleAdminOrNewsletterAdmin
        {
            get
            {
                if (AccessRole())
                {
                    return IsAdmin || IsContentAdmin || IsRoleAdmin || IsNewsletterAdmin;
                }
                return false;
            }
        }

        public static bool IsAdminOrNewsletterAdmin
        {
            get
            {
                if (AccessRole())
                {
                    return IsAdmin || IsNewsletterAdmin;
                }
                return false;

            }
        }

        //public static bool IsNotAllowedToEditModuleSettings
        //{
        //    get 
        //    {
        //        if (!HttpContext.Current.Request.IsAuthenticated) return true;
        //        if (IsAdmin) { return false; }
        //        if (IsContentAdmin) { return false; }

        //        if (ConfigurationManager.AppSettings["RolesNotAllowedToEditModuleSettings"] != null)
        //        {
        //            string forbiddenRoles = ConfigurationManager.AppSettings["RolesNotAllowedToEditModuleSettings"];
        //            if (!string.IsNullOrEmpty(forbiddenRoles))
        //            {
        //                return IsInRoles(forbiddenRoles);
        //            }
        //        }

        //        return true;
        //    }
        //}


        public static bool HasEditPermissions(int siteId, int moduleId, int pageId)
        {
            if (HttpContext.Current == null || HttpContext.Current.User == null) return false;

            if (!HttpContext.Current.Request.IsAuthenticated) return false;
            if (AccessRole())
            {
                if (IsAdmin || IsContentAdmin) return true;

                Module module = new Module(moduleId, pageId);
                PageSettings pageSettings = new PageSettings(siteId, module.PageId);

                if (pageSettings == null) return false;
                if (pageSettings.PageId < 0) return false;


                if (IsInRoles(pageSettings.EditRoles) || IsInRoles(module.AuthorizedEditRoles))
                {
                    return true;
                }

                if (module.EditUserId > 0)
                {
                    SiteSettings siteSettings = (SiteSettings)HttpContext.Current.Items["SiteSettings"];
                    SiteUser siteUser = new SiteUser(siteSettings, HttpContext.Current.User.Identity.Name);
                    if (module.EditUserId == siteUser.UserId)
                    {
                        return true;
                    }
                }
            }

            return false;
        }




        //public static bool HasPageEditPermissions(int siteID, int pageID)
        //{
        //    if (IsAdmin || IsContentAdmin) return true;
        //    PageSettings pageSettings = new PageSettings(siteID, pageID);
        //    return IsInRoles(pageSettings.EditRoles);
        //}

    }
}

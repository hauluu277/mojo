using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojoPortal.Web
{
    public partial class UserSyncTool : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateControl();
            }
        }

        private void PopulateControl()
        {
            //get list site
            var lstSite = SiteSettings.GetAllSiteID();
            var lstRoleSite1 = Role.GetbySite(1);
            var siteSettings = CacheHelper.GetCurrentSiteSettings();
            var lstUserSite1 = SiteUser.GetBySite(1);
            foreach (var item in lstUserSite1)
            {
                item.SiteSync = 1;
                item.SiteManager = "1";
                item.Save();
            }
            foreach (var site in lstSite)
            {
                if (site != 1 && site < 98) { continue; }
                var lstUser = SiteUser.GetBySite(site);
                foreach (var item in lstUser)
                {

                    if (lstUserSite1.Where(x => x.LoginName.Equals(item.LoginName)).Any()) { continue; }
                    if (item.LoginName.Equals("admin") || (item.LoginName.Contains("admin") && (item.LoginName.Length == 7 || item.LoginName.Length == 8)))
                    {
                        var lstRole = Role.GetUserRole(item.UserId);
                        //insert user for site 1
                        item.SiteSync = item.SiteId;
                        item.SiteManager = item.SiteId.ToString();
                        item.SiteId = 1;
                        item.SiteGuid = siteSettings.SiteGuid;
                        item.UserId = -1;
                        item.UserGuid = Guid.NewGuid();
                        item.Save();
                        foreach (var role in lstRole)
                        {
                            if (role.DisplayName.Equals("Admins"))
                            {
                                var getRole = lstRoleSite1.Where(x => x.RoleName.Equals("Administrators")).FirstOrDefault();
                                if (getRole != null)
                                {
                                    Role.AddUser(getRole.RoleId, item.UserId, getRole.RoleGuid, item.UserGuid);
                                }
                            }
                            else if (lstRoleSite1.Where(x => x.RoleName.Equals(role.RoleName)).Any())
                            {
                                Role.AddUser(role.RoleId, item.UserId, role.RoleGuid, item.UserGuid);
                                //RoleGroup
                            }
                        }
                    }
                }

            }

        }
    }
}
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web;

namespace mojoPortal.Features
{
    public static class CommonEvent
    {
        public static bool AccessManageEvent
        {
            get
            {
                return WebUser.IsInRoles(WebConfigSettings.RoleManageEvent);
            }
        }
    }
}
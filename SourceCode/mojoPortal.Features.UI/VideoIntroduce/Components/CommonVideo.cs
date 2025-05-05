using mojoPortal.Business.WebHelpers;
using mojoPortal.Web;

namespace mojoPortal.Features
{
    public static class CommonVideo
    {
        public static bool AccessManageVideo
        {
            get
            {
                return WebUser.IsInRoles(WebConfigSettings.RoleManageVideo);
            }
        }
    }
}
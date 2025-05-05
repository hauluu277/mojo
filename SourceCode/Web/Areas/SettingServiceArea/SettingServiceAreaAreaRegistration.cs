using System.Web.Mvc;

namespace mojoPortal.Web.Areas.SettingServiceArea
{
    public class SettingServiceAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SettingServiceArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SettingServiceArea_default",
                "SettingServiceArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
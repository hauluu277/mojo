using System.Web.Mvc;

namespace mojoPortal.Web.Areas.SettingSSOArea
{
    public class SettingSSOAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SettingSSOArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SettingSSOArea_default",
                "SettingSSOArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
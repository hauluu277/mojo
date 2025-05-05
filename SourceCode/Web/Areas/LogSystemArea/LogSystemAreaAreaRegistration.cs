using System.Web.Mvc;

namespace mojoPortal.Web.Areas.LogSystemArea
{
    public class LogSystemAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LogSystemArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LogSystemArea_default",
                "LogSystemArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
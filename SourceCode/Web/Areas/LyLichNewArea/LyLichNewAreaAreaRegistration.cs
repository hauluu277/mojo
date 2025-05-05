using System.Web.Mvc;

namespace mojoPortal.Web.Areas.LyLichNewArea
{
    public class LyLichNewAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LyLichNewArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LyLichNewArea_default",
                "LyLichNewArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
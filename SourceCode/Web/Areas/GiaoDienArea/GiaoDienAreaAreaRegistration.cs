using System.Web.Mvc;

namespace mojoPortal.Web.Areas.GiaoDienArea
{
    public class GiaoDienAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "GiaoDienArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "GiaoDienArea_default",
                "GiaoDienArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
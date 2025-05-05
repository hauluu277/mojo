using System.Web.Mvc;

namespace mojoPortal.Web.Areas.QLLienHeArea
{
    public class QLLienHeAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "QLLienHeArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "QLLienHeArea_default",
                "QLLienHeArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
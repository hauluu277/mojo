using System.Web.Mvc;

namespace mojoPortal.Web.Areas.ThuTucArea
{
    public class ThuTucAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ThuTucArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ThuTucArea_default",
                "ThuTucArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
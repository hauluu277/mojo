using System.Web.Mvc;

namespace mojoPortal.Web.Areas.BaoCaoArea
{
    public class BaoCaoAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "BaoCaoArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "BaoCaoArea_default",
                "BaoCaoArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}